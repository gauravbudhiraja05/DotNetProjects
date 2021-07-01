using DoseBookAdmin.Core.DomainServices;
using DoseBookAdmin.Core.Repositories;
using DoseBookAdmin.Repo;
using DoseBookAdmin.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace DoseBookAdmin.WebAdmin
{
    public class Startup
    {
        private IHostingEnvironment _env;
        private IConfigurationRoot _config;

        public Startup(IHostingEnvironment env)
        {
            _env = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(_env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                //.AddJsonFile("commonSettings.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets("aspnet-Pickfords.Web-716a052e-4235-4e5f-93bf-ba57ba6934b9");

            }

            _config = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_config);


            if (_env.IsDevelopment())
            {
                //services.AddScoped<IMailService, DebugMailService>();
            }

            services.AddLogging();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMvc(config =>
            {
                if (_env.IsProduction())
                {
                    //config.Filters.Add(new RequireHttpsAttribute());
                }
            });


            // Windows Authentication on IIS
            services.AddAuthentication(IISDefaults.AuthenticationScheme);


            services.AddAuthentication("AdminCookies")
                     .AddCookie("AdminCookies", options =>
                     {
                         options.LoginPath = "/Account/Login/";
                     });

            services.AddHttpContextAccessor();
            services.AddScoped(typeof(IRepository), typeof(Repository));
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMedicineDoseRepository, MedicineDoseRepository>();
            services.AddScoped<IDiagnosticTestRepository, DiagnosticTestRepository>();
            services.AddScoped<IMiscSuggestionRepository, MiscSuggestionRepository>();
            services.AddScoped<IDoseDataRepository, DoseDataRepository>();
            services.AddScoped<IClassificationDataRepository, ClassificationDataRepository>();
            services.AddScoped<IClassificationResultRepository, ClassificationResultRepository>();
            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMedicineDoseService, MedicineDoseService>();
            services.AddScoped<IDiagnosticTestService, DiagnosticTestService>();
            services.AddScoped<IMiscSuggestionService, MiscSuggestionService>();
            services.AddScoped<IDoseDataService, DoseDataService>();
            services.AddScoped<IClassificationDataService, ClassificationDataService>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IClassificationResultService, ClassificationResultService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                loggerFactory.AddDebug(LogLevel.Error);
            }

            app.UseStaticFiles();

            app.UseAuthentication(); // The order of this Use method matters in relation to other pipeline middleware!!!

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
