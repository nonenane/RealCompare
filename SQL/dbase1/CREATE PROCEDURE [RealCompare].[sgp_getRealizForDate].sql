USE [dbase1]
GO
/****** Object:  StoredProcedure [RealCompare].[GetDataDBASE1]    Script Date: 14.07.2020 16:34:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<S P Y>
-- Create date: <2020-08-21>
-- Description:	<��������� ������ ��� ������ c j_realiz>
-- =============================================
CREATE PROCEDURE [RealCompare].[sgp_getRealizForDate]
	@dateStart DateTime,
	@dateEnd DateTime,
	@isVVO bit,
	@withTovar bit
AS
BEGIN
	SET NOCOUNT ON;
IF @withTovar = 1
	BEGIN
		SELECT  
			ltrim(rtrim(t.ean)) as ean,				
			r.drealiz AS dreal,
			SUM([r].[summa]) as RealSql,
			t.id_otdel as id_dep,
			dep.name as depName				
		FROM 
			[dbo].[j_realiz] r 
				LEFT JOIN s_tovar t ON t.id = r.id_tovar
				left join dbo.departments dep on t.id_otdel = dep.id		
		WHERE 
			@dateStart<=cast(r.drealiz as date) and  cast(r.drealiz as date)<=@dateEnd
			AND ((@isVVO =0 and t.id_otdel != 6) OR (@isVVO = 1 and t.id_otdel = 6))
			and dep.if_comm = 1 
			and dep.ldeyst  = 1
		GROUP BY 
			drealiz, t.ean, id_otdel, name		
	END
ELSE
	BEGIN
		SELECT  			
			r.drealiz AS dreal,
			SUM([r].[summa]) as RealSql,
			t.id_otdel as id_dep,
			dep.name as depName				
		FROM 
			[dbo].[j_realiz] r 
				LEFT JOIN s_tovar t ON t.id = r.id_tovar
				left join dbo.departments dep on t.id_otdel = dep.id				
		WHERE 
			@dateStart<=cast(r.drealiz as date) and  cast(r.drealiz as date)<=@dateEnd
			AND ((@isVVO =0 and t.id_otdel != 6) OR (@isVVO = 1 and t.id_otdel = 6))
			and dep.if_comm = 1 
			and dep.ldeyst  = 1
		GROUP BY 
			drealiz,id_otdel, dep.name		
	END
END







