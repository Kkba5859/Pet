using Blazored.LocalStorage;
using Pet.Repositories;

namespace Pet.Services.Users;
public class LogoutService : ILogoutService
{
    private readonly IAuthRepository _authRepository;
    private readonly ILocalStorageService _localStorage;
    private readonly HttpClient _http;
    private readonly IAuthStateService _authStateService;

    public LogoutService(IAuthRepository authRepository, ILocalStorageService localStorage, HttpClient http, IAuthStateService authStateService)
    {
        _authRepository = authRepository;
        _localStorage = localStorage;
        _http = http;
        _authStateService = authStateService;
    }

    public async Task<bool> Logout(string username)
    {
        var result = await _authRepository.Logout(username);
        if (result)
        {
            await _localStorage.RemoveItemAsync("authToken");
            await _localStorage.RemoveItemAsync("username");
            _http.DefaultRequestHeaders.Authorization = null;

            // Обновляем состояние, указываем, что пользователь вышел
            _authStateService.UpdateAuthState(false); // Обновление состояния

            return true;
        }
        return false;
    }
}
