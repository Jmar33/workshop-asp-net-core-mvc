using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMvcContext _context; // readonly faz com que a dependência não seja alterada

        public DepartmentService(SalesWebMvcContext context)
        {
            _context = context;
        }

        // O sufixo Async é uma recomendação do c# para métodos assíncronos
        // As expressões linq são declaradas mas apenas executadas quando se necessita do valor que ela retorna, no exemplo 
        // abaixo ocorre quando executamos o método ToListAsync
        // Quando usamos um método assíncrono como o ToListAsync devemos usar a palavra await
        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
