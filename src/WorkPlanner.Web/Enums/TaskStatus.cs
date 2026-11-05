using System.ComponentModel.DataAnnotations;

namespace WorkPlanner.Web.Enums
{
    public enum TaskStatus
    {
        New,
        [Display(Name = "In Progress")]
        InProgress,
        Blocked,
        Completed,
        Cancelled

    }
}
