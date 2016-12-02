CREATE TABLE [dbo].[Report]
(
	ReportId INT NOT NULL PRIMARY KEY identity,
	ReportGroupId int not null,
	Name varchar(255) not null,
	StoredProcedure varchar(255) not null,
	CreateAt datetime not null default getdate(),
	UpdateAt datetime null,
	LastUserName varchar(255) not null,
	Enabled bit default 1
)
