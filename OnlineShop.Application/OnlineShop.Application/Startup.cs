using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineShop.Backend.Models;
using OnlineShop.Application.Data;

namespace OnlineShop.Application
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
            services.AddMvc();
            var connection = @"Server=ZALNET-PC\SQLDEV2019;Database=ETDatabase;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<ETDatabaseContext>(options => options.UseSqlServer(connection));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            //services.AddDefaultIdentity<ApplicationUser>()
            //    .AddDefaultUI(UIFramework.Bootstrap4)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //Info about Passwords Strength
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 6;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            //The Account Login page's settings
            services.ConfigureApplicationCookie(options =>
            {
                // Cookies settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.LoginPath = "/Account/Login"; // You can type here you own LoginPath, if you don't set custom path, ASP.NET Core will default to /Account/Login
                options.LogoutPath = "/Account/Logout"; // You can type here you own LogoutPath, if you don't set custom path, ASP.NET Core will default to /Account/Logout
                options.AccessDeniedPath = "/Account/AccessDenied"; // You can type you own AccesDeniedPath, if you don't set custom path, ASP.NET Core will default to /Account/AccessDenied;
                options.SlidingExpiration = true;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("OnlyForAdminAccess", policy => policy.RequireRole("Admin"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            CreateUserRoles(services).Wait();
        }

        private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            //initializing custom roles   
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Admin", "User", "Manager" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and seed them to the database
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            ApplicationUser user = await UserManager.FindByEmailAsync("bartosz.chojnacki4+1@gmail.com");
            if (user == null)
            {
                user = new ApplicationUser()
                {
                    UserName = "bartosz.chojnacki4+1@gmail.com",
                    Email = "bartosz.chojnacki4+1@gmail.com",
                };
                await UserManager.CreateAsync(user, "Pa55w.rd");
            }
            await UserManager.AddToRoleAsync(user, "Admin");
            ApplicationUser user1 = await UserManager.FindByEmailAsync("bartosz.chojnacki4+2@gmail.com");
            if (user1 == null)
            {
                user1 = new ApplicationUser()
                {
                    UserName = "bartosz.chojnacki4+2@gmail.com",
                    Email = "bartosz.chojnacki4+2@gmail.com",
                };
                await UserManager.CreateAsync(user1, "Pa55w.rd");
            }
            await UserManager.AddToRoleAsync(user1, "User");
            ApplicationUser user2 = await UserManager.FindByEmailAsync("bartosz.chojnacki4+3@gmail.com");
            if (user2 == null)
            {
                user2 = new ApplicationUser()
                {
                    UserName = "bartosz.chojnacki4+3@gmail.com",
                    Email = "bartosz.chojnacki4+3@gmail.com",
                };
                await UserManager.CreateAsync(user2, "Pa55w.rd");
            }
            await UserManager.AddToRoleAsync(user2, "Manager");
        }
    }
}
