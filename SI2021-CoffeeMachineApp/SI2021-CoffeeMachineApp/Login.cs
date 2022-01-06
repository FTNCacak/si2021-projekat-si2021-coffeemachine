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
    public partial class Login : Form
    {
        public Korisnik korisnik { get; set; }
        public Login()
        {
            InitializeComponent();
        }
        

        private void btnPrijava_Click(object sender, EventArgs e)
        {
            if (tbUsername.Text != "" && tbPassword.Text != "")
            {
                BusinessRepository b = new BusinessRepository();
                Magacin m = new Magacin();
                m = b.getData();
                foreach(Korisnik Kor in m.ListaKorisnika)
                {
                    if (Kor.Username.Equals(tbUsername.Text) && Kor.Password.Equals(tbPassword.Text))
                    {
                        MessageBox.Show("Uspešna prijava");
                        this.DialogResult = DialogResult.OK;
                        this.korisnik = Kor;
                        this.Close();
                        return;
                    }
                }
                MessageBox.Show("Username ili Password nisu ispravni");
            }
            else
                MessageBox.Show("Unesite podatke!");
        }
    }
}
