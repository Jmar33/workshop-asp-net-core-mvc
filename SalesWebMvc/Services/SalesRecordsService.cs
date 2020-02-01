using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SalesRecordsService
    {
        private readonly SalesWebMvcContext _context; // readonly faz com que a dependência não seja alterada

        public SalesRecordsService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if(minDate.HasValue)
            {
                result = result.Where(x => x.Date > minDate);
            }
            if(maxDate.HasValue)
            {
                result = result.Where(x => x.Date < maxDate);
            }

            return await result
                .Include(x => x.Seller) //comando para fazer o join com a tabela vendedor
                .Include(x => x.Seller.Department) // join com a tabela departamento
                .OrderByDescending(x => x.Date) // ordernar por data
                .ToListAsync(); // retorna o objeto em forma de lista
                
                           
        }

    }
}
