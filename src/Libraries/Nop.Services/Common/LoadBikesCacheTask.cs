using Nop.Core;
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

        public LoadBikesCacheTask(IStoreContext storeContext)
        {
            this._storeContext = storeContext;
        }

        public void Execute()
        {
            string url = _storeContext.CurrentStore.Url + "api/categories/bikereferences";
            using (var wc = new WebClient())
            {
                wc.Headers.Add("Content-Type", "application/json");
                var response = wc.DownloadString(url);
            }
        }
    }
}
