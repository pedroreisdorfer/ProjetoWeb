using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models.ViewModels
{
    public class SellerFormViewModel
    {
        // quais os dados para uma tela de cadastro de vendedor? //
        public Seller Seller { get; set; }
        public ICollection<Department> Departments { get; set; }
    }
}
