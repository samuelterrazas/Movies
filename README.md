# Movies
Web API REST de películas implementando la plantilla de [Clean Architecture](https://github.com/jasontaylordev/CleanArchitecture) de [Jason Taylor](https://github.com/jasontaylordev).


## Tecnologías
* ASP.NET Core Web API 6
* Entity Framework Core 6
* MediatR
* FluentValidation
* JWT, Identity Entity Framework Core 6
* NSwag
* SQL Server
* Azure


## DER
<img src="https://user-images.githubusercontent.com/66186644/151639748-efb3e67f-bf75-470a-8553-570259e8a887.png"/>


## Instrucciones
### Configuración del ConnectionStrings para Azure y SQL Server
* **SQLServerConnectionr**: Cadena de conexión a su base de datos SQL.
* **AzureStorage**: Crear una cuenta de almacenamiento, dirigirse a _Claves de acceso_, copiar la _cadena de conexión_ de la key1 y pegarla en el .json.
> WebAPI: appsettings.Development.json
```json
"ConnectionStrings": {
  "SQLServerConnection": "",
  "AzureStorage": ""
}
```
> Application.IntegrationTests: appsettings.json
```json
"ConnectionStrings": {
  "SQLServerConnection": ""
}
```


### Migración y ejecución
* Ejecutar en la terminal:
  * `dotnet ef migrations add "First" --startup-project WebAPI --project Infrastructure --output-dir Persistence/Migrations`
  * `dotnet ef database update --startup-project WebAPI --project Infrastructure`
* Iniciar el proyecto desde **WebAPI**.


## Observaciones
* Falta agregar documentación.
* Faltan las pruebas unitarias y de integración.
