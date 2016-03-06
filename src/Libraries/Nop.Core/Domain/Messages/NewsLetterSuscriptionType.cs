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
        MyReference = 3,

        /// <summary>
        /// Suscripciones de usuarios de tipo tienda
        /// </summary>
        User = 4,

        /// <summary>
        /// Suscripciones de usuarios de tipo tienda
        /// </summary>
        Shop = 5,

        /// <summary>
        /// Suscripciones de usuarios de tipo taller
        /// </summary>
        RepairShop = 6,
        
        /// <summary>
        /// Suscripciones en el landing como tiendas
        /// </summary>
        LandingStores = 7,

        /// <summary>
        /// Suscripciones en el landing como taller
        /// </summary>
        LandingRepairs = 8
    }
}
