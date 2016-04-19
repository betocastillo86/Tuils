using Nop.Core.Infrastructure;
using Nop.Services.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
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

        #region IsMobile
        public static bool IsMobile(this HtmlHelper html, bool includeIpad = true)
        {
            var userAgent = HttpContext.Current.Request.UserAgent;

            var b = new Regex(@"(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            var v = new Regex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-", RegexOptions.IgnoreCase | RegexOptions.Multiline);

            //Valida la expresión de dispositivos moviles
            if (b.IsMatch(userAgent) || v.IsMatch(userAgent.Substring(0, 4)))
            {
                //Si no debe incluir el ipad valida que no exista la palabra en el userAgent
                if (!includeIpad)
                    return userAgent.IndexOf("iPad", StringComparison.CurrentCultureIgnoreCase) == -1;

                return true;
            }

            return false;
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