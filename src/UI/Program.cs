using Domain.Services.Intefaces.Account;
using Domain.Services.Intefaces.Adress.Interfaces;
using Domain.Services.Intefaces.Authentication;
using Domain.Services.Intefaces.Company;
using Domain.Services.Intefaces.Person;
using Domain.Services.Intefaces.Roles;
using Infraestructure.Services.Account;
using Infraestructure.Services.Adress;
using Infraestructure.Services.Authentication;
using Infraestructure.Services.Company;
using Infraestructure.Services.Person;
using Infraestructure.Services.Roles;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using UI;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<TokenAuthenticationProvider>();

builder.Services.AddScoped<IAuthorizeServices, TokenAuthenticationProvider>(
    provider => provider.GetRequiredService<TokenAuthenticationProvider>()
);

string baseAddress = builder.Configuration.GetSection("API:BaseUrl").Value ?? string.Empty;

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });
builder.Services.AddScoped<AuthenticationStateProvider, TokenAuthenticationProvider>(
    provider => provider.GetRequiredService<TokenAuthenticationProvider>());

builder.Services.AddHttpClient<IAccountServices, AccountServices>(x =>
{
    x.BaseAddress = new Uri(baseAddress);
    x.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHttpClient<IAdressLocatorServices, AdressLocatorServices>(x =>
{
    x.BaseAddress = new Uri(baseAddress);
    x.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHttpClient<IPersonServices, PersonServices>(x =>
{
    x.BaseAddress = new Uri(baseAddress);
    x.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHttpClient<IRolesServices, RolesServices>(x =>
{
    x.BaseAddress = new Uri(baseAddress);
    x.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHttpClient<ICompanyServices, CompanyServices>(x =>
{
    x.BaseAddress = new Uri(baseAddress);
    x.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddOptions();

builder.Services.AddAuthorizationCore();
await builder.Build().RunAsync();
