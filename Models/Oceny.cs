using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MajsterChef.Models
{
    public class Oceny
    {
        [Key]
        public int Id { get; set; }
        
        public string Id_usera { get; set; }

        
        public int Id_wpisu { get; set; }

        public char Czy_ocenil { get; set; }
    }
}
