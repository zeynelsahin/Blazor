using BlazingTrails.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices(configuration =>
{
    configuration.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
    configuration.SnackbarConfiguration.PreventDuplicates = false;
    configuration.SnackbarConfiguration.NewestOnTop = false;
    configuration.SnackbarConfiguration.VisibleStateDuration =1000;
    configuration.SnackbarConfiguration.HideTransitionDuration = 500;
    configuration.SnackbarConfiguration.ShowTransitionDuration = 50;
    configuration.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});

await builder.Build().RunAsync();
