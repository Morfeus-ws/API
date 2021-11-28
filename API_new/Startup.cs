using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Oracle.ManagedDataAccess.Client;
using Microsoft.AspNetCore.Http;
using WebApplication1.Business;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Model;

namespace WebApplication1
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
            services.AddDbContext<TourOfHeroesContexto>();
            services
                .AddScoped<ITourOfHeroesRepository, TourOfHeroesRepository>()
                .AddScoped<IGrupoBusiness, GrupoBusiness>()
                .AddScoped<IHeroBusiness, HeroBusiness>();
            services.AddControllers().AddNewtonsoftJson(x =>
                x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = $"heroApi",
                    Description = $"heroApi",
                    Contact = new OpenApiContact
                    {
                        Name = $"heroApi",
                        Email = string.Empty,
                    }
                });
            });
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

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.Run(async (context) =>
            {
                // string connectionString = "User Id=Archerd;Password=fin_contab;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.171.130.114)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=orcl)));Service Name=orcl;Direct=true";
                await context.Response.WriteAsync("Hello World");

                using (var db = new TourOfHeroesContexto())
                {
                    // Creating a new department and saving it to the database
                    var t = db.Heroi.Where(x => x.Poder != "");

                    if (t.Count() > 0)
                        Console.WriteLine("All departments in the database:");
                }
            });
        }
    }
}