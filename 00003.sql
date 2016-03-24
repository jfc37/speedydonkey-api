ALTER TABLE [User]
ADD AgreesToTerms [bit] NULL DEFAULT(0)
GO

UPDATE [User] set AgreesToTerms = 0
GO