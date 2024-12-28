using Blazored.LocalStorage;
using Pet.Repositories;

namespace Pet.Services.Users
{
    public class LoginService : ILoginService
    {
        private readonly IAuthRepository _authRepository;
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _http;

        public LoginService(IAuthRepository authRepository, ILocalStorageService localStorage, HttpClient http)
        {
            _authRepository = authRepository;
            _localStorage = localStorage;
            _http = http;
        }

        public async Task<bool> Login(string username, string password)
        {
            var token = await _authRepository.Login(username, password);
            if (!string.IsNullOrEmpty(token))
            {
                await _localStorage.SetItemAsync("authToken", token);
                _http.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                return true;
            }
            return false;
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
