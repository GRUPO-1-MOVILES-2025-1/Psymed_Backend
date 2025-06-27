using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using psymed_platform.IAM.Appilcation.Internal.CommandServices;
using psymed_platform.IAM.Domain.Model.Repositories;
using psymed_platform.IAM.Infrastructure.Repositories;
using psymed_platform.Profiles.Application.Internal.CommandServices;
using psymed_platform.Profiles.Application.Internal.QueryServices;
using psymed_platform.Profiles.Domain.Repositories;
using psymed_platform.Profiles.Domain.Services;
using psymed_platform.Profiles.Infrastructure.Persistence.EFC.Repositories;
using psymed_platform.Shared.Domain.Repositories;
using psymed_platform.Shared.Infrastructure.Persistence.EFC.Configuration;
using psymed_platform.Shared.Infrastructure.Persistence.EFC.Repositories;
using psymed_platform.Tasks.Application.Internal.CommandServices;
using psymed_platform.Tasks.Application.Internal.QueryServices;
using psymed_platform.Tasks.Domain.Model.Repositories;
using psymed_platform.Tasks.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configurar la conexión con MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21))));

// Configuración JWT
var jwtSecret = builder.Configuration["Jwt:Secret"];
var key = Encoding.ASCII.GetBytes(jwtSecret);
if (string.IsNullOrEmpty(jwtSecret))
{
    throw new Exception("Jwt:Secret no está configurado.");
}

if (string.IsNullOrEmpty(connectionString))
{
    throw new Exception("Cadena de conexión no configurada.");
}

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// ✅ HABILITAR CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()    // Puedes restringir luego por seguridad
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Agregar controladores y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Agrega los servicios de tareas
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<TaskCommandService>();
builder.Services.AddScoped<TaskQueryService>();

// Registrar UnitOfWork y repositorios
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();

// Registrar servicios de aplicación
builder.Services.AddScoped<IProfileCommandService, ProfileCommandService>();

// Registrar servicios de IAM
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<AuthCommandService>(provider => 
    new AuthCommandService(
        provider.GetRequiredService<IUserRepository>(),
        jwtSecret
    ));

// Registrar Profile
builder.Services.AddScoped<IProfileQueryService, ProfileQueryService>();

var app = builder.Build();

Console.WriteLine($"Cadena de conexión usada: {connectionString}");

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(); // 👈 Esto aplica la política CORS que definiste

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

