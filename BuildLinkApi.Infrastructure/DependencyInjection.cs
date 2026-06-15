using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildLinkApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BuildLinkApi.Application.Interfaces.Repositories;
using BuildLinkApi.Infrastructure.Repositories;
using AutoMapper;
using BuildLinkApi.Application.Interfaces.Services;
using BuildLinkApi.Infrastructure.Services;
using Amazon.SQS;
using Amazon;
namespace BuildLinkApi.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
         this IServiceCollection services,
         IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMapper, Mapper>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            // AWS SQS Registration
            var awsRegion = configuration["AWS:Region"] ?? "ap-southeast-1";
            var sqsConfig = new AmazonSQSConfig
            {
                RegionEndpoint = RegionEndpoint.GetBySystemName(awsRegion)
            };
            services.AddSingleton<IAmazonSQS>(new AmazonSQSClient(sqsConfig));
            services.AddHttpClient<ITurnstileService, TurnstileService>();
            services.AddScoped<IMessageQueueService, MessageQueueService>();

            return services;
        }
    }
}