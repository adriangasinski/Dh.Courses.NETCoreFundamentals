using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NoweM.Core;
using NoweM.Data;

namespace NoweM
{
    public class EditModel : PageModel
    {
        private readonly IHouseData houseData;
        private readonly IHtmlHelper htmlHelper;

        [BindProperty]
        public House House { get; set; }
        public IEnumerable<SelectListItem> HouseTypes { get; set; }
        public EditModel(IHouseData houseData, IHtmlHelper htmlHelper)
        {
            this.houseData = houseData;
            this.htmlHelper = htmlHelper;
        }
        public IActionResult OnGet(int? houseID)
        {
            if (houseID.HasValue)
            {
                House = houseData.GetHouseById(houseID.Value);
            }
            else
            {
                House = new House();
            }
            HouseTypes = htmlHelper.GetEnumSelectList<HouseType>();
            if(House == null)
            {
                RedirectToPage("./NotFound");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                HouseTypes = htmlHelper.GetEnumSelectList<HouseType>();
                return Page();
            }
            
            if (House.Id > 0)
            {
                House = houseData.Update(House);                
            } 
            else
            {
                houseData.Add(House);
            }

            houseData.Commit();
            TempData["Message"] = "House data saved.";
            return RedirectToPage("./Detail", new { houseId = House.Id });
                                                  
        }
    }
}