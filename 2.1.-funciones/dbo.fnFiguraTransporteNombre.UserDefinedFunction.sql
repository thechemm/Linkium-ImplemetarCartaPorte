
/****** Object:  UserDefinedFunction [dbo].[fnFiguraTransporteNombre]    Script Date: 20/12/2021 04:29:37 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
alter FUNCTION [dbo].[fnFiguraTransporteNombre] 
(
	-- Add the parameters for the function here
	@clave varchar(50)
)
RETURNS varchar(50)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result varchar(50)

	-- Add the T-SQL statements to compute the return value here
	SELECT @Result = (select top 1 isnull(descripcion,'') from tblFiguraTransporte where clave = @clave)

	-- Return the result of the function
	RETURN @Result

END
GO
