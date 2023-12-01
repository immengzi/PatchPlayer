create database MVVMLoginDb
go
use MVVMLoginDb
go
create table [User]
(
	Id UNIQUEIDENTIFIER primary Key default NEWID(),
	Username nvarchar (50) unique not null,
	[Password] nvarchar (100) not null
)
go
insert into [User] values (NEWID(), 'admin', 'admin')
insert into [User] values (NEWID(), 'xcx', '123456')
insert into [User] values (NEWID(), 'lzm', '123456')
go

select *from [User]