
/****** Object:  Table [dbo].[tblOrigenDirecciones]    Script Date: 16/12/2021 04:49:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblOrigenDirecciones](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](150) NULL,
	[Calle_or] [varchar](100) NULL,
	[NumeroExterior_or] [varchar](20) NULL,
	[NumeroInterior_or] [varchar](20) NULL,
	[Colonia_or] [varchar](120) NULL,
	[Localidad_or] [varchar](120) NULL,
	[Municipio_or] [varchar](10) NULL,
	[Referencia_or] [varchar](200) NULL,
	[Estado_or] [varchar](30) NULL,
	[Pais_or] [varchar](10) NULL,
	[CodigoPostal_or] [varchar](12) NULL,
	[borradorbit] [bit] NULL,
	[rfc_or] [varchar](15) NULL,
	[nombre_or] [varchar](300) NULL,
	[idOR] [varchar](10) NULL,
 CONSTRAINT [PK_tblOrigenDirecciones] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
