using System.Net;
using System.Text;
using System.Text.Json;
using BUUME.Application.Abstractions.Authentication;
using BUUME.Identity.Authentication;
using BUUME.Identity.Data;
using BUUME.Identity.Jwt;
using BUUME.SharedKernel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace BUUME.Identity;

public static class DependencyInjection
{
    public static IServiceCollection AddBuumeIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddServices(configuration)
            .AddIdentityProvider(configuration);
    }
    
    private static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection("JwtSettings"));
        
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        
        services.AddHttpContextAccessor();

        services.AddScoped<IUserContext, UserContext>();
        
        return services;
    }
    
    private static IServiceCollection AddIdentityProvider(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("IdentityDatabase");
        
        services.AddDbContext<IdentityProviderDbContext>(
            options => options
                .UseNpgsql(connectionString, npgsqlOptions =>
                    npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Default))
                .UseSnakeCaseNamingConvention());

        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Sign in
                options.SignIn.RequireConfirmedAccount = true;
                options.SignIn.RequireConfirmedPhoneNumber = true;
                options.SignIn.RequireConfirmedEmail = false;

                // User
                options.User.RequireUniqueEmail = false;
                options.User.AllowedUserNameCharacters = "abcçdefgðhijklmnoöpqrsþtuüvwxyzABCÇDEFGÐHIÝJKLMNOÖPQRSÞTUÜVWXYZ0123456789._";

                // Lockout
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(365);
                options.Lockout.AllowedForNewUsers = true;

                //Password
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 10;
            })
            .AddEntityFrameworkStores<IdentityProviderDbContext>()
            .AddDefaultTokenProviders();
        
        var jwtOptions = configuration.GetSection("JwtSettings").Get<JwtOptions>();
        
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "LifetimeValidation";
                options.DefaultChallengeScheme = "LifetimeValidation"; 
                options.DefaultScheme = "LifetimeValidation";
            }).AddJwtBearer("LifetimeValidation", options =>
            {
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = async context =>
                    {
                        // Call this to skip the default logic and avoid using the default response
                        context.HandleResponse();
                        
                        var response = Result.Failure(Error.Failure(HttpStatusCode.Unauthorized.ToString(), "Giriş yapman gerekiyor!"));

                        string json = JsonSerializer.Serialize(response);
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsync(json);
                    }
                };

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = jwtOptions?.Issuer,
                    ValidAudience = jwtOptions?.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions?.Key ?? string.Empty)),
                };
            }).AddJwtBearer("NoLifetimeValidation", options =>
            {
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = async context =>
                    {
                        context.HandleResponse();

                        var response = Result.Failure(Error.Failure(HttpStatusCode.Unauthorized.ToString(), "Giriş yapman gerekiyor!"));

                        string json = JsonSerializer.Serialize(response);
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsync(json);
                    }
                };

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = jwtOptions?.Issuer,
                    ValidAudience = jwtOptions?.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions?.Key ?? string.Empty))
                };
            });

        services.AddAuthorization(options =>
            {
                options.AddPolicy("ConcreteJWT", policy => policy.RequireClaim("IsTemp", "false"));
                options.AddPolicy("TempJWT", policy => policy.RequireClaim("IsTemp", "true"));
                options.AddPolicy("ResetPassword", policy => policy.RequireClaim("ForgetPassword", "true"));
                options.AddPolicy("IsAValidCitizen", policy => policy.RequireClaim("IsAValidCitizen", "true"));
            });

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<IdentityProviderDbContext>());
        return services;
    }
}