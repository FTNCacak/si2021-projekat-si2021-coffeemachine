using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Data_Layer;
using Data_Layer.Models;
using Business_Layer;
using Business_Layer.Interfaces;

namespace MagacinWebApp
{
    public partial class SiteMaster : MasterPage
    {
        private readonly BusinessRepository br = new BusinessRepository(new MagacinRepository());
        public double cena = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            br.getData();
            foreach (Proizvod proizvod in br.magacin.ListaProizvoda)
            {
                string p = proizvod.Naziv + " " + proizvod.Opis + " " + proizvod.Cena;
                ListBox1.Items.Add(p);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string pretraga = TextBox1.Text;
            ListBox1.Items.Clear();
            foreach (Proizvod proizvod in br.magacin.ListaProizvoda)
            {
                if((proizvod.Naziv + " " + proizvod.Opis).ToLower().Contains(pretraga.ToLower()))
                {
                    string p = proizvod.Naziv + " " + proizvod.Opis + " " + proizvod.Cena;
                    ListBox1.Items.Add(p);
                }
            }
        }
        
        protected void Button2_Click(object sender, EventArgs e)
        {
            
            if (ListBox1.SelectedIndex != -1)
            {
                string item = ListBox1.SelectedValue;
                double cena = 0;
                ListBox2.Items.Add(item);

                for (int i = 0; i < ListBox2.Items.Count; i++)
                {
                    cena += Convert.ToDouble(ListBox2.Items[i].ToString().Split(' ').Last());
                }
                Label1.Text = "" + cena;
            }
            ListBox1.Items.Clear();
            foreach (Proizvod proizvod in br.magacin.ListaProizvoda)
            {
                string p = proizvod.Naziv + " " + proizvod.Opis + " " + proizvod.Cena;
                ListBox1.Items.Add(p);
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            double cena = 0;
            ListBox1.Items.Clear();
            foreach (Proizvod proizvod in br.magacin.ListaProizvoda)
            {
                string p = proizvod.Naziv + " " + proizvod.Opis + " " + proizvod.Cena;
                ListBox1.Items.Add(p);
            }
            if (ListBox2.SelectedIndex != -1)
            {
                int item = ListBox1.SelectedIndex + 1;
                
                ListBox2.Items.RemoveAt(item);

                for (int i = 0; i < ListBox2.Items.Count; i++)
                {
                    cena += Convert.ToDouble(ListBox2.Items[i].ToString().Split(' ').Last());
                }
                Label1.Text = cena.ToString();
            }
            
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            ListBox1.Items.Clear();
            ListBox2.Items.Clear();
            Label1.Text = "0";
            TextBox1.Text = "";
            foreach (Proizvod proizvod in br.magacin.ListaProizvoda)
            {
                string p = proizvod.Naziv + " " + proizvod.Opis + " " + proizvod.Cena;
                ListBox1.Items.Add(p);
            }
        }
    }
}