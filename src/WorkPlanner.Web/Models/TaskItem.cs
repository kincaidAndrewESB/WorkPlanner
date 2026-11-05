
using System.ComponentModel.DataAnnotations;
using System.Security;
using WorkPlanner.Web.Enums;

namespace WorkPlanner.Web.Models
{
    public class TaskItem
    {
        public int TaskItemId { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; }
        [Required]
        public Enums.TaskStatus Status { get; set; } = Enums.TaskStatus.New;
        [Required]
        public Enums.TaskPriority Priority { get; set; } = Enums.TaskPriority.Medium;

        public DateTime? DueDate { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }

        public DateTime? CompletedDate { get; set; }


    }
}
