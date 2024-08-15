use PokemonDb

Alter table RolesPermisos add Estado bit

Alter table RolesPermisos Drop Column Estado;


insert into Roles (Nombre_Rol) values('Admin')
insert into Roles (Nombre_Rol) values('Entrenador')
insert into Roles (Nombre_Rol) values('Enfermero')

select * from Roles


insert into Permisos (Nombre_Permiso) values('Crear')
insert into Permisos (Nombre_Permiso) values('Leer')
insert into Permisos (Nombre_Permiso) values('Actualizar')
insert into Permisos (Nombre_Permiso) values('Eliminar')
insert into Permisos (Nombre_Permiso) values('CargarPokedex') /*Me falta ingresarlo*/

select * from Permisos

insert into RolesPermisos (Id_Rol,Id_Permiso,Estado) values(1,1,1)
insert into RolesPermisos (Id_Rol,Id_Permiso,Estado) values(1,2,1)
insert into RolesPermisos (Id_Rol,Id_Permiso,Estado) values(1,3,1)
insert into RolesPermisos (Id_Rol,Id_Permiso,Estado) values(1,4,1)

select  * from RolesPermisos


Truncate Table RolesPermisos;



Go 

create procedure sp_registrar
@Name varchar(255),
@Rol varchar(255),
@usuario varchar(255),
@contrasena varchar(255),
@Patron varchar(255)
as 
begin
insert into Usuarios values(@Name, @Rol, @usuario, ENCRYPTBYPASSPHRASE(@Patron, @contrasena))
end;

GO

create procedure sp_login
@usuario varchar(255),
@contrasena varchar(255),
@Patron varchar(255)
as
begin 
select * from Usuarios where usuario = @usuario and Convert(varchar(255), DECRYPTBYPASSPHRASE(@Patron, contrasena))=@contrasena
end;

GO

create procedure sp_permisos
@Id_Rol int 
as 
begin
select Nombre_Permiso, Estado from RolesPermisos inner join Permisos on Permisos.Id=RolesPermisos.Id_Permiso where Id_Rol=@Id_Rol
end;

GO

create procedure sp_datos
as
begin
select * from Usuarios
end



DELETE FROM __EFMigrationsHistory WHERE MigrationId = '20240803064544_ThirdMigration';

DELETE FROM __EFMigrationsHistory WHERE MigrationId = '20240807183721_FifthMigration';

Alter Table Usuarios
Drop Constraint DF__Usuarios__Confir__2739D489;

Alter table Usuarios
Drop Column ConfirmarContrasena;

SELECT * 
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'Usuarios';