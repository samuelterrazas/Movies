using Microsoft.AspNetCore.Identity;
using Movies.Domain.Entities;
using Movies.Domain.Enums;
using Movies.Infrastructure.Identity;

namespace Movies.Infrastructure.Persistence;

public static class ApplicationDbContextSeed
{
    public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        // Roles
        var adminRole = new IdentityRole("Administrator");
        var userRole = new IdentityRole("User");

        if (roleManager.Roles.All(r => r.Name != adminRole.Name))
            await roleManager.CreateAsync(adminRole);

        if (roleManager.Roles.All(r => r.Name != userRole.Name))
            await roleManager.CreateAsync(userRole);
        
        // Administrator
        var admin = new ApplicationUser {UserName = "administrator@localhost", Email = "administrator@localhost"};

        if (userManager.Users.All(u => u.UserName != admin.UserName))
        {
            await userManager.CreateAsync(admin, "Abc123.");
            await userManager.AddToRoleAsync(admin, adminRole.Name);
        }
    }

    public static async Task SeedSampleDataAsync(ApplicationDbContext dbContext)
    {
        if(!dbContext.Movies.Any())
        {
            Genre warlike = new() {Name = "Bélico"};
            Genre action = new() {Name = "Acción"};
            Genre drama = new() {Name = "Drama"};
            Genre blackHumor = new() {Name = "Humor negro"};
            Genre comedy = new() {Name = "Comedia"};
            Genre biographical = new() {Name = "Biográfico"};

            #region Movie: Inglourious Basterds
            Movie inglouriousBasterds = new()
            {
                Title = "Bastardos sin gloria",
                Release = 2009,
                Duration = "02:34",
                MaturityRating = "18+",
                Summary = "Es el primer año de la ocupación alemana de Francia. El oficial aliado, teniente Aldo Raine, ensambla un equipo de " +
                "soldados judíos para cometer actos violentos en contra de los nazis, incluyendo la toma de cabelleras. Él y sus hombres unen " +
                "fuerzas con Bridget von Hammersmark, una actriz alemana y agente encubierto, para derrocar a los líderes del Tercer Reich. Sus destinos " +
                "convergen con la dueña de teatro Shosanna Dreyfus, quien busca vengar la ejecución de su familia."
            };

            dbContext.MovieGenres.AddRange(
                new MovieGenre
                {
                    Genre = warlike,
                    Movie = inglouriousBasterds
                },
                new MovieGenre
                {
                    Genre = action,
                    Movie = inglouriousBasterds
                },
                new MovieGenre
                {
                    Genre = drama,
                    Movie = inglouriousBasterds
                },
                new MovieGenre
                {
                    Genre = blackHumor,
                    Movie = inglouriousBasterds
                }
            );

            dbContext.MoviePersons.AddRange(
                new MoviePerson
                {
                    Movie = inglouriousBasterds,
                    Person = new Person {FullName = "Quentin Tarantino"},
                    Role = (byte)Role.Director,
                    Order = 1
                },
                new MoviePerson
                {
                    Movie = inglouriousBasterds,
                    Person = new Person {FullName = "Brad Pitt"},
                    Role = (byte)Role.Cast,
                    Order = 1
                },
                new MoviePerson
                {
                    Movie = inglouriousBasterds,
                    Person = new Person {FullName = "Christoph Waltz"},
                    Role = (byte)Role.Cast,
                    Order = 2
                },
                new MoviePerson
                {
                    Movie = inglouriousBasterds,
                    Person = new Person {FullName = "Michael Fassbender"},
                    Role = (byte)Role.Cast,
                    Order = 3
                },
                new MoviePerson
                {
                    Movie = inglouriousBasterds,
                    Person = new Person {FullName = "Eli Roth"},
                    Role = (byte)Role.Cast,
                    Order = 4
                },
                new MoviePerson
                {
                    Movie = inglouriousBasterds,
                    Person = new Person {FullName = "Diane Kruger"},
                    Role = (byte)Role.Cast,
                    Order = 5
                },
                new MoviePerson
                {
                    Movie = inglouriousBasterds,
                    Person = new Person {FullName = "Daniel Brühl"},
                    Role = (byte)Role.Cast,
                    Order = 6
                },
                new MoviePerson
                {
                    Movie = inglouriousBasterds,
                    Person = new Person {FullName = "Til Schweiger"},
                    Role = (byte)Role.Cast,
                    Order = 7
                },
                new MoviePerson
                {
                    Movie = inglouriousBasterds,
                    Person = new Person {FullName = "Mélanie Laurent"},
                    Role = (byte)Role.Cast,
                    Order = 8
                }
            );
            #endregion
            
            #region Movie: The Wolf of Wall Street
            Movie theWolfOfWallStreet = new()
            {
                Title = "El lobo de Wall Street",
                Release = 2013,
                Duration = "03:00",
                MaturityRating = "18+",
                Summary = "Jordan Belfort es un ambicioso corredor de bolsa que asciende a niveles enormes de " +
                "riqueza, pero sus métodos no son del todo legales."
            };

            dbContext.MovieGenres.AddRange(
                new MovieGenre
                {
                    Genre = comedy,
                    Movie = theWolfOfWallStreet
                },
                new MovieGenre
                {
                    Genre = drama,
                    Movie = theWolfOfWallStreet
                },
                new MovieGenre
                {
                    Genre = blackHumor,
                    Movie = theWolfOfWallStreet
                },
                new MovieGenre
                {
                    Genre = biographical,
                    Movie = theWolfOfWallStreet
                }
            );

            dbContext.MoviePersons.AddRange(
                new MoviePerson
                {
                    Movie = theWolfOfWallStreet,
                    Person = new Person {FullName = "Martin Scorsese"},
                    Role = (byte)Role.Director,
                    Order = 1
                },
                new MoviePerson
                {
                    Movie = theWolfOfWallStreet,
                    Person = new Person {FullName = "Leonardo DiCaprio"},
                    Role = (byte)Role.Cast,
                    Order = 1
                },
                new MoviePerson
                {
                    Movie = theWolfOfWallStreet,
                    Person = new Person {FullName = "Jonah Hill"},
                    Role = (byte)Role.Cast,
                    Order = 2
                },
                new MoviePerson
                {
                    Movie = theWolfOfWallStreet,
                    Person = new Person {FullName = "Margot Robbie"},
                    Role = (byte)Role.Cast,
                    Order = 3
                },
                new MoviePerson
                {
                    Movie = theWolfOfWallStreet,
                    Person = new Person {FullName = "Matthew McConaughey"},
                    Role = (byte)Role.Cast,
                    Order = 4
                },
                new MoviePerson
                {
                    Movie = theWolfOfWallStreet,
                    Person = new Person {FullName = "Kyle Chandler"},
                    Role = (byte)Role.Cast,
                    Order = 5
                },
                new MoviePerson
                {
                    Movie = theWolfOfWallStreet,
                    Person = new Person {FullName = "Rob Reiner"},
                    Role = (byte)Role.Cast,
                    Order = 6
                },
                new MoviePerson
                {
                    Movie = theWolfOfWallStreet,
                    Person = new Person {FullName = "Jon Bernthal"},
                    Role = (byte)Role.Cast,
                    Order = 7
                },
                new MoviePerson
                {
                    Movie = theWolfOfWallStreet,
                    Person = new Person {FullName = "Jon Favreau"},
                    Role = (byte)Role.Cast,
                    Order = 8
                },
                new MoviePerson
                {
                    Movie = theWolfOfWallStreet,
                    Person = new Person {FullName = "Jean Dujardin"},
                    Role = (byte)Role.Cast,
                    Order = 9
                }
            );
            #endregion

            await dbContext.SaveChangesAsync();
        }
    }
}
