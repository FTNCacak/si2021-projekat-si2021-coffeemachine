﻿using System;
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
    public partial class PrikazDobavljaca : Form
    {
        public Magacin magacin { get; set; }
        public int nacinSortiranja { get; set; }
        private readonly BusinessRepository br = new BusinessRepository();
        public PrikazDobavljaca(Magacin magacin)
        {
            this.magacin = magacin;
            InitializeComponent();
        }



        private void PrikazDobavljaca_Load(object sender, EventArgs e)
        {
            cbNacinSortiranja.SelectedIndex = 0;
            dataGridView1.Columns.Add("ID_Dobavljaca", "ID dobavljača");
            dataGridView1.Columns.Add("Naziv", "Naziv dobavljača");
            dataGridView1.Columns.Add("Adresa", "Adresa dobavljača");
            dataGridView1.Rows.Add(magacin.ListaDobavljaca.Count);
            for (int i = 0; i < magacin.ListaDobavljaca.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = magacin.ListaDobavljaca[i].ID_Dobavljaca;
                dataGridView1.Rows[i].Cells[1].Value = magacin.ListaDobavljaca[i].Naziv;
                dataGridView1.Rows[i].Cells[2].Value = magacin.ListaDobavljaca[i].Adresa;
            }
        }

        private void Sort()
        {
            for (int i = 0; i < magacin.ListaDobavljaca.Count - 1; i++)
            {
                for (int j = i; j < magacin.ListaDobavljaca.Count; j++)
                {
                    if (nacinSortiranja == 0 && magacin.ListaDobavljaca[i].Naziv.CompareTo(magacin.ListaDobavljaca[j].Naziv) > 0)
                    {
                        Dobavljac pom = magacin.ListaDobavljaca[i];
                        magacin.ListaDobavljaca[i] = magacin.ListaDobavljaca[j];
                        magacin.ListaDobavljaca[j] = pom;
                    }
                    else if (nacinSortiranja == 1 && magacin.ListaDobavljaca[i].Naziv.CompareTo(magacin.ListaDobavljaca[j].Naziv) < 0)
                    {
                        Dobavljac pom = magacin.ListaDobavljaca[i];
                        magacin.ListaDobavljaca[i] = magacin.ListaDobavljaca[j];
                        magacin.ListaDobavljaca[j] = pom;
                    }
                    else if (nacinSortiranja == 2 && magacin.ListaDobavljaca[i].ID_Dobavljaca < magacin.ListaDobavljaca[j].ID_Dobavljaca)
                    {
                        Dobavljac pom = magacin.ListaDobavljaca[i];
                        magacin.ListaDobavljaca[i] = magacin.ListaDobavljaca[j];
                        magacin.ListaDobavljaca[j] = pom;
                    }
                    else if (nacinSortiranja == 3 && magacin.ListaDobavljaca[i].ID_Dobavljaca > magacin.ListaDobavljaca[j].ID_Dobavljaca)
                    {
                        Dobavljac pom = magacin.ListaDobavljaca[i];
                        magacin.ListaDobavljaca[i] = magacin.ListaDobavljaca[j];
                        magacin.ListaDobavljaca[j] = pom;
                    }
                }
            }
        }
        private void Prikazi()
        {
            dataGridView1.Rows.Clear();
            //if (magacin.ListaKorisnika.Count > 1)
                dataGridView1.Rows.Add(magacin.ListaDobavljaca.Count - 1);
            for (int i = 0; i < magacin.ListaDobavljaca.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = magacin.ListaDobavljaca[i].ID_Dobavljaca;
                dataGridView1.Rows[i].Cells[1].Value = magacin.ListaDobavljaca[i].Naziv;
                dataGridView1.Rows[i].Cells[2].Value = magacin.ListaDobavljaca[i].Adresa;
            }
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
                br.DeleteDobavljac(id);
                magacin.ListaDobavljaca.RemoveAt(Row.Index);
            }
            Prikazi();
        }

        private void cbNacinSortiranja_SelectedIndexChanged(object sender, EventArgs e)
        {
            nacinSortiranja = cbNacinSortiranja.SelectedIndex;
        }
    }
}
