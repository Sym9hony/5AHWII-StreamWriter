using StreamWriter.Web.Controllers;
using StreamWriter.Web;

// Create the application builder and load configuration from the command line and environment.
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register MVC controllers so the daily challenge API can receive HTTP requests.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// Register OpenAPI document generation for development-time API inspection.
builder.Services.AddOpenApi();

// Build the configured generator tree from the DailyChallenge settings section.
builder.Services.AddSingleton<IDailyChallengeTextGenerator>(serviceProvider =>
    DailyChallengeTextGeneratorFactory.Create(
        builder.Configuration.GetSection("DailyChallenge")));

// Build the configured web application.
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Expose the generated OpenAPI document only during development.
    app.MapOpenApi();
}

// Redirect HTTP requests to HTTPS.
app.UseHttpsRedirection();

// Apply authorization middleware to the request pipeline.
app.UseAuthorization();

// Map controller routes, including /daily-challenge/text.
app.MapControllers();

// Serve the default page and other files from wwwroot.
app.UseDefaultFiles();
app.UseStaticFiles();

// Start listening for incoming requests.
app.Run();
