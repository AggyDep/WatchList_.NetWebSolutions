using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.Extensions;
//using UI.Handlers;
using UI.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UI.Handlers;
using Microsoft.AspNetCore.Mvc.Razor;

namespace UI
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
            // Start HttpClient
            // ================
            services.AddTransient<ValidateHeaderHandler>();
            services.AddHttpClient("WebApi", c =>
            {
                c.BaseAddress = new Uri("http://localhost:55169/api/");
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            }).AddHttpMessageHandler<ValidateHeaderHandler>();
            // End HttpClient
            // ==============

            // Start session state configuration
            // =================================
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromSeconds(3600);
                options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
            });
            // End session state configuration
            // ===============================

            services.AddHttpContextAccessor();

            // Start Localization
            // ==================

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddControllersWithViews()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix, options => options.ResourcesPath = "Resources")
                .AddDataAnnotationsLocalization();

            // End Localization
            // ================

            // Start Configure strongly typed settings objects
            // ===============================================

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();

            // End Configure strongly typed settings objects
            // =============================================

            // Start Custom Services
            // =====================

            services.AddSingleton<IStateHelper, StateHelper>();

            // End Custom Services
            // ===================
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IStateHelper stateHelper)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();

            app.Use(async delegate (HttpContext context, Func<Task> next)
            {
                if (context.Request.GetStateData("StateData") != null)
                {
                    stateHelper.SetState(context.Request.GetStateData("StateData"));
                }
                await next.Invoke().ConfigureAwait(false);
            });

            app.UseRouting();

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
