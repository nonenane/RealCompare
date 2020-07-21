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
-- Description:	<��������� ������ ��� ������ �� ������� �����>
-- =============================================

ALTER PROCEDURE [RealCompare].[GetReportMainKass]
	@dateStart Date,
	@dateEnd Date,
	@typeReport int --1 - ����� �� ������� � ���������� ,2 -����� �� ���������� ������ ������
AS
BEGIN
	SET NOCOUNT ON;

IF @typeReport = 1
BEGIN
	select 
		mk.Data,
		case when rd.SourceDifference = 1 then '� ����. SQL' else '� ��������' end as nameType,
		rr.DateSubmission,
		rr.Number,
		cr.Comment,
		rr.DateConfirm
	from 
		Repair.j_RequestRepair rr
			inner join RealCompare.j_RequestOfDifference rd on rd.id_RequestRepair = rr.id
			inner join RealCompare.j_MainKass mk on mk.id = rd.id_MainKass
			inner join [Repair].[j_CommentRequest] cr on cr.id_RequestRepair = rr.id
	where 
		@dateStart<= cast(rr.DateSubmission as date) and cast(rr.DateSubmission as date) <=@dateEnd
END
ELSE IF @typeReport = 2
BEGIN
	---
	---��� ������ �������� ����� 
	select mk.MainKass,mk.Data,mk.isVVO , 1 as type
	from RealCompare.j_MainKass mk 
	where @dateStart <= mk.Data and mk.Data <= @dateEnd
	UNION ALL
	---��� ������ � �����. SQL� 
	select mk.MainKass,mk.Data,mk.isVVO, 2 as type
	from RealCompare.j_MainKass mk 
	where @dateStart <= mk.Data and mk.Data <= @dateEnd and mk.RealSQL is null
	UNION ALL
	---������ �����. SQL� ���������� �� ������������ 
	select mk.MainKass,mk.Data,mk.isVVO, 3 as type
	from RealCompare.j_MainKass mk 
	where @dateStart <= mk.Data and mk.Data <= @dateEnd and mk.RealSQL is not null 
END

END







