using System.Collections.Generic;
using System.Linq;
using System;

namespace WebService.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>(); // isso porque no esquema do programa, diz  com * que são vários vendedores, logo, é preciso de uma coleção.  //
                                                                               // com essa instanciação, foi implementada a associação de Department com Seller //

        public Department()
        {
        }

        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        } // não se coloca as coleções

        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller); // add a minha coleção chamada sellers
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sellers.Sum(seller => seller.TotalSales(initial, final));
        }                       // estou pegando cada vendedor da minha lista Sellers, chamando o Totalsales do vendedor naquele período inicial e final, e então faço uma soma para todos os vendedores do meu departamento //
    }
}
