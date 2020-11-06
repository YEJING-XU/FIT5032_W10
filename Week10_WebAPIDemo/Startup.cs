using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Week10_WebAPIDemo.Models;

namespace Week10_WebAPIDemo
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<UnitContext>(opt =>
               opt.UseInMemoryDatabase("UnitList"));
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins(
                                          //"https://cdpn.io",
                            "https://localhost:44320",
                            "https://localhost"
                            //"https://127.0.0.1",
                            //"https://192.168.0.107"
                            ).AllowAnyHeader()
                                                  .AllowAnyMethod();
                                  });
            });

            /*            services.AddCors(options =>
                        {
                            options.AddDefaultPolicy(
                                builder =>
                                {
                                    builder.WithOrigins(
                                        "https://cdpn.io",
                                        "https://localhost:44320", 
                                        "https://localhost",
                                        "https://127.0.0.1",
                                        "https://192.168.0.107");
                                });
                        });*/
            services.AddControllers();
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

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
