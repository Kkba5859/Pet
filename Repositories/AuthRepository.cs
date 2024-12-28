using Pet.Models;
using System.Net.Http.Json;

namespace Pet.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly HttpClient _http;

        public AuthRepository(HttpClient http)
        {
            _http = http;
        }

        public async Task<string?> Login(string username, string password)
        {
            var loginDto = new { Username = username, Password = password };
            try
            {
                var response = await _http.PostAsJsonAsync("http://localhost:5000/api/Auth/login", loginDto);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
                    return result?.Token;
                }
                var errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Ошибка входа: {errorMessage}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка входа: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> Register(string username, string password)
        {
            var userDto = new { Username = username, Password = password };
            try
            {
                var response = await _http.PostAsJsonAsync("http://localhost:5000/api/Auth/register", userDto);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка регистрации: {ex.Message}");
                return false;
            }
        }
    }
}
