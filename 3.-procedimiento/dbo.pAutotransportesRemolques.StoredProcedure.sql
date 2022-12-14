
/****** Object:  StoredProcedure [dbo].[pAutotransportesRemolques]    Script Date: 17/01/2022 01:28:53 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
alter PROCEDURE [dbo].[pAutotransportesRemolques] 
	-- Add the parameters for the stored procedure here
	@cmd int = 0, 
	@id int = 0,
	@autotransporteid int = 0,
	@descripcion varchar(100) = null,
	@placa varchar(50) = null,
	@SubTipoRem varchar(50) = null,
	@BorradorBit bit = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	/* Inserta un registro de un AutotransportesRemolques*/
	if @cmd=1
	begin
	 insert into tblAutotransportesRemolques (autotransporteid, placa, SubTipoRem, descripcion)
							values( @autotransporteid, @placa, @SubTipoRem, @descripcion)
	end
	/* Lee un registro de un AutotransportesRemolques*/
	if @cmd=2
	begin
	select 
	id,
	isnull(autotransporteid ,'') as autotransporteid ,
	isnull(placa ,'') as placa ,
	isnull(SubTipoRem ,'') as  SubTipoRem,
	isnull(descripcion ,'') as  descripcion
	from tblAutotransportesRemolques
	where id = @id
	end
	/* Lee todos los registros de AutotransportesRemolques*/
	if @cmd=3
	begin
	select 
	id,
	isnull(placa ,'') as placa ,
	isnull(SubTipoRem ,'') as SubTipoRem,
	isnull(descripcion ,'') as descripcion 
	from tblAutotransportesRemolques
	where (BorradorBit is null) and (@autotransporteid = null or autotransporteid = @autotransporteid)
	end
	/* Actualiza un registro de un AutotransportesRemolques*/
	if @cmd=4
	begin
	update tblAutotransportesRemolques set 	
			placa = @placa ,
			SubTipoRem = @SubTipoRem,
			descripcion = @descripcion
		where id = @id
	end
	/* Elimina un registro de un AutotransportesRemolques*/
	if @cmd=5
	begin
	update tblAutotransportesRemolques set 	
			BorradorBit = 1
		where id = @id
	end
	/* Elimina un remolques de un Autotransportess*/
	if @cmd=6
	begin
	update tblAutotransportesRemolques set 	
			BorradorBit = 1
		where autotransporteid = @autotransporteid
	end
END
GO
