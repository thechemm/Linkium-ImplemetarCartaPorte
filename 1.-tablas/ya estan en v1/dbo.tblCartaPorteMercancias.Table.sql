
/****** Object:  Table [dbo].[tblCartaPorteMercancias]    Script Date: 16/12/2021 04:49:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCartaPorteMercancias](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[cartaporteid] [bigint] NULL,
	[BienesTransp] [varchar](10) NULL,
	[BienesTransp_D] [varchar](600) NULL,
	[Descripcion] [varchar](1000) NULL,
	[Cantidad] [float] NULL,
	[ClaveUnidad] [varchar](10) NULL,
	[PesoEnKg] [float] NULL,
	[ValorMercancia] [float] NULL,
	[Moneda] [varchar](10) NULL,
	[MaterialPeligroso] [varchar](10) NULL,
	[CveMaterialPeligroso] [varchar](10) NULL,
	[DescripEmbalaje] [varchar](200) NULL,
	[Embalaje] [varchar](100) NULL,
	[FraccionArancelaria] [varchar](100) NULL,
	[UUIDComercioExt] [varchar](100) NULL,
	[fechaCreacion] [datetime] NULL,
	[BorradorBit] [bit] NULL,
 CONSTRAINT [PK_tblCartaPorteMercancias] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
