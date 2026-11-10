using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using WorkPlanner.Models;
using WorkPlanner.Web.Data;
using WorkPlanner.Web.Enums;
using WorkPlanner.Web.ViewModels;

namespace WorkPlanner.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
            

        }


        public async Task<IActionResult> Index()
        {
            ViewBag.RecentTasks = await _context.Tasks
                            .OrderByDescending(t => t.CreatedDate)
                            .Take(5)
                            .ToListAsync();

            

            var model = new DashboardViewModel
            {
                TotalTasks = await _context.Tasks.CountAsync(),

                OpenTasks = await _context.Tasks
                    .CountAsync(t =>
                        t.Status != Web.Enums.TaskStatus.Completed &&
                        t.Status != Web.Enums.TaskStatus.Cancelled),

                CompletedTasks = await _context.Tasks
                    .CountAsync(t =>
                        t.Status == Web.Enums.TaskStatus.Completed),

                OverdueTasks = await _context.Tasks
                    .CountAsync(t =>
                        t.DueDate < DateTime.Today &&
                        t.Status != Web.Enums.TaskStatus.Completed),

                
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
