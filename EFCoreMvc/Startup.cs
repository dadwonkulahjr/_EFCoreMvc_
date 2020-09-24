using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreMvc.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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

            });
            services.AddScoped<IEmployeeRepo, SQLRepository>();
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
            //app.UseWelcomePage();
            app.UseStaticFiles();
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
