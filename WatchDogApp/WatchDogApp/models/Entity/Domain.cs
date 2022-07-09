namespace WatchDogApp.models.Entity
{
    public class Domain
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDown { get; set; }
        public ICollection<History> history { get; set; }
    }
}
