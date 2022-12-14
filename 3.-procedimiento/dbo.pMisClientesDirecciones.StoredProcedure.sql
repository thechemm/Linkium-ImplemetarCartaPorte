
/****** Object:  StoredProcedure [dbo].[pMisClientesDirecciones]    Script Date: 17/01/2022 01:28:53 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
alter PROCEDURE [dbo].[pMisClientesDirecciones] 
	-- Add the parameters for the stored procedure here
	@cmd int = 0, 
	@id int = 0,
	@clienteid int = 0,
	@nombre varchar(150) = null,
	@Calle_des varchar(100) = null,
	@NumeroExterior_des varchar(20) = null,
	@NumeroInterior_des varchar(20) = null,
	@Colonia_des varchar(120) = null,
	@Localidad_des varchar(120) = null,
	@Municipio_des varchar(10) = null,
	@Referencia_des varchar(200) = null,
	@Estado_des varchar(30) = null,
	@Pais_des varchar(10) = null,
	@CodigoPostal_des varchar(12) = null,
	@rfc_des varchar(15) = null,
	@nombre_des varchar(300)=null,
	@idDE varchar(10)=null,
	@borradorbit bit = null

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	/*:::::::::::::: START MODEL CRUD [tblMisClientesDirecciones]*/

	/*:::::::::::::: CREATE ONE [tblMisClientesDirecciones]*/
	if @cmd = 1
	begin
INSERT INTO [dbo].[tblMisClientesDirecciones]
           ([nombre]
		   ,[rfc_des]
		   ,[nombre_des]
		   ,[idDE]
		   ,[clienteid]
           ,[Calle_des]
           ,[NumeroExterior_des]
           ,[NumeroInterior_des]
           ,[Colonia_des]
           ,[Localidad_des]
           ,[Municipio_des]
           ,[Referencia_des]
           ,[Estado_des]
           ,[Pais_des]
           ,[CodigoPostal_des])
     VALUES
           (@nombre
		   ,@rfc_des 
		   ,@nombre_des
		   ,@idDE 
		   ,@clienteid
           ,@Calle_des
           ,@NumeroExterior_des
           ,@NumeroInterior_des
           ,@Colonia_des
           ,@Localidad_des
           ,@Municipio_des
           ,@Referencia_des
           ,@Estado_des
           ,@Pais_des
           ,@CodigoPostal_des)
	end
	/*:::::::::::::: READ ONE [tblMisClientesDirecciones]*/
	if @cmd = 2
	begin
	SELECT  [id],[clienteid]
      ,isnull(nombre,'') as nombre
	  ,isnull(rfc_des,'') as rfc_des
	  ,isnull(nombre_des,'') as nombre_des
	  ,isnull(idDE,'') as idDE
      ,isnull(Calle_des,'') as Calle_des
      ,isnull(NumeroExterior_des,'') as NumeroExterior_des
      ,isnull(NumeroInterior_des,'') as NumeroInterior_des
      ,isnull(Colonia_des,'') as Colonia_des
      ,isnull(Localidad_des,'') as Localidad_des
      ,isnull(Municipio_des,'') as Municipio_des
      ,isnull(Referencia_des,'') as Referencia_des
      ,isnull(Estado_des,'') as Estado_des
      --,isnull(Pais_des,'') as Pais_des
	  ,isnull('MEX','') as Pais_des
      ,isnull(CodigoPostal_des,'') as CodigoPostal_des
	  ,isnull(dbo.fnColoniaNombre(CodigoPostal_des,Colonia_des),'') as colonia_nombre
	  ,isnull(dbo.fnEstadoNombre(CodigoPostal_des),'') as estado_nombre
	  ,isnull(dbo.fnMunicipioNombre(CodigoPostal_des),'') as municipio_nombre
      ,isnull(borradorbit,'') as borradorbit
  FROM [dbo].[tblMisClientesDirecciones]
  WHERE
  borradorbit is null
  and id = @id 

	end
	/*:::::::::::::: READ ALL  [tblMisClientesDirecciones]*/
	if @cmd = 3
	begin

	SELECT  [id]
      ,isnull(nombre,'') as nombre
	  ,isnull(rfc_des,'') as rfc_des
	  ,isnull(nombre_des,'') as nombre_des
	  ,isnull(idDE,'') as idDE
      ,isnull(Calle_des,'') as Calle_des
      ,isnull(NumeroExterior_des,'') as NumeroExterior_des
      ,isnull(NumeroInterior_des,'') as NumeroInterior_des
      ,isnull(Colonia_des,'') as Colonia_des
      ,isnull(Localidad_des,'') as Localidad_des
      ,isnull(Municipio_des,'') as Municipio_des
      ,isnull(Referencia_des,'') as Referencia_des
      ,isnull(Estado_des,'') as Estado_des
      ,isnull(Pais_des,'') as Pais_des
      ,isnull(CodigoPostal_des,'') as CodigoPostal_des
      ,isnull(borradorbit,'') as borradorbit
  FROM [dbo].[tblMisClientesDirecciones]
  WHERE
  borradorbit is null
  and clienteid = @clienteid 

	end
	/*:::::::::::::: UPDATE ONE  [tblMisClientesDirecciones]*/
	if @cmd = 4
	begin
	UPDATE [dbo].[tblMisClientesDirecciones]
	 SET nombre = @nombre
	  ,rfc_des = @rfc_des 
	  ,nombre_des = @nombre_des 
	  ,idDE = @idDE
      ,Calle_des = @Calle_des
      ,NumeroExterior_des = @NumeroExterior_des
      ,NumeroInterior_des = @NumeroInterior_des
      ,Colonia_des = @Colonia_des
      ,Localidad_des = @Localidad_des
      ,Municipio_des = @Municipio_des
      ,Referencia_des = @Referencia_des
      ,Estado_des = @Estado_des
      ,Pais_des = @Pais_des
      ,CodigoPostal_des = @CodigoPostal_des
	where id = @id
	end
	/*:::::::::::::: DELETE ONE <CRUD [tblMisClientesDirecciones]*/
	if @cmd = 5
	begin
	UPDATE [dbo].[tblMisClientesDirecciones]
	 SET borradorbit = 1
	where id = @id
	end
	/*:::::::::::::: END CRUD [tblMisClientesDirecciones]*/
END
GO
