--DECLARE @days int,@hours int,@minutes int, @seconds int;
--select @days=(select expDate from demo.dbo.Boards where id=10),
--@hours = DATEPART(HOUR,(select expTime from [demo].[dbo].[Boards] where id=10)),
--@minutes= DATEPART(MINUTE,(select expTime from [demo].[dbo].[Boards] where id=10)),
--@seconds= DATEPART(SECOND,(select expTime from [demo].[dbo].[Boards] where id=10))
--select DATEADD(DAY, @days, 
--							(SELECT DATEADD(HOUR, @hours,
--														(SELECT DATEADD(MINUTE, @minutes,
--																						(SELECT DATEADD(SECOND,@seconds,GETDATE()))
--																		)
--														)
--											)
--							)
--				)

insert into demo.dbo.Invoices (assetid,qty,boardid,memberid,expireadate)
select a.id, a.assetid, a.qty, a.boardid, a.memberid, a.accountid,  a.side, a.dealType,
	dateadd(second, datepart(second, b.expTime), dateadd(minute, datepart(minute, b.expTime), dateadd(hour, datepart(hour, b.expTime), cast(dateadd(day, b.expDate, cast(GETDATE() as date)) as datetime)))) as expireadate
from demo.dbo.Deal2 a
left outer join demo.dbo.Boards b on a.boardid=b.id
where cast(a.modified as date) = cast(GETDATE() as date)
