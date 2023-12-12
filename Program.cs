using api.Data;
using Microsoft.EntityFrameworkCore;
using api.SignalHub;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
        policy =>
        {
            policy
            .WithOrigins("http://127.0.0.1:5173")
            .AllowAnyHeader()
            .WithMethods("GET", "POST")
            .AllowCredentials();
        });
}

);
// Custom services
builder.Services.AddSingleton<PresenceTracker>();
builder.Services.AddSingleton<GroupPresenceTracker>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<MessageRepository>();
builder.Services.AddScoped<GroupRepository>();

var app = builder.Build();

// Middlewares
app.UseCors("AllowAll");
app.MapControllers();

// Routes
app.MapGet("/", () => "Hello World!");

// Hubs
app.MapHub<ChatHub>("/chatHub");
app.MapHub<ConnectionHub>("/connectionHub");
app.MapHub<GroupHub>("/groupHub");

// Data seed
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedUsers(context);
}
catch (Exception ex)
{
    var logger = services.GetService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
}

app.Run();
