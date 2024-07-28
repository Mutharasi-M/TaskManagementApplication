using TaskManagementApplication.Models;

namespace TaskManagementApplication.Helpers
{
    public static class BadgeHelper
    {
        public static string GetPriorityBadgeClass(Priority priority)
        {
            return priority switch
            {
                Priority.Low => "bg-secondary",
                Priority.Medium => "bg-warning",
                Priority.High => "bg-danger",
                _ => "bg-secondary"
            };
        }

        public static string GetStatusBadgeClass(Status status)
        {
            return status switch
            {
                Status.Pending => "bg-info",
                Status.InProgress => "bg-primary",
                Status.Completed => "bg-success",
                _ => "bg-info"
            };
        }
    }
}
