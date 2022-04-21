using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Data;
using WebService.Models;

namespace WebService.Services
{
    public class SalesRecordService
    {
        private readonly WebServiceContext _conetext; // criando assim uma dependência do nosso DbContext //

        public SalesRecordService(WebServiceContext context) // construtor para que injeção de dependência possa ocorrer //
        {
            _conetext = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate) // essa operação vai receber duas datas, data mínima e data máxima. No caso, as datas são opcionais
        {
            var result = from obj in _conetext.SalesRecord select obj; // lógica para encontrar as vendas em um dado intervalo de data // foi criado objeto para construir consultas em cima dele a partir do DbContext do SalesRecord //
            // essa declaração vai pegar o SalesRecord do tipo DbSet e construir um objeto result do tipo IQueryable //
            if (minDate.HasValue) // se informei uma data mínima
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }                        // expressão lambda que expresse minha restrição de data
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date >= maxDate.Value);
            }
            // para executar minha consulta:
            return await result
                .Include(x => x.Seller) // faz o join das tabelas
                .Include(x => x.Seller.Department) // faço o Join com a tabela de departamentos
                .OrderByDescending(x => x.Date) // ordena por data em ordem descrescente
                .ToListAsync();
        }

        public async Task<List< IGrouping<Department, SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate) // essa operação vai receber duas datas, data mínima e data máxima. No caso, as datas são opcionais
        {               // quando existe agrupamento, não retorna uma lista normal, mas uma coleção Igrouping 
            var result = from obj in _conetext.SalesRecord select obj; // lógica para encontrar as vendas em um dado intervalo de data // foi criado objeto para construir consultas em cima dele a partir do DbContext do SalesRecord //
            // essa declaração vai pegar o SalesRecord do tipo DbSet e construir um objeto result do tipo IQueryable //
            if (minDate.HasValue) // se informei uma data mínima
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }                        // expressão lambda que expresse minha restrição de data
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date >= maxDate.Value);
            }
            // para executar minha consulta:
            return await result
                .Include(x => x.Seller) // faz o join das tabelas
                .Include(x => x.Seller.Department) 
                .OrderByDescending(x => x.Date) 
                .GroupBy(x => x.Seller.Department) // para agrupar meus resultados por departamento
                .ToListAsync();
        }

    }
}
