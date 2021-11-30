# Movies
Web API REST de películas implementando la plantilla de [Clean Architecture](https://github.com/jasontaylordev/CleanArchitecture) de [Jason Taylor](https://github.com/jasontaylordev).


## Tecnologías
* ASP.NET Core Web API 5
* Entity Framework Core 5
* MediatR
* AutoMapper
* FluentValidation
* JWT, Identity Entity Framework Core 5
* NSwag
* SQL Server
* Azure


## DER
<img src="https://user-images.githubusercontent.com/66186644/144080567-c99398c5-5fd2-48a2-a242-1e97bddf1b30.png"/>


## Instrucciones

### Configuración del ConnectionStrings para Azure y SQL Server
* **Azure**: Crear una cuenta de almacenamiento, dirigirse a _Claves de acceso_, copiar la _cadena de conexión_ de la key1 y pegarla en el .json.
* **SQL Server**: La conexión por defecto es la que se muestra en el .json. La bases de datos utilizada es [SQL Server Express LocalDB](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver15).
```json
"ConnectionStrings": {
  "SQLServerConnection": "Server=(localdb)\\MSSQLLocalDB;Database=Movies;Trusted_Connection=True;",
  "AzureStorage": ""
}
```

### Migración y ejecución
* Ejecutar en la terminal:
  * `dotnet ef migrations add "Initial" --project Infrastructure -o Persistence/Migrations --startup-project WebAPI`
  * `dotnet ef database update --project Infrastructure --startup-project WebAPI`
* Iniciar el proyecto desde **WebAPI**


## Descripción

### Web API

#### Movies
* [GetAll] : Retorna una lista paginada de películas. Se pueden filtrar por _título_, _género_, _persona_.

* [GetById] : Retorna los detalles de la película: _título_, _lanzamiento_, _duración_, _calificación_, _resumen_, _imagen_, _géneros_, _cast_.

* [Create] & [Update] : Para crear o actualizar una película, se debe hacer por medio de Postman, Insomnia u otro cliente HTTP. Ya que Swagger no soporta las listas compuestas por parámetro a través de [FromForm]. Para el campo de _genres_ se debe pasar una lista de enteros que representa los id's de los géneros existentes. Para el campo _persons_ se debe pasar una lista de objetos compuesta por _personId_, _role_, _order_; _role_ representa el rol que cumple la persona en la película (director/a: 1, cast: 2); _order_ representa el orden en que se deben mostrar los actores para que al principio de la lista aparezcan los principales.
```json
[3, 4, 5, 6]
```
```json
[
  {"personId": 8, "role": 1, "order": 1}, 
  {"personId": 7, "role": 2, "order": 1}, 
  {"personId": 6, "role": 2, "order": 2},
  {"personId": 5, "role": 2, "order": 3},
  {"personId": 4, "role": 2, "order": 4},
  {"personId": 3, "role": 2, "order": 5},
  {"personId": 2, "role": 2, "order": 6},
  {"personId": 1, "role": 2, "order": 7},
  {"personId": 18, "role": 2, "order": 8},
  {"personId": 19, "role": 2, "order": 9}
]
```

#### Persons
> _**GetAll**_ : Paginación y filtrado por nombre
<img src="https://user-images.githubusercontent.com/66186644/143806277-3490bd3f-98e1-4482-bb3c-94aa43843782.png"/>

> _**GetById**_ : Persona y películas en las que participo
<img src="https://user-images.githubusercontent.com/66186644/143807476-5551ddd1-2785-4970-9d7c-a3e262680d9c.png"/>


## Observaciones
* Faltan las pruebas unitarias y de integración.
* Hay varias cosas por mejorar y optimizar pero la API es totalmente funcional.
