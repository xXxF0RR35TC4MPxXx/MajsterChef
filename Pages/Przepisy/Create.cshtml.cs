using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MajsterChef.Data;
using MajsterChef.Models;
using Microsoft.AspNetCore.Authorization;

namespace MajsterChef.Pages.Przepisy
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly PrzepisContext _context;

        public CreateModel(PrzepisContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Przepis Przepis { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (!string.IsNullOrEmpty(User.Identity.Name))
                Przepis.Owner = User.Identity.Name;
            else Przepis.Owner = "Anonymous";
            Przepis.Data_publikacji = DateTime.Now;
            _context.Przepis.Add(Przepis);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
