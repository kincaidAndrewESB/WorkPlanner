namespace WorkPlanner.Web.Models
{
    public class DashboardStats
    {
        public int TotalTasks { get; set; }

        public int OpenTasks { get; set; }

        public int CompletedTasks { get; set; }

        public int OverdueTasks { get; set; }
    }
}
