using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MajsterChef.Data;
using MajsterChef.Models;

namespace MajsterChef.Pages.Ulubione
{
    public class DeleteModel : PageModel
    {
        private readonly MajsterChef.Data.PrzepisContext _context;

        public DeleteModel(MajsterChef.Data.PrzepisContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Favourites Ulubiony { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ulubiony = await _context.Favourites.FirstOrDefaultAsync(m => m.Id == id);

            if (Ulubiony == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ulubiony = await _context.Favourites.FindAsync(id);

            if (Ulubiony != null)
            {
                _context.Favourites.Remove(Ulubiony);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Index");
        }
    }
}
