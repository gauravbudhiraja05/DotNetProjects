using HiveReport.WebAdmin.Account.Af;
using HiveReport.WebAdmin.Account.Repository;
using HiveReport.WebAdmin.Account.Service;
using HiveReport.WebAdmin.Common.Af;
using HiveReport.WebAdmin.Common.Repository;
using HiveReport.WebAdmin.Common.Service;
using HiveReport.WebAdmin.Configuration;
using HiveReport.WebAdmin.Dashboard.Af;
using HiveReport.WebAdmin.Dashboard.Repository;
using HiveReport.WebAdmin.Dashboard.Service;
using HiveReport.WebAdmin.Infrastructure.Sql;
using HiveReport.WebAdmin.Infrastructure.ToolsLog;
using HiveReport.WebAdmin.User.Af;
using HiveReport.WebAdmin.User.Repository;
using HiveReport.WebAdmin.User.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace HiveReport.WebAdmin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ConnectionString>(Configuration.GetSection("ConnectionStrings"));

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });


            services.AddAuthentication("ReportUserCookies")
                     .AddCookie("ReportUserCookies", options =>
                     {
                         options.LoginPath = "/Home/Index/";
                     });

            services.AddHttpContextAccessor();

            //Logger
            services.AddSingleton<ILogger, LoggerFake>();

            services.AddControllersWithViews();

            services.AddHttpContextAccessor();

            services.AddScoped<ISqlServerHelper, SqlServerHelper>();

            services.AddScoped<IAccountAf, AccountAf>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAccountDao, AccountDao>();

            services.AddScoped<IUserAf, UserAf>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserDao, UserDao>();

            services.AddScoped<IDashboardAf, DashboardAf>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IDashboardDao, DashboardDao>();

            services.AddScoped<ISharedLayoutAf, SharedLayoutAf>();
            services.AddScoped<ISharedLayoutService, SharedLayoutService>();
            services.AddScoped<ISharedLayoutDao, SharedLayoutDao>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
