using Microsoft.EntityFrameworkCore;
using Umuna.Core.Contracts.DTOs.CameraPositions;
using Umuna.Core.Contracts.DTOs.User;
using Umuna.Core.Contracts.DTOs.UserSettings;
using Umuna.Server.Domain.Entities;
using Umuna.Server.Domain.Mappings;
using Umuna.Server.Infrastructure.Database;
using Umuna.Server.Infrastructure.Repositories;
using Umuna.Server.Infrastructure.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// For SQLite:
builder.Services.AddDbContext<UmunaDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICameraPositionRepository, CameraPositionRepository>();
builder.Services.AddScoped<IUserSettingsRepository, UserSettingsRepository>();

// Mappers
builder.Services.AddScoped<
    IEntityToDtoMapper<User, UserReadDto, UserCreateDto, UserUpdateDto>, 
    UserMapper>();

builder.Services.AddScoped<
    IEntityToDtoMapper<CameraPosition, CameraPositionReadDto, CameraPositionCreateDto, CameraPositionUpdateDto>, 
    CameraPositionMapper>();

builder.Services.AddScoped<
    IEntityToDtoMapper<UserSettings, SettingsReadDto, SettingsCreateDto, SettingsUpdateDto>, 
    UserSettingsMapper>();


// Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICameraPositionService, CameraPositionService>();
builder.Services.AddScoped<IUserSettingsService, UserSettingsService>();

// Add services to the container.

builder.Services.AddControllers();

//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
