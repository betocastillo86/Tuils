using Nop.Core.Infrastructure;
using Nop.Services.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace System.Web.Mvc.Html
{
    public static class MvcHelperExtensions
    {
        private static ILocalizationService _localizationService;

        private static ILocalizationService localizationService
        {
            get { return _localizationService ?? (_localizationService = EngineContext.Current.Resolve<ILocalizationService>()); }
        }

        /// <summary>
        /// Crea un label con el texto de una llave registrada en los recursos
        /// </summary>
        /// <param name="helper">this</param>
        /// <param name="key">llave de configuración de texto</param>
        /// <returns>objeto </returns>
        public static MvcHtmlString LabelT(this HtmlHelper helper, string key)
        {
            return helper.Label(localizationService.GetResource(key) ?? string.Empty);
        }

        #region Html Required Fields

        /// <summary>
        /// Retorna el HHTML completo de un textbox con su label y etiquetas relacionadas con las validaciones
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="controlType">TIpo de control que se va renderizar</param>
        /// <param name="field">Nombre del campo que se va agregar</param>
        /// <param name="value">Valor asignado al control por defecto</param>
        /// <param name="labelText">texto que se encuentra en el Label</param>
        /// <param name="labelResource">texto tomado de los recursos que se carga en el Label (Este valor prima sobre labelText)</param>
        /// <param name="controlHtmlAttributes">objeto con las propiedades agregadas al control</param>
        /// <param name="selectList">Listado de opcines en los casos de dropdownlist</param>
        /// <returns></returns>
        private static MvcHtmlString ControlRequired(this HtmlHelper helper, ControlType controlType, string field, string value = null, string labelText = null, string labelResource = null, object controlHtmlAttributes = null, IEnumerable<SelectListItem> selectList = null)
        {
            var htmlControl = new StringBuilder();

            //Si el label viene de los recursos realiza la consulta sino pega el html que llegó
            if (labelResource != null)
                htmlControl.Append(LabelT(helper, labelResource).ToHtmlString());
            else
                htmlControl.Append(helper.Label(labelText ?? string.Empty).ToHtmlString());
            
            ///Valida el tipo de control y lo carga en el html
            switch (controlType)
            {
                case ControlType.TextBox:
                    htmlControl.Append(helper.TextBox(string.Concat("txt", field), value ?? string.Empty, controlHtmlAttributes).ToHtmlString());
                    break;
                case ControlType.DropdownList:
                    htmlControl.Append(helper.DropDownList(string.Concat("txt", field), selectList, controlHtmlAttributes).ToHtmlString());
                    break;
                case ControlType.TextArea:
                    htmlControl.Append(helper.TextArea(string.Concat("txt", field), value ?? string.Empty, controlHtmlAttributes).ToHtmlString());
                    break;
                case ControlType.Password:
                    htmlControl.Append(helper.Password(string.Concat("txt", field), value ?? string.Empty, controlHtmlAttributes).ToHtmlString());
                    break;
                default:
                    break;
            }


            //Agrega la sección de codigo que contiene lo que se marca como obligatorio
            htmlControl.Append(GetRequiredHtmlPart(field));

            return new MvcHtmlString(htmlControl.ToString());
        }

        /// <summary>
        /// Retorna el HHTML completo de un textbox con su label y etiquetas relacionadas con las validaciones
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="field">Nombre del campo que se va agregar</param>
        /// <param name="value">Valor asignado al control por defecto</param>
        /// <param name="labelText">texto que se encuentra en el Label</param>
        /// <param name="labelResource">texto tomado de los recursos que se carga en el Label (Este valor prima sobre labelText)</param>
        /// <param name="controlHtmlAttributes">objeto con las propiedades agregadas al control</param>
        /// <returns>contenido Html del control</returns>
        public static MvcHtmlString TextBoxRequired(this HtmlHelper helper, string field, string value = null, string labelText = null, string labelResource = null, object controlHtmlAttributes = null) 
        {
            return ControlRequired(helper, ControlType.TextBox, field, value, labelText, labelResource, controlHtmlAttributes);
        }

        /// <summary>
        /// Retorna el HHTML completo de un textbox con su label y etiquetas relacionadas con las validaciones
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="field">Nombre del campo que se va agregar</param>
        /// <param name="value">Valor asignado al control por defecto</param>
        /// <param name="labelText">texto que se encuentra en el Label</param>
        /// <param name="labelResource">texto tomado de los recursos que se carga en el Label (Este valor prima sobre labelText)</param>
        /// <param name="controlHtmlAttributes">objeto con las propiedades agregadas al control</param>
        /// <returns>contenido Html del control</returns>
        public static MvcHtmlString PasswordRequired(this HtmlHelper helper, string field, string labelText = null, string labelResource = null)
        {
            return ControlRequired(helper, ControlType.Password, field, null, labelText, labelResource);
        }


        /// <summary>
        /// Retorna el HHTML completo de un dropdown con su label y etiquetas relacionadas con las validaciones
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="field">Nombre del campo que se va agregar</param>
        /// <param name="value">Valor asignado al control por defecto</param>
        /// <param name="labelText">texto que se encuentra en el Label</param>
        /// <param name="labelResource">texto tomado de los recursos que se carga en el Label (Este valor prima sobre labelText)</param>
        /// <param name="controlHtmlAttributes">objeto con las propiedades agregadas al control</param>
        /// <param name="selectList">Lista de opciones del dropdown</param>
        /// <returns>contenido Html del control</returns>
        public static MvcHtmlString DropDownRequired(this HtmlHelper helper, string field, SelectList selectList, string value = null, string labelText = null, string labelResource = null, object controlHtmlAttributes = null)
        {
            return ControlRequired(helper, ControlType.DropdownList, field, value, labelText, labelResource, controlHtmlAttributes, selectList);
        }



        /// <summary>
        /// Retorna la parte final de los controles que deben ser obligatorios
        /// </summary>
        /// <returns></returns>
        private static string GetRequiredHtmlPart(string field)
        {
            var str = new StringBuilder();
            str.Append("<span class=\"required\">*</span>");
            str.Append("<div class=\"text-character\"></div>");
            str.AppendFormat("<span class=\"field-validation-error\" tuils-val-for=\"{0}\"></span>", field);
            return str.ToString();
        }

        #endregion

        private enum ControlType
        { 
            TextBox, 
            DropdownList,
            TextArea,
            Password
        }
    }
}