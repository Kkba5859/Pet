using Blazored.LocalStorage;

namespace Pet.Services.Users
{
    public class AuthStateService : IAuthStateService
    {
        public bool IsLoggedIn { get; private set; }
        public string? Username { get; private set; }

        public event Action? OnStateChanged;

        private readonly ILocalStorageService _localStorage;

        public AuthStateService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task InitializeAuthStateAsync()
        {
            // Загрузка состояния аутентификации из локального хранилища
            Username = await _localStorage.GetItemAsync<string>("username");
            var token = await _localStorage.GetItemAsync<string>("authToken");

            // Устанавливаем статус входа в систему на основе наличия токена
            IsLoggedIn = !string.IsNullOrEmpty(token);
            OnStateChanged?.Invoke();  // Уведомляем компоненты, что состояние изменилось
        }

        public void UpdateAuthState(bool isLoggedIn, string? username = null)
        {
            IsLoggedIn = isLoggedIn;
            Username = username;
            OnStateChanged?.Invoke();
        }
    }
}
