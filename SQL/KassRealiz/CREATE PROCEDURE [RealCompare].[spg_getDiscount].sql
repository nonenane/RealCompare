SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-07-14
-- Description:	Получение данных по скидкам
-- =============================================
CREATE PROCEDURE [RealCompare].[spg_getDiscount]		 
	@dateStart date,
	@dateEnd  date,
	@isVVO bit
AS
BEGIN
	SET NOCOUNT ON;
	

DECLARE @year int, @month int 
select @year= DATEPART(YEAR,@dateStart), @month =DATEPART(month,@dateStart )

DECLARE @nSQL nvarchar(MAX) 
DECLARE @base nvarchar(100) = 'dbo.journal_'+cast(@year as nvarchar(4))+'_'+case when @month <10 then '0'+cast(@month as nvarchar(2)) else cast(@month as nvarchar(2)) end

DECLARE @startTime datetime = @dateStart,@endTime datetime = @dateEnd
SET  @startTime = DATEADD(HOUR,6,@startTime)
SET  @endTime = DATEADD(HOUR,27,@endTime)

DECLARE @operationClose varchar(5) = '509',@operationDiscount varchar(5) = '524'


set @nSQL = 'select time,cast(cash_val as numeric(16,2))/100 as cash_val,case when convert(varchar(100),t.time,108) <''04:00:00'' then dateadd(day,-1,cast(t.time as date)) else cast(t.time as date) end as conDate from '+@base+' t
inner join  (select terminal,doc_id from '+@base+' where op_code = '+@operationClose+' and '''+convert(varchar(150),cast(@startTime as datetime),120)+'''<=time and time<='''+convert(varchar(150),cast(@endTime as datetime),120)+''') t2 on t2.doc_id = t.doc_id and t2.terminal = t.terminal
where t.op_code = '+@operationDiscount+' and t.cash_val <> 0'

CREATE TABLE #TMP (inTime datetime, cash_val  numeric(16,2),conDate date)
insert INTO #TMP
EXEC (@nSQL)

select 
	conDate,sum(cash_val) as cash_val,@isVVO as isVVO
from 
	#TMP
group by conDate
order by conDate asc

DROP TABLE #TMP
END
