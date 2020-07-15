SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-07-14
-- Description:	Получение данных по главной кассе за промежуток дат
-- =============================================
CREATE PROCEDURE [RealCompare].[spg_getMainKassForListDate]		 
	@dateStart Date,
	@dateEnd Date
AS
BEGIN
	SET NOCOUNT ON;
	
	select 
		mk.id,mk.Data,mk.MainKass,mk.ChessBoard,mk.RealSQL,mk.isVVO,mk.DateEdit,isnull(ltrim(rtrim(lu.FIO)),'') as FIO
	from 
		[RealCompare].[j_MainKass] mk
			left join dbo.ListUsers lu on lu.id = mk.id_Editor
	where 
		@dateStart<=mk.Data and mk.Data<=@dateEnd
END
