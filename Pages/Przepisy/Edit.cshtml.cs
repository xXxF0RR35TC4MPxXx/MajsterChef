using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MajsterChef.Data;
using MajsterChef.Models;
using Microsoft.AspNetCore.Authorization;

namespace MajsterChef.Pages.Przepisy
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly PrzepisContext _context;

        public EditModel(PrzepisContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Przepis).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrzepisExists(Przepis.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PrzepisExists(int id)
        {
            return _context.Przepis.Any(e => e.ID == id);
        }
    }
}
