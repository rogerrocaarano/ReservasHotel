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

-- Entidades principales del negocio

create table "Cliente"(
    "id" serial primary key not null,
    "nombres" varchar(128) not null,
    "apellidos" varchar(128) not null,
    "razonSocial" varchar(128),
    "nroRazonSocial" varchar(64),
    "email" varchar(128)
);
create table "Huesped"(
    "id" serial primary key not null,
    "nombres" varchar(128) not null,
    "apellidos" varchar(128) not null,
    "docIdentidad" varchar(32) not null,
    "tipoDocIdentidad" varchar(16) not null,
    "nacionalidad" varchar(16) not null
);
create table "TipoHabitacion"(
    "id" serial primary key not null,
    "nombre" varchar(128) not null,
    "descripcion" varchar(256),
    "huespedesPermitidos" int not null,
    "precioNoche" float not null
);
create table "Habitacion"(
    "id" serial primary key not null,
    "idTipoHabitacion" serial references "TipoHabitacion"(id) not null,
    "habilitado" bool default true,
    "reservado" bool default false,
    "nro" varchar(16) not null
);
create table "PaquetePromocional"(
    "id" serial primary key not null,
    "nombre" varchar(128) not null,
    "descripcion" varchar(256) not null,
    "precio" float default 0.00 not null,
    "habilitado" bool default false,
    "fechaDisponibleInicio" timestamp not null,
    "fechaDisponibleFin" timestamp not null
);
create table "Reserva"(
    "id" serial primary key not null,
    "idCliente" serial references "Cliente"(id),
    "idHabitacion" serial references "Habitacion"(id),
    "fechaReserva" timestamp default now(),
    "inicioReserva" timestamp,
    "finReserva" timestamp,
    "estado" varchar(32) default 'RESERVADO'
);

-- Reportes Reservas

-- create table "ReservaCliente"(
--     "idReserva" serial references "Reserva"(id),
--     "idCliente" serial references "Cliente"(id)
-- );
-- create table "HabitacionReservada"(
--     "idHabitacion" serial references "Habitacion"(id),
--     "idReserva" serial references "Reserva"(id)
-- );
-- create table "PaquetePromocionalReserva"(
--     "idPaquetePromocional" serial references "PaquetePromocional"(id),
--     "idReserva" serial references "Reserva"(id)
-- );

-- Registro de ingreso y salida de huéspedes

create table "CheckIn"(
    "idHuesped" serial references "Huesped"(id) not null,
    "idHabitacion" serial references "Habitacion"(id) not null,
    "ingreso" timestamp default now()
);
create table "CheckOut"(
    "idHuesped" serial references "Huesped"(id) not null,
    "idHabitacion" serial references "Habitacion"(id) not null,
    "salida" timestamp default now()
);

-- Inventario Habitación

create table "ItemHabitacion"(
    "id" serial primary key not null,
    "descripcion" varchar(128) not null,
    "costo" float not null
);
create table "InventarioHabitacion"(
    "idHabitacion" serial references "Habitacion"(id),
    "idItem" serial references "ItemHabitacion"(id)
);
create table "InventarioReposicion"(
    "id" serial primary key not null,
    "idHabitacion" serial references "Habitacion"(id),
    "idItemHabitacion" serial references "ItemHabitacion"(id)
);

-- Cobro

create table "Cobro"(
    "id" serial primary key not null,
    "idReserva" serial references "Reserva"(id) not null,
    "descripcion" varchar(128),
    "total" float not null,
    "estado" varchar(64)
);

-- Pagos

create table "Pago"(
    "id" serial primary key not null,
    "idCobro" serial references "Cobro"(id) not null,
    "monto" float not null
);
create table "PagoQr"(
    "idPago" serial references "Pago"(id) not null,
    "nroTransaccion" varchar(128) not null
);
create table "PagoTarjeta"(
    "idPago" serial references "Pago"(id) not null,
    "tipoTarjeta" varchar(16) not null,
    "ultimosDigTarjeta" int not null,
    "autorizacion" varchar(32) not null,
    "operacion" varchar(32) not null
);
create table if not exists "PagoStripe"(
    "idPago" serial references "Pago"(id) not null,
    "idStripe" varchar(64) not null,
    "estado" varchar(32) not null
);
