using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using RestApiStudy.Models;
using RestApiStudy.Repositories;
using System;
using System.IO;
using System.Reflection;

namespace RestApiStudy
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {           

            services.Configure<PersonDatabaseSettings>(
                Configuration.GetSection(nameof(PersonDatabaseSettings)));

            services.AddSingleton<IPersonDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<PersonDatabaseSettings>>().Value);

            services.AddScoped<IPersonRepository, PersonRepository>();

            services.AddControllers();
            services.AddSwaggerGen();
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Person API",
                    Version = "v1",
                    Description = "This is an educational site"
                });
                var xfile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xpath = Path.Combine(AppContext.BaseDirectory, xfile);
                c.IncludeXmlComments(xpath);
            });

            //services.AddScoped<IInhouseHotelReportRepository, InhouseHotelReportRepository>();

        }

        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

        }

    }
}
