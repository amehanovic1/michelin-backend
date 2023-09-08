using Michelin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Michelin.Util
{
    public class PretplatnikRepozitorij
    {
        #region Prototypes
        public static List<IPretplatnik> pretplatnici { get; set; } = new List<IPretplatnik>();
        public static PretplatnikRepozitorij repozitorij { get; set; } = new PretplatnikRepozitorij();

        
        #endregion

        #region Metode
        public static PretplatnikRepozitorij getInstance()
        {
            if (repozitorij == null)
                repozitorij = new PretplatnikRepozitorij();
            return repozitorij;
        }
        public void dodajPretplatnika(IPretplatnik pretplatnik)
        {
            if (!(pretplatnici.Contains(pretplatnik)))
            pretplatnici.Add(pretplatnik);
        }
        public void ukloniPretplatnika(IPretplatnik pretplatnik)
        {
            if (pretplatnici.Contains(pretplatnik))
                pretplatnici.Remove(pretplatnik);
        }

        public void obavijestiPretplatnike()
        {
            foreach(IPretplatnik p in pretplatnici)
            {
                p.posaljiMail();
                p.posaljiPoruku();
            }
        }
        #endregion
    }

}
