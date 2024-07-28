using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagementApplication.Models
{
    public class Task
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title cannot be longer than 100 characters")]
        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime? DateTime { get; set; }


        [Required(ErrorMessage = "Priority is required")]
        public Priority Priority { get; set; } = Priority.Low;


        [Required(ErrorMessage = "Status is required")]
        public Status Status { get; set; } = Status.Pending;

    }
}
