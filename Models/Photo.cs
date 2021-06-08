using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MajsterChef.Models
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }
        public int PrzepisID { get; set; }
        [Url]
        public string URL { get; set; }

        //public IFormFile Foto { get; set; }
    }
}
