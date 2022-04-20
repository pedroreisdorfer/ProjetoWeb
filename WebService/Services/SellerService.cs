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

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync(); // returna lista com todos os vendedores do banco de dados.
                                             // acessa minha fonte de dados relacionada a tabela de vendedores e converte para uma lista //
        }

        public async Task InsertAsync(Seller obj) // método para inserir um novo vendedor no banco de dados // para ser assíncrono, o void passa a ser Task
        {
            _context.Add(obj); // acessa banco de dados com _context e adiciona o objeto.
            await _context.SaveChangesAsync();
        } // tornando operação assíncrona: async, sufixo async em Insert, await e mudando SaveChanges para SaveChangesAsync //

        public async Task<Seller> FindByIdAsync(int id) // retorna o vendedor de possui esse id // mudanças para assíncrono
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id); // include precisa ser com using Microsoft.EntityFrameworkCore; // faz  join e busca o departamento //
        } // no Include a gente coloca o que queremos incluir através de uma expressão lambda. Com isso, aparece nome Department lá em Details do site //

        public async Task RemoveAsync(int id)
        {
            var obj = await _context.Seller.FindAsync(id);
            _context.Seller.Remove(obj); // removo o obj do DbSet.
            await _context.SaveChangesAsync(); // para o Entity Framework efetivar a alteração lá no banco de dados //
        }

        public async Task UpDateAsync(Seller obj) // atualizando objeto do tipo Seller
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id); // mudança devido transformação para assíncrono
            if (!hasAny)                                                       // teste se tem algum(any)  id  desse obj no banco de dados. Como estou atualizando, o id desse objeto tem que existir. Se NÃO (!) existir, lança uma exceção //
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
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
