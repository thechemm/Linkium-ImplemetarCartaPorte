/****** Object:  Table [dbo].[tblConfigAutotransporte]    Script Date: 16/12/2021 04:43:36 p. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblConfigAutotransporte]') AND type in (N'U'))
DROP TABLE [dbo].[tblConfigAutotransporte]
GO
/****** Object:  Table [dbo].[tblConfigAutotransporte]    Script Date: 16/12/2021 04:43:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblConfigAutotransporte](
	[clave] [varchar](100) NULL,
	[descripcion] [varchar](100) NULL,
	[numeroEjes] [varchar](100) NULL,
	[numeroLlantas] [varchar](100) NULL,
	[Remolque] [varchar](100) NULL,
	[fechaInicioVigencia] [varchar](100) NULL,
	[fechaFinVigencia] [varchar](100) NULL
) ON [PRIMARY]
GO
INSERT [dbo].[tblConfigAutotransporte] ([clave], [descripcion], [numeroEjes], [numeroLlantas], [Remolque], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'VL', N'Vehículo ligero de carga (2 llantas en el eje delantero y 2 llantas en el eje trasero)', N'02', N'04', N'"0, 1"', N'12/1/21', N'')
INSERT [dbo].[tblConfigAutotransporte] ([clave], [descripcion], [numeroEjes], [numeroLlantas], [Remolque], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'C2', N'Camión Unitario (2 llantas en el eje delantero y 4 llantas en el eje trasero)', N'02', N'06', N'0', N'01/06/2021', N'')
INSERT [dbo].[tblConfigAutotransporte] ([clave], [descripcion], [numeroEjes], [numeroLlantas], [Remolque], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'C3', N'Camión Unitario (2 llantas en el eje delantero y 6 o 8 llantas en los dos ejes traseros)', N'03', N'8 o 10', N'0', N'01/06/2021', N'')
INSERT [dbo].[tblConfigAutotransporte] ([clave], [descripcion], [numeroEjes], [numeroLlantas], [Remolque], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'C2R2', N'Camión-Remolque (6 llantas en el camión y 8 llantas en remolque)', N'04', N'14', N'1', N'01/06/2021', N'')
INSERT [dbo].[tblConfigAutotransporte] ([clave], [descripcion], [numeroEjes], [numeroLlantas], [Remolque], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'C3R2', N'Camión-Remolque (10 llantas en el camión y 8 llantas en remolque)', N'05', N'18', N'1', N'01/06/2021', N'')
INSERT [dbo].[tblConfigAutotransporte] ([clave], [descripcion], [numeroEjes], [numeroLlantas], [Remolque], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'C2R3', N'Camión-Remolque (6 llantas en el camión y 12 llantas en remolque)', N'05', N'18', N'1', N'01/06/2021', N'')
INSERT [dbo].[tblConfigAutotransporte] ([clave], [descripcion], [numeroEjes], [numeroLlantas], [Remolque], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'C3R3', N'Camión-Remolque (10 llantas en el camión y 12 llantas en remolque)', N'06', N'22', N'1', N'01/06/2021', N'')
INSERT [dbo].[tblConfigAutotransporte] ([clave], [descripcion], [numeroEjes], [numeroLlantas], [Remolque], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'T2S1', N'"Tractocamión Articulado (6 llantas en el tractocamión, 4 llantas en el semirremolque)"', N'03', N'10', N'1', N'01/06/2021', N'')
INSERT [dbo].[tblConfigAutotransporte] ([clave], [descripcion], [numeroEjes], [numeroLlantas], [Remolque], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'T2S2', N'"Tractocamión Articulado (6 llantas en el tractocamión, 8 llantas en el semirremolque)"', N'04', N'14', N'1', N'01/06/2021', N'')
INSERT [dbo].[tblConfigAutotransporte] ([clave], [descripcion], [numeroEjes], [numeroLlantas], [Remolque], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'T2S3', N'"Tractocamión Articulado (6 llantas en el tractocamión, 12 llantas en el semirremolque)"', N'05', N'18', N'1', N'01/06/2021', N'')
INSERT [dbo].[tblConfigAutotransporte] ([clave], [descripcion], [numeroEjes], [numeroLlantas], [Remolque], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'T3S1', N'"Tractocamión Articulado (10 llantas en el tractocamión, 4 llantas en el semirremolque)"', N'04', N'14', N'1', N'01/06/2021', N'')
INSERT [dbo].[tblConfigAutotransporte] ([clave], [descripcion], [numeroEjes], [numeroLlantas], [Remolque], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'T3S2', N'"Tractocamión Articulado (10 llantas en el tractocamión, 8 llantas en el semirremolque)"', N'05', N'18', N'1', N'01/06/2021', N'')
INSERT [dbo].[tblConfigAutotransporte] ([clave], [descripcion], [numeroEjes], [numeroLlantas], [Remolque], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'T3S3', N'"Tractocamión Articulado (10 llantas en el tractocamión, 12 llantas en el semirremolque)"', N'06', N'22', N'1', N'01/06/2021', N'')
INSERT [dbo].[tblConfigAutotransporte] ([clave], [descripcion], [numeroEjes], [numeroLlantas], [Remolque], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'OTROEVGP', N'Especializado de carga Voluminosa y/o Gran Peso', N'', N'', N'"0, 1"', N'01/12/2021', N'')
INSERT [dbo].[tblConfigAutotransporte] ([clave], [descripcion], [numeroEjes], [numeroLlantas], [Remolque], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'OTROSG', N'Servicio de Grúas', N'', N'', N'0', N'01/06/2021', N'')
INSERT [dbo].[tblConfigAutotransporte] ([clave], [descripcion], [numeroEjes], [numeroLlantas], [Remolque], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'GPLUTA', N'Grúa de Pluma Tipo A', N'', N'', N'0', N'01/12/2021', N'')
INSERT [dbo].[tblConfigAutotransporte] ([clave], [descripcion], [numeroEjes], [numeroLlantas], [Remolque], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'GPLUTB', N'Grúa de Pluma Tipo B', N'', N'', N'0', N'01/12/2021', N'')
INSERT [dbo].[tblConfigAutotransporte] ([clave], [descripcion], [numeroEjes], [numeroLlantas], [Remolque], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'GPLUTC', N'Grúa de Pluma Tipo C', N'', N'', N'0', N'01/12/2021', N'')
INSERT [dbo].[tblConfigAutotransporte] ([clave], [descripcion], [numeroEjes], [numeroLlantas], [Remolque], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'GPLUTD', N'Grúa de Pluma Tipo D', N'', N'', N'0', N'01/12/2021', N'')
INSERT [dbo].[tblConfigAutotransporte] ([clave], [descripcion], [numeroEjes], [numeroLlantas], [Remolque], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'GPLATA', N'Grúa de Plataforma Tipo A', N'', N'', N'0', N'01/12/2021', N'')
INSERT [dbo].[tblConfigAutotransporte] ([clave], [descripcion], [numeroEjes], [numeroLlantas], [Remolque], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'GPLATB', N'Grúa de Plataforma Tipo B', N'', N'', N'0', N'01/12/2021', N'')
INSERT [dbo].[tblConfigAutotransporte] ([clave], [descripcion], [numeroEjes], [numeroLlantas], [Remolque], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'GPLATC', N'Grúa de Plataforma Tipo C', N'', N'', N'0', N'01/12/2021', N'')
INSERT [dbo].[tblConfigAutotransporte] ([clave], [descripcion], [numeroEjes], [numeroLlantas], [Remolque], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'GPLATD', N'Grúa de Plataforma Tipo D', N'', N'', N'0', N'01/12/2021', N'')
GO
