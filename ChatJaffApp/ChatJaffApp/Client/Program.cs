using Blazored.LocalStorage;
using ChatJaffApp.Client;
using ChatJaffApp.Client.Account.Contracts;
using ChatJaffApp.Client.Account.Services;
using ChatJaffApp.Client.Chat.CreateChat.Contracts;
using ChatJaffApp.Client.Chat.CreateChat.Services;
using ChatJaffApp.Client.Chat.Member.Contracts;
using ChatJaffApp.Client.Chat.Member.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<ICreateChatService, CreateChatService>();
builder.Services.AddScoped<IMemberService, MemberService>();

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
