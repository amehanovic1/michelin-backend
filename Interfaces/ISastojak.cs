using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Michelin.Models;

namespace Michelin.Interfaces
{
    public interface ISastojak : IPrototip
    {
        String dajNaziv();

        //zasad zakomentarisano dok se ne rijesi problem s enumom
        //MjernaJednica dajMjernuJedinicu();
    }
}
