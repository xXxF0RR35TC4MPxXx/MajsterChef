using MajsterChef.Data;
using MajsterChef.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MajsterChef
{
    public class IndexModel : PageModel
    {
        private readonly PrzepisContext _context;

        public IndexModel(PrzepisContext context)
        {
            _context = context;
        }
        public string UserName(string name)
        {
            if (!String.IsNullOrEmpty(name))
                return name.Split('@')[0];
            else return "Anonymous";
        }
        public IList<Przepis> Przepis { get; set; }
        public async Task OnGetAsync()
        {
            IQueryable<Przepis> przepisyIQ = from s in _context.Przepis select s;
            Przepis = await przepisyIQ.AsNoTracking().OrderByDescending(u => u.Score).Take(10).ToListAsync();
        }
    }
}
