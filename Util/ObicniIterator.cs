using Michelin.Interfaces;
using Michelin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Michelin.Util
{
    public class ObicniIterator : IIterator
    {
        #region Prototypes
        public List<ZahtjevZaPomoc> zahtjeviZaPomoc { get; set; }
        public int trenutniZahtjev { get; set; }
        #endregion

        #region Constructor
        public ObicniIterator(List<ZahtjevZaPomoc> sviZahtjevi)
        {
            //uzima sve neobradjene zahtjeve iz baze i njima inicijalizira listu
            //trenutni zahtjev postavlja na 0

            sviZahtjevi.RemoveAll(z => z.obradjeno == true);
            sviZahtjevi.Sort((x, y) => y.datum.CompareTo(x.datum));
            zahtjeviZaPomoc = sviZahtjevi;
            trenutniZahtjev = 0;
          
        }
        #endregion

        #region Metode
        public ZahtjevZaPomoc dajNaredniZahtjevZaPomoc()
        {
            if (trenutniZahtjev + 1 < zahtjeviZaPomoc.Count)
            {
                trenutniZahtjev++;
                return zahtjeviZaPomoc[trenutniZahtjev - 1];
            }

            throw new Exception("Van opsega");
        }
        #endregion
    }
}
