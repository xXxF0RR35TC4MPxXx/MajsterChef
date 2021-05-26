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
        //public IList<Favourites> Przepis { get; set; }
        public IList<Favourites> Favourites { get; set; }
        //public IQueryable<Przepis> PrzepisyIQ { get;set; }
        public string UserName(string name)
        {
            if (!String.IsNullOrEmpty(name))
                return name.Split('@')[0];
            else return "Anonymous";
        }

        public async Task OnGetAsync()
        {
            //PROBLEM - NIE PRZEKAZUJE PRZEPISU! Jest Favourite.Id_usera, ale Przepis jest NULLem
            IQueryable<Favourites> FavouritesIQ = from f in _context.Favourites select f;
            //weź wszystkie istniejące przepisy
            //PrzepisyIQ = from s in _context.Przepis select s;

            //weź wszystkie polubienia przez użytkowników

            //weź tylko te polubienia, które należą do mnie
            FavouritesIQ = FavouritesIQ.Where(f => f.Id_usera == User.Identity.Name);

            //weź te przepisy, których id równe jest id polubień
            //PrzepisyIQ = PrzepisyIQ.Where(s => s.ID.Equals(FavouritesIQ.Where(f => f.Id_wpisu == s.ID)));
            //Przepis = await _context.Przepis.Where().ToListAsync();
            Favourites = await FavouritesIQ.AsNoTracking().OrderByDescending(u => u.Przepis.Data_publikacji).ToListAsync();
        }
    }
}
