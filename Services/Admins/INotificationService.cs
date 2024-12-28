namespace Pet.Services.Admins
{
    public interface INotificationService : IAsyncDisposable
    {
        event Action<string>? OnUserDeleted;
        event Action<string>? OnAdminDeleted;
        Task StartConnectionAsync();
    }
}
