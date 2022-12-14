
/****** Object:  StoredProcedure [dbo].[pCartaPorte]    Script Date: 17/01/2022 01:28:53 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
alter PROCEDURE [dbo].[pCartaPorte] 
	-- Add the parameters for the stored procedure here
	@cmd int = 0, 
	@id int = 0,
	@cfdid int = 0,
	@origenid int = null,
    @autotransporteid int = null,
	@TranspInternac varchar(50) = null,
    @FechaHoraSalida datetime = null,
    @FechaHoraProgLlegada datetime = null,
    @TotalDistRec float = null,
    @UnidadPeso varchar(50) = null,
    @PesoNetoTotal float = null,
    @FechaCrea datetime = null,
    @FechaModifica datetime = null,
    @fecha_factura datetime = null,
    @serie varchar(50) = null,
    @serieid int = null,
    @folio int = null,
    @uuid varchar(500) = null,
    @timbrado bit = null,
    @sello varchar(500) = null,
    @cadenaoriginal varchar(500) = null,
    @estatus int = null,
    @BorradorBit bit = null,
	@cartaPorteBit bit = null,
	@ckbit bit = null,
	@cartaporteid int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	/*:::::::::::::::: START CRUD [tblCartaPorte]:::::::::::::::::::::::::*/
	/*:::::::::::::::: CREATE ONE [tblCartaPorte]:::::::::::::::::::::::::*/
	IF @cmd = 1
	BEGIN  
		set @cartaporteid = (select top 1 isnull(id,0) from tblcartaporte where cfdid=@cfdid)
		--select @cartaporteid 
	if @cartaporteid > 0
	begin
		select @cartaporteid 
	end
	else
		begin
		INSERT INTO [dbo].[tblCartaPorte]
				   (
				    [cfdid]
				   ,[origenid]
				   ,[autotransporteid]
				   ,[TranspInternac]
				   ,[FechaHoraSalida]
				   ,[TotalDistRec]
				   ,[UnidadPeso]
				   ,[PesoNetoTotal]
				   ,[FechaCrea]
				   ,[FechaModifica]
				   ,[fecha_factura]
				   ,[serie]
				   ,[serieid]
				   ,[folio]
				   ,[uuid]
				   ,[timbrado]
				   ,[sello]
				   ,[cadenaoriginal]
				   ,[estatus]
				   ,[BorradorBit])
				 VALUES
				  (@cfdid,
				   @origenid,
				   @autotransporteid,
				   @TranspInternac,
				   @FechaHoraSalida,
				   @TotalDistRec,
				   @UnidadPeso,
				   @PesoNetoTotal,
				   getdate(),
				   @FechaModifica,
				   @fecha_factura,
				   @serie,
				   @serieid,
				   @folio,
				   @uuid,
				   @timbrado,
				   @sello,
				   @cadenaoriginal,
				   @estatus,
				   @BorradorBit)
		select @@identity
		end
	END
	/*:::::::::::::::: READ ONE [tblCartaPorte]:::::::::::::::::::::::::*/
	IF @cmd = 3
	BEGIN 
	SELECT [id],
      		isnull(origenid,'') as origenid,   
			isnull(autotransporteid,'') as autotransporteid,   
			isnull(FechaHoraSalida,'') as FechaHoraSalida,     
			isnull(TotalDistRec,'') as TotalDistRec,  
			isnull(UnidadPeso,'') as UnidadPeso,   
			isnull(PesoNetoTotal,'') as PesoNetoTotal,  
			isnull(FechaCrea,'') as FechaCrea,  
			isnull(FechaModifica,'') as FechaModifica,  
			isnull(fecha_factura,'') as fecha_factura,  
			isnull(serie,'') as serie,  
			isnull(serieid,'') as serieid,  
			isnull(folio,'') as folio,  
			isnull(uuid,'') as uuid,  
			isnull(timbrado,'') as timbrado,  
			isnull(sello,'') as sello,  
			isnull(cadenaoriginal,'') as cadenaoriginal,  
			isnull(estatus,'') as estatus
	 FROM [dbo].[tblCartaPorte]
	 where id = @id
	END
	/*:::::::::::::::: READ ALL [tblCartaPorte]:::::::::::::::::::::::::*/
	IF @cmd = 2
	BEGIN 
	SELECT [id],
      		isnull(origenid,'') as origenid,   
			isnull(autotransporteid,'') as autotransporteid,   
			isnull(FechaHoraSalida,'') as FechaHoraSalida,     
			isnull(TotalDistRec,'') as TotalDistRec,  
			isnull(UnidadPeso,'') as UnidadPeso,   
			isnull(PesoNetoTotal,'') as PesoNetoTotal,  
			isnull(FechaCrea,'') as FechaCrea,  
			isnull(FechaModifica,'') as FechaModifica,  
			isnull(fecha_factura,'') as fecha_factura,  
			isnull(serie,'') as serie,  
			isnull(serieid,'') as serieid,  
			isnull(folio,'') as folio,  
			isnull(uuid,'') as uuid,  
			isnull(timbrado,'') as timbrado,  
			isnull(sello,'') as sello,  
			isnull(cadenaoriginal,'') as cadenaoriginal,  
			isnull(estatus,'') as estatus   
	 FROM [dbo].[tblCartaPorte]
	 where borradorbit is null or borradorbit = 0
	END
	/*:::::::::::::::: UPDATE ONE [tblCartaPorte]:::::::::::::::::::::::::*/
	IF @cmd = 4
	BEGIN 
	UPDATE [dbo].[tblCartaPorte]
    SET 
      origenid = @origenid,   
      autotransporteid = @autotransporteid,  
	  TranspInternac = @TranspInternac,
      FechaHoraSalida = @FechaHoraSalida,     
      TotalDistRec = @TotalDistRec,  
      UnidadPeso = @UnidadPeso,   
      PesoNetoTotal = @PesoNetoTotal,  
      FechaCrea = @FechaCrea,
	  ckbit = @ckbit,
      FechaModifica = getdate()
	WHERE id = @id
	END
	/*:::::::::::::::: DELETE ONE [tblCartaPorte]:::::::::::::::::::::::::*/
	IF @cmd = 5
	BEGIN 
	UPDATE [dbo].[tblCartaPorte]
    SET 
      [borradorbit] = 1
	 ,[FechaModifica] = getdate()
	WHERE id = @id
	END
	/*:::::::::::::::: END CRUD [tblCartaPorte]:::::::::::::::::::::::::*/


-- selecciona si un CFD contiene carta porte
if @cmd=7
begin 
select top 1 id,
			isnull(origenid,'') as origenid,   
			isnull(autotransporteid,'') as autotransporteid,   
			isnull(FechaHoraSalida,getdate()) as FechaHoraSalida,     
			isnull(TotalDistRec,'') as TotalDistRec,  
			isnull(UnidadPeso,'KGM') as UnidadPeso,   
			isnull(PesoNetoTotal,'') as PesoNetoTotal,  
			isnull(FechaCrea,'') as FechaCrea,  
			isnull(FechaModifica,'') as FechaModifica,  
			isnull(fecha_factura,'') as fecha_factura,  
			isnull(serie,'') as serie,  
			isnull(serieid,'') as serieid,  
			isnull(folio,'') as folio,  
			isnull(uuid,'') as uuid,  
			isnull(timbrado,'') as timbrado,  
			isnull(sello,'') as sello,  
			isnull(cadenaoriginal,'') as cadenaoriginal,  
			isnull(estatus,'') as estatus
from tblCartaPorte where cfdid = @cfdid 
end
 /*	Inserta un carta porte como tipo 'T' */
--if @cmd=8
--	begin
--	declare @cpid int = 0
--	insert into tblCartaPorte ( clienteid, razonsocial, RFCDestinatario   )
--	select '0', 'Cliente Generico', 'XAXX010101000'			
--	select @cpid = @@identity 
--	insert into tblCartaPorteMercancias(cartaporteid, BienesTransp, Cantidad, ClaveUnidad, fechaCreacion)
--	values(@cpid,'78101800','1.00','E48', GETDATE())
--	select @cpid as cfdid
--	end
if @cmd=9
begin
	select * from tblCFD where clienteid = 0 order by fecha_crea desc
end
	/*	Regresa siguiente serie y folio disponibles	*/
	if @cmd=10
		begin
			
			declare @tmpserie varchar(20)
			declare @tmpfolio bigint
			select top 1 @tmpserie=isnull(serie,''), @tmpfolio=folio from tblMisFolios where isnull(utilizado,0)=0 and tipoid=5 order by folio asc
			select top 1 isnull(serie,'') as serie, folio, aprobacion, annio_solicitud, isnull(tipoid,0) as tipoid from tblMisFolios where isnull(utilizado,0)=0 and tipoid=5 order by folio asc
			
			update tblMisFolios set utilizado=1, fecha_utilizacion=getdate() where isnull(serie,'')=@tmpserie and folio=@tmpfolio
			
			update tblCartaPorte set estatus=2,  
			folio=@tmpfolio, serie=@tmpserie, FechaCrea=getdate(), 
			fecha_factura=getdate()
			where id=@id
		end
	/*	Marca el cfd como no timbrado cuando haya falla con el PAC	*/
	if @cmd=11
		begin
			select top 1 @tmpserie=isnull(serie,''), @tmpFolio=isnull(folio,'') from tblCartaPorte where id=@id
			UPDATE tblMisFolios SET utilizado=NULL, fecha_utilizacion=NULL where serie=@tmpserie and folio=@tmpFolio and tipoid = 5

			update tblCartaPorte set timbrado=0, serie=NULL, folio=NULL where id=@id 
		end

    /*	Marca el cfd como timbrado cuando no haya falla con el PAC	*/
	if @cmd=12
	begin
		update tblCartaPorte set timbrado=1, uuid=@uuid,  estatus = 1 where id=@id		   
	end
	/*Seleccionar la serie y el folio de un carta porte timbrado*/
	if @cmd=14 
	begin
	select isnull(serie, '') as serie, isnull(folio, 0) as folio from tblCartaPorte where id = @id 
	end
	/*	Cancela CFD	*/
	if @cmd=15
	begin
		update tblCartaPorte set estatus=3 where id= @id
	end
	/* Regresa el servicio de un carta porte*/
	if @cmd=16
	begin
	declare @claveprodserv varchar(20)
	declare @claveunidad varchar(20)
	declare @unidad varchar(20)
	declare @descripcion varchar(20)
	
	select @claveprodserv = codigo, @descripcion = descripcion  from tblClaveProdServ where codigo = '78101800'
	select @claveunidad=codigo, @unidad = nombre  from tblClaveUnidad where codigo = 'E48'

	select  
	@claveprodserv as claveprodserv, 
	1 as cantidad,
	@claveunidad as claveunidad, 
	@unidad as unidad, 
	@descripcion as descripcion, 
	0 as base, 
	0 as tasaOcuota,
	0 as impuesto,
	0 as tipofactor,
	0 as importeT,
	0 as precio,
	0 as descuento, 
	0 as importe	
	end
	/* Direccion de origen para pdf*/
	if @cmd=17
	begin
		select
		isnull(A.rfc_or,'') as rfc,
		isnull(A.nombre_or,'') as nombre,
		isnull(B.FechaHoraSalida,'') as fecha,
		isnull(A.idOR,'') as id_or,
		CONCAT(
		isnull(A.Calle_or,''), ' ',
		isnull(A.NumeroExterior_or,''), ' ',
		isnull(A.NumeroInterior_or,''), ',',	
		' Col. ',
		isnull(dbo.fnColoniaNombre(A.CodigoPostal_or, A.Colonia_or),''),
		'. Municipio de ',
		isnull(dbo.fnMunicipioNombre(A.CodigoPostal_or),''), ', ',
		isnull(dbo.fnEstadoNombre(A.CodigoPostal_or), ' '),
		', MEX. CP ',
		isnull(A.CodigoPostal_or, ''), '. ', 
		isnull(A.Referencia_or,'')
		) as direccion
		from tblOrigenDirecciones A
		left join tblCartaPorte B on B.origenid = A.id
		where B.id = @cartaporteid
		and A.borradorbit is null
		and B.borradorBit is null
	end
	/* Direccion de destino para pdf*/
	if @cmd=18
	begin
		select
		isnull(A.rfc_des,'') as rfc,
		isnull(A.nombre_des,'') as nombre,
		isnull(B.fechaHoraLlegada,'') as fecha,
		isnull(A.idDE,'') as id_des,
		isnull(B.distanciaRecorrida,'') as distancia,
		CONCAT(
		isnull(A.Calle_des,''), ' ',
		isnull(A.NumeroExterior_des,''), ' ',
		isnull(A.NumeroInterior_des,''), ',',	
		' Col. ',
		isnull(dbo.fnColoniaNombre(A.CodigoPostal_des, A.Colonia_des),''),
		'. Municipio de ',
		isnull(dbo.fnMunicipioNombre(A.CodigoPostal_des),''), ', ',
		isnull(dbo.fnEstadoNombre(A.CodigoPostal_des), ' '),
		', MEX. CP ',
		isnull(A.CodigoPostal_des, ''), '. ', 
		isnull(A.Referencia_des,'')
		) as direccion
		from tblMisClientesDirecciones A
		left join tblCartaPorteDestinos B on B.clienteDireccionId = A.id		
		where B.cartaPorteId = @cartaporteid
		and A.borradorbit is null
		and B.borradorBit is null
		order by B.fechaHoraLlegada asc
	end
	/* Direccion de operadores para pdf*/
	if @cmd=19
	begin
		select
		isnull(C.descripcion,'') as tipo,
		isnull(A.RFCOperador,'') as rfc,
		isnull(A.NombreOperador,'') as nombre,
		isnull(A.NumLicencia,'') as licencia,
		CONCAT(
		isnull(A.Calle_op,''), ' ',
		isnull(A.NumeroExterior_op,''), ' ',
		isnull(A.NumeroInterior_op,''), ',',	
		' Col. ',
		isnull(dbo.fnColoniaNombre(A.CodigoPostal_op, A.Colonia_op),''),
		'. Municipio de ',
		isnull(dbo.fnMunicipioNombre(A.CodigoPostal_op),''), ', ',
		isnull(dbo.fnEstadoNombre(A.CodigoPostal_op), ' '),
		', MEX. CP ',
		isnull(A.CodigoPostal_op, ''), '. ', 
		isnull(A.Referencia_op,'')
		) as direccion
		from tblOperadores A
		left join tblCartaPorteOperadores B on B.operadorid = A.id
		left join tblFiguraTransporte C on A.tipoFigura = C.clave 
		where B.cartaPorteId = @cartaporteid
		and A.borradorbit is null
		and B.borradorBit is null		
		order by B.id asc
	end
	/*remolques de un carta porte para pdf*/
	if @cmd=20
	begin
		declare @remolque varchar(10) = null
		declare @confveh varchar(10) = null
		select @confveh = isnull(a.ConfigVehicularId,'') from tblAutotransportes a inner join tblCartaPorte b on b.autotransporteid = a.id where b.id = @cartaporteid
		select @remolque = isnull(Remolque,'') from tblConfigAutotransporte where clave = @confveh
		if not @remolque = '0'
		begin
			select 
			isnull(A.descripcion,'') as tipo,
			isnull(A.placa, '') as placa
			from tblAutotransportesRemolques A
			left join tblCartaPorte B on A.autotransporteid = B.autotransporteid 
			where B.id =@cartaporteid and A.BorradorBit is null
		end
		else
		begin
		select '-' as placa, '-' as tipo
		end
	end
END
GO
