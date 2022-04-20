using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; // para uso das operações assíncronas //
using WebService.Data;
using WebService.Models;
using Microsoft.EntityFrameworkCore; // para usar operação ToListAsync()

namespace WebService.Services
{
    public class DepartmentService
    {
        private readonly WebServiceContext _conetext; // criando assim uma dependência do nosso DbContext //

        public DepartmentService(WebServiceContext context) // construtor para que injeção de dependência possa ocorrer //
        {
            _conetext = context;
        }

        public async Task<List<Department>> FindAllAsync() // método para retornar todos os departamentos no formato lista. Sufixo Async operação que passa a ser assíncrona. Não é uma regra, mas é padrão no C#  //
        {          
            return await _conetext.Department.OrderBy(x => x.Name).ToListAsync(); // lembrando que para a operação LINQ ser executada, precisa chamar alguma coisa que provoque a execução dela. No caso, foi chamado o ToList(). Se não for chamada, ela só prepara minha consulta. A operação ToList() é síncrona, ficando bloqueada executando o ToList.
        } // para tornar assíncrona, foi colocado o async e Task<>
        // depois foi colocado também o aviso da chamada assíncrona com await e foi transformado o ToList em ToListAsync

        internal void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
