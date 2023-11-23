## Preparación de entorno de desarrollo

### 1. Requerimientos

- PosgreSQL versión 16
- dotNET Framework versión 7

### 2. Configuración de la base de datos

Crear dos bases de datos para el proyecto

```postgresql
-- Base de datos para la aplicación
create database "ReservasHotel";
-- Base de datos para la autenticación en la aplicación
create database "IdentityReservasHotel";
```

Definir las siguientes variables de entorno en el sistema operativo para las cadenas de conexión de las bases de datos:
- `ReservasHotelDb` - Define la conexión con la base de datos.
- `ReservasHotelIdentityDb` - Define la conexión con la base de datos de identidad (autenticación y roles).
- `ReservasHotelEmailAccount` - Define la cuenta de correo electrónico para el envío de correos electrónicos.

Ejemplo de configuración en Windows:
```cmd
setx ReservasHotelDb "Host=localhost;Database=ReservasHotel;Username=postgres;Password=****"
setx ReservasHotelIdentityDb "Host=localhost;Database=IdentiyReservasHotel;Username=postgres;Password=****"
setx ReservasHotelEmailAccount "{\"host\":\"mail.reinseg.com\",\"port\":\"25\",\"useSsl\":\"false\"
,\"username\":\"test@example.com\",\"password\":\"****\"}"
```

Cargar el script `./db/Schema.sql` para cargar las tablas de la base de datos del sistema.
### 3. Aplicar las migraciones de la base de datos de Identity

```powershell
cd SistemaHotel
dotnet ef database update -c IdentityDatabase
```
## Notas

### Actualizar modelo de base de datos
```powershell
dotnet ef dbcontext scaffold $env:ReservasHotelDb Npgsql.EntityFrameworkCore.PostgreSQL -c Database -o Models -f --no-pluralize
```

### Flujo de trabajo con git

- Rama `feature`: Rama de desarrollo de una característica del proyecto, crear una nueva rama feature para cada caso de uso del sistema.
- Rama `develop`: Rama donde se realiza el desarrollo del proyecto, para subir cambios a esta rama hay que hacer pull request desde una rama `feature`.
- Rama `release`: Esta rama sale de develop, al momento de realizarse un incremento en el proyecto.
- Rama `main`: rama protegida, solo subir cambios mediante pull request desde una rama `feature`.