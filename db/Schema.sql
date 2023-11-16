-- Limpiar la base de datos

do $$
declare
    tabname text;
begin
    for tabname in (select tablename from pg_tables where schemaname = 'public')
        loop
            execute 'DROP TABLE IF EXISTS "' || tabname || '" CASCADE';
        end loop;
end
$$;

-- Usuarios y Roles del sistema Web

create table "Roles"(
    "id" serial primary key not null,
    "rol" varchar(32) not null
);
insert into "Roles"("rol") VALUES ('ADMINISTRADOR'),
                                  ('CLIENTE'),
                                  ('RECEPCION'),
                                  ('RESERVA'),
                                  ('INVENTARIO'),
                                  ('CAJA');
create table "Usuarios"(
    "id" serial primary key not null,
    "usuario" varchar(64) not null,
    "hashPassword" varchar(512) not null,
    "salHash" varchar(64) not null,
    "habilitado" boolean default true
);
create table "RolUsuario"(
    "idUsuario" serial references "Usuarios"(id) not null,
    "idRol" serial references "Roles"(id) not null
);

-- Entidades principales del negocio

create table "Clientes"(
    "id" serial primary key not null,
    "nombres" varchar(128) not null,
    "apellidos" varchar(128) not null,
    "razonSocial" varchar(128),
    "nroRazonSocial" varchar(64),
    "email" varchar(128)
);
create table "Huespedes"(
    "id" serial primary key not null,
    "nombres" varchar(128) not null,
    "apellidos" varchar(128) not null,
    "docIdentidad" varchar(32) not null,
    "tipoDocIdentidad" varchar(16) not null,
    "nacionalidad" varchar(16) not null
);
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
    "nro" varchar(16) not null
);
create table "PaquetesPromocionales"(
    "id" serial primary key not null,
    "nombre" varchar(128) not null,
    "descripcion" varchar(256) not null,
    "precio" float default 0.00 not null,
    "habilitado" bool default false,
    "fechaDisponibleInicio" timestamp not null,
    "fechaDisponibleFin" timestamp not null
);
create table "Reservas"(
    "id" serial primary key not null,
    "fechaReserva" timestamp default now(),
    "inicioReserva" timestamp,
    "finReserva" timestamp,
    "estado" varchar(32) default 'RESERVADO'
);

-- Reportes Reservas

create table "ReservasClientes"(
    "idReserva" serial references "Reservas"(id),
    "idCliente" serial references "Clientes"(id)
);
create table "HabitacionesReservadas"(
    "idHabitacion" serial references "Habitaciones"(id),
    "idReserva" serial references "Reservas"(id)
);
create table "PaquetesPromocionalesReservas"(
    "idPaquetePromocional" serial references "PaquetesPromocionales"(id),
    "idReserva" serial references "Reservas"(id)
);

-- Registro de ingreso y salida de huéspedes

create table "CheckIn"(
    "idHuesped" serial references "Huespedes"(id) not null,
    "idHabitacion" serial references "Habitaciones"(id) not null,
    "ingreso" timestamp default now()
);
create table "CheckOut"(
    "idHuesped" serial references "Huespedes"(id) not null,
    "idHabitacion" serial references "Habitaciones"(id) not null,
    "salida" timestamp default now()
);

-- Inventario Habitación

create table "ItemsHabitacion"(
    "id" serial primary key not null,
    "descripcion" varchar(128) not null,
    "costo" float not null
);
create table "ItemsIncluidosHabitacion"(
    "idHabitacion" serial references "Habitaciones"(id),
    "idItem" serial references "ItemsHabitacion"(id)
);
create table "InventarioReposicion"(
    "id" serial primary key not null,
    "idHabitacion" serial references "Habitaciones"(id),
    "idItemHabitacion" serial references "ItemsHabitacion"(id)
);

-- Cobro

create table "Cobros"(
    "id" serial primary key not null,
    "idReserva" serial references "Reservas"(id) not null,
    "descripcion" varchar(128),
    "total" float not null,
    "estado" varchar(64)
);

-- Pagos

create table "Pagos"(
    "id" serial primary key not null,
    "idCobro" serial references "Cobros"(id) not null,
    "monto" float not null
);
create table "PagosQr"(
    "idPago" serial references "Pagos"(id) not null,
    "nroTransaccion" varchar(128) not null
);
create table "PagosPos"(
    "idPago" serial references "Pagos"(id) not null,
    "tipoTarjeta" varchar(16) not null,
    "ultimosDigTarjeta" int not null,
    "autorizacion" varchar(32) not null,
    "operacion" varchar(32) not null
);
create table if not exists "PagosStripe"(
    "idPago" serial references "Pagos"(id) not null,
    "idStripe" varchar(64) not null,
    "estado" varchar(32) not null
);
