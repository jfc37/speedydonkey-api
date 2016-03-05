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

UPDATE [User] set FullName = FirstName + ' ' + Surname