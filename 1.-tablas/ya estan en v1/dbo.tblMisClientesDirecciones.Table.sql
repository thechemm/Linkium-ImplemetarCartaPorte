
/****** Object:  Table [dbo].[tblMisClientesDirecciones]    Script Date: 16/12/2021 04:49:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblMisClientesDirecciones](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[clienteid] [int] NULL,
	[nombre] [varchar](150) NULL,
	[Calle_des] [varchar](100) NULL,
	[NumeroExterior_des] [varchar](20) NULL,
	[NumeroInterior_des] [varchar](20) NULL,
	[Colonia_des] [varchar](120) NULL,
	[Localidad_des] [varchar](120) NULL,
	[Municipio_des] [varchar](10) NULL,
	[Referencia_des] [varchar](200) NULL,
	[Estado_des] [varchar](30) NULL,
	[Pais_des] [varchar](10) NULL,
	[CodigoPostal_des] [varchar](12) NULL,
	[borradorbit] [bit] NULL,
	[rfc_des] [varchar](15) NULL,
	[nombre_des] [varchar](300) NULL,
	[idDE] [varchar](10) NULL,
 CONSTRAINT [PK_tblMisClientesDirecciones] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
