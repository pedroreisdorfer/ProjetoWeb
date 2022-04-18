using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebService.Models;

namespace WebService.Data
{
    public class WebServiceContext : DbContext
    {
        public WebServiceContext (DbContextOptions<WebServiceContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Department { get; set; } // caso der algum erro, acrescentar WebService.Models.Department //
        public DbSet<Seller> Seller { get; set; } // para que os Models criados possam ser reconhecidos no meu banco de dados //
        public DbSet<SalesRecord> SalesRecord { get; set; } // para que os Models criados possam ser reconhecidos no meu banco de dados //
    }
}
