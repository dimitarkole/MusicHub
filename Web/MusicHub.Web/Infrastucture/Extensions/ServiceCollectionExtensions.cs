namespace MusicHub.Web.Infrastucture.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Data;
    using MusicHub.Data.Common;
    using MusicHub.Data.Common.Repositories;
    using MusicHub.Data.Models;
    using MusicHub.Data.Repositories;
    using MusicHub.Services;
    using MusicHub.Services.Messaging;
    using MusicHub.Web.Infrastucture.Configurations;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using MusicHub.Common.Settings;
    using MusicHub.Data;
    using MusicHub.Services.Data;
    using MusicHub.Services.Data.Interfaces;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
            => services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetDefaultConnectionString()));

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>(options =>
                {
                    //Remove password rules if we are in development environment
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredUniqueChars = 0;
                })
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            return services;
        }

        public static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services,
            ApplicationSettings appSetings,
            IConfiguration configuration)
        {
            var key = Encoding.ASCII.GetBytes(appSetings.Secret);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
           {
               options.RequireHttpsMetadata = false;
               options.SaveToken = true;
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(key),
                   ValidateIssuer = false,
                   ValidateAudience = false,
               };
           });

            var jwtSettingsSection = configuration.GetSection("ApplicationSettings");
            services.Configure<JwtSettings>(jwtSettingsSection);

            var jwtSettings = jwtSettingsSection.Get<JwtSettings>();

            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            return services;
        }

        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services)
            => services
                .AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>))
                .AddScoped(typeof(IRepository<>), typeof(EfRepository<>))
                .AddScoped<IDbQueryRunner, DbQueryRunner>()
                .AddTransient<ICategoryService, CategoryService>()
                .AddTransient<IActiveDirectoryService, ActiveDirectoryService>()
                .AddTransient<INotificationsService, NotificationsService>()
                .AddTransient<IProfileService, ProfileService>()
                .AddTransient<ICommentService, CommentService>()
                .AddTransient<IMusicService, MusicService>()
                .AddTransient<IFollowService, FollowService>()
                .AddTransient<IPaginationService, PaginationService>()
                .AddTransient<ICommentReactionService, CommentReactionService>()
                .AddTransient<ILicenseService, LicenseService>();
                //.AddTransient<IEmailSender, SendGridEmailSender>();
        //.AddTransient<IAzureStorageService, AzureStorageService>()

        public static IServiceCollection AddSwagger(this IServiceCollection services)
           => services.AddSwaggerGen(c =>
           {
               c.SwaggerDoc("v1", new OpenApiInfo { Title = "Song Me API", Version = "v1" });
               c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
               {
                   Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                   Name = "Authorization",
                   In = ParameterLocation.Header,
                   Type = SecuritySchemeType.ApiKey,
                   Scheme = "Bearer",
               });

               c.AddSecurityRequirement(new OpenApiSecurityRequirement()
               {
                   {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                   }
               });
           });
    }
}
