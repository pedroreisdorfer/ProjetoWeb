using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Data;
using WebService.Models;
using Microsoft.EntityFrameworkCore;
using WebService.Services.Exceptions;

namespace WebService.Services
{
    public class SellerService
    {
        private readonly WebServiceContext _context; // criando assim uma dependência do nosso DbContext //

        public SellerService(WebServiceContext context) // construtor para que injeção de dependência possa ocorrer //
        {
            _context = context; //ou seja, agora quando for usar SellerService, é necessário chamar  _context, que irá administrar os objetos com o banco de dados //
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList(); // returna lista com todos os vendedores do banco de dados.
                                             // acessa minha fonte de dados relacionada a tabela de vendedores e converte para uma lista //
        }

        public void Insert(Seller obj) // método para inserir um novo vendedor no banco de dados //
        {
            _context.Add(obj); // acessa banco de dados com _context e adiciona o objeto.
            _context.SaveChanges();
        }

        public Seller FindById(int id) // retorna o vendedor de possui esse id
        {
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id); // include precisa ser com using Microsoft.EntityFrameworkCore; // faz  join e busca o departamento //
        } // no Include a gente coloca o que queremos incluir através de uma expressão lambda. Com isso, aparece nome Department lá em Details do site //

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj); // removo o obj do DbSet.
            _context.SaveChanges(); // para o Entity Framework efetivar a alteração lá no banco de dados //
        }

        public void UpDate(Seller obj) // atualizando objeto do tipo Seller
        {
            if (!_context.Seller.Any(x => x.Id == obj.Id)) // teste se tem algum(any)  id  desse obj no banco de dados. Como estou atualizando, o id desse objeto tem que existir. Se NÃO (!) existir, lança uma exceção //
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch (DbConcurrencyException e) // captura uma possível concorrência com o banco de dados
            {
                throw new DbConcurrencyException(e.Message); // ai relanço minha exceção personalizada em nível de serviço
            }
            // eu to interceptanto uma exceção de nível de acesso a dados (DbConcurrencyException) e relançando, mas com minha exceção eme nível de serviço //
            // importante para segregar as camadas //

        }

    }
}
