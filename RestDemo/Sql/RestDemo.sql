CREATE DATABASE res_demo;
GO

USE res_demo; 


CREATE TABLE Empleado(
	empleado_id BIGINT NOT NULL IDENTITY,
	nombres VARCHAR(100) NOT NULL,
	apellidos VARCHAR(100) NOT NULL,
	telefono VARCHAR(12) NOT NULL,
	correo VARCHAR(80) NOT NULL,
	fecha_nacimiento DATE NOT NULL,
	sueldo DECIMAL(10,2) NOT NULL,
	PRIMARY KEY(empleado_id)
)
GO

/* SP CREAR EMPLEADO */
CREATE PROCEDURE sp_insert_empleado(
	@nombres VARCHAR(100),
	@apellidos VARCHAR(100),
	@telefono VARCHAR(12),
	@correo VARCHAR(80),
	@fecha_nacimiento DATE,
	@sueldo DECIMAL(10,2)
)
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO Empleado(nombres,apellidos, telefono, correo, fecha_nacimiento, sueldo)
	VALUES(@nombres, @apellidos, @telefono, @correo, @fecha_nacimiento, @sueldo)
END
GO

CREATE PROCEDURE sp_update_empleado(
	@id BIGINT,
	@nombres VARCHAR(100),
	@apellidos VARCHAR(100),
	@telefono VARCHAR(12),
	@correo VARCHAR(80),
	@fecha_nacimiento DATE,
	@sueldo DECIMAL(10,2)
)
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Empleado
	SET nombres=@nombres,
		apellidos=@apellidos,
		telefono=@telefono,
		correo=@correo,
		fecha_nacimiento=@fecha_nacimiento,
		sueldo=@sueldo
	WHERE empleado_id=@id
END
GO


/* CREANDO REGISTRO DE EMPLEADOS */
INSERT INTO Empleado(nombres, apellidos, telefono, correo, fecha_nacimiento, sueldo)
VALUES('José Roberto', 'Musun Campos', '75478969', 'public.jmusun09@gmail.com', '1994/09/07', 800)

INSERT INTO Empleado(nombres, apellidos, telefono, correo, fecha_nacimiento, sueldo)
VALUES('Gorge Alberto', 'Gomez García', '69005632', 'gorgegarcia@outlook.com', '1980/11/10', 1500)

INSERT INTO Empleado(nombres, apellidos, telefono, correo, fecha_nacimiento, sueldo)
VALUES('María Luisa', 'Escobar Lopez', '65500763', 'malu@gmail.com', '1998/01/12', 500)

INSERT INTO Empleado(nombres, apellidos, telefono, correo, fecha_nacimiento, sueldo)
VALUES('Laura Roxana', 'Asencio Hernandez', '71340000', 'lauraxana@gmail.com', '1992/04/05', 600)

INSERT INTO Empleado(nombres, apellidos, telefono, correo, fecha_nacimiento, sueldo)
VALUES('Hector Francisco', 'Valdés', '70904531', 'franvaldes@gmail.com', '1985/10/01', 2000)

SELECT * FROM Empleado