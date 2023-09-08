using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Michelin.Models
{
	public  class Komentar
	{
        #region Properties

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string id { get; set; }

        [Required]
		[StringLength(maximumLength:300, MinimumLength =5, ErrorMessage ="Komentar mora biti duži od 5 karaktera i kraći od 300!")]
		public string sadrzaj { get; set; }

		[Required]
		public Korisnik autor { get; set; }

		[Required]
		public Recept recept { get; set; }

		[DataType(DataType.Date)]
		public DateTime datum { get; set; }

        #endregion

        #region Constructor
		public Komentar(string sadrzaj, Korisnik autor, Recept recept)
        {
			this.sadrzaj = sadrzaj;
			this.autor = autor;
			this.recept = recept;
			//id = generirajId();
			datum = DateTime.Now;
        }

		public Komentar() { }
        #endregion
    }
}
