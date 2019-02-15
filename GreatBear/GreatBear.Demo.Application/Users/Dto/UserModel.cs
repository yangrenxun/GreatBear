using System;
using System.Collections.Generic;
using System.Text;

namespace GreatBear.Demo.Application.Users.Dto
{
    public class UserModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string RealName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
    }
}
