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
* Iniciar el proyecto desde **WebAPI**.


## Observaciones

* Para crear o actualizar una película, se debe hacer por medio de Postman, Insomnia u otro cliente HTTP. Ya que Swagger no soporta las listas compuestas por parámetro a través de [FromForm].
* Para el campo de _**genres**_ se debe pasar una lista de enteros que representa los id's de los géneros existentes.
```json
[3, 4, 5, 6]
```
* Para el campo _**persons**_ se debe pasar una lista de objetos compuesta:
    * _personId_: id de una persona existente.
    * _role_: director (1), cast (2).
    * _order_: para ordenar el cast.
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
* Faltan las pruebas unitarias y de integración.
* Hay varias cosas por mejorar y optimizar pero la API es totalmente funcional.
