using Microsoft.EntityFrameworkCore;

namespace Longbow.Tasks.EFCore
{
    public class SchedulerDBContext : DbContext
    {
        public SchedulerDBContext(DbContextOptions<SchedulerDBContext> options) : base(options)
        {
        }

        public DbSet<Scheduler> Schedulers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new TypeEntityConfigure());
        }
    }
}