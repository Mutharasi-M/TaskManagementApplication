Task Management Application


Overview

  This project is an ASP.NET Core MVC application for managing tasks.
  It includes features for creating, editing, viewing, and deleting tasks, 
  with additional functionality for filtering and searching tasks.
  The application uses Entity Framework Core for data access and Microsoft Identity for user authentication.





Features

  Task Management: Create, read, update, delete tasks.
  
  Filtering and Searching: Filter tasks by status, priority, and due date. Search tasks by title or description.
  
  User Authentication: Multiple users can register, login, and manage their tasks.
  
  Responsive Design: Uses Bootstrap for a clean and responsive UI.
  
  Task Notifications: Reminders for tasks nearing their due date.
  
  Validation: Client-side and server-side validation for task forms.






Technologies Used

  ASP.NET Core MVC - version 6
  
  Entity Framework Core
  
  Microsoft Identity
  
  Bootstrap
  
  jQuery





Prerequisites

  .NET 6 SDK
  
  Visual Studio 2022

  SQL Server






Getting Started 

Setup and Installation



For Database:

  Update the appsettings.json file with your SQL Server connection string.

  update-database
  
  The application uses Entity Framework Core for data access. Ensure the database is properly configured and migrations are applied.




Installed Packages

  Microsoft.AspNetCore.Identity.EntityFrameworkCore --version 6.0.32
  
  Microsoft.AspNetCore.Identity.UI --version 6.0.32
  
  Microsoft.EntityFrameworkCore --version 6.0.32
  
  Microsoft.EntityFrameworkCore.SqlServer --version 6.0.32
  
  Microsoft.EntityFrameworkCore.Tools --version 6.0.32
  
  Microsoft.VisualStudio.Web.CodeGeneration.Design --version 6.0.17




Database Setup
Task Model - c# code

public class Task
{

    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, ErrorMessage = "Title cannot be longer than 100 characters")]
    public string Title { get; set; }

    public string? Description { get; set; }

    public DateTime? DateTime { get; set; }


    [Required(ErrorMessage = "Priority is required")]
    public Priority Priority { get; set; } = Priority.Low;


    [Required(ErrorMessage = "Status is required")]
    public Status Status { get; set; } = Status.Pending;

    [ForeignKey("User")]
    public string UserId { get; set; }

    public ApplicationUser User { get; set; }

}

public enum Priority
{
    Low,
    Medium,
    High
}

public enum Status
{
    Pending,
    InProgress,
    Completed
}







Views
  Index View
    Displays a list of tasks with filtering options by status, priority, search with title and description.
    ![image](https://github.com/user-attachments/assets/819627e7-b3ea-472a-86cf-f69eb23674a7)

  Details View
    Shows detailed information about a task.
    ![image](https://github.com/user-attachments/assets/6d899c5a-61e4-4a65-8c33-46cdc3800cef)

  Create View
    Form to add a new task.
    ![image](https://github.com/user-attachments/assets/8a33978b-4704-4978-a023-2819842b20da)

  Edit View
    Form to edit an existing task.
    ![image](https://github.com/user-attachments/assets/bf7cd49e-57ae-4682-a987-e5afe70aa616)

  
  Delete View
    Confirmation page to delete a task.
    ![image](https://github.com/user-attachments/assets/78a3d4a2-c1db-4c3c-a9af-ae8600aea36c)


Controllers

  TaskController
    Handles CRUD operations for tasks.



Styling
  This application uses Bootstrap for a clean and responsive design. Different styles or badges indicate task priority and status.



Validation
  Client-side and server-side validation are implemented for task forms to ensure data integrity.




Search and Filter
  Search: Find tasks by title or description.
  Filter: Filter tasks by due date, priority, and status.


  
Bonus Features

  User Authentication
    Implemented using Microsoft Identity, allowing multiple users to register, login, and manage their tasks.

  
  upcoming reminder
    Live Countdown: Displays a live countdown timer for tasks that are upcoming within 3 days.



-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

