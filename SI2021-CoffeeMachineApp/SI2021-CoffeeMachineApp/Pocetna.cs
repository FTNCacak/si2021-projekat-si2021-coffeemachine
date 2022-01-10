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
                    else
                    {
                        prikazRadnikaToolStripMenuItem.Visible = false;
                        prikazKorisnikaToolStripMenuItem.Visible = false;
                        upisRadnikaToolStripMenuItem.Visible = false;
                        upisKorisnikaToolStripMenuItem.Visible = false;
                    }
                    odjaviSeToolStripMenuItem.Visible = true;
                    prijaviSeToolStripMenuItem.Visible = false;
                    lblWelcome.Text = "Dobrodošli, korisnik "+korisnik.Ime+" "+korisnik.Prezime;
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
            odjaviSeToolStripMenuItem.Visible = false;
            prijaviSeToolStripMenuItem.Visible = true;
        }

        private void prikazProizvođačaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (PrikazProizvodjaca pp = new PrikazProizvodjaca(magacin))
            {
                pp.ShowDialog();
                if (pp.DialogResult == DialogResult.Cancel)
                {
                    this.magacin = pp.magacin;
                }
            }
        }

        private void prikazKorisnikaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (PrikazKorisnika pk = new PrikazKorisnika(magacin))
            {
                pk.ShowDialog();
                if (pk.DialogResult == DialogResult.Cancel)
                {
                    this.magacin = pk.magacin;
                }
            }
        }

        private void prikazDobavljačaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (PrikazDobavljaca pd = new PrikazDobavljaca(magacin))
            {
                pd.ShowDialog();
                if (pd.DialogResult == DialogResult.Cancel)
                {
                    this.magacin = pd.magacin;
                }
            }
        }

        private void prikazDopremnicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (PrikazDopremnice pd = new PrikazDopremnice(magacin))
            {
                pd.ShowDialog();
                if (pd.DialogResult == DialogResult.Cancel)
                {
                    this.magacin = pd.magacin;
                }
            }
        }

        private void prikazNarudžbinaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (PrikazNarudzbine pn = new PrikazNarudzbine(magacin))
            {
                pn.ShowDialog();
                if (pn.DialogResult == DialogResult.Cancel)
                {
                    this.magacin = pn.magacin;
                }
            }
        }

        private void prikazRadnikaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (PrikazRadnika pr = new PrikazRadnika(magacin))
            {
                pr.ShowDialog();
                if (pr.DialogResult == DialogResult.Cancel)
                {
                    this.magacin = pr.magacin;
                }
            }
        }

        private void prikazEvidencijeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (PrikazEvidencije pe = new PrikazEvidencije(magacin))
            {
                pe.ShowDialog();
                if (pe.DialogResult == DialogResult.Cancel)
                {
                    this.magacin = pe.magacin;
                }
            }
        }

        private void upisProizvodaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (UpisProizvoda up = new UpisProizvoda(magacin)) {
                up.ShowDialog();
                if(up.DialogResult == DialogResult.Cancel)
                {
                    this.magacin = up.magacin;
                }
            }
        }

        private void odjaviSeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            korisnik = new Korisnik();
            magacin = new Magacin();
            menuStrip1.Items[0].Enabled = false;
            menuStrip1.Items[1].Enabled = false;
            prikazRadnikaToolStripMenuItem.Visible = false;
            prikazKorisnikaToolStripMenuItem.Visible = false;
            upisRadnikaToolStripMenuItem.Visible = false;
            upisKorisnikaToolStripMenuItem.Visible = false;

            odjaviSeToolStripMenuItem.Visible = false;
            prijaviSeToolStripMenuItem.Visible = true;
            lblWelcome.Text = "";
        }

        private void upisProizvođačaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (UpisProizvodjaca up = new UpisProizvodjaca(magacin))
            {
                up.ShowDialog();
                if (up.DialogResult == DialogResult.Cancel)
                {
                    this.magacin = up.magacin;
                }
            }
        }

        private void upisToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (UpisDobavljaca ud = new UpisDobavljaca(magacin))
            {
                ud.ShowDialog();
                if (ud.DialogResult == DialogResult.Cancel)
                {
                    this.magacin = ud.magacin;
                }
            }
        }

        private void upisToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            using (UpisDopremnica ud = new UpisDopremnica(magacin))
            {
                ud.ShowDialog();
                if (ud.DialogResult == DialogResult.Cancel)
                {
                    this.magacin = ud.magacin;
                }
            }
        }
    }
}
