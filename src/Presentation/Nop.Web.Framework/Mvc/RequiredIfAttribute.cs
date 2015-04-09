using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Web.Framework.Mvc
{
    /// <summary>
    /// Atributo que realiza validaciones condicionales dependiendo del valor de otro campo
    /// Tomado de http://forums.asp.net/post/5464365.aspx
    /// </summary>
    public class RequiredIfAttribute : ConditionalValidationAttribute
    {
        protected override string ValidationName
        {
            get { return "requiredif"; }
        }
        public RequiredIfAttribute(string dependentProperty, object targetValue)
            : base(new RequiredAttribute(), dependentProperty, targetValue)
        {

        }
        protected override IDictionary<string, object> GetExtraValidationParameters()
        {
            return new Dictionary<string, object> 
        { 
            { "rule", "required" }
        };
        }
    }
}
