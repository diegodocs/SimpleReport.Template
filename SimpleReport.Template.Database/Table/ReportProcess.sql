CREATE TABLE [dbo].[ReportProcess]
(
	ReportProcessId INT NOT NULL PRIMARY KEY identity,
	ReportId int not null,
	ReportProcessStatusId int not null,	
	FileName varchar(255) not null,	
	StoredProcedureName varchar(255) not null,	
	StartAt datetime not null,
	EndAt datetime null,
	CreateAt datetime not null default getdate(),
	UpdateAt datetime null,
	LastUserName varchar(255) not null,
	Enabled bit default 1
)
