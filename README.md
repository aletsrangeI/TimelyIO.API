# TimelyIO.API - Backend Service

Backend principal de **TimelyIO** desarrollado bajo Clean Architecture en **.NET 10** con **PostgreSQL**, enfocado en gestión de citas médicas, profesionales, servicios, clientes y expedientes.

---

## Características Técnicas
* **Runtime:** .NET 10 (`<TargetFramework>net10.0</TargetFramework>`).
* **Arquitectura:** Clean Architecture (`Domain`, `UseCases`, `Persistence`, `Interface`, `WebApi`, `Logging`, `Common`).
* **Mapeo:** `Riok.Mapperly` (generador de código en compilación, cero reflexión, alto rendimiento).
* **Base de Datos:** PostgreSQL con Entity Framework Core 9 / Npgsql.
* **Documentación Interactiva:** Scalar API Reference (`/scalar/v1`) y OpenAPI (`/openapi/v1.json`).
* **Scheduler:** Coravel.
* **Seguridad:** JWT Bearer Authentication, Sanitización estricta de repositorios y soporte para User Secrets e Infisical.

---

## Configuración de Entorno y Secretos (Local & Producción)

El repositorio mantiene `appsettings.json` sanitizado sin contraseñas ni llaves privadas.

### Desarrollo Local (`dotnet user-secrets`)
El proyecto `WebApi.csproj` tiene configurado el almacén de secretos:
```powershell
cd WebApi

# Configurar conexión a PostgreSQL
dotnet user-secrets set "ConnectionStrings:Timely_IO" "Server=100.110.215.58;Port=5432;Database=timely_io_dev;User Id=orionsys;Password=TU_PASSWORD;"

# Configurar claves JWT
dotnet user-secrets set "Config:Secret" "TU_JWT_SECRET_KEY"
dotnet user-secrets set "Config:Issuer" "OrionSys"
dotnet user-secrets set "Config:Audience" "OrionSys"
```

### Homelab / Producción (Docker + Infisical)
En el servidor Homelab (`100.110.215.58`), los secretos se gestionan centralizadamente en el panel de **Infisical** (`:8088`), se exportan a `/opt/timelyio/.env` (`chmod 600`) mediante el script `/opt/timelyio/export-secrets.sh`, y Docker Compose los inyecta en el contenedor de la API como variables de entorno (`ConnectionStrings__Timely_IO`, `Config__Secret`).

Para la guía detallada de SecretOps, consulta la [Documentación de Gestión de Secretos](file:///c:/OrionSys/TimelyIO/docs/SECRETS_MANAGEMENT.md).

---

## Comandos Útiles

```powershell
# Compilar proyecto
dotnet build WebApi/WebApi.csproj

# Ejecutar API en modo desarrollo
dotnet run --project WebApi/WebApi.csproj
```
