
/****** Object:  Table [dbo].[tblFiguraTransporte]    Script Date: 16/12/2021 05:15:02 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblFiguraTransporte](
	[clave] [varchar](50) NULL,
	[descripcion] [varchar](50) NULL
) ON [PRIMARY]
GO
INSERT [dbo].[tblFiguraTransporte] ([clave], [descripcion]) VALUES (N'01', N'Operador')
GO
INSERT [dbo].[tblFiguraTransporte] ([clave], [descripcion]) VALUES (N'02', N'Propietario')
GO
INSERT [dbo].[tblFiguraTransporte] ([clave], [descripcion]) VALUES (N'03', N'Arrendador')
GO
INSERT [dbo].[tblFiguraTransporte] ([clave], [descripcion]) VALUES (N'04', N'Notificado')
GO
