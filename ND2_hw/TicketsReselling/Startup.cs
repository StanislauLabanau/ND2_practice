using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TicketsReselling.Business.Models;
using Microsoft.AspNetCore.Mvc.Razor;
using TicketsReselling.Core;
using TicketsReselling.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using TicketsReselling.DAL.Models;
using TicketsReselling.Core.Interfaces;

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

            services.AddControllers();

            services.AddLocalization(opts =>
            {
                opts.ResourcesPath = "Resources";
            });

            services.AddScoped<IEventsService, EventsService>();
            services.AddScoped<IOrdersService, OrdersService>();
            services.AddScoped<ITicketsService, TicketsService>();
            services.AddScoped<IVenuesService, VenuesService>();
            services.AddScoped<ICitiesService, CitiesService>();
            services.AddScoped<IEmailService, EmailService>();

            services.AddDbContext<TicketsResellingContext>(o =>
            {
                o.UseSqlServer(Configuration.GetConnectionString("TicketResellingConnection"))
                    .EnableSensitiveDataLogging();
            });

            services.AddDefaultIdentity<User>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<TicketsResellingContext>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;

                options.SignIn.RequireConfirmedAccount = true;
            });

            services.AddSwaggerGen();
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

            app.UseSwagger();

            var localizationoptions = new RequestLocalizationOptions()
                .SetDefaultCulture(Locales.SupportedLocales[0])
                .AddSupportedCultures(Locales.SupportedLocales)
                .AddSupportedUICultures(Locales.SupportedLocales);

            app.UseRequestLocalization(localizationoptions);

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwaggerUI(s=> {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "TicketReselling API v1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllers();
            });
        }
    }
}
