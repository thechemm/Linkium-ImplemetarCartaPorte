
/****** Object:  Table [dbo].[tblCartaPorte]    Script Date: 16/12/2021 04:49:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCartaPorte](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[cfdid] [int] NULL,
	[origenid] [int] NULL,
	[autotransporteid] [int] NULL,
	[TranspInternac] [varchar](50) NULL,
	[FechaHoraSalida] [datetime] NULL,
	[TotalDistRec] [float] NULL,
	[UnidadPeso] [varchar](50) NULL,
	[PesoNetoTotal] [float] NULL,
	[FechaCrea] [datetime] NULL,
	[FechaModifica] [datetime] NULL,
	[fecha_factura] [datetime] NULL,
	[serie] [varchar](50) NULL,
	[serieid] [int] NULL,
	[folio] [int] NULL,
	[uuid] [varchar](500) NULL,
	[timbrado] [bit] NULL,
	[sello] [varchar](500) NULL,
	[cadenaoriginal] [varchar](500) NULL,
	[estatus] [int] NULL,
	[ckbit] [bit] NULL,
	[BorradorBit] [bit] NULL,
 CONSTRAINT [PK_tblCartaPorte] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
