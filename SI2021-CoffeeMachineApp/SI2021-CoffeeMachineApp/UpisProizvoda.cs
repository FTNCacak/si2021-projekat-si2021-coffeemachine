﻿using System;
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
using System.Drawing.Imaging;
using System.IO;

namespace SI2021_CoffeeMachineApp
{
    public partial class UpisProizvoda : Form
    {
        public Magacin magacin { get; set; }
        private int nacinSortiranja { get; set; }
        private string putanjaDoSlike { get; set; } 
        private List<Proizvodjac> listaProizvodjaca = new List<Proizvodjac>();
        private List<int> listaSelektovanihProizvodjacID = new List<int>();
        private readonly BusinessRepository br = new BusinessRepository();
        public UpisProizvoda(Magacin magacin)
        {
            this.magacin = magacin; 
            InitializeComponent();
        }

        private void slika_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                Bitmap pb = new Bitmap(dialog.FileName);
                Image slika = Image.FromHbitmap(pb.GetHbitmap());
                pictureBox1.Image = slika;
                putanjaDoSlike = dialog.FileName;
            }
            else
            {
                MessageBox.Show("Morate uneti validnu sliku za proizvod!");
                putanjaDoSlike = "";
                pictureBox1.Image = null;
            }
        }

        private void UpisProizvoda_Load(object sender, EventArgs e)
        {
            cbNacinSortiranja.SelectedIndex = 0;
            foreach (Proizvodjac proizvodjac in magacin.ListaProizvodjaca)
            {
                FKProizvodjac.Items.Add(proizvodjac.Naziv);
                listaProizvodjaca.Add(proizvodjac);
            }
            listaSelektovanihProizvodjacID.Clear();
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
                listaSelektovanihProizvodjacID.Add(magacin.ListaProizvoda[i].FK_Proizvodjac.ID_Proizvodjaca);
                pb.Dispose();
            }
        }
        private void Prikazi()
        {
            dataGridView1.Rows.Clear();
            if (magacin.ListaProizvoda.Count > 1)
                dataGridView1.Rows.Add(magacin.ListaProizvoda.Count - 1);
            listaSelektovanihProizvodjacID.Clear();
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
                listaSelektovanihProizvodjacID.Add(magacin.ListaProizvoda[i].FK_Proizvodjac.ID_Proizvodjaca);
                pb.Dispose();
            }
        }
        private void Sort()
        {
            for (int i = 0; i < magacin.ListaProizvoda.Count - 1; i++)
            {
                for (int j = i; j < magacin.ListaProizvoda.Count; j++)
                {
                    if (nacinSortiranja == 0 && magacin.ListaProizvoda[i].Naziv.CompareTo(magacin.ListaProizvoda[j].Naziv) > 0)
                    {
                        Proizvod pom = magacin.ListaProizvoda[i];
                        magacin.ListaProizvoda[i] = magacin.ListaProizvoda[j];
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
            try { 
                if(naziv.Text!="" && opis.Text!="" && FKProizvodjac.SelectedIndex!=-1 && putanjaDoSlike != "" && cena.Text != "")
                {
                    if (!File.Exists("..\\..\\Images\\" + putanjaDoSlike.Split('\\').Last().Split('.')[0] + ".jpg"))
                    {
                        try
                        {
                            Bitmap pb = new Bitmap(putanjaDoSlike);
                            Image slika = Image.FromHbitmap(pb.GetHbitmap());
                            pb.Dispose();
                            ImageCodecInfo myImageCodecInfo = GetEncoder(ImageFormat.Jpeg);
                            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                            EncoderParameter myEncoderParameter;
                            EncoderParameters myEncoderParameters = new EncoderParameters(1);

                            myEncoderParameter = new EncoderParameter(myEncoder, 100L);
                            myEncoderParameters.Param[0] = myEncoderParameter;
                            slika.Save("..\\..\\Images\\" + putanjaDoSlike.Split('\\').Last().Split('.')[0] + ".jpg", myImageCodecInfo, myEncoderParameters);
                        }
                        catch
                        {
                            MessageBox.Show("Nije uspelo dodavanje nove slike za proizvod!");
                            pictureBox1.Image = null;
                            putanjaDoSlike = "";
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Slika pod ovim nazivom već postoji! Pokušajte da promenite naziv izvorne slike, ili da dodate drugu sliku proizvoda. Trenutna slika pod ovim nazivom koja se nalazi u bazi podataka biće upisana kao Slika Proizvoda za ovaj proizvod.");
                        //pictureBox1.Image = null;
                        //putanjaDoSlike = "";
                        //return;
                    }

                    putanjaDoSlike = "..\\..\\Images\\" + putanjaDoSlike.Split('\\').Last().Split('.')[0] + ".jpg";

                    Proizvod p = new Proizvod() { Naziv = naziv.Text , Opis = opis.Text , FK_Proizvodjac = listaProizvodjaca[FKProizvodjac.SelectedIndex], Slika_Proizvoda = putanjaDoSlike, Cena = Convert.ToDecimal(cena.Text)};
                    if (br.InsertProizvod(p))
                    {
                        MessageBox.Show("Uspešno ste uneli proizvod.");
                        naziv.Text = "";
                        opis.Text = "";
                        cena.Text = "";
                        FKProizvodjac.SelectedIndex = -1;
                        putanjaDoSlike = "";
                        pictureBox1.Image = null;
                        magacin = br.getData();
                        Prikazi();
                    }
                    else
                        MessageBox.Show("Proizvod nije unet.");
                }
                else
                {
                    MessageBox.Show("Morate popuniti sve podatke na ispravan način kako bi uneli proizvod!");
                }
            }
            catch { MessageBox.Show("Podaci nisu ispravno uneti!"); }
        }
        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try { 
                if (naziv.Text != "" && opis.Text != "" && FKProizvodjac.SelectedIndex != -1 && putanjaDoSlike != "" && cena.Text != "")
                {
                    if (dataGridView1.SelectedRows.Count != 0)
                    {
                        if (!File.Exists("..\\..\\Images\\" + putanjaDoSlike.Split('\\').Last().Split('.')[0] + ".jpg"))
                        {
                            try
                            {
                                Bitmap pb = new Bitmap(putanjaDoSlike);
                                Image slika = Image.FromHbitmap(pb.GetHbitmap());
                                pb.Dispose();
                                ImageCodecInfo myImageCodecInfo = GetEncoder(ImageFormat.Jpeg);
                                System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                                EncoderParameter myEncoderParameter;
                                EncoderParameters myEncoderParameters = new EncoderParameters(1);

                                myEncoderParameter = new EncoderParameter(myEncoder, 100L);
                                myEncoderParameters.Param[0] = myEncoderParameter;
                                slika.Save("..\\..\\Images\\" + putanjaDoSlike.Split('\\').Last().Split('.')[0] + ".jpg", myImageCodecInfo, myEncoderParameters);
                            }
                            catch
                            {
                                MessageBox.Show("Nije uspelo dodavanje nove slike za proizvod!");
                                pictureBox1.Image = null;
                                putanjaDoSlike = "";
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Slika ažuriranog proizvoda ostaće nepromenjena. Ako želite da je promenite izaberite drugu sliku sa drugačijim nazivom.");
                            //pictureBox1.Image = null;
                            //putanjaDoSlike = "";
                            //return;
                        }

                        putanjaDoSlike = "..\\..\\Images\\" + putanjaDoSlike.Split('\\').Last().Split('.')[0] + ".jpg";

                        Proizvod p = new Proizvod() { Naziv = naziv.Text, Opis = opis.Text, FK_Proizvodjac = listaProizvodjaca[FKProizvodjac.SelectedIndex], Slika_Proizvoda = putanjaDoSlike, Cena = Convert.ToDecimal(cena.Text) };
                        bool check = false;
                        foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                        {
                            int ID = Convert.ToInt32(row.Cells[1].Value.ToString());
                            if (br.UpdateProizvod(ID, p))
                            {
                                check = true;
                                naziv.Text = "";
                                opis.Text = "";
                                cena.Text = "";
                                FKProizvodjac.SelectedIndex = -1;
                                putanjaDoSlike = "";
                                pictureBox1.Image = null;
                                magacin = br.getData();
                                Prikazi();
                            }
                            else
                            {
                                check = false;
                                break;
                            }
                        }
                        if(check)
                            MessageBox.Show("Uspešno ste ažurirali proizvode.");
                        else
                            MessageBox.Show("Proizvodi nisu ažurirani.");
                    }
                    else
                        MessageBox.Show("Morate odabrati proizvode koje treba ažurirati kako bi ažurirali proizvode!");

                }
                else
                {
                    MessageBox.Show("Morate popuniti sve podatke na ispravan način kako bi ažurirali proizvode!");
                }
            }
            catch { MessageBox.Show("Podaci nisu ispravno uneti!"); }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (magacin.ListaProizvoda.Count <= 0)
                return;
            naziv.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            opis.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            cena.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            FKProizvodjac.SelectedIndex = listaProizvodjaca.FindIndex(p => p.ID_Proizvodjaca == listaSelektovanihProizvodjacID[e.RowIndex]);
            putanjaDoSlike = magacin.ListaProizvoda[e.RowIndex].Slika_Proizvoda;
            Bitmap pb = new Bitmap(magacin.ListaProizvoda[e.RowIndex].Slika_Proizvoda);
            Image slika = Image.FromHbitmap(pb.GetHbitmap());
            pictureBox1.Image = slika;
            pb.Dispose();
        }
    }
}
