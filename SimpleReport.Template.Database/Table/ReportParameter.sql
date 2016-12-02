CREATE TABLE [dbo].[ReportParameter]
(
	ReportParameterId INT NOT NULL PRIMARY KEY identity,
	ReportId int not null,
	ExternalName varchar(255) not null,
	InternalName varchar(255) not null,
	DataType varchar(255) not null,
	DefaultValue varchar(255) not null,
	CreateAt datetime not null default getdate(),
	UpdateAt datetime null,
	LastUserName varchar(255) not null,
	Enabled bit default 1
)
