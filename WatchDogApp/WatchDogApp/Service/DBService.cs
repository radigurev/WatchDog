using WatchDogApp.Data;
using WatchDogApp.models.Entity;
using WatchDogApp.models.View;

namespace WatchDogApp.Service
{
    public class DBService
    {
       static  WatchDogContext context = new WatchDogContext();
        

        public List<TableViewModel> getAllRecords() 
        {
            List<TableViewModel> responses = new List<TableViewModel>();
            
            foreach(var record in context.histories) {

                TableViewModel model = new TableViewModel()
                {
                    Name = context.domains.Find(record.DomainId).Name,
                    Date = record.date,
                    Status = record.isDown ? "Up" : "Down"
                };
                responses.Add(model);
            }
            responses.Reverse();

            return responses;
        }

        public List<Domain> getAllDomains() 
        {
            return context.domains.ToList();
        }

        public void CheckWebsite() 
        {
            List<Domain> domains = getAllDomains();

            domains.ForEach(async d =>
            {
                HttpClient client = new HttpClient();

                try 
                {
                    var response = await client.GetAsync(d.Name);
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(response.StatusCode.ToString());
                        if (!d.IsDown)
                        {
                            d.IsDown = true;
                            addRecord(d, false);
                        }
                    }
                    else 
                    {
                        if (d.IsDown) 
                        {
                            d.IsDown = false;
                            addRecord(d, true);
                        }
                    }
                } 
                catch(Exception e)
                {
                    Console.WriteLine();
                    if (!d.IsDown) 
                    {
                        d.IsDown= true;
                        addRecord(d, false);
                    }
                }
                context.SaveChangesAsync();
            });
        }

        public void addRecord(Domain domain,bool status) 
        {
            History history = new History()
            {
                date=DateTime.Now,
                DomainId = domain.Id,
                isDown = status
            };
            context.Add(history);
        }

        public void addDomain(string DomainName) {
            Domain domain = new Domain()
            {
                Name=DomainName,
                IsDown=false
            };
            context.Add(domain);
            context.SaveChanges();

        }

        public void deleteDomain(int id) 
        {

            context.histories.RemoveRange(context.histories.Where(x => x.DomainId == id));
            
            context.domains.Remove(context.domains.Find(id));
            context.SaveChanges();
        }
    }
}
