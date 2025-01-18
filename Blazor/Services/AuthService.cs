using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Blazor.Services
{
    public class AuthService
    {
        private readonly ProtectedLocalStorage _localStorage;

        public AuthService(ProtectedLocalStorage localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<string?> GetUserNameFromToken()
        {
            try
            {
                var result = await _localStorage.GetAsync<string>("jwt_token");
                if (!result.Success || string.IsNullOrEmpty(result.Value))
                    return null;

                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(result.Value);

                var nameClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
                return nameClaim?.Value;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> IsAuthenticated()
        {
            try
            {
                var result = await _localStorage.GetAsync<string>("jwt_token");
                return result.Success && !string.IsNullOrEmpty(result.Value);
            }
            catch
            {
                return false;
            }
        }

        public async Task SetToken(string token)
        {
            await _localStorage.SetAsync("jwt_token", token);
        }

        public async Task Logout()
        {
            await _localStorage.DeleteAsync("jwt_token");
        }
    }
}