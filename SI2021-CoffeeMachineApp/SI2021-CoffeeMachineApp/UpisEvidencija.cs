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
using System.Data.SqlClient;

namespace SI2021_CoffeeMachineApp
{
    public partial class UpisEvidencija : Form
    {
        public Magacin magacin { get; set; }
        private readonly BusinessRepository br = new BusinessRepository();
        private int nacinSortiranja { get; set; }
        private List<Narudzbina> listaNarudzbina = new List<Narudzbina>();
        private List<Proizvod> listaProizvoda = new List<Proizvod>();

        private List<int> listaSelektovanihNarudzbinaID = new List<int>();
        private List<int> listaSelektovanihProizvodID = new List<int>();
        public UpisEvidencija(Magacin magacin)
        {
            this.magacin = magacin;
            InitializeComponent();
        }

        private void UpisEvidencija_Load(object sender, EventArgs e)
        {
            foreach (Narudzbina narudzbina in magacin.ListaNarudzbina)
            {
                FKNarudzbina.Items.Add(narudzbina.Opis);
                listaNarudzbina.Add(narudzbina);
            }
            foreach (Proizvod proizvod in magacin.ListaProizvoda)
            {
                FKProizvod.Items.Add(proizvod.Naziv);
                listaProizvoda.Add(proizvod);
            }
            listaSelektovanihNarudzbinaID.Clear();
            listaSelektovanihProizvodID.Clear();
            cbNacinSortiranja.SelectedIndex = 0;
            dataGridView1.Columns.Add("Opis", "Opis");
            dataGridView1.Columns.Add("Napomena", "Napomena");
            dataGridView1.Columns.Add("FK_ID_Narudzbine", "ID narudžbine");
            dataGridView1.Columns.Add("FK_ID_Proizvoda", "Naziv proizvoda");
            if (magacin.ListaEvidencija.Count > 1)
                dataGridView1.Rows.Add(magacin.ListaEvidencija.Count - 1);
            for (int i = 0; i < magacin.ListaEvidencija.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = magacin.ListaEvidencija[i].Opis;
                dataGridView1.Rows[i].Cells[1].Value = magacin.ListaEvidencija[i].Napomena;
                dataGridView1.Rows[i].Cells[2].Value = magacin.ListaEvidencija[i].FK_Narudzbina.ID_Narudzbine;
                dataGridView1.Rows[i].Cells[3].Value = magacin.ListaEvidencija[i].FK_Proizvod.Naziv;
                listaSelektovanihNarudzbinaID.Add(magacin.ListaEvidencija[i].FK_Narudzbina.ID_Narudzbine);
                listaSelektovanihProizvodID.Add(magacin.ListaEvidencija[i].FK_Proizvod.ID_Proizvoda);
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
            if (magacin.ListaEvidencija.Count > 1)
                dataGridView1.Rows.Add(magacin.ListaEvidencija.Count - 1);
            listaSelektovanihNarudzbinaID.Clear();
            listaSelektovanihProizvodID.Clear();
            for (int i = 0; i < magacin.ListaEvidencija.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = magacin.ListaEvidencija[i].Opis;
                dataGridView1.Rows[i].Cells[1].Value = magacin.ListaEvidencija[i].Napomena;
                dataGridView1.Rows[i].Cells[2].Value = magacin.ListaEvidencija[i].FK_Narudzbina.ID_Narudzbine;
                dataGridView1.Rows[i].Cells[3].Value = magacin.ListaEvidencija[i].FK_Proizvod.Naziv;
                listaSelektovanihNarudzbinaID.Add(magacin.ListaEvidencija[i].FK_Narudzbina.ID_Narudzbine);
                listaSelektovanihProizvodID.Add(magacin.ListaEvidencija[i].FK_Proizvod.ID_Proizvoda);
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
                if (FKNarudzbina.SelectedIndex != -1 && FKProizvod.SelectedIndex != -1 && opis.Text != "" && napomena.Text != "")
                {
                    Evidencija ev = new Evidencija() { Opis = opis.Text, Napomena = napomena.Text, FK_Narudzbina = listaNarudzbina[FKNarudzbina.SelectedIndex], FK_Proizvod = listaProizvoda[FKProizvod.SelectedIndex] };
                    if (br.InsertEvidencija(ev))
                    {
                        MessageBox.Show("Uspešno ste uneli evidenciju.");
                        FKNarudzbina.SelectedIndex = -1;
                        FKProizvod.SelectedIndex = -1;
                        opis.Text = "";
                        napomena.Text = "";
                        magacin = br.getData();
                        Prikazi();
                    }
                    else
                        MessageBox.Show("Evidencija nije uneta.");
                }
                else
                {
                    MessageBox.Show("Morate popuniti sve podatke na ispravan način kako bi uneli evidenciju!");
                }
            }
            catch (SqlException ex) { MessageBox.Show("Podaci ne mogu biti uneti u bazu, jer kombinacija ove Evidencije i Proizvoda vec postoje u bazi podataka!"); }
            catch { MessageBox.Show("Podaci nisu ispravno uneti!"); }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (FKNarudzbina.SelectedIndex != -1 && FKProizvod.SelectedIndex != -1 && opis.Text != "" && napomena.Text != "")
                {
                    if (dataGridView1.SelectedRows.Count != 0)
                    {
                        Evidencija ev = new Evidencija() { Opis = opis.Text, Napomena = napomena.Text, FK_Narudzbina = listaNarudzbina[FKNarudzbina.SelectedIndex], FK_Proizvod = listaProizvoda[FKProizvod.SelectedIndex] };
                        bool check = false;
                        foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                        {
                            int ID1 = listaSelektovanihNarudzbinaID[row.Index];
                            int ID2 = listaSelektovanihProizvodID[row.Index];
                            if (br.UpdateEvidencija(ID1, ID2, ev))
                            {
                                check = true;
                                FKNarudzbina.SelectedIndex = -1;
                                FKProizvod.SelectedIndex = -1;
                                opis.Text = "";
                                napomena.Text = "";
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
                            MessageBox.Show("Uspešno ste ažurirali evidencije.");
                        else
                            MessageBox.Show("Evidencije nisu ažurirane.");
                    }
                    else
                        MessageBox.Show("Morate odabrati evidencije koje treba ažurirati kako bi ažurirali evidencije!");

                }
                else
                {
                    MessageBox.Show("Morate popuniti sve podatke na ispravan način kako bi ažurirali evidencije!");
                }
            }
            catch { MessageBox.Show("Podaci nisu ispravno uneti!"); }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (magacin.ListaEvidencija.Count <= 0)
                return;
            FKNarudzbina.SelectedIndex = listaNarudzbina.FindIndex(n => n.ID_Narudzbine == listaSelektovanihNarudzbinaID[e.RowIndex]);
            FKProizvod.SelectedIndex = listaProizvoda.FindIndex(p => p.ID_Proizvoda == listaSelektovanihProizvodID[e.RowIndex]);
            opis.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            napomena.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }
    }
}
