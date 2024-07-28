using TaskManagementApplication.Models;

namespace TaskManagementApplication.Services
{
    public interface ITaskManagement
    {
        // all methods uses userid parameter

        /// <summary> Retrieves tasks filtered by priority, status and search with title or description.</summary>
        /// <param name="priority">The priority to filter by</param>
        /// <param name="status">The status to filter by</param>
        /// <param name="searchQuery">The title or description</param>
        /// <returns>A collection of filtered tasks.</returns>
        Task<IList<Models.Task>?> GetAllByFilterAsync(string userId, Priority? priority, Status? status, string searchQuery, DateTime? fromDate, DateTime? toDate);

        /// <summary> Retrieves task details by id. </summary>
        /// <param name="id">The id of the task.</param>
        /// <returns>The task with the specified id.</returns>
        Task<Models.Task?> GetByIdAsync(string userId, int id);

        /// <summary> Adds a new task. </summary>
        /// <param name="task">The task to add.</param>
        /// <returns>The added task.</returns>
        Task<Models.Task> AddTaskAsync(string userId, Models.Task task);

        /// <summary> Updates an existing task.</summary>
        /// <param name="task">The task to update.</param>
        /// <returns>The updated task.</returns>
        Task<Models.Task?> UpdateTaskAsync(string userId, Models.Task task);

        /// <summary> Deletes a task by id. </summary>
        /// <param name="id">The id of the task to delete.</param>
        /// <returns>The deleted task.</returns>
        Task<Models.Task?> DeleteTaskAsync(string userId, int id);
    }
}
