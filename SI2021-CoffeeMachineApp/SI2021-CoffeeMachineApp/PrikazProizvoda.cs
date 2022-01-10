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
using System.IO;

namespace SI2021_CoffeeMachineApp
{
    public partial class PrikazProizvoda : Form
    {
        public Magacin magacin { get; set; }
        public int nacinSortiranja { get; set; }
        private readonly BusinessRepository br = new BusinessRepository();
        public PrikazProizvoda(Magacin magacin)
        {
            this.magacin = magacin;
            InitializeComponent();
        }

        private void PrikazProizvoda_Load(object sender, EventArgs e)
        {
            cbNacinSortiranja.SelectedIndex = 0;
            DataGridViewImageColumn kolona = new DataGridViewImageColumn();
            kolona.HeaderText = "Slika proizvoda";
            kolona.ImageLayout = DataGridViewImageCellLayout.Zoom;
            kolona.Name = "Slika_Proizvoda";
            dataGridView1.Columns.Insert(0, kolona);
            dataGridView1.Columns.Add("ID_Proizvoda", "ID proizvoda");
            dataGridView1.Columns.Add("Naziv", "Naziv proizvoda");
            dataGridView1.Columns.Add("Cena", "Cena proizvoda");
            dataGridView1.Columns.Add("Opis", "Opis proizvoda");
            dataGridView1.Columns.Add("FK_ID_Proizvodjaca", "Proizvođač");
            if (magacin.ListaProizvoda.Count > 1)
                dataGridView1.Rows.Add(magacin.ListaProizvoda.Count - 1);
            for (int i = 0; i < magacin.ListaProizvoda.Count; i++)
            {
                dataGridView1.Rows[i].Height = 50;
                Bitmap pb = new Bitmap(magacin.ListaProizvoda[i].Slika_Proizvoda);
                Image slika = Image.FromHbitmap(pb.GetHbitmap());
                ((DataGridViewImageCell)dataGridView1.Rows[i].Cells[0]).Value = slika;
                dataGridView1.Rows[i].Cells[1].Value = magacin.ListaProizvoda[i].ID_Proizvoda;
                dataGridView1.Rows[i].Cells[2].Value = magacin.ListaProizvoda[i].Naziv;
                dataGridView1.Rows[i].Cells[3].Value = magacin.ListaProizvoda[i].Cena;
                dataGridView1.Rows[i].Cells[4].Value = magacin.ListaProizvoda[i].Opis;
                dataGridView1.Rows[i].Cells[5].Value = magacin.ListaProizvoda[i].FK_Proizvodjac.Naziv;
                pb.Dispose();
            }
        }
        private void Sort()
        {
            for(int i=0;i<magacin.ListaProizvoda.Count-1;i++)
            {
                for (int j = i; j < magacin.ListaProizvoda.Count; j++)
                {
                    if(nacinSortiranja==0 && magacin.ListaProizvoda[i].Naziv.CompareTo(magacin.ListaProizvoda[j].Naziv)>0)
                    {
                        Proizvod pom = magacin.ListaProizvoda[i];
                        magacin.ListaProizvoda[i]= magacin.ListaProizvoda[j];
                        magacin.ListaProizvoda[j] = pom;
                    }
                    else if (nacinSortiranja == 1 && magacin.ListaProizvoda[i].Naziv.CompareTo(magacin.ListaProizvoda[j].Naziv) < 0)
                    {
                        Proizvod pom = magacin.ListaProizvoda[i];
                        magacin.ListaProizvoda[i] = magacin.ListaProizvoda[j];
                        magacin.ListaProizvoda[j] = pom;
                    }
                    else if (nacinSortiranja == 2 && magacin.ListaProizvoda[i].Cena > magacin.ListaProizvoda[j].Cena)
                    {
                        Proizvod pom = magacin.ListaProizvoda[i];
                        magacin.ListaProizvoda[i] = magacin.ListaProizvoda[j];
                        magacin.ListaProizvoda[j] = pom;
                    }
                    else if (nacinSortiranja == 3 && magacin.ListaProizvoda[i].Cena < magacin.ListaProizvoda[j].Cena)
                    {
                        Proizvod pom = magacin.ListaProizvoda[i];
                        magacin.ListaProizvoda[i] = magacin.ListaProizvoda[j];
                        magacin.ListaProizvoda[j] = pom;
                    }
                    else if (nacinSortiranja == 4 && magacin.ListaProizvoda[i].ID_Proizvoda < magacin.ListaProizvoda[j].ID_Proizvoda)
                    {
                        Proizvod pom = magacin.ListaProizvoda[i];
                        magacin.ListaProizvoda[i] = magacin.ListaProizvoda[j];
                        magacin.ListaProizvoda[j] = pom;
                    }
                    else if (nacinSortiranja == 5 && magacin.ListaProizvoda[i].ID_Proizvoda > magacin.ListaProizvoda[j].ID_Proizvoda)
                    {
                        Proizvod pom = magacin.ListaProizvoda[i];
                        magacin.ListaProizvoda[i] = magacin.ListaProizvoda[j];
                        magacin.ListaProizvoda[j] = pom;
                    }
                    else if (nacinSortiranja == 6 && magacin.ListaProizvoda[i].FK_Proizvodjac.Naziv.CompareTo(magacin.ListaProizvoda[j].FK_Proizvodjac.Naziv) < 0)
                    {
                        Proizvod pom = magacin.ListaProizvoda[i];
                        magacin.ListaProizvoda[i] = magacin.ListaProizvoda[j];
                        magacin.ListaProizvoda[j] = pom;
                    }
                    else if (nacinSortiranja == 7 && magacin.ListaProizvoda[i].FK_Proizvodjac.Naziv.CompareTo(magacin.ListaProizvoda[j].FK_Proizvodjac.Naziv) < 0)
                    {
                        Proizvod pom = magacin.ListaProizvoda[i];
                        magacin.ListaProizvoda[i] = magacin.ListaProizvoda[j];
                        magacin.ListaProizvoda[j] = pom;
                    }

                }
            }
        }
        private void Prikazi()
        {
            dataGridView1.Rows.Clear();
            if (magacin.ListaProizvoda.Count > 1)
                dataGridView1.Rows.Add(magacin.ListaProizvoda.Count - 1);
            for (int i = 0; i < magacin.ListaProizvoda.Count; i++)
            {
                dataGridView1.Rows[i].Height = 50;
                Bitmap pb = new Bitmap(magacin.ListaProizvoda[i].Slika_Proizvoda);
                Image slika = Image.FromHbitmap(pb.GetHbitmap());
                ((DataGridViewImageCell)dataGridView1.Rows[i].Cells[0]).Value = slika;
                dataGridView1.Rows[i].Cells[1].Value = magacin.ListaProizvoda[i].ID_Proizvoda;
                dataGridView1.Rows[i].Cells[2].Value = magacin.ListaProizvoda[i].Naziv;
                dataGridView1.Rows[i].Cells[3].Value = magacin.ListaProizvoda[i].Cena;
                dataGridView1.Rows[i].Cells[4].Value = magacin.ListaProizvoda[i].Opis;
                dataGridView1.Rows[i].Cells[5].Value = magacin.ListaProizvoda[i].FK_Proizvodjac.Naziv;
                pb.Dispose();
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
            if (magacin.ListaProizvoda.Count <= 0)
                return;
            foreach (DataGridViewRow Row in dataGridView1.SelectedRows)
            {
                int id = Convert.ToInt32(Row.Cells[1].Value.ToString());
                br.DeleteProizvod(id);

                string putanja = magacin.ListaProizvoda[Row.Index].Slika_Proizvoda;
                bool check = false;
                foreach(Proizvod proizvod in magacin.ListaProizvoda)
                {
                    if(proizvod.Slika_Proizvoda.Equals(putanja) && proizvod.ID_Proizvoda != id)
                    {
                        check = true;
                        break;
                    }
                }
                if (!check)
                {
                    File.Delete(putanja);
                }
                
                magacin.ListaProizvoda.RemoveAt(Row.Index);
            }
            Prikazi();
        }
    }
}
