using Nop.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.ControlPanel
{
    public class ControlPanelSettings : ISettings
    {
        /// <summary>
        /// Tamaño por defecto de paginación para las funcionalidades del panel de control
        /// </summary>
        public int defaultPageSize { get; set; }
    }
}
