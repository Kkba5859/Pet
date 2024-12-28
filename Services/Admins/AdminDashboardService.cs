using System.Net.Http.Json;
using Pet.Models;

namespace Pet.Services.Admins
{
    public class AdminDashboardService : IAdminDashboardService
    {
        private readonly HttpClient _httpClient;
        private List<User> _cachedUsers = new();

        public AdminDashboardService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Fetch users and cache the result
        public async Task<List<User>> GetUsersAsync()
        {
            _cachedUsers = await _httpClient.GetFromJsonAsync<List<User>>("http://localhost:5004/api/AdminDashboard/getUsers") ?? new List<User>();
            return _cachedUsers;
        }

        // Delete user and update the cached list
        public async Task<bool> DeleteUserAsync(string username)
        {
            var response = await _httpClient.DeleteAsync($"http://localhost:5004/api/AdminDashboard/deleteUser/{username}");
            if (response.IsSuccessStatusCode)
            {
                _cachedUsers.RemoveAll(user => user.Username == username);
                return true;
            }
            return false;
        }

        // Provide cached users without fetching
        public List<User> GetCachedUsers()
        {
            return _cachedUsers;
        }
    }
}
