using api_task.DataAccess;
using api_task.Interface;
using api_task.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Adding project dependencies...
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddDbContextPool<AppDbContext>(options =>
    {
        // change Connection String from appsettings.json file
        // I used connection string for my local sql server here!
        options.UseSqlServer(builder.Configuration.GetConnectionString("UserDb"));
    }
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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