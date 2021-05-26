﻿using System;
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
    public class DetailsModel : PageModel
    {
        private readonly MajsterChef.Data.PrzepisContext _context;

        public DetailsModel(MajsterChef.Data.PrzepisContext context)
        {
            _context = context;
        }

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
    }
}
