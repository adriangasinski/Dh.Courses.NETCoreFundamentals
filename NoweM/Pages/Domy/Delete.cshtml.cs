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
    public class DeleteModel : PageModel
    {
        private readonly IHouseData houseData;
        public House House { get; set; }

        public DeleteModel(IHouseData houseData)
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
        public IActionResult OnPost(int houseId)
        {
            var house = houseData.Delete(houseId);
            houseData.Commit();

            if (house == null)
            {
                return RedirectToPage("./NotFound");
            }
            TempData["Message"] = $"Usunięto {house.Address}";
            return RedirectToPage("./List");

        }
    }
}