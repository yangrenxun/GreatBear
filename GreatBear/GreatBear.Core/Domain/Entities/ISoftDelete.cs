using System;
using System.Collections.Generic;
using System.Text;

namespace GreatBear.Core.Domain.Entities
{
    /// <summary>
    /// Soft delete interface
    /// </summary>
    public interface ISoftDelete
    {
        /// <summary>
        /// Is deleted
        /// </summary>
        bool IsDeleted { get; set; }
    }
}
