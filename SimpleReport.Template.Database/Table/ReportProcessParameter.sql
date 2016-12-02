CREATE TABLE [dbo].[ReportProcessParameter]
(
	ReportProcessParameterId INT NOT NULL PRIMARY KEY identity,
	ReportProcessId int not null,		
	ParameterName varchar(255) not null,		
	ParameterValue varchar(255) not null,
	CreateAt datetime not null default getdate(),
	UpdateAt datetime null,
	LastUserName varchar(255) not null,
	Enabled bit default 1
)
