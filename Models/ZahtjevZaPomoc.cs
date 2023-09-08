using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Michelin.Models
{
	public enum KategorijaZahtjevaZaPomoc
    {
		[Display(Name = "Tehnički problem")]
		Tehnicki_Problem,
		[Display(Name = "Pogrešno napisan recept")]
		Pogresno_napisan_recept,
		[Display(Name = "Neprimjeren sadržaj")]
		Neprimjeren_sadrzaj,
		[Display(Name = "Pitanja i sugestije")]
		Pitanja_Sugestije
    }
	public class ZahtjevZaPomoc
	{
        #region Properties

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string id { get; set; }

        [Required]
		[EnumDataType(typeof(KategorijaZahtjevaZaPomoc))]
		[Display(Name ="Kategorija")]
		public KategorijaZahtjevaZaPomoc kategorija { get; set; }

		[Required]
		[StringLength(1000, MinimumLength = 10, 
							ErrorMessage ="Problem se mora opisati sa najviše 1000, a najmanje 10 karaktera!")]
		[Display(Name = "Opis problema")]
		public string sadrzaj { get; set; }

		
		[Display(Name = "Prijavio")]
		public Korisnik korisnik { get; set; }

		
		[Display(Name = "Obrađeno")]
		public Boolean obradjeno { get; set; }

		[Display(Name ="Datum prijave")]
		public DateTime datum { get; set; }

        #endregion

        #region Constructor
		public ZahtjevZaPomoc(KategorijaZahtjevaZaPomoc kategorija, string sadrzaj, Korisnik korisnik)
        {
			this.sadrzaj = sadrzaj;
			this.kategorija = kategorija;
			this.korisnik = korisnik;
			this.obradjeno = false;
        }

		public ZahtjevZaPomoc() { }
        #endregion
    }
}
