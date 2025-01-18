using System.Net.Http.Json;

namespace Blazor.Services
{
    public partial class APIService
    {

        public async Task<string> LoginAsync(LoginRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/Users/login", request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                return result?.Token ?? throw new Exception("Token ikke modtaget");
            }

            throw new Exception("Login fejlede: " + response.ReasonPhrase);
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
    }
}
