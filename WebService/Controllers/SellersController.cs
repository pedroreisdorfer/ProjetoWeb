using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Models;
using WebService.Services;
using WebService.Models.ViewModels;
using WebService.Services.Exceptions;
using System.Diagnostics;

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
        } // resumo: eu chamei o controlador, o controlador acessou o meu Models, pegou dadona lista e vai encaminhar esses dados para a minha View

        public IActionResult Create() // criando a ação chamada create. Método que abre formulário para cadastro do vendedor //
        {
            var departments = _departmentService.FindAll(); // busca no banco de dados os departamentos //
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        } 

        [HttpPost]
        [ValidateAntiForgeryToken] // para prevenir que a aplicação sofra ataques CSFS: é quando alguém aproveita sua sessão de autenticação e envia dados maliciosos, aproveitando sua atenticação 
        public IActionResult Create(Seller seller) // para inserir no banco de dados
        {
         
            if(!ModelState.IsValid) // para testar se o seller de Seller é válido ou não. NO caso, se o model foi validado //
            {
                var departments = _departmentService.FindAll();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
            // no caso se não for validado, retorna meu objeto seller. E isso vai ser feito até o usuário preencher certinho o formulário //

            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index)); // redirecionar minha requisição para a ação Index
        }

        public IActionResult Delete(int? id) // o int é opcional, por isso o ponto de interrogação //
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" }); // assim eu redireciono para a minha pagina de erro
            }          // para qual ação nós vamos redirecionar? para a Error, ai vai nameof antes. Essa ação Error recebe como argumento, que é a mensagem. Para passar tal argumento, foi instanciado um objeto anônimo //

            var obj = _sellerService.FindById(id.Value); // quem é o objeto que to mandando deletar. FindById busca no banco de dados. .Value pq o int é opcional //
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });

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

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = _sellerService.FindById(id.Value); 
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });

            }

            return View(obj);
        }

        public IActionResult Edit(int? id) // abre a tela para editar o nosso vendedor//
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            // para abrir minha tela de edição, preciso carregar os departamentos para povoar minha caixinha de seleção
            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid) // para testar se o seller de Seller é válido ou não. NO caso, se o model foi validado //
            {
                var departments = _departmentService.FindAll();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
            
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                _sellerService.UpDate(seller);
                return RedirectToAction(nameof(Index));
            }
            catch(ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message}); // e.Message pq aqui se trata dê uma exceção
            }
           
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier // aqui foi feito um macete no Entity Framework para pegar o Id interno da Requisição //
            }; // criação do objeto viewModel
            return View(viewModel);
        } 
    }
}
