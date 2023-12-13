create database PatchPlayerDB
go
use PatchPlayerDB
go
create table [User]
(
	Id UNIQUEIDENTIFIER primary Key default NEWID(),
	Username nvarchar (50) unique not null,
	[Password] nvarchar (100) not null
)
go
insert into [User] values (NEWID(), 'admin', 'admin')
go

CREATE TABLE [LoveSongs] (
    UserId uniqueidentifier NOT NULL,
    SongId int NOT NULL,
    SongUrl nvarchar(255) NOT NULL,
    SongName nvarchar(100) NOT NULL,
    ArtistName nvarchar(50) NOT NULL,
    AlbumName nvarchar(50) NOT NULL,
    SongLabel nvarchar(50) NULL,
    PRIMARY KEY (UserId, SongId)
);


select *from [User]
select *from [LoveSongs]