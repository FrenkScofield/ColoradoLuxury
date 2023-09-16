using ColoradoLuxury.Models.BLL;
using ColoradoLuxury.Models.DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Stripe;
using ColoradoLuxury.Models.VM;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
//public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
//        WebHost.CreateDefaultBuilder(args)
//            .UseUrls("https://*:5566")
//            .UseContentRoot(Directory.GetCurrentDirectory())
//            .UseIISIntegration()
//            .UseStartup<Startup>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ColoradoContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("StripeSettings"));

builder.WebHost
                            .UseUrls("https://*:5000")
                            .UseContentRoot(Directory.GetCurrentDirectory())
                            .UseIISIntegration();


var app = builder.Build();

StripeConfiguration.ApiKey = "sk_test_51DxVxFCfsgGnUgqjqZkYUOm9ZCixkzcRloQSlTYsEE3zRAbb18JJFRRPC99g1MY5qxW5dgvkrLIYxfI056zutLX000ft9sfaQV";

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
     name: "areas", "WebCms",
     pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
      name: "default",
      pattern: "{controller=Home}/{action=Index}/{id?}");
});



app.Run();
