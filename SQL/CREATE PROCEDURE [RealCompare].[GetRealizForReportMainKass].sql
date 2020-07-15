USE [dbase1]
GO
/****** Object:  StoredProcedure [RealCompare].[GetDataDBASE1]    Script Date: 14.07.2020 16:34:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Sporykhin G Y>
-- Create date: <2020-07-15>
-- Description:	<Получение данных по реализации на промежуток дат>
-- =============================================

ALTER PROCEDURE [RealCompare].[GetRealizForReportMainKass]
	@dateStart Date,
	@dateEnd Date
AS
BEGIN
	SET NOCOUNT ON;
		select 
			r.drealiz AS dreal,
			SUM([r].[summa]) as RealSql				
		FROM [dbo].[j_realiz] r 
			LEFT JOIN s_tovar t ON t.id = r.id_tovar
			LEFT JOIN dbo.departments dep on t.id_otdel = dep.id
		WHERE 
			@dateStart<=cast(r.drealiz as date) and cast(r.drealiz as date)<=@dateEnd
		AND t.id_otdel != 6 and dep.if_comm = 1 and dep.ldeyst  = 1
		GROUP BY drealiz
END







