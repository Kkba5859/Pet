namespace Pet.Services.Users
{
    public interface IAuthStateService
    {
        bool IsLoggedIn { get; }
        string? Username { get; }
        event Action? OnStateChanged;

        Task InitializeAuthStateAsync();  // Добавляем метод в интерфейс
        void UpdateAuthState(bool isLoggedIn, string? username = null);
    }
}
