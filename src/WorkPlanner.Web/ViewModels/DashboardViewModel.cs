namespace WorkPlanner.Web.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalTasks { get; set; }

        public int OpenTasks { get; set; }

        public int CompletedTasks { get; set; }

        public int DueToday { get; set; }

        public int OverdueTasks { get; set; }
    }
}
