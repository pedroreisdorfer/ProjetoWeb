using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Data;
using WebService.Models;

namespace WebService.Services
{
    public class SellerService
    {
        private readonly WebServiceContext _context; // criando assim uma dependência do nosso DbContext //

        public SellerService (WebServiceContext context) // construtor para que injeção de dependência possa ocorrer //
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList(); // returna lista com todos os vendedores do banco de dados.
                             // acessa minha fonte de dados relacionada a tabela de vendedores e converte para uma lista //
        }

        public void Insert(Seller obj) // método para inserir um novo vendedor no banco de dados //
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindById(int id)
        {
            return _context.Seller.FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges(); // para o Entity Framework efetivar a alteração lá no banco de dados //
        }

    }
}
