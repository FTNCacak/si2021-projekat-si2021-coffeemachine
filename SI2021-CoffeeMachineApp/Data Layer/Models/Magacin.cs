using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Models
{
    public class Magacin
    {
        public List<Proizvodjac> ListaProizvodjaca { get; set; }
        public List<Proizvod> ListaProizvoda { get; set; }
        public List<Dobavljac> ListaDobavljaca { get; set; }
        public List<Dopremnica> ListaDopremnica{ get; set; }
        public List<Narudzbina> ListaNarudzbina { get; set; }
        public List<Evidencija> ListaEvidencija { get; set; }
        public List<Radnik> ListaRadnika { get; set; }
        public List<Korisnik> ListaKorisnika { get; set; }
    }
}
