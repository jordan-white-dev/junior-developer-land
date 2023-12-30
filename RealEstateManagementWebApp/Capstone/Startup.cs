﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Capstone.DAL;
using Capstone.DAL.Interfaces;
using Capstone.Providers.Auth;

namespace Capstone
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //This method gets called by the runtime.Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                // Sets session expiration to 20 minutes
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            string connectionString = Configuration.GetConnectionString("Default");


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IPropertyDAL, PropertyDAL>(c => new PropertyDAL(connectionString));
            services.AddScoped<IUnitDAL, UnitDAL>(c => new UnitDAL(connectionString));
            services.AddScoped<IServiceRequestDAL, ServiceRequestDAL>(c => new ServiceRequestDAL(connectionString));
            services.AddScoped<IApplicationDAL, ApplicationDAL>(c => new ApplicationDAL(connectionString));
            services.AddScoped<IPaymentDAL, PaymentDAL>(c => new PaymentDAL(connectionString));
            services.AddScoped<IAuthProvider, SessionAuthProvider>();
            services.AddTransient<IUserDAL, UserDAL>(c => new UserDAL(connectionString));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseCookiePolicy();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Home}/{id?}");
            });
        }
    }
}
