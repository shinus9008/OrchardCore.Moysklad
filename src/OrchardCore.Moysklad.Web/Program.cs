using OrchardCore.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostingContext, configBuilder) =>
    {
        configBuilder.ReadFrom.Configuration(hostingContext.Configuration)
        .Enrich.FromLogContext();
    });


var configuration = builder.Configuration;

builder.Services
    .AddSingleton(configuration)
    .AddOrchardCms(builder =>
    {
        if (!configuration.IsUITesting())
        {
            builder.AddSetupFeatures("OrchardCore.AutoSetup");
        }
    });
// // Orchard Specific Pipeline
// .ConfigureServices( services => {
// })
// .Configure( (app, routes, services) => {
// })
;

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseOrchardCore(c => c.UseSerilogTenantNameLogging());

app.Run();
