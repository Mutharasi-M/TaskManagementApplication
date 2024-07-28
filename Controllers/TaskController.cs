using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApplication.Areas.Identity.Data;
using TaskManagementApplication.Helpers;
using TaskManagementApplication.Models;
using TaskManagementApplication.Services;
using TaskManagementApplication.ViewModels;

namespace TaskManagementApplication.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly ITaskManagement _taskManagement;
        private readonly UserManager<ApplicationUser> _userManager;

        public TaskController(ITaskManagement taskManagement, UserManager<ApplicationUser> userManager)
        {
            _taskManagement = taskManagement;
            _userManager = userManager;
        }

        private async Task<string> GetUserId()
        {
            var user = await _userManager.GetUserAsync(User);
            return user?.Id;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Priority? priority, Status? status, string searchQuery, DateTime? fromDate, DateTime? toDate)
        {
            var userId = await GetUserId();
            var tasks = await _taskManagement.GetAllByFilterAsync(userId, priority, status, searchQuery, fromDate, toDate);

            var taskVms = tasks?.Select(task => new TaskVm
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DateTime = task.DateTime,
                Priority = task.Priority,
                Status = task.Status
            }).ToList();

            return View(taskVms);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var userId = await GetUserId();
            var task = await _taskManagement.GetByIdAsync(userId, id);

            if (task == null)
            {
                return NotFound();
            }

            var taskVm = new TaskVm
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DateTime = task.DateTime,
                Priority = task.Priority,
                Status = task.Status
            };

            return View(taskVm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskVm taskVm)
        {
            if (ModelState.IsValid)
            {
                var userId = await GetUserId();
                var task = new Models.Task
                {
                    Title = taskVm.Title,
                    Description = taskVm.Description,
                    DateTime = taskVm.DateTime,
                    Priority = taskVm.Priority,
                    Status = taskVm.Status,
                    UserId = userId
                };

                await _taskManagement.AddTaskAsync(userId, task);
                return RedirectToAction("Index");
            }

            return View(taskVm);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = await GetUserId();
            var task = await _taskManagement.GetByIdAsync(userId, id);

            if (task == null) return NotFound();

            var taskVm = new TaskVm
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DateTime = task.DateTime,
                Priority = task.Priority,
                Status = task.Status
            };

            return View(taskVm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TaskVm taskVm)
        {
            ModelState.Remove("UserId");

            if (ModelState.IsValid)
            {
                var userId = await GetUserId();
                var task = new Models.Task
                {
                    Id = taskVm.Id,
                    Title = taskVm.Title,
                    Description = taskVm.Description,
                    DateTime = taskVm.DateTime,
                    Priority = taskVm.Priority,
                    Status = taskVm.Status,
                    UserId = userId
                };

                await _taskManagement.UpdateTaskAsync(userId, task);
                return RedirectToAction("Index");
            }

            return View(taskVm);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteConfirmation(int id)
        {
            var userId = await GetUserId();
            var task = await _taskManagement.GetByIdAsync(userId, id);

            if (task == null)
            {
                return NotFound();
            }

            var taskVm = new TaskVm
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DateTime = task.DateTime,
                Priority = task.Priority,
                Status = task.Status
            };

            return View(taskVm);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = await GetUserId();
            await _taskManagement.DeleteTaskAsync(userId, id);

            return RedirectToAction("Index");
        }
    }
}
