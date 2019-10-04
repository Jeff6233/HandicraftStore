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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Handicraft.Models;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using Handicraft.Filter;
using Handicraft.Services;

namespace Handicraft
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options=> {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
            });
            services.AddSwaggerGen(option => {
                option.SwaggerDoc("v1", new Info
                {
                    Title = "Handicraft",
                    Version = "v1",
                });
                option.OperationFilter<SwaggerUploadFilter>();
            });
            services.AddDbContext<HandicraftContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("HandicraftContext")));
            services.AddCors(option => option.AddPolicy("Cors", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

            services.AddTransient<ISystemService, SystemService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(option =>
                option.SwaggerEndpoint("/Handicraft/Swagger/v1/Swagger.json", "Api v1")
                );
            app.UseCors("Cors");
        }
    }
}
