using Pet.Models;

namespace Pet.Services.Admins
{
    public interface IAdminLoginService
    {
        Task<bool> Login(AdminLoginModel model);
    }
}
