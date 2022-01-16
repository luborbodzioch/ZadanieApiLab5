using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZadanieApi.Models;



namespace ZadanieApi
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
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Zadanie lab5 prezenty",
                    Version = "v1",
                    Description = "Moje API z prezentami",
                    Contact = new OpenApiContact { Name = "RT", Email = "r@rmail.com" },
                    License = new OpenApiLicense { Name = "Github", Url = new System.Uri("http://github.com/rt/license") }
                });
            });

            services.AddDbContext<PrezentContext>(opt => opt.UseInMemoryDatabase("Prezents"));
            services.AddMvc();

        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ZadanieApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<PrezentContext>();
            //var context = app.ApplicationServices.GetService<PrezentContext>();
            if(!context.Prezents.Any())
            { AddTestData(context); };
                
             
        }

        
        private static void AddTestData(PrezentContext context)
        {

            var prezent = new Prezent
            {
                Id = 1,
                NazwaPrezentu = "Lalka",
                CenaPrezentu = 100,
                KategoriaPrezentu = "Zabawka",
                WiekPrezentu = "1-10"
            };

            context.Prezents.Add(prezent);

            prezent = new Prezent
            {
                Id = 2,
                NazwaPrezentu = "Zegarek",
                CenaPrezentu = 1000,
                KategoriaPrezentu = "Bizuteria",
                WiekPrezentu = "18+"
            };

            context.Prezents.Add(prezent);

            prezent = new Prezent
            {
                Id = 3,
                NazwaPrezentu = "Samochodzik",
                CenaPrezentu = 250,
                KategoriaPrezentu = "Zabawka",
                WiekPrezentu = "1-10"
            };

            context.Prezents.Add(prezent);

            prezent = new Prezent
            {
                Id = 4,
                NazwaPrezentu = "Skarpetki",
                CenaPrezentu = 30,
                KategoriaPrezentu = "Ubranie",
                WiekPrezentu = "10+"
            };

            context.Prezents.Add(prezent);

            context.SaveChanges();
        }
    } 

}
