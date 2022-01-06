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
    public partial class PrikazProizvodjaca : Form
    {
        public Magacin magacin { get; set; }
        public int nacinSortiranja { get; set; }
        private readonly BusinessRepository br = new BusinessRepository();
        public PrikazProizvodjaca(Magacin magacin)
        {
            this.magacin = magacin;
            InitializeComponent();
        }


        private void PrikazProizvodjaca_Load(object sender, EventArgs e)
        {
            cbNacinSortiranja.SelectedIndex = 0;
            dataGridView1.Columns.Add("ID_Proizvodjaca", "ID proizvođača");
            dataGridView1.Columns.Add("Naziv", "Naziv proizvođača");
            dataGridView1.Columns.Add("Drzava", "Država u kojoj je proizvođač");
            dataGridView1.Columns.Add("Adresa", "Adresa proizvođača");
            dataGridView1.Columns.Add("Opis", "Opis proizvođača");
            dataGridView1.Rows.Add(magacin.ListaProizvodjaca.Count);
            for (int i = 0; i < magacin.ListaProizvodjaca.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = magacin.ListaProizvodjaca[i].ID_Proizvodjaca;
                dataGridView1.Rows[i].Cells[1].Value = magacin.ListaProizvodjaca[i].Naziv;
                dataGridView1.Rows[i].Cells[2].Value = magacin.ListaProizvodjaca[i].Drzava;
                dataGridView1.Rows[i].Cells[3].Value = magacin.ListaProizvodjaca[i].Adresa;
                dataGridView1.Rows[i].Cells[4].Value = magacin.ListaProizvodjaca[i].Opis;
            }
        }
        private void Sort()
        {
            for (int i = 0; i < magacin.ListaProizvoda.Count - 1; i++)
            {
                for (int j = i; j < magacin.ListaProizvoda.Count; j++)
                {
                    if (nacinSortiranja == 0 && magacin.ListaProizvodjaca[i].Naziv.CompareTo(magacin.ListaProizvodjaca[j].Naziv) > 0)
                    {
                        Proizvodjac pom = magacin.ListaProizvodjaca[i];
                        magacin.ListaProizvodjaca[i] = magacin.ListaProizvodjaca[j];
                        magacin.ListaProizvodjaca[j] = pom;
                    }
                    else if (nacinSortiranja == 1 && magacin.ListaProizvodjaca[i].Naziv.CompareTo(magacin.ListaProizvodjaca[j].Naziv) < 0)
                    {
                        Proizvodjac pom = magacin.ListaProizvodjaca[i];
                        magacin.ListaProizvodjaca[i] = magacin.ListaProizvodjaca[j];
                        magacin.ListaProizvodjaca[j] = pom;
                    }
                    else if (nacinSortiranja == 2 && magacin.ListaProizvodjaca[i].ID_Proizvodjaca < magacin.ListaProizvodjaca[j].ID_Proizvodjaca)
                    {
                        Proizvodjac pom = magacin.ListaProizvodjaca[i];
                        magacin.ListaProizvodjaca[i] = magacin.ListaProizvodjaca[j];
                        magacin.ListaProizvodjaca[j] = pom;
                    }
                    else if (nacinSortiranja == 3 && magacin.ListaProizvodjaca[i].ID_Proizvodjaca > magacin.ListaProizvodjaca[j].ID_Proizvodjaca)
                    {
                        Proizvodjac pom = magacin.ListaProizvodjaca[i];
                        magacin.ListaProizvodjaca[i] = magacin.ListaProizvodjaca[j];
                        magacin.ListaProizvodjaca[j] = pom;
                    }
                }
            }
        }
        private void Prikazi()
        {
            dataGridView1.Rows.Clear();
            if (magacin.ListaKorisnika.Count > 1)
                dataGridView1.Rows.Add(magacin.ListaProizvodjaca.Count - 1);
            for (int i = 0; i < magacin.ListaProizvodjaca.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = magacin.ListaProizvodjaca[i].ID_Proizvodjaca;
                dataGridView1.Rows[i].Cells[1].Value = magacin.ListaProizvodjaca[i].Naziv;
                dataGridView1.Rows[i].Cells[2].Value = magacin.ListaProizvodjaca[i].Drzava;
                dataGridView1.Rows[i].Cells[3].Value = magacin.ListaProizvodjaca[i].Adresa;
                dataGridView1.Rows[i].Cells[4].Value = magacin.ListaProizvodjaca[i].Opis;
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
            foreach (DataGridViewRow Row in dataGridView1.SelectedRows)
            {
                int id = Convert.ToInt32(Row.Cells[0].Value.ToString());
                br.DeleteProizvodjac(id);
                magacin.ListaProizvodjaca.RemoveAt(Row.Index);
            }
            Prikazi();
        }
    }
}
