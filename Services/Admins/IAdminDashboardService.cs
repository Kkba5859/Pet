using Pet.Models;

namespace Pet.Services.Admins
{
    public interface IAdminDashboardService
    {
        // Работа с пользователями
        Task<List<User>> GetUsersAsync();           // Получение списка пользователей из API
        Task<bool> DeleteUserAsync(string username); // Удаление пользователя по имени
        List<User> GetCachedUsers();                // Получение кешированного списка пользователей

        // Работа с администраторами
        Task<List<User>> GetAdminsAsync();          // Получение списка администраторов из API
        Task<bool> DeleteAdminAsync(string username); // Удаление администратора по имени
        List<User> GetCachedAdmins();               // Получение кешированного списка администраторов
    }
}
