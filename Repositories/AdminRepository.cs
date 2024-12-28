using System.Net.Http.Json;
using Pet.Models;

namespace Pet.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly HttpClient _httpClient;

        public AdminRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<User>>("http://localhost:5004/api/AdminDashboard/getUsers") ?? new List<User>();
        }

        public async Task<bool> DeleteUserAsync(string username)
        {
            var response = await _httpClient.DeleteAsync($"http://localhost:5004/api/AdminDashboard/deleteUser/{username}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<User>> GetAdminsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<User>>("http://localhost:5004/api/AdminDashboard/getAdmins") ?? new List<User>();
        }

        public async Task<bool> DeleteAdminAsync(string username)
        {
            var response = await _httpClient.DeleteAsync($"http://localhost:5004/api/AdminDashboard/deleteAdmin/{username}");
            return response.IsSuccessStatusCode;
        }
    }
}
