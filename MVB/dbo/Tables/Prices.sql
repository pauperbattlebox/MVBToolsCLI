CREATE TABLE [dbo].[Prices]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CardsphereId] INT NOT NULL UNIQUE, 
    [CardspherePrice] FLOAT NULL, 
    [ScryfallPrice] FLOAT NULL, 
    [CardKingdomPrice] FLOAT NULL
)
