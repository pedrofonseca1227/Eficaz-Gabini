using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace crudd.Pages.Home
{
    public class Dashboard : PageModel
    {
        public class DashboardModel : PageModel {
            [BindProperty(SupportsGet = true)]
            public int Id { get; set; }

            public void OnGet()
            {
            }
        }

    }
}