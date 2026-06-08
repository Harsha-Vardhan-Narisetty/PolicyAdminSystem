using Microsoft.EntityFrameworkCore;
using PolicyAdmin.Application.Authentication;
using PolicyAdmin.Application.Interfaces;
using PolicyAdmin.Application.Services;
using PolicyAdmin.Persistence.Contexts;
using PolicyAdmin.Persistence.Repositories;
using PolicyAdmin.API.Middleware;
using PolicyAdmin.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPolicyHolderRepository, PolicyHolderRepository>();

builder.Services.AddScoped<IPolicyHolderService, PolicyHolderService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddDbContext<PolicyAdminDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
