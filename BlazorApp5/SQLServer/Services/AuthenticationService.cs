// Services/AuthenticationService.cs
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazorApp5.SQLServer.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationService(
            AuthenticationStateProvider authStateProvider,
            IHttpContextAccessor httpContextAccessor)
        {
            _authStateProvider = authStateProvider;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task SignInAsync(ClaimsPrincipal user)
        {
            if (_httpContextAccessor.HttpContext == null) return;

            await _httpContextAccessor.HttpContext.SignInAsync(user);
            ((AuthStateProvider)_authStateProvider).NotifyAuthenticationStateChanged();
        }

        public async Task SignOutAsync()
        {
            if (_httpContextAccessor.HttpContext == null) return;

            await _httpContextAccessor.HttpContext.SignOutAsync();
            ((AuthStateProvider)_authStateProvider).NotifyAuthenticationStateChanged();
        }

        public ClaimsPrincipal GetCurrentUser()
        {
            return _httpContextAccessor.HttpContext?.User;
        }
    }
}