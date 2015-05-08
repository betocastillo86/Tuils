using Nop.Core.Domain.ControlPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nop.Services.ControlPanel
{
    public interface IControlPanelService
    {
        /// <summary>
        /// Retorna todos los modulos a los que el usuario actual puede tener acceso en el panel de control
        /// </summary>
        /// <returns></returns>
        List<ControlPanelModule> GetModulesActiveUser();
    }
}
