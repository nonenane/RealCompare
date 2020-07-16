--select * from [RealCompare].[j_MainKass]
--select * from [RealCompare].[j_RequestOfDifference]


DECLARE @dateStart date ='2020-01-01', @dateEnd date = '2020-06-01'

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
	@dateStart<= rr.DateSubmission and rr.DateSubmission<=@dateEnd

	---
---��� ������ �������� ����� 
select mk.MainKass,mk.Data,mk.isVVO from RealCompare.j_MainKass mk 
where @dateStart <= mk.Data and mk.Data <= @dateEnd

---��� ������ � �����. SQL� 
select mk.MainKass,mk.Data,mk.isVVO from RealCompare.j_MainKass mk 
where @dateStart <= mk.Data and mk.Data <= @dateEnd and mk.RealSQL is null

---������ �����. SQL� ���������� �� ������������ 
select mk.MainKass,mk.Data,mk.isVVO from RealCompare.j_MainKass mk 
where @dateStart <= mk.Data and mk.Data <= @dateEnd and mk.RealSQL is not null 


exec [RealCompare].[GetRealizForReportMainKass] '2020-01-01','2020-07-15'

exec [RealCompare].[GetDataDBASE1] '2020-01-01','2020-07-15'