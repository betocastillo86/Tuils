using Nop.Core;
using Nop.Core.Domain.Logging;
using Nop.Services.Logging;
using Nop.Services.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Common
{
    /// <summary>
    /// Tarea creada para que cada cierto tiempo se consulten las referencias de motocicletas, ya que es una consulta pesada
    /// y no puede bloquear los usuarios
    /// </summary>
    public class LoadBikesCacheTask : ITask
    {
        private readonly IStoreContext _storeContext;
        private readonly ILogger _logger;

        public LoadBikesCacheTask(IStoreContext storeContext, ILogger logger)
        {
            this._storeContext = storeContext;
            this._logger = logger;
        }

        public void Execute()
        {
            var guid = Guid.NewGuid();
            
            try
            {
                string url = _storeContext.CurrentStore.Url + "api/categories/bikereferences";
                _logger.InsertLog(LogLevel.Debug, string.Format("Inicio de la tarea LoadBikesCacheTask URL: {2} - {0} --------->{1} ", DateTime.UtcNow, guid, url));
                using (var wc = new WebClient())
                {

                    wc.Headers.Add("Content-Type", "application/json");
                    var response = wc.DownloadString(url);
                }
            }
            catch (Exception e)
            {
                _logger.InsertLog(LogLevel.Debug, string.Format("Error de la tarea LoadBikesCacheTask {1} {0}", DateTime.UtcNow, guid));
                throw e;
            }

            
            _logger.InsertLog(LogLevel.Debug, string.Format("Fin de la tarea LoadBikesCacheTask {1} {0}", DateTime.UtcNow, guid));
        }
    }
}
