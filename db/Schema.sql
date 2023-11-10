-- USUARIOS Y ROLES
drop table if exists "Roles","Usuarios","RolUsuario";

-- Llevar registro de los roles del sistema
create table "Roles"(
    "id" serial primary key not null,
    "rol" varchar(32)
);
-- Usuarios del sistema Web
create table "Usuarios"(
    "id" serial primary key not null,
    "habilitado" boolean default true,
    "usuario" varchar(64),
    "hashPassword" varchar(512),
    "salHash" varchar(64)
);
-- Registrar los roles asignados a los usuarios del sistema
create table "RolUsuario"(
    "idUsuario" serial references "Usuarios"(id) not null,
    "idRol" serial references "Roles"(id) not null
);

-- ENTIDADES DEL NEGOCIO
drop table if exists "Clientes","Huespedes";

-- Registrar a quienes realizan reservas en el hotel
create table "Clientes"(
    "id" serial primary key not null,
    "nombres" varchar(128) not null,
    "apellidos" varchar(128) not null,
    "razonSocial" varchar(128),
    "nroRazonSocial" varchar(64),
    "email" varchar(128)
);
-- Registrar los huéspedes del hotel
create table "Huespedes"(
    "id" serial primary key not null,
    "nombres" varchar(128) not null,
    "apellidos" varchar(128) not null,
    "docIdentidad" varchar(32) not null,
    "tipoDocIdentidad" varchar(16) not null,
    "nacionalidad" varchar(16) not null
);

drop table if exists "TipoHabitaciones","Habitaciones","PaquetesPromocionales";

create table "TipoHabitaciones"(
    "id" serial primary key not null,
    "nombre" varchar(128) not null,
    "descripcion" varchar(256),
    "huespedesPermitidos" int not null,
    "precioNoche" float not null
);
create table "Habitaciones"(
    "id" serial primary key not null,
    "idTipoHabitacion" serial references "TipoHabitaciones"(id) not null,
    "habilitado" bool default true,
    "reservado" bool default false,
    "nro" varchar(16)
);
create table "PaquetesPromocionales"(
    "id" serial primary key not null,
    "nombre" varchar(128),
    "descripcion" varchar(256),
    "precio" float,
    "habilitado" bool default false,
    "disponible" bool default false
);
