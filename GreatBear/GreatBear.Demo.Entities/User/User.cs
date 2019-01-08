using GreatBear.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GreatBear.Demo.Entities.User
{
    public class User : Entity, IHasModificationTime, IHasCreationTime
    {
        public const int MaxUserNameLength = 50;
        public const int MinUserNameLength = 1;
        public const int MaxPasswordLength = 50;
        public const int MinPasswordLength = 6;

        /// <summary>
        /// Username for login
        /// </summary>
        [Required]
        [StringLength(MaxUserNameLength, MinimumLength = MinUserNameLength)]
        public virtual string UserName { get; set; }

        /// <summary>
        /// User password for login
        /// </summary>
        [Required]
        [StringLength(MaxPasswordLength, MinimumLength = MinPasswordLength)]
        public virtual string Password { get; set; }

        /// <summary>
        /// The last modified time for this user.
        /// </summary>
        public virtual DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// Creation time of this user.
        /// </summary>
        public virtual DateTime CreationTime { get; set; }

        ///// <summary>
        ///// User information owned
        ///// </summary>
        //public virtual UserInfo UserInfo { get; set; }

        ///// <summary>
        ///// Book collection owned by the user
        ///// </summary>
        //[InverseProperty(nameof(Book.User))]
        //public virtual ICollection<Book> Books { get; set; }
    }
}
