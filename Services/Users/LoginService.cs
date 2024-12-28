using Blazored.LocalStorage;
using System.Net.Http.Json;
using Pet.Models;

namespace Pet.Services.Users
{
    public class LoginService : ILoginService
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorage;

        public LoginService(HttpClient http, ILocalStorageService localStorage)
        {
            _http = http;
            _localStorage = localStorage;
        }

        public async Task<bool> Login(string username, string password)
        {
            var loginDto = new { Username = username, Password = password };
            try
            {
                var response = await _http.PostAsJsonAsync("http://localhost:5000/api/Auth/login", loginDto);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
                    if (result?.Token != null)
                    {
                        await _localStorage.SetItemAsync("authToken", result.Token);

                        _http.DefaultRequestHeaders.Authorization =
                            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result.Token);

                        return true;
                    }
                }
                var errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Ошибка входа: {errorMessage}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка входа: {ex.Message}");
                return false;
            }
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            _http.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<string?> GetToken()
        {
            try
            {
                return await _localStorage.GetItemAsync<string>("authToken");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка получения токена: {ex.Message}");
                return null;
            }
        }

        
    }
}
