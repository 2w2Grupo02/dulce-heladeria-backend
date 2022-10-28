using Dulce.Heladeria.Api.Helpers;
using Dulce.Heladeria.DataAccess.Data;
using Dulce.Heladeria.Models.UnitOfWork;
using Dulce.Heladeria.Repositories.IRepositories;
using Dulce.Heladeria.Repositories.Repositories;
using Dulce.Heladeria.Services.IManager;
using Dulce.Heladeria.Services.Manager;
using Dulce.Heladeria.Services.Mappings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Dulce.Heladeria.Api
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
            services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork, BaseUnitOfWork>();

            services.AddScoped<IItemRepository,ItemRepository>();
            services.AddScoped <IItemManager,ItemManager> ();

            services.AddScoped<IItemStockRepository, ItemStockRepository>();
            services.AddScoped<IItemStockManager, ItemStockManager>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserManager, UserManager>();

            services.AddScoped<IClientRepository,ClientRepository>();
            services.AddScoped <IClientManager,ClientManager> ();

            services.AddAutoMapper(typeof(EntityToDtoProfile));
            services.AddAutoMapper(typeof(DtoToEntityProfile));

            services.AddSwaggerGen(options => 
            {
                options.SwaggerDoc("DulceHeladeriaApi", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Dulce Heladeria Api",
                    Version = "v1",
                    Description = "Grupo 02, Metodologia de Sistemas, Tecnicatura Universitaria en Programacion, UTN FRC",
                    License = new Microsoft.OpenApi.Models.OpenApiLicense()
                    {
                        Name = "Documentacion del proyecto",
                        Url= new Uri("https://112902.atlassian.net/wiki/spaces/2022TUP2W2/")
                    }
                });

                options.AddSecurityDefinition("Bearer",
                   new OpenApiSecurityScheme
                   {
                       Description = "Autentificacion JWT (Bearer)",
                       Type = SecuritySchemeType.Http,
                       Scheme = "bearer"
                   });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement{
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type=ReferenceType.SecurityScheme
                        }
                    }, new List<string>()
                }
                 });
            });
            services.AddControllers();
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        var error = context.Features.Get<IExceptionHandlerFeature>();

                        if (error != null)
                        {
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
            }

            app.UseHttpsRedirection();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/DulceHeladeriaApi/swagger.json", "Dulce Heladeria Api");
                options.RoutePrefix = "";
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            
        }
    }
}
