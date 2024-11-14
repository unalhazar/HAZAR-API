namespace Infrastructure.Persistence
{
    public class HazarDbContext : DbContext
    {
        public HazarDbContext(DbContextOptions<HazarDbContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<LogUser> LogUsers { get; set; }
        public virtual DbSet<GlobalLog> GlobalLogs { get; set; }
        public virtual DbSet<AppLog> AppLogs { get; set; }
        public virtual DbSet<StudentLifestyle> StudentLifestyles { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Brand>().ToTable("brands");
            modelBuilder.Entity<Category>().ToTable("categories");
            modelBuilder.Entity<Product>().ToTable("products");
            modelBuilder.Entity<StudentLifestyle>(entity =>
            {
                entity.ToTable("studentlifestyles");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.StudentId).HasColumnName("studentid");
                entity.Property(e => e.StudyHoursPerDay).HasColumnName("studyhoursperday");
                entity.Property(e => e.ExtracurricularHoursPerDay).HasColumnName("extracurricularhoursperday");
                entity.Property(e => e.SleepHoursPerDay).HasColumnName("sleephoursperday");
                entity.Property(e => e.SocialHoursPerDay).HasColumnName("socialhoursperday");
                entity.Property(e => e.PhysicalActivityHoursPerDay).HasColumnName("physicalactivityhoursperday");
                entity.Property(e => e.GPA).HasColumnName("gpa");
                entity.Property(e => e.StressLevel).HasColumnName("stresslevel");
            });
            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("movies");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.Title).HasColumnName("title");
                entity.Property(e => e.Type).HasColumnName("type");
                entity.Property(e => e.Genres).HasColumnName("genres");
                entity.Property(e => e.ReleaseYear).HasColumnName("releaseyear");
                entity.Property(e => e.ImdbId).HasColumnName("imdbid");
                entity.Property(e => e.ImdbAverageRating).HasColumnName("imdbaveragerating");
                entity.Property(e => e.ImdbNumVotes).HasColumnName("imdbnumvotes");
                entity.Property(e => e.AvailableCountries).HasColumnName("availablecountries");
            });
        }
    }
}
