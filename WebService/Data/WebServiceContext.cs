using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebService.Models;

namespace WebService.Data // essa classe foi criada automaticamente quando foi criado o new Scaffolded Item
{
    public class WebServiceContext : DbContext 
    {
        public WebServiceContext (DbContextOptions<WebServiceContext> options) // criado automaticamente
            : base(options)
        {
        }

        public DbSet<Department> Department { get; set; } // caso der algum erro, acrescentar WebService.Models.Department // esse public foi criado automaticamente
        public DbSet<Seller> Seller { get; set; } // para que os Models criados possam ser reconhecidos no meu banco de dados //
        public DbSet<SalesRecord> SalesRecord { get; set; } // para que os Models criados possam ser reconhecidos no meu banco de dados //
    }
}
