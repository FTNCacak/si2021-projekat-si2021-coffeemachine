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
    public partial class PrikazRadnika : Form
    {
        public Magacin magacin { get; set; }
        public int nacinSortiranja { get; set; }
        private readonly BusinessRepository br = new BusinessRepository();
        public PrikazRadnika(Magacin magacin)
        {
            this.magacin = magacin;
            InitializeComponent();
        }
        private void PrikazRadnika_Load(object sender, EventArgs e)
        {
            cbNacinSortiranja.SelectedIndex = 0;
            dataGridView1.Columns.Add("ID_Radnika", "ID radnika");
            dataGridView1.Columns.Add("Ime", "Ime radnika");
            dataGridView1.Columns.Add("Prezime", "Prezime radnika");
            dataGridView1.Columns.Add("Telefon", "Telefon radnika");
            dataGridView1.Columns.Add("JMBG", "JMBG radnika");
            dataGridView1.Columns.Add("Email", "Email radnika");
            dataGridView1.Columns.Add("FK_ID_Rukovodioca", "Ime rukovodioca");
            dataGridView1.Columns.Add("Username", "Username");
            dataGridView1.Columns.Add("Password", "Password");
            if (magacin.ListaRadnika.Count > 1)
                dataGridView1.Rows.Add(magacin.ListaRadnika.Count - 1);
            for (int i = 0; i < magacin.ListaRadnika.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = magacin.ListaRadnika[i].ID_Radnika;
                dataGridView1.Rows[i].Cells[1].Value = magacin.ListaRadnika[i].Ime;
                dataGridView1.Rows[i].Cells[2].Value = magacin.ListaRadnika[i].Prezime;
                dataGridView1.Rows[i].Cells[3].Value = magacin.ListaRadnika[i].Telefon;
                dataGridView1.Rows[i].Cells[4].Value = magacin.ListaRadnika[i].JMBG;
                dataGridView1.Rows[i].Cells[5].Value = magacin.ListaRadnika[i].Email;
                dataGridView1.Rows[i].Cells[6].Value = magacin.ListaRadnika[i].FK_Rukovodilac.Ime;
                dataGridView1.Rows[i].Cells[7].Value = magacin.ListaRadnika[i].Username;
                dataGridView1.Rows[i].Cells[8].Value = magacin.ListaRadnika[i].Password;
            }
        }

        private void Sort()
        {
            for (int i = 0; i < magacin.ListaRadnika.Count - 1; i++)
            {
                for (int j = i; j < magacin.ListaRadnika.Count; j++)
                {
                    if (nacinSortiranja == 0 && magacin.ListaRadnika[i].Ime.CompareTo(magacin.ListaRadnika[j].Ime) > 0)
                    {
                        Radnik pom = magacin.ListaRadnika[i];
                        magacin.ListaRadnika[i] = magacin.ListaRadnika[j];
                        magacin.ListaRadnika[j] = pom;
                    }
                    else if (nacinSortiranja == 1 && magacin.ListaRadnika[i].Ime.CompareTo(magacin.ListaRadnika[j].Ime) < 0)
                    {
                        Radnik pom = magacin.ListaRadnika[i];
                        pom = magacin.ListaRadnika[i];
                        magacin.ListaRadnika[i] = magacin.ListaRadnika[j];
                        magacin.ListaRadnika[j] = pom;
                    }
                    if (nacinSortiranja == 2 && magacin.ListaRadnika[i].Prezime.CompareTo(magacin.ListaRadnika[j].Prezime) > 0)
                    {
                        Radnik pom = magacin.ListaRadnika[i];
                        magacin.ListaRadnika[i] = magacin.ListaRadnika[j];
                        magacin.ListaRadnika[j] = pom;
                    }
                    else if (nacinSortiranja == 3 && magacin.ListaRadnika[i].Prezime.CompareTo(magacin.ListaRadnika[j].Prezime) < 0)
                    {
                        Radnik pom = magacin.ListaRadnika[i];
                        pom = magacin.ListaRadnika[i];
                        magacin.ListaRadnika[i] = magacin.ListaRadnika[j];
                        magacin.ListaRadnika[j] = pom;
                    }
                    else if (nacinSortiranja == 4 && magacin.ListaRadnika[i].ID_Radnika < magacin.ListaRadnika[j].ID_Radnika)
                    {
                        Radnik pom = magacin.ListaRadnika[i];
                        magacin.ListaRadnika[i] = magacin.ListaRadnika[j];
                        magacin.ListaRadnika[j] = pom;
                    }
                    else if (nacinSortiranja == 5 && magacin.ListaRadnika[i].ID_Radnika > magacin.ListaRadnika[j].ID_Radnika)
                    {
                        Radnik pom = magacin.ListaRadnika[i];
                        magacin.ListaRadnika[i] = magacin.ListaRadnika[j];
                        magacin.ListaRadnika[j] = pom;
                    }

                }
            }
        }
        private void Prikazi()
        {
            dataGridView1.Rows.Clear();
            if (magacin.ListaRadnika.Count > 1)
                dataGridView1.Rows.Add(magacin.ListaRadnika.Count - 1);
            for (int i = 0; i < magacin.ListaRadnika.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = magacin.ListaRadnika[i].ID_Radnika;
                dataGridView1.Rows[i].Cells[1].Value = magacin.ListaRadnika[i].Ime;
                dataGridView1.Rows[i].Cells[2].Value = magacin.ListaRadnika[i].Prezime;
                dataGridView1.Rows[i].Cells[3].Value = magacin.ListaRadnika[i].Telefon;
                dataGridView1.Rows[i].Cells[4].Value = magacin.ListaRadnika[i].JMBG;
                dataGridView1.Rows[i].Cells[5].Value = magacin.ListaRadnika[i].Email;
                dataGridView1.Rows[i].Cells[6].Value = magacin.ListaRadnika[i].FK_Rukovodilac.Ime;
                dataGridView1.Rows[i].Cells[7].Value = magacin.ListaRadnika[i].Username;
                dataGridView1.Rows[i].Cells[8].Value = magacin.ListaRadnika[i].Password;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Sort();
            Prikazi();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (magacin.ListaRadnika.Count <= 0)
                return;
            bool check = false;
            foreach (DataGridViewRow Row in dataGridView1.SelectedRows)
            {
                int id = Convert.ToInt32(Row.Cells[0].Value.ToString());
                if(!br.DeleteRadnik(id))
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

        private void cbNacinSortiranja_SelectedIndexChanged(object sender, EventArgs e)
        {
            nacinSortiranja = cbNacinSortiranja.SelectedIndex;
        }
    }
}
