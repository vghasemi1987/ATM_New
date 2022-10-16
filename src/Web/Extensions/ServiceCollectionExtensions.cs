using DomainEntities.ApplicationUserAggregate;
using FluentValidation.AspNetCore;
using Infrastructure.Data.Commons;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using Web.Extensions.Attributes;

namespace Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection Build(this IServiceCollection services,
             IWebHostEnvironment webHostEnvironment)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Permission", policyBuilder =>
                {
                    policyBuilder.Requirements.Add(new PermissionAuthorizationRequirement());
                });
            });
            services.AddAntiforgery(opts =>
            {
                opts.Cookie.Name = "_gui";
                opts.Cookie.SecurePolicy = CookieSecurePolicy.None;
                //opts.Cookie.SecurePolicy = webHostEnvironment.IsDevelopment() ? CookieSecurePolicy.None : CookieSecurePolicy.Always;
                opts.Cookie.SameSite = SameSiteMode.Strict;
            });
            return services;
        }

        public static IServiceCollection AddCustomControllers(this IServiceCollection services)
        {
            services.AddControllersWithViews(config =>
            {
                //config.ModelBinderProviders.Insert(0, new CustomStringModelBinderProvider());
                //config.Filters.Add(new MvcValidateModelStateFilter());
                //config.Filters.Add(typeof(MvcValidateModelStateFilter));
                //config.Filters.Add(typeof(DynamicAuthorizationFilter));
            })
                .AddRazorRuntimeCompilation()
                .AddJsonOptions(jsonOptions =>
                {
                    jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null;
                })
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>())
                .SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            return services;
        }

        public static IServiceCollection AddCustomizedCookie(this IServiceCollection services, IWebHostEnvironment webHostEnvironment)
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                //options.Cookie.SecurePolicy = webHostEnvironment.IsDevelopment() ? CookieSecurePolicy.None : CookieSecurePolicy.Always;
                options.Cookie.SecurePolicy = CookieSecurePolicy.None;
                options.ExpireTimeSpan = TimeSpan.FromHours(24);
                options.LoginPath = "/usermanagement/signin";
                options.LogoutPath = "/usermanagement/signout";
                //options.Cookie.Name = "_cnf";
                options.SessionStore = new MemoryCacheTicketStore();
                options.AccessDeniedPath = new PathString("/error/code/403");
            });
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
                options.ConsentCookie.Name = "cookieconsent_status";
            });
            return services;
        }

        public static IServiceCollection AddCustomizedSession(this IServiceCollection services,
            IWebHostEnvironment webHostEnvironment)
        {
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(2);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                //options.Cookie.SecurePolicy = webHostEnvironment.IsDevelopment() ? CookieSecurePolicy.None : CookieSecurePolicy.Always;
                options.Cookie.SecurePolicy = CookieSecurePolicy.None;
                options.Cookie.Name = "_panel";
            });
            return services;
        }

        public static IServiceCollection AddCustomizedIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 5;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.User.RequireUniqueEmail = false;
                })
                .AddEntityFrameworkStores<AtmContext>()
                .AddDefaultTokenProviders();


            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(o =>
               {
                   o.LoginPath = new PathString("/usermanagement/signin");
                   o.ExpireTimeSpan = TimeSpan.FromMinutes(200);
               });

            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, UserClaimsPrincipalFactory>();
            return services;
        }
        public static IServiceCollection ConvertUrlsToLowercase(this IServiceCollection services)
        {
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
            return services;
        }

        public static IServiceCollection AddRazorViewEngine(this IServiceCollection services)
        {
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Insert(0, new AppViewLocationExpander());
            });
            return services;
        }

        public static IServiceCollection AddCustomizedDataStore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AtmContext>(c =>
            {
                c.UseSqlServer(configuration.GetConnectionString("AtmConnection"),
                                     sqlServerOptionsAction: sqlOptions =>
                                     {
                                         sqlOptions.MigrationsAssembly(typeof(AtmContext).GetTypeInfo().Assembly.GetName().Name);
                                         sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                     });
            });
            return services;
        }
    }
}