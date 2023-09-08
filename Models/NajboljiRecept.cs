using Michelin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Michelin.Util
{
    public class NajboljiRecept
    {

        #region Prototypes
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string id { get; set; }

        public Recept recept { get; set; }

        #endregion



    }
}
