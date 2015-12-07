using Nop.Core.Infrastructure;
using Nop.Services.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        #region  ControlRequiredFor
        /// <summary>
        /// Genera la estructura de una caja de texto, label y marcas de obligatorios
        /// </summary>
        /// <typeparam name="TModel">Modelo</typeparam>
        /// <typeparam name="TProperty">Propiedad a revisar</typeparam>
        /// <param name="helper">helper</param>
        /// <param name="expression">expresion de la propiedad a evaluar</param>
        /// <param name="htmlAttributes">attributos html</param>
        /// <param name="labelText">si el texto no viene del modelo se envía en esta propiedad</param>
        /// <param name="required">True: Muestra la zona de requerido. False: No lo muestra</param>
        /// <returns></returns>
        public static MvcHtmlString TextBoxRequiredFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null, string labelText = null, bool required = true, bool showLabel = true, bool showAsterisk = true)
        {
            return ControlRequiredFor(helper, ControlType.TextBox, expression, htmlAttributes, labelText, required, showLabel, showAsterisk);
        }

        public static MvcHtmlString PasswordRequiredFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null, string labelText = null, bool required = true, bool showLabel = true, bool showAsterisk = true)
        {
            return ControlRequiredFor(helper, ControlType.Password, expression, htmlAttributes, labelText, required, showLabel, showAsterisk);
        }

        public static MvcHtmlString TextAreaRequiredFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null, string labelText = null, bool required = true, bool showLabel = true, bool showAsterisk = true)
        {
            return ControlRequiredFor(helper, ControlType.TextArea, expression, htmlAttributes, labelText, required, showLabel, showAsterisk);
        }


        private static MvcHtmlString ControlRequiredFor<TModel, TProperty>(this HtmlHelper<TModel> helper, ControlType controlType, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null, string labelText = null, bool required= true, bool showLabel = true, bool showAsterisk = true)
        {
            var htmlControl = new StringBuilder();

            if (showLabel)
            {
                //Si el label viene de los recursos realiza la consulta sino pega el html que llegó
                if (labelText != null)
                    htmlControl.Append(LabelExtensions.LabelFor(helper, expression, labelText, htmlAttributes).ToHtmlString());
                else
                    htmlControl.Append(LabelExtensions.LabelFor(helper, expression, htmlAttributes).ToHtmlString());
            }

            ///Valida el tipo de control y lo carga en el html
            switch (controlType)
            {
                case ControlType.TextBox:

                    htmlControl.Append(helper.TextBoxFor(expression, htmlAttributes));
                    break;
                case ControlType.DropdownList:
                    //htmlControl.Append(helper.DropDownList(string.Concat("txt", field), selectList, controlHtmlAttributes).ToHtmlString());
                    break;
                case ControlType.TextArea:
                    htmlControl.Append(helper.TextAreaFor(expression, htmlAttributes));
                    break;
                case ControlType.Password:
                    htmlControl.Append(helper.PasswordFor(expression, htmlAttributes));
                    break;
                default:
                    break;
            }

            //Agrega la sección de codigo que contiene lo que se marca como obligatorio
            htmlControl.Append(GetRequiredHtmlPart(ExpressionHelper.GetExpressionText(expression), required, showAsterisk));

            return new MvcHtmlString(htmlControl.ToString());
        }
        #endregion

        #region ControlRequired
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
        /// <param name="showAsterisk">Cuando es obligatorio, muestra o no el asterisco</param>
        /// <returns></returns>
        private static MvcHtmlString ControlRequired(this HtmlHelper helper, ControlType controlType, string field, string value = null, string labelText = null, string labelResource = null, object controlHtmlAttributes = null, IEnumerable<SelectListItem> selectList = null, bool required = true, bool showAsterisk = true)
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
                    htmlControl.Append(helper.DropDownList(string.Concat("ddl", field), selectList, "-", controlHtmlAttributes).ToHtmlString());
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
            htmlControl.Append(GetRequiredHtmlPart(field, required, showAsterisk));

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
        public static MvcHtmlString TextBoxRequired(this HtmlHelper helper, string field, string value = null, string labelText = null, string labelResource = null, object controlHtmlAttributes = null, bool required = true)
        {
            return ControlRequired(helper, ControlType.TextBox, field, value, labelText, labelResource, controlHtmlAttributes, required: required);
        }

        /// <summary>
        /// Retorna el HHTML completo de un textarea con su label y etiquetas relacionadas con las validaciones
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="field">Nombre del campo que se va agregar</param>
        /// <param name="value">Valor asignado al control por defecto</param>
        /// <param name="labelText">texto que se encuentra en el Label</param>
        /// <param name="labelResource">texto tomado de los recursos que se carga en el Label (Este valor prima sobre labelText)</param>
        /// <param name="controlHtmlAttributes">objeto con las propiedades agregadas al control</param>
        /// <param name="showAsterisk">En el caso de ser obligatorio, si debe o no mostrar el asterisco</param>
        /// <returns>contenido Html del control</returns>
        public static MvcHtmlString TextAreaRequired(this HtmlHelper helper, string field, string value = null, string labelText = null, string labelResource = null, object controlHtmlAttributes = null, bool required = true, bool showAsterisk = true)
        {
            return ControlRequired(helper, ControlType.TextArea, field, value, labelText, labelResource, controlHtmlAttributes, required: required, showAsterisk: showAsterisk);
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
        public static MvcHtmlString DropDownRequired(this HtmlHelper helper, string field, SelectList selectList, string value = null, string labelText = null, string labelResource = null, object controlHtmlAttributes = null, bool required = true)
        {
            return ControlRequired(helper, ControlType.DropdownList, field, value, labelText, labelResource, controlHtmlAttributes, selectList, required);
        }




        /// <summary>
        /// Retorna la parte final de los controles que deben ser obligatorios
        /// </summary>
        /// <returns></returns>
        private static string GetRequiredHtmlPart(string field, bool required = true, bool showAsterisk = true)
        {
            var str = new StringBuilder();
            if(required && showAsterisk)
                str.Append("<span class=\"required\">*</span>");
            str.Append("<div class=\"text-character\"></div>");
            //En los casso que contenga . solo toma la parte final de la cadena
            str.AppendFormat("<span class=\"field-validation-error\" tuils-val-for=\"{0}\"></span>", !field.Contains(".") ? field : field.Split(new char[] { '.' })[field.Split(new char[] { '.' }).Length - 1]);
            return str.ToString();
        }
        #endregion

        #endregion

        #region FlipSwitch
        public static MvcHtmlString FlipSwitchFor<TModel>(this HtmlHelper<TModel> helper, Expression<Func<TModel, bool>> expression, object htmlAttributes = null)
        {
            string field = ExpressionHelper.GetExpressionText(expression);
            
            var sbHtml = new StringBuilder();
            sbHtml.Append("<div class=\"onoffswitch\">");

            //////obtiene el listado de atributos adicionales enviados
            ////var attributes = ((IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes ?? new {}));
            ////if(attributes.ContainsKey("class"))
            ////    attributes["class"] +=  "onoffswitch-checkbox";
            ////else
            ////    attributes.Add("class", "onoffswitch-checkbox");


            ////var strAttributes = new StringBuilder();
            ////attributes.ToList().ForEach(x => strAttributes.AppendFormat())

            ////attributes.Add("name", field);
            ////attributes.Add("id", field);

            //////Agrega los atributos al nuevo objeto
            ////var tag = new TagBuilder("input");
            ////tag.Attributes.Add("type", "checkbox");
            ////tag.MergeAttributes(attributes, true);
            ////sbHtml.Append(tag.ToString(TagRenderMode.Normal));

            
            sbHtml.AppendFormat("<input type=\"checkbox\" name=\"{0}\" class=\"onoffswitch-checkbox\" id=\"{0}\" {1}>", field, Convert.ToBoolean(ModelMetadata.FromLambdaExpression(expression, helper.ViewData).Model) ? "checked" : string.Empty);
            sbHtml.AppendFormat("<label class=\"onoffswitch-label\" for=\"{0}\">", field);
            sbHtml.Append("<span class=\"onoffswitch-inner\"></span>");
            sbHtml.Append("<span class=\"onoffswitch-switch\"></span>");
            sbHtml.Append("</label>");
            sbHtml.Append("</div>");
            return new MvcHtmlString(sbHtml.ToString());
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