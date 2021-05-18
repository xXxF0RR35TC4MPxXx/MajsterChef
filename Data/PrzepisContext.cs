using MajsterChef.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MajsterChef.Data
{
    public class PrzepisContext:DbContext
    {
        public PrzepisContext(DbContextOptions<PrzepisContext> options) : base(options) { }
        public DbSet<Przepis> Przepis { get; set; }
    }
}
