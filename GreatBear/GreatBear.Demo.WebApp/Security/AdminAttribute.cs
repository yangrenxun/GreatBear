using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreatBear.Demo.WebApp
{
    public class AdminAttribute: AuthorizeAttribute
    {
        public const string AuthenticationScheme = "AdminScheme";

        public AdminAttribute()
        {
            base.AuthenticationSchemes = AuthenticationScheme;
        }
    }
}
