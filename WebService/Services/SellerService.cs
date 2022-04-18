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
        public readonly WebServiceContext _conetext; // criando assim uma dependência do nosso DbContext //

        public SellerService (WebServiceContext context) // construtor para que injeção de dependência possa ocorrer //
        {
            _conetext = context;
        }

        public List<Seller> FindAll()
        {
            return _conetext.Seller.ToList(); // returna lista com todos os vendedores do banco de dados.
                             // acessa minha fonte de dados relacionada a tabela de vendedores e converte para uma lista //
        }

        public void Insert(Seller obj) // método para inserir um novo vendedor no banco de dados //
        {
            _conetext.Add(obj);
            _conetext.SaveChanges();
        }

    }
}
