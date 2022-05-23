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
            await userManager.CreateAsync(admin, "Admin123.");
            await userManager.AddToRolesAsync(admin, new[] {adminRole.Name});
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
            Genre superHeroes = new() {Name = "Superhéroes"};
            Genre scienceFiction = new() {Name = "Ciencia ficción"};
            Genre adventure = new() {Name = "Aventura"};

            #region Movie: Inglorious Bastards
            Movie ingloriousBastards = new()
            {
                Title = "Bastardos sin gloria",
                Release = 2009,
                Duration = "02:34",
                MaturityRating = "18+",
                Summary = "Es el primer año de la ocupación alemana de Francia. El oficial aliado, teniente Aldo Raine, ensambla un equipo de " +
                          "soldados judíos para cometer actos violentos en contra de los nazis, incluyendo la toma de cabelleras. Él y sus hombres unen " +
                          "fuerzas con Bridget von Hammersmark, una actriz alemana y agente encubierto, para derrocar a los líderes del Tercer Reich. Sus destinos " +
                          "convergen con la dueña de teatro Shosanna Dreyfus, quien busca vengar la ejecución de su familia.",
                Teaser = ""
            };

            await dbContext.MovieGenres.AddRangeAsync(
                new MovieGenre
                {
                    Genre = warlike,
                    Movie = ingloriousBastards
                },
                new MovieGenre
                {
                    Genre = action,
                    Movie = ingloriousBastards
                },
                new MovieGenre
                {
                    Genre = drama,
                    Movie = ingloriousBastards
                },
                new MovieGenre
                {
                    Genre = blackHumor,
                    Movie = ingloriousBastards
                }
            );

            await dbContext.MoviePersons.AddRangeAsync(
                new MoviePerson
                {
                    Movie = ingloriousBastards,
                    Person = new Person {FullName = "Quentin Tarantino"},
                    Role = (byte)Role.Director,
                    Order = 1
                },
                new MoviePerson
                {
                    Movie = ingloriousBastards,
                    Person = new Person {FullName = "Brad Pitt"},
                    Role = (byte)Role.Cast,
                    Order = 1
                },
                new MoviePerson
                {
                    Movie = ingloriousBastards,
                    Person = new Person {FullName = "Christoph Waltz"},
                    Role = (byte)Role.Cast,
                    Order = 2
                },
                new MoviePerson
                {
                    Movie = ingloriousBastards,
                    Person = new Person {FullName = "Michael Fassbender"},
                    Role = (byte)Role.Cast,
                    Order = 3
                },
                new MoviePerson
                {
                    Movie = ingloriousBastards,
                    Person = new Person {FullName = "Eli Roth"},
                    Role = (byte)Role.Cast,
                    Order = 4
                },
                new MoviePerson
                {
                    Movie = ingloriousBastards,
                    Person = new Person {FullName = "Diane Kruger"},
                    Role = (byte)Role.Cast,
                    Order = 5
                },
                new MoviePerson
                {
                    Movie = ingloriousBastards,
                    Person = new Person {FullName = "Daniel Brühl"},
                    Role = (byte)Role.Cast,
                    Order = 6
                },
                new MoviePerson
                {
                    Movie = ingloriousBastards,
                    Person = new Person {FullName = "Til Schweiger"},
                    Role = (byte)Role.Cast,
                    Order = 7
                },
                new MoviePerson
                {
                    Movie = ingloriousBastards,
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
                          "riqueza, pero sus métodos no son del todo legales.",
                Teaser = $"{Links.YouTube}iszwuX1AK6A"
            };

            await dbContext.MovieGenres.AddRangeAsync(
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

            await dbContext.MoviePersons.AddRangeAsync(
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

            #region Movie: Spider-Man: No Way Home
            Movie spiderMan = new()
            {
                Title = "Spider-Man: No Way Home",
                Release = 2022,
                Duration = "2h 28m",
                MaturityRating = "13+",
                Summary = "Después de que la identidad de Peter Parker como Spider-Man es expuesta por Mysterio al final de Spider-Man: Lejos de casa, la vida y la reputación de Parker son puestas patas arriba. " +
                          "Para arreglar este asunto, Peter decide contactar al Dr. Stephen Strange para que este lo ayude a restaurar su antigua identidad secreta con magia, pero a raíz de esto, algo sale terriblemente mal " +
                          "en el encantamiento y provoca una fractura en el multiverso, causando que cinco supervillanos de otras realidades alternas (que previamente han luchado contra un Spider-Man en sus respectivas dimensiones) ingresen a su universo.",
                Teaser = $"{Links.YouTube}JfVOs4VSpmA"
            };
            
            await dbContext.MovieGenres.AddRangeAsync(
                new MovieGenre
                {
                    Genre = superHeroes,
                    Movie = spiderMan
                },
                new MovieGenre
                {
                    Genre = action,
                    Movie = spiderMan
                },
                new MovieGenre
                {
                    Genre = scienceFiction,
                    Movie = spiderMan
                },
                new MovieGenre
                {
                    Genre = adventure,
                    Movie = spiderMan
                }
            );
            
            await dbContext.MoviePersons.AddRangeAsync(
                new MoviePerson
                {
                    Movie = spiderMan,
                    Person = new Person {FullName = "Jon Watts"},
                    Role = (byte)Role.Director,
                    Order = 1
                },
                new MoviePerson
                {
                    Movie = spiderMan,
                    Person = new Person {FullName = "Tom Holland"},
                    Role = (byte)Role.Cast,
                    Order = 1
                },
                new MoviePerson
                {
                    Movie = spiderMan,
                    Person = new Person {FullName = "Zendaya"},
                    Role = (byte)Role.Cast,
                    Order = 2
                },
                new MoviePerson
                {
                    Movie = spiderMan,
                    Person = new Person {FullName = "Jacob Batalon"},
                    Role = (byte)Role.Cast,
                    Order = 3
                },
                new MoviePerson
                {
                    Movie = spiderMan,
                    Person = new Person {FullName = "Benedict Cumberbatch"},
                    Role = (byte)Role.Cast,
                    Order = 4
                },
                new MoviePerson
                {
                    Movie = spiderMan,
                    Person = new Person {FullName = "Jon Favreau"},
                    Role = (byte)Role.Cast,
                    Order = 5
                },
                new MoviePerson
                {
                    Movie = spiderMan,
                    Person = new Person {FullName = "Jamie Foxx"},
                    Role = (byte)Role.Cast,
                    Order = 6
                },
                new MoviePerson
                {
                    Movie = spiderMan,
                    Person = new Person {FullName = "Willem Dafoe"},
                    Role = (byte)Role.Cast,
                    Order = 7
                },
                new MoviePerson
                {
                    Movie = spiderMan,
                    Person = new Person {FullName = "Alfred Molina"},
                    Role = (byte)Role.Cast,
                    Order = 8
                },
                new MoviePerson
                {
                    Movie = spiderMan,
                    Person = new Person {FullName = "Marisa Tomei"},
                    Role = (byte)Role.Cast,
                    Order = 9
                },
                new MoviePerson
                {
                    Movie = spiderMan,
                    Person = new Person {FullName = "Andrew Garfield"},
                    Role = (byte)Role.Cast,
                    Order = 10
                },
                new MoviePerson
                {
                    Movie = spiderMan,
                    Person = new Person {FullName = "Tobey Maguire"},
                    Role = (byte)Role.Cast,
                    Order = 11
                }
            );
            #endregion

            await dbContext.SaveChangesAsync();
        }
    }
}
