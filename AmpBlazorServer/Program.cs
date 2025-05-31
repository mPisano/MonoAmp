using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using AmpBlazorServer.Data;
using AmpBlazorServer.Services; // Added for AmpControlService
using AmpBlazorServer.Hubs;   // Added for AmpControlHub
using System; // For Console.WriteLine and Exception

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<IAmpControlService, AmpControlService>(); // Added AmpControlService
builder.Services.AddSignalR(); // Added SignalR

var app = builder.Build();

// Initialize AmpControlService
// It's important this is after app.Build() so that all services are registered,
// and before app.Run() if it needs to run before request processing starts.
var ampService = app.Services.GetRequiredService<IAmpControlService>();
try
{
    // Example values - adjust COM port as necessary for your environment.
    // TODO: Make COM port and other settings configurable (e.g., via appsettings.json).
    ampService.InitializeAmp(
        comPort: "COM3",
        units: 1,
        sources: new string[] { "Source 1", "Source 2", "Source 3", "Source 4", "Source 5", "Source 6" },
        polledWait: false,
        pollMs: 1000,
        queueDupeElimination: true
    );
    Console.WriteLine("AmpControlService initialized successfully via Program.cs.");
}
catch (Exception ex)
{
    Console.WriteLine($"Error initializing AmpControlService in Program.cs: {ex.Message}");
    // Decide if the app should fail to start if Amp initialization fails.
    // For now, it logs and continues. Consider re-throwing if critical.
}

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
app.MapHub<AmpControlHub>("/ampcontrolhub"); // Added Hub mapping
app.MapFallbackToPage("/_Host");

app.Run();
