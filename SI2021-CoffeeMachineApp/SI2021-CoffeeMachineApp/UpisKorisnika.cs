using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Data_Layer;
using Data_Layer.Models;
using Business_Layer;

namespace SI2021_CoffeeMachineApp
{
    public partial class UpisKorisnika : Form
    {
        public Magacin magacin { get; set; }
        private readonly BusinessRepository br = new BusinessRepository();
        private int nacinSortiranja { get; set; }
        public UpisKorisnika(Magacin magacin)
        {
            this.magacin = magacin;
            InitializeComponent();
        }

        private void UpisKorisnika_Load(object sender, EventArgs e)
        {
            cbNacinSortiranja.SelectedIndex = 0;
            dataGridView1.Columns.Add("ID_Korisnika", "ID korisnika");
            dataGridView1.Columns.Add("Username", "Korisničko ime");
            dataGridView1.Columns.Add("Password", "Šifra");
            dataGridView1.Columns.Add("Email", "Email");
            dataGridView1.Columns.Add("Ime", "Prezime korisnika");
            dataGridView1.Columns.Add("Prezime", "Prezime korisnika");
            dataGridView1.Columns.Add("Telefon", "Telefon korisnika");
            dataGridView1.Columns.Add("Role", "Uloga korisnika");
            if (magacin.ListaKorisnika.Count > 1)
                dataGridView1.Rows.Add(magacin.ListaKorisnika.Count - 1);
            for (int i = 0; i < magacin.ListaKorisnika.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = magacin.ListaKorisnika[i].ID_Korisnika;
                dataGridView1.Rows[i].Cells[1].Value = magacin.ListaKorisnika[i].Username;
                dataGridView1.Rows[i].Cells[2].Value = magacin.ListaKorisnika[i].Password;
                dataGridView1.Rows[i].Cells[3].Value = magacin.ListaKorisnika[i].Email;
                dataGridView1.Rows[i].Cells[4].Value = magacin.ListaKorisnika[i].Ime;
                dataGridView1.Rows[i].Cells[5].Value = magacin.ListaKorisnika[i].Prezime;
                dataGridView1.Rows[i].Cells[6].Value = magacin.ListaKorisnika[i].Telefon;
                dataGridView1.Rows[i].Cells[7].Value = magacin.ListaKorisnika[i].Role;
            }
        }
        private void Sort()
        {
            for (int i = 0; i < magacin.ListaKorisnika.Count - 1; i++)
            {
                for (int j = i; j < magacin.ListaKorisnika.Count; j++)
                {
                    if (nacinSortiranja == 0 && magacin.ListaKorisnika[i].Ime.CompareTo(magacin.ListaKorisnika[j].Ime) > 0)
                    {
                        Korisnik pom = magacin.ListaKorisnika[i];
                        magacin.ListaKorisnika[i] = magacin.ListaKorisnika[j];
                        magacin.ListaKorisnika[j] = pom;
                    }
                    else if (nacinSortiranja == 1 && magacin.ListaKorisnika[i].Ime.CompareTo(magacin.ListaKorisnika[j].Ime) < 0)
                    {
                        Korisnik pom = magacin.ListaKorisnika[i];
                        pom = magacin.ListaKorisnika[i];
                        magacin.ListaKorisnika[i] = magacin.ListaKorisnika[j];
                        magacin.ListaKorisnika[j] = pom;
                    }
                    if (nacinSortiranja == 2 && magacin.ListaKorisnika[i].Prezime.CompareTo(magacin.ListaKorisnika[j].Prezime) > 0)
                    {
                        Korisnik pom = magacin.ListaKorisnika[i];
                        magacin.ListaKorisnika[i] = magacin.ListaKorisnika[j];
                        magacin.ListaKorisnika[j] = pom;
                    }
                    else if (nacinSortiranja == 3 && magacin.ListaKorisnika[i].Prezime.CompareTo(magacin.ListaKorisnika[j].Prezime) < 0)
                    {
                        Korisnik pom = magacin.ListaKorisnika[i];
                        pom = magacin.ListaKorisnika[i];
                        magacin.ListaKorisnika[i] = magacin.ListaKorisnika[j];
                        magacin.ListaKorisnika[j] = pom;
                    }
                    else if (nacinSortiranja == 4 && magacin.ListaKorisnika[i].ID_Korisnika < magacin.ListaKorisnika[j].ID_Korisnika)
                    {
                        Korisnik pom = magacin.ListaKorisnika[i];
                        magacin.ListaKorisnika[i] = magacin.ListaKorisnika[j];
                        magacin.ListaKorisnika[j] = pom;
                    }
                    else if (nacinSortiranja == 5 && magacin.ListaKorisnika[i].ID_Korisnika > magacin.ListaKorisnika[j].ID_Korisnika)
                    {
                        Korisnik pom = magacin.ListaKorisnika[i];
                        magacin.ListaKorisnika[i] = magacin.ListaKorisnika[j];
                        magacin.ListaKorisnika[j] = pom;
                    }
                    else if (nacinSortiranja == 6 && magacin.ListaKorisnika[i].Role.CompareTo(magacin.ListaKorisnika[j].Role) > 0)
                    {
                        Korisnik pom = magacin.ListaKorisnika[i];
                        magacin.ListaKorisnika[i] = magacin.ListaKorisnika[j];
                        magacin.ListaKorisnika[j] = pom;
                    }

                }
            }
        }
        private void Prikazi()
        {
            dataGridView1.Rows.Clear();
            if (magacin.ListaKorisnika.Count > 1)
                dataGridView1.Rows.Add(magacin.ListaKorisnika.Count - 1);
            for (int i = 0; i < magacin.ListaKorisnika.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = magacin.ListaKorisnika[i].ID_Korisnika;
                dataGridView1.Rows[i].Cells[1].Value = magacin.ListaKorisnika[i].Username;
                dataGridView1.Rows[i].Cells[2].Value = magacin.ListaKorisnika[i].Password;
                dataGridView1.Rows[i].Cells[3].Value = magacin.ListaKorisnika[i].Email;
                dataGridView1.Rows[i].Cells[4].Value = magacin.ListaKorisnika[i].Ime;
                dataGridView1.Rows[i].Cells[5].Value = magacin.ListaKorisnika[i].Prezime;
                dataGridView1.Rows[i].Cells[6].Value = magacin.ListaKorisnika[i].Telefon;
                dataGridView1.Rows[i].Cells[7].Value = magacin.ListaKorisnika[i].Role;
            }
        }

        private void cbNacinSortiranja_SelectedIndexChanged(object sender, EventArgs e)
        {
            nacinSortiranja = cbNacinSortiranja.SelectedIndex;
        }

        private void btnSortiraj_Click(object sender, EventArgs e)
        {
            Sort();
            Prikazi();
        }

        private void btnUpisi_Click(object sender, EventArgs e)
        {
            try
            {
                if (username.Text != "" && password.Text != "" && email.Text != "" && ime.Text != "" && prezime.Text != "" && telefon.Text != "" && role.SelectedIndex != -1)
                {
                    Korisnik k = new Korisnik() { Username = username.Text, Password = password.Text, Email = email.Text, Ime=ime.Text, Prezime = prezime.Text, Telefon = telefon.Text, Role = role.SelectedItem.ToString() };
                    if (br.InsertKorisnik(k))
                    {
                        MessageBox.Show("Uspešno ste uneli korisnika.");
                        username.Text = "";
                        password.Text = "";
                        email.Text = "";
                        ime.Text = "";
                        prezime.Text = "";
                        telefon.Text = "";
                        role.SelectedIndex = -1;
                        magacin = br.getData();
                        Prikazi();
                    }
                    else
                        MessageBox.Show("Korisnik nije unet.");
                }
                else
                {
                    MessageBox.Show("Morate popuniti sve podatke na ispravan način kako bi uneli korisnika!");
                }
            }
            catch { MessageBox.Show("Podaci nisu ispravno uneti!"); }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (username.Text != "" && password.Text != "" && email.Text != "" && ime.Text != "" && prezime.Text != "" && telefon.Text != "" && role.SelectedIndex != -1)
                {
                    if (dataGridView1.SelectedRows.Count != 0)
                    {
                        Korisnik k = new Korisnik() { Username = username.Text, Password = password.Text, Email = email.Text, Ime = ime.Text, Prezime = prezime.Text, Telefon = telefon.Text, Role = role.SelectedItem.ToString() };
                        bool check = false;
                        foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                        {
                            int ID = Convert.ToInt32(row.Cells[0].Value.ToString());
                            if (br.UpdateKorisnik(ID, k))
                            {
                                check = true;
                                username.Text = "";
                                password.Text = "";
                                email.Text = "";
                                ime.Text = "";
                                prezime.Text = "";
                                telefon.Text = "";
                                role.SelectedIndex = -1;
                                magacin = br.getData();
                                Prikazi();
                            }
                            else
                            {
                                check = false;
                                break;
                            }
                        }
                        if (check)
                            MessageBox.Show("Uspešno ste ažurirali korisnike.");
                        else
                            MessageBox.Show("Korisnici nisu ažurirani.");
                    }
                    else
                        MessageBox.Show("Morate odabrati korisnike koje treba ažurirati kako bi ažurirali korisnike!");

                }
                else
                {
                    MessageBox.Show("Morate popuniti sve podatke na ispravan način kako bi ažurirali korisnike!");
                }
            }
            catch { MessageBox.Show("Podaci nisu ispravno uneti!"); }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (magacin.ListaKorisnika.Count <= 0)
                return;
            username.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            password.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            email.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            ime.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            prezime.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            telefon.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            role.SelectedIndex = role.FindString(dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString());
        }
    }
}
