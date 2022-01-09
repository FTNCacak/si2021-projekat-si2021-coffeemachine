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
    public partial class PrikazEvidencije : Form
    {
        public Magacin magacin { get; set; }
        public int nacinSortiranja { get; set; }
        private readonly BusinessRepository br = new BusinessRepository();
        public PrikazEvidencije(Magacin magacin)
        {
            this.magacin = magacin;
            InitializeComponent();
        }
        private void PrikazEvidencije_Load(object sender, EventArgs e)
        {
            cbNacinSortiranja.SelectedIndex = 0;
            dataGridView1.Columns.Add("Opis", "Opis");
            dataGridView1.Columns.Add("Napomena", "Napomena");
            dataGridView1.Columns.Add("FK_ID_Narudzbine", "ID narudžbine");
            dataGridView1.Columns.Add("FK_ID_Proizvoda", "Naziv proizvoda");
            dataGridView1.Rows.Add(magacin.ListaEvidencija.Count);
            for (int i = 0; i < magacin.ListaEvidencija.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = magacin.ListaEvidencija[i].Opis;
                dataGridView1.Rows[i].Cells[1].Value = magacin.ListaEvidencija[i].Napomena;
                dataGridView1.Rows[i].Cells[2].Value = magacin.ListaEvidencija[i].FK_Narudzbina.ID_Narudzbine;
                dataGridView1.Rows[i].Cells[3].Value = magacin.ListaEvidencija[i].FK_Proizvod.Naziv;
            }
        }
        private void Sort()
        {
            for (int i = 0; i < magacin.ListaEvidencija.Count - 1; i++)
            {
                for (int j = i; j < magacin.ListaEvidencija.Count; j++)
                {
                    if (nacinSortiranja == 0 && magacin.ListaEvidencija[i].FK_Narudzbina.ID_Narudzbine.CompareTo(magacin.ListaEvidencija[j].FK_Narudzbina.ID_Narudzbine) > 0)
                    {
                        Evidencija pom = magacin.ListaEvidencija[i];
                        magacin.ListaEvidencija[i] = magacin.ListaEvidencija[j];
                        magacin.ListaEvidencija[j] = pom;
                    }
                    else if (nacinSortiranja == 0 && magacin.ListaEvidencija[i].FK_Narudzbina.ID_Narudzbine.CompareTo(magacin.ListaEvidencija[j].FK_Narudzbina.ID_Narudzbine) < 0)
                    {
                        Evidencija pom = magacin.ListaEvidencija[i];
                        magacin.ListaEvidencija[i] = magacin.ListaEvidencija[j];
                        magacin.ListaEvidencija[j] = pom;
                    }
                    else if (nacinSortiranja == 2 && magacin.ListaEvidencija[i].FK_Proizvod.Naziv.CompareTo(magacin.ListaEvidencija[j].FK_Proizvod.Naziv) < 0)
                    {
                        Evidencija pom = magacin.ListaEvidencija[i];
                        magacin.ListaEvidencija[i] = magacin.ListaEvidencija[j];
                        magacin.ListaEvidencija[j] = pom;
                    }
                    else if (nacinSortiranja == 3 && magacin.ListaEvidencija[i].FK_Proizvod.Naziv.CompareTo(magacin.ListaEvidencija[j].FK_Proizvod.Naziv) < 0)
                    {
                        Evidencija pom = magacin.ListaEvidencija[i];
                        magacin.ListaEvidencija[i] = magacin.ListaEvidencija[j];
                        magacin.ListaEvidencija[j] = pom;
                    }
                }
            }
        }
        private void Prikazi()
        {
            dataGridView1.Rows.Clear();
            //if (magacin.ListaKorisnika.Count > 1)
            dataGridView1.Rows.Add(magacin.ListaEvidencija.Count - 1);
            for (int i = 0; i < magacin.ListaEvidencija.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = magacin.ListaEvidencija[i].Opis;
                dataGridView1.Rows[i].Cells[1].Value = magacin.ListaEvidencija[i].Napomena;
                dataGridView1.Rows[i].Cells[2].Value = magacin.ListaEvidencija[i].FK_Narudzbina.ID_Narudzbine;
                dataGridView1.Rows[i].Cells[3].Value = magacin.ListaEvidencija[i].FK_Proizvod.Naziv;
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
                br.DeleteEvidencija(id,id);
                magacin.ListaEvidencija.RemoveAt(Row.Index);
            }
            Prikazi();
        }
    }
}
