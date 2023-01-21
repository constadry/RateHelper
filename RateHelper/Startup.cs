using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RateHelper.Models;
using RateHelper.Repositories;
using RateHelper.Services;

namespace RateHelper
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
            services.Configure<RateStoreDatabaseSettings>(
                Configuration.GetSection("RateStoreDatabase"));
            services.AddScoped<IRateRepository, RateRepository>();
            services.AddScoped<IRateService, RateService>();
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            // var options = Configuration.GetSection("RateStoreDatabase")
            //     .Get<IOptions<RateStoreDatabaseSettings>>();
            //
            // try
            // {
            //     SampleData.Initialize(options);
            // }
            // catch (Exception e)
            // {
            //     Console.WriteLine(e.Message + "An error occurred seeding the DB.");
            //     throw;
            // }
        }
    }
}