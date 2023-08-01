using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BikingBuddy.Data;
using BikingBuddy.Data.Models;
using BikingBuddy.Services.Contracts;
using BikingBuddy.Web.Infrastructure.Extensions;
using BikingBuddy.Web.Infrastructure.ModelBinders;
using static BikingBuddy.Common.GlobalConstants;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<BikingBuddyDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddDefaultIdentity<AppUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount =
            builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");


        options.Password.RequireDigit = builder.Configuration.GetValue<bool>("Identity:Password:RequireDigit");
        options.Password.RequireNonAlphanumeric =
            builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
        options.Password.RequireLowercase = builder.Configuration.GetValue<bool>("Identity:Password:");
        options.Password.RequireUppercase = builder.Configuration.GetValue<bool>("Identity:Password:RequireUppercase");
        options.Password.RequireNonAlphanumeric =
            builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
        options.Password.RequiredLength = builder.Configuration.GetValue<int>("Identity:Password:RequiredLength");
    })
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<BikingBuddyDbContext>();

builder.Services.AddControllersWithViews()
    .AddMvcOptions(options => { options.ModelBinderProviders.Insert(0, new DoubleModelBinderProvider()); });

builder.Services.AddApplicationServices(typeof(IEventService));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseDeveloperExceptionPage();
}
else
{ 
    app.UseExceptionHandler("/Home/Error/5OO");
    app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();



if (app.Environment.IsDevelopment())
{
    app.SeedAdministrator(AdminRoleEmail);
}

app.UseEndpoints(config =>
{
    config.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
    
    config.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    config.MapRazorPages();

});
    app.Run();
