SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-07-16
-- Description:	Установка или удаление сверки
-- =============================================
ALTER PROCEDURE [RealCompare].[spg_setValidateMainKass]		 
	@id int,
	@RealSQL numeric(16,2) = null,
	@ChessBoard numeric(16,2) = null,
	@id_user int

AS
BEGIN
	SET NOCOUNT ON;

BEGIN TRY 
	
		UPDATE RealCompare.j_MainKass 
		set RealSQL = @RealSQL,ChessBoard = @ChessBoard,DateEdit = GETDATE(),id_Editor = @id_user 
		where id = @id

		select 0 as id

END TRY 
BEGIN CATCH 
	SELECT -9999 as id
	return;
END CATCH
	
END
