using System;
using Business_Layer;
using System.IO;
using Data_Layer;
using Data_Layer.Models;
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
    public partial class Pocetna : Form
    {
        private Korisnik korisnik = new Korisnik();
        public Magacin magacin { get; set; }
        public Pocetna()
        {
            InitializeComponent();
        }

        private void prijaviSeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Login f = new Login())
            {
                var rezultat = f.ShowDialog();
                if (rezultat == DialogResult.OK)
                {
                    korisnik = f.korisnik;
                    menuStrip1.Items[0].Enabled = true;
                    menuStrip1.Items[1].Enabled = true;
                    if (korisnik.Role.ToUpper().Equals("ADMIN"))
                    {
                        prikazRadnikaToolStripMenuItem.Visible = true;
                        prikazKorisnikaToolStripMenuItem.Visible = true;
                        upisRadnikaToolStripMenuItem.Visible = true;
                        upisKorisnikaToolStripMenuItem.Visible = true;
                    }
                    BusinessRepository br = new BusinessRepository();
                    magacin = br.getData();
                }
            }
            
        }

        private void prikazProizvodaToolStripMenuItem_Click(object sender, EventArgs e)
        {

            PrikazProizvoda pp = new PrikazProizvoda(magacin);
            pp.ShowDialog();
        }

        private void Pocetna_Load(object sender, EventArgs e)
        {
        }

        private void prikazProizvođačaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrikazProizvodjaca pp = new PrikazProizvodjaca(magacin);
            pp.ShowDialog();
        }

        private void prikazKorisnikaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrikazKorisnika pk = new PrikazKorisnika(magacin);
            pk.ShowDialog();
        }
    }
}
