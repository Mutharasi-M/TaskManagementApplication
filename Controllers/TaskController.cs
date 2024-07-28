using Microsoft.AspNetCore.Mvc;
using TaskManagementApplication.Helpers;
using TaskManagementApplication.Models;
using TaskManagementApplication.Services;

namespace TaskManagementApplication.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskManagement _taskManagement;

        public TaskController(ITaskManagement taskManagement)
        {
            _taskManagement = taskManagement;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Priority? priority, Status? status, string searchQuery, DateTime? fromDate, DateTime? toDate)
        {
            var TaskList = await _taskManagement.GetAllByFilterAsync(priority, status, searchQuery, fromDate, toDate);

            ViewBag.GetPriorityBadgeClass = (Func<Priority, string>)BadgeHelper.GetPriorityBadgeClass;
            ViewBag.GetStatusBadgeClass = (Func<Status, string>)BadgeHelper.GetStatusBadgeClass;

            return View(TaskList);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var TaskDetail = await _taskManagement.GetByIdAsync(id);

            return View(TaskDetail);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Models.Task task)
        {
            if (ModelState.IsValid)
            {
                await _taskManagement.AddTaskAsync(task);
            }

            return Redirect("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _taskManagement.GetByIdAsync(id);

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Models.Task task)
        {
            await _taskManagement.UpdateTaskAsync(task);

            return Redirect("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteConfirmation(int Id)
        {
            var result = await _taskManagement.GetByIdAsync(Id);

            return View(result);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _taskManagement.DeleteTaskAsync(id);

            return Redirect("Index");
        }        
    }
}
