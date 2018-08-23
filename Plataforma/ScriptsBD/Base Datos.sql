--create database DB_PAAS_SCFAMA;

go

use DB_PAAS_SCFAMA;

go

if exists(select * from sys.objects where name='detalletarea') drop table detalletarea;
if exists(select * from sys.objects where name='recurso') drop table recurso;
if exists(select * from sys.objects where name='tarea') drop table tarea;
if exists(select * from sys.objects where name='proyecto') drop table proyecto;
if exists(select * from sys.objects where name='usuario') drop table usuario;
if exists(select * from sys.objects where name='empresa') drop table empresa;

go

create table usuario
(
id int identity not null,
id_empresa int not null,
nombre varchar(100) not null,
clave varchar(100) not null,
tipo varchar(50) not null,
inhabilitado bit not null
);

create table empresa
(
id int identity not null,
cedula varchar(100) not null,
nombre varchar(100) not null,
telefono varchar(100) not null,
correo varchar(100) not null,
ubicacion varchar(256) not null
);

create table recurso
(
id int identity not null,
id_empresa int not null,
tipo varchar(100) not null,
descripcion varchar(256) not null,
stock int not null,
inhabilitado bit not null
);

create table proyecto
(
id int identity not null,
codigo varchar(10) null,
id_usuario int not null,
id_ultimo_usuario_modificar int not null,
descripcion varchar(256) not null,
costo_total varchar(100) not null,
tiempo_total int not null,
estado varchar (15) not null,
inhabilitado bit not null
);

create table tarea
(
id int identity not null,
codigo varchar(10) null,
id_proyecto int not null,
descripcion varchar(256) not null,
tiempo int not null,
costo varchar(100) not null
);

create table detalletarea
(
id int identity not null,
id_tarea int not null,
id_recurso int not null,
monto varchar(100) not null,
cantidad int not null
);

---Keys---
go

alter table usuario add constraint pk_idusuario primary key (id);

alter table empresa add constraint pk_idempresa primary key (id);

alter table recurso add constraint pk_idrecurso primary key (id);

alter table proyecto add constraint pk_idproyecto primary key (id);

alter table tarea add constraint pk_idtarea primary key (id);

alter table detalletarea add constraint pk_iddetalletarea primary key (id, id_tarea, id_recurso);

---Foreign Keys---
go

alter table usuario add constraint fk_usuario_empresa foreign key (id_empresa) references empresa (id);

alter table recurso add constraint fk_recurso_empresa foreign key (id_empresa) references empresa (id);

alter table proyecto add constraint fk_proyecto_usuario foreign key (id_usuario) references usuario (id);
alter table proyecto add constraint fk_proyecto_usuario_modificar foreign key (id_ultimo_usuario_modificar) references usuario (id);

alter table tarea add constraint fk_tarea_proyecto foreign key (id_proyecto) references proyecto (id);

alter table detalletarea add constraint fk_detalletarea_tarea foreign key (id_tarea) references tarea (id) ON DELETE CASCADE;
alter table detalletarea add constraint fk_detalletarea_recurso foreign key (id_recurso) references recurso (id);

---Procedure---
go

create trigger consecutivo_proyecto on proyecto after insert 
as
set nocount on
begin
declare @UltimoId int;
declare @Consecutivo int;
set @UltimoId = (SELECT @@IDENTITY); 
set @Consecutivo = (select count(id) from proyecto where YEAR(GETDATE())= YEAR(GETDATE()));
update proyecto set codigo = ('P' + CONVERT(varchar(5), @Consecutivo) + '-' + SUBSTRING(CONVERT(varchar(5), YEAR(GETDATE())),3,2)) where id=@UltimoId;
end

go

create trigger consecutivo_tarea on tarea after insert 
as
set nocount on
begin
declare @UltimoId int;
declare @Consecutivo int;
declare @Proyecto int;
set @UltimoId = (SELECT @@IDENTITY); 
--Revisar funcion, trae el ultimo id, no el consecutivo--
set @Proyecto = (SELECT id_proyecto from tarea where id = @UltimoId);
set @Consecutivo = (select count(id) from tarea where id_proyecto = @Proyecto);
update tarea set codigo = ('T' + CONVERT(varchar(5), @Consecutivo) + '-P' + CONVERT(varchar(5), @Proyecto) + '-' + SUBSTRING(CONVERT(varchar(5), YEAR(GETDATE())),3,2)) where id=@UltimoId;
end

--go

--create trigger inhabilitar_usuario on usuario after insert 
--as
--set nocount on
--begin
--declare @UltimoId int;
--set @UltimoId = (SELECT @@IDENTITY);
--update usuario set inhabilitado = 0 where id=@UltimoId;
--end

---Inserts---
go

insert into empresa values ('CJ-208-EF-089-AS-1234', 'Constructora de Pruebas', '00506-2453-6098', 'constructoraCR@pruebas.co.cr', 'Palmares de Alajuela, 1km al sur del barrio las tres Marias.');

insert into usuario values (1, 'Admin','Admin', 'Gerente', 0);

insert into empresa values ('CJ-548-EF-298-BS-2240', 'Constructora ABC', '00506-2445-8914', 'constructorabc@gmail.co.cr', 'San Ramon de Alajuela, 500 metros sur de la escuela de Santiago.');

insert into usuario values (2, 'Admin','Admin', 'Gerente', 0);

--Clave Admin en formato Encriptado
--insert into usuario values (1, 'Admin','fIdUH9Pz71AW4S1BGQDIemBGqOg=',1);