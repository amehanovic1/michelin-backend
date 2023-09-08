using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Michelin.Models
{

	public enum NacionalnoJelo
    {
		[Display(Name = "Talijanska kuhinja")]
		Talijanska_Kuhinja,
		[Display(Name = "Francuska kuhinja")]
		Francuska_Kuhinja,
		[Display(Name = "Bosanska kuhinja")]
		Bosanska_Kuhinja,
		[Display(Name = "Kineska kuhinja")]
		Kineska_Kuhinja,
		[Display(Name = "Meksička kuhinja")]
		Meksicka_Kuhinja,
		[Display(Name = "Ostalo")]
		Ostalo

    }

	public enum VrstaJela
    {
		[Display(Name = "Kuhano")]
		Slano_Kuhano,
		[Display(Name = "Prženo")]
		Slano_Przeno,
		[Display(Name = "Pečeno")]
		Slano_Peceno,
		[Display(Name = "Roštilj")]
		Slano_Rostilj,
		[Display(Name = "Slano")]
		Slano_Ostalo,
		[Display(Name = "Torta")]
		Slatko_Torta,
		[Display(Name = "Kolač")]
		Slatko_Kolac,
		[Display(Name = "Slatko")]
		Slatko_Ostalo

    }
	public class Recept
	{
        #region Properties

	
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public String id { get; set; }

        [Required]
		[StringLength(maximumLength:30,MinimumLength =3,
			ErrorMessage ="Naziv recepta mora sadržavati minimalno 3, a najviše 30 karaktera!")]
		[Display(Name ="Naziv")]
		public string naziv { get; set; }

		[Required]
		[Display(Name = "Vrijeme pripreme")]
		public int vrijemePripreme { get; set; }

		[Required]
		[EnumDataType(typeof(NacionalnoJelo))]
		[Display(Name = "Kuhinja")]
		public NacionalnoJelo nacionalnoJelo { get; set; }

		[Required]
		[EnumDataType(typeof(VrstaJela))]
		[Display(Name = "Vrsta jela")]
		public VrstaJela vrstaJela { get; set; }

		[Required]
		[Display(Name = "Vegansko")]
		public Boolean vegansko { get; set; }

		[Required]
		[Display(Name = "Objavio")]
		public Korisnik autor { get; set; }

		[Required]
		[Display(Name = "Nacin pripreme")]
		public NacinPripreme nacinPripreme { get; set; }

		[DataType(DataType.Date)]
		[Display(Name = "Datum objave")]
		public DateTime datum { get; set; }

        [Required]
		[Display(Name = "Ocjene za recept")]
		public List<Ocjena> ocjene { get; set; }

        public string slika { get; set; } 
		public string video { get; set; }


        #endregion

        #region Constructor
        public Recept(string naziv, int vrijemePripreme, NacionalnoJelo nacionalnoJelo,
			VrstaJela vrsta, Korisnik autor, NacinPripreme nacinPripreme)
        {
			this.id = new Guid().ToString();
		
			this.naziv = naziv;
			this.vrijemePripreme = vrijemePripreme;
			this.nacionalnoJelo = nacionalnoJelo;
			this.vrstaJela = vrsta;
			//this.vegansko = vegansko;
			this.autor = autor;
			this.nacinPripreme = nacinPripreme;
			datum = DateTime.Now;
			ocjene = new List<Ocjena>();
        }

		public Recept() { }

		#endregion

		#region Metode
        public static List<Recept> DajDesetNajboljihRecepata(List<Recept> recepti)
        {
            recepti.Sort((x, y) => y.IzracunajOcjenu().CompareTo(x.IzracunajOcjenu()));

            if (recepti.Count > 10)
            {
                recepti.RemoveRange(9, recepti.Count - 10);
            }

            return recepti;
        }

        public double IzracunajOcjenu()
        {

            if (ocjene.Count == 0)
            {
                return 0;
            }

            double sumaOcjena = 0;

			foreach(Ocjena ocjena in ocjene)
            {
				sumaOcjena += ocjena.vrijednost;
            }

			return sumaOcjena / ocjene.Count;
        }

        //prima listu recepata koju treba filtirati tako da zadovoljava da atribut vrstaJela se nalazi
        //u proslijedjenoj listi i atribut nacionalnoJelo se nalazi u odgovarajucoj listi
        //te da je vrijeme pripreme do proslijedjenog parametra (npr ako je proslijedjeno 50, priprema mora biti <= 50min)
		public static List<Recept> DajRecepte(List<Recept> recepti, List<VrstaJela> vrstaJela,
			List<NacionalnoJelo> nacionalnoJelo, int vrijemePripreme)
        {
			List<Recept> rez = new List<Recept>();

			foreach (Recept recept in recepti)
            {
				if (vrstaJela.Contains(recept.vrstaJela) && nacionalnoJelo.Contains(recept.nacionalnoJelo)
					&& recept.vrijemePripreme <= vrijemePripreme)
				{
                    rez.Add(recept);
				}
            }

			return rez;
        }

        public static List<Recept> FiltrirajReceptePoParametrima(List<Recept> recepti, List<VrstaJela> vrstaJela,
            List<NacionalnoJelo> nacionalnoJelo, int vrijemePripreme)
        {
            List<Recept> filtriraniRecepti = new List<Recept>();

            foreach (Recept recept in recepti)
            {
                if (vrstaJela.Contains(recept.vrstaJela) && nacionalnoJelo.Contains(recept.nacionalnoJelo)
                    && recept.vrijemePripreme <= vrijemePripreme)
                {
                    filtriraniRecepti.Add(recept);
                }
            }

            return filtriraniRecepti;
        }

        //vraca sve recepte koji u nazivu sadrze proslijedjeni string
        public static List<Recept> FiltrirajReceptePoNazivu(List<Recept> recepti, 
			String trazeniNaziv)
        {
			List<Recept> filtriraniReceptiPoNazivu = new List<Recept>();

			foreach (Recept recept in recepti)
            {
				if (recept.naziv.Equals(trazeniNaziv))
                {
                    filtriraniReceptiPoNazivu.Add(recept);
                }
            }

			return filtriraniReceptiPoNazivu;
        } 

		public static List<Recept> FiltrirajRecepteBezOdabranihSastojaka(List<Recept> recepti, 
			List<Sastojak> sastojci)
        {
			List<Recept> filtriraniReceptiBezSastojaka = new List<Recept>();

			foreach (Recept recept in recepti)
            {
                Boolean postojiSastojak = false;

				foreach (Sastojak idSastojka in recept.nacinPripreme.listaSastojaka)
                {
					if (sastojci.Contains(idSastojka))
					{
						postojiSastojak = true;
						break;
					} 
				}

				if (!postojiSastojak) 
				{
					filtriraniReceptiBezSastojaka.Add(recept);
				}
            }

			return filtriraniReceptiBezSastojaka;
		}

	
		
        #endregion
    }
}
