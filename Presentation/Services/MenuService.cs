using Infrastructure.Entities;
using Infrastructure.Services;
using System.Text.RegularExpressions;

namespace Presentation.Services;

public class MenuService(UserService userService, TaskService taskService, AssignmentService assignmentService)
{
    private readonly UserService _userService = userService;
    private readonly TaskService _taskService = taskService;
    private readonly AssignmentService _assignmentService = assignmentService;

    public void ShowMenu()
    {
        Console.Clear();

        Console.WriteLine("1. Show all tasks");
        Console.WriteLine("");
        Console.WriteLine("2. Show all users");
        Console.WriteLine("");
        Console.WriteLine("3. Create User");
        Console.WriteLine("");
        Console.WriteLine("4. Add new task");
        Console.WriteLine("");
        Console.WriteLine("5. Update a task or a user");
        Console.WriteLine("");
        Console.WriteLine("6. Delete task or user");
        Console.WriteLine("");
        Console.WriteLine("7. Exit");
        Console.WriteLine("");
        Console.Write("Enter your choice: ");

        switch (Console.ReadLine())
        {
            case "1":
                ShowAllTasksMenu();
                break;
            case "2":
                ShowAllUsersMenu();
                break;
            case "3":
                CreateUserMenu();
                break;
            case "4":
                CreateTaskMenu();
                break;
            case "5":
                UpdateMenu();
                break;
            case "6":
                DeleteMenu();
                break;
            case "7":
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Invalid input, please try again");
                ShowMenu();
                break;
        }
    }

    public void ShowAllUsersMenu()
    {
        Console.Clear();

        var users = _userService.GetAllUsers();
        if (users == null)
        {
            Console.WriteLine("There are no users in the database");
            Console.WriteLine("");
            Console.WriteLine("Press any key to go to main menu");
            Console.ReadKey();
            ShowMenu();
        }
        else
        {
            foreach (var user in users)
            {
                Console.WriteLine($"User: {user.Name} with the email: {user.Email}");
                Console.WriteLine("--------------------------------------------------------------------------------------");
            }
        }

        Console.WriteLine("");
        Console.WriteLine("Press any key to go to main menu");
        Console.ReadKey();
        ShowMenu();
    }

    public void ShowAllTasksMenu()
    {
        Console.Clear();

        var assignments = _assignmentService.GetAllAssignments();
        if (assignments == null)
        {
            Console.WriteLine("There are no tasks in the database");
            Console.WriteLine("");
            Console.WriteLine("Press any key to go to main menu");
            Console.ReadKey();
            ShowMenu();
        }
        else
        {
            foreach (var assignment in assignments)
            {
                Console.WriteLine($"User: {assignment.User.Name} with the email: {assignment.User.Email} has the task: {assignment.Task.TaskName} with the status : {assignment.Task.Status.StatusName}");
                Console.WriteLine($"Description: {assignment.Task.Description}");
                Console.WriteLine($"Category: {assignment.Task.Category.CategoryName}");
                Console.WriteLine("--------------------------------------------------------------------------------------");
            }
        }

        Console.WriteLine("");
        Console.WriteLine("Press any key to go to main menu");
        Console.ReadKey();
        ShowMenu();

    }


    public void CreateTaskMenu()
    {
        Console.Clear();

        if (!_userService.AreThereAnyUsers())
        {
            Console.WriteLine("There are no users in the database, you need to create a user first");
            Console.WriteLine("");
            Console.WriteLine("Press any key to go to create user menu");
            Console.ReadKey();
            CreateUserMenu();
            return;
        }

        Console.WriteLine("");
        Console.WriteLine("Enter task name");
        var taskName = Console.ReadLine()!;
        if (string.IsNullOrEmpty(taskName))
        {
            Console.WriteLine("Task name cannot be empty. Please enter a valid task name.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            CreateTaskMenu();
        }

        var existingTask = _taskService.GetTaskByName(taskName);
        if (existingTask != null)
        {
            Console.WriteLine("The provided task name already exists. Please provide a different task name.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            CreateTaskMenu();
        }

        Console.WriteLine("");
        Console.WriteLine("Enter task description");
        var taskDescription = Console.ReadLine()!;

        Console.WriteLine("");
        Console.WriteLine("Enter task status");
        var taskStatus = Console.ReadLine()!;
        if (string.IsNullOrEmpty(taskStatus))
        {
            Console.WriteLine("Task status cannot be empty. Please enter a valid task status.");
            CreateTaskMenu();
        }
        Console.WriteLine("");
        Console.WriteLine("Enter task category");
        var taskCategory = Console.ReadLine()!;
        if (string.IsNullOrEmpty(taskCategory))
        {
            Console.WriteLine("Task category cannot be empty. Please enter a valid task category.");
            CreateTaskMenu();
        }
        Console.WriteLine("");
        Console.WriteLine("Enter the email of the user with the task");
        var userEmail = Console.ReadLine()!;
        if (string.IsNullOrEmpty(userEmail))
        {
            Console.WriteLine("User email cannot be empty. Please enter a valid user email.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            CreateTaskMenu();
        }
        else
        {
            var user = _userService.GetUserByEmail(userEmail);
            if (user == null)
            {
                Console.WriteLine("No user with the provided email exists. Please enter a valid user email.");
                Console.WriteLine("Press any key to go back");
                Console.ReadKey();
                CreateTaskMenu();
            }
        }
        Console.WriteLine("");


        var task = new TaskEntity
        {
            TaskName = taskName,
            Description = taskDescription
        };

        var status = new StatusEntity
        {
            StatusName = taskStatus
        };

        var category = new CategoryEntity
        {
            CategoryName = taskCategory
        };

        var userId = _userService.GetIdByEmail(userEmail);
        if (userId == 0)
        {
            Console.WriteLine("The provided email does not exist in the database. Please provide a valid email.");
            return;
        }


        if (_taskService.CreateTask(task, status, category, userId))
        {
            Console.WriteLine("Task created successfully");
        }
        else
        {
            Console.WriteLine("Task already exists, try again");
        }

        Console.WriteLine("");
        Console.WriteLine("Press any key to go to main menu");
        Console.ReadKey();
        ShowMenu();

    }


    public void CreateUserMenu()
    {
        Console.Clear();

        Console.WriteLine("Enter user name");
        var userName = Console.ReadLine()!;
        if (string.IsNullOrEmpty(userName))
        {
            Console.WriteLine("User name cannot be empty. Please enter a valid user name.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            CreateUserMenu();
        }
        Console.WriteLine("Enter user email");
        var userEmail = Console.ReadLine()!;

        if (string.IsNullOrEmpty(userEmail))
        {
            Console.WriteLine("User email cannot be empty. Please enter a valid user email.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            CreateUserMenu();
        }

        var emailRegex = new Regex(@"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$");
        if (!emailRegex.IsMatch(userEmail))
        {
            Console.WriteLine("Invalid email format. Please enter a valid user email.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            CreateUserMenu();
        }

        var existingUser = _userService.GetUserByEmail(userEmail);
        if (existingUser != null)
        {
            Console.WriteLine("The provided email already exists in the database. Please provide a different email.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            CreateUserMenu();
        }

        var user = new UserEntity
        {
            Name = userName,
            Email = userEmail
        };

        if (_userService.CreateUser(user))
        {
            Console.WriteLine("User created successfully");
        }
        else
        {
            Console.WriteLine("User already exists, try again");
        }

        Console.WriteLine("");
        Console.WriteLine("Press any key to go to main menu");
        Console.ReadKey();
        ShowMenu();
    }

    public void UpdateMenu()
    {
        Console.Clear();

        Console.WriteLine("1. Update User");
        Console.WriteLine("2. Update Task");
        Console.WriteLine("3. Go back to main menu");
        Console.Write("Enter your choice: ");

        switch (Console.ReadLine())
        {
            case "1":
                UpdateUserMenu();
                break;
            case "2":
                UpdateTaskMenu();
                break;
            case "3":
                ShowMenu();
                break;
            default:
                Console.WriteLine("Invalid input, please try again");
                UpdateMenu();
                break;
        }
    }

    public void UpdateUserMenu()
    {
        Console.Clear();

        Console.WriteLine("Enter current user email");
        var currentEmail = Console.ReadLine()!;
        if (string.IsNullOrEmpty(currentEmail))
        {
            Console.WriteLine("User email cannot be empty. Please enter a valid user email.");
            UpdateUserMenu();
        }

        var user = _userService.GetUserByEmail(currentEmail);
        if (user == null)
        {
            Console.WriteLine("User does not exist, try again");
            Console.WriteLine("");
            Console.WriteLine("Press any key to go to update menu");
            Console.ReadKey();
            UpdateMenu();
        }

        Console.WriteLine("Enter new user name");
        var newUserName = Console.ReadLine()!;
        if (string.IsNullOrEmpty(newUserName))
        {
            Console.WriteLine("User name cannot be empty. Please enter a valid user name.");
            UpdateUserMenu();
        }

        Console.WriteLine("Enter new user email");
        var newEmail = Console.ReadLine()!;
        if (string.IsNullOrEmpty(newEmail))
        {
            Console.WriteLine("User email cannot be empty. Please enter a valid user email.");
            UpdateUserMenu();
        }

        if (_userService.GetUserByEmail(newEmail) != null)
        {
            Console.WriteLine("Email already exists, try again");
            Console.WriteLine("");
            Console.WriteLine("Press any key to go to update menu");
            Console.ReadKey();
            UpdateMenu();
        }

        user.Name = newUserName;
        user.Email = newEmail;

        if (_userService.UpdateUser(user))
        {
            Console.WriteLine("User updated successfully");
        }
        else
        {
            Console.WriteLine("An error occurred, try again");
        }

        Console.WriteLine("");
        Console.WriteLine("Press any key to go to main menu");
        Console.ReadKey();
        ShowMenu();
    }

    public void UpdateTaskMenu()
    {
        Console.Clear();
        Console.WriteLine("Enter current task name");
        var currentTaskName = Console.ReadLine()!;
        if (string.IsNullOrEmpty(currentTaskName))
        {
            Console.WriteLine("Task name cannot be empty. Please enter a valid task name.");
            UpdateTaskMenu();
        }

        var task = _taskService.GetTaskByName(currentTaskName);
        if (task == null)
        {
            Console.WriteLine("Task does not exist, try again");
            Console.WriteLine("");
            Console.WriteLine("Press any key to go to update menu");
            Console.ReadKey();
            UpdateMenu();
        }

        Console.WriteLine("Enter new task name");
        var newTaskName = Console.ReadLine()!;
        if (string.IsNullOrEmpty(newTaskName))
        {
            Console.WriteLine("Task name cannot be empty. Please enter a valid task name.");
            Console.WriteLine("Press any key to go to update menu");
            Console.ReadKey();
            UpdateTaskMenu();
        }
        var existingTask = _taskService.GetTaskByName(newTaskName);
        if (existingTask != null)
        {
            Console.WriteLine("The provided task name already exists. Please provide a different task name.");
            Console.WriteLine("Press any key to go to update menu");
            Console.ReadKey();
            UpdateTaskMenu();
        }

        Console.WriteLine("Enter new task description if you want");
        var newTaskDescription = Console.ReadLine()!;


        Console.WriteLine("Enter new task status");
        var newTaskStatus = Console.ReadLine()!;
        if (string.IsNullOrEmpty(newTaskStatus))
        {
            Console.WriteLine("Task status cannot be empty. Please enter a valid task status.");
            Console.WriteLine("Press any key to go to update menu");
            Console.ReadKey();
            UpdateTaskMenu();
        }

        Console.WriteLine("Enter new task category");
        var newTaskCategory = Console.ReadLine()!;
        if (string.IsNullOrEmpty(newTaskCategory))
        {
            Console.WriteLine("Task category cannot be empty. Please enter a valid task category.");
            Console.WriteLine("Press any key to go to update menu");
            Console.ReadKey();
            UpdateTaskMenu();
        }

        task.TaskName = newTaskName;
        task.Description = newTaskDescription;
        task.Status.StatusName = newTaskStatus;
        task.Category.CategoryName = newTaskCategory;

        if (_taskService.UpdateTask(task))
        {
            Console.WriteLine("Task updated successfully");
        }
        else
        {
            Console.WriteLine("An error occurred, try again");
        }

        Console.WriteLine("");
        Console.WriteLine("Press any key to go to main menu");
        Console.ReadKey();
        ShowMenu();
    }

    public void DeleteMenu()
    {
        Console.Clear();

        Console.WriteLine("1. Delete User");
        Console.WriteLine("2. Delete Task");
        Console.WriteLine("3. Go back to main menu");
        Console.Write("Enter your choice: ");

        switch (Console.ReadLine())
        {
            case "1":
                DeleteUserMenu();
                break;
            case "2":
                DeleteTaskMenu();
                break;
            case "3":
                ShowMenu();
                break;
            default:
                Console.WriteLine("Invalid input, please try again");
                DeleteMenu();
                break;
        }
    }

    public void DeleteUserMenu()
    {
        Console.Clear();

        Console.WriteLine("Enter user email");
        var userEmail = Console.ReadLine()!;
        if (string.IsNullOrEmpty(userEmail))
        {
            Console.WriteLine("User email cannot be empty. Please enter a valid user email.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            DeleteUserMenu();
        }

        var user = _userService.GetUserByEmail(userEmail);
        if (user == null)
        {
            Console.WriteLine("User does not exist, try again");
            Console.WriteLine("");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            DeleteUserMenu();
        }

        if (_userService.DeleteUser(userEmail))
        {
            Console.WriteLine("User deleted successfully");
        }
        else
        {
            Console.WriteLine("An error occurred, try again");
        }

        Console.WriteLine("");
        Console.WriteLine("Press any key to go to main menu");
        Console.ReadKey();
        ShowMenu();
    }

    public void DeleteTaskMenu()
    {
        Console.Clear();

        Console.WriteLine("Enter task name");
        var taskName = Console.ReadLine()!;
        if (string.IsNullOrEmpty(taskName))
        {
            Console.WriteLine("Task name cannot be empty. Please enter a valid task name.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            DeleteTaskMenu();
        }

        var task = _taskService.GetTaskByName(taskName);
        if (task == null)
        {
            Console.WriteLine("Task does not exist, try again");
            Console.WriteLine("");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            DeleteTaskMenu();
            return;
        }

        if (_taskService.DeleteTask(taskName))
        {
            Console.WriteLine("Task deleted successfully");
        }
        else
        {
            Console.WriteLine("An error occurred, try again");
        }

        Console.WriteLine("");
        Console.WriteLine("Press any key to go to main menu");
        Console.ReadKey();
        ShowMenu();
    }


}
