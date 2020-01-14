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
 
insert into demo.dbo.Invoices (boardid, dealno, side, accountid, assetid, dealType, qty, totalPrice, state, fee, expiredate, expiretime)
select						 a.boardid, a.dealno, a.side, a.accountid, a.assetid, a.dealType, a.qty, a.totalPrice, a.state, a.fee, 
	dateadd(day, b.expDate, cast(GETDATE() as date)), 
	b.expTime
from demo.dbo.Deal2 a 
left outer join demo.dbo.Boards b on a.boardid=b.id 
where a.invoice=0 
update demo.dbo.Deal2
set invoice=1
where invoice=0;
select DATEPART(NANOSECOND, GETDATE())