﻿namespace Pet.Services.Users
{
    public interface ILoginService
    {
        Task<bool> Login(string username, string password);
        Task<string?> GetToken();
    }
}
