namespace MusicHub.Web
{
    using System.IO;
    using System.Reflection;
    using System.Text;
    using MusicHub.Data;
    using MusicHub.Data.Common;
    using MusicHub.Data.Common.Repositories;
    using MusicHub.Data.Models;
    using MusicHub.Data.Repositories;
    using MusicHub.Data.Seeding;

    using MusicHub.Services.Mapping;
    using MusicHub.Services.Messaging;
    using MusicHub.Web.Infrastucture.Configurations;
    using MusicHub.Web.Infrastucture.Filters;
    using MusicHub.Web.ViewModels;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.FileProviders;
    using Microsoft.Extensions.Hosting;
    using Microsoft.AspNetCore.Mvc.Formatters.Json;

    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SpaServices.AngularCli;
    using MusicHub.Web.Infrastucture.Extensions;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.ApplicationInsights;
    using System;

    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            this.Configuration = configuration;

        public IConfiguration Configuration { get; protected set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddApplicationInsightsTelemetry();

            services.AddLogging(options =>
            {
                // hook the Console Log Provider
                options.AddConsole();
                options.SetMinimumLevel(LogLevel.Trace);

                // hook the Application Insights Provider
                options.AddFilter<ApplicationInsightsLoggerProvider>("", LogLevel.Trace);

                // pass the InstrumentationKey provided under the appsettings
                options.AddApplicationInsights(this.Configuration["ApplicationInsights:InstrumentationKey"]);
            });

            services
                .AddDatabase(this.Configuration)
                .AddIdentity()
                .AddJwtAuthentication(services.GetAppSettings(this.Configuration), this.Configuration)
                .AddApplicationServices()
                .ConfigureAzureBlobStorage()
                .AddApplicationSettings((Microsoft.Extensions.Configuration.IConfiguration)this.Configuration)
                .ConfigureHangfire(this.Configuration)
                .AddCors(options => options.AddPolicy("AllowWebApp", builder => builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins("http://localhost:4200", "http://localhost:5000", "https://localhost:4200", "https://localhost:5000")))
                .AddSwagger()
                .AddControllers();

            services.AddMvc(options =>
            {
                options.Filters.Add<ValidateModelStateActionFilter>();
                options.Filters.Add<ExceptionFilter>();
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Uncomment the line below if you want to seed data in your database
            // app.SeedData();
            app.UseDeveloperExceptionPage();
            if (env.IsDevelopment())
            {
                app.UseDatabaseErrorPage();
            }

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"ClientApp/src/assets/resources")),
                RequestPath = new PathString("/ClientApp/src/assets/resources"),
            });

            app
                .UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Music Hub API V1");
                })
                .UseRouting()
                .UseCors("AllowWebApp")
                .UseCors(options => options
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins("http://localhost:4200"))
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                })
                .ApplyMigrations();

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
                spa.Options.StartupTimeout = new TimeSpan(0, 5, 0);
                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
