## Preparación de entorno de desarrollo

### Requerimientos

- PosgreSQL versión 16
- dotNET Framework versión 7


### Actualizar modelo de base de datos
```
dotnet ef dbcontext scaffold $env:ReservasHotelDb Npgsql.EntityFrameworkCore.PostgreSQL -c Database -o Models -f --no-pluralize
```
### Variables de entorno

- `ReservasHotelDb` - Define la conexión con la base de datos, debe tener 
el siguiente formato: 
`Host=localhost;Database=ReservasHotel;Username=postgres;Password=****`.