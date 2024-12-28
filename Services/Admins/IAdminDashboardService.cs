using Pet.Models;

namespace Pet.Services.Admins
{
    public interface IAdminDashboardService
    {
        Task<List<User>> GetUsersAsync();       // Fetch users from the API
        Task<bool> DeleteUserAsync(string username); // Delete a user by username
        List<User> GetCachedUsers();           // Retrieve cached user list
    }

    // User model to match the structure of the response from the controller
  
}
