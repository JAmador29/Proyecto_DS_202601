/****** Objeto: Database [LOBORUSYSTEMDB] Fecha de script: 14/7/2026 22:59:29 ******/
CREATE DATABASE [LOBORUSYSTEMDB]  (EDITION = 'Basic', SERVICE_OBJECTIVE = 'Basic', MAXSIZE = 2 GB) WITH CATALOG_COLLATION = SQL_Latin1_General_CP1_CI_AS, LEDGER = OFF;
GO
ALTER DATABASE [LOBORUSYSTEMDB] SET COMPATIBILITY_LEVEL = 160
GO
ALTER DATABASE [LOBORUSYSTEMDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LOBORUSYSTEMDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LOBORUSYSTEMDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LOBORUSYSTEMDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LOBORUSYSTEMDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [LOBORUSYSTEMDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LOBORUSYSTEMDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LOBORUSYSTEMDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LOBORUSYSTEMDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LOBORUSYSTEMDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LOBORUSYSTEMDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LOBORUSYSTEMDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LOBORUSYSTEMDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LOBORUSYSTEMDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LOBORUSYSTEMDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LOBORUSYSTEMDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [LOBORUSYSTEMDB] SET  MULTI_USER 
GO
ALTER DATABASE [LOBORUSYSTEMDB] SET AUTOMATIC_INDEX_COMPACTION = OFF 
GO
ALTER DATABASE [LOBORUSYSTEMDB] SET ENCRYPTION ON
GO
ALTER DATABASE [LOBORUSYSTEMDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [LOBORUSYSTEMDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
/****** Objeto: UserDefinedTableType [dbo].[EDetalle_Compra] Fecha de script: 14/7/2026 22:59:29 ******/
CREATE TYPE [dbo].[EDetalle_Compra] AS TABLE(
	[IdProducto] [int] NULL,
	[PrecioCompra] [decimal](18, 2) NULL,
	[PrecioVenta] [decimal](18, 2) NULL,
	[Cantidad] [int] NULL,
	[MontoTotal] [decimal](18, 2) NULL
)
GO
/****** Objeto: UserDefinedTableType [dbo].[EDetalle_Venta] Fecha de script: 14/7/2026 22:59:29 ******/
CREATE TYPE [dbo].[EDetalle_Venta] AS TABLE(
	[IdProducto] [int] NULL,
	[PrecioVenta] [decimal](18, 2) NULL,
	[Cantidad] [int] NULL,
	[SubTotal] [decimal](18, 2) NULL
)
GO
/****** Objeto: Table [dbo].[BITACORA] Fecha de script: 14/7/2026 22:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BITACORA](
	[IdBitacora] [int] IDENTITY(1,1) NOT NULL,
	[TablaAfectada] [varchar](100) NOT NULL,
	[Accion] [varchar](20) NOT NULL,
	[IdUsuario] [int] NULL,
	[Detalle] [varchar](max) NULL,
	[FechaRegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdBitacora] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[CATEGORIA] Fecha de script: 14/7/2026 22:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CATEGORIA](
	[IdCategoria] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](100) NULL,
	[Estado] [bit] NULL,
	[FechaRegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCategoria] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[CLIENTE] Fecha de script: 14/7/2026 22:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CLIENTE](
	[IdCliente] [int] IDENTITY(1,1) NOT NULL,
	[Documento] [varchar](13) NULL,
	[NombreCompleto] [varchar](50) NULL,
	[Correo] [varchar](50) NULL,
	[Telefono] [varchar](50) NULL,
	[Estado] [bit] NULL,
	[FechaRegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCliente] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[COMPRA] Fecha de script: 14/7/2026 22:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[COMPRA](
	[IdCompra] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NULL,
	[IdProveedor] [int] NULL,
	[TipoDocumento] [varchar](50) NULL,
	[NumeroDocumento] [varchar](50) NULL,
	[MontoTotal] [decimal](10, 2) NULL,
	[FechaRegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCompra] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[DETALLE_COMPRA] Fecha de script: 14/7/2026 22:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DETALLE_COMPRA](
	[IdDetalleCompra] [int] IDENTITY(1,1) NOT NULL,
	[IdCompra] [int] NULL,
	[IdProducto] [int] NULL,
	[PrecioCompra] [decimal](10, 2) NULL,
	[PrecioVenta] [decimal](10, 2) NULL,
	[Cantidad] [int] NULL,
	[MontoTotal] [decimal](10, 2) NULL,
	[FechaRegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdDetalleCompra] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[DETALLE_VENTA] Fecha de script: 14/7/2026 22:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DETALLE_VENTA](
	[IdDetalleVenta] [int] IDENTITY(1,1) NOT NULL,
	[IdVenta] [int] NULL,
	[IdProducto] [int] NULL,
	[PrecioVenta] [decimal](10, 2) NULL,
	[Cantidad] [int] NULL,
	[SubTotal] [decimal](10, 2) NULL,
	[FechaRegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdDetalleVenta] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[NEGOCIO] Fecha de script: 14/7/2026 22:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NEGOCIO](
	[IdNegocio] [int] NOT NULL,
	[Nombre] [varchar](60) NULL,
	[RTN] [varchar](14) NULL,
	[Direccion] [varchar](200) NULL,
	[Logo] [varbinary](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdNegocio] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[PERMISO] Fecha de script: 14/7/2026 22:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PERMISO](
	[IdPermiso] [int] IDENTITY(1,1) NOT NULL,
	[IdRol] [int] NULL,
	[NombreMenu] [varchar](100) NULL,
	[FechaRegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPermiso] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[PRODUCTO] Fecha de script: 14/7/2026 22:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PRODUCTO](
	[IdProducto] [int] IDENTITY(1,1) NOT NULL,
	[Codigo] [varchar](50) NULL,
	[Nombre] [varchar](50) NULL,
	[Descripcion] [varchar](50) NULL,
	[IdCategoria] [int] NULL,
	[Stock] [int] NOT NULL,
	[PrecioCompra] [decimal](10, 2) NULL,
	[PrecioVenta] [decimal](10, 2) NULL,
	[Estado] [bit] NULL,
	[FechaRegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdProducto] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[PROVEEDOR] Fecha de script: 14/7/2026 22:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PROVEEDOR](
	[IdProveedor] [int] IDENTITY(1,1) NOT NULL,
	[RTN] [varchar](14) NULL,
	[RazonSocial] [varchar](50) NULL,
	[Correo] [varchar](50) NULL,
	[Telefono] [varchar](50) NULL,
	[Estado] [bit] NULL,
	[FechaRegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdProveedor] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[ROL] Fecha de script: 14/7/2026 22:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ROL](
	[IdRol] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](50) NULL,
	[FechaRegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[TOKEN_AUTENTICACION] Fecha de script: 14/7/2026 22:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TOKEN_AUTENTICACION](
	[IdToken] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[Token] [varchar](255) NOT NULL,
	[Tipo] [varchar](20) NOT NULL,
	[FechaExpiracion] [datetime] NOT NULL,
	[Utilizado] [bit] NOT NULL,
	[FechaRegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdToken] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[USUARIO] Fecha de script: 14/7/2026 22:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USUARIO](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Documento] [varchar](13) NULL,
	[NombreCompleto] [varchar](50) NULL,
	[Correo] [varchar](50) NULL,
	[Clave] [nvarchar](255) NULL,
	[IdRol] [int] NULL,
	[Estado] [bit] NULL,
	[FechaRegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[VENTA] Fecha de script: 14/7/2026 22:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VENTA](
	[IdVenta] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NULL,
	[IdCliente] [int] NOT NULL,
	[TipoDocumento] [varchar](50) NULL,
	[NumeroDocumento] [varchar](50) NULL,
	[MetodoPago] [varchar](20) NULL,
	[MontoPago] [decimal](10, 2) NULL,
	[MontoCambio] [decimal](10, 2) NULL,
	[MontoTotal] [decimal](10, 2) NULL,
	[FechaRegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdVenta] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BITACORA] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[CATEGORIA] ADD  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[CATEGORIA] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[CLIENTE] ADD  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[CLIENTE] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[COMPRA] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[DETALLE_COMPRA] ADD  DEFAULT ((0)) FOR [PrecioCompra]
GO
ALTER TABLE [dbo].[DETALLE_COMPRA] ADD  DEFAULT ((0)) FOR [PrecioVenta]
GO
ALTER TABLE [dbo].[DETALLE_COMPRA] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[DETALLE_VENTA] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[PERMISO] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[PRODUCTO] ADD  DEFAULT ((0)) FOR [Stock]
GO
ALTER TABLE [dbo].[PRODUCTO] ADD  DEFAULT ((0)) FOR [PrecioCompra]
GO
ALTER TABLE [dbo].[PRODUCTO] ADD  DEFAULT ((0)) FOR [PrecioVenta]
GO
ALTER TABLE [dbo].[PRODUCTO] ADD  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[PRODUCTO] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[PROVEEDOR] ADD  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[PROVEEDOR] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[ROL] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[TOKEN_AUTENTICACION] ADD  DEFAULT ((0)) FOR [Utilizado]
GO
ALTER TABLE [dbo].[TOKEN_AUTENTICACION] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[USUARIO] ADD  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[USUARIO] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[VENTA] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[BITACORA]  WITH CHECK ADD FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[USUARIO] ([IdUsuario])
GO
ALTER TABLE [dbo].[COMPRA]  WITH CHECK ADD FOREIGN KEY([IdProveedor])
REFERENCES [dbo].[PROVEEDOR] ([IdProveedor])
GO
ALTER TABLE [dbo].[COMPRA]  WITH CHECK ADD FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[USUARIO] ([IdUsuario])
GO
ALTER TABLE [dbo].[DETALLE_COMPRA]  WITH CHECK ADD FOREIGN KEY([IdCompra])
REFERENCES [dbo].[COMPRA] ([IdCompra])
GO
ALTER TABLE [dbo].[DETALLE_COMPRA]  WITH CHECK ADD FOREIGN KEY([IdProducto])
REFERENCES [dbo].[PRODUCTO] ([IdProducto])
GO
ALTER TABLE [dbo].[DETALLE_VENTA]  WITH CHECK ADD FOREIGN KEY([IdProducto])
REFERENCES [dbo].[PRODUCTO] ([IdProducto])
GO
ALTER TABLE [dbo].[DETALLE_VENTA]  WITH CHECK ADD FOREIGN KEY([IdVenta])
REFERENCES [dbo].[VENTA] ([IdVenta])
GO
ALTER TABLE [dbo].[PERMISO]  WITH CHECK ADD FOREIGN KEY([IdRol])
REFERENCES [dbo].[ROL] ([IdRol])
GO
ALTER TABLE [dbo].[PRODUCTO]  WITH CHECK ADD FOREIGN KEY([IdCategoria])
REFERENCES [dbo].[CATEGORIA] ([IdCategoria])
GO
ALTER TABLE [dbo].[TOKEN_AUTENTICACION]  WITH CHECK ADD FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[USUARIO] ([IdUsuario])
GO
ALTER TABLE [dbo].[USUARIO]  WITH CHECK ADD FOREIGN KEY([IdRol])
REFERENCES [dbo].[ROL] ([IdRol])
GO
ALTER TABLE [dbo].[VENTA]  WITH CHECK ADD FOREIGN KEY([IdCliente])
REFERENCES [dbo].[CLIENTE] ([IdCliente])
GO
ALTER TABLE [dbo].[VENTA]  WITH CHECK ADD FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[USUARIO] ([IdUsuario])
GO
ALTER TABLE [dbo].[BITACORA]  WITH CHECK ADD CHECK  (([Accion]='DELETE' OR [Accion]='UPDATE' OR [Accion]='INSERT'))
GO
ALTER TABLE [dbo].[CATEGORIA]  WITH CHECK ADD CHECK  (([Estado]=(1) OR [Estado]=(0)))
GO
ALTER TABLE [dbo].[CLIENTE]  WITH CHECK ADD CHECK  (([Correo] like '_%@_%._%'))
GO
ALTER TABLE [dbo].[CLIENTE]  WITH CHECK ADD CHECK  (([Estado]=(1) OR [Estado]=(0)))
GO
ALTER TABLE [dbo].[DETALLE_COMPRA]  WITH CHECK ADD  CONSTRAINT [CK_DetalleCompra_MontoTotal] CHECK  (([MontoTotal]=[Cantidad]*[PrecioCompra]))
GO
ALTER TABLE [dbo].[DETALLE_COMPRA] CHECK CONSTRAINT [CK_DetalleCompra_MontoTotal]
GO
ALTER TABLE [dbo].[DETALLE_VENTA]  WITH CHECK ADD  CONSTRAINT [CK_DetalleVenta_SubTotal] CHECK  (([SubTotal]=[Cantidad]*[PrecioVenta]))
GO
ALTER TABLE [dbo].[DETALLE_VENTA] CHECK CONSTRAINT [CK_DetalleVenta_SubTotal]
GO
ALTER TABLE [dbo].[PRODUCTO]  WITH CHECK ADD CHECK  (([Estado]=(1) OR [Estado]=(0)))
GO
ALTER TABLE [dbo].[PROVEEDOR]  WITH CHECK ADD CHECK  (([Correo] like '_%@_%._%'))
GO
ALTER TABLE [dbo].[PROVEEDOR]  WITH CHECK ADD CHECK  (([Estado]=(1) OR [Estado]=(0)))
GO
ALTER TABLE [dbo].[TOKEN_AUTENTICACION]  WITH CHECK ADD CHECK  (([Utilizado]=(1) OR [Utilizado]=(0)))
GO
ALTER TABLE [dbo].[TOKEN_AUTENTICACION]  WITH CHECK ADD CHECK  (([Tipo]='RECUPERACION_CLAVE' OR [Tipo]='LOGIN'))
GO
ALTER TABLE [dbo].[USUARIO]  WITH CHECK ADD CHECK  (([Correo] like '_%@_%._%'))
GO
ALTER TABLE [dbo].[USUARIO]  WITH CHECK ADD CHECK  (([Estado]=(1) OR [Estado]=(0)))
GO
ALTER TABLE [dbo].[VENTA]  WITH CHECK ADD CHECK  (([MetodoPago]='Transferencia' OR [MetodoPago]='Tarjeta' OR [MetodoPago]='Efectivo'))
GO
ALTER TABLE [dbo].[VENTA]  WITH CHECK ADD  CONSTRAINT [CK_Venta_MontoCambio] CHECK  (([MontoCambio]=([MontoPago]-[MontoTotal])))
GO
ALTER TABLE [dbo].[VENTA] CHECK CONSTRAINT [CK_Venta_MontoCambio]
GO
/****** Objeto: StoredProcedure [dbo].[sp_CerrarSesion] Fecha de script: 14/7/2026 22:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- sp_CerrarSesion: marca un token de LOGIN como utilizado (logout).
CREATE PROC [dbo].[sp_CerrarSesion](
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
GO
/****** Objeto: StoredProcedure [dbo].[sp_EditarCategoria] Fecha de script: 14/7/2026 22:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create procedure [dbo].[sp_EditarCategoria](
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
GO
/****** Objeto: StoredProcedure [dbo].[SP_EDITARUSUARIO] Fecha de script: 14/7/2026 22:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_EDITARUSUARIO](
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
        update usuario set
        Documento = @Documento,
        NombreCompleto = @NombreCompleto,
        Correo = @Correo,
        Clave = @Clave,
        IdRol = @IdRol,
        Estado = @Estado
        where IdUsuario = @IdUsuario

        set @Respuesta = 1
    end
    else
        set @Mensaje = 'No se puede repetir el documento para más de un usuario'
end
GO
/****** Objeto: StoredProcedure [dbo].[sp_EliminarCategoria] Fecha de script: 14/7/2026 22:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[sp_EliminarCategoria](
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
/****** Objeto: StoredProcedure [dbo].[sp_EliminarCliente] Fecha de script: 14/7/2026 22:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- sp_EliminarCliente: NUEVO. Antes no existia porque CLIENTE no tenia
-- relaciones; ahora que VENTA depende de CLIENTE, se necesita esta
-- validacion para no dejar ventas huerfanas o violar la FK.
create procedure [dbo].[sp_EliminarCliente](
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
GO
/****** Objeto: StoredProcedure [dbo].[SP_EliminarProducto] Fecha de script: 14/7/2026 22:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROC [dbo].[SP_EliminarProducto](
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
GO
/****** Objeto: StoredProcedure [dbo].[sp_EliminarProveedor] Fecha de script: 14/7/2026 22:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[sp_EliminarProveedor](
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
GO
/****** Objeto: StoredProcedure [dbo].[SP_ELIMINARUSUARIO] Fecha de script: 14/7/2026 22:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROC [dbo].[SP_ELIMINARUSUARIO](
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
GO
/****** Objeto: StoredProcedure [dbo].[sp_GenerarTokenLogin] Fecha de script: 14/7/2026 22:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* ---------- PROCEDIMIENTOS PARA TOKEN_AUTENTICACION -----------------*/

-- sp_GenerarTokenLogin: al iniciar sesion correctamente, invalida
-- cualquier token de tipo LOGIN anterior de ese usuario (regla de
-- "solo un token activo a la vez") y genera uno nuevo.
CREATE PROC [dbo].[sp_GenerarTokenLogin](
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
GO
/****** Objeto: StoredProcedure [dbo].[sp_ModificarCliente] Fecha de script: 14/7/2026 22:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_ModificarCliente](
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
    SET @Mensaje = ''

    IF NOT EXISTS (SELECT * FROM CLIENTE WHERE Documento = @Documento and IdCliente != @IdCliente)
    begin
        update CLIENTE set
        Documento = @Documento,
        NombreCompleto = @NombreCompleto,
        Correo = @Correo,
        Telefono = @Telefono,
        Estado = @Estado
        where IdCliente = @IdCliente

        set @Mensaje = 'Cliente actualizado correctamente'
    end
    else
    begin
        SET @Resultado = 0
        set @Mensaje = 'El numero de documento ya existe'
    end
end
GO
/****** Objeto: StoredProcedure [dbo].[sp_ModificarProducto] Fecha de script: 14/7/2026 22:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[sp_ModificarProducto](
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
GO
/****** Objeto: StoredProcedure [dbo].[sp_ModificarProveedor] Fecha de script: 14/7/2026 22:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ModificarProveedor]
    @IdProveedor int,
    @RTN varchar(50),
    @RazonSocial varchar(100),
    @Correo varchar(100),
    @Telefono varchar(50),
    @Estado bit,
    @Resultado bit output,
    @Mensaje varchar(500) output
AS
BEGIN
    SET @Resultado = 1
    IF NOT EXISTS (SELECT * FROM PROVEEDOR WHERE RTN = @RTN AND IdProveedor != @IdProveedor)
    BEGIN
        UPDATE PROVEEDOR SET
        RTN = @RTN,
        RazonSocial = @RazonSocial,
        Correo = @Correo,
        Telefono = @Telefono,
        Estado = @Estado
        WHERE IdProveedor = @IdProveedor
    END
    ELSE
    BEGIN
        SET @Resultado = 0
        SET @Mensaje = 'El RTN ya existe en otro proveedor.'
    END
END
GO
/****** Objeto: StoredProcedure [dbo].[SP_RegistrarCategoria] Fecha de script: 14/7/2026 22:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* ---------- PROCEDIMIENTOS PARA CATEGORIA -----------------*/

create PROC [dbo].[SP_RegistrarCategoria](
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
GO
/****** Objeto: StoredProcedure [dbo].[sp_RegistrarCliente] Fecha de script: 14/7/2026 22:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_RegistrarCliente](
    @Documento varchar(50),
    @NombreCompleto varchar(50),
    @Correo varchar(50),
    @Telefono varchar(50),
    @Estado bit,
    @Resultado int output,
    @Mensaje varchar(500) output
) as
begin
    SET @Resultado = 0
    SET @Mensaje = ''

    IF NOT EXISTS (SELECT * FROM CLIENTE WHERE Documento = @Documento)
    begin
        insert into CLIENTE(Documento,NombreCompleto,Correo,Telefono,Estado) 
        values (@Documento,@NombreCompleto,@Correo,@Telefono,@Estado)

        set @Resultado = SCOPE_IDENTITY()
        set @Mensaje = 'Cliente registrado correctamente'
    end
    else
        set @Mensaje = 'El numero de documento ya existe'
end
GO
/****** Objeto: StoredProcedure [dbo].[sp_RegistrarCompra] Fecha de script: 14/7/2026 22:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- NOTA: este SP YA NO actualiza el Stock/Precios manualmente.
-- Esa logica ahora la hace el trigger TR_DetalleCompra_ActualizarStock
-- al insertarse filas en DETALLE_COMPRA, para evitar duplicar la suma.
-- IMPORTANTE: el MontoTotal que envia @DetalleCompra para cada producto
-- debe venir calculado como Cantidad * PrecioCompra desde C#, o el INSERT
-- fallara por el CHECK CONSTRAINT CK_DetalleCompra_MontoTotal.
CREATE PROCEDURE [dbo].[sp_RegistrarCompra](
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
/****** Objeto: StoredProcedure [dbo].[sp_RegistrarProducto] Fecha de script: 14/7/2026 22:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* ---------- PROCEDIMIENTOS PARA PRODUCTO -----------------*/

create PROC [dbo].[sp_RegistrarProducto](
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
/****** Objeto: StoredProcedure [dbo].[sp_RegistrarProveedor] Fecha de script: 14/7/2026 22:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_RegistrarProveedor](
    @RTN varchar(50),
    @RazonSocial varchar(50),
    @Correo varchar(50),
    @Telefono varchar(50),
    @Estado bit,
    @Resultado int output,
    @Mensaje varchar(500) output
) as
begin
    SET @Resultado = 0
    SET @Mensaje = ''

    IF NOT EXISTS (SELECT * FROM PROVEEDOR WHERE RTN = @RTN)
    begin
        insert into PROVEEDOR(RTN,RazonSocial,Correo,Telefono,Estado) 
        values (@RTN,@RazonSocial,@Correo,@Telefono,@Estado)

        set @Resultado = SCOPE_IDENTITY()
        set @Mensaje = 'Proveedor registrado correctamente'
    end
    else
        set @Mensaje = 'El RTN ya existe'
end
GO
/****** Objeto: StoredProcedure [dbo].[SP_REGISTRARUSUARIO] Fecha de script: 14/7/2026 22:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_REGISTRARUSUARIO](
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
        (@Documento,@NombreCompleto,@Correo,@Clave,@IdRol,@Estado)

        set @IdUsuarioResultado = SCOPE_IDENTITY()
    end
    else
        set @Mensaje = 'No se puede repetir el documento para más de un usuario'
end
GO
/****** Objeto: StoredProcedure [dbo].[sp_ReporteCompras] Fecha de script: 14/7/2026 22:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROC [dbo].[sp_ReporteCompras](
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
 pr.RTN[RTN],pr.RazonSocial,
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
GO
/****** Objeto: StoredProcedure [dbo].[sp_ReporteVentas] Fecha de script: 14/7/2026 22:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROC [dbo].[sp_ReporteVentas]
(
    @FechaInicio DATE,
    @FechaFin DATE
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        CONVERT(CHAR(10), v.FechaRegistro, 103) AS FechaRegistro,
        v.TipoDocumento,
        v.NumeroDocumento,
        v.MontoTotal,

        u.NombreCompleto AS UsuarioRegistro,

        c.Documento AS DocumentoCliente,
        c.NombreCompleto AS NombreCliente,

        v.MetodoPago,

        p.Codigo AS CodigoProducto,
        p.Nombre AS NombreProducto,
        ca.Descripcion AS Categoria,

        dv.PrecioVenta,
        dv.Cantidad,
        dv.SubTotal

    FROM VENTA AS v

    INNER JOIN USUARIO AS u
        ON u.IdUsuario = v.IdUsuario

    INNER JOIN CLIENTE AS c
        ON c.IdCliente = v.IdCliente

    INNER JOIN DETALLE_VENTA AS dv
        ON dv.IdVenta = v.IdVenta

    INNER JOIN PRODUCTO AS p
        ON p.IdProducto = dv.IdProducto

    INNER JOIN CATEGORIA AS ca
        ON ca.IdCategoria = p.IdCategoria

    WHERE v.FechaRegistro >= @FechaInicio
      AND v.FechaRegistro < DATEADD(DAY, 1, @FechaFin);
END;

GO
/****** Objeto: StoredProcedure [dbo].[sp_RestablecerClave] Fecha de script: 14/7/2026 22:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- sp_RestablecerClave: valida el token de recuperacion, actualiza la
-- clave (cifrada con HASHBYTES) y marca el token como utilizado para
-- que no pueda reutilizarse.
CREATE PROC [dbo].[sp_RestablecerClave](
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
GO
/****** Objeto: StoredProcedure [dbo].[sp_SolicitarRecuperacionClave] Fecha de script: 14/7/2026 22:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- sp_SolicitarRecuperacionClave: genera un token de un solo uso para
-- resetear la clave, invalidando cualquier token de recuperacion
-- anterior que el usuario tuviera pendiente.
CREATE PROC [dbo].[sp_SolicitarRecuperacionClave](
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
GO
/****** Objeto: StoredProcedure [dbo].[sp_ValidarToken] Fecha de script: 14/7/2026 22:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- sp_ValidarToken: verifica si un token (de cualquier tipo) sigue
-- siendo valido: existe, no fue usado, y no ha expirado.
CREATE PROC [dbo].[sp_ValidarToken](
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
GO
/****** Objeto: StoredProcedure [dbo].[usp_RegistrarVenta] Fecha de script: 14/7/2026 22:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[usp_RegistrarVenta]
(
    @IdUsuario INT,
    @IdCliente INT,
    @TipoDocumento VARCHAR(50),
    @NumeroDocumento VARCHAR(50),
    @MetodoPago VARCHAR(20),
    @MontoPago DECIMAL(18,2),
    @MontoCambio DECIMAL(18,2),
    @MontoTotal DECIMAL(18,2),
    @DetalleVenta dbo.EDetalle_Venta READONLY,
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        DECLARE @IdVenta INT;

        SET @Resultado = 1;
        SET @Mensaje = '';

        BEGIN TRANSACTION;

        INSERT INTO VENTA
        (
            IdUsuario,
            IdCliente,
            TipoDocumento,
            NumeroDocumento,
            MetodoPago,
            MontoPago,
            MontoCambio,
            MontoTotal
        )
        VALUES
        (
            @IdUsuario,
            @IdCliente,
            @TipoDocumento,
            @NumeroDocumento,
            @MetodoPago,
            @MontoPago,
            @MontoCambio,
            @MontoTotal
        );

        SET @IdVenta = SCOPE_IDENTITY();

        INSERT INTO DETALLE_VENTA
        (
            IdVenta,
            IdProducto,
            PrecioVenta,
            Cantidad,
            SubTotal
        )
        SELECT
            @IdVenta,
            IdProducto,
            PrecioVenta,
            Cantidad,
            SubTotal
        FROM @DetalleVenta;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;
GO
ALTER DATABASE [LOBORUSYSTEMDB] SET  READ_WRITE 
GO
