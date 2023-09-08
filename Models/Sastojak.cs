using Michelin.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Michelin.Models
{
	public enum MjernaJedinica
    {
		[Display(Name = "kg")]
		kg,
		[Display(Name = "g")]
		g,
		[Display(Name = "l")]
		l,
		[Display(Name = "ml")]
		ml,
		[Display(Name = "komad")]
		komad
    }
	public class Sastojak : ISastojak, IPrototip
	{
        #region Properties

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string id { get; set; }

		[Required]
        public string naziv { get; set; }

		[Required]
		public double kolicina { get; set; }

		[Required]
		[EnumDataType(typeof(MjernaJedinica))]
		public MjernaJedinica mjernaJedinica { get; set; }

        #endregion

        #region Constructor

		public Sastojak (string naziv, double kolicina, MjernaJedinica mjernaJedinica)
        {
			this.naziv = naziv;
			this.kolicina = kolicina;
			this.mjernaJedinica = mjernaJedinica;
        }

		public Sastojak() { }

        public string dajNaziv()
        {
			return naziv;
        }

        public IPrototip kloniraj()
        {
			return (IPrototip)this.MemberwiseClone();
        }
        #endregion

    }
}
