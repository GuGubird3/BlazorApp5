// Services/IAuthenticationService.cs
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazorApp5.SQLServer.Services
{
    public interface IAuthenticationService
    {
        Task SignInAsync(ClaimsPrincipal user);
        Task SignOutAsync();
        ClaimsPrincipal GetCurrentUser();
    }
}
