﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Models;
using WebService.Services;

namespace WebService.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService; // criada dependência para o SellerService //
        
        public SellersController(SellerService sellerService) // construtor para injeção de dependência //
        {
            _sellerService = sellerService;
        }

        public IActionResult Index() //index da pasta sellers da pasta Views //
        {
            var list = _sellerService.FindAll(); // operação que retorna lista de seller. Lista criada em SellerService //
            return View(list); // passo então essa lista como argumento no método View, gerando um IActionresult contendo essa lista
        }

        public IActionResult Create() // criando a ação chamada create
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // para prevenir que a aplicação sofra ataques CSFS: équando alguém aproveita sua sessão de autenticação e envia dados maliciosos, aproveitando sua atenticação 
        public IActionResult Create(Seller seller) // para inserir no banco de dados
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index)); // redirecionar minha requisição para a ação Index
        }
    }
}