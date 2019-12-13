using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgroPi.BLL.Services;
using AgroPi.Dal;
using AgroPi.Dal.Entities;
using AgroPi.Dal.Seed;
using AgroPi.Web.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace AgroPi.Web
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //MySQL Adatbázis hozzáadása 
            services.AddDbContextPool<AgroPiDbContext>(
                     options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
                     mysqlOptions =>
                     {
                         mysqlOptions
                            .ServerVersion(new Version(5, 5, 0), ServerType.MySql);
                     }
                     ));

            //Identity hozáadása
            services
                .AddIdentity<User, Role>(options =>
                    {
                        // Password settings
                        options.Password.RequireDigit = Configuration.GetValue<bool>("Password:RequireDigit", true);
                        options.Password.RequiredLength = Configuration.GetValue<int>("Password:RequiredLength", 8);
                        options.Password.RequireNonAlphanumeric = Configuration.GetValue<bool>("Password:RequireNonAlphanumeric", false);
                        options.Password.RequireUppercase = Configuration.GetValue<bool>("Password:RequireUppercase", true);
                        options.Password.RequireLowercase = Configuration.GetValue<bool>("Password:RequireLowercase", false);
                        options.Password.RequiredUniqueChars = Configuration.GetValue<int>("Password:RequiredUniqueChars", 6);

                        // Lockout settings
                        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(Configuration.GetValue<int>("Password:DefaultLockoutTimeSpan", 30));
                        options.Lockout.MaxFailedAccessAttempts = Configuration.GetValue<int>("Password:MaxFailedAccessAttempts", 10);
                        options.Lockout.AllowedForNewUsers = Configuration.GetValue<bool>("Password:AllowedForNewUsers", true);

                        // User settings
                        options.User.RequireUniqueEmail = Configuration.GetValue<bool>("Password:RequireUniqueEmail", true);
                    })
                .AddDefaultUI()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<AgroPiDbContext>();

            services
                .ConfigureApplicationCookie(options =>
                    {
                        // Cookie settings
                        options.Cookie.HttpOnly = Configuration.GetValue<bool>("Cookie:HttpOnly", true);
                        options.ExpireTimeSpan = TimeSpan.FromMinutes(Configuration.GetValue<int>("Cookie:ExpireTimeSpan", 30));
                        // If the LoginPath isn't set, ASP.NET Core defaults 
                        // the path to /Account/Login.
                        options.LoginPath = Configuration.GetValue("Cookie:LoginPath", "/Account/Login");
                        // If the AccessDeniedPath isn't set, ASP.NET Core defaults 
                        // the path to /Account/AccessDenied.
                        options.AccessDeniedPath = Configuration.GetValue("Cookie:LoginPath", "/Account/AccessDenied");
                        options.LogoutPath = Configuration.GetValue("Cookie:LogoutPath", "/Account/Logout");
                        options.SlidingExpiration = Configuration.GetValue<bool>("Cookie:SlidingExpiration", true);
                        options.Cookie.Name = Configuration.GetValue("Cookie:Name", ".AspNetCore.Cookies");
                    });

            services.Configure<SignInOptions>(options =>
            {
                options.RequireConfirmedEmail = Configuration.GetValue<bool>("SignInOptions:RequireConfirmedEmail", true);
                options.RequireConfirmedPhoneNumber = Configuration.GetValue<bool>("SignInOptions:RequireConfirmedPhoneNumber", false);
            });

            //Add SignalR
            services.AddSignalR();

            //Add Transient Services
            services
                .AddTransient<RolesAndAdministratorSeeder>()
                .AddTransient<SensorsService>();

            //Add Hosted Services
            services.AddHostedService<TimerService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseSignalR(routes =>
            {
                routes.MapHub<SensorsHub>("/SensorsHub");
            });
            app.UseMvc();
        }
    }
}
