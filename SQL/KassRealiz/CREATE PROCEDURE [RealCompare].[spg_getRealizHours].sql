SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-07-14
-- Description:	Получение данных Реализации
-- =============================================
CREATE PROCEDURE [RealCompare].[spg_getRealizHours]		 
	@dateStart date,
	@dateEnd  date,
	@withDeps bit 

AS
BEGIN
	SET NOCOUNT ON;
	
IF @withDeps = 0
	BEGIN

	select cast(r.DateCount as date) as DateCount,0 as id_Departments,r.Realiz  from [dbo].[j_RealizHours]  r 
	inner join (select max(DateCount) as DateCount from  [dbo].[j_RealizHours] where @dateStart<= cast(DateCount as date) and cast(DateCount as date)<=@dateEnd
	group by cast(DateCount as date)) as n on n.DateCount = r.DateCount
	END
ELSE
	BEGIN	
		select r.DateCount,d.id_Departments,d.Realiz from  [dbo].[j_RealizHoursDepartments] d inner join (select r.id,cast(r.DateCount as date) as DateCount from [dbo].[j_RealizHours]  r 
			inner join (select max(DateCount) as DateCount from  [dbo].[j_RealizHours] where @dateStart<= cast(DateCount as date) and cast(DateCount as date)<=@dateEnd
			group by cast(DateCount as date)) as n on n.DateCount = r.DateCount) as r on r.id = d.id_RealizHours
	END
END
