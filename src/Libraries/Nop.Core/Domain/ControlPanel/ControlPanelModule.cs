using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.ControlPanel
{
    public class ControlPanelModule
    {
        /// <summary>
        /// Nombre del modulo que se busca
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Acción que ejecuta el modulo
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Controlador que se ejecuta
        /// </summary>
        public string Controller { get; set; }

        public string IconMini { get; set; }

        public string IconBig { get; set; }

        public object Parameters { get; set; }

        /// <summary>
        /// Son los parametros que son opcionales, y que no necesariamente deben venir en el query string.
        /// Por ejemplo. MyProdcuts con parametro "p" = false trae los productos inactivos
        ///              Pero MyProducts sin el parametro, debería traerlos también
        /// </summary>
        public object OptionalParameters { get; set; }

        private List<ControlPanelModule> _subModules { get; set; }
        public List<ControlPanelModule> SubModules 
        {
            get { return _subModules ?? new List<ControlPanelModule>(); }
            set { _subModules = value; }
        }
    }
}
