---------------PRIVATE LESSONS----------------------

----------------------------------------------------
/**[TeacherAvailability]**/
CREATE TABLE [dbo].[TeacherAvailability](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedDateTime] [datetime] NULL,
	[LastUpdatedDateTime] [datetime] NULL,
	[Teacher_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[TeacherAvailability]  WITH CHECK ADD  CONSTRAINT [FK7016A75D6B7EB39D] FOREIGN KEY([Teacher_id])
REFERENCES [dbo].[Teacher] ([Id])
GO

ALTER TABLE [dbo].[TeacherAvailability] CHECK CONSTRAINT [FK7016A75D6B7EB39D]
GO
--------------------------------------------------
/**[TimeSlot]**/
CREATE TABLE [dbo].[TimeSlot](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Day] [nvarchar](255) NULL,
	[OpeningTime] [varbinary](max) NULL,
	[ClosingTime] [varbinary](max) NULL,
	[CreatedDateTime] [datetime] NULL,
	[LastUpdatedDateTime] [datetime] NULL,
	[TeacherAvailability_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[TimeSlot]  WITH CHECK ADD  CONSTRAINT [FK2FF312B5366393C1] FOREIGN KEY([TeacherAvailability_id])
REFERENCES [dbo].[TeacherAvailability] ([Id])
GO

ALTER TABLE [dbo].[TimeSlot] CHECK CONSTRAINT [FK2FF312B5366393C1]
GO
