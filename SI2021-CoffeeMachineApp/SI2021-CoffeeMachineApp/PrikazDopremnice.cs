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
    public partial class PrikazDopremnice : Form
    {
        public Magacin magacin { get; set; }
        public int nacinSortiranja { get; set; }
        private readonly BusinessRepository br = new BusinessRepository();
        public PrikazDopremnice(Magacin magacin)
        {
            this.magacin = magacin;
            InitializeComponent();
        }

        private void PrikazDopremnice_Load(object sender, EventArgs e)
        {
            cbNacinSortiranja.SelectedIndex = 0;
            dataGridView1.Columns.Add("ID_Dopremnice", "ID dopremnice");
            dataGridView1.Columns.Add("FK_ID_Proizvoda", "Naziv proizvoda");
            dataGridView1.Columns.Add("FK_ID_Dobavljaca", "Naziv dobavljača");
            dataGridView1.Rows.Add(magacin.ListaDopremnica.Count);
            for (int i = 0; i < magacin.ListaDopremnica.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = magacin.ListaDopremnica[i].ID_Dopremnice;
                dataGridView1.Rows[i].Cells[1].Value = magacin.ListaDopremnica[i].FK_Proizvod.Naziv;
                dataGridView1.Rows[i].Cells[2].Value = magacin.ListaDopremnica[i].FK_Dobavljac.Naziv;
            }
        }

        private void Sort()
        {
            for (int i = 0; i < magacin.ListaDopremnica.Count - 1; i++)
            {
                for (int j = i; j < magacin.ListaDopremnica.Count; j++)
                {
                    if (nacinSortiranja == 0 && magacin.ListaDopremnica[i].FK_Dobavljac.Naziv.CompareTo(magacin.ListaDopremnica[j].FK_Dobavljac.Naziv) > 0)
                    {
                        Dopremnica pom = magacin.ListaDopremnica[i];
                        magacin.ListaDopremnica[i] = magacin.ListaDopremnica[j];
                        magacin.ListaDopremnica[j] = pom;
                    }
                    else if (nacinSortiranja == 1 && magacin.ListaDopremnica[i].FK_Dobavljac.Naziv.CompareTo(magacin.ListaDopremnica[j].FK_Dobavljac.Naziv) < 0)
                    {
                        Dopremnica pom = magacin.ListaDopremnica[i];
                        magacin.ListaDopremnica[i] = magacin.ListaDopremnica[j];
                        magacin.ListaDopremnica[j] = pom;
                    }
                    else if (nacinSortiranja == 2 && magacin.ListaDopremnica[i].FK_Proizvod.Naziv.CompareTo(magacin.ListaDopremnica[j].FK_Proizvod.Naziv) < 0)
                    {
                        Dopremnica pom = magacin.ListaDopremnica[i];
                        magacin.ListaDopremnica[i] = magacin.ListaDopremnica[j];
                        magacin.ListaDopremnica[j] = pom;
                    }
                    else if (nacinSortiranja == 3 && magacin.ListaDopremnica[i].FK_Proizvod.Naziv.CompareTo(magacin.ListaDopremnica[j].FK_Proizvod.Naziv) < 0)
                    {
                        Dopremnica pom = magacin.ListaDopremnica[i];
                        magacin.ListaDopremnica[i] = magacin.ListaDopremnica[j];
                        magacin.ListaDopremnica[j] = pom;
                    }
                    else if (nacinSortiranja == 4 && magacin.ListaDopremnica[i].ID_Dopremnice < magacin.ListaDopremnica[j].ID_Dopremnice)
                    {
                        Dopremnica pom = magacin.ListaDopremnica[i];
                        magacin.ListaDopremnica[i] = magacin.ListaDopremnica[j];
                        magacin.ListaDopremnica[j] = pom;
                    }
                    else if (nacinSortiranja == 5 && magacin.ListaDopremnica[i].ID_Dopremnice > magacin.ListaDopremnica[j].ID_Dopremnice)
                    {
                        Dopremnica pom = magacin.ListaDopremnica[i];
                        magacin.ListaDopremnica[i] = magacin.ListaDopremnica[j];
                        magacin.ListaDopremnica[j] = pom;
                    }
                }
            }
        }
        private void Prikazi()
        {
            dataGridView1.Rows.Clear();
            //if (magacin.ListaKorisnika.Count > 1)
            dataGridView1.Rows.Add(magacin.ListaDopremnica.Count - 1);
            for (int i = 0; i < magacin.ListaDopremnica.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = magacin.ListaDopremnica[i].ID_Dopremnice;
                dataGridView1.Rows[i].Cells[1].Value = magacin.ListaDopremnica[i].FK_Proizvod.Naziv;
                dataGridView1.Rows[i].Cells[2].Value = magacin.ListaDopremnica[i].FK_Dobavljac.Naziv;
            }
        }

        private void cbNacinSortiranja_SelectedIndexChanged(object sender, EventArgs e)
        {
            nacinSortiranja = cbNacinSortiranja.SelectedIndex;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Sort();
            Prikazi();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Row in dataGridView1.SelectedRows)
            {
                int id = Convert.ToInt32(Row.Cells[0].Value.ToString());
                br.DeleteDopremnica(id);
                magacin.ListaDopremnica.RemoveAt(Row.Index);
            }
            Prikazi();
        }
    }
}
