CREATE DATABASE LOBORUSYSTEMDB
GO

USE LOBORUSYSTEMDB
GO

-- =========================================================
-- ROL: Define los roles de usuario del sistema (ej. Administrador,
-- Vendedor, Bodeguero). Sirve como base para el control de permisos.
-- =========================================================
CREATE TABLE ROL(
    IdRol INT PRIMARY KEY IDENTITY, -- Id autogenerado
    Descripcion VARCHAR(50),        -- Nombre del rol (ej. "Administrador")
    FechaRegistro DATETIME DEFAULT GETDATE()
)
GO

-- =========================================================
-- PERMISO: Indica a qué menús/módulos del sistema tiene acceso
-- cada rol. Permite controlar qué puede ver/usar cada tipo de usuario.
-- =========================================================
CREATE TABLE PERMISO(
    IdPermiso INT PRIMARY KEY IDENTITY,
    IdRol INT REFERENCES ROL(IdRol),  -- A qué rol pertenece este permiso
    NombreMenu VARCHAR(100),          -- Menu/modulo habilitado (ej. "Ventas")
    FechaRegistro DATETIME DEFAULT GETDATE()
)
GO

-- =========================================================
-- PROVEEDOR: Empresas o personas a quienes la papeleria les compra
-- mercaderia/insumos. Se usa en el modulo de Compras.
-- CHECK CONSTRAINT: valida formato basico de Correo (algo@algo.algo).
-- =========================================================
CREATE TABLE PROVEEDOR(
    IdProveedor INT PRIMARY KEY IDENTITY,
    RTN VARCHAR(14),           -- RTN/identificacion del proveedor
    RazonSocial VARCHAR(50),   -- Nombre o empresa del proveedor
    Correo VARCHAR(50) CHECK (Correo LIKE '_%@_%._%'),
    Telefono VARCHAR(50),
    Estado BIT DEFAULT 1 CHECK (Estado IN (0,1)), -- 1=Activo, 0=Inactivo
    FechaRegistro DATETIME DEFAULT GETDATE()
)
GO

-- =========================================================
-- CLIENTE: Personas que compran en la papeleria. Ahora es referenciada
-- de forma OBLIGATORIA desde VENTA (FK NOT NULL): todo cliente debe
-- estar registrado aqui antes de poder generarsele una venta.
-- CHECK CONSTRAINT: valida formato basico de Correo (algo@algo.algo).
-- =========================================================
CREATE TABLE CLIENTE(
    IdCliente INT PRIMARY KEY IDENTITY,
    Documento VARCHAR(13),       -- DNI/identidad del cliente
    NombreCompleto VARCHAR(50),
    Correo VARCHAR(50) CHECK (Correo LIKE '_%@_%._%'),
    Telefono VARCHAR(50),
    Estado BIT DEFAULT 1 CHECK (Estado IN (0,1)), -- 1=Activo, 0=Inactivo
    FechaRegistro DATETIME DEFAULT GETDATE()
)
GO

-- =========================================================
-- USUARIO: Personas que tienen acceso al sistema (login), es decir,
-- los empleados de la papeleria (ej. cajeros, administradores).
-- No confundir con CLIENTE (que solo compra, no inicia sesion).
-- CHECK CONSTRAINT: valida formato basico de Correo (algo@algo.algo).
-- =========================================================
CREATE TABLE USUARIO(
    IdUsuario INT PRIMARY KEY IDENTITY,
    Documento VARCHAR(13),
    NombreCompleto VARCHAR(50),
    Correo VARCHAR(50) CHECK (Correo LIKE '_%@_%._%'),
    Clave VARBINARY(64),  -- Caso 9: contrasena cifrada con HASHBYTES (SHA2_256)
    IdRol INT REFERENCES ROL(IdRol),  -- Rol que define sus permisos
    Estado BIT DEFAULT 1 CHECK (Estado IN (0,1)), -- 1=Activo, 0=Inactivo
    FechaRegistro DATETIME DEFAULT GETDATE()
)
GO

-- =========================================================
-- CATEGORIA: Clasificacion de los productos (ej. "Utiles escolares",
-- "Oficina", "Arte"). Sirve para organizar el inventario.
-- =========================================================
CREATE TABLE CATEGORIA(
    IdCategoria INT PRIMARY KEY IDENTITY,
    Descripcion VARCHAR(100),  -- Nombre de la categoria
    Estado BIT DEFAULT 1 CHECK (Estado IN (0,1)), -- 1=Activo, 0=Inactivo
    FechaRegistro DATETIME DEFAULT GETDATE()
)
GO

-- =========================================================
-- PRODUCTO: Articulos que la papeleria vende (lapices, cuadernos, etc.).
-- Es la tabla central del modulo de Inventario.
-- =========================================================
CREATE TABLE PRODUCTO(
    IdProducto INT PRIMARY KEY IDENTITY,
    Codigo VARCHAR(50),         -- Codigo interno o SKU del producto
    Nombre VARCHAR(50),
    Descripcion VARCHAR(50),
    IdCategoria INT REFERENCES CATEGORIA(IdCategoria), -- A que categoria pertenece
    Stock INT NOT NULL DEFAULT 0,           -- Cantidad disponible en inventario
    PrecioCompra DECIMAL(10,2) DEFAULT 0,   -- Costo al que se compra
    PrecioVenta DECIMAL(10,2) DEFAULT 0,    -- Precio al que se vende
    Estado BIT DEFAULT 1 CHECK (Estado IN (0,1)), -- 1=Activo, 0=Inactivo (descontinuado)
    FechaRegistro DATETIME DEFAULT GETDATE()
)
GO

-- =========================================================
-- COMPRA: Registra cada compra que la papeleria le hace a un proveedor
-- (encabezado de la transaccion). El detalle de productos esta en
-- DETALLE_COMPRA.
-- =========================================================
CREATE TABLE COMPRA(
    IdCompra INT PRIMARY KEY IDENTITY,
    IdUsuario INT REFERENCES USUARIO(IdUsuario),       -- Quien registro la compra
    IdProveedor INT REFERENCES PROVEEDOR(IdProveedor), -- A quien se le compro
    TipoDocumento VARCHAR(50),    -- Ej. Factura, Recibo
    NumeroDocumento VARCHAR(50),  -- Numero del documento fiscal
    MontoTotal DECIMAL(10,2),     -- Total pagado en la compra
    FechaRegistro DATETIME DEFAULT GETDATE()
)
GO

-- =========================================================
-- DETALLE_COMPRA: Lista los productos especificos incluidos en cada
-- COMPRA, con cantidad y precios. El trigger TR_DetalleCompra_ActualizarStock
-- es lo que actualiza el Stock de PRODUCTO al momento de comprar.
-- =========================================================
CREATE TABLE DETALLE_COMPRA(
    IdDetalleCompra INT PRIMARY KEY IDENTITY,
    IdCompra INT REFERENCES COMPRA(IdCompra),       -- A que compra pertenece
    IdProducto INT REFERENCES PRODUCTO(IdProducto), -- Que producto se compro
    PrecioCompra DECIMAL(10,2) DEFAULT 0,  -- Precio de compra de ese producto en ese momento
    PrecioVenta DECIMAL(10,2) DEFAULT 0,   -- Precio de venta sugerido en ese momento
    Cantidad INT,                          -- Unidades compradas
    MontoTotal DECIMAL(10,2),              -- Cantidad * PrecioCompra
    FechaRegistro DATETIME DEFAULT GETDATE(),
    CONSTRAINT CK_DetalleCompra_MontoTotal CHECK (MontoTotal = Cantidad * PrecioCompra)
)
GO

-- =========================================================
-- VENTA: Registra cada venta hecha a un cliente (encabezado de la
-- transaccion). El detalle de productos vendidos esta en DETALLE_VENTA.
-- CAMBIO DE DISEÑO: se reemplazaron las columnas de texto libre
-- DocumentoCliente/NombreCliente por una FK real y obligatoria hacia
-- CLIENTE (IdCliente NOT NULL). Esto implica que, a partir de ahora,
-- todo cliente debe estar registrado en CLIENTE antes de poder
-- generarsele una venta; ya no se admiten ventas a clientes ocasionales
-- sin registro previo.
-- =========================================================
CREATE TABLE VENTA(
    IdVenta INT PRIMARY KEY IDENTITY,
    IdUsuario INT REFERENCES USUARIO(IdUsuario),  -- Empleado que atendio la venta
    IdCliente INT NOT NULL REFERENCES CLIENTE(IdCliente), -- Cliente registrado al que se le vendio
    TipoDocumento VARCHAR(50),       -- Ej. Factura, Recibo
    NumeroDocumento VARCHAR(50),     -- Numero del documento fiscal
    MetodoPago VARCHAR(20) CHECK (MetodoPago IN ('Efectivo','Tarjeta','Transferencia')), -- Forma de pago usada
    MontoPago DECIMAL(10,2),         -- Cuanto pago el cliente
    MontoCambio DECIMAL(10,2),       -- Vuelto entregado
    MontoTotal DECIMAL(10,2),        -- Total de la venta
    FechaRegistro DATETIME DEFAULT GETDATE(),
    CONSTRAINT CK_Venta_MontoCambio CHECK (MontoCambio = MontoPago - MontoTotal)
)
GO

-- =========================================================
-- DETALLE_VENTA: Lista los productos especificos incluidos en cada
-- VENTA, con cantidad y precio. El trigger TR_DetalleVenta_ActualizarStock
-- es lo que descuenta el Stock de PRODUCTO al momento de vender.
-- =========================================================
CREATE TABLE DETALLE_VENTA(
    IdDetalleVenta INT PRIMARY KEY IDENTITY,
    IdVenta INT REFERENCES VENTA(IdVenta),          -- A que venta pertenece
    IdProducto INT REFERENCES PRODUCTO(IdProducto), -- Que producto se vendio
    PrecioVenta DECIMAL(10,2), -- Precio unitario al momento de la venta
    Cantidad INT,              -- Unidades vendidas
    SubTotal DECIMAL(10,2),    -- Cantidad * PrecioVenta
    FechaRegistro DATETIME DEFAULT GETDATE(),
    CONSTRAINT CK_DetalleVenta_SubTotal CHECK (SubTotal = Cantidad * PrecioVenta)
)
GO

-- =========================================================
-- NEGOCIO: Datos generales de la empresa (la papeleria misma), usados
-- para mostrar en facturas/reportes (nombre, RTN, logo, direccion).
-- Normalmente solo tiene un registro. No se relaciona con ninguna otra
-- tabla: es una tabla de configuracion fija, no una entidad transaccional.
-- =========================================================
CREATE TABLE NEGOCIO(
    IdNegocio INT PRIMARY KEY,
    Nombre VARCHAR(60),         -- Nombre comercial de la papeleria
    RTN VARCHAR(14),            -- Numero de identificacion fiscal
    Direccion VARCHAR(200),
    Logo VARBINARY(MAX) NULL    -- Imagen del logo, para mostrar en reportes/facturas
)
GO

-- =========================================================
-- BITACORA: Registro de auditoria. Guarda que usuario hizo que accion
-- (INSERT/UPDATE/DELETE) y sobre que tabla, para tener trazabilidad
-- de los cambios realizados en el sistema. Alimentada por triggers.
-- =========================================================
CREATE TABLE BITACORA(
    IdBitacora INT PRIMARY KEY IDENTITY,
    TablaAfectada VARCHAR(100) NOT NULL,   -- Nombre de la tabla modificada
    Accion VARCHAR(20) NOT NULL CHECK (Accion IN ('INSERT','UPDATE','DELETE')), -- Tipo de operacion
    IdUsuario INT REFERENCES USUARIO(IdUsuario), -- Quien hizo el cambio
    Detalle VARCHAR(MAX) NULL,             -- Descripcion/valores del cambio (opcional)
    FechaRegistro DATETIME DEFAULT GETDATE()
)
GO

-- =========================================================
-- TOKEN_AUTENTICACION: Tabla generica para tokens de seguridad del
-- sistema. Cubre dos casos de uso, diferenciados por la columna Tipo:
--   - 'LOGIN'              -> token de sesion/ingreso al sistema
--   - 'RECUPERACION_CLAVE' -> token de un solo uso para resetear clave
-- Regla de negocio: un usuario solo puede tener UN token ACTIVO (no
-- usado, no expirado) por tipo a la vez. Esa regla la aplican los
-- procedimientos almacenados (invalidando el token anterior antes de
-- generar uno nuevo), no la tabla en si.
-- =========================================================
CREATE TABLE TOKEN_AUTENTICACION(
    IdToken INT PRIMARY KEY IDENTITY,
    IdUsuario INT NOT NULL REFERENCES USUARIO(IdUsuario), -- A que usuario pertenece el token
    Token VARCHAR(255) NOT NULL,        -- Valor del token (ej. GUID o cadena aleatoria segura)
    Tipo VARCHAR(20) NOT NULL CHECK (Tipo IN ('LOGIN','RECUPERACION_CLAVE')), -- Para que se emitio
    FechaExpiracion DATETIME NOT NULL,  -- Momento en que el token deja de ser valido
    Utilizado BIT NOT NULL DEFAULT 0 CHECK (Utilizado IN (0,1)), -- 1=ya se uso/cerro sesion, 0=activo
    FechaRegistro DATETIME DEFAULT GETDATE()
)
GO


/*************************** CREACION DE PROCEDIMIENTOS ALMACENADOS ***************************/
/*--------------------------------------------------------------------------------------------*/

go

create PROC SP_REGISTRARUSUARIO(
@Documento varchar(50),
@NombreCompleto varchar(100),
@Correo varchar(100),
@Clave varchar(100),
@IdRol int,
@Estado bit,
@IdUsuarioResultado int output,
@Mensaje varchar(500) output
)
as
begin
	set @IdUsuarioResultado = 0
	set @Mensaje = ''

	if not exists(select * from USUARIO where Documento = @Documento)
	begin
		insert into usuario(Documento,NombreCompleto,Correo,Clave,IdRol,Estado) values
		(@Documento,@NombreCompleto,@Correo,HASHBYTES('SHA2_256',@Clave),@IdRol,@Estado)

		set @IdUsuarioResultado = SCOPE_IDENTITY()
	end
	else
		set @Mensaje = 'No se puede repetir el documento para más de un usuario'
end

go

create PROC SP_EDITARUSUARIO(
@IdUsuario int,
@Documento varchar(50),
@NombreCompleto varchar(100),
@Correo varchar(100),
@Clave varchar(100),
@IdRol int,
@Estado bit,
@Respuesta bit output,
@Mensaje varchar(500) output
)
as
begin
	set @Respuesta = 0
	set @Mensaje = ''

	if not exists(select * from USUARIO where Documento = @Documento and idusuario != @IdUsuario)
	begin
		update  usuario set
		Documento = @Documento,
		NombreCompleto = @NombreCompleto,
		Correo = @Correo,
		Clave = HASHBYTES('SHA2_256',@Clave),
		IdRol = @IdRol,
		Estado = @Estado
		where IdUsuario = @IdUsuario

		set @Respuesta = 1
	end
	else
		set @Mensaje = 'No se puede repetir el documento para más de un usuario'
end
go

create PROC SP_ELIMINARUSUARIO(
@IdUsuario int,
@Respuesta bit output,
@Mensaje varchar(500) output
)
as
begin
	set @Respuesta = 0
	set @Mensaje = ''
	declare @pasoreglas bit = 1

	IF EXISTS (SELECT * FROM COMPRA C 
	INNER JOIN USUARIO U ON U.IdUsuario = C.IdUsuario
	WHERE U.IDUSUARIO = @IdUsuario
	)
	BEGIN
		set @pasoreglas = 0
		set @Respuesta = 0
		set @Mensaje = @Mensaje + 'No se puede eliminar porque el usuario se encuentra relacionado a una COMPRA\n' 
	END

	IF EXISTS (SELECT * FROM VENTA V
	INNER JOIN USUARIO U ON U.IdUsuario = V.IdUsuario
	WHERE U.IDUSUARIO = @IdUsuario
	)
	BEGIN
		set @pasoreglas = 0
		set @Respuesta = 0
		set @Mensaje = @Mensaje + 'No se puede eliminar porque el usuario se encuentra relacionado a una VENTA\n' 
	END

	if(@pasoreglas = 1)
	begin
		delete from USUARIO where IdUsuario = @IdUsuario
		set @Respuesta = 1 
	end
end

go

/* ---------- PROCEDIMIENTOS PARA CATEGORIA -----------------*/

create PROC SP_RegistrarCategoria(
@Descripcion varchar(50),
@Estado bit,
@Resultado int output,
@Mensaje varchar(500) output
)as
begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM CATEGORIA WHERE Descripcion = @Descripcion)
	begin
		insert into CATEGORIA(Descripcion,Estado) values (@Descripcion,@Estado)
		set @Resultado = SCOPE_IDENTITY()
	end
	ELSE
		set @Mensaje = 'No se puede repetir la descripcion de una categoria'
end

go

Create procedure sp_EditarCategoria(
@IdCategoria int,
@Descripcion varchar(50),
@Estado bit,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
	SET @Resultado = 1
	IF NOT EXISTS (SELECT * FROM CATEGORIA WHERE Descripcion =@Descripcion and IdCategoria != @IdCategoria)
		update CATEGORIA set
		Descripcion = @Descripcion,
		Estado = @Estado
		where IdCategoria = @IdCategoria
	ELSE
	begin
		SET @Resultado = 0
		set @Mensaje = 'No se puede repetir la descripcion de una categoria'
	end
end

go

create procedure sp_EliminarCategoria(
@IdCategoria int,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
	SET @Resultado = 1
	IF NOT EXISTS (
	 select *  from CATEGORIA c
	 inner join PRODUCTO p on p.IdCategoria = c.IdCategoria
	 where c.IdCategoria = @IdCategoria
	)
	begin
	 delete top(1) from CATEGORIA where IdCategoria = @IdCategoria
	end
	ELSE
	begin
		SET @Resultado = 0
		set @Mensaje = 'La categoria se encuentara relacionada a un producto'
	end
end

GO

/* ---------- PROCEDIMIENTOS PARA PRODUCTO -----------------*/

create PROC sp_RegistrarProducto(
@Codigo varchar(20),
@Nombre varchar(30),
@Descripcion varchar(30),
@IdCategoria int,
@Estado bit,
@Resultado int output,
@Mensaje varchar(500) output
)as
begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM producto WHERE Codigo = @Codigo)
	begin
		insert into producto(Codigo,Nombre,Descripcion,IdCategoria,Estado) values (@Codigo,@Nombre,@Descripcion,@IdCategoria,@Estado)
		set @Resultado = SCOPE_IDENTITY()
	end
	ELSE
	 SET @Mensaje = 'Ya existe un producto con el mismo codigo' 
end

GO

create procedure sp_ModificarProducto(
@IdProducto int,
@Codigo varchar(20),
@Nombre varchar(30),
@Descripcion varchar(30),
@IdCategoria int,
@Estado bit,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
	SET @Resultado = 1
	IF NOT EXISTS (SELECT * FROM PRODUCTO WHERE codigo = @Codigo and IdProducto != @IdProducto)
		update PRODUCTO set
		codigo = @Codigo,
		Nombre = @Nombre,
		Descripcion = @Descripcion,
		IdCategoria = @IdCategoria,
		Estado = @Estado
		where IdProducto = @IdProducto
	ELSE
	begin
		SET @Resultado = 0
		SET @Mensaje = 'Ya existe un producto con el mismo codigo' 
	end
end

go

create PROC SP_EliminarProducto(
@IdProducto int,
@Respuesta bit output,
@Mensaje varchar(500) output
)
as
begin
	set @Respuesta = 0
	set @Mensaje = ''
	declare @pasoreglas bit = 1

	IF EXISTS (SELECT * FROM DETALLE_COMPRA dc 
	INNER JOIN PRODUCTO p ON p.IdProducto = dc.IdProducto
	WHERE p.IdProducto = @IdProducto
	)
	BEGIN
		set @pasoreglas = 0
		set @Respuesta = 0
		set @Mensaje = @Mensaje + 'No se puede eliminar porque se encuentra relacionado a una COMPRA\n' 
	END

	IF EXISTS (SELECT * FROM DETALLE_VENTA dv
	INNER JOIN PRODUCTO p ON p.IdProducto = dv.IdProducto
	WHERE p.IdProducto = @IdProducto
	)
	BEGIN
		set @pasoreglas = 0
		set @Respuesta = 0
		set @Mensaje = @Mensaje + 'No se puede eliminar porque se encuentra relacionado a una VENTA\n' 
	END

	if(@pasoreglas = 1)
	begin
		delete from PRODUCTO where IdProducto = @IdProducto
		set @Respuesta = 1 
	end
end
go

/* ---------- PROCEDIMIENTOS PARA CLIENTE -----------------*/

create PROC sp_RegistrarCliente(
@Documento varchar(50),
@NombreCompleto varchar(50),
@Correo varchar(50),
@Telefono varchar(50),
@Estado bit,
@Resultado int output,
@Mensaje varchar(500) output
)as
begin
	SET @Resultado = 0
	DECLARE @IDPERSONA INT 
	IF NOT EXISTS (SELECT * FROM CLIENTE WHERE Documento = @Documento)
	begin
		insert into CLIENTE(Documento,NombreCompleto,Correo,Telefono,Estado) values (
		@Documento,@NombreCompleto,@Correo,@Telefono,@Estado)

		set @Resultado = SCOPE_IDENTITY()
	end
	else
		set @Mensaje = 'El numero de documento ya existe'
end

go

create PROC sp_ModificarCliente(
@IdCliente int,
@Documento varchar(50),
@NombreCompleto varchar(50),
@Correo varchar(50),
@Telefono varchar(50),
@Estado bit,
@Resultado bit output,
@Mensaje varchar(500) output
)as
begin
	SET @Resultado = 1
	DECLARE @IDPERSONA INT 
	IF NOT EXISTS (SELECT * FROM CLIENTE WHERE Documento = @Documento and IdCliente != @IdCliente)
	begin
		update CLIENTE set
		Documento = @Documento,
		NombreCompleto = @NombreCompleto,
		Correo = @Correo,
		Telefono = @Telefono,
		Estado = @Estado
		where IdCliente = @IdCliente
	end
	else
	begin
		SET @Resultado = 0
		set @Mensaje = 'El numero de documento ya existe'
	end
end

GO

-- sp_EliminarCliente: NUEVO. Antes no existia porque CLIENTE no tenia
-- relaciones; ahora que VENTA depende de CLIENTE, se necesita esta
-- validacion para no dejar ventas huerfanas o violar la FK.
create procedure sp_EliminarCliente(
@IdCliente int,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
	SET @Resultado = 1
	IF NOT EXISTS (
	 select * from VENTA v
	 where v.IdCliente = @IdCliente
	)
	begin
	 delete from CLIENTE where IdCliente = @IdCliente
	end
	ELSE
	begin
		SET @Resultado = 0
		set @Mensaje = 'El cliente se encuentra relacionado a una venta y no puede eliminarse'
	end
end

go

/* ---------- PROCEDIMIENTOS PARA PROVEEDOR -----------------*/

create PROC sp_RegistrarProveedor(
@Documento varchar(50),
@RazonSocial varchar(50),
@Correo varchar(50),
@Telefono varchar(50),
@Estado bit,
@Resultado int output,
@Mensaje varchar(500) output
)as
begin
	SET @Resultado = 0
	DECLARE @IDPERSONA INT 
	IF NOT EXISTS (SELECT * FROM PROVEEDOR WHERE Documento = @Documento)
	begin
		insert into PROVEEDOR(Documento,RazonSocial,Correo,Telefono,Estado) values (
		@Documento,@RazonSocial,@Correo,@Telefono,@Estado)

		set @Resultado = SCOPE_IDENTITY()
	end
	else
		set @Mensaje = 'El numero de documento ya existe'
end

GO

create PROC sp_ModificarProveedor(
@IdProveedor int,
@Documento varchar(50),
@RazonSocial varchar(50),
@Correo varchar(50),
@Telefono varchar(50),
@Estado bit,
@Resultado bit output,
@Mensaje varchar(500) output
)as
begin
	SET @Resultado = 1
	DECLARE @IDPERSONA INT 
	IF NOT EXISTS (SELECT * FROM PROVEEDOR WHERE Documento = @Documento and IdProveedor != @IdProveedor)
	begin
		update PROVEEDOR set
		Documento = @Documento,
		RazonSocial = @RazonSocial,
		Correo = @Correo,
		Telefono = @Telefono,
		Estado = @Estado
		where IdProveedor = @IdProveedor
	end
	else
	begin
		SET @Resultado = 0
		set @Mensaje = 'El numero de documento ya existe'
	end
end

go

create procedure sp_EliminarProveedor(
@IdProveedor int,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
	SET @Resultado = 1
	IF NOT EXISTS (
	 select *  from PROVEEDOR p
	 inner join COMPRA c on p.IdProveedor = c.IdProveedor
	 where p.IdProveedor = @IdProveedor
	)
	begin
	 delete top(1) from PROVEEDOR where IdProveedor = @IdProveedor
	end
	ELSE
	begin
		SET @Resultado = 0
		set @Mensaje = 'El proveedor se encuentara relacionado a una compra'
	end
end

go

/* PROCESOS PARA REGISTRAR UNA COMPRA */

CREATE TYPE [dbo].[EDetalle_Compra] AS TABLE(
	[IdProducto] int NULL,
	[PrecioCompra] decimal(18,2) NULL,
	[PrecioVenta] decimal(18,2) NULL,
	[Cantidad] int NULL,
	[MontoTotal] decimal(18,2) NULL
)

GO

-- NOTA: este SP YA NO actualiza el Stock/Precios manualmente.
-- Esa logica ahora la hace el trigger TR_DetalleCompra_ActualizarStock
-- al insertarse filas en DETALLE_COMPRA, para evitar duplicar la suma.
-- IMPORTANTE: el MontoTotal que envia @DetalleCompra para cada producto
-- debe venir calculado como Cantidad * PrecioCompra desde C#, o el INSERT
-- fallara por el CHECK CONSTRAINT CK_DetalleCompra_MontoTotal.
CREATE PROCEDURE sp_RegistrarCompra(
@IdUsuario int,
@IdProveedor int,
@TipoDocumento varchar(500),
@NumeroDocumento varchar(500),
@MontoTotal decimal(18,2),
@DetalleCompra [EDetalle_Compra] READONLY,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
	begin try
		declare @idcompra int = 0
		set @Resultado = 1
		set @Mensaje = ''

		begin transaction registro

		insert into COMPRA(IdUsuario,IdProveedor,TipoDocumento,NumeroDocumento,MontoTotal)
		values(@IdUsuario,@IdProveedor,@TipoDocumento,@NumeroDocumento,@MontoTotal)

		set @idcompra = SCOPE_IDENTITY()

		insert into DETALLE_COMPRA(IdCompra,IdProducto,PrecioCompra,PrecioVenta,Cantidad,MontoTotal)
		select @idcompra,IdProducto,PrecioCompra,PrecioVenta,Cantidad,MontoTotal from @DetalleCompra
		-- El UPDATE de Stock/PrecioCompra/PrecioVenta lo hace TR_DetalleCompra_ActualizarStock

		commit transaction registro
	end try
	begin catch
		set @Resultado = 0
		set @Mensaje = ERROR_MESSAGE()
		rollback transaction registro
	end catch
end

GO

/* PROCESOS PARA REGISTRAR UNA VENTA */

CREATE TYPE [dbo].[EDetalle_Venta] AS TABLE(
	[IdProducto] int NULL,
	[PrecioVenta] decimal(18,2) NULL,
	[Cantidad] int NULL,
	[SubTotal] decimal(18,2) NULL
)

GO

-- NOTA: el descuento de Stock lo hace el trigger TR_DetalleVenta_ActualizarStock
-- al insertarse filas en DETALLE_VENTA.
-- CAMBIO: @DocumentoCliente/@NombreCliente fueron reemplazados por
-- @IdCliente, ya que VENTA ahora exige un cliente registrado (FK NOT NULL).
-- IMPORTANTE: @SubTotal en @DetalleVenta debe venir calculado como
-- Cantidad * PrecioVenta, y @MontoCambio debe venir como
-- @MontoPago - @MontoTotal, o el INSERT fallara por los CHECK CONSTRAINT
-- CK_DetalleVenta_SubTotal y CK_Venta_MontoCambio respectivamente.
create procedure usp_RegistrarVenta(
@IdUsuario int,
@IdCliente int,
@TipoDocumento varchar(500),
@NumeroDocumento varchar(500),
@MetodoPago varchar(20),
@MontoPago decimal(18,2),
@MontoCambio decimal(18,2),
@MontoTotal decimal(18,2),
@DetalleVenta [EDetalle_Venta] READONLY,                                      
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
	begin try
		declare @idventa int = 0
		set @Resultado = 1
		set @Mensaje = ''

		if not exists(select * from CLIENTE where IdCliente = @IdCliente and Estado = 1)
		begin
			set @Resultado = 0
			set @Mensaje = 'El cliente no existe o se encuentra inactivo'
			return
		end

		begin  transaction registro

		insert into VENTA(IdUsuario,IdCliente,TipoDocumento,NumeroDocumento,MetodoPago,MontoPago,MontoCambio,MontoTotal)
		values(@IdUsuario,@IdCliente,@TipoDocumento,@NumeroDocumento,@MetodoPago,@MontoPago,@MontoCambio,@MontoTotal)

		set @idventa = SCOPE_IDENTITY()

		insert into DETALLE_VENTA(IdVenta,IdProducto,PrecioVenta,Cantidad,SubTotal)
		select @idventa,IdProducto,PrecioVenta,Cantidad,SubTotal from @DetalleVenta

		commit transaction registro
	end try
	begin catch
		set @Resultado = 0
		set @Mensaje = ERROR_MESSAGE()
		rollback transaction registro
	end catch
end

go

create PROC sp_ReporteCompras(
 @fechainicio varchar(10),
 @fechafin varchar(10),
 @idproveedor int
 )
  as
 begin
  SET DATEFORMAT dmy;
   select 
 convert(char(10),c.FechaRegistro,103)[FechaRegistro],c.TipoDocumento,c.NumeroDocumento,c.MontoTotal,
 u.NombreCompleto[UsuarioRegistro],
 pr.Documento[DocumentoProveedor],pr.RazonSocial,
 p.Codigo[CodigoProducto],p.Nombre[NombreProducto],ca.Descripcion[Categoria],dc.PrecioCompra,dc.PrecioVenta,dc.Cantidad,dc.MontoTotal[SubTotal]
 from COMPRA c
 inner join USUARIO u on u.IdUsuario = c.IdUsuario
 inner join PROVEEDOR pr on pr.IdProveedor = c.IdProveedor
 inner join DETALLE_COMPRA dc on dc.IdCompra = c.IdCompra
 inner join PRODUCTO p on p.IdProducto = dc.IdProducto
 inner join CATEGORIA ca on ca.IdCategoria = p.IdCategoria
 where CONVERT(date,c.FechaRegistro) between @fechainicio and @fechafin
 and pr.IdProveedor = iif(@idproveedor=0,pr.IdProveedor,@idproveedor)
 end

 go

-- CAMBIO: ahora hace INNER JOIN CLIENTE para traer el documento y
-- nombre reales del cliente registrado, en vez de los campos de texto
-- libre que tenia VENTA anteriormente.
 CREATE PROC sp_ReporteVentas(
 @fechainicio varchar(10),
 @fechafin varchar(10)
 )
 as
 begin
 SET DATEFORMAT dmy;  
 select 
 convert(char(10),v.FechaRegistro,103)[FechaRegistro],v.TipoDocumento,v.NumeroDocumento,v.MontoTotal,
 u.NombreCompleto[UsuarioRegistro],
 cl.Documento[DocumentoCliente],cl.NombreCompleto[NombreCliente],v.MetodoPago,
 p.Codigo[CodigoProducto],p.Nombre[NombreProducto],ca.Descripcion[Categoria],dv.PrecioVenta,dv.Cantidad,dv.SubTotal
 from VENTA v
 inner join USUARIO u on u.IdUsuario = v.IdUsuario
 inner join CLIENTE cl on cl.IdCliente = v.IdCliente
 inner join DETALLE_VENTA dv on dv.IdVenta = v.IdVenta
 inner join PRODUCTO p on p.IdProducto = dv.IdProducto
 inner join CATEGORIA ca on ca.IdCategoria = p.IdCategoria
 where CONVERT(date,v.FechaRegistro) between @fechainicio and @fechafin
end

go

/* ---------- PROCEDIMIENTOS PARA TOKEN_AUTENTICACION -----------------*/

-- sp_GenerarTokenLogin: al iniciar sesion correctamente, invalida
-- cualquier token de tipo LOGIN anterior de ese usuario (regla de
-- "solo un token activo a la vez") y genera uno nuevo.
CREATE PROC sp_GenerarTokenLogin(
@IdUsuario int,
@Token varchar(255),
@MinutosExpiracion int,
@IdTokenResultado int output,
@Mensaje varchar(500) output
)
as
begin
	set @IdTokenResultado = 0
	set @Mensaje = ''

	if exists(select * from USUARIO where IdUsuario = @IdUsuario and Estado = 1)
	begin
		-- Invalidamos cualquier token de LOGIN activo previo de este usuario
		update TOKEN_AUTENTICACION
		set Utilizado = 1
		where IdUsuario = @IdUsuario
		and Tipo = 'LOGIN'
		and Utilizado = 0

		insert into TOKEN_AUTENTICACION(IdUsuario,Token,Tipo,FechaExpiracion,Utilizado)
		values(@IdUsuario,@Token,'LOGIN',DATEADD(MINUTE,@MinutosExpiracion,GETDATE()),0)

		set @IdTokenResultado = SCOPE_IDENTITY()
	end
	else
		set @Mensaje = 'El usuario no existe o se encuentra inactivo'
end
go

-- sp_ValidarToken: verifica si un token (de cualquier tipo) sigue
-- siendo valido: existe, no fue usado, y no ha expirado.
CREATE PROC sp_ValidarToken(
@Token varchar(255),
@Tipo varchar(20),
@EsValido bit output,
@IdUsuario int output,
@Mensaje varchar(500) output
)
as
begin
	set @EsValido = 0
	set @IdUsuario = 0
	set @Mensaje = ''

	select @IdUsuario = IdUsuario
	from TOKEN_AUTENTICACION
	where Token = @Token
	and Tipo = @Tipo
	and Utilizado = 0
	and FechaExpiracion > GETDATE()

	if @IdUsuario is not null and @IdUsuario > 0
		set @EsValido = 1
	else
	begin
		set @IdUsuario = 0
		set @Mensaje = 'El token no existe, ya fue utilizado, o se encuentra expirado'
	end
end
go

-- sp_CerrarSesion: marca un token de LOGIN como utilizado (logout).
CREATE PROC sp_CerrarSesion(
@Token varchar(255),
@Respuesta bit output,
@Mensaje varchar(500) output
)
as
begin
	set @Respuesta = 0
	set @Mensaje = ''

	if exists(select * from TOKEN_AUTENTICACION where Token = @Token and Tipo = 'LOGIN' and Utilizado = 0)
	begin
		update TOKEN_AUTENTICACION
		set Utilizado = 1
		where Token = @Token
		and Tipo = 'LOGIN'

		set @Respuesta = 1
	end
	else
		set @Mensaje = 'El token no existe o ya estaba cerrado'
end
go

-- sp_SolicitarRecuperacionClave: genera un token de un solo uso para
-- resetear la clave, invalidando cualquier token de recuperacion
-- anterior que el usuario tuviera pendiente.
CREATE PROC sp_SolicitarRecuperacionClave(
@Correo varchar(100),
@Token varchar(255),
@MinutosExpiracion int,
@IdTokenResultado int output,
@Mensaje varchar(500) output
)
as
begin
	set @IdTokenResultado = 0
	set @Mensaje = ''
	declare @IdUsuario int

	select @IdUsuario = IdUsuario from USUARIO where Correo = @Correo and Estado = 1

	if @IdUsuario is not null
	begin
		update TOKEN_AUTENTICACION
		set Utilizado = 1
		where IdUsuario = @IdUsuario
		and Tipo = 'RECUPERACION_CLAVE'
		and Utilizado = 0

		insert into TOKEN_AUTENTICACION(IdUsuario,Token,Tipo,FechaExpiracion,Utilizado)
		values(@IdUsuario,@Token,'RECUPERACION_CLAVE',DATEADD(MINUTE,@MinutosExpiracion,GETDATE()),0)

		set @IdTokenResultado = SCOPE_IDENTITY()
	end
	else
		set @Mensaje = 'No existe un usuario activo con ese correo'
end
go

-- sp_RestablecerClave: valida el token de recuperacion, actualiza la
-- clave (cifrada con HASHBYTES) y marca el token como utilizado para
-- que no pueda reutilizarse.
CREATE PROC sp_RestablecerClave(
@Token varchar(255),
@NuevaClave varchar(100),
@Respuesta bit output,
@Mensaje varchar(500) output
)
as
begin
	set @Respuesta = 0
	set @Mensaje = ''
	declare @IdUsuario int

	select @IdUsuario = IdUsuario
	from TOKEN_AUTENTICACION
	where Token = @Token
	and Tipo = 'RECUPERACION_CLAVE'
	and Utilizado = 0
	and FechaExpiracion > GETDATE()

	if @IdUsuario is not null
	begin
		update USUARIO
		set Clave = HASHBYTES('SHA2_256',@NuevaClave)
		where IdUsuario = @IdUsuario

		update TOKEN_AUTENTICACION
		set Utilizado = 1
		where Token = @Token

		set @Respuesta = 1
	end
	else
		set @Mensaje = 'El token no existe, ya fue utilizado, o se encuentra expirado'
end
go


/*************************** CREACION DE TRIGGERS ***************************/
/*----------------------------------------------------------------------------*/

-- =========================================================
-- TR_DetalleCompra_ActualizarStock: Al registrarse una linea de compra,
-- suma la cantidad comprada al Stock del producto y actualiza sus
-- precios de compra/venta con los valores de esa compra.
-- =========================================================
CREATE TRIGGER TR_DetalleCompra_ActualizarStock
ON DETALLE_COMPRA
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE p
    SET p.Stock = p.Stock + i.Cantidad,
        p.PrecioCompra = i.PrecioCompra,
        p.PrecioVenta = i.PrecioVenta
    FROM PRODUCTO p
    INNER JOIN inserted i ON i.IdProducto = p.IdProducto
END
GO

-- =========================================================
-- TR_DetalleVenta_ActualizarStock: Al registrarse una linea de venta,
-- resta la cantidad vendida del Stock del producto.
-- =========================================================
CREATE TRIGGER TR_DetalleVenta_ActualizarStock
ON DETALLE_VENTA
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE p
    SET p.Stock = p.Stock - i.Cantidad
    FROM PRODUCTO p
    INNER JOIN inserted i ON i.IdProducto = p.IdProducto
END
GO

-- =========================================================
-- TR_Producto_Bitacora: Registra en BITACORA cada INSERT/UPDATE/DELETE
-- realizado sobre la tabla PRODUCTO.
-- =========================================================
CREATE TRIGGER TR_Producto_Bitacora
ON PRODUCTO
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Accion varchar(20)

    IF EXISTS(SELECT * FROM inserted) AND EXISTS(SELECT * FROM deleted)
        SET @Accion = 'UPDATE'
    ELSE IF EXISTS(SELECT * FROM inserted)
        SET @Accion = 'INSERT'
    ELSE
        SET @Accion = 'DELETE'

    IF @Accion = 'DELETE'
        INSERT INTO BITACORA(TablaAfectada, Accion, Detalle)
        SELECT 'PRODUCTO', @Accion, 'IdProducto=' + CAST(IdProducto AS VARCHAR) + ', Nombre=' + Nombre
        FROM deleted
    ELSE
        INSERT INTO BITACORA(TablaAfectada, Accion, Detalle)
        SELECT 'PRODUCTO', @Accion, 'IdProducto=' + CAST(IdProducto AS VARCHAR) + ', Nombre=' + Nombre
        FROM inserted
END
GO

-- =========================================================
-- TR_Usuario_Bitacora: Registra en BITACORA cada INSERT/UPDATE/DELETE
-- realizado sobre la tabla USUARIO.
-- =========================================================
CREATE TRIGGER TR_Usuario_Bitacora
ON USUARIO
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Accion varchar(20)

    IF EXISTS(SELECT * FROM inserted) AND EXISTS(SELECT * FROM deleted)
        SET @Accion = 'UPDATE'
    ELSE IF EXISTS(SELECT * FROM inserted)
        SET @Accion = 'INSERT'
    ELSE
        SET @Accion = 'DELETE'

    IF @Accion = 'DELETE'
        INSERT INTO BITACORA(TablaAfectada, Accion, Detalle)
        SELECT 'USUARIO', @Accion, 'IdUsuario=' + CAST(IdUsuario AS VARCHAR) + ', Nombre=' + NombreCompleto
        FROM deleted
    ELSE
        INSERT INTO BITACORA(TablaAfectada, Accion, IdUsuario, Detalle)
        SELECT 'USUARIO', @Accion, IdUsuario, 'IdUsuario=' + CAST(IdUsuario AS VARCHAR) + ', Nombre=' + NombreCompleto
        FROM inserted
END
GO

-- =========================================================
-- TR_Cliente_Bitacora: Registra en BITACORA cada INSERT/UPDATE/DELETE
-- realizado sobre la tabla CLIENTE.
-- =========================================================
CREATE TRIGGER TR_Cliente_Bitacora
ON CLIENTE
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Accion varchar(20)

    IF EXISTS(SELECT * FROM inserted) AND EXISTS(SELECT * FROM deleted)
        SET @Accion = 'UPDATE'
    ELSE IF EXISTS(SELECT * FROM inserted)
        SET @Accion = 'INSERT'
    ELSE
        SET @Accion = 'DELETE'

    IF @Accion = 'DELETE'
        INSERT INTO BITACORA(TablaAfectada, Accion, Detalle)
        SELECT 'CLIENTE', @Accion, 'IdCliente=' + CAST(IdCliente AS VARCHAR) + ', Nombre=' + NombreCompleto
        FROM deleted
    ELSE
        INSERT INTO BITACORA(TablaAfectada, Accion, Detalle)
        SELECT 'CLIENTE', @Accion, 'IdCliente=' + CAST(IdCliente AS VARCHAR) + ', Nombre=' + NombreCompleto
        FROM inserted
END
GO

-- =========================================================
-- TR_Proveedor_Bitacora: Registra en BITACORA cada INSERT/UPDATE/DELETE
-- realizado sobre la tabla PROVEEDOR.
-- =========================================================
CREATE TRIGGER TR_Proveedor_Bitacora
ON PROVEEDOR
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Accion varchar(20)

    IF EXISTS(SELECT * FROM inserted) AND EXISTS(SELECT * FROM deleted)
        SET @Accion = 'UPDATE'
    ELSE IF EXISTS(SELECT * FROM inserted)
        SET @Accion = 'INSERT'
    ELSE
        SET @Accion = 'DELETE'

    IF @Accion = 'DELETE'
        INSERT INTO BITACORA(TablaAfectada, Accion, Detalle)
        SELECT 'PROVEEDOR', @Accion, 'IdProveedor=' + CAST(IdProveedor AS VARCHAR) + ', RazonSocial=' + RazonSocial
        FROM deleted
    ELSE
        INSERT INTO BITACORA(TablaAfectada, Accion, Detalle)
        SELECT 'PROVEEDOR', @Accion, 'IdProveedor=' + CAST(IdProveedor AS VARCHAR) + ', RazonSocial=' + RazonSocial
        FROM inserted
END
GO

-- =========================================================
-- TR_Compra_Bitacora: Registra en BITACORA cada INSERT/UPDATE/DELETE
-- realizado sobre la tabla COMPRA.
-- =========================================================
CREATE TRIGGER TR_Compra_Bitacora
ON COMPRA
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Accion varchar(20)

    IF EXISTS(SELECT * FROM inserted) AND EXISTS(SELECT * FROM deleted)
        SET @Accion = 'UPDATE'
    ELSE IF EXISTS(SELECT * FROM inserted)
        SET @Accion = 'INSERT'
    ELSE
        SET @Accion = 'DELETE'

    IF @Accion = 'DELETE'
        INSERT INTO BITACORA(TablaAfectada, Accion, Detalle)
        SELECT 'COMPRA', @Accion, 'IdCompra=' + CAST(IdCompra AS VARCHAR) + ', MontoTotal=' + CAST(MontoTotal AS VARCHAR)
        FROM deleted
    ELSE
        INSERT INTO BITACORA(TablaAfectada, Accion, IdUsuario, Detalle)
        SELECT 'COMPRA', @Accion, IdUsuario, 'IdCompra=' + CAST(IdCompra AS VARCHAR) + ', MontoTotal=' + CAST(MontoTotal AS VARCHAR)
        FROM inserted
END
GO

-- =========================================================
-- TR_Venta_Bitacora: Registra en BITACORA cada INSERT/UPDATE/DELETE
-- realizado sobre la tabla VENTA.
-- =========================================================
CREATE TRIGGER TR_Venta_Bitacora
ON VENTA
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Accion varchar(20)

    IF EXISTS(SELECT * FROM inserted) AND EXISTS(SELECT * FROM deleted)
        SET @Accion = 'UPDATE'
    ELSE IF EXISTS(SELECT * FROM inserted)
        SET @Accion = 'INSERT'
    ELSE
        SET @Accion = 'DELETE'

    IF @Accion = 'DELETE'
        INSERT INTO BITACORA(TablaAfectada, Accion, Detalle)
        SELECT 'VENTA', @Accion, 'IdVenta=' + CAST(IdVenta AS VARCHAR) + ', MontoTotal=' + CAST(MontoTotal AS VARCHAR)
        FROM deleted
    ELSE
        INSERT INTO BITACORA(TablaAfectada, Accion, IdUsuario, Detalle)
        SELECT 'VENTA', @Accion, IdUsuario, 'IdVenta=' + CAST(IdVenta AS VARCHAR) + ', MontoTotal=' + CAST(MontoTotal AS VARCHAR)
        FROM inserted
END
GO


/****************** INSERTAMOS REGISTROS A LAS TABLAS ******************/
/*---------------------------------------------------------------------*/

insert into rol (Descripcion)
values('ADMINISTRADOR')

GO

insert into rol (Descripcion)
values('EMPLEADO')

GO

insert into USUARIO(Documento,NombreCompleto,Correo,Clave,IdRol,Estado)
values 
('101010','ADMIN','admin@gmail.com',HASHBYTES('SHA2_256','123'),1,1)

GO

insert into USUARIO(Documento,NombreCompleto,Correo,Clave,IdRol,Estado)
values 
('20','EMPLEADO','empleado@gmail.com',HASHBYTES('SHA2_256','456'),2,1)

GO

-- NOTA: agregamos un cliente de prueba, ya que VENTA ahora exige que
-- exista un IdCliente valido antes de poder registrar cualquier venta.
insert into CLIENTE(Documento,NombreCompleto,Correo,Telefono,Estado)
values
('0801199912345','Cliente de Prueba','cliente@gmail.com','99999999',1)

GO

insert into NEGOCIO(IdNegocio,Nombre,RTN,Direccion,Logo) values
(1,'Lurobu Sublima','08011990123456','Tegucigalpa, Honduras',null)