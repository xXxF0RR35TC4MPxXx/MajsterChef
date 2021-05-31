using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MajsterChef.Data;
using MajsterChef.Models;

namespace MajsterChef.Pages.Profile
{
    public class DeleteModel : PageModel
    {
        private readonly MajsterChef.Data.PrzepisContext _context;

        public DeleteModel(MajsterChef.Data.PrzepisContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Przepis Przepis { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Przepis = await _context.Przepis.FirstOrDefaultAsync(m => m.ID == id);

            if (Przepis == null)
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

            Przepis = await _context.Przepis.FindAsync(id);

            if (Przepis != null)
            {
                _context.Przepis.Remove(Przepis);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
