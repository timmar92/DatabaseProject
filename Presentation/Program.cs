using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation.Services;

var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    services.AddDbContext<TaskDbContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\WIN23\c-sharp\DatabaseProject\Infrastructure\Data\TaskDb.mdf;Integrated Security=True;Connect Timeout=30"));
    services.AddScoped<AssignmentRepository>();
    services.AddScoped<CategoryRepository>();
    services.AddScoped<StatusRepository>();
    services.AddScoped<TaskRepository>();
    services.AddScoped<UserRepository>();
    services.AddScoped<UserService>();
    services.AddScoped<TaskService>();
    services.AddScoped<MenuService>();
    services.AddScoped<AssignmentService>();

}).Build();

builder.Start();
Console.Clear();

var menuService = builder.Services.GetService<MenuService>();

menuService!.ShowMenu();




