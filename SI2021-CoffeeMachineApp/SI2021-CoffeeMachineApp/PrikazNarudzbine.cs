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
    public partial class PrikazNarudzbine : Form
    {
        public Magacin magacin { get; set; }
        public int nacinSortiranja { get; set; }
        private readonly BusinessRepository br = new BusinessRepository();
        public PrikazNarudzbine(Magacin magacin)
        {
            this.magacin = magacin;
            InitializeComponent();
        }
        private void PrikazNarudzbine_Load(object sender, EventArgs e)
        {
            cbNacinSortiranja.SelectedIndex = 0;
            dataGridView1.Columns.Add("ID_Narudzbine", "ID narudzbine");
            dataGridView1.Columns.Add("Napomena", "Napomenat");
            dataGridView1.Columns.Add("Opis", "Opis");
            dataGridView1.Columns.Add("Datum", "Datum");
            if (magacin.ListaNarudzbina.Count > 1)
                dataGridView1.Rows.Add(magacin.ListaNarudzbina.Count - 1);
            for (int i = 0; i < magacin.ListaNarudzbina.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = magacin.ListaNarudzbina[i].ID_Narudzbine;
                dataGridView1.Rows[i].Cells[1].Value = magacin.ListaNarudzbina[i].Napomena;
                dataGridView1.Rows[i].Cells[2].Value = magacin.ListaNarudzbina[i].Opis;
                dataGridView1.Rows[i].Cells[3].Value = magacin.ListaNarudzbina[i].Datum;
            }
        }
        private void Sort()
        {
            for (int i = 0; i < magacin.ListaNarudzbina.Count - 1; i++)
            {
                for (int j = i; j < magacin.ListaNarudzbina.Count; j++)
                { 
                    if (nacinSortiranja == 0 && magacin.ListaNarudzbina[i].ID_Narudzbine < magacin.ListaNarudzbina[j].ID_Narudzbine)
                    {
                        Narudzbina pom = magacin.ListaNarudzbina[i];
                        magacin.ListaNarudzbina[i] = magacin.ListaNarudzbina[j];
                        magacin.ListaNarudzbina[j] = pom;
                    }
                    else if (nacinSortiranja == 1 && magacin.ListaNarudzbina[i].ID_Narudzbine > magacin.ListaNarudzbina[j].ID_Narudzbine)
                    {
                        Narudzbina pom = magacin.ListaNarudzbina[i];
                        magacin.ListaNarudzbina[i] = magacin.ListaNarudzbina[j];
                        magacin.ListaNarudzbina[j] = pom;
                    }
                }
            }
        }
        private void Prikazi()
        {
            dataGridView1.Rows.Clear();
            if (magacin.ListaNarudzbina.Count > 1)
                dataGridView1.Rows.Add(magacin.ListaNarudzbina.Count - 1);
            for (int i = 0; i < magacin.ListaNarudzbina.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = magacin.ListaNarudzbina[i].ID_Narudzbine;
                dataGridView1.Rows[i].Cells[1].Value = magacin.ListaNarudzbina[i].Napomena;
                dataGridView1.Rows[i].Cells[2].Value = magacin.ListaNarudzbina[i].Opis;
                dataGridView1.Rows[i].Cells[3].Value = magacin.ListaNarudzbina[i].Datum;
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
            if (magacin.ListaNarudzbina.Count <= 0)
                return;
            bool check = false;
            foreach (DataGridViewRow Row in dataGridView1.SelectedRows)
            {
                int id = Convert.ToInt32(Row.Cells[0].Value.ToString());
                if(!br.DeleteNarudzbina(id))
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
