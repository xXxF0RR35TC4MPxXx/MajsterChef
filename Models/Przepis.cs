using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MajsterChef.Models
{
    public class Przepis
    {
        public int ID { get; set; }
        [MaxLength(100)]
        public string Owner { get; set; }
        [Required]
        [MaxLength(100)]
        public string Nazwa { get; set; }
        [Required]
        [MaxLength(2000)]
        public string Opis_wykonania { get; set; }
        public DateTime Data_publikacji { get; set; }
        public ICollection<Skladnik> Lista_skladnikow { get; set; }
    }
}
