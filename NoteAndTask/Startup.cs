using System;
using System.Text;
using GraphiQl;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
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
using NoteAndTask.GraphQL;
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

            #endregion CookiePolicy

            #region EmailSender

            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, EmailSender>();

            #endregion EmailSender

            #region DbContext

            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DbConnection"));
            });
            services.AddTransient<IRepository, EfRepository<ApplicationContext>>();

            #endregion DbContext

            #region GraphQL

            services.AddScoped<IDependencyResolver>(ServiceProviderServiceExtensions =>
                new FuncDependencyResolver(ServiceProviderServiceExtensions.GetRequiredService));

            services.AddScoped<NatSchema>();

            services.AddGraphQL(x =>
            {
                x.ExposeExceptions = true; //set true only in development mode. make it switchable.
            })
            .AddGraphTypes(ServiceLifetime.Scoped);


            #endregion GraphQL

            #region GraphQlAuthorization

            services.AddGraphQL(x =>
            {
                x.ExposeExceptions = true;
            })
.AddGraphTypes(ServiceLifetime.Scoped)
.AddUserContextBuilder(httpContext => httpContext.User)
.AddDataLoader();

            #endregion GraphQlAuthorization

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

            #endregion Authentication



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
                app.UseHsts();
            }

            app.UseAuthentication();

            //app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseSpaStaticFiles();
            app.UseCookiePolicy();
            
            
            //app.UseGraphQL<NatSchema>();
            //app.UseGraphQLPlayground(new GraphQLPlaygroundOptions()); //to explorer API navigate https://*DOMAIN*/ui/playground
            app.UseGraphiQl("/graphql");
            
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