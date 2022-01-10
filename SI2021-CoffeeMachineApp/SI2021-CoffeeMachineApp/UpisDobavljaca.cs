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
    public partial class UpisDobavljaca : Form
    {
        public Magacin magacin { get; set; }
        private readonly BusinessRepository br = new BusinessRepository();
        private int nacinSortiranja { get; set; }
        public UpisDobavljaca(Magacin magacin)
        {
            this.magacin = magacin;
            InitializeComponent();
        }

        private void UpisDobavljaca_Load(object sender, EventArgs e)
        {
            cbNacinSortiranja.SelectedIndex = 0;
            dataGridView1.Columns.Add("ID_Dobavljaca", "ID dobavljača");
            dataGridView1.Columns.Add("Naziv", "Naziv dobavljača");
            dataGridView1.Columns.Add("Adresa", "Adresa dobavljača");
            if (magacin.ListaDobavljaca.Count > 1)
                dataGridView1.Rows.Add(magacin.ListaDobavljaca.Count - 1);
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
            if (magacin.ListaDobavljaca.Count > 1)
                dataGridView1.Rows.Add(magacin.ListaDobavljaca.Count - 1);
            for (int i = 0; i < magacin.ListaDobavljaca.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = magacin.ListaDobavljaca[i].ID_Dobavljaca;
                dataGridView1.Rows[i].Cells[1].Value = magacin.ListaDobavljaca[i].Naziv;
                dataGridView1.Rows[i].Cells[2].Value = magacin.ListaDobavljaca[i].Adresa;
            }
        }

        private void btnSortiraj_Click(object sender, EventArgs e)
        {
            Sort();
            Prikazi();
        }

        private void cbNacinSortiranja_SelectedIndexChanged(object sender, EventArgs e)
        {
            nacinSortiranja = cbNacinSortiranja.SelectedIndex;
        }

        private void btnUpisi_Click(object sender, EventArgs e)
        {
            try
            {
                if (naziv.Text != "" && adresa.Text != "")
                {
                    Dobavljac d = new Dobavljac() { Naziv = naziv.Text, Adresa = adresa.Text };
                    if (br.InsertDobavljac(d))
                    {
                        MessageBox.Show("Uspešno ste uneli dobavljača.");
                        naziv.Text = "";
                        adresa.Text = "";
                        magacin = br.getData();
                        Prikazi();
                    }
                    else
                        MessageBox.Show("Dobavljač nije unet.");
                }
                else
                {
                    MessageBox.Show("Morate popuniti sve podatke na ispravan način kako bi uneli dobavlača!");
                }
            }
            catch { MessageBox.Show("Podaci nisu ispravno uneti!"); }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (naziv.Text != "" && adresa.Text != "")
                {
                    if (dataGridView1.SelectedRows.Count != 0)
                    {
                        Dobavljac d = new Dobavljac() { Naziv = naziv.Text, Adresa = adresa.Text }; 
                        bool check = false;
                        foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                        {
                            int ID = Convert.ToInt32(row.Cells[0].Value.ToString());
                            if (br.UpdateDobavljac(ID, d))
                            {
                                check = true;
                                naziv.Text = "";
                                adresa.Text = "";
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
                            MessageBox.Show("Uspešno ste ažurirali dobavljače.");
                        else
                            MessageBox.Show("Dobavljači nisu ažurirani.");
                    }
                    else
                        MessageBox.Show("Morate odabrati dobavljače koje treba ažurirati kako bi ažurirali dobavljače!");

                }
                else
                {
                    MessageBox.Show("Morate popuniti sve podatke na ispravan način kako bi ažurirali dobavljače!");
                }
            }
            catch { MessageBox.Show("Podaci nisu ispravno uneti!"); }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (magacin.ListaDobavljaca.Count <= 0)
                return;
            naziv.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            adresa.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
        }
    }
}
