create database PuestoTrabajo;
use PuestoTrabajo;

Create table Usuarios(
 idUsuario int primary key identity(1,1),
 email varchar(100) not null,
 password varchar(100) not null,
 tipo varchar(30) not null
);


----------Agregar usuario----------

create procedure AgregarUsuario(
 @email varchar(100),
 @password varchar(100),
 @tipo varchar(30)
)
as
begin
	insert into Usuarios(email, password, tipo) values(@email, @password, @tipo)
end

----------Actualizar usuario----------

create procedure ActualizarUsuario(
 @idUsuario int,
 @email varchar(100),
 @password varchar(100),
 @tipo varchar(30)
)
as
begin
	Update Usuarios set email = @email, password = @password, tipo = @tipo where idUsuario = @idUsuario
end


----------Eliminar usuario----------

create procedure EliminarUsuario(
 @idUsuario int
)
as
begin
	Delete from Usuarios where idUsuario = @idUsuario
end

----------Listar usuarios----------

create procedure ListarUsuario
as
begin
	Select * from Usuarios
end


EXECUTE ListarUsuario

----------Listar usuario por ID----------

create procedure ListarID_Usuario(
@idUsuario int
)
as
begin
	Select * from Usuarios where idUsuario = @idUsuario
end

--------------------Procedimiento Almacenado Validar Login--------------------

CREATE PROCEDURE SP_Validar_Usuario (
 @email varchar(100),
 @password varchar(100)
)
AS
BEGIN
SELECT email, password FROM Usuarios WHERE email = @email and password = @password;
END

--------------------Procedimiento Almacenado Validar Tipo Usuario--------------------

CREATE PROCEDURE SP_ValidarTipoUsuario (
	@email varchar(100)
)
AS
BEGIN
SELECT tipo FROM Usuarios WHERE email = @email;
END


--------------------Procedimiento Almacenado Listar idUsuario por email--------------------

CREATE PROCEDURE listarUsuario_email(
	@email varchar(100)
) 
AS
BEGIN
	SELECT idUsuario FROM Usuarios where email = @email;
END
