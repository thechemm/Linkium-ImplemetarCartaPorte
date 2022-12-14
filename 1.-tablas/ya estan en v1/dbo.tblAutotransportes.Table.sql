
/****** Object:  Table [dbo].[tblAutotransportes]    Script Date: 16/12/2021 04:49:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblAutotransportes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[PermSCTId] [varchar](50) NULL,
	[NumPermisoSCT] [varchar](50) NULL,
	[NombreAseg] [varchar](50) NULL,
	[NumPolizaSeguro] [varchar](50) NULL,
	[ConfigVehicularId] [varchar](50) NULL,
	[PlacaVM] [varchar](50) NULL,
	[AnioModeloVM] [varchar](50) NULL,
	[BorradorBit] [bit] NULL,
 CONSTRAINT [PK_tblAutotransportes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
