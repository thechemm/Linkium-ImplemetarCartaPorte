
/****** Object:  UserDefinedFunction [dbo].[fnMunicipioNombre]    Script Date: 20/12/2021 04:29:37 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
alter FUNCTION [dbo].[fnMunicipioNombre]
(
	-- Add the parameters for the function here
	@codigopostal varchar(50)
)
RETURNS varchar(50)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @NombreMunicipio varchar(50)

	-- Add the T-SQL statements to compute the return value here
	SELECT @NombreMunicipio = (select top 1 ISNULL(b.descripcion,'') from tblCodigoPostal a right join tblMunicipio b on a.clave = b.estado and a.c_Municipio = b.municipio where a.codigo = @codigopostal)

	-- Return the result of the function
	RETURN @NombreMunicipio 

END
GO
