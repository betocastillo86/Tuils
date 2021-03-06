using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using ImageResizer;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Media;
using Nop.Services.Configuration;
using Nop.Services.Events;
using Nop.Services.Logging;
using Nop.Services.Seo;
using Nop.Core.Domain.Common;

namespace Nop.Services.Media
{
    /// <summary>
    /// Picture service
    /// </summary>
    public partial class PictureService : IPictureService
    {
        #region Const

        private const int MULTIPLE_THUMB_DIRECTORIES_LENGTH = 3;

        #endregion

        #region Fields

        private static readonly object s_lock = new object();

        private readonly IRepository<Picture> _pictureRepository;
        private readonly IRepository<ProductPicture> _productPictureRepository;
        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;
        private readonly ILogger _logger;
        private readonly IEventPublisher _eventPublisher;
        private readonly MediaSettings _mediaSettings;
        private readonly TuilsSettings _tuilsSettings;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="pictureRepository">Picture repository</param>
        /// <param name="productPictureRepository">Product picture repository</param>
        /// <param name="settingService">Setting service</param>
        /// <param name="webHelper">Web helper</param>
        /// <param name="logger">Logger</param>
        /// <param name="eventPublisher">Event publisher</param>
        /// <param name="mediaSettings">Media settings</param>
        public PictureService(IRepository<Picture> pictureRepository,
            IRepository<ProductPicture> productPictureRepository,
            ISettingService settingService, IWebHelper webHelper,
            ILogger logger, IEventPublisher eventPublisher,
            MediaSettings mediaSettings,
            TuilsSettings tuilsSettings)
        {
            this._pictureRepository = pictureRepository;
            this._productPictureRepository = productPictureRepository;
            this._settingService = settingService;
            this._webHelper = webHelper;
            this._logger = logger;
            this._eventPublisher = eventPublisher;
            this._mediaSettings = mediaSettings;
            this._tuilsSettings = tuilsSettings;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Calculates picture dimensions whilst maintaining aspect
        /// </summary>
        /// <param name="originalSize">The original picture size</param>
        /// <param name="targetSize">The target picture size (longest side)</param>
        /// <param name="resizeType">Resize type</param>
        /// <param name="ensureSizePositive">A value indicatingh whether we should ensure that size values are positive</param>
        /// <returns></returns>
        protected virtual Size CalculateDimensions(Size originalSize, int targetSize, 
            ResizeType resizeType = ResizeType.LongestSide, bool ensureSizePositive = true)
        {
            var newSize = new Size();
            switch (resizeType)
            {
                case ResizeType.LongestSide:
                    if (originalSize.Height > originalSize.Width)
                    {
                        // portrait 
                        newSize.Width = (int)(originalSize.Width * (float)(targetSize / (float)originalSize.Height));
                        newSize.Height = targetSize;
                    }
                    else 
                    {
                        // landscape or square
                        newSize.Height = (int)(originalSize.Height * (float)(targetSize / (float)originalSize.Width));
                        newSize.Width = targetSize;
                    }
                    break;
                case ResizeType.Width:
                    newSize.Height = (int)(originalSize.Height * (float)(targetSize / (float)originalSize.Width));
                    newSize.Width = targetSize;
                    break;
                case ResizeType.Height:
                    newSize.Width = (int)(originalSize.Width * (float)(targetSize / (float)originalSize.Height));
                    newSize.Height = targetSize;
                    break;
                default:
                    throw new Exception("Not supported ResizeType");
            }

            if (ensureSizePositive)
            {
                if (newSize.Width < 1)
                    newSize.Width = 1;
                if (newSize.Height < 1)
                    newSize.Height = 1;
            }

            return newSize;
        }
        
        /// <summary>
        /// Returns the file extension from mime type.
        /// </summary>
        /// <param name="mimeType">Mime type</param>
        /// <returns>File extension</returns>
        protected virtual string GetFileExtensionFromMimeType(string mimeType)
        {
            if (mimeType == null)
                return null;

            //also see System.Web.MimeMapping for more mime types

            string[] parts = mimeType.Split('/');
            string lastPart = parts[parts.Length - 1];
            switch (lastPart)
            {
                case "pjpeg":
                    lastPart = "jpg";
                    break;
                case "x-png":
                    lastPart = "png";
                    break;
                case "x-icon":
                    lastPart = "ico";
                    break;
            }
            return lastPart;
        }

        /// <summary>
        /// Loads a picture from file
        /// </summary>
        /// <param name="pictureId">Picture identifier</param>
        /// <param name="mimeType">MIME type</param>
        /// <returns>Picture binary</returns>
        protected virtual byte[] LoadPictureFromFile(int pictureId, string mimeType)
        {
            string lastPart = GetFileExtensionFromMimeType(mimeType);
            string fileName = string.Format("{0}_0.{1}", pictureId.ToString("0000000"), lastPart);
            var filePath = GetPictureLocalPath(fileName);
            if (!File.Exists(filePath))
                return new byte[0];
            return File.ReadAllBytes(filePath);
        }

        /// <summary>
        /// Save picture on file system
        /// </summary>
        /// <param name="pictureId">Picture identifier</param>
        /// <param name="pictureBinary">Picture binary</param>
        /// <param name="mimeType">MIME type</param>
        protected virtual void SavePictureInFile(int pictureId, byte[] pictureBinary, string mimeType)
        {
            string lastPart = GetFileExtensionFromMimeType(mimeType);
            string fileName = string.Format("{0}_0.{1}", pictureId.ToString("0000000"), lastPart);
            File.WriteAllBytes(GetPictureLocalPath(fileName), pictureBinary);
        }

        /// <summary>
        /// Delete a picture on file system
        /// </summary>
        /// <param name="picture">Picture</param>
        protected virtual void DeletePictureOnFileSystem(Picture picture)
        {
            if (picture == null)
                throw new ArgumentNullException("picture");

            string lastPart = GetFileExtensionFromMimeType(picture.MimeType);
            string fileName = string.Format("{0}_0.{1}", picture.Id.ToString("0000000"), lastPart);
            string filePath = GetPictureLocalPath(fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        /// <summary>
        /// Delete picture thumbs
        /// </summary>
        /// <param name="picture">Picture</param>
        protected virtual void DeletePictureThumbs(Picture picture)
        {
            string filter = string.Format("{0}*.*", picture.Id.ToString("0000000"));
            var thumbDirectoryPath = _webHelper.MapPath("~/content/images/thumbs");
            string[] currentFiles = System.IO.Directory.GetFiles(thumbDirectoryPath, filter, SearchOption.AllDirectories);
            foreach (string currentFileName in currentFiles)
            {
                var thumbFilePath = GetThumbLocalPath(currentFileName);
                File.Delete(thumbFilePath);
            }
        }

        /// <summary>
        /// Get picture (thumb) local path
        /// </summary>
        /// <param name="thumbFileName">Filename</param>
        /// <returns>Local picture thumb path</returns>
        protected virtual string GetThumbLocalPath(string thumbFileName)
        {
            var thumbsDirectoryPath = _webHelper.MapPath("~/content/images/thumbs");
            if (_mediaSettings.MultipleThumbDirectories)
            {
                //get the first two letters of the file name
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(thumbFileName);
                if (fileNameWithoutExtension != null && fileNameWithoutExtension.Length > MULTIPLE_THUMB_DIRECTORIES_LENGTH)
                {
                    var subDirectoryName = fileNameWithoutExtension.Substring(0, MULTIPLE_THUMB_DIRECTORIES_LENGTH);
                    thumbsDirectoryPath = Path.Combine(thumbsDirectoryPath, subDirectoryName);
                    if (!System.IO.Directory.Exists(thumbsDirectoryPath))
                    {
                        System.IO.Directory.CreateDirectory(thumbsDirectoryPath);
                    }
                }
            }
            var thumbFilePath = Path.Combine(thumbsDirectoryPath, thumbFileName);
            return thumbFilePath;
        }

        /// <summary>
        /// Get picture (thumb) URL 
        /// </summary>
        /// <param name="thumbFileName">Filename</param>
        /// <param name="storeLocation">Store location URL; null to use determine the current store location automatically</param>
        /// <returns>Local picture thumb path</returns>
        protected virtual string GetThumbUrl(string thumbFileName, string storeLocation = null)
        {
            storeLocation = !String.IsNullOrEmpty(storeLocation)
                                    ? storeLocation
                                    : _webHelper.GetStoreLocation();
            var url = storeLocation + "content/images/thumbs/";

            if (_mediaSettings.MultipleThumbDirectories)
            {
                //get the first two letters of the file name
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(thumbFileName);
                if (fileNameWithoutExtension != null && fileNameWithoutExtension.Length > MULTIPLE_THUMB_DIRECTORIES_LENGTH)
                {
                    var subDirectoryName = fileNameWithoutExtension.Substring(0, MULTIPLE_THUMB_DIRECTORIES_LENGTH);
                    url = url + subDirectoryName + "/";
                }
            }

            url = url + thumbFileName;
            return url;
        }

        /// <summary>
        /// Get picture local path. Used when images stored on file system (not in the database)
        /// </summary>
        /// <param name="fileName">Filename</param>
        /// <param name="imagesDirectoryPath">Directory path with images; if null, then default one is used</param>
        /// <returns>Local picture path</returns>
        protected virtual string GetPictureLocalPath(string fileName, string imagesDirectoryPath = null)
        {
            if (String.IsNullOrEmpty(imagesDirectoryPath))
            {
                imagesDirectoryPath = _webHelper.MapPath("~/content/images/");
            }
            var filePath = Path.Combine(imagesDirectoryPath, fileName);
            return filePath;
        }

        /// <summary>
        /// Gets the loaded picture binary depending on picture storage settings
        /// </summary>
        /// <param name="picture">Picture</param>
        /// <param name="fromDb">Load from database; otherwise, from file system</param>
        /// <returns>Picture binary</returns>
        protected virtual byte[] LoadPictureBinary(Picture picture, bool fromDb)
        {
            if (picture == null)
                throw new ArgumentNullException("picture");

            var result = fromDb 
                ? picture.PictureBinary
                : LoadPictureFromFile(picture.Id, picture.MimeType);
            return result;
        }

        #endregion

        #region Getting picture local path/URL methods

        /// <summary>
        /// Gets the loaded picture binary depending on picture storage settings
        /// </summary>
        /// <param name="picture">Picture</param>
        /// <returns>Picture binary</returns>
        public virtual byte[] LoadPictureBinary(Picture picture)
        {
            return LoadPictureBinary(picture, this.StoreInDb);
        }

        /// <summary>
        /// Get picture SEO friendly name
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>Result</returns>
        public virtual string GetPictureSeName(string name)
        {
            return SeoExtensions.GetSeName(name, true, false);
        }

        /// <summary>
        /// Gets the default picture URL
        /// </summary>
        /// <param name="targetSize">The target picture size (longest side)</param>
        /// <param name="defaultPictureType">Default picture type</param>
        /// <param name="storeLocation">Store location URL; null to use determine the current store location automatically</param>
        /// <returns>Picture URL</returns>
        public virtual string GetDefaultPictureUrl(int targetSize = 0, 
            PictureType defaultPictureType = PictureType.Entity,
            string storeLocation = null,
            bool crop = false)
        {
            string defaultImageFileName;
            switch (defaultPictureType)
            {
                case PictureType.Entity:
                    defaultImageFileName = _settingService.GetSettingByKey("Media.DefaultImageName", "default-image.gif");
                    break;
                case PictureType.Avatar:
                    defaultImageFileName = _settingService.GetSettingByKey("Media.Customer.DefaultAvatarImageName", "default-avatar.jpg");
                    break;
                default:
                    defaultImageFileName = _settingService.GetSettingByKey("Media.DefaultImageName", "default-image.gif");
                    break;
            }

            string filePath = GetPictureLocalPath(defaultImageFileName,
                imagesDirectoryPath: _settingService.GetSettingByKey<string>("Media.DefaultImageDirectoryPath"));

            if (!File.Exists(filePath))
            {
                return "";
            }
            if (targetSize == 0)
            {
                string url = (!String.IsNullOrEmpty(storeLocation)
                                 ? storeLocation
                                 : _webHelper.GetStoreLocation())
                                 + "content/images/" + defaultImageFileName;
                return url;
            }
            else
            {
                string fileExtension = Path.GetExtension(filePath);
                string thumbFileName = string.Format("{0}_{1}{2}",
                    Path.GetFileNameWithoutExtension(filePath),
                    targetSize,
                    fileExtension);
                var thumbFilePath = GetThumbLocalPath(thumbFileName);
                if (!File.Exists(thumbFilePath))
                {
                    using (var b = new Bitmap(filePath))
                    {
                        var newSize = CalculateDimensions(b.Size, targetSize);

                        var destStream = new MemoryStream();

                        //Si debe cortar la imagen carga el ancho y alto fijo
                        ResizeSettings settings;
                        if(crop)
                            settings = new ResizeSettings
                            {
                                Width = newSize.Width,
                                Height = newSize.Width,
                                Mode = FitMode.Crop
                            };
                        else
                            settings = new ResizeSettings
                            {
                                Width = newSize.Width,
                                Height = newSize.Height,
                                Scale = ScaleMode.Both,
                                Quality = _mediaSettings.DefaultImageQuality
                            };



                        ImageBuilder.Current.Build(b, destStream, settings);
                        var destBinary = destStream.ToArray();
                        File.WriteAllBytes(thumbFilePath, destBinary);
                    }
                }
                var url = GetThumbUrl(thumbFileName, storeLocation);
                return url;
            }
        }

        /// <summary>
        /// Retorna un cover por defecto para una tienda, el numero lo toma aleatoreo
        /// </summary>
        /// <returns></returns>
        public virtual string GetDefaultCoverLocation()
        {
            return GetPictureLocalPath(string.Format("defaultCovers\\{0}.jpg", new Random().Next(1,8)),
                imagesDirectoryPath: _settingService.GetSettingByKey<string>("Media.DefaultImageDirectoryPath"));
        }

        /// <summary>
        /// Get a picture URL
        /// </summary>
        /// <param name="pictureId">Picture identifier</param>
        /// <param name="targetSize">The target picture size (longest side)</param>
        /// <param name="showDefaultPicture">A value indicating whether the default picture is shown</param>
        /// <param name="storeLocation">Store location URL; null to use determine the current store location automatically</param>
        /// <param name="defaultPictureType">Default picture type</param>
        /// <returns>Picture URL</returns>
        public virtual string GetPictureUrl(int pictureId,
            int targetSize = 0,
            bool showDefaultPicture = true, 
            string storeLocation = null, 
            PictureType defaultPictureType = PictureType.Entity)
        {
            var picture = GetPictureById(pictureId);
            return GetPictureUrl(picture, targetSize, showDefaultPicture, storeLocation, defaultPictureType);
        }
        
        /// <summary>
        /// Get a picture URL
        /// </summary>
        /// <param name="picture">Picture instance</param>
        /// <param name="targetSize">The target picture size (longest side)</param>
        /// <param name="showDefaultPicture">A value indicating whether the default picture is shown</param>
        /// <param name="storeLocation">Store location URL; null to use determine the current store location automatically</param>
        /// <param name="defaultPictureType">Default picture type</param>
        /// <returns>Picture URL</returns>
        public virtual string GetPictureUrl(Picture picture, 
            int targetSize = 0,
            bool showDefaultPicture = true, 
            string storeLocation = null, 
            PictureType defaultPictureType = PictureType.Entity,
            bool crop = false,
            bool cropProportional = false,
            string[] watermarks = null,
            int heightSize = 0)
        {
            string url = string.Empty;
            byte[] pictureBinary = null;
            if (picture != null)
                pictureBinary = LoadPictureBinary(picture);
            if (picture == null || pictureBinary == null || pictureBinary.Length == 0)
            {
                if(showDefaultPicture)
                {
                    url = GetDefaultPictureUrl(targetSize, defaultPictureType, storeLocation);
                }
                return url;
            }

            string lastPart = GetFileExtensionFromMimeType(picture.MimeType);
            string thumbFileName;
            string complementNameCroppedPicture = crop ? "_c" : string.Empty;
            string queryResizeOptions = watermarks != null && watermarks.Length > 0 ? string.Format("watermark={0}", string.Join(",", watermarks)) : string.Empty;

            if (picture.IsNew)
            {
                DeletePictureThumbs(picture);

                //we do not validate picture binary here to ensure that no exception ("Parameter is not valid") will be thrown
                picture = UpdatePicture(picture.Id, 
                    pictureBinary, 
                    picture.MimeType, 
                    picture.SeoFilename, 
                    false, 
                    false);
            }
            lock (s_lock)
            {
                string seoFileName = picture.SeoFilename; // = GetPictureSeName(picture.SeoFilename); //just for sure
                if (targetSize == 0)
                {
                    thumbFileName = !String.IsNullOrEmpty(seoFileName) ?
                        string.Format("{0}_{1}{3}.{2}", picture.Id.ToString("0000000"), seoFileName, lastPart, complementNameCroppedPicture) :
                        string.Format("{0}{2}.{1}", picture.Id.ToString("0000000"), lastPart, complementNameCroppedPicture);
                    var thumbFilePath = GetThumbLocalPath(thumbFileName);
                    if (!File.Exists(thumbFilePath))
                    {
                        File.WriteAllBytes(thumbFilePath, pictureBinary);
                    }
                }
                else
                {
                    thumbFileName = !String.IsNullOrEmpty(seoFileName) ?
                        string.Format("{0}_{1}_{2}{4}.{3}", picture.Id.ToString("0000000"), seoFileName, targetSize, lastPart, complementNameCroppedPicture) :
                        string.Format("{0}_{1}{3}.{2}", picture.Id.ToString("0000000"), targetSize, lastPart, complementNameCroppedPicture);
                    var thumbFilePath = GetThumbLocalPath(thumbFileName);
                    if (!File.Exists(thumbFilePath))
                    {
                        using (var stream = new MemoryStream(pictureBinary))
                        {
                            Bitmap b = null;
                            try
                            {
                                //try-catch to ensure that picture binary is really OK. Otherwise, we can get "Parameter is not valid" exception if binary is corrupted for some reasons
                                b = new Bitmap(stream);
                            }
                            catch (ArgumentException exc)
                            {
                                _logger.Error(string.Format("Error generating picture thumb. ID={0}", picture.Id), exc);
                            }
                            if (b == null)
                            {
                                //bitmap could not be loaded for some reasons
                                return url;
                            }

                            var newSize = CalculateDimensions(b.Size, targetSize);

                            var destStream = new MemoryStream();
                            
                            //Si debe cortar la imagen carga el ancho y alto fijo
                            ResizeSettings settings;
                            if (crop)
                            {
                                if (newSize.Width > newSize.Height)
                                    newSize.Height = newSize.Width;
                                else
                                    newSize.Width = newSize.Height;
                                
                                settings = new ResizeSettings(queryResizeOptions)
                                {
                                    Width = newSize.Width,
                                    Height = newSize.Width,
                                    Mode = FitMode.Crop,
                                    Quality = _mediaSettings.DefaultImageQuality
                                };
                            }
                            else if (cropProportional)
                            {
                                settings = new ResizeSettings(queryResizeOptions)
                                {
                                    Width = targetSize,
                                    Height = heightSize,
                                    Mode = FitMode.Crop,
                                    Scale = ScaleMode.Both,
                                    Quality = _mediaSettings.DefaultImageQuality
                                };
                            }
                            else
                            {
                                settings = new ResizeSettings(queryResizeOptions)
                                {
                                    Width = newSize.Width,
                                    Height = newSize.Height,
                                    Scale = ScaleMode.Both,
                                    Quality = _mediaSettings.DefaultImageQuality
                                };
                            }
                                


                            ImageBuilder.Current.Build(b, destStream, settings);
                            var destBinary = destStream.ToArray();
                            File.WriteAllBytes(thumbFilePath, destBinary);

                            b.Dispose();
                        }
                    }
                }
            }
            url = GetThumbUrl(thumbFileName, storeLocation);
            return url;
        }

        /// <summary>
        /// Crea el Thumbnail de una imagen sin importar si est� creada en BD o no
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="targetSize"></param>
        /// <returns></returns>
        public virtual string CreateThumbnailImage(string filePath, int targetSize, bool crop= false)
        {
            string fileExtension = Path.GetExtension(filePath);
            string thumbFileName = string.Format("{0}_{1}{2}",
                    Path.GetFileNameWithoutExtension(filePath),
                    targetSize,
                    fileExtension);
            string directory = Path.GetDirectoryName(filePath);

            //Obtiene la ruta del nuevo archivo
           // var relativeThumbPath = string.Format("{0}{1}", directory, thumbFileName);
            var thumbFilePath = string.Format("{0}\\{1}", directory, thumbFileName);
            
            if (!File.Exists(thumbFilePath))
            {
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    Bitmap b = null;
                    try
                    {
                        //try-catch to ensure that picture binary is really OK. Otherwise, we can get "Parameter is not valid" exception if binary is corrupted for some reasons
                        b = new Bitmap(stream);
                    }
                    catch (ArgumentException exc)
                    {
                        _logger.Error(string.Format("Error generating picture thumb. Url={0}", filePath), exc);
                    }
                    if (b == null)
                    {
                        //bitmap could not be loaded for some reasons
                        return filePath;
                    }

                    var newSize = CalculateDimensions(b.Size, targetSize);

                    var destStream = new MemoryStream();

                    //Si debe cortar la imagen carga el ancho y alto fijo
                    ResizeSettings settings;
                    if (crop)
                    {
                        if (newSize.Width > newSize.Height)
                            newSize.Height = newSize.Width;
                        else
                            newSize.Width = newSize.Height;

                        settings = new ResizeSettings
                        {
                            Width = newSize.Width,
                            Height = newSize.Width,
                            Mode = FitMode.Crop,
                            Quality = _mediaSettings.DefaultImageQuality
                        };
                    }
                    else
                        settings = new ResizeSettings
                        {
                            Width = newSize.Width,
                            Height = newSize.Height,
                            Scale = ScaleMode.Both,
                            Quality = _mediaSettings.DefaultImageQuality
                        };


                    ImageBuilder.Current.Build(b, destStream, settings);
                    var destBinary = destStream.ToArray();
                    File.WriteAllBytes(thumbFilePath, destBinary);

                    b.Dispose();
                }
            }

            return thumbFilePath;
        }

        /// <summary>
        /// Get a picture local path
        /// </summary>
        /// <param name="picture">Picture instance</param>
        /// <param name="targetSize">The target picture size (longest side)</param>
        /// <param name="showDefaultPicture">A value indicating whether the default picture is shown</param>
        /// <returns></returns>
        public virtual string GetThumbLocalPath(Picture picture, int targetSize = 0, bool showDefaultPicture = true)
        {
            string url = GetPictureUrl(picture, targetSize, showDefaultPicture);
            if(String.IsNullOrEmpty(url))
                return String.Empty;
            
            return GetThumbLocalPath(Path.GetFileName(url));
        }

        #endregion

        #region CRUD methods

        /// <summary>
        /// Gets a picture
        /// </summary>
        /// <param name="pictureId">Picture identifier</param>
        /// <returns>Picture</returns>
        public virtual Picture GetPictureById(int pictureId)
        {
            if (pictureId == 0)
                return null;

            return _pictureRepository.GetById(pictureId);
        }

        /// <summary>
        /// Deletes a picture
        /// </summary>
        /// <param name="picture">Picture</param>
        public virtual void DeletePicture(Picture picture)
        {
            if (picture == null)
                throw new ArgumentNullException("picture");

            //delete thumbs
            DeletePictureThumbs(picture);
            
            //delete from file system
            if (!this.StoreInDb)
                DeletePictureOnFileSystem(picture);

            //delete from database
            _pictureRepository.Delete(picture);

            //event notification
            _eventPublisher.EntityDeleted(picture);
        }

        /// <summary>
        /// Gets a collection of pictures
        /// </summary>
        /// <param name="pageIndex">Current page</param>
        /// <param name="pageSize">Items on each page</param>
        /// <returns>Paged list of pictures</returns>
        public virtual IPagedList<Picture> GetPictures(int pageIndex, int pageSize)
        {
            var query = from p in _pictureRepository.Table
                       orderby p.Id descending
                       select p;
            var pics = new PagedList<Picture>(query, pageIndex, pageSize);
            return pics;
        }
        

        /// <summary>
        /// Gets pictures by product identifier
        /// </summary>
        /// <param name="productId">Product identifier</param>
        /// <param name="recordsToReturn">Number of records to return. 0 if you want to get all items</param>
        /// <param name="onlyActive">Solo retorna las activas, por defecto est� apagado</param>
        /// <returns>Pictures</returns>
        public virtual IList<Picture> GetPicturesByProductId(int productId, int recordsToReturn = 0, bool onlyActive = false)
        {
            if (productId == 0)
                return new List<Picture>();

            var productPicture = _productPictureRepository.Table.Where(p => p.ProductId == productId);

            //Si es solo activo, debe filtrar los productos
            if (onlyActive)
                productPicture = productPicture.Where(p => p.Active);


            var query = from p in _pictureRepository.Table
                        //join pp in _productPictureRepository.Table on p.Id equals pp.PictureId
                        join pp in productPicture on p.Id equals pp.PictureId
                        orderby pp.DisplayOrder
                        where pp.ProductId == productId
                        select p;

            if (recordsToReturn > 0)
                query = query.Take(recordsToReturn);

            var pics = query.ToList();
            return pics;
        }

        /// <summary>
        /// Inserts a picture
        /// </summary>
        /// <param name="pictureBinary">The picture binary</param>
        /// <param name="mimeType">The picture MIME type</param>
        /// <param name="seoFilename">The SEO filename</param>
        /// <param name="isNew">A value indicating whether the picture is new</param>
        /// <param name="validateBinary">A value indicating whether to validated provided picture binary</param>
        /// <returns>Picture</returns>
        public virtual Picture InsertPicture(byte[] pictureBinary, string mimeType, string seoFilename,
            bool isNew, bool validateBinary = true)
        {
            mimeType = CommonHelper.EnsureNotNull(mimeType);
            mimeType = CommonHelper.EnsureMaximumLength(mimeType, 20);

            seoFilename = CommonHelper.EnsureMaximumLength(seoFilename, 100);

            if (validateBinary)
                pictureBinary = ValidatePicture(pictureBinary, mimeType);

            var picture = new Picture
                              {
                                  PictureBinary = this.StoreInDb ? pictureBinary : new byte[0],
                                  MimeType = mimeType,
                                  SeoFilename = seoFilename,
                                  IsNew = isNew,
                              };
            _pictureRepository.Insert(picture);

            if(!this.StoreInDb)
                SavePictureInFile(picture.Id, pictureBinary, mimeType);
            
            //event notification
            _eventPublisher.EntityInserted(picture);

            return picture;
        }

        /// <summary>
        /// Updates the picture
        /// </summary>
        /// <param name="pictureId">The picture identifier</param>
        /// <param name="pictureBinary">The picture binary</param>
        /// <param name="mimeType">The picture MIME type</param>
        /// <param name="seoFilename">The SEO filename</param>
        /// <param name="isNew">A value indicating whether the picture is new</param>
        /// <param name="validateBinary">A value indicating whether to validated provided picture binary</param>
        /// <returns>Picture</returns>
        public virtual Picture UpdatePicture(int pictureId, byte[] pictureBinary, string mimeType,
            string seoFilename, bool isNew, bool validateBinary = true)
        {
            mimeType = CommonHelper.EnsureNotNull(mimeType);
            mimeType = CommonHelper.EnsureMaximumLength(mimeType, 20);

            seoFilename = CommonHelper.EnsureMaximumLength(seoFilename, 100);

            if (validateBinary)
                pictureBinary = ValidatePicture(pictureBinary, mimeType);

            var picture = GetPictureById(pictureId);
            if (picture == null)
                return null;

            //delete old thumbs if a picture has been changed
            if (seoFilename != picture.SeoFilename)
                DeletePictureThumbs(picture);

            picture.PictureBinary = (this.StoreInDb ? pictureBinary : new byte[0]);
            picture.MimeType = mimeType;
            picture.SeoFilename = seoFilename;
            picture.IsNew = isNew;

            _pictureRepository.Update(picture);

            if(!this.StoreInDb)
                SavePictureInFile(picture.Id, pictureBinary, mimeType);

            //event notification
            _eventPublisher.EntityUpdated(picture);

            return picture;
        }

        /// <summary>
        /// Updates a SEO filename of a picture
        /// </summary>
        /// <param name="pictureId">The picture identifier</param>
        /// <param name="seoFilename">The SEO filename</param>
        /// <returns>Picture</returns>
        public virtual Picture SetSeoFilename(int pictureId, string seoFilename)
        {
            var picture = GetPictureById(pictureId);
            if (picture == null)
                throw new ArgumentException("No picture found with the specified id");

            //update if it has been changed
            if (seoFilename != picture.SeoFilename)
            {
                //update picture
                picture = UpdatePicture(picture.Id, LoadPictureBinary(picture), picture.MimeType, seoFilename, true, false);
            }
            return picture;
        }

        /// <summary>
        /// Validates input picture dimensions
        /// </summary>
        /// <param name="pictureBinary">Picture binary</param>
        /// <param name="mimeType">MIME type</param>
        /// <returns>Picture binary or throws an exception</returns>
        public virtual byte[] ValidatePicture(byte[] pictureBinary, string mimeType)
        {
            var destStream = new MemoryStream();
            ImageBuilder.Current.Build(pictureBinary, destStream, new ResizeSettings
            {
                MaxWidth = _mediaSettings.MaximumImageSize,
                MaxHeight = _mediaSettings.MaximumImageSize,
                Quality = _mediaSettings.DefaultImageQuality
            });
            return destStream.ToArray();
        }
        
        #endregion

        #region TempFiles
        /// <summary>
        /// Retorna los bytes de un archivo temporal buscando por el nombre
        /// </summary>
        /// <param name="tempFileName">nombre del archivo temporal. Cambia el valor del ingresado por la ruta completa del archivo</param>
        /// <returns>bytes con la informaci�n del archivo que correspondiente al nombre</returns>
        public byte[] GetTempFile(ref string tempFileName)
        {
            tempFileName = Path.Combine(_webHelper.MapPath(_tuilsSettings.tempUploadFiles), tempFileName);
            return File.Exists(tempFileName) ? File.ReadAllBytes(tempFileName) : null;
        }

        /// <summary>
        /// Inserta registros en la tabla Picture desde un listado de archivos temporales asociandolos a un producto
        /// </summary>
        /// <param name="tempFiles">Listado de nombres de archivos temporales</param>
        /// <param name="productId">Id del producto al que se va asociar</param>
        /// <returns>Listado de Picture insertados</returns>
        public IList<Picture> InsertPicturesFromTempFiles(string[] tempFiles)
        {
            var pictures = new List<Picture>();
            
            for (int iTempFile = 0; iTempFile < tempFiles.Length; iTempFile++)
            {
                string fileName = tempFiles[iTempFile];
                var  file = GetTempFile(ref fileName);
                if (file != null)
                {
                    var picture = InsertPicture(file, GetContentTypeFromExtension(Path.GetExtension(fileName)), null, false);
                    if (picture.Id > 0)
                    {
                        try
                        {
                            File.Delete(fileName);
                        }
                        catch (Exception e)
                        {
                            _logger.Error(e.ToString());
                        }
                        pictures.Add(picture);
                    }
                        
                }
            }

            return pictures;
        }

        /// <summary>
        /// Elimina todas las fotos temporales que se han cargado 
        /// </summary>
        /// <param name="tempFiles"></param>
        /// <param name="resizes"></param>
        public void RemovePicturesFromTempFiles(string[] tempFiles, params int[] resizes)
        {
            for (int iTempFile = 0; iTempFile < tempFiles.Length; iTempFile++)
            {
                string fileName = tempFiles[iTempFile];

                string fullName = Path.Combine(_webHelper.MapPath(_tuilsSettings.tempUploadFiles), fileName);

                //Elimina los thumbnails que se crearon
                foreach (var size in resizes)
                {
                    string fullNameSize = Path.Combine(_webHelper.MapPath(_tuilsSettings.tempUploadFiles), string.Format("{0}_{1}{2}", Path.GetFileNameWithoutExtension(fileName), size, Path.GetExtension(fileName)));
                    try
                    {
                        File.Delete(fullNameSize);
                    }
                    catch (Exception e)
                    {
                        _logger.Error(e.ToString());
                    }
                }

                try
                {
                    File.Delete(fullName);
                }
                catch (Exception e)
                {
                    _logger.Error(e.ToString());
                }
            }
        }

        
        #endregion

        /// <summary>
        /// Retorna el tipo de contenido desde la extension
        /// </summary>
        /// <param name="fileExtension"></param>
        /// <returns></returns>
        public string GetContentTypeFromExtension(string fileExtension)
        {
            string contentType = string.Empty;
            //contentType is not always available 
            //that's why we manually update it here
            //http://www.sfsu.edu/training/mimetype.htm
            if (String.IsNullOrEmpty(contentType))
            {
                switch (fileExtension.ToLower())
                {
                    case ".bmp":
                        contentType = "image/bmp";
                        break;
                    case ".gif":
                        contentType = "image/gif";
                        break;
                    case ".jpeg":
                    case ".jpg":
                    case ".jpe":
                    case ".jfif":
                    case ".pjpeg":
                    case ".pjp":
                        contentType = "image/jpeg";
                        break;
                    case ".png":
                        contentType = "image/png";
                        break;
                    case ".tiff":
                    case ".tif":
                        contentType = "image/tiff";
                        break;
                    default:
                        break;
                }
            }

            return contentType;
        }

        #region Properties
        
        /// <summary>
        /// Gets or sets a value indicating whether the images should be stored in data base.
        /// </summary>
        public virtual bool StoreInDb
        {
            get
            {
                return _settingService.GetSettingByKey("Media.Images.StoreInDB", true);
            }
            set
            {
                //check whether it's a new value
                if (this.StoreInDb != value)
                {
                    //save the new setting value
                    _settingService.SetSetting("Media.Images.StoreInDB", value);

                    //update all picture objects
                    var pictures = this.GetPictures(0, int.MaxValue);
                    foreach (var picture in pictures)
                    {
                        var pictureBinary = LoadPictureBinary(picture, !value);

                        //delete from file system
                        if (value)
                            DeletePictureOnFileSystem(picture);

                        //just update a picture (all required logic is in UpdatePicture method)
                        UpdatePicture(picture.Id,
                                      pictureBinary,
                                      picture.MimeType,
                                      picture.SeoFilename,
                                      true,
                                      false);
                        //we do not validate picture binary here to ensure that no exception ("Parameter is not valid") will be thrown when "moving" pictures
                    }
                }
            }
        }

        #endregion

        #region Correct Image Orientation From Phone EXIF
        /// <summary>
        /// Realiza la validaci�n y reorientaci�n de una imagen si est� qued� torcida
        /// </summary>
        /// <param name="originalFile"></param>
        /// <returns></returns>
        public bool CorrectImageOrientationEXIF(string originalFile)
        {
            var image = System.Drawing.Image.FromFile(originalFile);
            return CorrectImageOrientationEXIF(image, originalFile);
        }

        public bool CorrectImageOrientationEXIF(System.IO.Stream originalFile, string pathToSave)
        {
            var image = System.Drawing.Image.FromStream(originalFile);
            return CorrectImageOrientationEXIF(image, pathToSave);
        }

        private bool CorrectImageOrientationEXIF(Image image, string pathToSave)
        {

            try
            {
                //Valor quemado que contiene el valor del header
                int orientationEXIFHeader = 274;

                if (image.PropertyIdList.Contains(orientationEXIFHeader))
                {
                    var orientationProperty = image.GetPropertyItem(orientationEXIFHeader);
                    var orientation = (EXIFPictureOrientationType)orientationProperty.Value[0];

                    var rotationToApply = RotateFlipType.RotateNoneFlipNone;

                    switch (orientation)
                    {
                        case EXIFPictureOrientationType.TopLeft:
                            rotationToApply = System.Drawing.RotateFlipType.RotateNoneFlipNone;
                            break;
                        case EXIFPictureOrientationType.TopRight:
                            rotationToApply = System.Drawing.RotateFlipType.RotateNoneFlipX;
                            break;
                        case EXIFPictureOrientationType.BottomRight:
                            rotationToApply = System.Drawing.RotateFlipType.Rotate180FlipNone;
                            break;
                        case EXIFPictureOrientationType.BottomLeft:
                            rotationToApply = System.Drawing.RotateFlipType.Rotate180FlipX;
                            break;
                        case EXIFPictureOrientationType.LeftTop:
                            rotationToApply = System.Drawing.RotateFlipType.Rotate90FlipX;
                            break;
                        case EXIFPictureOrientationType.RightTop:
                            rotationToApply = System.Drawing.RotateFlipType.Rotate90FlipNone;
                            break;
                        case EXIFPictureOrientationType.RightBottom:
                            rotationToApply = System.Drawing.RotateFlipType.Rotate270FlipX;
                            break;
                        case EXIFPictureOrientationType.LeftBottom:
                            rotationToApply = System.Drawing.RotateFlipType.Rotate270FlipNone;
                            break;
                        default:
                            rotationToApply = System.Drawing.RotateFlipType.RotateNoneFlipNone;
                            break;
                    }

                    image.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
                    image.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);

                    image.RotateFlip(rotationToApply);
                    image.GetPropertyItem(orientationEXIFHeader).Value[0] = (byte)EXIFPictureOrientationType.TopLeft;

                    //System.Drawing.Image newImage = image.GetThumbnailImage(image.Width, image.Height, null, IntPtr.Zero);
                    //image.Dispose();
                    //ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);
                    //
                    //System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                    //EncoderParameters myEncoderParameters = new EncoderParameters(1);
                    //
                    //EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 80L);
                    //myEncoderParameters.Param[0] = myEncoderParameter;

                    ////newImage.Save(originalFile);
                    ////newImage.Dispose();

                    image.Save(pathToSave, System.Drawing.Imaging.ImageFormat.Jpeg);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                _logger.Error("Error guardando la imagen desde telefono", e);
                return false;
            }
            finally
            {
                image.Dispose();
            }
        }

        #endregion


    }
}
