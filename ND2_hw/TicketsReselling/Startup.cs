using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TicketsReselling.Business;
using TicketsReselling.Business.Models;
using Microsoft.AspNetCore.Mvc.Razor;

namespace TicketsReselling
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
            services.AddControllersWithViews()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            services.AddLocalization(opts =>
            {
                opts.ResourcesPath = "Resources";
            });


            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
              .AddCookie(opts =>
              {
                  opts.LoginPath = "/User/Login";
                  opts.AccessDeniedPath = "/User/Login";
                  opts.Cookie.Name = "AuthDemo";
              });

            services.AddAuthorization(opts =>
            {
                opts.AddPolicy("HasRole", policy => policy.RequireClaim(ClaimTypes.Role));
                opts.AddPolicy("User", policy => policy.RequireClaim(ClaimTypes.Role, UserRoles.User));
                opts.AddPolicy("Administrator", policy => policy.RequireClaim(ClaimTypes.Role, UserRoles.Administrator));
            });

            services.AddSingleton<EventsRepository>();
            services.AddSingleton<UsersRepository>();
            services.AddSingleton<TicketsRepository>();
            services.AddSingleton<OrdersRepository>();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            var localizationoptions = new RequestLocalizationOptions()
                .SetDefaultCulture(Locales.SupportedLocales[0])
                .AddSupportedCultures(Locales.SupportedLocales)
                .AddSupportedUICultures(Locales.SupportedLocales);

            app.UseRequestLocalization(localizationoptions);

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
