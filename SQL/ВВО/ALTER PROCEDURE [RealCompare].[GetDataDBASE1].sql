USE [dbase1]
GO
/****** Object:  StoredProcedure [RealCompare].[GetDataDBASE1]    Script Date: 14.07.2020 16:37:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Kamaev A V>
-- Create date: <2020-06-05>
-- Description:	<Получение данных для сверки c j_realiz ВВО>
-- =============================================
-- EXEC [RealCompare].[GetCompareData] '2015-01-13', '2015-01-13', 3, 234, 0, 1, 1, 1
--exec RealCompare.GetCompareData '2017-10-27', '2017-10-27',3,234,0,1,1,0
--exec [RealCompare].[GetDataDBASE1] '2017-10-27', '2017-10-27',2
ALTER PROCEDURE [RealCompare].[GetDataDBASE1]
	@dateStart DateTime,
	@dateEnd DateTime
	--@groupType int = 1 --Тип группировки. 1 - Дата. 2 - Дата, отдел. 3 - Дата, отдел, товар.
AS
BEGIN
	SET NOCOUNT ON;
	--Реализация j_realiz	
		SELECT  ltrim(rtrim(t.ean)) as ean,				
				r.drealiz AS dreal,
				SUM([r].[summa]) as RealSql,
				t.id_otdel as id_dep,
				dep.name as depName
				
		FROM [dbo].[j_realiz] r 
			LEFT JOIN s_tovar t ON t.id = r.id_tovar
			left join dbo.departments dep on t.id_otdel = dep.id
			--left join s_ntovar nt on t.id = nt.id_tovar and nt.tdate_n = (select max(tdate_n) from s_ntovar where id_tovar = t.id)
		WHERE CAST(drealiz AS DATE) BETWEEN CAST(@dateStart AS DATE) AND CAST(@dateEnd AS DATE)
		AND t.id_otdel = 6 and dep.if_comm = 1 and dep.ldeyst  = 1
		GROUP BY drealiz, t.ean, id_otdel, name		
END







