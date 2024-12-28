using Pet.Models;
using Pet.Repositories;

namespace Pet.Services.Admins
{
    public class AdminLoginService : IAdminLoginService
    {
        private readonly IAdminAuthRepository _repository;

        public AdminLoginService(IAdminAuthRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Login(AdminLoginModel model)
        {
            return await _repository.LoginAdminAsync(model);
        }
    }
}
