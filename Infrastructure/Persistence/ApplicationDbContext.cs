namespace Movies.Infrastructure.Persistence;

 public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        
        public DbSet<Genre> Genres => Set<Genre>();
        public DbSet<Movie> Movies => Set<Movie>();
        public DbSet<Person> Persons => Set<Person>();
        public DbSet<Image> Images => Set<Image>();
        public DbSet<MovieGenre> MovieGenres => Set<MovieGenre>();
        public DbSet<MoviePerson> MoviePersons => Set<MoviePerson>();
        
        
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DateTime>().HaveColumnType("SMALLDATETIME");
            configurationBuilder.Properties<string>().AreUnicode(false);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
            
            IdentityConfiguration(builder);
        }

        private static void IdentityConfiguration(ModelBuilder builder)
        {
            builder.Ignore<IdentityUserClaim<string>>();
            builder.Ignore<IdentityUserLogin<string>>();
            builder.Ignore<IdentityUserToken<string>>();
            builder.Ignore<IdentityRoleClaim<string>>();

            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable("Users");
                
                entity.Ignore(u => u.PhoneNumber);
                entity.Ignore(u => u.PhoneNumberConfirmed);
                entity.Ignore(u => u.TwoFactorEnabled);
                entity.Ignore(u => u.LockoutEnd);
                entity.Ignore(u => u.LockoutEnabled);
                entity.Ignore(u => u.AccessFailedCount);
            });
            
            builder.Entity<IdentityRole>().ToTable("Roles");

            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
        }
    }
