using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories.Tasks;

public class AssignmentRepository_Tests
{
    private readonly TaskDbContext _context = new(new DbContextOptionsBuilder<TaskDbContext>()
           .UseInMemoryDatabase($"{Guid.NewGuid()}")
           .Options);

    [Fact]
    public void CreateAssignment_Should_CreateAssignment_then_ReturnAssignment()
    {
        //arrange
        var assignmentRepo = new AssignmentRepository(_context);
        var assignmentEntity = new AssignmentEntity
        {
            UserId = 1,
            TaskId = 1
        };

        //act
        var result = assignmentRepo.Create(assignmentEntity);

        //assert
        Assert.NotNull(result);
        Assert.NotEqual(0, result.AssignmentId);
        Assert.Equal(assignmentEntity.UserId, result.UserId);
        Assert.Equal(assignmentEntity.TaskId, result.TaskId);
    }

    [Fact]
    public void CreateAssignment_ShouldNot_CreateAssignment_then_ReturnNull()
    {
        //arrange
        var assignmentRepo = new AssignmentRepository(_context);
        var assignmentEntity = new AssignmentEntity();

        //act
        var result = assignmentRepo.Create(assignmentEntity);

        //assert
        Assert.Null(result);
    }

    [Fact]
    public void GetAllAssignments_Should_GetAllAssignments_AsList_Then_ReturnList()
    {
        //arrange
        var assignmentRepo = new AssignmentRepository(_context);

        //act
        var result = assignmentRepo.GetAll();

        //assert
        Assert.NotNull(result);
        Assert.IsType<List<AssignmentEntity>>(result);
    }

    [Fact]
    public void UpdateAssignment_Should_UpdateAssignment_then_ReturnUpdatedAssignment()
    {
        //arrange
        var assignmentRepo = new AssignmentRepository(_context);
        var assignmentEntity = new AssignmentEntity
        {
            UserId = 1,
            TaskId = 1
        };
        var result = assignmentRepo.Create(assignmentEntity);
        result.UserId = 2;
        result.TaskId = 2;

        //act
        var updatedResult = assignmentRepo.Update(result);

        //assert
        Assert.NotNull(updatedResult);
        Assert.Equal(result.UserId, updatedResult.UserId);
        Assert.Equal(result.TaskId, updatedResult.TaskId);
    }

    [Fact]
    public void DeleteAssignment_Should_DeleteAssignment_then_ReturnTrue()
    {
        //arrange
        var assignmentRepo = new AssignmentRepository(_context);
        var assignmentEntity = new AssignmentEntity
        {
            UserId = 1,
            TaskId = 1
        };
        assignmentEntity = assignmentRepo.Create(assignmentEntity);

        //act
        var result = assignmentRepo.Delete(x => x.AssignmentId == assignmentEntity.AssignmentId);

        //assert
        Assert.True(result);

    }

    [Fact]
    public void DeleteAssignment_ShouldNot_DeleteAssignment_then_ReturnFalse()
    {
        //arrange
        var assignmentRepo = new AssignmentRepository(_context);
        var assignmentEntity = new AssignmentEntity
        {
            UserId = 1,
            TaskId = 1
        };

        //act
        var result = assignmentRepo.Delete(x => x.AssignmentId == assignmentEntity.AssignmentId);

        //assert
        Assert.False(result);
    }


}
