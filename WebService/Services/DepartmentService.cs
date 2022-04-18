using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Data;
using WebService.Models;

namespace WebService.Services
{
    public class DepartmentService
    {
        private readonly WebServiceContext _conetext; // criando assim uma dependência do nosso DbContext //

        public DepartmentService(WebServiceContext context) // construtor para que injeção de dependência possa ocorrer //
        {
            _conetext = context;
        }

        public List<Department> FindAll() // método para retornar todos os departamentos no formato lista //
        {
            return _conetext.Department.OrderBy(x => x.Name).ToList();
        }
    }
}
