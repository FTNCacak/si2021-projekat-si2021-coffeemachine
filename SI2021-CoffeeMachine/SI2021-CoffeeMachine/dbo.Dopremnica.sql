CREATE TABLE [dbo].[Dopremnica]
(
	[ID_Dopremnice] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FK_ID_Proizvoda] INT NOT NULL, 
    [FK_ID_Dobavljaca] INT NOT NULL
)
