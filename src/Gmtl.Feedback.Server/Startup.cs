using System;
using System.Collections.Generic;
using FluentValidation.AspNetCore;
using Gmtl.Feedback.Server.Data;
using Gmtl.Feedback.Server.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gmtl.Feedback.Server
{
    public class Startup
    {
        public const string CookieName = "Feedback.Session";
        public const string LoginUrl = "/Home/Login";

        public Startup(IHostingEnvironment env)
        {
            Environment = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        private IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            var sqlConnectionString = Configuration.GetConnectionString("connStr");
            services.AddDbContext<FeedbackDbContext>(options =>
                options.UseLazyLoadingProxies().UseNpgsql(sqlConnectionString,
                    b => b.MigrationsHistoryTable("__FeedbackMigrationsHistory")
                ), ServiceLifetime.Transient);

            services.AddOptions();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => options.LoginPath = LoginUrl);
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.Name = CookieName;
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddTransient<DomainService, DomainService>();
            services.AddTransient<FeedbackService, FeedbackService>();
            services.AddTransient<UserService, UserService>();
            services.AddTransient<ApiKeyService, ApiKeyService>();

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "Feedback API",
                    Version = "v1",
                    Description = "Exemplary description of this project"
                });
                s.DescribeAllEnumsAsStrings();
            });
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseSession();
            app.UseAuthentication();
            app.UseMvc(routes => routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"));

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                foreach (var dbContextType in new List<Type> { typeof(FeedbackDbContext) })
                {
                    var dbContext = (DbContext)serviceScope.ServiceProvider.GetRequiredService(dbContextType);
                    dbContext.Database.Migrate();
                }
            }

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Feedback API v1");
            });
        }
    }
}