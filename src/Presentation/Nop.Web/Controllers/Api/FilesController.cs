using Nop.Core.Domain.Common;
using Nop.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Nop.Web.Controllers.Api
{
    [Route("api/files")]
    public class FilesController : ApiController
    {
        #region Ctor
        public FilesController(TuilsSettings tuilsSettings)
        {
            this._tuilsSettings = tuilsSettings;
        }

        #endregion

        #region Fields
        private readonly TuilsSettings _tuilsSettings;
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
                            switch (fileToUpload.ContentType)
                            {
                                case "image/bmp":
                                    extension = ".bmp";
                                    break;
                                case "image/gif":
                                    extension = ".gif";
                                    break;
                                case "image/jpeg":
                                    extension = ".jpg";
                                    break;
                                case "image/png":
                                    extension = ".png";
                                    break;
                                default:
                                    break;
                            }
                        }

                        string fileName = string.Empty;
                        SaveTempFile(fileToUpload.Data, extension, out fileName);

                        return Ok(new { fileGuid = string.Concat(fileName, extension) });
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
        public bool SaveTempFile(byte[] archivo, string extension, out string fileName)
        {
            fileName = Guid.NewGuid().ToString();

            string ruta = System.IO.Path.Combine(HttpContext.Current.Server.MapPath(_tuilsSettings.tempUploadFiles), string.Concat(fileName, extension));

            //Si el archivo ya existe se intenta crear uno nuevo
            if (!File.Exists(ruta))
            {
                try
                {
                    using (System.IO.FileStream stream = new System.IO.FileStream(ruta, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                    {
                        stream.Write(archivo, 0, archivo.Length);
                        stream.Close();
                    }
                    return true;
                }
                catch (Exception)
                {
                    throw;
                }

            }
            else
            {
                return SaveTempFile(archivo, extension, out fileName);
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
