using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Timers;
using WatchDogApp.Data;
using WatchDogApp.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<DBService>();

builder.Services.AddDbContext<WatchDogContext>(option => option.UseSqlServer("workstation id=WatchDog.mssql.somee.com;packet size=4096;user id=radigurev_SQLLogin_1;pwd=zakvgr8lli;data source=WatchDog.mssql.somee.com;persist security info=False;initial catalog=WatchDog;MultipleActiveResultSets=True;"));
DBService dbService = new DBService();



var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<WatchDogContext>();
    dbContext.Database.Migrate();
}




var timer = new System.Timers.Timer()
{
    Interval = 1 * 60 * 1000
};

timer.Elapsed += Timer_Elapsed;

void Timer_Elapsed(object? sender, ElapsedEventArgs e) 
{
    dbService.CheckWebsite();
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
