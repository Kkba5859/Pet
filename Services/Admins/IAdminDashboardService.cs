using Pet.Models;

namespace Pet.Services.Admins
{
    public interface IAdminDashboardService
    {
        Task<List<User>> GetUsersAsync();
        Task<bool> DeleteUserAsync(string username);
        Task<List<User>> GetAdminsAsync();
        Task<bool> DeleteAdminAsync(string username);
    }
}
