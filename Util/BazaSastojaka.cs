using Michelin.Interfaces;
using Michelin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Michelin.Util
{
    public class BazaSastojaka
    {

        #region Prototypes
        public static List<ISastojak> listaSastojaka { get; set; }
        public static BazaSastojaka bazaSastojaka { get; set; } = new BazaSastojaka();
        #endregion

       

        #region Metode
        public static BazaSastojaka getInstance()
        {
            return bazaSastojaka;
        }
        public ISastojak dodajUBazuSastojaka(String naziv,double kolicina, MjernaJedinica mjernaJedinica)
        {
            //provjerava da li u listaSastojaka postoji ISastojak s istim nazivom i mjernom jedinicom i
            //ako postoji, klonira ga i vraca njegovu kopiju kao povratnu vrijednost
            //ako ne postoji, kreira novi Sastojak i eventualno korigira naziv tako da pocinje velikim slovom
            // a ostala su mala, te taj novi Sastojak dodaje u listaSastojaka i vraca ga kao povratnu vrijednost

           string modifikovanNaziv = naziv.ToLowerInvariant();
            foreach(ISastojak s in listaSastojaka)
            {
                if (s.dajNaziv().Equals(naziv))
                {
                    return (ISastojak)s.kloniraj();
                }

            }

            return new Sastojak(modifikovanNaziv, kolicina, mjernaJedinica);
            
        }
        #endregion
    }
}
