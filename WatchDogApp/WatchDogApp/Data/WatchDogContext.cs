using Microsoft.EntityFrameworkCore;
using WatchDogApp.models.Entity;

namespace WatchDogApp.Data

{
    public class WatchDogContext : DbContext
    {
        public WatchDogContext(DbContextOptions options) : base(options)
        {
        }

        public WatchDogContext()
        {
        }

        public DbSet<Domain> domains { get; set; }
        public DbSet<History> histories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("workstation id=WatchDog.mssql.somee.com;packet size=4096;user id=radigurev_SQLLogin_1;pwd=zakvgr8lli;data source=WatchDog.mssql.somee.com;persist security info=False;initial catalog=WatchDog;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<History>()
                .HasOne(x => x.DomainName)
                .WithMany(x => x.history)
                .HasForeignKey(x => x.DomainId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
