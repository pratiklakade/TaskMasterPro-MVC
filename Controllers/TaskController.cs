using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskMasterPro.Data;
using TaskMasterPro.Models;
using TaskMasterPro.Models.Enum;

namespace TaskMasterPro.Controllers
{
    [Authorize]  // Ensures only logged-in users can access any action
    public class TaskController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        
        
        public TaskController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
   

        // GET: Task
        public async Task <IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var tasks = await _context.Tasks
                .Include(t => t.User)  // This loads the related user data
                .Where(t => t.UserId == userId)
                .ToListAsync();

            return View(tasks);
        }

        // GET: Task/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Task/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskModel model)
        {
           

            // Validate DueDate is in future
            if (model.DueDate < DateTime.Today)
            {
                ModelState.AddModelError("DueDate", "Due date must be in the future");
            }
            // Set default priority if not provided
            if (model.Priority == 0)
            {
                model.Priority = PriorityLevel.Medium;
            }

            // Get and set user info
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ModelState.AddModelError("", "User not found");
                return View(model);
            }

            model.UserId = user.Id;
            model.User = user;

            // Clear any existing UserId validation errors
            ModelState.Remove("UserId");

            model.CreatedAt = DateTime.Now;
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Tasks.Add(model);
                        await _context.SaveChangesAsync();

                        TempData["SuccessMessage"] = "Task created successfully!";
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Error creating task: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                // Add user-friendly message
                ModelState.AddModelError("", "An error occurred while saving. Please try again.");

                // For debugging, you can also add:
                ModelState.AddModelError("", $"Debug: {ex.Message}");
            }

            // Repopulate dropdowns if needed
            ViewBag.Categories = Enum.GetValues(typeof(TaskCategory))
                                   .Cast<TaskCategory>()
                                   .Select(e => new SelectListItem
                                   {
                                       Value = e.ToString(),
                                       Text = e.ToString()
                                   }).ToList();

            return View(model);
        }

        // GET: Task/Edit/5
        public async Task<IActionResult> Edit(int?id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks.FindAsync(id);

            if (task == null || task.UserId != _userManager.GetUserId(User))
            return Unauthorized();

            return View(task);
        }

        // POST: Task/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id , TaskModel model)
         {
            if(id != model.Id)
            {
                return NotFound();
            }

            // Validate DueDate is in future
            if (model.DueDate < DateTime.Today)
            {
                ModelState.AddModelError("DueDate", "Due date must be in the future");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existing = await _context.Tasks.FindAsync(id);

                    if (existing == null || existing.UserId != _userManager.GetUserId(User))
                        return Unauthorized();

                    existing.Title = model.Title;
                    existing.Description = model.Description;
                    existing.DueDate = model.DueDate;
                    existing.Category = model.Category;
                    existing.Priority = model.Priority;

                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Task updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error saving changes: {ex.Message}");
                }



            }
            return View(model);

         }

        public async Task<IActionResult>Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks.FindAsync(id);

            if (task == null || task.UserId != _userManager.GetUserId(User))
                return Unauthorized();

            return View(task);

        }

        // POST: Task/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null || task.UserId != _userManager.GetUserId(User))
                return Unauthorized();

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Task deleted successfully!";
            return RedirectToAction(nameof(Index));
        }


        public async  Task<IActionResult> Details(int?id)
        {
            if (id == null)
                return NotFound();

            var task = await _context.Tasks
         .FirstOrDefaultAsync(m => m.Id == id);

            if (task == null)
                return NotFound();

            var user = await _userManager.GetUserAsync(User);
            if (task.UserId != user.Id)
                return Unauthorized();

            return View(task);
        }


    }
}
