using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebService.Models;
using WebService.Models.ViewModels;
using ErrorViewModel = WebService.Models.ViewModels.ErrorViewModel;

namespace WebService.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Salles Web MVC App from C# Course."; //ViewData é um dictionary do C#, UMA COLEÇÃO DE [] PARES VALOR //
            ViewData["Aluno"] = "Pedro Vinícius";

            return View(); // quando um método chama return View(), é o que se chama de method builder, é um método auxiliar que retorna um objeto do tipo "IActionResult"
        }                  // o que o framework então vai fazer: vai procurar na pasta Views, na subpasta Home, uma página chamada About.cshtml

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
