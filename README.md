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
<img src="https://user-images.githubusercontent.com/66186644/144080567-c99398c5-5fd2-48a2-a242-1e97bddf1b30.png"/>


## Instrucciones
### Configuración del ConnectionStrings para Azure y SQL Server
* **SQLServerConnectionr**: Cadena de conexión a su base de datos SQL.
* **AzureStorage**: Crear una cuenta de almacenamiento, dirigirse a _Claves de acceso_, copiar la _cadena de conexión_ de la key1 y pegarla en el .json.
```json
"ConnectionStrings": {
  "SQLServerConnection": "",
  "AzureStorage": ""
}
```


### Migración y ejecución
* Ejecutar en la terminal:
  * `dotnet ef migrations add "Initial" --project Infrastructure -o Persistence/Migrations --startup-project WebAPI`
  * `dotnet ef database update --project Infrastructure --startup-project WebAPI`
* Iniciar el proyecto desde **WebAPI**.


## Observaciones
* Falta agregar documentación.
* Faltan las pruebas unitarias y de integración.
