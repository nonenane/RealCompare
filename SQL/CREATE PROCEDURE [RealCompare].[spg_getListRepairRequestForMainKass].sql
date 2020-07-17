SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-07-14
-- Description:	Получение данных по заявкам на ремонт по главной кассе
-- =============================================
CREATE PROCEDURE [RealCompare].[spg_getListRepairRequestForMainKass]		 
	@id_MainKass int 
AS
BEGIN
	SET NOCOUNT ON;
	


select 
	rd.id_RequestRepair,
	rr.Number,
	rr.DateSubmission,
	rr.Fault,
	rr.DateConfirm,
	s.cName
from 
	RealCompare.j_RequestOfDifference  rd
		inner join Repair.j_RequestRepair rr on rr.id = rd.id_RequestRepair
		inner join Repair.s_Status s on s.id = rr.Status
where rd.id_MainKass = @id_MainKass 

END
