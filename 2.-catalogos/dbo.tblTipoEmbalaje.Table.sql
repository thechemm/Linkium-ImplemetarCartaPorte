
/****** Object:  Table [dbo].[tblTipoEmbalaje]    Script Date: 16/12/2021 04:43:36 p. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblTipoEmbalaje]') AND type in (N'U'))
DROP TABLE [dbo].[tblTipoEmbalaje]
GO
/****** Object:  Table [dbo].[tblTipoEmbalaje]    Script Date: 16/12/2021 04:43:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblTipoEmbalaje](
	[clave] [varchar](10) NULL,
	[descripcion] [varchar](200) NULL,
	[fechaInicioVigencia] [date] NULL,
	[fechaFinVigencia] [date] NULL
) ON [PRIMARY]
GO
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'1A1', N'Bidones (Tambores) de Acero 1 de tapa no desmontable', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'1A2', N'Bidones (Tambores) de Acero 1 de tapa desmontable', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'1B1', N'Bidones (Tambores) de Aluminio de tapa no desmontable', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'1B2', N'Bidones (Tambores) de Aluminio de tapa desmontable', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'1D', N'Bidones (Tambores) de Madera contrachapada', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'1G', N'Bidones (Tambores) de Cartón', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'1H1', N'Bidones (Tambores) de Plástico de tapa no desmontable', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'1H2', N'Bidones (Tambores) de Plástico de tapa desmontable', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'1N1', N'Bidones (Tambores) de Metal que no sea acero ni aluminio de tapa no desmontable', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'1N2', N'Bidones (Tambores) de Metal que no sea acero ni aluminio de tapa desmontable', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'3A1', N'Jerricanes (Porrones) de Acero de tapa no desmontable', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'3A2', N'Jerricanes (Porrones) de Acero de tapa desmontable', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'3B1', N'Jerricanes (Porrones) de Aluminio de tapa no desmontable', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'3B2', N'Jerricanes (Porrones) de Aluminio de tapa desmontable', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'3H1', N'Jerricanes (Porrones) de Plástico de tapa no desmontable', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'3H2', N'Jerricanes (Porrones) de Plástico de tapa desmontable', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'4A', N'Cajas de Acero', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'4B', N'Cajas de Aluminio', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'4C1', N'Cajas de Madera natural ordinaria', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'4C2', N'Cajas de Madera natural de paredes a prueba de polvos (estancas a los pulverulentos)', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'4D', N'Cajas de Madera contrachapada', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'4F', N'Cajas de Madera reconstituida', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'4G', N'Cajas de Cartón', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'4H1', N'Cajas de Plástico Expandido', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'4H2', N'Cajas de Plástico Rígido', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'5H1', N'Sacos (Bolsas) de Tejido de plástico sin forro ni revestimientos interiores', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'5H2', N'Sacos (Bolsas) de Tejido de plástico a prueba de polvos (estancos a los pulverulentos)', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'5H3', N'Sacos (Bolsas) de Tejido de plástico resistente al agua', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'5H4', N'Sacos (Bolsas) de Película de plástico', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'5L1', N'Sacos (Bolsas) de Tela sin forro ni revestimientos interiores', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'5L2', N'Sacos (Bolsas) de Tela a prueba de polvos (estancos a los pulverulentos)', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'5L3', N'Sacos (Bolsas) de Tela resistentes al agua', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'5M1', N'Sacos (Bolsas) de Papel de varias hojas', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'5M2', N'Sacos (Bolsas) de Papel de varias hojas, resistentes al agua', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'6HA1', N'Envases y embalajes compuestos de Recipiente de plástico, con bidón (tambor) de acero', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'6HA2', N'Envases y embalajes compuestos de Recipiente de plástico, con una jaula o caja de acero', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'6HB1', N'Envases y embalajes compuestos de Recipiente de plástico, con un bidón (tambor) exterior de aluminio', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'6HB2', N'Envases y embalajes compuestos de Recipiente de plástico, con una jaula o caja de aluminio', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'6HC', N'Envases y embalajes compuestos de Recipiente de plástico, con una caja de madera', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'6HD1', N'Envases y embalajes compuestos de Recipiente de plástico, con un bidón (tambor) de madera contrachapada', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'6HD2', N'Envases y embalajes compuestos de Recipiente de plástico, con una caja de madera contrachapada', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'6HG1', N'Envases y embalajes compuestos de Recipiente de plástico, con un bidón (tambor) de cartón', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'6HG2', N'Envases y embalajes compuestos de Recipiente de plástico, con una caja de cartón', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'6HH1', N'Envases y embalajes compuestos de Recipiente de plástico, con un bidón (tambor) de plástico', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'6HH2', N'Envases y embalajes compuestos de Recipiente de plástico, con caja de plástico rígido', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'6PA1', N'Envases y embalajes compuestos de Recipiente de vidrio, porcelana o de gres, con un bidón (tambor) de acero', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'6PA2', N'Envases y embalajes compuestos de Recipiente de vidrio, porcelana o de gres, con una jaula o una caja de acero', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'6PB1', N'Envases y embalajes compuestos de Recipiente de vidrio, porcelana o de gres, con un bidón (tambor) exterior de aluminio', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'6PB2', N'Envases y embalajes compuestos de Recipiente de vidrio, porcelana o de gres, con una jaula o una caja de aluminio', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'6PC', N'Envases y embalajes compuestos de Recipiente de vidrio, porcelana o de gres, con una caja de madera', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'6PD 1', N'Envases y embalajes compuestos de Recipiente de vidrio, porcelana o de gres, con bidón (tambor) de madera contrachapada', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'6PD2', N'Envases y embalajes compuestos de Recipiente de vidrio, porcelana o de gres, con canasta de mimbre', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'6PG1', N'Envases y embalajes compuestos de Recipiente de vidrio, porcelana o de gres, con un bidón (tambor) de cartón', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'6PG2', N'Envases y embalajes compuestos de Recipiente de vidrio, porcelana o de gres, con una caja de cartón', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'6PH1', N'Envases y embalajes compuestos de Recipiente de vidrio, porcelana o de gres, con un envase y embalaje de plástico expandido', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'6PH2', N'Envases y embalajes compuestos de Recipiente de vidrio, porcelana o de gres, con un envase y embalaje de plástico rígido', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'7H1', N'Bultos de Plástico', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'7L1', N'Bultos de Tela', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
INSERT [dbo].[tblTipoEmbalaje] ([clave], [descripcion], [fechaInicioVigencia], [fechaFinVigencia]) VALUES (N'Z01', N'No aplica ', CAST(N'2021-06-01' AS Date), CAST(N'1900-01-01' AS Date))
GO
