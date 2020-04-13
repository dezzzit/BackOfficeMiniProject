using BackOfficeMiniProject.Cache.AppSettings;
using BackOfficeMiniProject.DataAccess.Database.Context;
using BackOfficeMiniProject.DataAccess.Database.Repositories;
using BackOfficeMiniProject.DataAccess.Repository;
using BackOfficeMiniProjectCross.AppSettings;
using BackOfficeMiniProjectCross.VueCoreConnection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace BackOfficeMiniProjectCross
{
    public class Startup
    {
        private CacheSetting _cacheSetting;
        private ConnectionString _connectionString;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //add CORS

            services.AddCors(options =>
            {
                options.AddPolicy("VueCorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod(); 
                });
            });

            services.AddControllers();
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp";
            });
            

            _connectionString = Configuration.GetSection(nameof(ConnectionString))
                .Get<ConnectionString>();
            _cacheSetting = Configuration.GetSection(nameof(CacheSetting))
                .Get<CacheSetting>();
            services
                .Configure<CacheSetting>(
                    Configuration.GetSection(nameof(CacheSetting)))
                .Configure<ConnectionString>(
                    Configuration.GetSection(nameof(ConnectionString)));

            services.AddDbContextPool<BackOfficeDbContext>(options => options
                // replace with your connection string
                .UseMySql(_connectionString.DefaultConnectionString, mySqlOptions => mySqlOptions
                    // replace with your Server Version and Type
                    .MigrationsAssembly("BackOfficeMiniProjectCross")

                ));

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
            //exclude spa invocation for run Swagger  
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

            app.UseCors("VueCorsPolicy");
        }
    }
}
