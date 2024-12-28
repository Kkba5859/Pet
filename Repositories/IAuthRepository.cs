namespace Pet.Repositories
{
    public interface IAuthRepository
    {
        Task<string?> Login(string username, string password);
        Task<bool> Register(string username, string password);
    }
}
