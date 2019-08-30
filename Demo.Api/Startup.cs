using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Demo.Api.Services;
using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.Swagger;

namespace Demo.Api
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
            services.AddDbContext<RentalDbContext>(opt =>
             opt.UseInMemoryDatabase("RentalList"));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddMvcCore().AddJsonFormatters();

            services.AddVersionedApiExplorer(
                      options =>
                      {
                          //The format of the version added to the route URL
                          options.GroupNameFormat = "'v'VVV";
                          //Tells swagger to replace the version in the controller route
                          options.SubstituteApiVersionInUrl = true;
                      }); ;

            services.AddApiVersioning(options => options.ReportApiVersions = true);
            services.AddSwaggerGen(
                options =>
                {
                    // Resolve the temprary IApiVersionDescriptionProvider service
                    var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                    // Add a swagger document for each discovered API version
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerDoc(description.GroupName, new Info()
                        {
                            Title = $"{this.GetType().Assembly.GetCustomAttribute<System.Reflection.AssemblyProductAttribute>().Product} {description.ApiVersion}",
                            Version = description.ApiVersion.ToString(),
                            Description = description.IsDeprecated ? $"{this.GetType().Assembly.GetCustomAttribute<AssemblyDescriptionAttribute>().Description} - DEPRECATED" : this.GetType().Assembly.GetCustomAttribute<AssemblyDescriptionAttribute>().Description,

                        });
                    }

                    // Add a custom filter for settint the default values
                    options.OperationFilter<SwaggerDefaultValues>();

                    // Tells swagger to pick up the output XML document file
                    options.IncludeXmlComments(Path.Combine(
                        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"{this.GetType().Assembly.GetName().Name}.xml"
                        ));

                });

            //Register Services
            services.AddTransient<IRentalService, RentalService>();
        }

        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    //Build a swagger endpoint for each discovered API version
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });


            app.UseMvc();

        }
    }
}
