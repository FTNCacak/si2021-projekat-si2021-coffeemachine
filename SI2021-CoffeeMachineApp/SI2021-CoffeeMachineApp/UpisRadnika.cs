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
    public partial class UpisRadnika : Form
    {
        public Magacin magacin { get; set; }
        private readonly BusinessRepository br = new BusinessRepository();
        private List<Radnik> listaRukovodilaca = new List<Radnik>();
        private List<int> listaSelektovanihRukovodilacID = new List<int>();
        private int nacinSortiranja { get; set; }
        public UpisRadnika(Magacin magacin)
        {
            this.magacin = magacin;
            InitializeComponent();
        }

        private void UpisRadnika_Load(object sender, EventArgs e)
        {
            foreach(Radnik rukovodilac in magacin.ListaRadnika)
            {
                FKRukovodilac.Items.Add(rukovodilac.Ime);
                listaRukovodilaca.Add(rukovodilac);
            }
            listaSelektovanihRukovodilacID.Clear();
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
                listaSelektovanihRukovodilacID.Add(magacin.ListaRadnika[i].FK_Rukovodilac.ID_Radnika);
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
            listaSelektovanihRukovodilacID.Clear();
            FKRukovodilac.Items.Clear();
            listaRukovodilaca.Clear(); 
            foreach (Radnik rukovodilac in magacin.ListaRadnika)
            {
                FKRukovodilac.Items.Add(rukovodilac.Ime);
                listaRukovodilaca.Add(rukovodilac);
            }
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
                listaSelektovanihRukovodilacID.Add(magacin.ListaRadnika[i].FK_Rukovodilac.ID_Radnika);
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
                if (ime.Text != "" && prezime.Text != "" && telefon.Text != "" && jmbg.Text != "" && email.Text != "" && username.Text != "" && password.Text != "" && jmbg.TextLength == 13)
                {
                    Radnik r = new Radnik() { Ime = ime.Text, Prezime = prezime.Text, Telefon = telefon.Text, JMBG = jmbg.Text, Email = email.Text, Username = username.Text, Password = password.Text};
                    if (FKRukovodilac.SelectedIndex == -1)
                        r.FK_Rukovodilac = null;
                    else
                        r.FK_Rukovodilac = listaRukovodilaca[FKRukovodilac.SelectedIndex];

                    if (br.InsertRadnik(r))
                    {
                        MessageBox.Show("Uspešno ste uneli radnika.");
                        ime.Text = "";
                        prezime.Text = "";
                        telefon.Text = "";
                        jmbg.Text = "";
                        email.Text = "";
                        username.Text = "";
                        password.Text = "";
                        FKRukovodilac.SelectedIndex = -1;
                        magacin = br.getData();
                        Prikazi();
                    }
                    else
                        MessageBox.Show("Radnik nije unet.");
                }
                else
                {
                    MessageBox.Show("Morate popuniti sve podatke na ispravan način kako bi uneli radnika!");
                }
            }
            catch(Exception ex) { MessageBox.Show(ex + ""); MessageBox.Show("Podaci nisu ispravno uneti!"); }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (magacin.ListaRadnika.Count <= 0)
                return;
            ime.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            prezime.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            telefon.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            jmbg.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            email.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            username.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            password.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();

            FKRukovodilac.SelectedIndex = listaRukovodilaca.FindIndex(r => r.ID_Radnika == listaSelektovanihRukovodilacID[e.RowIndex]);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (ime.Text != "" && prezime.Text != "" && telefon.Text != "" && jmbg.Text != "" && email.Text != "" && username.Text != "" && password.Text != "" && jmbg.TextLength == 13)
                {
                    if (dataGridView1.SelectedRows.Count != 0)
                    {
                        Radnik r = new Radnik() { Ime = ime.Text, Prezime = prezime.Text, Telefon = telefon.Text, JMBG = jmbg.Text, Email = email.Text, Username = username.Text, Password = password.Text };
                        if (FKRukovodilac.SelectedIndex == -1)
                            r.FK_Rukovodilac = null;
                        else
                            r.FK_Rukovodilac = listaRukovodilaca[FKRukovodilac.SelectedIndex]; 
                        bool check = false;
                        foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                        {
                            int ID = Convert.ToInt32(row.Cells[0].Value.ToString());
                            if (br.UpdateRadnik(ID, r))
                            {
                                check = true;
                                ime.Text = "";
                                prezime.Text = "";
                                telefon.Text = "";
                                jmbg.Text = "";
                                email.Text = "";
                                username.Text = "";
                                password.Text = "";
                                FKRukovodilac.SelectedIndex = -1;
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
                            MessageBox.Show("Uspešno ste ažurirali radnike.");
                        else
                            MessageBox.Show("Radnici nisu ažurirani.");
                    }
                    else
                        MessageBox.Show("Morate odabrati radnike koje treba ažurirati kako bi ažurirali radnike!");

                }
                else
                {
                    MessageBox.Show("Morate popuniti sve podatke na ispravan način kako bi ažurirali radnike!");
                }
            }
            catch { MessageBox.Show("Podaci nisu ispravno uneti!"); }
        }
    }
}
