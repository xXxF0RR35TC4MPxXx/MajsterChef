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

namespace MajsterChef.Pages.Ulubione
{
    [Authorize]
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
        public IQueryable<Favourites> Ulub { get; set; }
        public int FavID(int? id)
        {
            Ulub = _context.Favourites.Where(m => m.PrzepisID == id && m.Id_usera==User.Identity.Name);
            return Ulub.First().Id;
        }
        public IList<Przepis> Przepisy { get; set; }
        public async Task OnGetAsync()
        {
            IQueryable<Favourites> ulubione = from b in _context.Favourites
                                              where b.Id_usera == User.Identity.Name
                                              select b;
            var ulub = _context.Przepis.Where(c => ulubione.Select(b => b.PrzepisID).Contains(c.ID));
            Przepisy = await ulub.AsNoTracking().OrderByDescending(u => u.Data_publikacji).ToListAsync();
        }
    }
}
