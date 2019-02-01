using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreatBear.Demo.WebApp.Models.Account
{
    public class LoginModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string CaptchaCode { get; set; }

        public string ReturnUrl { get; set; }
    }
}
