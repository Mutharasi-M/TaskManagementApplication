namespace TaskManagementApplication.Services
{
    public interface INotificationService
    {
        System.Threading.Tasks.Task SendNotificationAsync(Models.Task task);
    }
}
