﻿using System;
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
using ToDoNotes.Models;
using ToDoNotes.Services;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ToDoNotes
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment currentEnvironment)
        {
            Configuration = configuration;
            _currentEnvironment = currentEnvironment;
        }
        public IHostingEnvironment _currentEnvironment{ get; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            // Adding service for SWAGGER
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
            // Added Services for ToDoNotesContext database
            // Added services for interface class and its child class which implements its methods
            services.AddScoped<IDataAccess, DataAccess>();

            services.AddTransient<DataAccess>();
            services.AddMvc();
            if (_currentEnvironment.IsEnvironment("Testing"))
            {
                services.AddDbContext<PrototypeContext>(options =>
                options.UseInMemoryDatabase("InMemoryDataBaseString"));

            }
            
                services.AddDbContext<PrototypeContext>(options =>
                   options.UseSqlServer(Configuration.GetConnectionString("PrototypeContext"), dbOptions => dbOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd:null)));
                //services.AddDbContext<PrototypeContext>(options =>
                //    options.UseSqlServer(Configuration.GetConnectionString("PrototypeContext")));

            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,PrototypeContext context)
        {
            if (env.IsDevelopment())
            {
               // context.Database.Migrate();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseHttpsRedirection();
            app.UseMvc();
            //context.Database.Migrate();
        }
        
    }
}
