using WatchDog.Service;
using System.Timers;
using watchDog.Entity;

HttpClient client = new HttpClient();

var timer = new System.Timers.Timer()
{
    Interval = 1 * 60 * 1000
};
List<DomainClass> domains = new List<DomainClass> { new DomainClass("https://smcon.com/",false), new DomainClass("https://outlook.live.com",false),new DomainClass("http://proekt7.eu",false) };

EmailService email = new EmailService();

timer.Elapsed += Timer_Elapsed;


//$"{domain} is up again!", $"{domain} is up. \r\n {response}");
//$"{domain} is down!", $"{domain} is not responding. \r\n HttpRequest: {response}");

async void Timer_Elapsed(object? sender, ElapsedEventArgs e)
{
    domains.ForEach(async d =>
    {
        var response = await client.GetAsync(d.domain);

        if (!response.IsSuccessStatusCode)
        {
            if (!d.isWebsiteDown)
            {
                d.isWebsiteDown = true;
                email.sendEmail(d.domain, response,$"{d.domain} is down!", $"{d.domain} is not responding. \r\n HttpRequest: {response}");
            }
        }
        else
        {
            if (d.isWebsiteDown)
            {
                d.isWebsiteDown = false;
                email.sendEmail(d.domain, response, $"{d.domain} is up again!", $"{d.domain} is up. \r\n {response}");
            }
        }
    });
}
timer.Start();
await Task.Delay(-1);