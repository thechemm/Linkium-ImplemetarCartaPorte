
/****** Object:  UserDefinedFunction [dbo].[fnColoniaNombre]    Script Date: 20/12/2021 04:29:37 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
alter FUNCTION [dbo].[fnColoniaNombre]
(
	-- Add the parameters for the function here
	@codigopostal varchar(50),
	@codigo varchar(50)
)
RETURNS varchar(50)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @NombreAsentamiento varchar(50)

	-- Add the T-SQL statements to compute the return value here
	SELECT @NombreAsentamiento = (select isnull(NombreAsentamiento,'') from tblColonia where c_CodigoPostal = @codigopostal and c_Colonia = @codigo)

	-- Return the result of the function
	RETURN @NombreAsentamiento

END
GO
