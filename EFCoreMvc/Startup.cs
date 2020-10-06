using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreMvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EFCoreMvc
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                options.Filters.Add(new AuthorizeFilter(policy));

            });
            services.ConfigureApplicationCookie(configure: options =>
             {
                 options.AccessDeniedPath = new PathString("/Admin/AccessDenied");
             });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("DeleteRolePolicy", a => a.RequireClaim("Delete Role")
                    .RequireClaim("Create Role"));
                options.AddPolicy("AdminRolePolicy", options =>
                {
                    options.RequireRole("Admin");
                });
                options.AddPolicy("EditRolePolicy",
                    options =>
                    {
                        options.RequireClaim("Edit Role", "true");
                    });
            });
            
            services.AddScoped<IEmployeeRepo, SQLRepository>();
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequiredUniqueChars = 3;
                options.Password.RequiredLength = 10;
            }).AddEntityFrameworkStores<ApplicationDbContext>();


            //services.AddDbContextPool<ApplicationDbContext>(options =>
            //{
            //    options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            //});
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }
            //app.UseWelcomePage();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(options =>
            {
                options.MapRoute(
                    name: "default",
                    template: "{controller=Home}" +
                    "/{action=Index}/{id?}");
            });
            //app.UseMvc();

        }
    }
}
