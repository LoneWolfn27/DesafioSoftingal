global using DesafioSoftingal.Models;
global using DesafioSoftingal.Services.AddressService;
global using DesafioSoftingal.DTOS.Address;
global using AutoMapper;
global using Microsoft.EntityFrameworkCore;
global using DesafioSoftingal.Data;
global using Microsoft.AspNetCore.Mvc;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DbAddress>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(a =>
{
    a.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = """Standard Authorization header using the Bearer scheme. Example: "bearer {token}" """,
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    a.OperationFilter<SecurityRequirementsOperationFilter>();
}
);

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                    .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddHttpContextAccessor();

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
