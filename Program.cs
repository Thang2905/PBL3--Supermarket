using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Admin;
using Admin.Services;
using System.Net.Http;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:8080/") });

// Đăng ký AuthorizedHttpClient
builder.Services.AddScoped(sp =>
{
    var client = new HttpClient
    {
        BaseAddress = new Uri("http://localhost:8080/")
    };

    var jsRuntime = sp.GetRequiredService<IJSRuntime>();
    return new AuthorizedHttpClient(client, jsRuntime);
});

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<StatisticService>();

await builder.Build().RunAsync();