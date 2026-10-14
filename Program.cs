using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Mintakt.Client;
using Mintakt.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Registrera services
builder.Services.AddScoped<LocalStorageService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddMudServices();

// Registrera AuthorizationMessageHandler
builder.Services.AddTransient<AuthorizationMessageHandler>();

// Registrera HttpClient med vår handler
// Alla HTTP-anrop går nu genom handlern som:
// - Lägger på JWT-token automatiskt
// - Fångar 401 och redirectar till /login
builder.Services.AddScoped(sp =>
{
    var handler = sp.GetRequiredService<AuthorizationMessageHandler>();
    handler.InnerHandler = new HttpClientHandler();

    return new HttpClient(handler)
    {
        BaseAddress = new Uri("http://localhost:5137/")
    };
});

await builder.Build().RunAsync();