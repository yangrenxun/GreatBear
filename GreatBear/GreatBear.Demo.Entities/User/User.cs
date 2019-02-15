using GreatBear.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GreatBear.Demo.Entities.User
{
    public class User : Entity
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string PasswordSalt { get; set; }

        public string RealName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
        /// <summary>
        /// Creation time of this user.
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// The last modified time for this user.
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        public DateTime? LastLoginTime { get; set; }
    }
}
