using WatchDog.Service;
using System.Timers;

HttpClient client = new HttpClient();

var timer = new System.Timers.Timer()
{
    Interval = (int)(1 * 1 * 1000)
};


EmailService email = new EmailService();

email.emailForWebsiteDownIsSent = false;

timer.Elapsed += Timer_Elapsed;



async void Timer_Elapsed(object? sender, ElapsedEventArgs e)
{
    var response = await client.GetAsync("http://proekt7.eu");

    if (!response.IsSuccessStatusCode)
    {
        if (!email.emailForWebsiteDownIsSent)
        {
            Console.WriteLine("neba4ka");
            //email.SendEmailForDownServer();
            email.emailForWebsiteDownIsSent = true;
        }
    }
    else
    {
        if (email.emailForWebsiteDownIsSent)
        {
            Console.WriteLine("ba4ka");
            //email.SendEmailForDownServer();
            email.emailForWebsiteDownIsSent = false;
        }
    }

}
timer.Start();
await Task.Delay(-1);