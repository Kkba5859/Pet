namespace Pet.Services.Users
{
    public interface ILogoutService
    {
        Task<bool> Logout(string username);
    }
}
