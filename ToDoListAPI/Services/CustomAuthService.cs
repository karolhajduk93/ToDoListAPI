using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ToDoListAPI.Services
{
    public class CustomAuthService : AuthenticationService
    {
        public CustomAuthService(IAuthenticationSchemeProvider schemes, IAuthenticationHandlerProvider handlers, IClaimsTransformation transform, IOptions<AuthenticationOptions> options) : base(schemes, handlers, transform, options)
        {
        }

        public override Task<AuthenticateResult> AuthenticateAsync(HttpContext context, string scheme)
        {
            return base.AuthenticateAsync(context, scheme);
        }

        public override Task ChallengeAsync(HttpContext context, string scheme, AuthenticationProperties properties)
        {
            return base.ChallengeAsync(context, scheme, properties);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override Task ForbidAsync(HttpContext context, string scheme, AuthenticationProperties properties)
        {
            return base.ForbidAsync(context, scheme, properties);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override Task SignInAsync(HttpContext context, string scheme, ClaimsPrincipal principal, AuthenticationProperties properties)
        {
            return base.SignInAsync(context, scheme, principal, properties);
        }

        public override Task SignOutAsync(HttpContext context, string scheme, AuthenticationProperties properties)
        {
            return base.SignOutAsync(context, scheme, properties);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
