CREATE TABLE [dbo].[Proizvod]
(
	[ID_Proizvoda] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Naziv] NVARCHAR(50) NOT NULL, 
    [Opis] NVARCHAR(100) NOT NULL, 
    [FK_ID_Proizvodjaca] INT NOT NULL
)
