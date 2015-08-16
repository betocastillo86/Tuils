using Nop.Core.Domain.Common;
using Nop.Core.Infrastructure;
using Nop.Services.Catalog;
using Nop.Web.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Extensions.Api
{
    public static class ApiValidationExtensions
    {

        #region ProductBaseModel
        /// <summary>
        /// Realiza validaciones adicionales a los modelos que no se encuentran en los atributos
        /// </summary>
        /// <param name="model">Datos del modelo que se desea validar</param>
        /// <returns>True: Datos validos</returns>
        public static bool Validate(this ProductBaseModel model)
        {
            var tuilsSettings = EngineContext.Current.Resolve<TuilsSettings>();

            if (model.ProductTypeId == tuilsSettings.productBaseTypes_product)
            {
                //Si es tipo producto
                //debe tener marca
                if (model.ManufacturerId == 0) return false;
            }
            else if (model.ProductTypeId == tuilsSettings.productBaseTypes_bike)
            {
                //si es de tipo motocicleta debe tener
                //Color, Placa, Recorrido y Año
                //if (model.Color == 0 || string.IsNullOrEmpty(model.CarriagePlate) || model.Kms == 0 || model.Year == 0) return false;
                if (string.IsNullOrEmpty(model.CarriagePlate) || model.Kms == 0 || model.Year == 0) return false;
            }
            else if (model.ProductTypeId == tuilsSettings.productBaseTypes_service)
            {
                //Si el envio está habilitado y no está detallado el valor
                //Si NO incluye insumos y no está especificado el valor de estos
                if ((model.IsShipEnabled && string.IsNullOrEmpty(model.DetailShipping)) || (!model.IncludeSupplies && model.SuppliesValue == 0)) return false;
            }
            //No puede existir oto tipo de producto
            else
                return false;

            return true;
        }
        #endregion

        #region UserBaseModel

        //public static bool ValidateRegister(this CustomerBaseModel model)
        //{
        //    return string.IsNullOrEmpty(model)
        //}


        #endregion

        #region VendorModel
        /// <summary>
        /// Realiza las validaciones de obligatoriedad unicamente para los campos que van en el cabezote del vendor
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool IsHeaderValid(this VendorModel model)
        {
            return !string.IsNullOrEmpty(model.Description) && !string.IsNullOrEmpty(model.Name);
        }
        #endregion





    }
}