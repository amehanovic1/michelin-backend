using Michelin.Interfaces;
using Michelin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Michelin.Util
{
    public class ReceptAdapter : Recept, IVegan
    {
        public List<Recept> dajVeganskaJela(List<Recept> sviRecepti) 
        {
            sviRecepti.RemoveAll(r => r.vegansko != true);
            return sviRecepti;
        }


        public List<Recept> dajVeganskaJelaFiltrirana(List<VrstaJela> vrstaJela, List<NacionalnoJelo> nacionalnoJelo, int vrijemePripreme, List<Recept> receptiZaFiltriranje)
        {

            //vraca proslijedjenu listu recepata isfiltriranu na osnovu proslijedjenih parametara
            //i takvu da sadrzi iskljucivo veganske recepte
            receptiZaFiltriranje.RemoveAll(r => r.vegansko != true);
            return Recept.FiltrirajReceptePoParametrima
                (receptiZaFiltriranje,vrstaJela, nacionalnoJelo, vrijemePripreme);
        }
    }
}
