using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WhatYouHaveLost.Repository;
using WhatYouHaveLost.Repository.Data;
using WhatYouHaveLost.Services.Interface;

namespace WhatYouHaveLost.Views.Models;

    public class DetailsModel : PageModel
    {
        public News News { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }
    }
