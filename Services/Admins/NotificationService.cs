using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Pet.Repositories;

namespace Pet.Services.Admins
{
    public class NotificationService : INotificationService
    {
        private readonly HubConnection _hubConnection;

        public event Action<string>? OnUserDeleted;
        public event Action<string>? OnAdminDeleted;

        public NotificationService(NavigationManager navigation)
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(navigation.ToAbsoluteUri("http://localhost:5004/notificationHub"))
                .Build();

            // Подписка на уведомления
            _hubConnection.On<string>("UserDeleted", username => OnUserDeleted?.Invoke(username));
            _hubConnection.On<string>("AdminDeleted", username => OnAdminDeleted?.Invoke(username));
        }

        public async Task StartConnectionAsync()
        {
            if (_hubConnection.State == HubConnectionState.Disconnected)
            {
                try
                {
                    await _hubConnection.StartAsync();
                }
                catch (Exception ex)
                {
                    // Обработка ошибок подключения, например, логирование
                    Console.Error.WriteLine($"Error starting SignalR connection: {ex.Message}");
                }
            }
        }

        public async ValueTask DisposeAsync()
        {
            await _hubConnection.DisposeAsync();
        }
    }
}