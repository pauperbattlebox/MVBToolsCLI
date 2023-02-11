CREATE TABLE [dbo].[Prices]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CsId] INT NOT NULL UNIQUE, 
    [CsPrice] FLOAT NULL, 
    [ScryfallPrice] FLOAT NULL, 
    [CardKingdomPrice] FLOAT NULL
)
