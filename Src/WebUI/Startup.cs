using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentValidation.AspNetCore;
using Patronage2020.Infrastructure;
using Patronage2020.Persistence;
using Patronage2020.Application;
using Patronage2020.Application.Common.Interfaces;
using Patronage2020.WebUI.Common;
using Patronage2020.WebUI.Services;
using Serilog;
using System.IO;
using Patronage2020.Common;
using AutoMapper;

namespace Patronage2020.WebUI
{
    public class Startup
    {
        private IServiceCollection _services;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Environment = environment;

            var builder = new ConfigurationBuilder()
            .SetBasePath(Environment.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; set; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructure(Configuration, Environment);
            services.AddPersistence(Configuration);
            services.AddApplication();

            services.AddHealthChecks()
                .AddDbContextCheck<Patronage2020DbContext>();

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();

            services
                .AddControllersWithViews()
                .AddNewtonsoftJson()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<IPatronage2020DbContext>());

            // Customise default API behaviour
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });


            services.AddOpenApiDocument(configure =>
            {
                configure.Title = "Patronage2020 API";
            });

            services.AddOptions();

            services.Configure<WritingFilesConfig>(Configuration.GetSection("WritingFiles"));
            services.Configure<LoggingConfig>(Configuration.GetSection("Logging"));

            var dirName = Configuration.GetValue<string>("Logging:DirectoryName");
            var fileName = Configuration.GetValue<string>("Logging:FileName");
            var loggingPath = Path.Combine(dirName, fileName);

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(loggingPath, shared:true, outputTemplate: "{Timestamp:yyyy:MM:dd HH:mm:ss} {Message}{NewLine}")
                .CreateLogger();

            services.AddAutoMapper(typeof(Startup));

            _services = services;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment() || Environment.IsDocker())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();

                app.UseOpenApi();
                app.UseSwaggerUi3();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCustomExceptionHandler();
            app.UseHealthChecks("/health");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "api/{controller}/{action=Index}/{id?}");
                endpoints.MapControllers();
            });
        }
    }
}
