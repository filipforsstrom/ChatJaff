using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using ChatJaffApp.Server.Identity.Data;
using ChatJaffApp.Server.Identity.Models;
using ChatJaffApp.Server.Extensions;
using ChatJaffApp.Client;
using Microsoft.OpenApi.Models;
using ChatJaffApp.Server.Data;
using ChatJaffApp.Server.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddServiceInjections();

builder.Services.AddDbContext<IdentityContext>(options => options.UseSqlite(
    builder.Configuration.GetConnectionString("IdentityDb")
    ));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{    
    options.SignIn.RequireConfirmedAccount = false;
    options.Lockout.MaxFailedAccessAttempts = 4;
    options.Lockout.AllowedForNewUsers = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<IdentityContext>();

builder.Services.ConfigureAuthentication(builder.Configuration);

builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.AddSecurityDefinition("AuthToken", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Input a valid token to access this API"
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "AuthToken" }
            }, new List<string>() }
    });
});
builder.Services.AddDbContext<JaffDbContext>(options => options.UseSqlite(
    builder.Configuration.GetConnectionString("JaffDb")));

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddSignalR();
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

var app = builder.Build();
app.UseResponseCompression();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blazor API V1");
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();



app.MapRazorPages();
app.MapControllers();
app.MapHub<ChatHub>("/chathub");
app.MapFallbackToFile("index.html");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<IdentityContext>();
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<JaffDbContext>();
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
}

app.Run();

public partial class Program
{

}
