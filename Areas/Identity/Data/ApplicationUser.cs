using Microsoft.AspNetCore.Identity;

namespace TaskManagementApplication.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Models.Task> Tasks { get; set; }

    }
}
