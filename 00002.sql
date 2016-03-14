---------------SETTINGS----------------------
CREATE TABLE [dbo].[SettingItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Value] [nvarchar](255) NULL,
	[CreatedDateTime] [datetime] NULL,
	[LastUpdatedDateTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-----------User full name-------------------
ALTER TABLE [User]
ADD FullName [nvarchar](255) NULL
GO

UPDATE [User] set FullName = FirstName + ' ' + Surname
GO


----------------Soft deleting-----------------

ALTER TABLE [Announcement]
ADD IsDeleted [bit] NULL DEFAULT(0)
GO

update [Announcement] set IsDeleted = 0
GO

ALTER TABLE [Block]
ADD IsDeleted [bit] NULL DEFAULT(0)
GO

update [Block] set IsDeleted = 0
GO

ALTER TABLE [Event]
ADD IsDeleted [bit] NULL DEFAULT(0)
GO

update [Event] set IsDeleted = 0
GO

ALTER TABLE [OnlinePayment]
ADD IsDeleted [bit] NULL DEFAULT(0)
GO

update [OnlinePayment] set IsDeleted = 0
GO

ALTER TABLE [Pass]
ADD IsDeleted [bit] NULL DEFAULT(0)
GO

update [Pass] set IsDeleted = 0
GO

ALTER TABLE [PassStatistic]
ADD IsDeleted [bit] NULL DEFAULT(0)
GO

update [PassStatistic] set IsDeleted = 0
GO

ALTER TABLE [PassTemplate]
ADD IsDeleted [bit] NULL DEFAULT(0)
GO

update [PassTemplate] set IsDeleted = 0
GO

ALTER TABLE [ReferenceData]
ADD IsDeleted [bit] NULL DEFAULT(0)
GO

update [ReferenceData] set IsDeleted = 0
GO

ALTER TABLE [Registration]
ADD IsDeleted [bit] NULL DEFAULT(0)
GO

update [Registration] set IsDeleted = 0
GO

ALTER TABLE [Room]
ADD IsDeleted [bit] NULL DEFAULT(0)
GO

update [Room] set IsDeleted = 0
GO

ALTER TABLE [SettingItem]
ADD IsDeleted [bit] NULL DEFAULT(0)
GO

update [SettingItem] set IsDeleted = 0
GO

ALTER TABLE [Teacher]
ADD IsDeleted [bit] NULL DEFAULT(0)
GO

update [Teacher] set IsDeleted = 0
GO

ALTER TABLE [TeacherAvailability]
ADD IsDeleted [bit] NULL DEFAULT(0)
GO

update [TeacherAvailability] set IsDeleted = 0
GO

ALTER TABLE [TimeSlot]
ADD IsDeleted [bit] NULL DEFAULT(0)
GO

update [TimeSlot] set IsDeleted = 0
GO

ALTER TABLE [User]
ADD IsDeleted [bit] NULL DEFAULT(0)
GO

update [User] set IsDeleted = 0
GO
