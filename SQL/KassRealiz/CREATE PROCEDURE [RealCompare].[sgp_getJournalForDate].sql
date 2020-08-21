SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<S G Y>
-- Create date: <2020-08-21>
-- Description:	<ѕолучение данных дл€ сверки c журнала продаж>
-- =============================================
CREATE PROCEDURE [RealCompare].[sgp_getJournalForDate]
	@dateStart DateTime,
	@dateEnd DateTime,
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

DECLARE @operationClose varchar(5) = '509',@operationDiscount varchar(5) = '505,507,520'
DECLARE @strDepVVO nvarchar(150) = 'dpt_no <> 6'
	IF @isVVO = 1
		SET @strDepVVO = 'dpt_no = 6'


set @nSQL = '
select t.conDate,sum(t.cash_val)  as cash_val,dpt_no
from(
select 	
	case when t.op_code = 507 then -1 else 1 end * cast(cash_val as numeric(16,2))/100 as cash_val,
	case when convert(varchar(100),t.time,108) <''04:00:00'' then dateadd(day,-1,cast(t.time as date)) else cast(t.time as date) end as conDate,
	t.dpt_no	
from 
	'+@base+' t
		inner join  (select terminal,doc_id from '+@base+' where op_code in ('+@operationClose+') and '''+convert(varchar(150),cast(@startTime as datetime),120)+'''<=time and time<='''+convert(varchar(150),cast(@endTime as datetime),120)+''') t2 on t2.doc_id = t.doc_id and t2.terminal = t.terminal
where 
	t.op_code in ('+@operationDiscount+') and t.cash_val <> 0 and '+@strDepVVO+') as t
GROUP BY	
	t.conDate,t.dpt_no'

EXEC (@nSQL)

END







