
/****** Object:  StoredProcedure [dbo].[pCartaPorteDestinos]    Script Date: 17/01/2022 01:28:53 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
alter PROCEDURE [dbo].[pCartaPorteDestinos]
	-- Add the parameters for the stored procedure here
	@cmd int = 0, 
	@id int = 0,
	@cartaPorteId int = null,
    @clienteDireccionId int = null,
	@fechaHoraLlegada datetime = null,
	@distanciaRecorrida float = null,
    @fechaCrea datetime = null,
    @fechaModifica datetime = null,
    @borradorBit bit = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	/*:::::::::::::::: START CRUD [tblCartaPorteDestinos]:::::::::::::::::::::::::*/
	/*:::::::::::::::: CREATE ONE [tblCartaPorteDestinos]:::::::::::::::::::::::::*/
	IF @cmd = 1
	BEGIN 
	INSERT INTO [dbo].[tblCartaPorteDestinos]
           ([cartaPorteId]
           ,[clienteDireccionId]
		   ,[fechaHoraLlegada]
		   ,[distanciaRecorrida]
           ,[fechaCrea])
     VALUES
           (@cartaPorteId
           ,@clienteDireccionId		   
		   ,@fechaHoraLlegada
		   ,@distanciaRecorrida
           ,GETDATE())
	select @@identity
	END
	/*:::::::::::::::: READ ALL [tblCartaPorteDestinos] Trae todos apartir de cartaporteid ::::::::*/
	IF @cmd = 2
	BEGIN 
	SELECT A.id
	  ,isnull(B.nombre,'') as nombre
	  ,isnull(B.rfc_des,'') as rfc_des
	  ,isnull(B.nombre_des,'') as nombre_des
	  ,isnull(B.idDE,'') as idDE
      ,isnull(B.Calle_des,'') as Calle_des
      ,isnull(B.NumeroExterior_des,'') as NumeroExterior_des
      ,isnull(B.NumeroInterior_des,'') as NumeroInterior_des
      ,isnull(B.Colonia_des,'') as Colonia_des
      ,isnull(B.Localidad_des,'') as Localidad_des
      ,isnull(B.Municipio_des,'') as Municipio_des
      ,isnull(B.Referencia_des,'') as Referencia_des
      ,isnull(B.Estado_des,'') as Estado_des
      ,isnull(B.Pais_des,'') as Pais_des
      ,isnull(B.CodigoPostal_des,'') as CodigoPostal_des
	  ,isnull(dbo.fnColoniaNombre(B.CodigoPostal_des, B.Colonia_des),'') as colonia_nombre
	  ,isnull(dbo.fnEstadoNombre(B.CodigoPostal_des),'') as estado_nombre
	  ,isnull(dbo.fnMunicipioNombre(B.CodigoPostal_des),'') as municipio_nombre
	  ,isnull(A.fechaHoraLlegada,0) as fechaHoraLlegada
	  ,isnull(A.distanciaRecorrida,0) as distanciaRecorrida 
	 FROM [dbo].[tblCartaPorteDestinos] A
	 right join [dbo].[tblMisClientesDirecciones] B on A.clienteDireccionId = B.id
	 where A.cartaPorteId = @cartaPorteId and (A.borradorbit is null or A.borradorbit = 0)
	END
	/*:::::::::::::::: READ ONE [tblCartaPorteDestinos]:::::::::::::::::::::::::*/
	IF @cmd = 3
	BEGIN 
	SELECT [id]
      ,isnull(cartaPorteId,'') as cartaPorteId
	 FROM [dbo].[tblCartaPorteDestinos]
	 where id = @id
	END
	/*:::::::::::::::: UPDATE ONE [tblCartaPorteDestinos]:::::::::::::::::::::::::*/
	IF @cmd = 4
	BEGIN 
	UPDATE [dbo].[tblCartaPorteDestinos]
    SET 
      [cartaPorteId] = @cartaPorteId
	  ,[fechaModifica] = getdate()
      ,[borradorbit] = @borradorbit
	WHERE id = @id
	END
	/*:::::::::::::::: DELETE ONE [tblCartaPorteDestinos]:::::::::::::::::::::::::*/
	IF @cmd = 5
	BEGIN 
	UPDATE [dbo].[tblCartaPorteDestinos]
    SET 
      [borradorbit] = 1
	 ,[fechaModifica] = getdate()
	WHERE id = @id
	END
	/*:::::::::::::::: END CRUD [tblCartaPorteDestinos]:::::::::::::::::::::::::*/
	/* Regresa la distancia total recorrida de un carta porte*/
	if @cmd= 6
	begin
		select isnull(sum(distanciaRecorrida),0) from tblCartaPorteDestinos where cartaPorteId = @cartaPorteId 
	end
END
GO
