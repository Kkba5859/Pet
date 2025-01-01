using Blazored.LocalStorage;
using Pet.Repositories;

namespace Pet.Services.Users;
public class LoginService : ILoginService
{
    private readonly IAuthRepository _authRepository;
    private readonly ILocalStorageService _localStorage;
    private readonly HttpClient _http;
    private readonly IAuthStateService _authStateService;

    public LoginService(IAuthRepository authRepository, ILocalStorageService localStorage, HttpClient http, IAuthStateService authStateService)
    {
        _authRepository = authRepository;
        _localStorage = localStorage;
        _http = http;
        _authStateService = authStateService;
    }

    public async Task<bool> Login(string username, string password)
    {
        var token = await _authRepository.Login(username, password);
        if (!string.IsNullOrEmpty(token))
        {
            await _localStorage.SetItemAsync("authToken", token);
            await _localStorage.SetItemAsync("username", username); // Сохранение имени пользователя
            _http.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            // Обновляем состояние аутентификации
            _authStateService.UpdateAuthState(true, username); // Обновление состояния

            return true;
        }
        return false;
    }

    public async Task<string?> GetToken()
    {
        return await _localStorage.GetItemAsync<string>("authToken");
    }
}
