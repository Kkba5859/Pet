using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Pet.Repositories;
using Pet.Services.Admins;
using Pet.Services.Users;
using Blazored.LocalStorage;

namespace Pet
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            // ��������� HttpClient ��� ������ � API PetApi
            builder.Services.AddHttpClient("PetApi", client =>
            {
                client.BaseAddress = new Uri("http://localhost:5004"); // �������� �� HTTPS, ���� ���������
            });

            builder.Services.AddScoped(sp =>
                sp.GetRequiredService<IHttpClientFactory>().CreateClient("PetApi"));

            // ��������� HttpClient ��� ������ � API PetAdminApi
            builder.Services.AddHttpClient("PetAdminApi", client =>
            {
                client.BaseAddress = new Uri("http://localhost:5000"); // �������� �� ���������� �����
            });

            builder.Services.AddScoped(sp =>
                sp.GetRequiredService<IHttpClientFactory>().CreateClient("PetAdminApi"));

            // ���������� ��������� LocalStorage
            builder.Services.AddBlazoredLocalStorage();

            // �������� HttpClient ��� Blazor
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            // ����������� AuthService
            builder.Services.AddScoped<IAuthRepository, AuthRepository>();
            builder.Services.AddScoped<IRegisterService, RegisterService>();
            builder.Services.AddScoped<ILoginService, LoginService>();
            builder.Services.AddScoped<ILogoutService, LogoutService>();
            builder.Services.AddScoped<IAuthStateService, AuthStateService>();



            // ����������� AdmiAuthService
            builder.Services.AddScoped<IAdminAuthRepository, AdminAuthRepository>();
            builder.Services.AddScoped<IAdminRegisterService, AdminRegisterService>();
            builder.Services.AddScoped<IAdminLoginService, AdminLoginService>();

            // ����������� AdminDashboardService
            builder.Services.AddScoped<IAdminRepository, AdminRepository>();
            builder.Services.AddScoped<IAdminDashboardService, AdminDashboardService>();

            // ����������� NotificationService ��� Singleton
            builder.Services.AddSingleton<INotificationService, NotificationService>();
            builder.Services.AddSingleton<INotificationRepository, NotificationRepository>();

            await builder.Build().RunAsync();
        }
    }
}
