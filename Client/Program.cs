global using Dziennik.Shared;
global using Dziennik.Shared.Mark;
global using Dziennik.Shared.User;
global using Dziennik.Shared.Subject;

global using Dziennik.Client.Service;
global using Dziennik.Client.Service.MarkService;
global using Dziennik.Client.Service.StudentService;
global using Dziennik.Client.Service.SubjectService;

global using System.Text;
global using System.Text.Json;
global using System.Net.Http.Json;

using Blazored.SessionStorage;
using Dziennik.Client;
using Dziennik.Client.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMarkService, MarkService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();


await builder.Build().RunAsync();
