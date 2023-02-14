using Blazored.LocalStorage;
using ChatJaffApp.Client;
using ChatJaffApp.Client.Account.Contracts;
using ChatJaffApp.Client.Account.Services;
using ChatJaffApp.Client.ChatRoom.CreateChat.Contracts;
using ChatJaffApp.Client.ChatRoom.CreateChat.Services;
using ChatJaffApp.Client.Member.Contracts;
using ChatJaffApp.Client.Member.Services;
using ChatJaffApp.Client.ChatRoom.Messages.Contracts;
using ChatJaffApp.Client.ChatRoom.Messages.Services;
using ChatJaffApp.Client.ChatRoom.MyChatRooms.Contracts;
using ChatJaffApp.Client.ChatRoom.MyChatRooms.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<ICreateChatService, CreateChatService>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IChatRoomsService, ChatRoomsService>();
builder.Services.AddScoped<IMessageService, MessageService>();

builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CustomAuthStateProvider>());

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
