CREATE TABLE [dbo].[Edition]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CardsphereId] INT NOT NULL UNIQUE, 
    [CardsphereName] VARCHAR(100) NOT NULL UNIQUE, 
    [MtgJsonCode] VARCHAR(10) NULL 
)
