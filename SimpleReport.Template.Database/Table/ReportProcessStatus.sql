CREATE TABLE [dbo].[ReportProcessStatus]
(
	ReportProcessStatusId INT NOT NULL PRIMARY KEY identity,
	Name varchar(255) not null,	
	CreateAt datetime not null default getdate(),
	UpdateAt datetime null,
	LastUserName varchar(255) not null,
	Enabled bit default 1
)
