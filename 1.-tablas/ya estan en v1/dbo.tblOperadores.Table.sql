
/****** Object:  Table [dbo].[tblOperadores]    Script Date: 16/12/2021 04:49:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblOperadores](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[tipoFigura] [varchar](50) NULL,
	[RFCOperador] [varchar](50) NULL,
	[NumLicencia] [varchar](50) NULL,
	[NombreOperador] [varchar](100) NULL,
	[BorradorBit] [bit] NULL,
	[Calle_op] [varchar](100) NULL,
	[NumeroExterior_op] [varchar](20) NULL,
	[NumeroInterior_op] [varchar](20) NULL,
	[Colonia_op] [varchar](120) NULL,
	[Localidad_op] [varchar](120) NULL,
	[Municipio_op] [varchar](10) NULL,
	[Referencia_op] [varchar](200) NULL,
	[Estado_op] [varchar](30) NULL,
	[Pais_op] [varchar](10) NULL,
	[CodigoPostal_op] [varchar](12) NULL,
 CONSTRAINT [PK_tblOperadores] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
