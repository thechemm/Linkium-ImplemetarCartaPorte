
/****** Object:  StoredProcedure [dbo].[pOrigenDirecciones]    Script Date: 17/01/2022 01:29:30 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
alter PROCEDURE [dbo].[pOrigenDirecciones] 
	-- Add the parameters for the stored procedure here
	@cmd int = 0, 
	@id int = 0,
	@nombre varchar(150) = null,
	@Calle_or varchar(100) = null,
	@NumeroExterior_or varchar(20) = null,
	@NumeroInterior_or varchar(20) = null,
	@Colonia_or varchar(120) = null,
	@Localidad_or varchar(120) = null,
	@Municipio_or varchar(10) = null,
	@Referencia_or varchar(200) = null,
	@Estado_or varchar(30) = null,
	@Pais_or varchar(10) = null,
	@CodigoPostal_or varchar(12) = null,
	@rfc_or varchar(15) = null,
	@nombre_or varchar(300)=null,
	@idOR varchar(10)=null,
	@borradorbit bit = null

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	/*:::::::::::::: START MODEL CRUD [tblOrigenDirecciones]*/

	/*:::::::::::::: CREATE ONE [tblOrigenDirecciones]*/
	if @cmd = 1
	begin
INSERT INTO [dbo].[tblOrigenDirecciones]
           ([nombre]
		   ,[rfc_or]
		   ,[nombre_or]
		   ,[idOR]
           ,[Calle_or]
           ,[NumeroExterior_or]
           ,[NumeroInterior_or]
           ,[Colonia_or]
           ,[Localidad_or]
           ,[Municipio_or]
           ,[Referencia_or]
           ,[Estado_or]
           ,[Pais_or]
           ,[CodigoPostal_or])
     VALUES
           (@nombre
		   ,@rfc_or 
		   ,@nombre_or
		   ,@idOR 
           ,@Calle_or
           ,@NumeroExterior_or
           ,@NumeroInterior_or
           ,@Colonia_or
           ,@Localidad_or
           ,@Municipio_or
           ,@Referencia_or
           ,@Estado_or
           ,@Pais_or
           ,@CodigoPostal_or)
	end
	/*:::::::::::::: READ ONE [tblOrigenDirecciones]*/
	if @cmd = 2
	begin
	SELECT  [id]
      ,isnull(nombre,'') as nombre
	  ,isnull(rfc_or,'') as rfc_or
	  ,isnull(nombre_or,'') as nombre_or
	  ,isnull(idOR,'') as idOR
      ,isnull(Calle_or,'') as Calle_or
      ,isnull(NumeroExterior_or,'') as NumeroExterior_or
      ,isnull(NumeroInterior_or,'') as NumeroInterior_or
      ,isnull(Colonia_or,'') as Colonia_or
      ,isnull(Localidad_or,'') as Localidad_or
      ,isnull(Municipio_or,'') as Municipio_or
      ,isnull(Referencia_or,'') as Referencia_or
      ,isnull(Estado_or,'') as Estado_or
      ,isnull(Pais_or,'') as Pais_or
      ,isnull(CodigoPostal_or,'') as CodigoPostal_or
      ,isnull(borradorbit,'') as borradorbit
  FROM [dbo].[tblOrigenDirecciones]
  WHERE
  borradorbit is null
  and id = @id 

	end
	/*:::::::::::::: READ ALL  [tblOrigenDirecciones]*/
	if @cmd = 3
	begin

	SELECT  [id]
      ,isnull(nombre,'') as nombre
	  ,isnull(rfc_or,'') as rfc_or
	  ,isnull(nombre_or,'') as nombre_or
	  ,isnull(idOR,'') as idOR
      ,isnull(Calle_or,'') as Calle_or
      ,isnull(NumeroExterior_or,'') as NumeroExterior_or
      ,isnull(NumeroInterior_or,'') as NumeroInterior_or
      ,isnull(Colonia_or,'') as Colonia_or
      ,isnull(Localidad_or,'') as Localidad_or
      ,isnull(Municipio_or,'') as Municipio_or
      ,isnull(Referencia_or,'') as Referencia_or
      ,isnull(Estado_or,'') as Estado_or
      ,isnull(Pais_or,'') as Pais_or
      ,isnull(CodigoPostal_or,'') as CodigoPostal_or
      ,isnull(borradorbit,'') as borradorbit
  FROM [dbo].[tblOrigenDirecciones]
  WHERE
  borradorbit is null

	end
	/*:::::::::::::: UPDATE ONE  [tblOrigenDirecciones]*/
	if @cmd = 4
	begin
	UPDATE [dbo].[tblOrigenDirecciones]
	 SET nombre = @nombre
	  ,rfc_or = @rfc_or 
	  ,nombre_or = @nombre_or 
	  ,idOR = @idOR 
      ,Calle_or = @Calle_or
      ,NumeroExterior_or = @NumeroExterior_or
      ,NumeroInterior_or = @NumeroInterior_or
      ,Colonia_or = @Colonia_or
      ,Localidad_or = @Localidad_or
      ,Municipio_or = @Municipio_or
      ,Referencia_or = @Referencia_or
      ,Estado_or = @Estado_or
      ,Pais_or = @Pais_or
      ,CodigoPostal_or = @CodigoPostal_or
	where id = @id
	end
	/*:::::::::::::: DELETE ONE <CRUD [tblOrigenDirecciones]*/
	if @cmd = 5
	begin
	UPDATE [dbo].[tblOrigenDirecciones]
	 SET borradorbit = 1
	where id = @id
	end
	/*:::::::::::::: END CRUD [tblOrigenDirecciones]*/
END
GO
