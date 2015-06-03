using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.ExternalAuth.Facebook.Models
{
    public class LoginFacebookOptionsModel
    {
        public bool BlankWindow { get; set; }

        public string ReturnUrl { get; set; }
    }
}
