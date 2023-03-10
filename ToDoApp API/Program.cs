using Microsoft.EntityFrameworkCore;
using ToDoApp.API.Auth;
using ToDoApp_API.Auth;
using ToDoApp_API.Db;
using ToDoApp_API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IAuthService, AuthService>();

AuthConfigurator.Configure(builder);

builder.Services.AddDbContext<AppDbContext>(c =>
    c.UseSqlServer(builder.Configuration["ToDoAppDb"]));

builder.Services.AddTransient<ISendEmailRequestRepository, SendEmailRequestRepository>();


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();



var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())      
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();  // changes Http requests to Https

app.UseAuthorization();  // important if we use Authorization

app.MapControllers();   // something that's just need to be here, makes the app work faster

app.Run();  // to run the application (you can configure it but we are doing it in appsettingjson)
