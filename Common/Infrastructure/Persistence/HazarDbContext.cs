namespace Infrastructure.Persistence
{
    public class HazarDbContext : DbContext
    {
        public HazarDbContext(DbContextOptions options) : base(options)
        {
        }


        public virtual DbSet<Brand> Brands { get; set; }







        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            //optionsBuilder.UseLazyLoadingProxies().ConfigureWarnings(warnings => warnings.Ignore(new EventId(1, "LazyLoadingException")));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
