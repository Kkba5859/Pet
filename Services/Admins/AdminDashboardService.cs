using Pet.Models;
using Pet.Repositories;

namespace Pet.Services.Admins
{
    public class AdminDashboardService : IAdminDashboardService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminDashboardService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _adminRepository.GetUsersAsync();
        }

        public async Task<bool> DeleteUserAsync(string username)
        {
            return await _adminRepository.DeleteUserAsync(username);
        }

        public async Task<List<User>> GetAdminsAsync()
        {
            return await _adminRepository.GetAdminsAsync();
        }

        public async Task<bool> DeleteAdminAsync(string username)
        {
            return await _adminRepository.DeleteAdminAsync(username);
        }
    }
}
