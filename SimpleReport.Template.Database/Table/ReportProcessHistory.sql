CREATE TABLE [dbo].[ReportProcessHistory]
(
	ReportProcessHistoryId INT NOT NULL PRIMARY KEY identity,
	ReportProcessId int not null,	
	MessageType varchar(255) not null,	
	MessageText varchar(255) not null,		
	CreateAt datetime not null default getdate(),
	UpdateAt datetime null,
	LastUserName varchar(255) not null,
	Enabled bit default 1
)
