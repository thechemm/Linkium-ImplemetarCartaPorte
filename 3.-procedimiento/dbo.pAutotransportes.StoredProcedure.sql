
/****** Object:  StoredProcedure [dbo].[pAutotransportes]    Script Date: 17/01/2022 01:28:53 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
alter PROCEDURE [dbo].[pAutotransportes] 
	-- Add the parameters for the stored procedure here
	@id int = 0, 
	@cmd int = 0,
	@PermSCTId varchar(50) = null,
	@NumPermisoSCT varchar(50) = null,
	@NombreAseg varchar(50) = null,
	@NumPolizaSeguro varchar(50) = null,
	@ConfigVehicularId varchar(50) = null,
	@PlacaVM varchar(50) = null,
	@AnioModeloVM varchar(50) = null,
	@BorradorBit bit  = null,
	@PolizaMedAmbiente varchar(50) = null,
	@AseguraMedAmbiente varchar(300) = null

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	/* Inserta un registro de un autransporte*/
	if @cmd=1
	begin
	 insert into tblAutotransportes (PermSCTId, NumPermisoSCT, NombreAseg, NumPolizaSeguro, AseguraMedAmbiente, PolizaMedAmbiente, ConfigVehicularId, PlacaVM, AnioModeloVM)
							values( @PermSCTId, @NumPermisoSCT, @NombreAseg, @NumPolizaSeguro, @AseguraMedAmbiente, @PolizaMedAmbiente, @ConfigVehicularId, @PlacaVM, @AnioModeloVM)
	end
	/* Lee un registro de un autransporte*/
	if @cmd=2
	begin
	select 
	id,
	isnull(PermSCTId ,'') as PermSCTId ,
	isnull(NumPermisoSCT ,'') as NumPermisoSCT ,
	isnull(C.descripcion,'') as claveUnidadNom,
	isnull(NombreAseg ,'') as  NombreAseg,
	isnull(NumPolizaSeguro ,'') as NumPolizaSeguro ,
	isnull(AseguraMedAmbiente,'') as AseguraMedAmbiente,
	isnull(PolizaMedAmbiente,'') as PolizaMedAmbiente,
	isnull(ConfigVehicularId ,'') as ConfigVehicularId,
	isnull(B.descripcion,'') as configVehicularNom,
	isnull(PlacaVM,'') as PlacaVM ,
	isnull(AnioModeloVM,'') as AnioModeloVM 
	from tblAutotransportes A
	left join tblConfigAutotransporte B on A.ConfigVehicularId = B.clave 
	left join tblTipoPermiso C on A.PermSCTId = c.clave 
	where id = @id
	end
	/* Lee todos los registros de autransporte*/
	if @cmd=3
	begin
	select 
	id,
	isnull(PermSCTId ,'') as PermSCTId ,
	isnull(NumPermisoSCT ,'') as NumPermisoSCT ,
	isnull(NombreAseg ,'') as  NombreAseg,
	isnull(NumPolizaSeguro ,'') as NumPolizaSeguro ,	
	isnull(AseguraMedAmbiente,'') as AseguraMedAmbiente,
	isnull(PolizaMedAmbiente,'') as PolizaMedAmbiente,
	isnull(ConfigVehicularId ,'') as ConfigVehicularId ,
	isnull(PlacaVM,'') as PlacaVM ,
	isnull(AnioModeloVM,'') as AnioModeloVM 
	from tblAutotransportes
	where BorradorBit is null
	order by id asc
	end
	/* Actualiza un registro de un autransporte*/
	if @cmd=4
	begin
	update tblAutotransportes set 	
			PermSCTId = @PermSCTId ,
			NumPermisoSCT = @NumPermisoSCT ,
			NombreAseg = @NombreAseg,
			NumPolizaSeguro = @NumPolizaSeguro ,
			AseguraMedAmbiente = @AseguraMedAmbiente,
			PolizaMedAmbiente = @PolizaMedAmbiente,
			ConfigVehicularId = @ConfigVehicularId ,
			PlacaVM = @PlacaVM ,
			AnioModeloVM = @AnioModeloVM 
		where id = @id
	end
	/* Elimina un registro de un auttransporte*/
	if @cmd=5
	begin
	update tblAutotransportes set 	
			BorradorBit = 1
		where id = @id
	end
    /* Enlista para seleccionar en un combo*/
	if @cmd=6
	begin
	select 
	id,
	CONCAT('Placa: ',isnull(PlacaVM,''),', Año:',isnull(AnioModeloVM,''), ', Aseguradora: ',isnull(NombreAseg ,'')) as descripcion
	from tblAutotransportes 
	where BorradorBit is null
	order by id asc
	end
	/* determina si un transporte tiene remolque*/
	if @cmd=7
	begin
		select isnull(Remolque,'') from tblConfigAutotransporte where clave = @ConfigVehicularId
	end
END
GO
