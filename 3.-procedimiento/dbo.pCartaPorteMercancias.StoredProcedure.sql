
/****** Object:  StoredProcedure [dbo].[pCartaPorteMercancias]    Script Date: 17/01/2022 01:28:53 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
alter PROCEDURE [dbo].[pCartaPorteMercancias] 
	-- Add the parameters for the stored procedure here
	@cmd int = 0, 
	@id int = 0,
	@cartaporteid int = 0,
	@BienesTransp varchar(10) = '',
	@BienesTransp_D varchar(600) = '',
	@Descripcion varchar(100) = '',
	@Cantidad float = 0,
	@ClaveUnidad varchar(10) = '',
	@PesoEnKg float = 0,
	@ValorMercancia float = 0,
	@Moneda varchar(10) = '',

	@MaterialPeligroso varchar(10) = null,
	@CveMaterialPeligroso varchar(10) = null,
	@DescripEmbalaje varchar(200) = null,
	@Embalaje varchar(100) = null,

	@FraccionArancelaria varchar(100) = '',
	@UUIDComercioExt varchar(1000) = '',
	@fechaCreacion datetime = 0,
	@BorradorBit bit = null,
	@cfdid int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Model CRUD 
	--Create
	/* crea un nuevo registro  */
if @cmd=1 
begin
insert into tblCartaPorteMercancias (cartaporteid, BienesTransp, BienesTransp_D, Descripcion, Cantidad, ClaveUnidad, PesoEnKg, ValorMercancia, Moneda, MaterialPeligroso, CveMaterialPeligroso, DescripEmbalaje, Embalaje, FraccionArancelaria, UUIDComercioExt, fechaCreacion, BorradorBit)
                             values(@cartaporteid, @BienesTransp, @BienesTransp_D, @Descripcion, @Cantidad, @ClaveUnidad, @PesoEnKg, @ValorMercancia, @Moneda, @MaterialPeligroso, @CveMaterialPeligroso, @DescripEmbalaje, @Embalaje, @FraccionArancelaria, @UUIDComercioExt, GETDATE(), @BorradorBit)
end
	
	--Read
	/* lee los registros de un cartaporte  */
if @cmd=2
begin
	select
	a.id,
	isnull(a.BienesTransp, '') as BienesTransp,
	isnull(a.BienesTransp_D, '') as BienesTransp_D,
	isnull(a.Descripcion, '') as Descripcion,
	isnull(a.Cantidad, '') as Cantidad,
	isnull(a.ClaveUnidad, '') as ClaveUnidad,
	isnull(a.PesoEnKg, '') as PesoEnKg,
	isnull(a.ValorMercancia, '') as ValorMercancia,
	isnull(a.Moneda, '') as Moneda,
	isnull(a.MaterialPeligroso, '') as MaterialPeligroso,
	isnull(a.CveMaterialPeligroso, '') as CveMaterialPeligroso,
	isnull(a.DescripEmbalaje, '') as DescripEmbalaje,
	isnull(a.Embalaje, '') as Embalaje,	
	isnull(FraccionArancelaria, '') as FraccionArancelaria,
	isnull(UUIDComercioExt, '') as UUIDComercioEx
	from tblCartaPorteMercancias a 
	where a.cartaporteid = @cartaporteid and (a.BorradorBit = 0 or a.BorradorBit is null)
end

	/* lee un registro por el id  */
if @cmd=3
begin
	select
	isnull(BienesTransp, '') as BienesTransp,
	isnull(BienesTransp_D, '') as BienesTransp_D,
	isnull(Descripcion, '') as Descripcion,
	isnull(Cantidad, '') as Cantidad,
	isnull(ClaveUnidad, '') as ClaveUnidad,
	isnull(PesoEnKg, '') as PesoEnKg,
	isnull(ValorMercancia, '') as ValorMercancia,
	isnull(Moneda, '') as Moneda,
	isnull(MaterialPeligroso, '') as MaterialPeligroso,
	isnull(CveMaterialPeligroso, '') as CveMaterialPeligroso,
	isnull(DescripEmbalaje, '') as DescripEmbalaje,
	isnull(Embalaje, '') as Embalaje,	
	isnull(FraccionArancelaria, '') as FraccionArancelaria,
	isnull(UUIDComercioExt, '') as UUIDComercioExt	
	from tblCartaPorteMercancias 
	where id= @id and (BorradorBit = 0 or BorradorBit is null)
end
	--Update
	/* actualza un registro por el id  */
if @cmd=4
begin
	update tblCartaPorteMercancias 
	set BienesTransp = @BienesTransp,
	 BienesTransp_D= @BienesTransp_D,
	 Descripcion = @Descripcion,
	 Cantidad = @Cantidad,
	 ClaveUnidad = @ClaveUnidad,
	 PesoEnKg = @PesoEnKg,
	 ValorMercancia = @ValorMercancia,
	 Moneda = @Moneda,
	 MaterialPeligroso = @MaterialPeligroso,
	 CveMaterialPeligroso = @CveMaterialPeligroso,
	 DescripEmbalaje = @DescripEmbalaje,
	 Embalaje = @Embalaje,
	 FraccionArancelaria = @FraccionArancelaria,
	 UUIDComercioExt = @UUIDComercioExt
	where id = @id 
end
	--Delete
	/* elimina un registro  */
if @cmd=5
begin
	update tblCartaPorteMercancias 
	set BorradorBit = 1
	where id = @id
end
--Selecciona el carta porte id de un registro
if @cmd=6
begin
select isnull(id,0) from tblCartaPorte where cfdid = @cfdid 
end
--selecciona todas las mercancias para el pdf
if @cmd=7
begin

  select @cartaporteid = isnull(id,0) from tblCartaPorte where cfdid = @cfdid

  select ROW_NUMBER() OVER(ORDER BY a.id asc) AS numero, 
						a.BienesTransp as clave,
						case a.Descripcion when null then
						a.BienesTransp_D when '' then a.BienesTransp_D else a.Descripcion end as descripcion,
						a.cantidad as cantidad,
						a.PesoEnKg as PesoEnKg,						
						isnull((select top 1 c.nombre from tblClaveUnidad c where  a.ClaveUnidad = c.codigo),'-') as unidad,
						isnull(a.ClaveUnidad,'') as ClaveUnidad,
						a.MaterialPeligroso   as MaterialPeligroso,
						a.CveMaterialPeligroso as CveMaterialPeligroso,
						isnull((select top 1 d.descripcion from tblTipoEmbalaje d where d.clave = a.Embalaje ),'-') as Embalaje
						from tblCartaPorteMercancias a
				where a.cartaporteid = @cartaporteid
end
END


GO
