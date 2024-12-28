using System.Net.Http.Json;

namespace Pet.Services.Users
{
    public class RegisterService : IRegisterService
    {
        private readonly HttpClient _http;

        public RegisterService(HttpClient http)
        {
            _http = http;
        }

        public async Task<bool> Register(string username, string password)
        {
            var userDto = new { Username = username, Password = password };
            try
            {
                var response = await _http.PostAsJsonAsync("http://localhost:5000/api/Auth/register", userDto);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                var errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Ошибка регистрации: {errorMessage}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при регистрации: {ex.Message}");
                return false;
            }
        }
    }
}
