//https://www.c-sharpcorner.com/article/how-to-add-startup-cs-class-in-asp-net-core-6-project/
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stripe;
using TPGamePlus.Data;
using TPGamePlus.Domain;
using TPGamePlus.Domain.Entities;
using TPGamePlus.Models;
using TPGamePlus.Models.Admin;

namespace TPGamePlus
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        [Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<StripeOptions>(options =>
                Configuration.GetSection("StripeGP").Bind(options)
            );

            services.AddDbContext<GamePlusDbContext>(option =>
            option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            services.AddIdentity<ApplicationUser, IdentityRole>(opts =>
            opts.SignIn.RequireConfirmedEmail = false).AddDefaultTokenProviders()
            .AddEntityFrameworkStores<GamePlusDbContext>()
            .AddSignInManager();//.AddRoles<IdentityRole>();


            /* services.AddAuthentication().AddIdentityCookies();
             services.ConfigureApplicationCookie(options =>
                     options.AccessDeniedPath = "/Account/AccesRefuse");*/



            services.AddControllers()
                .AddFluentValidation(s =>
                {
                    s.RegisterValidatorsFromAssemblyContaining<Startup>();
                    s.DisableDataAnnotationsValidation = true;
                });

            services.AddControllersWithViews();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);

                options.Cookie.IsEssential = true;

            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                StripeConfiguration.SetApiKey(Configuration.GetConnectionString("Stripe:TestSecretKey"));

                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/Error/Error404";
                    await next();
                }
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();

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
