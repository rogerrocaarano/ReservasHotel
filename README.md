# Sistema web para la realización y administración de reservas y registro de ingreso/salida de huéspedes para hoteles

Se desarrollará un sistema web, que se basará en los procesos más comunes que puede requerir un establecimiento de la industria del hospedaje y hotelería; como ser la realización y gestión de reservas, registro de clientes y huéspedes, registro de check-in y check-out de huéspedes y todos los aspectos relacionados con el cobro de estas actividades. 

## Preparación de entorno de desarrollo

### Requerimientos

- PosgreSQL versión 16
- dotNET Framework versión 7

### Variables de entorno

- `ReservasHotelDb` - Define la conexión con la base de datos, debe tener

```powershell
setx ReservasHotelDb "Host=localhost;Database=ReservasHotel;Username=postgres;Password=****"
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