SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-07-14
-- Description:	Получение данных по главной кассе
-- =============================================
ALTER PROCEDURE [RealCompare].[spg_getMainKass]		 
	@date date,
	@isVVO bit
AS
BEGIN
	SET NOCOUNT ON;
	
	select k.id,k.isVVO,k.[Data],k.MainKass from [RealCompare].[j_MainKass] k where k.Data = @date and k.isVVO = @isVVO

END
