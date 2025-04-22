using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TaskMasterPro.Models
{
    // Extending IdentityUser to store extra fields for your user
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Full Name is required")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Profile Picture URL")]
        public string? ProfilePictureUrl { get; set; }

        // Future enhancement: theme, settings, etc.
        public string? ThemePreference { get; set; }

        // Navigation property for tasks
        public virtual ICollection<TaskModel> Task { get; set; } = new List<TaskModel>();
    }
}
