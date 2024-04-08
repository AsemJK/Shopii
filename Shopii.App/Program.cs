using Shopii.App.Application.Database;
using Shopii.App.Application.Startup;
using Spark.Library.Config;
using Spark.Library.Environment;
using Vite.AspNetCore.Extensions;

EnvManager.LoadConfig();

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetupSparkConfig();

// Add all services to the container.
builder.Services.AddAppServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    // Do something only in dev environments
    app.UseViteDevMiddleware();
}
using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<ShopiDbContext>();
await context.Database.EnsureCreatedAsync();


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Services.RegisterScheduledJobs();
app.Services.RegisterEvents();

app.Run();
