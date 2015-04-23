using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nop.Core.Domain.Messages
{
    /// <summary>
    /// Tipos de suscripciones de correo
    /// </summary>
    public enum NewsLetterSuscriptionType
    {
        /// <summary>
        /// Suscripción general de news
        /// </summary>
        General = 1,
        /// <summary>
        /// Suscripción a las noticias de la marca de la moto del usuario
        /// </summary>
        MyBrand = 2,
        /// <summary>
        /// Suscripción a las noticias de la referencia de moto del usuario
        /// </summary>
        MyReference = 3
    }
}
