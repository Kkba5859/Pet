using Pet.Repositories;

namespace Pet.Services.Users
{
    public class RegisterService : IRegisterService
    {
        private readonly IAuthRepository _authRepository;

        public RegisterService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<bool> Register(string username, string password)
        {
            return await _authRepository.Register(username, password);
        }
    }
}
