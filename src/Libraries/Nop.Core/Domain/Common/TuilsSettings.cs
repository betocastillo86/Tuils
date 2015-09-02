using Nop.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Common
{
    public class TuilsSettings : ISettings
    {
        /// <summary>
        /// Categoria principal para los productos
        /// </summary>
        public int productBaseTypes_product { get; set; }

        /// <summary>
        /// Categoria principal para los Servicios
        /// </summary>
        public int productBaseTypes_service { get; set; }

        /// <summary>
        /// Categoria principal para las Motos 
        /// </summary>
        public int productBaseTypes_bike { get; set; }


        /// <summary>
        /// Ruta en la que se cargaran los archivos temporales 
        /// </summary>
        public string tempUploadFiles { get; set; }

        /// <summary>
        /// Tamaño máximo (en bytes) de los archivos cargados por usuarios externos
        /// </summary>
        public long maxFileUploadSize { get; set; }
        
        /// <summary>
        /// Pais por defecto del aplicativo
        /// </summary>
        public int defaultCountry { get; set; }

        #region Atributos especialesde Productos
        /// <summary>
        /// Atributo de tipo  color 
        /// </summary>
        public int specificationAttributeColor { get; set; }

        /// <summary>
        /// Atributo de tipo condición
        /// </summary>
        public int specificationAttributeCondition { get; set; }

        /// <summary>
        /// Atributo de los accesorios de una moto
        /// </summary>
        public int specificationAttributeAccesories { get; set; }

        /// <summary>
        /// Atributo de las condiciones de negociación de una moto
        /// </summary>
        public int specificationAttributeNegotiation { get; set; }

        /// <summary>
        /// Opción por defecto que puede quedar seleccionada para el reccorido de una moto
        /// </summary>
        public int specificationAttributeOptionKms { get; set; }


        /// <summary>
        /// Opción por defecto que puede quedar seleccionada para las placas de moto
        /// </summary>
        public int specificationAttributeOptionCarriagePlate { get; set; }

        /// <summary>
        /// Atributo de insumos
        /// </summary>
        public int specificationAttributeSupplies { get; set; }

        /// <summary>
        /// Atributo correspondiente al tipo de motocicleta
        /// </summary>
        public int specificationAttributeBikeType { get; set; }

        /// <summary>
        /// Atributo Estado (Nuevo o Usado)
        /// </summary>
        public int specificationattributeIsNew { get; set; }

        /// <summary>
        /// Opción de atributo Nuevo
        /// </summary>
        public int specificationattributeOptionIsNewYes { get; set; }


        /// <summary>
        /// Opción de atributo Usado
        /// </summary>
        public int specificationattributeOptionIsNewNo { get; set; }

        /// <summary>
        // Cada vez que se recargue la información del javascript los clientes
        // deben eliminar del localstorage las referencias de motos que tienen
        /// </summary>
        public string ExpirationBikeReferencesKey { get; set; }

        #endregion

        #region Productos

        public int defaultStockQuantity { get; set; }

        public int SendMessageExpirationProductDaysBefore { get; set; }

        #endregion





    }
}
