using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Backend.Data;
using Backend.Repositories;
using System.Text;
using Backend;
using Backend.Services;

var builder = WebApplication.CreateBuilder(args);

//Add token verification.
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
var jwtsettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
var key = Encoding.ASCII.GetBytes(jwtsettings.Key);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = jwtsettings.Issuer,
        ValidAudience = jwtsettings.Audience,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

//Add cors restrictions
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") // <-- Your frontend URL here
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUSerService, UserService>();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=moneyApp.db"));

//Add controllers
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.Run();
