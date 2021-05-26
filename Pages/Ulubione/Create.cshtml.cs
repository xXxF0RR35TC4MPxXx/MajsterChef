﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MajsterChef.Data;
using MajsterChef.Models;

namespace MajsterChef.Pages.Ulubione
{
    public class CreateModel : PageModel
    {
        private readonly MajsterChef.Data.PrzepisContext _context;

        public CreateModel(MajsterChef.Data.PrzepisContext context)
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

            _context.Przepis.Add(Przepis);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
