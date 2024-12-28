using Pet.Repositories;
using Pet.Models;

namespace Pet.Services.Admins
{
    public class AdminRegisterService : IAdminRegisterService
    {
        private readonly IAdminAuthRepository _repository;

        public AdminRegisterService(IAdminAuthRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Register(AdminRegisterModel model)
        {
            return await _repository.RegisterAdminAsync(model);
        }
    }
}
