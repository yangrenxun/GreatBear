using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreatBear.Demo.WebApp
{
    public class MemberAttribute : AuthorizeAttribute
    {
        public MemberAttribute()
        {
            base.AuthenticationSchemes = "Member";
        }
    }
}
