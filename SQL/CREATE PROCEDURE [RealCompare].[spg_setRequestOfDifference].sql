SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-07-16
-- Description:	Сохранение связки заявки на ремонт и сверки главной кассы
-- =============================================
CREATE PROCEDURE [RealCompare].[spg_setRequestOfDifference]		 
	@id_MainKass int,
	@id_RequestRepair int,
	@isVVO bit,
	@SourceDifference int	

AS
BEGIN
	SET NOCOUNT ON;

BEGIN TRY 
	
		INSERT INTO RealCompare.j_RequestOfDifference (id_MainKass,id_RequestRepair,isVVO,SourceDifference)
		VALUES (@id_MainKass,@id_RequestRepair,@isVVO,@SourceDifference)

		select cast(SCOPE_IDENTITY() as int) as id

END TRY 
BEGIN CATCH 
	SELECT -9999 as id
	return;
END CATCH
	
END
