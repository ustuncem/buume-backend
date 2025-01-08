using BUUME.Application.Abstractions.Caching;
using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Files;
using BUUME.Application.Abstractions.SMS;
using BUUME.Domain.BusinessCategories;
using BUUME.Domain.CampaignTypes;
using BUUME.Domain.Cities;
using BUUME.Domain.Countries;
using BUUME.Domain.Districts;
using BUUME.Domain.Files;
using BUUME.Domain.Regions;
using BUUME.Domain.TaxOffices;
using BUUME.Domain.Users;
using BUUME.Infrastructure.Caching;
using BUUME.Infrastructure.Files;
using BUUME.Infrastructure.Outbox;
using BUUME.Infrastructure.Repositories;
using BUUME.Infrastructure.SMS;
using BUUME.Infrastructure.Time;
using BUUME.SharedKernel;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Npgsql;
using Quartz;
using FileOptions = BUUME.Infrastructure.Files.FileOptions;

namespace BUUME.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddServices(configuration)
            .AddDatabase(configuration)
            .AddBackgroundJobs(configuration)
            .AddCaching(configuration)
            .AddHealthChecks(configuration);
    }
    
    private static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SmsOptions>(configuration.GetSection("SmsSettings"));
        services.Configure<FileOptions>(configuration.GetSection("FileUploadSettings"));
        
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddHttpClient<ISmsService, SmsService>((serviceProvider, httpClient) =>
        {
            SmsOptions smsOptions = serviceProvider.GetRequiredService<IOptions<SmsOptions>>().Value;
            httpClient.BaseAddress = new Uri(smsOptions.Url);
        });
        services.AddScoped<IFileUploader, FileUploader>();

        return services;
    }
    
    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

        string? connectionString = configuration.GetConnectionString("Database");
        Ensure.NotNullOrEmpty(connectionString);

        services.AddSingleton<IDbConnectionFactory>(_ =>
            new DbConnectionFactory(new NpgsqlDataSourceBuilder(connectionString).Build()));

        services.AddDbContext<ApplicationDbContext>(
            options => options
                .UseNpgsql(connectionString, npgsqlOptions =>
                    npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Default))
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ITaxOfficeRepository, TaxOfficeRepository>();
        services.AddScoped<IBusinessCategoryRepository, BusinessCategoryRepository>();
        services.AddScoped<ICountryRepository, CountryRepository>();
        services.AddScoped<ICampaignTypeRepository, CampaignTypeRepository>();
        services.AddScoped<IRegionRepository, RegionRepository>();
        services.AddScoped<ICityRepository, CityRepository>();
        services.AddScoped<IDistrictRepository, DistrictRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IFileRepository, FileRepository>();

        return services;
    }
    
    private static IServiceCollection AddCaching(this IServiceCollection services, IConfiguration configuration)
    {
        string redisConnectionString = configuration.GetConnectionString("Cache")!;

        services.AddStackExchangeRedisCache(options => options.Configuration = redisConnectionString);

        services.AddSingleton<ICacheService, CacheService>();

        return services;
    }
    
    private static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionString("Database")!)
            .AddRedis(configuration.GetConnectionString("Cache")!);

        return services;
    }
    
    private static IServiceCollection AddBackgroundJobs(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<OutboxOptions>(configuration.GetSection("Outbox"));
        services.AddQuartz();
        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
        services.ConfigureOptions<ProcessOutboxMessagesJobSetup>();
        
        return services;
    }
}