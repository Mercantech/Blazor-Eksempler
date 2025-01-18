using System.Net.Http.Json;
using System.Text.Json;

namespace Blazor.Services
{
    public partial class APIService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://opgaver.mercantec.tech/api";

        public APIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}