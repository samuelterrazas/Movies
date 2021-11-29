# Movies
Web API REST de películas implementando la plantilla de Clean Architecture de Jason Taylor: _https://github.com/jasontaylordev_.

## Tecnologías
* ASP.NET Core Web API 5
* Entity Framework Core 5
* MediatR
* AutoMapper
* FluentValidation
* NSwag
* JWT
* Identity Entity Framework Core
* Azure

## Instrucciones

### Configuración de Azure
* Crear una cuenta de almacenamiento
* Dirigirse a **Claves de acceso**
* Copiar la **Cadena de conexión** de la key1
* Pegarla en el **appsettings.json** > **"ConnectionStrings"** > **"AzureStorage"**


### Confifuración de SQL Server
* La conexión por defecto es **"Server=(localdb)\\MSSQLLocalDB;Database=Movies;Trusted_Connection=True;"**
* Si desea cambiarla, dirigirse a **appsettings.json** > **"ConnectionStrings"** > **"SQLServerConnection"**


### Migración y ejecución
* Ejecutar en la terminal:
  * `dotnet ef migrations add "Initial" --project Infrastructure -o Persistence/Migrations --startup-project WebAPI`
  * `dotnet ef database update --project OngProject.DataAccess --startup-project OngProject`
* Iniciar el proyecto desde **WebAPI**


### Web API
#### Movies
> _**GetAll**_ : Paginación y filtrado por título, género, director/a, actor/actriz 
<img src="https://user-images.githubusercontent.com/66186644/143806620-26bfaba8-e345-41d9-b4ab-dce05e44aa4a.png"/>

> _**GetById**_ : Detalles de la película, generos, director/a y cast.
<img src="https://user-images.githubusercontent.com/66186644/143934045-96450615-49ec-456f-8dd3-a5643e7e2822.png"/>

> _**Create**_ : Para crear una película, se debe hacer por medio de Postman, Insomnia u otro cliente HTTP. Ya que Swagger no soporta las listas compuestas por parámetro a través de [FromForm]. Ejemplo:
<img src="https://user-images.githubusercontent.com/66186644/143937486-ea7001b3-18c6-4348-a7bb-fa84a52650f2.png"/>

> _summary_ recibe un texto plano. _genres_ es una lista de id's de los generos existentes y _persons_ recibe una lista compuesta por: personId, role, order. El id de una persona existente, el rol que cumple en la película (director: 1, cast: 2) y el orden para que los actores principales vayan primero en la lista al mostrar el resultado.
<img src="https://user-images.githubusercontent.com/66186644/143938645-7c0a2a4a-1c52-42bb-a77d-411f518a4009.png"/>

#### Persons
> _**GetAll**_ : Paginación y filtrado por nombre
<img src="https://user-images.githubusercontent.com/66186644/143806277-3490bd3f-98e1-4482-bb3c-94aa43843782.png"/>

> _**GetById**_ : Persona y películas en las que participo
<img src="https://user-images.githubusercontent.com/66186644/143807476-5551ddd1-2785-4970-9d7c-a3e262680d9c.png"/>


## Observaciones
* Faltan las pruebas unitarias y de integración.
* Hay varias cosas por mejorar y optimizar pero la API es totalmente funcional.
