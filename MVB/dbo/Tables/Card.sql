﻿CREATE TABLE [dbo].[Card]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CsId] INT NOT NULL UNIQUE, 
    [Name] VARCHAR(200) NOT NULL, 
    [MtgJsonId] VARCHAR(100) NOT NULL UNIQUE, 
    [ScryfallId] VARCHAR(100) NOT NULL UNIQUE, 
    [MtgJsonCode] VARCHAR(10) NOT NULL, 
    
)
