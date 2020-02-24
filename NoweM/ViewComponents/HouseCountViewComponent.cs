using Microsoft.AspNetCore.Mvc;
using NoweM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoweM.ViewComponents
{
    public class HouseCountViewComponent : ViewComponent
    {
        private readonly IHouseData houseData;

        public HouseCountViewComponent(IHouseData houseData)
        {
            this.houseData = houseData;
        }

        public IViewComponentResult Invoke()
        {
            var count = houseData.GetHousesCount();
            return View(count);
        }
    }
}
