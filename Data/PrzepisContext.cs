using MajsterChef.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MajsterChef.Data
{
    public class PrzepisContext:DbContext
    {
        public PrzepisContext(DbContextOptions<PrzepisContext> options) : base(options) { }
        public PrzepisContext(){ }
        public DbSet<Przepis> Przepis { get; set; }
        public DbSet<Oceny> Oceny { get; set; }
        public DbSet<Favourites> Favourites { get; set; }
    }
}
