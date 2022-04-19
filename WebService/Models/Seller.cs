using System;
using System.Collections.Generic;
using System.Linq;

namespace WebService.Models
{
    public class Seller
    {
        public  int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public double BaseSalary { get; set; }
        public Department Department { get; set; } //pq lá no esquema do programa diz que o seller possui 1 Departamento //
        public int DepartmentId { get; set; } // assim estamos avisando ao Entity Framework que esse Id vai ter q existir, uma vez que o tipo int não pode ser nulo. No caso quando for criado um novo seller lá no site //
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>(); // associação Sales com SalesRecord igual o esquema do programa //
    
        public Seller()
        {
        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount); // cálculo total de vendas de um vendedor em um determinado intervalod e datas //
        }
    }
}
