
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkPlanner.Web.Models;
using WorkPlanner.Web.Data;
using WorkPlanner.Web.Enums;

public class TasksController : Controller
{
    private readonly ApplicationDbContext _context;

    public TasksController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: TASKITEMS
    //public async Task<IActionResult> Index(string searchTerm)
    //{
    //    var tasks = _context.Tasks.AsQueryable();

    //    if (!string.IsNullOrEmpty(searchTerm))
    //    {
    //        tasks = tasks.Where(t => t.Title.Contains(searchTerm) || (!string.IsNullOrEmpty(t.Description) && t.Description.Contains(searchTerm)));
    //    }

    //    return View(await tasks.OrderBy(t => t.Status).ThenBy(t => t.DueDate).ToListAsync());
    //}

    public async Task<IActionResult> Index(
    string searchTerm,
    WorkPlanner.Web.Enums.TaskStatus? status,
    TaskPriority? priority)
    {
        var tasks = _context.Tasks.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            tasks = tasks.Where(t =>
                t.Title.Contains(searchTerm));
        }

        if (!string.IsNullOrEmpty(status.ToString()) && status.HasValue)
        {
            tasks = tasks.Where(t => t.Status == status);
        }

        if (priority.HasValue)
        {
            tasks = tasks.Where(t =>
                t.Priority == priority);
        }

        return View(await tasks.ToListAsync());
    }

    // GET: TASKITEMS/Details/5
    public async Task<IActionResult> Details(int? taskitemid)
    {
        if (taskitemid == null)
        {
            return NotFound();
        }

        var taskitem = await _context.Tasks
            .FirstOrDefaultAsync(m => m.TaskItemId == taskitemid);
        if (taskitem == null)
        {
            return NotFound();
        }

        return View(taskitem);
    }

    // GET: TASKITEMS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: TASKITEMS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("TaskItemId,Title,Description,Status,Priority,DueDate,CreatedDate,ModifiedDate,CompletedDate")] TaskItem taskitem)
    {
        if (ModelState.IsValid)
        {
            taskitem.CreatedDate = DateTime.Now;
            _context.Add(taskitem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(taskitem);
    }

    // GET: TASKITEMS/Edit/5
    public async Task<IActionResult> Edit(int? taskitemid)
    {
        if (taskitemid == null)
        {
            return NotFound();
        }

        var taskitem = await _context.Tasks.FindAsync(taskitemid);
        if (taskitem == null)
        {
            return NotFound();
        }
        return View(taskitem);
    }

    // POST: TASKITEMS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? taskitemid, [Bind("TaskItemId,Title,Description,Status,Priority,DueDate,CreatedDate,ModifiedDate,CompletedDate")] TaskItem taskitem)
    {
        if (taskitemid != taskitem.TaskItemId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                taskitem.ModifiedDate = DateTime.Now;
                if (taskitem.Status == WorkPlanner.Web.Enums.TaskStatus.Completed && taskitem.CompletedDate == null)
                {
                    taskitem.CompletedDate = DateTime.Now;
                }
                _context.Update(taskitem);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskItemExists(taskitem.TaskItemId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(taskitem);
    }

    [HttpPost]
    public async Task<IActionResult> Complete(int id)
    {
        var task = await _context.Tasks.FindAsync(id);

        if (task == null)
        {
            return NotFound();
        }

        task.Status = WorkPlanner.Web.Enums.TaskStatus.Completed;
        task.CompletedDate = DateTime.Now;

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // GET: TASKITEMS/Delete/5
    public async Task<IActionResult> Delete(int? taskitemid)
    {
        if (taskitemid == null)
        {
            return NotFound();
        }

        var taskitem = await _context.Tasks
            .FirstOrDefaultAsync(m => m.TaskItemId == taskitemid);
        if (taskitem == null)
        {
            return NotFound();
        }

        return View(taskitem);
    }

    // POST: TASKITEMS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? taskitemid)
    {
        var taskitem = await _context.Tasks.FindAsync(taskitemid);
        if (taskitem != null)
        {
            _context.Tasks.Remove(taskitem);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TaskItemExists(int? taskitemid)
    {
        return _context.Tasks.Any(e => e.TaskItemId == taskitemid);
    }
}
