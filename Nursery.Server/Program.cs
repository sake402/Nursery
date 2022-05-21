using Nursery.Core.Client;
using LivingThing.Core.Frameworks.Server;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using LivingThing.Interface.Server.Portal;
using LivingThing.Core.Frameworks.Portal;
using LivingThing.Core.Frameworks.Common.Services;
using LivingThing.Core.Community.All;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddCoreServerLibraries();
builder.Services.AddGidiClient();
// builder.Services.AddCommunity();
builder.Services.AddSingleton<IConfiguration>(x => builder.Configuration);
builder.Services.AddSingleton<IRootPortalConfiguration, RootPortalConfiguration>();
builder.Services.AddScoped<ServiceFactory>(x => (type, op) => !op ? x.GetRequiredService(type) : x.GetService(type));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
