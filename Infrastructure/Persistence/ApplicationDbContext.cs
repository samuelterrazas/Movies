using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Movies.Domain.Entities;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Movies.Application.Common.Interfaces;
using Movies.Domain.Common;
using Movies.Infrastructure.Identity;

namespace Movies.Infrastructure.Persistence;

 public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<MoviePerson> MoviePersons { get; set; }
        
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

            builder.Entity<ApplicationUser>()
                .Ignore(u => u.PhoneNumber)
                .Ignore(u => u.PhoneNumberConfirmed)
                .Ignore(u => u.TwoFactorEnabled)
                .Ignore(u => u.LockoutEnd)
                .Ignore(u => u.LockoutEnabled)
                .Ignore(u => u.AccessFailedCount);

            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
        }
    }
