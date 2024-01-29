using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services;

public class AssignmentService(AssignmentRepository assignmentRepository, CategoryRepository categoryRepository, StatusRepository statusRepository, TaskRepository taskRepository, UserRepository userRepository)
{
    private readonly AssignmentRepository _assignmentRepository = assignmentRepository;
    private readonly CategoryRepository _categoryRepository = categoryRepository;
    private readonly StatusRepository _statusRepository = statusRepository;
    private readonly TaskRepository _taskRepository = taskRepository;
    private readonly UserRepository _userRepository = userRepository;


    public IEnumerable<AssignmentEntity> GetAssignmentsByUserId(int userId)
    {
        try
        {
            var assignments = _assignmentRepository.GetAll(x => x.UserId == userId);
            return assignments;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return null!;
    }

    public IEnumerable<AssignmentEntity> GetAllAssignments()
    {

        try
        {
            var assignments = _assignmentRepository.GetAll();
            return assignments;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return null!;
    }
}
