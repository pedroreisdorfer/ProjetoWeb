using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Models;
using WebService.Services;
using WebService.Models.ViewModels;

namespace WebService.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService; // criada dependência para o SellerService //
        private readonly DepartmentService _departmentService; // criando dependência para DepartmentService
        
        public SellersController(SellerService sellerService, DepartmentService departmentService) // construtor para injeção de dependência //
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index() //index da pasta sellers da pasta Views //
        {
            var list = _sellerService.FindAll(); // operação que retorna lista de seller. Lista criada em SellerService //
            return View(list); // passo então essa lista como argumento no método View, gerando um IActionresult contendo essa lista
        }

        public IActionResult Create() // criando a ação chamada create. Método que abre formulário para cadastro do vendedor //
        {
            var departments = _departmentService.FindAll(); // busca no banco de dados os departamentos //
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // para prevenir que a aplicação sofra ataques CSFS: équando alguém aproveita sua sessão de autenticação e envia dados maliciosos, aproveitando sua atenticação 
        public IActionResult Create(Seller seller) // para inserir no banco de dados
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index)); // redirecionar minha requisição para a ação Index
        }

        public IActionResult Delete(int? id) // o int é opcional, por isso o ponto de interrogação //
        {
            if(id == null)
            {
                return NotFound(); // se o id foi nulo, siginifica que a requisição foi feita de uma forma indevida
            }

            var obj = _sellerService.FindById(id.Value); // quem é o objeto que to mandando deletar. FindById busca no banco de dados. .Value pq o int é opcional //
            if (obj == null)
            {
                return NotFound();

            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
