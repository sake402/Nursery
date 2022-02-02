using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Nursery.Wasm;
using Nursery.Core.Client;
using LivingThing.Web.Client.Blazor.Library.WebAssembly;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddGidiClient();
builder.Services.AddBlazorWebAssemblyLibraries();

await builder.Build().RunAsync();
