using Microsoft.EntityFrameworkCore;
using TaskManagementApplication.Data;
using TaskManagementApplication.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagementApplication.Services
{
    public class TaskManagement : ITaskManagement
    {
        private readonly TaskDbContext _taskDbContext;

        public TaskManagement(TaskDbContext taskDbContext)
        {
            _taskDbContext = taskDbContext;
        }

        public async Task<Models.Task> AddTaskAsync(Models.Task task)
        {
            await _taskDbContext.AddAsync(task);
            await _taskDbContext.SaveChangesAsync();

            return task;
        }

        public async Task<Models.Task?> DeleteTaskAsync(int id)
        {
            var result = await _taskDbContext.Tasks.FindAsync(id);

            if (result != null)
            {
                _taskDbContext.Tasks.Remove(result);
                await _taskDbContext.SaveChangesAsync();
            }

            return result;
        }

        public async Task<IList<Models.Task>?> GetAllByFilterAsync(Priority? priority, Status? status, string searchQuery, DateTime? fromDate, DateTime? toDate)
        {
            var tasks = _taskDbContext.Tasks.AsQueryable();

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
                tasks = tasks.Where(t => t.Title.Contains(searchQuery) || t.Description == null || t.Description.Contains(searchQuery));
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


        public async Task<Models.Task?> GetByIdAsync(int id)
        {
            var result = await _taskDbContext.Tasks.FindAsync(id);

            return result;
        }


        public async Task<Models.Task?> UpdateTaskAsync(Models.Task task)
        {
            var existingTask = await _taskDbContext.Tasks.FindAsync(task.Id);

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
    }
}
