using DoseBookAdmin.WebAdmin.Advice.Af;
using DoseBookAdmin.WebAdmin.Advice.Service;
using DoseBookAdmin.WebAdmin.Common.Repository;
using DoseBookAdmin.WebAdmin.Doctor.Af;
using DoseBookAdmin.WebAdmin.Doctor.Service;
using DoseBookAdmin.WebAdmin.MedicineDose.Af;
using DoseBookAdmin.WebAdmin.MedicineDose.Service;
using DoseBookAdmin.WebAdmin.PrescriptionMeta.Af;
using DoseBookAdmin.WebAdmin.PrescriptionMeta.Service;
using DoseBookAdmin.WebAdmin.Test.Af;
using DoseBookAdmin.WebAdmin.Test.Service;
using DoseBookAdmin.WebAdmin.User.Af;
using DoseBookAdmin.WebAdmin.User.Service;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoseBookAdmin.WebAdmin
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
            services.AddControllersWithViews();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });


            // Windows Authentication on IIS
            services.AddAuthentication(IISDefaults.AuthenticationScheme);


            services.AddAuthentication("AdminCookies")
                     .AddCookie("AdminCookies", options =>
                     {
                         options.LoginPath = "/Home/Index/";
                     });

            services.AddHttpContextAccessor();

            IUnitOfWork unitOfWork = new UnitOfWork();

            //services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUnitOfWork>(provider => new UnitOfWork());

            //services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserService>(provider => new UserService(unitOfWork));

            //services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IDoctorService>(provider => new DoctorService(unitOfWork));

            //services.AddScoped<IAdviceService, AdviceService>();
            services.AddScoped<IAdviceService>(provider => new AdviceService(unitOfWork));

            //services.AddScoped<ITestService, TestService>();
            services.AddScoped<ITestService>(provider => new TestService(unitOfWork));

            //services.AddScoped<IPrescriptionMetaService, PrescriptionMetaService>();
            services.AddScoped<IPrescriptionMetaService>(provider => new PrescriptionMetaService(unitOfWork));

            //services.AddScoped<IMedicineDoseService, MedicineDoseService>();
            services.AddScoped<IMedicineDoseService>(provider => new MedicineDoseService(unitOfWork));

            //services.AddScoped<IUserAf, UserAf>();
            services.AddScoped<IUserAf>(provider => new UserAf(new UserService(unitOfWork)));

            //services.AddScoped<IDoctorAf, DoctorAf>();
            services.AddScoped<IDoctorAf>(provider => new DoctorAf(new DoctorService(unitOfWork)));

            DoctorAf doctorAf = new DoctorAf(new DoctorService(unitOfWork));

            //services.AddScoped<IAdviceAf, AdviceAf
            services.AddScoped<IAdviceAf>(provider => new AdviceAf(new AdviceService(unitOfWork), doctorAf));

            //services.AddScoped<ITestAf, TestAf>();
            services.AddScoped<ITestAf>(provider => new TestAf(new TestService(unitOfWork), doctorAf));

            //services.AddScoped<IMedicineDoseAf, MedicineDoseAf>();
            services.AddScoped<IMedicineDoseAf>(provider => new MedicineDoseAf(new MedicineDoseService(unitOfWork), doctorAf));

            //services.AddScoped<IPrescriptionMetaAf, PrescriptionMetaAf>();
            services.AddScoped<IPrescriptionMetaAf>(provider => new PrescriptionMetaAf(new PrescriptionMetaService(unitOfWork)));

            //services.AddTransient(_ => new MySqlConnection(Configuration["ConnectionStrings:Default"]));
            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseForwardedHeaders();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseForwardedHeaders();
                //loggerFactory.AddDebug(LogLevel.Error);
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //app.UseCors();
            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
