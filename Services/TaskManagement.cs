using Microsoft.EntityFrameworkCore;
using TaskManagementApplication.Models;

namespace TaskManagementApplication.Services
{
    public class TaskManagement : ITaskManagement
    {
        private readonly TaskDbContext _taskDbContext;

        public TaskManagement(TaskDbContext taskDbContext)
        {
            _taskDbContext = taskDbContext;
        }

        public async Task<IList<Models.Task>?> GetAllByFilterAsync(string userId, Priority? priority, Status? status, string searchQuery, DateTime? fromDate, DateTime? toDate)
        {
            var tasks = _taskDbContext.Tasks.Where(t => t.UserId == userId).AsQueryable();

            if (priority.HasValue)
            {
                tasks = tasks.Where(t => t.Priority == priority.Value);
            }

            if (status.HasValue)
            {
                tasks = tasks.Where(t => t.Status == status.Value);
            }

            if (!string.IsNullOrEmpty(searchQuery))
            {
                tasks = tasks.Where(t => t.Title.Contains(searchQuery) || t.Description.Contains(searchQuery));
            }

            if (fromDate.HasValue)
            {
                tasks = tasks.Where(t => t.DateTime >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                tasks = tasks.Where(t => t.DateTime <= toDate.Value);
            }

            return await tasks.ToListAsync();
        }

        public async Task<Models.Task?> GetByIdAsync(string userId, int id)
        {
            return await _taskDbContext.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
        }

        public async Task<Models.Task> AddTaskAsync(string userId, Models.Task task)
        {
            task.UserId = userId;
            await _taskDbContext.Tasks.AddAsync(task);
            await _taskDbContext.SaveChangesAsync();
            return task;
        }

        public async Task<Models.Task?> UpdateTaskAsync(string userId, Models.Task task)
        {
            var existingTask = await _taskDbContext.Tasks.FirstOrDefaultAsync(t => t.Id == task.Id && t.UserId == userId);

            if (existingTask != null)
            {
                existingTask.Title = task.Title;
                existingTask.Description = task.Description;
                existingTask.DateTime = task.DateTime;
                existingTask.Priority = task.Priority;
                existingTask.Status = task.Status;

                _taskDbContext.Tasks.Update(existingTask);
                await _taskDbContext.SaveChangesAsync();
            }

            return existingTask;
        }

        public async Task<Models.Task?> DeleteTaskAsync(string userId, int id)
        {
            var task = await _taskDbContext.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if (task != null)
            {
                _taskDbContext.Tasks.Remove(task);
                await _taskDbContext.SaveChangesAsync();
            }

            return task;
        }
    }
}
