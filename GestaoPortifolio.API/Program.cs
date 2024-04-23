using GestaoPortfolio.Application.Background;
using GestaoPortfolio.Infra.Context;
using GestaoPortfolio.Infra.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace GestaoPortifolio.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "dev";

            IConfiguration configuration = builder.Configuration
                .AddJsonFile("appsettings.json", false)
                .AddJsonFile($"appsettings.{env}.json", false)
                .Build();
                        
            // Add services to the container.
            builder.Services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
            });
            builder.Services.AddHttpContextAccessor();
            builder.Services.RegisterDb(configuration);
            builder.Services.RegisterFacades();
            builder.Services.RegisterServices();
            builder.Services.RegisterKafka(configuration);
            builder.Services.AddHostedService<OrdemCompraHostedService>();
            builder.Services.AddHostedService<OrdemVendaHostedService>();
            builder.Services.AddHostedService<EmailVencimentoHostedService>();

            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Lockout = new()
                {
                    AllowedForNewUsers = true,
                    DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10),
                    MaxFailedAccessAttempts = 3
                };

                options.User = new()
                {
                    RequireUniqueEmail = true,
                };

                options.SignIn = new SignInOptions()
                {
                    RequireConfirmedAccount = false,
                    RequireConfirmedEmail = false,
                    RequireConfirmedPhoneNumber = false
                };

                options.Password = new()
                {
                    RequireUppercase = false,
                    RequireDigit = false,
                    RequireLowercase = false,
                    RequireNonAlphanumeric = false,
                    RequiredUniqueChars = 0,
                    RequiredLength = 1
                };
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            // Adding Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            // Adding Jwt Bearer
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(config =>
            {
                config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                config.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (env == "dev")
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}