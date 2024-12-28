using Pet.Models;

namespace Pet.Repositories
{
    public interface IAdminRepository
    {
        Task<List<User>> GetUsersAsync();               // Получить всех пользователей
        Task<bool> DeleteUserAsync(string username);    // Удалить пользователя
        Task<List<User>> GetAdminsAsync();              // Получить всех администраторов
        Task<bool> DeleteAdminAsync(string username);   // Удалить администратора
    }
}
