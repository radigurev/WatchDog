using WatchDog.Service;
using System.Timers;
using watchDog.Entity;

HttpClient client = new HttpClient();

var timer = new System.Timers.Timer()
{
    Interval = 10 * 60 * 1000
};
List<DomainClass> domains = new List<DomainClass> { new DomainClass("https://smcon.com/",false), new DomainClass("https://outlook.live.com",false) };

//,new DomainClass("http://proekt7.eu",false) test domain

EmailService email = new EmailService();

timer.Elapsed += Timer_Elapsed;


//$"{domain} is up again!", $"{domain} is up. \r\n {response}");
//$"{domain} is down!", $"{domain} is not responding. \r\n HttpRequest: {response}");

void Timer_Elapsed(object? sender, ElapsedEventArgs e)
{
    domains.ForEach(async d =>
    {
        var response = await client.GetAsync(d.Domain);

        if (!response.IsSuccessStatusCode)
        {
            if (!d.IsWebsiteDown)
            {
                d.IsWebsiteDown = true;
                email.SendEmail(d.Domain, response,$"{d.Domain} is down!", $"{d.Domain} is not responding. \r\n HttpRequest: {response}");
            }
        }
        else
        {
            if (d.IsWebsiteDown)
            {
                d.IsWebsiteDown = false;
                email.SendEmail(d.Domain, response, $"{d.Domain} is up again!", $"{d.Domain} is up. \r\n {response}");
            }
        }
    });
}

timer.Start();

await Task.Delay(-1);