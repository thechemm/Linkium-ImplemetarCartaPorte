
/****** Object:  Table [dbo].[tblCartaPorteOperadores]    Script Date: 16/12/2021 04:49:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCartaPorteOperadores](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[cartaPorteId] [int] NULL,
	[operadorid] [int] NULL,
	[fechaCrea] [datetime] NULL,
	[fechaModifica] [datetime] NULL,
	[borradorBit] [bit] NULL,
 CONSTRAINT [PK_tblCartaPorteOperadores] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
