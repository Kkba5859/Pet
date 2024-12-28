namespace Pet.Services.Users
{
    public interface IRegisterService
    {
        Task<bool> Register(string username, string password);
    }
}
