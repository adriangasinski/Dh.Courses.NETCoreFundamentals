using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NoweM.Core;
using NoweM.Data;

namespace NoweM
{
    public class DetailModel : PageModel
    {
        private readonly IHouseData houseData;

        public House House { get; set; }
        [TempData]
        public string Message { get; set; }

        public DetailModel(IHouseData houseData)
        {            
            this.houseData = houseData;
        }

        public IActionResult OnGet(int houseId)
        {
            House = houseData.GetHouseById(houseId);

            if (House == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }
    }
}