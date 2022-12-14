
/****** Object:  UserDefinedFunction [dbo].[fnEstadoNombre]    Script Date: 20/12/2021 04:29:37 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
create FUNCTION [dbo].[fnEstadoNombre]
(
	-- Add the parameters for the function here
	@codigopostal varchar(50)
)
RETURNS varchar(50)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @NombreEstado varchar(50)

	-- Add the T-SQL statements to compute the return value here
	SELECT @NombreEstado = (select top 1  isnull(a.nombre,'') from tblEstado a inner join tblCodigoPostal b on a.clave = b.clave where b.codigo = @codigopostal)

	-- Return the result of the function
	RETURN @NombreEstado 

END
GO
