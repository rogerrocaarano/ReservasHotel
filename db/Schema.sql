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
    "genero" varchar(3) not null,
    "razonSocial" varchar(128),
    "nroRazonSocial" varchar(64),
    "email" varchar(128),
    "telefono" varchar(16)
);
create table "Huesped"(
    "id" serial primary key not null,
    "nombres" varchar(128) not null,
    "apellidos" varchar(128) not null,
    "docIdentidad" varchar(32) not null,
    "tipoDocIdentidad" varchar(16) not null,
    "pais" varchar(64) not null
);
create table "TipoHabitacion"(
    "id" serial primary key not null,
    "nombre" varchar(128) not null,
    "descripcion" varchar(2048),
    "huespedesPermitidos" int not null,
    "precioNoche" float not null
);
create table "Habitacion"(
    "id" int primary key not null,
    "idTipoHabitacion" serial references "TipoHabitacion"(id) not null,
    "piso" int not null,
    "ubicacion" varchar(128) not null,
    "habilitado" bool default true not null,
    "disponible" bool default true not null
);
create table "Reserva"(
    "id" serial primary key not null,
    "idCliente" serial references "Cliente"(id),
    "fechaReserva" timestamp default now(),
    "estado" varchar(32) default 'RESERVADO'
);
create table "RegistroReservaHabitacion"(
    "idReserva" serial references "Reserva"(id),
    "idHabitacion" serial references "Habitacion"(id),
    "inicioReserva" timestamp,
    "finReserva" timestamp
);
create table "RegistroIngresoHuesped"(
    "idHuesped" serial references "Huesped"(id),
    "idHabitacion" serial references "Habitacion"(id),
    "checkIn" timestamp,
    "checkOut" timestamp
);

-- create table "CheckIn"(
--     "idHuesped" serial references "Huesped"(id) not null,
--     "idHabitacion" serial references "Habitacion"(id) not null,
--     "ingreso" timestamp default now()
-- );
-- create table "CheckOut"(
--     "idHuesped" serial references "Huesped"(id) not null,
--     "idHabitacion" serial references "Habitacion"(id) not null,
--     "salida" timestamp default now()
-- );

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
create table "PagoPos"(
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
