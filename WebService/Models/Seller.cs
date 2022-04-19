using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // para uso do [Display] //
using System.Linq;

namespace WebService.Models
{
    public class Seller
    {
        public  int Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)] // com essa funçãozinha, configura para o email já ser um link para vc mandar email para essa pessoas através do seu email padrão //
        public string Email { get; set; }
        [Display(Name = "Birth Date")] // customização de uma label, ou seja, posso customizar os rótulos lá na minha tela, no display //
        [DataType(DataType.Date)] // até então dentro de Create New em Sellers no site, estava sendo pedido o horário no Birth Date. Agora nos configuramos para só pedir a data //
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")] // assim fica formatado para para receber número que for digitado pelo usuário e depois do dois ponto a forma de data que estamos acostumados. Antes estava mês antes do dia //
        public DateTime BirthDate { get; set; }
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")] // assim estou formatando para ter duas casas decimais depois do ponto. Zero indica o valor do salario que será preenchido automaticamente
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
