# Movies
Web API REST de películas implementando la plantilla de [Clean Architecture](https://github.com/jasontaylordev/CleanArchitecture) de [Jason Taylor](https://github.com/jasontaylordev).


## Tecnologías
* ASP.NET Core Web API 6
* Entity Framework Core 6
* MediatR
* FluentValidation
* JWT & Identity Entity Framework Core 6
* SQL Server
* Azure
* NSwag
* NUnit, FluentAssertions, Moq & Respawn


## DER
<img src="https://user-images.githubusercontent.com/66186644/151639748-efb3e67f-bf75-470a-8553-570259e8a887.png"/>


## Instrucciones
### Configuraciones
* **SQLServerConnectionr**: Cadena de conexión a su base de datos SQL
> _WebAPI: appsettings.Development.json_
```json
"ConnectionStrings": {
  "SQLServerConnection": ""
}
```
> _Application.IntegrationTests: appsettings.json_
```json
"ConnectionStrings": {
  "SQLServerConnection": ""
}
```
* **AzureStorage**: Crear una cuenta de almacenamiento, dirigirse a _Claves de acceso_, copiar la _cadena de conexión_ de la key1 y pegarla en el .json
> _WebAPI: appsettings.json_
```json
"ConnectionStrings": {
  "AzureStorage": ""
}
```
* **LocalStorage**: En caso de elegir almacenar los archivos de manera local, cambiar la inyección del servicio
> _Infrastructure: ServiceExtensions.cs_
<img src="https://user-images.githubusercontent.com/66186644/169736045-b6b05765-e0b4-41c6-85cd-a56f21a4792c.png"/>


### Migración y ejecución
* Ejecutar en la terminal:
  * `dotnet ef migrations add "First" --startup-project WebAPI --project Infrastructure --output-dir Persistence/Migrations`
  * `dotnet ef database update --startup-project WebAPI --project Infrastructure`
* Iniciar el proyecto desde **WebAPI**
