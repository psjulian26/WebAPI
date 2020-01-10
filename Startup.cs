﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebAPI.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CoreCodeCamp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<StudentsContext>();
            services.AddScoped<IStudentsRepo, StudentsRepo>();

            services.AddAutoMapper();

            services.AddMvc()
              .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //services.AddDbContext<StudentsContext>();
            //services.AddScoped<IStudentsRepo, StudentsRepo>();

            //services.AddAutoMapper();

            //services.AddApiVersioning(opt =>
            //{
            //    opt.AssumeDefaultVersionWhenUnspecified = true;
            //    opt.DefaultApiVersion = new ApiVersion(1, 1);
            //    opt.ReportApiVersions = true;
            //    opt.ApiVersionReader = new UrlSegmentApiVersionReader();
            //opt.ApiVersionReader = ApiVersionReader.Combine(
            //  new HeaderApiVersionReader("X-Version"),
            //  new QueryStringApiVersionReader("ver", "version"));

            //opt.Conventions.Controller<SubjectController>()
            //  .HasApiVersion(new ApiVersion(1, 0))
            //  .HasApiVersion(new ApiVersion(1, 1))
            //  .Action(c => c.Delete(default(string), default(int)))
            //    .MapToApiVersion(1, 1);

            //});
            //services.AddMvc(opt => opt.EnableEndpointRouting = false);

            //services.AddMvc()
            //.SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
