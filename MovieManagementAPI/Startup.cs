using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace MovieManagementAPI
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
            services.AddControllers();
            services.AddDbContext<MovieDataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Database")));

            //cors
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                            .WithOrigins(Configuration["AllowedOrigins:Site"])
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .Build()
                );

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //LogApiRequests(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        //For some debugging so I can see objects passed in.
        private void LogApiRequests(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                try
                {
                    var requestBody = context.Request.Body;
                    var requestMethod = context.Request.Method;
                    if (requestMethod == "POST")
                    {
                        var reader = new StreamReader(requestBody);
                        var body = await reader.ReadToEndAsync();
                        await File.WriteAllTextAsync(@"C:\Users\Lee\Documents\MovieProj\logs\" + requestMethod + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + ".txt", body);
                    }
                }
                catch(Exception ex)
                {
                    File.AppendAllText(@"C:\Users\Lee\Documents\MovieProj\errors\error.txt", ex.ToString());
                }

                await next();
            });
        }
    }
}
