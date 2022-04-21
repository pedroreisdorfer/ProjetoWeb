using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Services;

namespace WebService.Controllers
{
    public class SalesRecordsController : Controller // a pasta criada na Views precisa ter o mesmo nome, no caso: SalesRecords
    {
        private readonly SalesRecordService _salesRecordService; // criação de dependência //

        public SalesRecordsController(SalesRecordService salesRecordService)
        {
            _salesRecordService = salesRecordService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue) // teste se minha data mínima possui um valor.No caso, "Se não possuir um valor mínimo:
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1); // recebe uma data, qual? dia primeiro de janeiro do ano atual
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd"); // passando esses dados para minha View por meio da ViewData
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            var result = await _salesRecordService.FindByDateAsync(minDate, maxDate);
            return View(result);
        }

        public async Task<IActionResult> GroupingSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue) // teste se minha data mínima possui um valor.No caso, "Se não possuir um valor mínimo:
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1); // recebe uma data, qual? dia primeiro de janeiro do ano atual
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd"); // passando esses dados para minha View por meio da ViewData
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            var result = await _salesRecordService.FindByDateGroupingAsync(minDate, maxDate);
            return View(result);
        }
    }
}
