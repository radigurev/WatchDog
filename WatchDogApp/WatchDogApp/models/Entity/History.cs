using System.ComponentModel.DataAnnotations;

namespace WatchDogApp.models.Entity
{
    public class History
    {
        public int Id { get; set; }
        public bool isDown { get; set; }
        public DateTime date { get; set; }
        public int DomainId { get; set; }
        public Domain DomainName { get; set; }

    }
}
