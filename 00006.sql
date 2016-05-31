-----REMOVE DUPLCATE USERS FROM PROD---------
--set missing name
update [user] set firstname = 'Kristen', surname='Davison' where email='kristendavison@hotmail.com'
GO

--copy over passes
update pass
set pass.user_id = u1.id
from [user] as u1
inner join [user] as u2 on u1.email = u2.email and u1.id != u2.id and u1.activationkey = '00000000-0000-0000-0000-000000000000' and u2.activationkey != '00000000-0000-0000-0000-000000000000'
inner join pass as p on p.user_id = u2.id
GO

--delete duplicate class attendance
delete classattendance where user_id =4554 and event_id=1063

--copy over class attendance
update classattendance
set classattendance.user_id = u1.id
from [user] as u1
inner join [user] as u2 on u1.email = u2.email and u1.id != u2.id and u1.activationkey = '00000000-0000-0000-0000-000000000000' and u2.activationkey != '00000000-0000-0000-0000-000000000000'
inner join classattendance as ca on ca.user_id = u2.id

--delete duplicate event roll
delete eventroll where user_id =4536 and event_id=631
delete eventroll where user_id =4536 and event_id=632
delete eventroll where user_id =4536 and event_id=633
delete eventroll where user_id =4536 and event_id=634
delete eventroll where user_id =4536 and event_id=635
delete eventroll where user_id =4536 and event_id=636

--copy over event roll
update eventroll
set eventroll.user_id = u1.id
from [user] as u1
inner join [user] as u2 on u1.email = u2.email and u1.id != u2.id and u1.activationkey = '00000000-0000-0000-0000-000000000000' and u2.activationkey != '00000000-0000-0000-0000-000000000000'
inner join eventroll as er on er.user_id = u2.id

--delete duplicate block enrolments
delete usersenroledblocks where user_id =4536 and block_id=123

--copy over block enrolments
update usersenroledblocks
set usersenroledblocks.user_id = u1.id
from [user] as u1
inner join [user] as u2 on u1.email = u2.email and u1.id != u2.id and u1.activationkey = '00000000-0000-0000-0000-000000000000' and u2.activationkey != '00000000-0000-0000-0000-000000000000'
inner join usersenroledblocks as er on er.user_id = u2.id

--copy over events
update [event]
set [event].user_id = u1.id
from [user] as u1
inner join [user] as u2 on u1.email = u2.email and u1.id != u2.id and u1.activationkey = '00000000-0000-0000-0000-000000000000' and u2.activationkey != '00000000-0000-0000-0000-000000000000'
inner join [event] as e on e.user_id = u2.id

--delete duplicate users
delete [user]
where id in (
select u2.id
from [user] as u1
inner join [user] as u2 on u1.email = u2.email and u1.id != u2.id and u1.activationkey = '00000000-0000-0000-0000-000000000000' and u2.activationkey != '00000000-0000-0000-0000-000000000000'
)






-----------------

update pass
set pass.user_id = u1.id
from [user] as u1
inner join [user] as u2 on u1.email = u2.email and u1.id != u2.id and u1.id > u2.id
inner join pass as p on p.user_id = u2.id
GO

--copy over class attendance
update classattendance
set classattendance.user_id = u1.id
from [user] as u1
inner join [user] as u2 on u1.email = u2.email and u1.id != u2.id and u1.id > u2.id
inner join classattendance as ca on ca.user_id = u2.id



delete eventroll where user_id =2192 and event_id=259
delete eventroll where user_id =2192 and event_id=260
delete eventroll where user_id =2192 and event_id=261
delete eventroll where user_id =2192 and event_id=262
delete eventroll where user_id =2192 and event_id=263
delete eventroll where user_id =2192 and event_id=264
delete eventroll where user_id =2192 and event_id=514
delete eventroll where user_id =2192 and event_id=515
delete eventroll where user_id =2192 and event_id=516
delete eventroll where user_id =2192 and event_id=517
delete eventroll where user_id =2192 and event_id=518
delete eventroll where user_id =2192 and event_id=519
delete eventroll where user_id =2192 and event_id=538
delete eventroll where user_id =2192 and event_id=539
delete eventroll where user_id =2192 and event_id=540
delete eventroll where user_id =2192 and event_id=541
delete eventroll where user_id =2192 and event_id=542
delete eventroll where user_id =2192 and event_id=543
delete eventroll where user_id =2192 and event_id=543
delete eventroll where user_id =2192 and event_id between 568 and 573

--copy over event roll
update eventroll
set eventroll.user_id = u1.id
from [user] as u1
inner join [user] as u2 on u1.email = u2.email and u1.id != u2.id and u1.id > u2.id
inner join eventroll as er on er.user_id = u2.id


--delete duplicate block enrolments
delete usersenroledblocks where user_id =2192 and block_id=44
delete usersenroledblocks where user_id =2192 and block_id=104
delete usersenroledblocks where user_id =2192 and block_id=108
delete usersenroledblocks where user_id =2192 and block_id=113

--copy over block enrolments
update usersenroledblocks
set usersenroledblocks.user_id = u1.id
from [user] as u1
inner join [user] as u2 on u1.email = u2.email and u1.id != u2.id and u1.id > u2.id
inner join usersenroledblocks as er on er.user_id = u2.id



--copy over events
update [event]
set [event].user_id = u1.id
from [user] as u1
inner join [user] as u2 on u1.email = u2.email and u1.id != u2.id and u1.id > u2.id
inner join [event] as e on e.user_id = u2.id


--delete duplicate users
delete [user]
where id in (
select u2.id
from [user] as u1
inner join [user] as u2 on u1.email = u2.email and u1.id != u2.id and u1.id > u2.id
)
