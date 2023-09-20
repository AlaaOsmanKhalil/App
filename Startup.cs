using DatingApp.Data;
using DatingApp.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(_config.GetConnectionString("DefaultConnection"));
            });

            services.AddControllers();
            services.AddCors();
            /* services.AddSwaggerGen(c =>
             {
                 c.SwaggerDoc("v1", new OpenApiInfo { Title = "DatingApp", Version = "v1" });
             });

            services.AddSpaStaticFiles(Configuration =>
            {
                Configuration.RootPath = "client/dist";
            });*/
            //services.AddTransient<DataSeeder>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)//DataSeeder dataSeeder
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                /*  app.UseSwagger();
                  app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DatingApp v1"));

              }
              app.UseSpa(spa =>
              {
                  spa.Options.SourcePath = "client"; // Adjust the path to your Angular app
                  spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
              });*/

                app.UseHttpsRedirection();

                app.UseRouting();

                app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200")); //make the app run on the same port

                app.UseAuthorization();
                //dataSeeder.SeedData();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            }
        }
    }
}
