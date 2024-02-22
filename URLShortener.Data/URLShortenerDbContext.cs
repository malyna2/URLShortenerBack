using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using URLShortener.Data.Entities;

namespace URLShortener.Data
{
    public class URLShortenerDbContext: DbContext
    {
        private DbContextOptions<URLShortenerDbContext> _dbContextOptions;

        public URLShortenerDbContext()
        {

        }

        public URLShortenerDbContext(DbContextOptions<URLShortenerDbContext> options) : base(options)
        {
            _dbContextOptions = options;
        }

        public DbSet<User> User { get; set; }
        public DbSet<Link> Link { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=URLShortener;User Id=sa;Password=Admin@1234;MultipleActiveResultSets=true;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasMany(u => u.Links)
                .WithOne(l => l.User)
                .HasForeignKey(l => l.UserId);

            modelBuilder.Entity<Link>()
                .HasIndex(l => l.OriginalURL)
                .IsUnique();

        }
    }
}
//dotnet ef migrations add Links --project URLShortener.Data/URLShortener.Data.csproj