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
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using ColoradoLuxury.Services;
using Stripe.BillingPortal;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Program>());

builder.Services.AddDbContext<ColoradoContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("StripeSettings"));
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddScoped<IEmailSender, EmailSender>(emailsender => new EmailSender(
               builder.Configuration.GetValue<string>("EmailSetting:Host"),
               builder.Configuration.GetValue<int>("EmailSetting:Port"),
               builder.Configuration.GetValue<bool>("EmailSetting:SSL"),
               builder.Configuration["EmailSetting:Username"],
               builder.Configuration["EmailSetting:Password"],
               builder.Configuration["EmailSetting:Subject"]

               ));

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IViewRenderService, ViewRenderToString>();

//builder.WebHost
//                            .UseUrls("https://*:5000")
//                            .UseContentRoot(Directory.GetCurrentDirectory())
//                            .UseIISIntegration();


var app = builder.Build();

StripeConfiguration.ApiKey = builder.Configuration.GetValue<string>("StripeSettings:Secretkey");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();
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
