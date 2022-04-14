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

        public DbSet<WebService.Models.Department> Department { get; set; }
    }
}
