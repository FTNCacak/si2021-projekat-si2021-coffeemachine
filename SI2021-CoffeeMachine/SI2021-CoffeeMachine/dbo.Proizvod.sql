CREATE TABLE [dbo].[Proizvod] (
    [ID_Proizvoda]       INT            IDENTITY (1, 1) NOT NULL,
    [Naziv]              NVARCHAR (50)  NOT NULL,
    [Opis]               NVARCHAR (100) NOT NULL,
    [FK_ID_Proizvodjaca] INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([ID_Proizvoda] ASC),
	CONSTRAINT FK_ID_Proizvodjac 
	FOREIGN KEY (FK_ID_Proizvodjaca) REFERENCES dbo.Proizvodjac(ID_Proizvodjaca)
);

