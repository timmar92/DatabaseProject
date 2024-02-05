using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories.Tasks;

public class UserRepository_Tests
{
    private readonly TaskDbContext _context = new(new DbContextOptionsBuilder<TaskDbContext>()
               .UseInMemoryDatabase($"{Guid.NewGuid()}")
               .Options);


    [Fact]
    public void CreateUser_Should_CreateUser_then_ReturnUser()
    {
        //arrange
        var userRepo = new UserRepository(_context);
        var userEntity = new UserEntity
        {
            Name = "Test User",
            Email = "test@domain.com"
        };

        //act
        var result = userRepo.Create(userEntity);

        //assert
        Assert.NotNull(result);
        Assert.NotEqual(0, result.UserId);
        Assert.Equal(userEntity.Name, result.Name);
    }

    [Fact]
    public void CreateUser_ShouldNot_CreateUser_then_ReturnNull()
    {
        //arrange
        var userRepo = new UserRepository(_context);
        var userEntity = new UserEntity();

        //act
        var result = userRepo.Create(userEntity);

        //assert
        Assert.Null(result);
    }

    [Fact]
    public void GetAllUsers_Should_GetAllUsers_AsList_Then_ReturnList()
    {
        //arrange
        var userRepo = new UserRepository(_context);

        //act
        var result = userRepo.GetAll();

        //assert
        Assert.NotNull(result);
        Assert.IsType<List<UserEntity>>(result);
    }

    [Fact]
    public void GetAllUsers_Should_GetAllUsers_WithPredicate_AsList_Then_ReturnList()
    {
        //arrange
        var userRepo = new UserRepository(_context);

        //act
        var result = userRepo.GetAll(u => u.Name == "Test User");

        //assert
        Assert.NotNull(result);
        Assert.IsType<List<UserEntity>>(result);
    }

    [Fact]
    public void GetOneUser_Should_GetOneUser_WithPredicate_Then_ReturnUser()
    {
        //arrange
        var userRepo = new UserRepository(_context);
        var userEntity = new UserEntity
        {
            Name = "Test User",
            Email = "test@domain.com"
        };
        userRepo.Create(userEntity);

        //act
        var result = userRepo.GetOne(u => u.Name == "Test User");

        //assert
        Assert.NotNull(result);
        Assert.IsType<UserEntity>(result);
    }

    [Fact]
    public void GetOneUser_ShouldNot_GetOneUser_WithPredicate_Then_ReturnNull()
    {
        //arrange
        var userRepo = new UserRepository(_context);

        //act
        var result = userRepo.GetOne(u => u.Name == "Test User 2");

        //assert
        Assert.Null(result);
    }

    [Fact]
    public void UpdateUser_Should_UpdateUser_Then_ReturnUser()
    {
        //arrange
        var userRepo = new UserRepository(_context);
        var userEntity = new UserEntity
        {
            Name = "Test User",
            Email = "test@domain.com"
        };
        userEntity = userRepo.Create(userEntity);

        //act
        userEntity.Name = "Updated User";
        userEntity.Email = "updated@domain.com";
        var result = userRepo.Update(userEntity);

        //assert
        Assert.NotNull(result);
        Assert.Equal(userEntity.Name, result.Name);
        Assert.Equal(userEntity.Email, result.Email);


    }

    [Fact]
    public void DeleteUser_Should_DeleteUserByEmail_Then_ReturnTrue()
    {
        //arrange
        var userRepo = new UserRepository(_context);
        var userEntity = new UserEntity
        {
            Name = "Test User",
            Email = "test@domain.com"
        };
        var createdUser = userRepo.Create(userEntity);

        //act
        var result = userRepo.Delete(x => x.Email == createdUser.Email);

        //assert
        Assert.True(result);
    }

    [Fact]
    public void DeleteUser_ShouldNot_DeleteUserByEmail_Then_ReturnFalse()
    {
        //arrange
        var userRepo = new UserRepository(_context);
        var userEntity = new UserEntity
        {
            Name = "Test User",
            Email = "test@domain.com"
        };
        var createdUser = userRepo.Create(userEntity);

        //act
        var result = userRepo.Delete(x => x.Email == "");

        //assert
        Assert.False(result);
    }
}
