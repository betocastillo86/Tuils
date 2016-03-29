using Nop.Core.Domain.Common;
using Nop.Utilities;
using Nop.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Nop.Web.Framework.Mvc.Api;
using Nop.Services.Media;

namespace Nop.Web.Controllers.Api
{
    [Route("api/files")]
    public class FilesController : ApiController
    {
        #region Ctor
        public FilesController(TuilsSettings tuilsSettings, 
            IPictureService pictureService)
        {
            this._tuilsSettings = tuilsSettings;
            _pictureService = pictureService;
        }

        #endregion

        #region Fields
        private readonly TuilsSettings _tuilsSettings;
        private readonly IPictureService _pictureService;
        #endregion

        #region Actions
        [Route("api/files/upload")]
        [HttpPost]
        public async Task<IHttpActionResult> Post()
        {

            if (!Request.Content.IsMimeMultipartContent())
            {
                return NotFound();
            }
            else
            {

                try
                {
                    var provider = await Request.Content.ReadAsMultipartAsync<InMemoryMultipartFormDataStreamProvider>(new InMemoryMultipartFormDataStreamProvider());

                    System.Web.Mvc.FormCollection formData = provider.FormData;

                    IList<HttpContent> fileContentList = provider.Files;

                    var fileDataList = provider.GetFiles();

                    var files = await fileDataList;
                    var fileToUpload = files.FirstOrDefault();

                    //Valida que el tamaño del archivo sea valido
                    if (fileToUpload.Size < _tuilsSettings.maxFileUploadSize)
                    {
                        string extension = Path.GetExtension(fileToUpload.FileName);

                        if (string.IsNullOrEmpty(extension))
                        {
                            extension = Files.GetExtensionByContentType(fileToUpload.ContentType);
                        }

                        string fileName = string.Empty;
                        string thumbnailName = string.Empty;
                        SaveTempFile(fileToUpload.Data, extension, out fileName, out thumbnailName);

                        return Ok(new { fileGuid = string.Concat(fileName, extension), thumbnail = thumbnailName });
                    }
                    else
                    {
                        return InternalServerError(new Nop.Core.NopException("El tamaño exede el permitido {0}", fileToUpload.Size));
                    }
                }
                catch (Exception e)
                {
                    return InternalServerError(e);
                }

            }


        }

        /// <summary>
        /// Elimina el archivo temporal enviado
        /// </summary>
        /// <param name="fileGuid"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/files/{fileGuid}")]
        public IHttpActionResult Delete(string fileGuid) 
        {
            if (DeleteTempFile(fileGuid))
                return Ok();
            else
                return NotFound();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Guarda un archivo temporal en el sistema y retorna la llave para ser recogido despues
        /// </summary>
        /// <param name="archivo"></param>
        /// <returns></returns>
        [NonAction]
        public bool SaveTempFile(byte[] archivo, string extension, out string fileName, out string thumbnailName)
        {
            fileName = Guid.NewGuid().ToString();

            string ruta = System.IO.Path.Combine(HttpContext.Current.Server.MapPath(_tuilsSettings.tempUploadFiles), string.Concat(fileName, extension));

            //Si el archivo ya existe se intenta crear uno nuevo
            //con un nuevo guid aleatoreo
            if (!File.Exists(ruta))
            {
                //Si la imagen no es procesada por el corrector de imagnes
                //Esta es guardada directamente en disco
                if (!_pictureService.CorrectImageOrientationEXIF(new MemoryStream(archivo), ruta))
                {
                    using (System.IO.FileStream stream = new System.IO.FileStream(ruta, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                    {
                        stream.Write(archivo, 0, archivo.Length);
                        stream.Close();
                    }
                }

                //Realiza un corte de la imagen para ser cargada en el cliente
                //Se quema el tamaño del thumbnail que se va a ver reflejado en el cliente
                thumbnailName = Path.GetFileName(_pictureService.CreateThumbnailImage(ruta, 300, true));

                return true;

            }
            else
            {
                return SaveTempFile(archivo, extension, out fileName, out thumbnailName);
            }
        }
        #endregion

        /// <summary>
        /// Elimina un archivo temporal de acuerdo al nombre guid
        /// </summary>
        /// <param name="fileGuid"></param>
        /// <returns></returns>
        public bool DeleteTempFile(string fileGuid)
        { 
            //Busca el archivo en la carpeta de temporales
            string fullPath = Directory.GetFiles(HttpContext.Current.Server.MapPath(_tuilsSettings.tempUploadFiles))
                .FirstOrDefault(f => f.Contains(fileGuid));

            //Valida que el archivo exista
            if (!string.IsNullOrEmpty(fullPath))
            {
                try
                {
                    File.Delete(fullPath);
                    return true;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            else
            {
                return false;
            }
        }
        

    }
}
