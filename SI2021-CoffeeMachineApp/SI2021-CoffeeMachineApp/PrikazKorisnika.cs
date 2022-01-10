using System;
using Data_Layer;
using Data_Layer.Models;
using Business_Layer;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SI2021_CoffeeMachineApp
{
    public partial class PrikazKorisnika : Form
    {
        public Magacin magacin { get; set; }
        public int nacinSortiranja { get; set; }
        private readonly BusinessRepository br = new BusinessRepository();
        public PrikazKorisnika(Magacin magacin)
        {
            this.magacin = magacin;
            InitializeComponent();
        }

        private void PrikazKorisnika_Load(object sender, EventArgs e)
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
                    else if (nacinSortiranja == 4 && magacin.ListaKorisnika[i].ID_Korisnika< magacin.ListaKorisnika[j].ID_Korisnika)
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

        private void button1_Click(object sender, EventArgs e)
        {
            Sort();
            Prikazi();
        }
        private void cbNacinSortiranja_SelectedIndexChanged(object sender, EventArgs e)
        {
            nacinSortiranja = cbNacinSortiranja.SelectedIndex;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (magacin.ListaKorisnika.Count <= 0)
                return;
            bool check = false;
            foreach (DataGridViewRow Row in dataGridView1.SelectedRows)
            {
                int id = Convert.ToInt32(Row.Cells[0].Value.ToString());
                if(!br.DeleteKorisnik(id))
                {
                    check = false;
                    break;
                }
                check = true;
            }
            magacin = br.getData();
            if (check)
                MessageBox.Show("Uspešno obrisani podaci!");
            else
                MessageBox.Show("Podaci nisu obrisani!");
            Prikazi();

        }
    }
}
