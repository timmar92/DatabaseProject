using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services;

public class TaskService(AssignmentRepository assignmentRepository, CategoryRepository categoryRepository, StatusRepository statusRepository, TaskRepository taskRepository, UserRepository userRepository)
{
    private readonly AssignmentRepository _assignmentRepository = assignmentRepository;
    private readonly CategoryRepository _categoryRepository = categoryRepository;
    private readonly StatusRepository _statusRepository = statusRepository;
    private readonly TaskRepository _taskRepository = taskRepository;
    private readonly UserRepository _userRepository = userRepository;

    public bool CreateTask(TaskEntity task, StatusEntity status, CategoryEntity category, int userId)
    {
        try
        {
            var statusEntity = _statusRepository.GetOne(x => x.StatusName == status.StatusName);
            if (statusEntity == null)
            {
                statusEntity = _statusRepository.Create(status);
            }

            var existingCategory = _categoryRepository.GetOne(x => x.CategoryId == category.CategoryId);
            if (existingCategory == null)
            {
                existingCategory = _categoryRepository.Create(category);
            }

            task.StatusId = statusEntity.StatusId;
            task.CategoryId = existingCategory.CategoryId;

            var taskEntity = _taskRepository.Create(task);
            if (taskEntity != null)
            {
                var assignment = new AssignmentEntity
                {
                    TaskId = taskEntity.TaskId,
                    UserId = userId
                };

                var assignmentEntity = _assignmentRepository.Create(assignment);
                if (assignmentEntity != null)
                {

                    return true;
                }
            }

        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return false;
    }

    public TaskEntity GetTaskByName(string task)
    {
        try
        {
            var result = _taskRepository.GetOne(x => x.TaskName == task);
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return null!;

    }

    public bool UpdateTask(TaskEntity task)
    {
        try
        {
            var existingTask = _taskRepository.GetOne(x => x.TaskId == task.TaskId);
            if (existingTask != null)
            {
                existingTask.TaskName = task.TaskName;
                existingTask.Description = task.Description;
                existingTask.StatusId = task.StatusId;
                existingTask.Category.CategoryName = task.Category.CategoryName;
                existingTask.Status.StatusName = task.Status.StatusName;
                _taskRepository.Update(existingTask);
                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return false;
    }


    public bool DeleteTask(string taskName)
    {
        try
        {
            var existingTask = GetTaskByName(taskName);
            if (existingTask != null)
            {
                _taskRepository.Delete(task => task.TaskName == taskName);
                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return false;
    }




}
