
using System.ComponentModel.DataAnnotations;
using System.Security;

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
        [StringLength(20)]
        public string? Status { get; set; } = "New";
        [Required]
        [StringLength(20)]
        public string? Priority { get; set; } = "Medium";

        public DateTime? DueDate { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }

        public DateTime? CompletedDate { get; set; }


    }
}
