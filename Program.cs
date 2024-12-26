using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using Pet.Services;

namespace Pet
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            // ��������� HttpClient ��� ������ � API
            builder.Services.AddHttpClient("PetApi", client =>
            {
                client.BaseAddress = new Uri("http://localhost:5000"); // ���������, ��� ��� HTTPS-�����
            });

            builder.Services.AddScoped(sp =>
                sp.GetRequiredService<IHttpClientFactory>().CreateClient("PetApi"));

            // ���������� ��������� LocalStorage
            builder.Services.AddBlazoredLocalStorage();

            // �������� HttpClient ��� Blazor
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            // ����������� AuthService
            builder.Services.AddScoped<IRegisterService, RegisterService>();
            builder.Services.AddScoped<ILoginService, LoginService>();

            await builder.Build().RunAsync();
        }
    }
}