using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Data_Layer.Models;

namespace Data_Layer
{
    public class MagacinRepository
    {
        private string ConnString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SI2021-CoffeeMachineDataBase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public Magacin GetAllData()
        {
            Magacin magacin = new Magacin();
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                string query = "SELECT * FROM Proizvodjac";
                SqlCommand comm = new SqlCommand(query, conn);
                SqlDataReader reader = comm.ExecuteReader();
                while(reader.Read())
                {
                    Proizvodjac pr = new Proizvodjac() { ID_Proizvodjaca = reader.GetInt32(0), Naziv = reader.GetString(1), Drzava = reader.GetString(2), Adresa = reader.GetString(3), Opis = reader.GetString(4)};
                    magacin.ListaProizvodjaca.Add(pr);
                }
                query = "SELECT * FROM Proizvod";
                comm = new SqlCommand(query, conn);
                reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    Proizvod pr = new Proizvod() { ID_Proizvoda = reader.GetInt32(0), Naziv = reader.GetString(1), Opis = reader.GetString(2) };
                    int IDProizvodjaca = reader.GetInt32(3);
                    foreach(Proizvodjac proizvodjac in magacin.ListaProizvodjaca)
                    {
                        if (proizvodjac.ID_Proizvodjaca == IDProizvodjaca)
                        {
                            pr.FK_Proizvodjac = proizvodjac;
                            break;
                        }
                    }
                    magacin.ListaProizvoda.Add(pr);
                }
                query = "SELECT * FROM Dobavljac";
                comm = new SqlCommand(query, conn);
                reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    Dobavljac db = new Dobavljac() { ID_Dobavljaca = reader.GetInt32(0), Naziv = reader.GetString(1), Adresa = reader.GetString(2) };
                    magacin.ListaDobavljaca.Add(db);
                }
                query = "SELECT * FROM Dopremnica";
                comm = new SqlCommand(query, conn);
                reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    Dopremnica dp = new Dopremnica() { ID_Dopremnice = reader.GetInt32(0) };
                    int IDProizvoda = reader.GetInt32(1);
                    int IDDobavljaca = reader.GetInt32(2);
                    foreach (Proizvod proizvod in magacin.ListaProizvoda)
                    {
                        if (proizvod.ID_Proizvoda == IDProizvoda)
                        {
                            dp.FK_Proizvod = proizvod;
                            break;
                        }
                    }
                    foreach (Dobavljac dobavljac in magacin.ListaDobavljaca)
                    {
                        if (dobavljac.ID_Dobavljaca == IDDobavljaca)
                        {
                            dp.FK_Dobavljac = dobavljac;
                            break;
                        }
                    }
                    magacin.ListaDopremnica.Add(dp);
                }
                query = "SELECT * FROM Narudzbina";
                comm = new SqlCommand(query, conn);
                reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    Narudzbina nr = new Narudzbina() { ID_Narudzbine = reader.GetInt32(0), Napomena = reader.GetString(1), Opis = reader.GetString(2), Datum = reader.GetDateTime(3)};
                    magacin.ListaNarudzbina.Add(nr);
                }
                query = "SELECT * FROM Evidencija";
                comm = new SqlCommand(query, conn);
                reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    Evidencija ev = new Evidencija() { Opis = reader.GetString(0), Napomena = reader.GetString(1) };
                    int IDNarudzbine = reader.GetInt32(2);
                    int IDProizvoda = reader.GetInt32(3);
                    foreach (Narudzbina narudzbina in magacin.ListaNarudzbina)
                    {
                        if (narudzbina.ID_Narudzbine == IDNarudzbine)
                        {
                            ev.FK_Narudzbina = narudzbina;
                            break;
                        }
                    }
                    foreach (Proizvod proizvod in magacin.ListaProizvoda)
                    {
                        if (proizvod.ID_Proizvoda == IDProizvoda)
                        {
                            ev.FK_Proizvod = proizvod;
                            break;
                        }
                    }
                    magacin.ListaEvidencija.Add(ev);
                }
                return magacin;
            }
        }

        public int InsertProizvod(Proizvod p)
        {
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                string query = "INSERT INTO Proizvod VALUES(@naziv, @opis, @id_proizvodjaca)";
                SqlCommand comm = new SqlCommand(query, conn);
                comm.Parameters.AddWithValue("@naziv", p.Naziv);
                comm.Parameters.AddWithValue("@opis", p.Opis);
                comm.Parameters.AddWithValue("@id_proizvodjaca", p.FK_Proizvodjac.ID_Proizvodjaca);
                return comm.ExecuteNonQuery();                
            }
        }
        public int InsertProizvodjac(Proizvodjac p)
        {
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                string query = "INSERT INTO Proizvodjac VALUES(@naziv, @drzava, @adresa, @opis)";
                SqlCommand comm = new SqlCommand(query, conn);
                comm.Parameters.AddWithValue("@naziv", p.Naziv);
                comm.Parameters.AddWithValue("@drzava", p.Drzava);
                comm.Parameters.AddWithValue("@adresa", p.Adresa);
                comm.Parameters.AddWithValue("@opis", p.Opis);
                return comm.ExecuteNonQuery();
            }
        }
        public int InsertDobavljac(Dobavljac d)
        {
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                string query = "INSERT INTO Dobavljac VALUES(@naziv, @adresa)";
                SqlCommand comm = new SqlCommand(query, conn);
                comm.Parameters.AddWithValue("@naziv", d.Naziv);
                comm.Parameters.AddWithValue("@adresa", d.Adresa);
                return comm.ExecuteNonQuery();
            }
        }
        public int InsertDopremnica(Dopremnica d)
        {
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                string query = "INSERT INTO Dopremnica VALUES(@id_proizvoda, @id_dobavljaca)";
                SqlCommand comm = new SqlCommand(query, conn);
                comm.Parameters.AddWithValue("@id_proizvoda", d.FK_Proizvod.ID_Proizvoda);
                comm.Parameters.AddWithValue("@id_dobavljaca", d.FK_Dobavljac.ID_Dobavljaca);
                return comm.ExecuteNonQuery();
            }
        }
        public int InsertNarudzbina(Narudzbina n)
        {
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                string query = "INSERT INTO Narudzbina VALUES(@napomena, @opis, @datum)";
                SqlCommand comm = new SqlCommand(query, conn);
                comm.Parameters.AddWithValue("@napomena", n.Napomena);
                comm.Parameters.AddWithValue("@opis", n.Opis);
                comm.Parameters.AddWithValue("@datum", n.Datum);
                return comm.ExecuteNonQuery();
            }
        }
        public int InsertEvidencija(Evidencija e)
        {
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                string query = "INSERT INTO Evidencija VALUES(@opis, @napomena, @id_narudzbine, @id_proizvoda)";
                SqlCommand comm = new SqlCommand(query, conn);
                comm.Parameters.AddWithValue("@opis", e.Opis);
                comm.Parameters.AddWithValue("@napomena", e.Napomena);
                comm.Parameters.AddWithValue("@id_narudzbine", e.FK_Narudzbina.ID_Narudzbine);
                comm.Parameters.AddWithValue("@id_proizvoda", e.FK_Proizvod.ID_Proizvoda);
                return comm.ExecuteNonQuery();
            }
        }
        public int InsertRadnik(Radnik r)
        {
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                string query = "INSERT INTO Radnik VALUES(@ime, @prezime, @telefon, @jmbg, @email, @id_rukovodioca, @username, @password)";
                SqlCommand comm = new SqlCommand(query, conn);
                comm.Parameters.AddWithValue("@ime", r.Ime);
                comm.Parameters.AddWithValue("@prezime", r.Prezime);
                comm.Parameters.AddWithValue("@telefon", r.Telefon);
                comm.Parameters.AddWithValue("@jmbg", r.JMBG);
                comm.Parameters.AddWithValue("@email", r.Email);
                comm.Parameters.AddWithValue("@id_rukovodioca", r.FK_Rukovodilac.ID_Radnika);
                comm.Parameters.AddWithValue("@username", r.Username);
                comm.Parameters.AddWithValue("@password", r.Password);
                return comm.ExecuteNonQuery();
            }
        }
        public int InsertKorisnik(Korisnik k)
        {
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                string query = "INSERT INTO Korisnik VALUES(@username, @password, @email, @ime, @prezime, @telefon, @role)";
                SqlCommand comm = new SqlCommand(query, conn);
                comm.Parameters.AddWithValue("@username", k.Username);
                comm.Parameters.AddWithValue("@password", k.Password);
                comm.Parameters.AddWithValue("@email", k.Email);
                comm.Parameters.AddWithValue("@ime", k.Ime);
                comm.Parameters.AddWithValue("@prezime", k.Prezime);
                comm.Parameters.AddWithValue("@telefon", k.Telefon);
                comm.Parameters.AddWithValue("@role", k.Role);
                return comm.ExecuteNonQuery();
            }
        }


        public int DeleteProizvod(int ID_Proizvoda)
        {
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                string query = string.Format("DELETE FROM Proizvod WHERE ID_Proizvoda = {0}", ID_Proizvoda);
                SqlCommand comm = new SqlCommand(query, conn);
                return comm.ExecuteNonQuery();
            }
        }
        public int DeleteProizvodjac(int ID_Proizvodjaca)
        {
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                string query = string.Format("DELETE FROM Proizvodjac WHERE ID_Proizvodjaca = {0}", ID_Proizvodjaca);
                SqlCommand comm = new SqlCommand(query, conn);
                return comm.ExecuteNonQuery();
            }
        }
        public int DeleteDobavljac(int ID_Dobavljaca)
        {
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                string query = string.Format("DELETE FROM Dobavljac WHERE ID_Dobavljaca = {0}", ID_Dobavljaca);
                SqlCommand comm = new SqlCommand(query, conn);
                return comm.ExecuteNonQuery();
            }
        }
        public int DeleteDopremnica(int ID_Dopremnice)
        {
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                string query = string.Format("DELETE FROM Dopremnica WHERE ID_Dopremnice = {0}", ID_Dopremnice);
                SqlCommand comm = new SqlCommand(query, conn);
                return comm.ExecuteNonQuery();
            }
        }
        public int DeleteNarudzbina(int ID_Narudzbine)
        {
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                string query = string.Format("DELETE FROM Narudzbina WHERE ID_Narudzbine = {0}", ID_Narudzbine);
                SqlCommand comm = new SqlCommand(query, conn);
                return comm.ExecuteNonQuery();
            }
        }
        public int DeleteEvidencija(int ID_Narudzbine, int ID_Proizvoda)
        {
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                string query = string.Format("DELETE FROM Evidencija WHERE ID_Narudzbine = {0} AND ID_Proizvoda = {1}", ID_Narudzbine, ID_Proizvoda);
                SqlCommand comm = new SqlCommand(query, conn);
                return comm.ExecuteNonQuery();
            }
        }
        public int DeleteRadnik(int ID_Radnika)
        {
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                string query = string.Format("DELETE FROM Radnik WHERE ID_Radnika = {0}", ID_Radnika);
                SqlCommand comm = new SqlCommand(query, conn);
                return comm.ExecuteNonQuery();
            }
        }
        public int DeleteKorisnik(int ID_Korisnika)
        {
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                string query = string.Format("DELETE FROM Korisnik WHERE ID_Korisnika = {0}", ID_Korisnika);
                SqlCommand comm = new SqlCommand(query, conn);
                return comm.ExecuteNonQuery();
            }
        }


        public int UpdateProizvod(int ID, Proizvod p)
        {
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                string query = "UPDATE Proizvod SET Naziv = @naziv, Opis = @opis, FK_ID_Proizvodjaca = @id_proizvodjaca WHERE ID_Proizvoda = @id";
                SqlCommand comm = new SqlCommand(query, conn);
                comm.Parameters.AddWithValue("@naziv", p.Naziv);
                comm.Parameters.AddWithValue("@opis", p.Opis);
                comm.Parameters.AddWithValue("@id_proizvodjaca", p.FK_Proizvodjac.ID_Proizvodjaca);
                comm.Parameters.AddWithValue("@id", ID);
                return comm.ExecuteNonQuery();
            }
        }
        public int UpdateProizvodjac(int ID, Proizvodjac p)
        {
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                string query = "UPDATE Proizvodjac SET Naziv = @naziv, Drzava = @drzava, Adresa = @adresa, Opis = @opis WHERE ID_Proizvodjaca = @id";
                SqlCommand comm = new SqlCommand(query, conn);
                comm.Parameters.AddWithValue("@naziv", p.Naziv);
                comm.Parameters.AddWithValue("@drzava", p.Drzava);
                comm.Parameters.AddWithValue("@adresa", p.Adresa);
                comm.Parameters.AddWithValue("@opis", p.Opis);
                comm.Parameters.AddWithValue("@id", ID);
                return comm.ExecuteNonQuery();
            }
        }
        public int UpdateDobavljac(int ID, Dobavljac d)
        {
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                string query = "UPDATE Dobavljac SET Naziv = @naziv, Adresa = @adresa WHERE ID_Dobavljaca = @id";
                SqlCommand comm = new SqlCommand(query, conn);
                comm.Parameters.AddWithValue("@naziv", d.Naziv);
                comm.Parameters.AddWithValue("@adresa", d.Adresa);
                comm.Parameters.AddWithValue("@id", ID);
                return comm.ExecuteNonQuery();
            }
        }
        public int UpdateDopremnica(int ID, Dopremnica d)
        {
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                string query = "UPDATE Dopremnica SET FK_ID_Proizvoda = @id_proizvoda, FK_ID_Dobavljaca = @id_dobavljaca WHERE ID_Dopremnice = @id";
                SqlCommand comm = new SqlCommand(query, conn);
                comm.Parameters.AddWithValue("@id_proizvoda", d.FK_Proizvod.ID_Proizvoda);
                comm.Parameters.AddWithValue("@id_dobavljaca", d.FK_Dobavljac.ID_Dobavljaca);
                comm.Parameters.AddWithValue("@id", ID);
                return comm.ExecuteNonQuery();
            }
        }
        public int UpdateNarudzbina(int ID, Narudzbina n)
        {
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                string query = "UPDATE Narudzbina SET Napomena = @napomena, Opis = @opis, Datum = @datum WHERE ID_Narudzbine = @id";
                SqlCommand comm = new SqlCommand(query, conn);
                comm.Parameters.AddWithValue("@napomena", n.Napomena);
                comm.Parameters.AddWithValue("@opis", n.Opis);
                comm.Parameters.AddWithValue("@datum", n.Datum);
                comm.Parameters.AddWithValue("@id", ID);
                return comm.ExecuteNonQuery();
            }
        }
        public int UpdateEvidencija(int ID_1, int ID_2, Evidencija e)
        {
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                string query = "UPDATE Evidencija SET Opis = @opis, Napomena = @napomena WHERE FK_ID_Narudzbine = @id1 AND FK_ID_Proizvoda = @id2";
                SqlCommand comm = new SqlCommand(query, conn);
                comm.Parameters.AddWithValue("@opis", e.Opis);
                comm.Parameters.AddWithValue("@napomena", e.Napomena); 
                comm.Parameters.AddWithValue("@id1", ID_1);
                comm.Parameters.AddWithValue("@id2", ID_2);
                return comm.ExecuteNonQuery();
            }
        }
        public int UpdateRadnik(int ID, Radnik r)
        {
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                string query = "UPDATE Radnik SET Ime = @ime, Prezime = @prezime, Telefon = @telefon, JMBG = @jmbg, Email = @email, FK_ID_Rukovodioca = @id_rukovodioca, Username = @username, Password = @password WHERE ID_Radnika = @id";
                SqlCommand comm = new SqlCommand(query, conn);
                comm.Parameters.AddWithValue("@ime", r.Ime);
                comm.Parameters.AddWithValue("@prezime", r.Prezime);
                comm.Parameters.AddWithValue("@telefon", r.Telefon);
                comm.Parameters.AddWithValue("@jmbg", r.JMBG);
                comm.Parameters.AddWithValue("@email", r.Email);
                comm.Parameters.AddWithValue("@id_rukovodioca", r.FK_Rukovodilac.ID_Radnika);
                comm.Parameters.AddWithValue("@username", r.Username);
                comm.Parameters.AddWithValue("@password", r.Password);
                comm.Parameters.AddWithValue("@id", ID);
                return comm.ExecuteNonQuery();
            }
        }
        public int UpdateKorisnik(int ID, Korisnik k)
        {
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                string query = "UPDATE Korisnik SET Username = @username, Password = @password, Email = @email, Ime = @ime, Prezime = @prezime, Telefon = @telefon, Role = @role WHERE ID_Korisnika = @id";
                SqlCommand comm = new SqlCommand(query, conn);
                comm.Parameters.AddWithValue("@username", k.Username);
                comm.Parameters.AddWithValue("@password", k.Password);
                comm.Parameters.AddWithValue("@email", k.Email);
                comm.Parameters.AddWithValue("@ime", k.Ime);
                comm.Parameters.AddWithValue("@prezime", k.Prezime);
                comm.Parameters.AddWithValue("@telefon", k.Telefon);
                comm.Parameters.AddWithValue("@role", k.Role);
                comm.Parameters.AddWithValue("@id", ID);
                return comm.ExecuteNonQuery();
            }
        }
    }
}
