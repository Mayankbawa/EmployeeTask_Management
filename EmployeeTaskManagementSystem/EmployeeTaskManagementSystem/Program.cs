using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.DataAccess.TaskManagementEntities;
using TaskManagementSystem.Interfaces;
using TaskManagementSystem.Services;
using TaskManagementSystem.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DbContext with SQL Server
builder.Services.AddDbContext<TaskManagementDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TaskManagementDB")));



builder.Services.AddTransient<IEmployeeRepo, EmployeeService>();
builder.Services.AddTransient<ITaskRepo, TaskService>();
builder.Services.AddTransient<INoteRepo, NotesService>();
builder.Services.AddTransient<IDocumentRepo, DocumentService>();
builder.Services.AddTransient<ITeamRepo, TeamService>();
builder.Services.AddTransient<FileUploadHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
