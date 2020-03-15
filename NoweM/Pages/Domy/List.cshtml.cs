using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NoweM.Core;
using NoweM.Data;

namespace NoweM
{
    public class ListaModel : PageModel
    {
        private readonly IConfiguration config;
        private readonly IHouseData houseData;
        private readonly ILogger<ListaModel> logger;

        public string Message { get; set; }
        public string MessageFromConfig { get; set; }
        public IEnumerable<House> Houses { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public ListaModel(IConfiguration config, IHouseData houseData,
            ILogger<ListaModel> logger)
        {
            this.config = config;
            this.houseData = houseData;
            this.logger = logger;
        }
        public void OnGet()
        {
            logger.LogError("Executing ListaModel");
            Message = "Hello world!";
            MessageFromConfig = config["Message"];
            Houses = houseData.GetHousesByAddress(SearchTerm);

        }
    }
}