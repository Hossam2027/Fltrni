using Fltrni.API.Middleware;
using Fltrni.core.Interface;
using Fltrni.infrastructure.Data;
using Fltrni.infrastructure.Repository;
using Fltrni.infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//add Cors angular

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DBContetxt
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CS") ));
// Add Identity
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();
// Add UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
// Add ServiceProviderService
builder.Services.AddScoped<IServiceProviderService, ServiceProviderService>();
// Add AuthService
builder.Services.AddScoped<IAuthService, AuthService>();
// Add Token
builder.Services.AddScoped<IToken, Token>();
// Add CategoryService
builder.Services.AddScoped<ICategoryService, CategoryService>();
// Add ReviewService
builder.Services.AddScoped<IReviewService, ReviewService>();
//add swagger authentication
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "CRUD API",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT Token only"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
// JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,

                ValidIssuer =
                    builder.Configuration["Jwt:Issuer"],

                ValidateAudience = true,

                ValidAudience =
                    builder.Configuration["Jwt:Audience"],

                ValidateLifetime = true,

                ValidateIssuerSigningKey = true,

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            builder.Configuration["Jwt:Key"]
                            ?? throw new InvalidOperationException(
                                "JWT Key is missing"
                            )
                        )
                    ),

                ClockSkew = TimeSpan.Zero
            };
});


//add cosr for runasp

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAngular", policy =>
//    {
//        policy
//            .WithOrigins("https://faltrni.runasp.net")
//            .AllowAnyHeader()
//            .AllowAnyMethod();
//    });
//});

// add cosr for localhost

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();
// Add GlobalExceptionMiddleware
//app.UseMiddleware<GlobalExceptionMiddleware>();
// use Cors



app.UseCors("AllowAngular");

// Roles seeding
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider
        .GetRequiredService<RoleManager<IdentityRole>>();

    string[] roles = { "Admin", "Provider", "Customer" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}
// Seed admin user


    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
