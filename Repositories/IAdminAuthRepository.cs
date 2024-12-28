using Pet.Models;

namespace Pet.Repositories
{
    public interface IAdminAuthRepository
    {
        Task<bool> RegisterAdminAsync(AdminRegisterModel model); // Регистрация администратора
        Task<bool> LoginAdminAsync(AdminLoginModel model);       // Вход администратора
    }
}
