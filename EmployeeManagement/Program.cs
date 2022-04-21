using EmployeeManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using NLog.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;
builder.Services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("EmployeeDbConnection")));
builder.Services.AddMvc(option => { 
    option.EnableEndpointRouting = false;
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    option.Filters.Add(new AuthorizeFilter(policy));
});
builder.Services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();
builder.Services.AddIdentity<ApplicationIdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 7;
    options.Password.RequiredUniqueChars = 1;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    
});
//Change the ASP.NET CORE DEFAULT PATHS

//builder.Services.ConfigureApplicationCookie(options => options.LoginPath = new PathString("/Account/Register"));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RolePolicy", policy => policy.RequireClaim("Delete Role")
                                                            .RequireClaim("Edit Role"));
    options.AddPolicy("DeleteRolePolicy", policy => policy.RequireClaim("Delete Role"));
});


//Logging
builder.Logging.AddConfiguration(configuration.GetSection("Logging"));
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.AddEventSourceLogger();
// Enable NLog as one of the Logging Provider
builder.Logging.AddNLog();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    //app.UseStatusCodePages();
    //app.UseStatusCodePagesWithRedirects("/Error/{0}");
    app.UseStatusCodePagesWithReExecute("/Error/{0}");

}

app.UseStaticFiles();
app.UseAuthentication();
app.UseMvc(routes =>
{
    routes.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
