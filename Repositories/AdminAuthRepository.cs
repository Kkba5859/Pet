using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Pet.Models;

namespace Pet.Repositories
{
    public class AdminAuthRepository : IAdminAuthRepository
    {
        private readonly HttpClient _httpClient;

        public AdminAuthRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> RegisterAdminAsync(AdminRegisterModel model)
        {
            try
            {
                var requestContent = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("http://localhost:5004/api/AdminRegister", requestContent);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка регистрации администратора: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> LoginAdminAsync(AdminLoginModel model)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("http://localhost:5004/api/AdminLogin", model);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadFromJsonAsync<ApiResponse>();
                    return responseData?.Message == "Login successful";
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка входа администратора: {ex.Message}");
                return false;
            }
        }
    }
}
