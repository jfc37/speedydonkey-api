---------------UNRELEASED------------------

CREATE TABLE [dbo].[TeacherRate](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedDateTime] [datetime] NULL,
	[LastUpdatedDateTime] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[SoloRate] [decimal](19, 5) NULL,
	[PartnerRate] [decimal](19, 5) NULL,
	PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
GO

--------------------------------------------

ALTER TABLE [Teacher]
ADD [Rate_id] [int] NULL
GO


ALTER TABLE [dbo].[Teacher]  WITH CHECK ADD  CONSTRAINT [FK499639A8D04813DB] FOREIGN KEY([Rate_id])
REFERENCES [dbo].[TeacherRate] ([Id])
GO

--------------------------------------------

ALTER TABLE [TeacherRate]
ADD [Temp_Teacher_Id] [int] NULL
GO

insert into TeacherRate
(CreatedDateTime, IsDeleted, SoloRate, PartnerRate, Temp_Teacher_Id)
select GETDATE(), 0, 30, 40, id from Teacher
GO

UPDATE
    t
SET
    t.rate_id = tr.id
FROM
    Teacher AS t
    INNER JOIN TeacherRate AS tr
        ON t.id = tr.temp_teacher_id
GO
		
ALTER TABLE [TeacherRate]
DROP COLUMN [Temp_Teacher_Id] 
GO
---------------------------

ALTER TABLE [StandAloneEvent]
ADD [TeacherRate] [decimal](19, 5) NULL
GO
------------------------------