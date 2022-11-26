/****** Object:  Table [dbo].[tblSubTipoRem]    Script Date: 16/12/2021 05:19:47 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblSubTipoRem](
	[clave] [varchar](100) NULL,
	[descripcion] [varchar](100) NULL,
	[fechaInicioVigencia] [datetime] NULL,
	[fechaFinVigencia] [varchar](100) NULL
) ON [PRIMARY]
GO
INSERT [dbo].[tblSubTipoRem] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'CTR001', N'Caballete', CAST(N'2021-01-06T00:00:00.000' AS DateTime), N'')
GO
INSERT [dbo].[tblSubTipoRem] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'CTR002', N'Caja', CAST(N'2021-01-06T00:00:00.000' AS DateTime), N'')
GO
INSERT [dbo].[tblSubTipoRem] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'CTR003', N'Caja Abierta', CAST(N'2021-01-06T00:00:00.000' AS DateTime), N'')
GO
INSERT [dbo].[tblSubTipoRem] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'CTR004', N'Caja Cerrada', CAST(N'2021-01-06T00:00:00.000' AS DateTime), N'')
GO
INSERT [dbo].[tblSubTipoRem] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'CTR005', N'Caja De Recolección Con Cargador Frontal', CAST(N'2021-01-06T00:00:00.000' AS DateTime), N'')
GO
INSERT [dbo].[tblSubTipoRem] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'CTR006', N'Caja Refrigerada', CAST(N'2021-01-06T00:00:00.000' AS DateTime), N'')
GO
INSERT [dbo].[tblSubTipoRem] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'CTR007', N'Caja Seca', CAST(N'2021-01-06T00:00:00.000' AS DateTime), N'')
GO
INSERT [dbo].[tblSubTipoRem] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'CTR008', N'Caja Transferencia', CAST(N'2021-01-06T00:00:00.000' AS DateTime), N'')
GO
INSERT [dbo].[tblSubTipoRem] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'CTR009', N'Cama Baja o Cuello Ganso', CAST(N'2021-01-06T00:00:00.000' AS DateTime), N'')
GO
INSERT [dbo].[tblSubTipoRem] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'CTR010', N'Chasis Portacontenedor', CAST(N'2021-01-06T00:00:00.000' AS DateTime), N'')
GO
INSERT [dbo].[tblSubTipoRem] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'CTR011', N'Convencional De Chasis', CAST(N'2021-01-06T00:00:00.000' AS DateTime), N'')
GO
INSERT [dbo].[tblSubTipoRem] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'CTR012', N'Equipo Especial', CAST(N'2021-01-06T00:00:00.000' AS DateTime), N'')
GO
INSERT [dbo].[tblSubTipoRem] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'CTR013', N'Estacas', CAST(N'2021-01-06T00:00:00.000' AS DateTime), N'')
GO
INSERT [dbo].[tblSubTipoRem] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'CTR014', N'Góndola Madrina', CAST(N'2021-01-06T00:00:00.000' AS DateTime), N'')
GO
INSERT [dbo].[tblSubTipoRem] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'CTR015', N'Grúa Industrial', CAST(N'2021-01-06T00:00:00.000' AS DateTime), N'')
GO
INSERT [dbo].[tblSubTipoRem] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'CTR016', N'Grúa ', CAST(N'2021-01-06T00:00:00.000' AS DateTime), N'')
GO
INSERT [dbo].[tblSubTipoRem] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'CTR017', N'Integral', CAST(N'2021-01-06T00:00:00.000' AS DateTime), N'')
GO
INSERT [dbo].[tblSubTipoRem] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'CTR018', N'Jaula', CAST(N'2021-01-06T00:00:00.000' AS DateTime), N'')
GO
INSERT [dbo].[tblSubTipoRem] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'CTR019', N'Media Redila', CAST(N'2021-01-06T00:00:00.000' AS DateTime), N'')
GO
INSERT [dbo].[tblSubTipoRem] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'CTR020', N'Pallet o Celdillas', CAST(N'2021-01-06T00:00:00.000' AS DateTime), N'')
GO
INSERT [dbo].[tblSubTipoRem] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'CTR021', N'Plataforma', CAST(N'2021-01-06T00:00:00.000' AS DateTime), N'')
GO
INSERT [dbo].[tblSubTipoRem] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'CTR022', N'Plataforma Con Grúa', CAST(N'2021-01-06T00:00:00.000' AS DateTime), N'')
GO
INSERT [dbo].[tblSubTipoRem] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'CTR023', N'Plataforma Encortinada', CAST(N'2021-01-06T00:00:00.000' AS DateTime), N'')
GO
INSERT [dbo].[tblSubTipoRem] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'CTR024', N'Redilas', CAST(N'2021-01-06T00:00:00.000' AS DateTime), N'')
GO
INSERT [dbo].[tblSubTipoRem] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'CTR025', N'Refrigerador', CAST(N'2021-01-06T00:00:00.000' AS DateTime), N'')
GO
INSERT [dbo].[tblSubTipoRem] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'CTR026', N'Revolvedora', CAST(N'2021-01-06T00:00:00.000' AS DateTime), N'')
GO
INSERT [dbo].[tblSubTipoRem] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'CTR027', N'Semicaja', CAST(N'2021-01-06T00:00:00.000' AS DateTime), N'')
GO
INSERT [dbo].[tblSubTipoRem] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'CTR028', N'Tanque', CAST(N'2021-01-06T00:00:00.000' AS DateTime), N'')
GO
INSERT [dbo].[tblSubTipoRem] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'CTR029', N'Tolva', CAST(N'2021-01-06T00:00:00.000' AS DateTime), N'')
GO
INSERT [dbo].[tblSubTipoRem] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'CTR031', N'Volteo', CAST(N'2021-01-06T00:00:00.000' AS DateTime), N'')
GO
INSERT [dbo].[tblSubTipoRem] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'CTR032', N'Volteo Desmontable', CAST(N'2021-01-06T00:00:00.000' AS DateTime), N'')
GO
