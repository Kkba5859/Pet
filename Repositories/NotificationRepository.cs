using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace Pet.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly HubConnection _hubConnection;

        public event Action<string>? OnUserDeleted;
        public event Action<string>? OnAdminDeleted;

        public NotificationRepository(NavigationManager navigation)
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(navigation.ToAbsoluteUri("http://localhost:5004/notificationHub"))
                .Build();

            _hubConnection.On<string>("UserDeleted", username => OnUserDeleted?.Invoke(username));
            _hubConnection.On<string>("AdminDeleted", username => OnAdminDeleted?.Invoke(username));
        }

        public async Task StartConnectionAsync()
        {
            await _hubConnection.StartAsync();
        }

        public async Task SendNotificationAsync(string message)
        {
            await _hubConnection.SendAsync("SendNotification", message);
        }

        public async ValueTask DisposeAsync()
        {
            await _hubConnection.DisposeAsync();
        }
    }
}
