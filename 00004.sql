
-------------------------------------------------
ALTER TABLE [Block]
ADD [ClassCapacity] [int] NULL DEFAULT(0)
GO

UPDATE [Block] set [ClassCapacity] = 30
GO
-------------------------------------------------

ALTER TABLE [Event]
ADD [ClassCapacity] [int] NULL DEFAULT(0)
GO

UPDATE [Event] set [ClassCapacity] = 30
GO
