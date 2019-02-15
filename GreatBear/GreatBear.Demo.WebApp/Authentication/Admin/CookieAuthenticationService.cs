using GreatBear.Demo.Application.Users;
using GreatBear.Demo.Entities.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GreatBear.Demo.WebApp.Authentication.Admin
{
    public class CookieAuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private User _cachedUser;

        #endregion

        #region Ctor

        public CookieAuthenticationService(IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            this._userService = userService;
            this._httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sign in
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="isPersistent">Whether the authentication session is persisted across multiple requests</param>
        public virtual async void SignIn(User user, bool isPersistent)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            //create claims for customer's username and email
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.UserData, user.Id.ToString(), ClaimValueTypes.String));

            if (!string.IsNullOrEmpty(user.UserName))
                claims.Add(new Claim(ClaimTypes.Name, user.UserName, ClaimValueTypes.String));

            if (!string.IsNullOrEmpty(user.Email))
                claims.Add(new Claim(ClaimTypes.Email, user.Email, ClaimValueTypes.Email));

            //create principal for the current authentication scheme
            var userIdentity = new ClaimsIdentity(claims, AdminAttribute.AuthenticationScheme);
            var userPrincipal = new ClaimsPrincipal(userIdentity);

            //set value indicating whether session is persisted and the time at which the authentication was issued
            var authenticationProperties = new AuthenticationProperties
            {
                IsPersistent = isPersistent,
                IssuedUtc = DateTime.UtcNow
            };

            //sign in
            await _httpContextAccessor.HttpContext.SignInAsync(AdminAttribute.AuthenticationScheme, userPrincipal, authenticationProperties);

            //cache authenticated customer
            _cachedUser = user;
        }

        /// <summary>
        /// Sign out
        /// </summary>
        public virtual async void SignOut()
        {
            //reset cached customer
            _cachedUser = null;

            //and sign out from the current authentication scheme
            await _httpContextAccessor.HttpContext.SignOutAsync(AdminAttribute.AuthenticationScheme);
        }

        /// <summary>
        /// Get authenticated customer
        /// </summary>
        /// <returns>Customer</returns>
        public virtual User GetAuthenticatedUser()
        {
            //whether there is a cached customer
            if (_cachedUser != null)
                return _cachedUser;

            //try to get authenticated user identity
            var authenticateResult = _httpContextAccessor.HttpContext.AuthenticateAsync(AdminAttribute.AuthenticationScheme).Result;
            if (!authenticateResult.Succeeded)
                return null;

            User user = null;

            //try to get customer by username
            var usernameClaim = authenticateResult.Principal.FindFirst(claim => claim.Type == ClaimTypes.Name);
            if (usernameClaim != null)
                user = _userService.GetUserByUsername(usernameClaim.Value);

            //cache authenticated customer
            _cachedUser = user;

            return _cachedUser;
        }

        #endregion
    }
}
