
/****** Object:  Table [dbo].[tblTipoPermiso]    Script Date: 16/12/2021 04:43:36 p. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblTipoPermiso]') AND type in (N'U'))
DROP TABLE [dbo].[tblTipoPermiso]
GO
/****** Object:  Table [dbo].[tblTipoPermiso]    Script Date: 16/12/2021 04:43:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblTipoPermiso](
	[clave] [varchar](10) NULL,
	[descripcion] [varchar](200) NULL,
	[claveTransporte] [varchar](10) NULL,
	[fechaInicioVigencia] [date] NULL,
	[fechaFinVigencia] [date] NULL
) ON [PRIMARY]
GO
INSERT [dbo].[tblTipoPermiso] ([clave], [descripcion], [claveTransporte], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'TPAF01', N'Autotransporte Federal de carga general.', N'01', CAST(N'2021-06-01' AS Date), NULL)
INSERT [dbo].[tblTipoPermiso] ([clave], [descripcion], [claveTransporte], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'TPAF02', N'Transporte privado de carga.', N'01', CAST(N'2021-06-01' AS Date), NULL)
INSERT [dbo].[tblTipoPermiso] ([clave], [descripcion], [claveTransporte], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'TPAF03', N'Autotransporte Federal de Carga Especializada de materiales y residuos peligrosos.', N'01', CAST(N'2021-06-01' AS Date), NULL)
INSERT [dbo].[tblTipoPermiso] ([clave], [descripcion], [claveTransporte], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'TPAF04', N'Transporte de automóviles sin rodar en vehículo tipo góndola.', N'01', CAST(N'2021-06-01' AS Date), NULL)
INSERT [dbo].[tblTipoPermiso] ([clave], [descripcion], [claveTransporte], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'TPAF05', N'Transporte de carga de gran peso y/o volumen de hasta 90 toneladas.', N'01', CAST(N'2021-06-01' AS Date), NULL)
INSERT [dbo].[tblTipoPermiso] ([clave], [descripcion], [claveTransporte], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'TPAF06', N'Transporte de carga especializada de gran peso y/o volumen de más 90 toneladas.', N'01', CAST(N'2021-06-01' AS Date), NULL)
INSERT [dbo].[tblTipoPermiso] ([clave], [descripcion], [claveTransporte], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'TPAF07', N'Transporte Privado de materiales y residuos peligrosos.', N'01', CAST(N'2021-06-01' AS Date), NULL)
INSERT [dbo].[tblTipoPermiso] ([clave], [descripcion], [claveTransporte], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'TPAF08', N'Autotransporte internacional de carga de largo recorrido.', N'01', CAST(N'2021-06-01' AS Date), NULL)
INSERT [dbo].[tblTipoPermiso] ([clave], [descripcion], [claveTransporte], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'TPAF09', N'Autotransporte internacional de carga especializada de materiales y residuos peligrosos de largo recorrido.', N'01', CAST(N'2021-06-01' AS Date), NULL)
INSERT [dbo].[tblTipoPermiso] ([clave], [descripcion], [claveTransporte], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'TPAF10', N'Autotransporte Federal de Carga General cuyo ámbito de aplicación comprende la franja fronteriza con Estados Unidos.', N'01', CAST(N'2021-06-01' AS Date), NULL)
INSERT [dbo].[tblTipoPermiso] ([clave], [descripcion], [claveTransporte], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'TPAF11', N'Autotransporte Federal de Carga Especializada cuyo ámbito de aplicación comprende la franja fronteriza con Estados Unidos.', N'01', CAST(N'2021-06-01' AS Date), NULL)
INSERT [dbo].[tblTipoPermiso] ([clave], [descripcion], [claveTransporte], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'TPAF12', N'Servicio auxiliar de arrastre en las vías generales de comunicación.', N'01', CAST(N'2021-06-01' AS Date), NULL)
INSERT [dbo].[tblTipoPermiso] ([clave], [descripcion], [claveTransporte], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'TPAF13', N'Servicio auxiliar de servicios de arrastre, arrastre y salvamento, y depósito de vehículos en las vías generales de comunicación.', N'01', CAST(N'2021-06-01' AS Date), NULL)
INSERT [dbo].[tblTipoPermiso] ([clave], [descripcion], [claveTransporte], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'TPAF14', N'Servicio de paquetería y mensajería en las vías generales de comunicación.', N'01', CAST(N'2021-06-01' AS Date), NULL)
INSERT [dbo].[tblTipoPermiso] ([clave], [descripcion], [claveTransporte], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'TPAF15', N'Transporte especial para el tránsito de grúas industriales con peso máximo de 90 toneladas.', N'01', CAST(N'2021-06-01' AS Date), NULL)
INSERT [dbo].[tblTipoPermiso] ([clave], [descripcion], [claveTransporte], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'TPAF16', N'Servicio federal para empresas arrendadoras servicio público federal.', N'01', CAST(N'2021-06-01' AS Date), NULL)
INSERT [dbo].[tblTipoPermiso] ([clave], [descripcion], [claveTransporte], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'TPAF17', N'Empresas trasladistas de vehículos nuevos.', N'01', CAST(N'2021-06-01' AS Date), NULL)
INSERT [dbo].[tblTipoPermiso] ([clave], [descripcion], [claveTransporte], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'TPAF18', N'Empresas fabricantes o distribuidoras de vehículos nuevos.', N'01', CAST(N'2021-06-01' AS Date), NULL)
INSERT [dbo].[tblTipoPermiso] ([clave], [descripcion], [claveTransporte], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'TPAF19', N'Autorización expresa para circular en los caminos y puentes de jurisdicción federal con configuraciones de tractocamión doblemente articulado.', N'01', CAST(N'2021-06-01' AS Date), NULL)
INSERT [dbo].[tblTipoPermiso] ([clave], [descripcion], [claveTransporte], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'TPAF20', N'Autotransporte Federal de Carga Especializada de fondos y valores.', N'01', CAST(N'2021-07-02' AS Date), NULL)
INSERT [dbo].[tblTipoPermiso] ([clave], [descripcion], [claveTransporte], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'TPTM01', N'Permiso temporal para navegación de cabotaje', N'02', CAST(N'2021-06-01' AS Date), NULL)
INSERT [dbo].[tblTipoPermiso] ([clave], [descripcion], [claveTransporte], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'TPTA01', N'Concesión y/o autorización para el servicio regular nacional y/o internacional para empresas mexicanas', N'03', CAST(N'2021-06-01' AS Date), NULL)
INSERT [dbo].[tblTipoPermiso] ([clave], [descripcion], [claveTransporte], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'TPTA02', N'Permiso para el servicio aéreo regular de empresas extranjeras', N'03', CAST(N'2021-06-01' AS Date), NULL)
INSERT [dbo].[tblTipoPermiso] ([clave], [descripcion], [claveTransporte], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'TPTA03', N'Permiso para el servicio nacional e internacional no regular de fletamento', N'03', CAST(N'2021-06-01' AS Date), NULL)
INSERT [dbo].[tblTipoPermiso] ([clave], [descripcion], [claveTransporte], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'TPTA04', N'Permiso para el servicio nacional e internacional no regular de taxi aéreo', N'03', CAST(N'2021-06-01' AS Date), NULL)
INSERT [dbo].[tblTipoPermiso] ([clave], [descripcion], [claveTransporte], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'TPXX00', N'Permiso no contemplado en el catálogo.', N'01,02,03', CAST(N'2021-07-02' AS Date), NULL)
GO
