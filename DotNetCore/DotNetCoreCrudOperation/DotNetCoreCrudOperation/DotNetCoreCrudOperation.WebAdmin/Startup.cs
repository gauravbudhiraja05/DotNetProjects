using System;
using System.Security.Principal;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using PickfordsIntranet.Core.DomainServices;
using PickfordsIntranet.Core.DomainServices.FrontEnd;
using PickfordsIntranet.Core.Helper;
using PickfordsIntranet.Core.Repositories;
using PickfordsIntranet.Core.ThirdPartyServices;
using PickfordsIntranet.Repo;
using PickfordsIntranet.Services;
using PickfordsIntranet.Services.FrontEnd;
using PickfordsIntranet.WebAdmin.Utility;

namespace PickfordsIntranet.WebAdmin
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
                .AddJsonFile("commonSettings.json", optional: true)
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
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
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



            services.Configure<FormOptions>(x => {
                x.ValueLengthLimit = 2147483647;
                x.ValueCountLimit = 2147483647;
            });

            //register custom filter
            //services.AddScoped<AuthorizeActionExecution>();

            // Windows Authentication on IIS
            services.AddAuthentication(IISDefaults.AuthenticationScheme);

            services.Configure<IISOptions>(options => {
                //options.AuthenticationDescriptions holds a list of allowed authentication schemes
                options.AutomaticAuthentication = true;
                options.ForwardClientCertificate = true;
               // options.ForwardWindowsAuthentication = true;
            });

            //Configure Cookies 
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //         .AddCookie(options =>
            //         {
            //             options.LoginPath = "/FrontEndHome/Login/";
            //         });

            services.AddAuthentication("AdminCookies")
                     .AddCookie("AdminCookies",options =>
                     {
                         options.LoginPath = "/Account/Login/";
                     });


            //services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IRepository), typeof(Repository));

            



            if (_config["MailSettings:Provider"].ToUpper() == "SMTP")
            {
                //  Load in Smtp (MailKit/MimeKit) services for email services
                services.AddTransient<IMessage, Services.SmtpMessage>();
                services.AddTransient<Utility.SmtpMessage, Utility.SmtpMessage>();
            }

            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<IPathProvider, PathProvider>();
            services.AddScoped<IViewParser, ViewParser>();
            services.AddScoped<IFaqService, FaqService>();
            services.AddScoped<IVacancyService, VacancyService>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IFronEndService, FrontEndService>();
            services.AddScoped<IEndUserService, EndUserService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IRewardService, RewardService>();
            services.AddScoped<ILeaveManagementService, LeaveManagementService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            IMessage emailMessage)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddFile("Logs/Trace-{Date}.txt", LogLevel.Trace);
            loggerFactory.AddFile("Logs/Error-{Date}.txt", LogLevel.Error);

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                loggerFactory.AddDebug(LogLevel.Information);

            }
            else
            {
                app.UseExceptionHandler("/error");
                loggerFactory.AddDebug(LogLevel.Error);
            }

            //app.UseDefaultFiles();
            app.UseStaticFiles();
            

            app.UseFileServer(new FileServerOptions()
            {
                FileProvider = new PhysicalFileProvider(_config["FileStoragePath"]),
                RequestPath = new PathString(_config["FileRequestPath"])
            });

            app.UseAuthentication(); // The order of this Use method matters in relation to other pipeline middleware!!!
           
            app.UseMvc(config =>
            {
                config.MapRoute(
                   name: "Default",
                   template: "{controller}/{action}/{id?}",
                   defaults: new { controller = "Account", action = "Login" }
                   //defaults: new { controller = "FrontEndHome", action = "Login" }
                   );

            });

            // seedPickfordData.Initialize();
            
        }
    }

}
