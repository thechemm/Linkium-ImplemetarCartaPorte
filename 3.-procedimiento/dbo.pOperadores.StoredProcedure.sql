
/****** Object:  StoredProcedure [dbo].[pOperadores]    Script Date: 17/01/2022 01:28:53 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
alter PROCEDURE [dbo].[pOperadores] 
	-- Add the parameters for the stored procedure here
	@id int = 0, 
	@cmd int = 0,
	@tipofigura varchar(50) = null,
	@RFCOperador varchar(50) = null,
	@NumLicencia varchar(50) = null,
	@NombreOperador varchar(100) = null,
	@Calle_op varchar(100) = null,
	@NumeroExterior_op varchar(20) = null,
	@NumeroInterior_op varchar(20) = null,
	@Colonia_op varchar(120) = null,
	@Localidad_op varchar(120) = null,
	@Municipio_op varchar(10) = null,
	@Referencia_op varchar(200) = null,
	@Estado_op varchar(30) = null,
	@Pais_op varchar(10) = null,
	@CodigoPostal_op varchar(12) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   /* Crea un registro de operario*/
   if @cmd=1 
   begin
   insert into tblOperadores(RFCOperador,NumLicencia,NombreOperador, tipoFigura, CodigoPostal_op, Estado_op, Municipio_op, Colonia_op, Calle_op, NumeroExterior_op, NumeroInterior_op, Referencia_op)
				values(@RFCOperador, @NumLicencia, @NombreOperador, @tipofigura, @CodigoPostal_op, @Estado_op, @Municipio_op, @Colonia_op, @Calle_op, @NumeroExterior_op, @NumeroInterior_op, @Referencia_op )
   end
   /* Lee un registro de operario*/
   if @cmd=2 
   begin   
   select 
   id,
   isnull(tipoFigura,0) as tipofigura,
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
   isnull('MEX', '') as Pais_op,
   isnull(CodigoPostal_op, '') as CodigoPostal_op,
   isnull(dbo.fnColoniaNombre(CodigoPostal_op, Colonia_op),'') as colonia_nombre,
   isnull(dbo.fnEstadoNombre(CodigoPostal_op),'') as estado_nombre,
   isnull(dbo.fnMunicipioNombre(CodigoPostal_op),'') as municipio_nombre,
   isnull(dbo.fnFiguraTransporteNombre(tipoFigura),'') as tipoFigura_descripcion	
   from tblOperadores 
   where id = @id
  --where BorradorBit is null
   end
   /* Lee todos los registros de los operarios*/
   if @cmd=3 
   begin
   select 
   id,
   isnull(RFCOperador,'') as RFCOperador ,
   isnull(NumLicencia, '') as NumLicencia,
   isnull(NombreOperador, '') as NombreOperador
   from tblOperadores 
   where BorradorBit is null
   end
   /* Actualiza un registro de operario*/
   if @cmd=4 
   begin
   update tblOperadores set
		tipoFigura = @tipofigura,
		RFCOperador = @RFCOperador, 
		NumLicencia = @NumLicencia, 
		NombreOperador = @NombreOperador ,
		Calle_op = @Calle_op,
		NumeroExterior_op = @NumeroExterior_op,
		NumeroInterior_op = @NumeroInterior_op,
		Colonia_op = @Colonia_op,
		Localidad_op = @Localidad_op,
		Municipio_op = @Municipio_op,
		Referencia_op = @Referencia_op,
		Estado_op = @Estado_op,
		Pais_op = @Pais_op,
		CodigoPostal_op = @CodigoPostal_op
	where id = @id
   end
   /* Elimina un registro de operario*/
   if @cmd=5 
   begin
   update tblOperadores set
		BorradorBit = 1
	where id = @id
   end
   /* Enlista para seleccionar en un combo*/
	if @cmd=6
	begin
	select 
   id,
   isnull(NombreOperador, '') as nombre
   from tblOperadores 
   where BorradorBit is null
	end
END
GO
