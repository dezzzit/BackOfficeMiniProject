using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Threading.Tasks;
using BackOfficeMiniProject.Cache.AppSettings;
using BackOfficeMiniProject.DataAccess.Database.Context;
using BackOfficeMiniProject.DataAccess.Database.Repositories;
using BackOfficeMiniProject.DataAccess.Repository;
using BackOfficeMiniProjectCross.VueCoreConnection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using VueCliMiddleware;

namespace BackOfficeMiniProjectCross
{
    public class Startup
    {
        private CacheSetting _cacheSetting;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp";
            });
            
            services.AddDbContextPool<BackOfficeDbContext>(options => options
                // replace with your connection string
                .UseMySql("Server=localhost;port=3307;Database=test8;User=root;Password=12345;", mySqlOptions => mySqlOptions
                    // replace with your Server Version and Type
                    .MigrationsAssembly("BackOfficeMiniProjectCross")
                    
                ));

            _cacheSetting = Configuration.GetSection(nameof(CacheSetting))
                .Get<CacheSetting>();
            services
                .Configure<CacheSetting>(
                    Configuration.GetSection(nameof(CacheSetting)));

            //Add memory caching
            services.AddMemoryCache();

            //Configure IoC
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<ISumOfInventoryRepository, SumOfInventoryRepository>();
            
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BackOffice v1 API", Version = "v1" });
            });

            //output format mapping
            services.AddMvc(options =>
                {
                    options.FormatterMappings.SetMediaTypeMappingForFormat("xml", "application/xml");
                    options.FormatterMappings.SetMediaTypeMappingForFormat("js", "application/json");
                })
                .AddXmlSerializerFormatters();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseSpaStaticFiles();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            //use middleware and launch server for Vue

            app.UseSpaStaticFiles();
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "clientapp";
                if (env.IsDevelopment())
                {

                    spa.UseVueDevelopmentServer();
                }
            });

            // Init Database
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetRequiredService<BackOfficeDbContext>();
            context.Database.EnsureCreated();

            // Swagger setup
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(
                    "/swagger/v1/swagger.json",
                    "BackOffice v1 API"
                );
            });

        }
    }
}
