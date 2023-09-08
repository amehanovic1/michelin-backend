using Michelin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Michelin.Interfaces
{
    public interface IVegan
    {
         List<Recept> dajVeganskaJela(List<Recept> sviRecepti);
         List<Recept> dajVeganskaJelaFiltrirana(List<VrstaJela> vrstaJela, List<NacionalnoJelo> nacionalnoJelo,
            int vrijemePripreme, List<Recept> receptiZaFiltriranje);
    }
}
