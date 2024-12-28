namespace Pet.Repositories
{
    public interface INotificationRepository : IAsyncDisposable
    {
        Task StartConnectionAsync();
        Task SendNotificationAsync(string message);
        event Action<string>? OnUserDeleted;
        event Action<string>? OnAdminDeleted;
    }
}
