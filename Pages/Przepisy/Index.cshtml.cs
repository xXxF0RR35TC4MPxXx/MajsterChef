﻿using System;
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

        public IList<Przepis> Przepis { get;set; }

        public async Task OnGetAsync()
        {
            Przepis = await _context.Przepis.OrderByDescending(u=>u.Data_publikacji).ToListAsync();
        }
    }
}