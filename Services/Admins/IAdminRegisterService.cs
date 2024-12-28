using Pet.Models;

namespace Pet.Services.Admins
{
    public interface IAdminRegisterService
    {
        Task<bool> Register(AdminRegisterModel model);
    }
}
