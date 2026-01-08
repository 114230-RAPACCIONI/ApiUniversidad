using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using ApiUniversidad.Interfaces;
using ApiUniversidad.Interfaces.Services;
using ApiUniversidad.Mappings;
using ApiUniversidad.Models;
using ApiUniversidad.Repositories;
using ApiUniversidad.Services.Alumno;
using ApiUniversidad.Services.Curso;
using ApiUniversidad.Services.Docente;
using ApiUniversidad.Services.Rol;
using ApiUniversidad.Services.Usuario;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers() 
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//-------------------------------------
//asi es originalmente, pero si metemos autenticacion hay que a�adir configuraciones 
//builder.Services.AddSwaggerGen();
//-------------------------------------
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "API Documentation", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Authentication with Bearer scheme",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// configure the connection string
builder.Services.AddDbContext<UniversidadContext>(option =>
{
    option.UseNpgsql(builder.Configuration.GetConnectionString("ConexionDB"));
});

// register services and repositories
builder.Services.AddScoped<IAlumnoRepository, AlumnoRepository>();
builder.Services.AddScoped<IAlumnoService, AlumnoService>();

builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleService, RolService>();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddScoped<IDocenteRepository, DocenteRepository>();
builder.Services.AddScoped<IDocenteService, DocenteService>();

builder.Services.AddScoped<ICursoRepository, CursoRepository>();
builder.Services.AddScoped<ICursoService, CursoService>();

// // configure AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

var key = builder.Configuration["JwtSettings:SecretKey"];

if (string.IsNullOrEmpty(key))
{
    throw new InvalidOperationException("JWT Secret Key no está configurada en appsettings.json");
}
//---------------------
//Esto tambi�n es por la autenticacion

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            ValidateIssuer = false,
            ValidateAudience = false,
            RoleClaimType = "NombreRol"
        };
    });

// Configurar autorización basada en roles
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("admin"));
    options.AddPolicy("AdminOrDocente", policy => policy.RequireRole("admin", "docente"));
    options.AddPolicy("AdminOrAlumno", policy => policy.RequireRole("admin", "alumno"));
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API Documentation V1");
        options.RoutePrefix = string.Empty;
    });
}

// maneja las fechas 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior",true);

app.UseHttpsRedirection();
app.UseCors();

//siempre primero El UseAuthentication y desp el Useauthorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();

