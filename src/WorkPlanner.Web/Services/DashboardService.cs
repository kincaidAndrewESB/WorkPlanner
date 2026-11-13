using Microsoft.EntityFrameworkCore;
using WorkPlanner.Web.Data;
using WorkPlanner.Web.Models;

namespace WorkPlanner.Web.Services
{
    public class DashboardService
    {
        private readonly ApplicationDbContext _context;

        public DashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardStats?> GetStatsAsync()
        {
            return await _context.DashboardStats
                .FromSqlRaw("EXEC sp_GetDashboardStats")
                .FirstOrDefaultAsync();
        }
    }
}
