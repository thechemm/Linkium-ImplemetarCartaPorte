
/****** Object:  StoredProcedure [dbo].[pCartaPorteOperadores]    Script Date: 17/01/2022 01:29:30 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
alter PROCEDURE [dbo].[pCartaPorteOperadores]
	-- Add the parameters for the stored procedure here
	@cmd int = 0, 
	@id int = 0,
	@cartaPorteId int = null,
    @operadorid int = null,
    @fechaCrea datetime = null,
    @fechaModifica datetime = null,
    @borradorBit bit = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	/*:::::::::::::::: START CRUD [tblCartaPorteOperadores]:::::::::::::::::::::::::*/
	/*:::::::::::::::: CREATE ONE [tblCartaPorteOperadores]:::::::::::::::::::::::::*/
	IF @cmd = 1
	BEGIN 
	INSERT INTO [dbo].[tblCartaPorteOperadores]
           ([cartaPorteId]
		   ,operadorid
           ,[fechaCrea])
     VALUES
           (@cartaPorteId
           ,@operadorid
           ,GETDATE())
	select @@identity
	END
	/*:::::::::::::::: READ ALL [tblCartaPorteOperadores] Trae todos apartir de cartaporteid ::::::::*/
	IF @cmd = 2
	BEGIN 
	SELECT  A.id,
       isnull(cartaPorteId,'') as cartaPorteId,
	   isnull(tipoFigura,'') as tipofigura,
	   isnull(RFCOperador,'') as RFCOperador ,
	   isnull(NumLicencia, '') as NumLicencia,
	   isnull(NombreOperador, '') as NombreOperador,
	   isnull(Calle_op, '') as Calle_op,
	   isnull(NumeroExterior_op, '') as NumeroExterior_op,
	   isnull(NumeroInterior_op, '') as NumeroInterior_op,
	   isnull(Colonia_op, '') as Colonia_op,
	   isnull(Localidad_op, '') as Localidad_op,
	   isnull(Municipio_op, '') as Municipio_op,
	   isnull(Referencia_op, '') as Referencia_op,
	   isnull(Estado_op, '') as Estado_op,
	   --isnull(Pais_op, '') as Pais_op,
	   isnull('MEX', '') as Pais_op,
	   isnull(CodigoPostal_op, '') as CodigoPostal_op,	   
	   isnull(dbo.fnColoniaNombre(B.CodigoPostal_op,B.Colonia_op),'') as colonia_nombre,
	   isnull(dbo.fnEstadoNombre(B.CodigoPostal_op),'') as estado_nombre,
	   isnull(dbo.fnMunicipioNombre(B.CodigoPostal_op),'') as municipio_nombre,
	   (select isnull(descripcion,'') from tblFiguraTransporte where clave = B.tipoFigura) as tipoFigura_descripcion	   
	 FROM [dbo].[tblCartaPorteOperadores] A
	 right join [dbo].[tblOperadores] B on A.operadorid = B.id
	 where (A.borradorbit is null or B.borradorbit = 0) AND (@cartaPorteId = null or A.cartaPorteId = @cartaPorteId)
	END
	/*:::::::::::::::: READ ONE [tblCartaPorteOperadores]:::::::::::::::::::::::::*/
	IF @cmd = 3
	BEGIN 
	SELECT A.id,
       isnull(cartaPorteId,'') as cartaPorteId,
	   isnull(tipoFigura,'') as tipofigura,
	   isnull(RFCOperador,'') as RFCOperador ,
	   isnull(NumLicencia, '') as NumLicencia,
	   isnull(NombreOperador, '') as NombreOperador,
	   isnull(Calle_op, '') as Calle_op,
	   isnull(NumeroExterior_op, '') as NumeroExterior_op,
	   isnull(NumeroInterior_op, '') as NumeroInterior_op,
	   isnull(Colonia_op, '') as Colonia_op,
	   isnull(Localidad_op, '') as Localidad_op,
	   isnull(Municipio_op, '') as Municipio_op,
	   isnull(Referencia_op, '') as Referencia_op,
	   isnull(Estado_op, '') as Estado_op,
	   isnull(Pais_op, '') as Pais_op,
	   isnull(CodigoPostal_op, '') as CodigoPostal_op,	   
	   isnull(dbo.fnColoniaNombre(B.CodigoPostal_op,B.Colonia_op),'') as colonia_nombre,
	   isnull(dbo.fnEstadoNombre(B.CodigoPostal_op),'') as estado_nombre,
	   isnull(dbo.fnMunicipioNombre(B.CodigoPostal_op),'') as municipio_nombre,
	   (select isnull(descripcion,'') from tblFiguraTransporte where clave = B.tipoFigura) as tipoFigura_descripcion	   
	 FROM [dbo].[tblCartaPorteOperadores] A
	 right join [dbo].[tblOperadores] B on A.operadorid = B.id
	 where A.id = @id
	END
	/*:::::::::::::::: UPDATE ONE [tblCartaPorteOperadores]:::::::::::::::::::::::::*/
	IF @cmd = 4
	BEGIN 
	UPDATE [dbo].[tblCartaPorteOperadores]
    SET 
      [cartaPorteId] = @cartaPorteId
	  ,[fechaModifica] = getdate()
      ,[borradorbit] = @borradorbit
	WHERE id = @id
	END
	/*:::::::::::::::: DELETE ONE [tblCartaPorteOperadores]:::::::::::::::::::::::::*/
	IF @cmd = 5
	BEGIN 
	UPDATE [dbo].[tblCartaPorteOperadores]
    SET 
      [borradorbit] = 1
	 ,[fechaModifica] = getdate()
	WHERE id = @id
	END
	/*:::::::::::::::: END CRUD [tblCartaPorteOperadores]:::::::::::::::::::::::::*/
END
GO
