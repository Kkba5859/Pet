namespace Pet.Services
{
    public interface IRegisterService
    {
        Task<bool> Register(string username, string password);
    }
}
