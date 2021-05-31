using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MajsterChef.Data;
using MajsterChef.Models;
using Microsoft.AspNetCore.Authorization;

namespace MajsterChef.Pages.Profile
{
    public class IndexModel : PageModel
    {
        private readonly MajsterChef.Data.PrzepisContext _context;

        public IndexModel(MajsterChef.Data.PrzepisContext context)
        {
            _context = context;
        }
        [BindProperty(SupportsGet = true)]
        public string UserName { get; set; }
        public IList<Przepis> Przepis { get;set; }
        public string NameUser(string name)
        {
            if (!String.IsNullOrEmpty(name))
                return name.Split('@')[0];
            else return "Anonymous";
        }
        public async Task OnGetAsync(string username)
        {
            IQueryable<Przepis> przepisyIQ = from s in _context.Przepis
                                             select s;
            if (!String.IsNullOrEmpty(username))
            {
                przepisyIQ = przepisyIQ.Where(s => s.Owner.Equals(username));
            }
            Przepis = await przepisyIQ.AsNoTracking().OrderByDescending(u => u.Data_publikacji).ToListAsync();
        }
    }
}
