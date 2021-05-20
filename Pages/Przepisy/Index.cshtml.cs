using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MajsterChef.Data;
using MajsterChef.Models;

namespace MajsterChef.Pages.Przepisy
{
    public class IndexModel : PageModel
    {
        private readonly MajsterChef.Data.PrzepisContext _context;

        public IndexModel(MajsterChef.Data.PrzepisContext context)
        {
            _context = context;
        }
        public string UserName(string name)
        {
            if (!String.IsNullOrEmpty(name))
                return name.Split('@')[0];
            else return "Anonymous";
        }
        public IList<Przepis> Przepis { get;set; }
        public async Task OnGetAsync(string searchString)
        { 
            IQueryable<Przepis> przepisyIQ = from s in _context.Przepis
                                             select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                przepisyIQ = przepisyIQ.Where(s => s.Owner.Contains(searchString)
                                       || s.Nazwa.Contains(searchString));
            }
            Przepis = await przepisyIQ.AsNoTracking().OrderByDescending(u=>u.Data_publikacji).ToListAsync();
        }
    }
}
