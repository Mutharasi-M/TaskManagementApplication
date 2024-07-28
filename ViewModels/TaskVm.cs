using System.ComponentModel.DataAnnotations;
using TaskManagementApplication.Models;

namespace TaskManagementApplication.ViewModels
{
    public class TaskVm
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime? DateTime { get; set; }

        public Priority Priority { get; set; } = Priority.Low;

        public Status Status { get; set; } = Status.Pending;
    }
}
