namespace Infrastructure.Persistence
{
    public class HazarDbContext : DbContext
    {
        public HazarDbContext(DbContextOptions<HazarDbContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        public virtual DbSet<ApplicationUser> Users { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<LogUser> LogUsers { get; set; }
        public virtual DbSet<GlobalLog> GlobalLogs { get; set; }
        public virtual DbSet<TokenBlacklist> TokenBlacklists { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.EnableSensitiveDataLogging();
            //optionsBuilder.UseLazyLoadingProxies().ConfigureWarnings(warnings => warnings.Ignore(new EventId(1, "LazyLoadingException")));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Brand>().ToTable("brands");
            modelBuilder.Entity<Category>().ToTable("categories");
            modelBuilder.Entity<Product>().ToTable("products");


            //// Product - Brand relationship
            //modelBuilder.Entity<Product>()
            //    .HasOne(p => p.Brand)
            //    .WithMany(b => b.Products)
            //    .HasForeignKey(p => p.BrandId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //// Product - Category relationship
            //modelBuilder.Entity<Product>()
            //    .HasOne(p => p.Category)
            //    .WithMany(c => c.Products)
            //    .HasForeignKey(p => p.CategoryId)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
