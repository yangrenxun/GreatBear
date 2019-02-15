using GreatBear.Core.Dependency;
using GreatBear.Demo.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreatBear.Demo.WebApp.Authentication.Admin
{
    /// <summary>
    /// Authentication service interface
    /// </summary>
    public interface IAuthenticationService : ITransientDependency
    {
        /// <summary>
        /// Sign in
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="isPersistent">Whether the authentication session is persisted across multiple requests</param>
        void SignIn(User user, bool isPersistent);

        /// <summary>
        /// Sign out
        /// </summary>
        void SignOut();

        /// <summary>
        /// Get authenticated customer
        /// </summary>
        /// <returns>Customer</returns>
        User GetAuthenticatedUser();
    }
}
