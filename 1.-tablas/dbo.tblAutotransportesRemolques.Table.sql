--USE [cfdi_transportesbravo]
--GO
/****** Object:  Table [dbo].[tblAutotransportesRemolques]    Script Date: 16/12/2021 04:49:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblAutotransportesRemolques](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[autotransporteid] [int] NULL,
	[placa] [varchar](50) NULL,
	[descripcion] [varchar](100) NULL,
	[SubTipoRem] [varchar](50) NULL,
	[BorradorBit] [bit] NULL,
 CONSTRAINT [PK_tblAutotransportesRemolques] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
