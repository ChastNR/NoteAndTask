using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NoteAndTask.Extensions;
using NoteAndTask.Extensions.EmailSender;
using Repository.Context;
using Repository.Interface;
using Repository.Repositories;

namespace NoteAndTask
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            #region CookiePolicy

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            #endregion

            #region EmailSender

            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, EmailSender>();

            #endregion

            #region DbContext

            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DbConnection"));
            });
            services.AddTransient<IRepository, EfRepository<ApplicationContext>>();

            #endregion

            #region Authentication

            services.Configure<AuthOptions>(Configuration.GetSection("AuthOptions"));

            var authConfig = Configuration.GetSection("AuthOptions").Get<AuthOptions>();

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfig.SecurityKey));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        //what to validate
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        //setup validate data
                        ValidIssuer = authConfig.Issuer,
                        ValidAudience = authConfig.Audience,
                        IssuerSigningKey = symmetricSecurityKey,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            #endregion

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "client-app/build"; });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                //app.UseHsts();
            }

            app.UseAuthentication();

            //app.UseStaticFiles();
            //app.UseHttpsRedirection();
            app.UseSpaStaticFiles();
            app.UseCookiePolicy();

            #region UseMvc

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            #endregion

            #region UseSpa

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "client-app";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });

            #endregion
        }
    }
}