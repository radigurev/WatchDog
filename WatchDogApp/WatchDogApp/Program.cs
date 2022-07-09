using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using System.Timers;
using WatchDogApp.Data;
using WatchDogApp.models.Entity;
using WatchDogApp.Service;

var builder = WebApplication.CreateBuilder(args);

DBService dbService = new DBService(); 
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<DBService>();

builder.Services.AddDbContext<WatchDogContext>(option => option.UseSqlServer("Server=.;Database=WatchDog;Trusted_Connection=True;"));

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<WatchDogContext>();
    dbContext.Database.Migrate();
}




var timer = new System.Timers.Timer()
{
    Interval = 1 * 10 * 1000
};

dbService.CheckWebsite();

void Timer_Elapsed(object? sender, ElapsedEventArgs e) 
{ 
  

}

timer.Start();





if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
