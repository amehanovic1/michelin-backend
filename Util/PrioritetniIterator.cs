using Michelin.Interfaces;
using Michelin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Michelin.Util
{
    public class PrioritetniIterator : IIterator
    {
        #region Prototypes
        public List<ZahtjevZaPomoc> zahtjeviZaPomoc { get; set; }
        public int trenutniZahtjev { get; set; }
        #endregion

        #region Constructor
        public PrioritetniIterator(List<ZahtjevZaPomoc> zahtjevi)
        {
            zahtjeviZaPomoc = zahtjevi;
            zahtjeviZaPomoc.Sort((ZahtjevZaPomoc x, ZahtjevZaPomoc y) =>
            {
                if (x.kategorija == KategorijaZahtjevaZaPomoc.Tehnicki_Problem && y.kategorija == KategorijaZahtjevaZaPomoc.Pogresno_napisan_recept)
                    return -1;
                else if (x.kategorija == KategorijaZahtjevaZaPomoc.Tehnicki_Problem && y.kategorija == KategorijaZahtjevaZaPomoc.Pitanja_Sugestije)
                    return -1;
                else if (x.kategorija == KategorijaZahtjevaZaPomoc.Neprimjeren_sadrzaj)
                    return -1;
                else if (x.kategorija == KategorijaZahtjevaZaPomoc.Pogresno_napisan_recept && y.kategorija == KategorijaZahtjevaZaPomoc.Pitanja_Sugestije)
                    return -1;
                else if (x.kategorija == y.kategorija)
                    return 0;

                return 1;
            });
            trenutniZahtjev = 0;
        }
        #endregion

        #region Metode
        public ZahtjevZaPomoc dajNaredniZahtjevZaPomoc()
        {
            if (trenutniZahtjev + 1 > zahtjeviZaPomoc.Count - 1) throw new Exception("Izvan opsega");
            trenutniZahtjev++;
            return zahtjeviZaPomoc[trenutniZahtjev-1];
        }
        #endregion
    }
}
