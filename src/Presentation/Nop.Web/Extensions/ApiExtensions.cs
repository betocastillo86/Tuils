﻿using Nop.Core.Domain.Catalog;
using Nop.Web.Models.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Nop.Web.Extensions.Api
{
    /// <summary>
    /// funciones de extension del web api
    /// </summary>
    public static class ApiExtensions
    {
        /// <summary>
        /// Minifica una clase a solo el id y el nombre sin importar que tipo de clase sea.
        /// Se puede especificar cuales son los cambios a minificar
        /// </summary>
        /// <typeparam name="T">Tipo Clase que se pueda minificar</typeparam>
        /// <param name="caller">objeto que raliza el llamado al metodo</param>
        /// <param name="label">propiedad que va tomar el valor del label, por defecto es Name</param>
        /// <param name="value">propiedad que va tomar el valor del "valor", por defecto es Id</param>
        /// <returns>Objeto tipo MInifiedJson con la información de lo que se desea mostrar</returns>
        public static MinifiedJson ToMinifiedModel<T>(this T model, string label = "Name", string value = "Id") where T : class
        {
            string labelValue = model.GetType().GetProperty(label).GetValue(model).ToString();
            int valueValue = int.Parse(model.GetType().GetProperty(value).GetValue(model).ToString());
            return new MinifiedJson(valueValue, labelValue);
        }

        /// <summary>
        /// Minifica una lista de objetos ayudado por el metodo ToMinifiedModel
        /// </summary>
        /// <typeparam name="T">Tipo de dato a minificar</typeparam>
        /// <param name="list">lista de datos a minificar</param>
        /// <param name="label">propiedad que va tomar el valor del label, por defecto es Name</param>
        /// <param name="value">propiedad que va tomar el valor del "valor", por defecto es Id</param>
        /// <returns>Listado de objetos tipo MInifiedJson con la información necesaria</returns>
        public static List<MinifiedJson> ToMinifiedListModel<T>(this IList<T> list, string label = "Name", string value = "Id") where T : class
        {
            var minifiedList = new List<MinifiedJson>();

            foreach (var model in list)
            {
                minifiedList.Add(model.ToMinifiedModel(label, value));
            }

            return minifiedList;
        }

        /// <summary>
        /// Realiza las validaciones si la solicitud que se envia a un controlador de Web Api debe minificar el modelo o no
        /// </summary>
        /// <typeparam name="T">Clase de tipo ApiController</typeparam>
        /// <param name="controller">controlador que llama el metodo</param>
        /// <returns>True: Se deben minificar las respuestas False: No es necesario</returns>
        public static bool MustMinifyModel<T>(this T controller) where T : System.Web.Http.ApiController
        {
            var headers = controller.Request.Headers;
            bool isMinify = false;
            IEnumerable<string> values;
            
            //Valida que tenga en los headers el parametro min y que esté en true
            if (headers != null && headers.TryGetValues("min", out values))
            {
                bool.TryParse(headers.GetValues("min").FirstOrDefault(), out isMinify);
            }

            return isMinify;
        }

        /// <summary>
        /// Retorna el valor de un header de un request por APi
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controller"></param>
        /// <param name="headerName"></param>
        /// <returns>valor del header, si no existe retorna false</returns>
        public static object GetHeader<T>(this T controller, string headerName) where T : ApiController
        {
            var headers = controller.Request.Headers;
            IEnumerable<string> values;

            object headerValue = null;

            //Valida que tenga el header
            if (headers != null && headers.TryGetValues(headerName, out values))
            {
                //Obtiene el valor del header
                headerValue = headers.GetValues(headerName).FirstOrDefault();
            }

            return headerValue;
        }

        /// <summary>
        /// Retorna el valor de un header booleano
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controller"></param>
        /// <param name="headerName">Nombre del header que se desea consultar</param>
        /// <param name="defaultValue">Valor por defecto que debería retornar si no existe el header</param>
        /// <returns></returns>
        public static bool GetHeaderBoolean<T>(this T controller, string headerName, bool defaultValue = false) where T : ApiController
        {
            bool headerValue = defaultValue;
            if (controller.GetHeader(headerName) != null)
                bool.TryParse(controller.GetHeader(headerName).ToString(), out headerValue);

            return headerValue;
        }


    }
}