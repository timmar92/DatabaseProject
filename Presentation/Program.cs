using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Infrastructure.Repositories.ProductRepositories;
using Infrastructure.Services;
using Infrastructure.Services.ProductServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation.Services;

var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    services.AddDbContext<TaskDbContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\WIN23\c-sharp\DatabaseProject\Infrastructure\Data\TaskDb.mdf;Integrated Security=True;Connect Timeout=30"));
    services.AddScoped<AssignmentRepository>();
    services.AddScoped<Infrastructure.Repositories.CategoryRepository>();
    services.AddScoped<StatusRepository>();
    services.AddScoped<TaskRepository>();
    services.AddScoped<UserRepository>();
    services.AddScoped<UserService>();
    services.AddScoped<TaskService>();
    services.AddScoped<MenuService>();
    services.AddScoped<AssignmentService>();


    //productcatalog

    services.AddDbContext<ProductDbContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\WIN23\c-sharp\DatabaseProject\Infrastructure\Data\ProductCatalogDb.mdf;Integrated Security=True"));
    services.AddScoped<Infrastructure.Repositories.ProductRepositories.CategoryRepository>();
    services.AddScoped<CustomerRepository>();
    services.AddScoped<ManufacturerRepository>();
    services.AddScoped<ProductRepository>();
    services.AddScoped<PurchaseRepository>();
    services.AddScoped<ProductService>();
    services.AddScoped<CustomerService>();
    services.AddScoped<PurchaseService>();
    services.AddScoped<ManufacturerService>();
    services.AddScoped<CategoryService>();


}).Build();

builder.Start();
Console.Clear();

var menuService = builder.Services.GetService<MenuService>();

menuService!.NavigationMenu();




