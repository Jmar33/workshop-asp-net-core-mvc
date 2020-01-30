using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]// torna o campo obrigatório
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size shold between {2} and {1}")] // define qual deve ser o tamanho do campo
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        [DataType(DataType.EmailAddress)] // transforma um texto em um link para endereço de email
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Birth Date")] //annotation que serve para dar um apelido ao campo
        [DataType(DataType.Date)] // transforma o tipo de dados em data
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Range(100.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")] // define um intervalo de valores válidos
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")] //mostra o valor com duas casas decimais
        public double BaseSalary { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

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

        public double TotalSales(DateTime inicial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= inicial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}
