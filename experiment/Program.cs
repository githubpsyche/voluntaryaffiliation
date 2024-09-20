using ExperimentalGoal;
using Microsoft.EntityFrameworkCore;
using RazorPagesDemo.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddDbContext<DatabaseContext>(options =>
  options.UseSqlite(builder.Configuration.GetConnectionString("ExperimentalGoalContext")));

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

var app = builder.Build();

// Populate the database with players from the "characters" directory
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DatabaseContext>();

    var dbPopulator = new DatabasePopulator(context);
dbPopulator.PopulatePlayersFromImages(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../wwwroot/images/characters"));
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();