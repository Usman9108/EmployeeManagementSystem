using CenterpriseAllProject.Models;
using CenterpriseAllProject.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using NLog.Web;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        ILoggingBuilder loggingBuilder = builder.Logging;
        loggingBuilder.AddNLog("nlog.config");
        var services = builder.Services;
        var _config =  builder.Configuration;
        services.AddDbContextPool<AppDbContext>(options =>
        {
            options.UseSqlServer(_config.GetConnectionString("EmployeeDbConnection"));
        });
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.Password.RequiredLength = 10;
            options.Password.RequiredUniqueChars = 3;
            options.Password.RequireNonAlphanumeric = false;
            options.SignIn.RequireConfirmedEmail = true;
            options.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        }).AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders()
        .AddTokenProvider<CustomEmailConfirmationTokenProvider<ApplicationUser>>("CustomEmailConfirmation");

        services.Configure<DataProtectionTokenProviderOptions>(options =>
        {
            options.TokenLifespan = TimeSpan.FromHours(5);
        });

        services.Configure<CustomEmailConfirmationTokenProviderOptions>(options =>
        {
            options.TokenLifespan = TimeSpan.FromDays(3);
        });

        services.AddMvc((options) =>
        {
            options.EnableEndpointRouting = false;
            var policy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();
            options.Filters.Add(new AuthorizeFilter(policy));
        }).AddXmlSerializerFormatters();

        services.AddAuthentication().AddGoogle(options =>
        {
            options.ClientId = _config.GetSection("Google")["ClientId"]!;
            options.ClientSecret = _config.GetSection("Google")["ClientSecret"]!;
        }).AddFacebook(options =>
        {
            options.AppId = _config.GetSection("Facebook")["AppId"]!; ;
            options.AppSecret = _config.GetSection("Facebook")["AppSecret"]!;
        });

        services.ConfigureApplicationCookie(options =>
        {
            options.AccessDeniedPath = new PathString("/Administration/AccessDenied");
        });
        services.AddAuthorization(options =>
        {
            options.AddPolicy("DeleteRolePolicy", policy => policy.RequireClaim("Delete Role","true"));
            options.AddPolicy("AdminRolePolicy", policy => policy.RequireRole("Admin"));
            options.AddPolicy("EditRolePolicy", policy => policy.AddRequirements(new ManageAdminRolesAndClaimsRequirement()));
            options.AddPolicy("CreateRolePolicy", policy => policy.RequireClaim("Create Role"));
        });
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddSingleton<IAuthorizationHandler,CanEditOnlyOtherAdminRolesAndClaimsHandler>();
        services.AddSingleton<IAuthorizationHandler,SuperAdminHandler>();
        services.AddSingleton<DataProtectionPurposeStrings>();
        var app = builder.Build();
        var env = app.Environment;
        if(env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseStatusCodePagesWithReExecute("/Error/{0}");
        }
        app.UseFileServer();
        app.UseAuthentication();
        //app.UseMvcWithDefaultRoute();
        app.UseMvc(routes =>
        {
            routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
        });
        app.Run();
    }
}