using System;
using System.Collections.Generic;
using System.Text;

namespace GreatBear.Core.Domain.Entities
{
    /// <summary>
    /// Entity base interface
    /// </summary>
    public interface IEntity<TPrimaryKey>
    {
        /// <summary>
        /// Primary key unique Id
        /// </summary>
        TPrimaryKey Id { get; set; }
    }
}
