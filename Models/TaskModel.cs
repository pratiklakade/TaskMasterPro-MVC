using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskMasterPro.Models.Enum;

namespace TaskMasterPro.Models
{
    public class TaskModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 

        [Required(ErrorMessage = "Task title is required")]
        public string Title { get; set; }

        public string? Description { get; set; }

        [Required]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        [Required]
        public TaskCategory Category { get; set; }

       
        public PriorityLevel Priority { get; set; } = PriorityLevel.Medium; // Default value

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }

       
        public virtual ApplicationUser? User { get; set; }  // Navigation property
    }
}
