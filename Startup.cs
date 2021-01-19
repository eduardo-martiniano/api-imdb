using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_imdb.Contracts;
using api_imdb.Contracts.IRepositories;
using api_imdb.Data;
using api_imdb.Repositories;
using api_imdb.Services;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace api_imdb
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
            services.AddSwaggerGen();

            services.AddAutoMapper(typeof(Startup));

            services.AddDbContext<Context>(options => 
            {
                options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Database;Integrated Security=True;"); 
            });

            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IActorRepository, ActorRepository>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IActingRepository, ActingRepository>();

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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TASK API");
                c.RoutePrefix = string.Empty;
            });
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });
        }
    }
}
