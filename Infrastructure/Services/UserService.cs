using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services;

public class UserService(AssignmentRepository assignmentRepository, CategoryRepository categoryRepository, StatusRepository statusRepository, TaskRepository taskRepository, UserRepository userRepository)
{
    private readonly AssignmentRepository _assignmentRepository = assignmentRepository;
    private readonly CategoryRepository _categoryRepository = categoryRepository;
    private readonly StatusRepository _statusRepository = statusRepository;
    private readonly TaskRepository _taskRepository = taskRepository;
    private readonly UserRepository _userRepository = userRepository;

    public bool CreateUser(UserEntity entity)
    {
        try
        {
            if (!_userRepository.Exists(x => x.Email == entity.Email))
            {
                _userRepository.Create(entity);
                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return false;

    }

    public bool AreThereAnyUsers()
    {
        var users = _userRepository.GetAll();
        return users.Any();

    }

    public UserEntity GetUserByEmail(string email)
    {
        try
        {
            var user = _userRepository.GetOne(x => x.Email == email);
            return user;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return null!;
    }

    public int GetIdByEmail(string email)
    {
        try
        {
            var user = _userRepository.GetOne(x => x.Email == email);
            return user.UserId;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return 0;
    }

    public UserEntity GetUserById(int id)
    {
        try
        {
            var user = _userRepository.GetOne(x => x.UserId == id);
            return user;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return null!;
    }

    public IEnumerable<UserEntity> GetAllUsers()
    {
        try
        {
            var result = _userRepository.GetAll();
            if (result != null)
            {
                return result;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;

    }

    public bool UpdateUser(UserEntity user)
    {
        try
        {
            var existingUser = _userRepository.GetOne(x => x.UserId == user.UserId);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                _userRepository.Update(existingUser);
                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return false;
    }

    public bool DeleteUser(string email)
    {
        try
        {
            var existingUser = GetUserByEmail(email);
            if (existingUser != null)
            {
                var userAssignments = _assignmentRepository.GetAll(a => a.UserId == existingUser.UserId);
                foreach (var assignment in userAssignments)
                {
                    _taskRepository.Delete(task => task.TaskId == assignment.TaskId);
                    _assignmentRepository.Delete(a => a.AssignmentId == assignment.AssignmentId);
                }
                _userRepository.Delete(user => user.UserId == existingUser.UserId);
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
