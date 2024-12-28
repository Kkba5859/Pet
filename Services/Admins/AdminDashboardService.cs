using System.Net.Http.Json;
using Pet.Models;

namespace Pet.Services.Admins
{
    public class AdminDashboardService : IAdminDashboardService
    {
        private readonly HttpClient _httpClient;
        private List<User> _cachedUsers = new();
        private List<User> _cachedAdmins = new();

        public AdminDashboardService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Универсальный метод для получения данных и кеширования
        private async Task<List<User>> FetchAndCacheAsync(string endpoint, List<User> cache)
        {
            var result = await _httpClient.GetFromJsonAsync<List<User>>(endpoint) ?? new List<User>();
            cache.Clear();
            cache.AddRange(result);
            return cache;
        }

        // Fetch users and cache the result
        public async Task<List<User>> GetUsersAsync()
        {
            return await FetchAndCacheAsync("http://localhost:5004/api/AdminDashboard/getUsers", _cachedUsers);
        }

        // Fetch admins and cache the result
        public async Task<List<User>> GetAdminsAsync()
        {
            return await FetchAndCacheAsync("http://localhost:5004/api/AdminDashboard/getAdmins", _cachedAdmins);
        }

        // Универсальный метод для удаления и обновления кеша
        private async Task<bool> DeleteAndUpdateCacheAsync(string endpoint, string username, List<User> cache)
        {
            var response = await _httpClient.DeleteAsync($"{endpoint}/{username}");
            if (response.IsSuccessStatusCode)
            {
                cache.RemoveAll(user => user.Username == username);
                return true;
            }
            return false;
        }

        // Delete user and update the cached list
        public async Task<bool> DeleteUserAsync(string username)
        {
            return await DeleteAndUpdateCacheAsync("http://localhost:5004/api/AdminDashboard/deleteUser", username, _cachedUsers);
        }

        // Delete admin and update the cached list
        public async Task<bool> DeleteAdminAsync(string username)
        {
            return await DeleteAndUpdateCacheAsync("http://localhost:5004/api/AdminDashboard/deleteAdmin", username, _cachedAdmins);
        }

        // Provide cached users without fetching
        public List<User> GetCachedUsers() => _cachedUsers;

        // Provide cached admins without fetching
        public List<User> GetCachedAdmins() => _cachedAdmins;
    }
}
