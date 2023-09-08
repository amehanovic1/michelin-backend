using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Michelin.Models { 
	public class NacinPripreme
	{
		#region Properties
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string id { get; set; }

		[Required]
		public List<Sastojak> listaSastojaka { get; set; }

		[Required]
		
		public string opisPripreme { get; set; }

        #endregion

        #region Constructor
		public NacinPripreme(List<Sastojak> sastojci, string opis)
		{
			listaSastojaka = sastojci;
			opisPripreme = opis;
        }

		public NacinPripreme() {
			
			listaSastojaka = new List<Sastojak> ();
			opisPripreme = "";
		}

        #endregion
    }
}
