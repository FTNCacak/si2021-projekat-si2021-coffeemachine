using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Moq;
using Business_Layer;
using Data_Layer.Models;
using Data_Layer.Interfaces;


namespace Unit_test.Tests
{
    [TestClass]
    public class UnitTestMagacinRepository
    {
        private readonly Mock<IMagacinRepository> mockMagacinRepository = new Mock<IMagacinRepository>();
        
        private BusinessRepository businessrepository;
        private readonly Magacin magacin = new Magacin();
        private readonly List<Korisnik> listaKorisnika = new List<Korisnik>();
        private readonly Korisnik korisnik = new Korisnik
        {
            ID_Korisnika=1,
            Username="AAA",
            Password="AAA",
            Email="AAA",
            Ime="AAA",
            Prezime="AAA",
            Telefon="000 000 000",
            Role="Admin"
        };
        private readonly Radnik radnik = new Radnik
        {
            ID_Radnika = 1,
            Ime = "AAA",
            Prezime = "AAA",
            Telefon = "AAA",
            JMBG = "123456789",
            Email = "AAA",
            Username = "Admin",
            Password="AAA"
        };
        private readonly Dobavljac dobavljac = new Dobavljac
        {
            ID_Dobavljaca = 1,
            Naziv = "AAA",
            Adresa = "AAA",
        };

        [TestInitialize]
        public void Initialize()
        {
            listaKorisnika.Add(korisnik);
            listaKorisnika.Add(new Korisnik
            {
                ID_Korisnika = 2,
                Username = "BBB",
                Password = "BBB",
                Email = "BBB",
                Ime = "BBB",
                Prezime = "BBB",
                Telefon = "111 111 111",
                Role = "Admin"
            });
        }
        [TestMethod]
        public void UnitTest1()
        {
            // Arrange

            mockMagacinRepository.Setup(x => x.GetAllData()).Returns(magacin);
            businessrepository = new BusinessRepository(mockMagacinRepository.Object);

            // Act

            var result = businessrepository.getData();

            // Assert

            Assert.AreEqual(result.ListaProizvoda.Count, magacin.ListaProizvoda.Count);
            Assert.AreEqual(result.ListaDobavljaca.Count, magacin.ListaDobavljaca.Count);
            Assert.AreEqual(result.ListaKorisnika.Count, magacin.ListaKorisnika.Count);
            Assert.AreEqual(result.ListaDopremnica.Count, magacin.ListaDopremnica.Count);
            Assert.AreEqual(result.ListaNarudzbina.Count, magacin.ListaNarudzbina.Count);
            Assert.AreEqual(result.ListaProizvodjaca.Count, magacin.ListaProizvodjaca.Count);
            Assert.AreEqual(result.ListaEvidencija.Count, magacin.ListaEvidencija.Count);
            Assert.AreEqual(result.ListaRadnika.Count, magacin.ListaRadnika.Count);
        }
        [TestMethod]
        public void UnitTest2()
        {
            // Arrange
            int broj = 0;
            mockMagacinRepository.Setup(x => x.InsertKorisnik(korisnik)).Returns(broj);
            businessrepository = new BusinessRepository(mockMagacinRepository.Object);

            // Act

            var result = businessrepository.InsertKorisnik(korisnik);

            // Assert

            Assert.AreEqual(result, broj!=0);
            
        }

        [TestMethod]
        public void UnitTest3()
        {
            // Arrange
            int broj = 0;
            mockMagacinRepository.Setup(x => x.InsertRadnik(radnik)).Returns(broj);
            businessrepository = new BusinessRepository(mockMagacinRepository.Object);

            // Act

            var result = businessrepository.InsertRadnik(radnik);

            // Assert

            Assert.AreEqual(result, broj != 0);

        }
        [TestMethod]
        public void UnitTest4()
        {
            // Arrange
            int broj = 0;
            mockMagacinRepository.Setup(x => x.InsertDobavljac(dobavljac)).Returns(broj);
            businessrepository = new BusinessRepository(mockMagacinRepository.Object);

            // Act

            var result = businessrepository.InsertDobavljac(dobavljac);

            // Assert

            Assert.AreEqual(result, broj != 0);

        }

        private readonly Dopremnica dopremnica = new Dopremnica
        {
            ID_Dopremnice = 1,
            FK_Dobavljac = new Dobavljac {
                ID_Dobavljaca = 1,
                Naziv = "AAA",
                Adresa = "AAA",
            },
            FK_Proizvod = new Proizvod {
                ID_Proizvoda = 1,
                Naziv = "AAA",
                Opis = "AAA",
                FK_Proizvodjac = new Proizvodjac
                {
                    ID_Proizvodjaca = 1,
                    Naziv = "AAA",
                    Drzava = "AAA",
                    Adresa = "AAA",
                    Opis = "AAA"
                },
                Slika_Proizvoda="AAA",
                Cena=1
            }
        };
        private readonly Proizvodjac proizvodjac = new Proizvodjac
        {
            ID_Proizvodjaca = 1,
            Naziv = "AAA",
            Drzava = "AAA",
            Adresa = "AAA",
            Opis = "AAA"
        };

        [TestMethod]
        public void UnitTest5()
        {
            // Arrange
            int broj = 0;
            mockMagacinRepository.Setup(x => x.InsertDopremnica(dopremnica)).Returns(broj);
            businessrepository = new BusinessRepository(mockMagacinRepository.Object);

            // Act

            var result = businessrepository.InsertDopremnica(dopremnica);

            // Assert

            Assert.AreEqual(result, broj != 0);

        }
        private readonly Evidencija evidencija = new Evidencija
        {
            Opis = "AAA",
            Napomena = "AAA",
            FK_Narudzbina = new Narudzbina
            {
                ID_Narudzbine = 1,
                Napomena = "AAA",
                Opis = "AAA",
                Datum = new System.DateTime()
            },
        };
        [TestMethod]
        public void UnitTest6()
        {
            // Arrange
            int broj = 0;
            mockMagacinRepository.Setup(x => x.InsertEvidencija(evidencija)).Returns(broj);
            businessrepository = new BusinessRepository(mockMagacinRepository.Object);

            // Act

            var result = businessrepository.InsertEvidencija(evidencija);

            // Assert

            Assert.AreEqual(result, broj != 0);
        }
        private readonly Narudzbina narudzbina = new Narudzbina
        {
                ID_Narudzbine = 1,
                Napomena = "AAA",
                Opis = "AAA",
                Datum = new System.DateTime()
        };
        [TestMethod]
        public void UnitTest7()
        {
            // Arrange
            int broj = 0;
            mockMagacinRepository.Setup(x => x.InsertNarudzbina(narudzbina)).Returns(broj);
            businessrepository = new BusinessRepository(mockMagacinRepository.Object);

            // Act

            var result = businessrepository.InsertNarudzbina(narudzbina);

            // Assert

            Assert.AreEqual(result, broj != 0);

        }
        private readonly Proizvod proizvod = new Proizvod
        {
            ID_Proizvoda = 1,
            Naziv = "AAA",
            Opis = "AAA",
            FK_Proizvodjac = new Proizvodjac
            {
                ID_Proizvodjaca = 1,
                Naziv = "AAA",
                Drzava = "AAA",
                Adresa = "AAA",
                Opis = "AAA"
            }
        };
        [TestMethod]
        public void UnitTest8()
        {
            // Arrange
            int broj = 0;
            mockMagacinRepository.Setup(x => x.InsertProizvod(proizvod)).Returns(broj);
            businessrepository = new BusinessRepository(mockMagacinRepository.Object);

            // Act

            var result = businessrepository.InsertProizvod(proizvod);

            // Assert

            Assert.AreEqual(result, broj != 0);

        }
        [TestMethod]
        public void UnitTest9()
        {
            // Arrange
            int broj = 0;
            mockMagacinRepository.Setup(x => x.InsertProizvodjac(proizvodjac)).Returns(broj);
            businessrepository = new BusinessRepository(mockMagacinRepository.Object);

            // Act

            var result = businessrepository.InsertProizvodjac(proizvodjac);

            // Assert

            Assert.AreEqual(result, broj != 0);
        }
        [TestMethod]
        public void UnitTest10()
        {
            // Arrange
            int broj = 0;
            mockMagacinRepository.Setup(x => x.UpdateDobavljac(1,dobavljac)).Returns(broj);
            businessrepository = new BusinessRepository(mockMagacinRepository.Object);

            // Act

            var result = businessrepository.UpdateDobavljac(1, dobavljac);

            // Assert

            Assert.AreEqual(result, broj != 0);
        }
        [TestMethod]
        public void UnitTest11()
        {
            // Arrange
            int broj = 0;
            mockMagacinRepository.Setup(x => x.UpdateDopremnica(1, dopremnica)).Returns(broj);
            businessrepository = new BusinessRepository(mockMagacinRepository.Object);

            // Act

            var result = businessrepository.UpdateDopremnica(1, dopremnica);

            // Assert

            Assert.AreEqual(result, broj != 0);
        }
        [TestMethod]
        public void UnitTest12()
        {
            // Arrange
            int broj = 0;
            mockMagacinRepository.Setup(x => x.UpdateEvidencija(1,1,evidencija)).Returns(broj);
            businessrepository = new BusinessRepository(mockMagacinRepository.Object);

            // Act

            var result = businessrepository.UpdateEvidencija(1, 1, evidencija);

            // Assert

            Assert.AreEqual(result, broj != 0);
        }
        [TestMethod]
        public void UnitTest13()
        {
            // Arrange
            int broj = 0;
            mockMagacinRepository.Setup(x => x.UpdateKorisnik(1,korisnik)).Returns(broj);
            businessrepository = new BusinessRepository(mockMagacinRepository.Object);

            // Act

            var result = businessrepository.UpdateKorisnik(1, korisnik);

            // Assert

            Assert.AreEqual(result, broj != 0);
        }
        [TestMethod]
        public void UnitTest14()
        {
            // Arrange
            int broj = 0;
            mockMagacinRepository.Setup(x => x.UpdateNarudzbina(1, narudzbina)).Returns(broj);
            businessrepository = new BusinessRepository(mockMagacinRepository.Object);

            // Act

            var result = businessrepository.UpdateNarudzbina(1, narudzbina);

            // Assert

            Assert.AreEqual(result, broj != 0);
        }
        [TestMethod]
        public void UnitTest15()
        {
            // Arrange
            int broj = 0;
            mockMagacinRepository.Setup(x => x.UpdateProizvod(1,proizvod)).Returns(broj);
            businessrepository = new BusinessRepository(mockMagacinRepository.Object);

            // Act

            var result = businessrepository.UpdateProizvod(1, proizvod);

            // Assert

            Assert.AreEqual(result, broj != 0);
        }
        [TestMethod]
        public void UnitTest16()
        {
            // Arrange
            int broj = 0;
            mockMagacinRepository.Setup(x => x.UpdateProizvodjac(1, proizvodjac)).Returns(broj);
            businessrepository = new BusinessRepository(mockMagacinRepository.Object);

            // Act

            var result = businessrepository.UpdateProizvodjac(1, proizvodjac);

            // Assert

            Assert.AreEqual(result, broj != 0);
        }
        [TestMethod]
        public void UnitTest17()
        {
            // Arrange
            int broj = 0;
            mockMagacinRepository.Setup(x => x.UpdateRadnik(1, radnik)).Returns(broj);
            businessrepository = new BusinessRepository(mockMagacinRepository.Object);

            // Act

            var result = businessrepository.UpdateRadnik(1, radnik);

            // Assert

            Assert.AreEqual(result, broj != 0);
        }
        [TestMethod]

        public void UnitTest18()
        {
            // Arrange
            int broj = 0;
            mockMagacinRepository.Setup(x => x.DeleteDobavljac(1)).Returns(broj);
            businessrepository = new BusinessRepository(mockMagacinRepository.Object);

            // Act

            var result = businessrepository.DeleteDobavljac(1);

            // Assert

            Assert.AreEqual(result, broj != 0);
        }
        [TestMethod]
        public void UnitTest19()
        {
            // Arrange
            int broj = 0;
            mockMagacinRepository.Setup(x => x.DeleteDopremnica(1)).Returns(broj);
            businessrepository = new BusinessRepository(mockMagacinRepository.Object);

            // Act

            var result = businessrepository.DeleteDopremnica(1);

            // Assert

            Assert.AreEqual(result, broj != 0);
        }
        [TestMethod]
        public void UnitTest20()
        {
            // Arrange
            int broj = 0;
            mockMagacinRepository.Setup(x => x.DeleteEvidencija(1,1)).Returns(broj);
            businessrepository = new BusinessRepository(mockMagacinRepository.Object);

            // Act

            var result = businessrepository.DeleteEvidencija(1,1);

            // Assert

            Assert.AreEqual(result, broj != 0);
        }
        [TestMethod]
        public void UnitTest21()
        {
            // Arrange
            int broj = 0;
            mockMagacinRepository.Setup(x => x.DeleteKorisnik(1)).Returns(broj);
            businessrepository = new BusinessRepository(mockMagacinRepository.Object);

            // Act

            var result = businessrepository.DeleteKorisnik(1);

            // Assert

            Assert.AreEqual(result, broj != 0);
        }
        [TestMethod]
        public void UnitTest22()
        {
            // Arrange
            int broj = 0;
            mockMagacinRepository.Setup(x => x.DeleteNarudzbina(1)).Returns(broj);
            businessrepository = new BusinessRepository(mockMagacinRepository.Object);

            // Act

            var result = businessrepository.DeleteNarudzbina(1);

            // Assert

            Assert.AreEqual(result, broj != 0);
        }
        [TestMethod]
        public void UnitTest23()
        {
            // Arrange
            int broj = 0;
            mockMagacinRepository.Setup(x => x.DeleteProizvod(1)).Returns(broj);
            businessrepository = new BusinessRepository(mockMagacinRepository.Object);

            // Act

            var result = businessrepository.DeleteProizvod(1);

            // Assert

            Assert.AreEqual(result, broj != 0);
        }
        [TestMethod]
        public void UnitTest24()
        {
            // Arrange
            int broj = 0;
            mockMagacinRepository.Setup(x => x.DeleteProizvodjac(1)).Returns(broj);
            businessrepository = new BusinessRepository(mockMagacinRepository.Object);

            // Act

            var result = businessrepository.DeleteProizvodjac(1);

            // Assert

            Assert.AreEqual(result, broj != 0);
        }
        [TestMethod]
        public void UnitTest25()
        {
            // Arrange
            int broj = 0;
            mockMagacinRepository.Setup(x => x.DeleteRadnik(1)).Returns(broj);
            businessrepository = new BusinessRepository(mockMagacinRepository.Object);

            // Act

            var result = businessrepository.DeleteRadnik(1);

            // Assert

            Assert.AreEqual(result, broj != 0);
        }
    }
}
