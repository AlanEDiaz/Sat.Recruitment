using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Sat.Recruitment.Application.UseCases;
using Sat.Recruitment.Application.UseCases.Interfaces;
using Sat.Recruitment.Domain.DTO;
using Sat.Recruitment.Domain.Interfaces;
using Sat.Recruitment.Infrastructure.Data.Context;
using Sat.Recruitment.Infrastructure.Repositories;
using Sat.Recruitment.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sat.Recruitment.Api", Version = "v1" });
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "Sat.Recruitment.Api", Version = "v2" });


                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
            services.AddApiVersioning(o =>
            {
                o.DefaultApiVersion = new ApiVersion(1, 0);
                // If the client hasn't specified the API version in the request, use the default API version number 
                o.AssumeDefaultVersionWhenUnspecified = true;
                // Advertise the API versions supported for the particular endpoint
                o.ReportApiVersions = true;
                //support to different types of versioning by QueryString =?api-version={0}, by Http Header X-Version={0} and by mediaType Header application/json;ver={0}
                o.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("api-version"),
                    new HeaderApiVersionReader("X-Version"),
                    new MediaTypeApiVersionReader("ver"));
            });

            services.AddMvcCore().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = (errorContext) =>
                {
                    var errors = errorContext.ModelState.Values.SelectMany(e => e.Errors.Select(m => new
                    {
                        m.ErrorMessage

                    })).ToList();

                    var result = new Response
                    {
                        IsSuccess = false,
                        Errors = errors.Select(e => e.ErrorMessage).FirstOrDefault()
                    };
                    return new BadRequestObjectResult(result);
                };
                options.SuppressMapClientErrors = true;
            });




            services.AddDbContext<SatRecruitmentContext>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddScoped<IUserBusinessLogic, UserBusinessLogic>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
                // specifying the Swagger JSON endpoint.
                // Only for development
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sat.Recruitment.Api V1");
                    c.SwaggerEndpoint("/swagger/v2/swagger.json", "Sat.Recruitment.Api V2");


                });

            }
            
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
