using System.Net.Http.Json;
using Pet.Models;

namespace Pet.Services.Admins
{
    public class AdminLoginService : IAdminLoginService
    {
        private readonly HttpClient _httpClient;

        public AdminLoginService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> Login(AdminLoginModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5004/api/AdminLogin", model);

            if (response.IsSuccessStatusCode)
            {
                // Чтение данных с использованием типизированной модели
                var responseData = await response.Content.ReadFromJsonAsync<ApiResponse>();

                // Проверка успешного сообщения в ответе
                return responseData?.Message == "Login successful";
            }

            return false;
        }
    }
}
