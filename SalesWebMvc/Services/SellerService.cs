using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context; // readonly faz com que a dependência não seja alterada

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Sellers.ToListAsync();
        }

        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);
             await _context.SaveChangesAsync(); //Somente o método SaveChanges acessa de fato o banco por isso recebe o sufixo e a palavra await
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            return await  _context.Sellers.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task Remove(int id)
        {
            var obj = await _context.Sellers.FindAsync(id);
            _context.Sellers.Remove(obj);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Seller obj)
        {
            bool hasAny = await _context.Sellers.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny ) //O comando Any serve para dizer se existe algo no banco de dados que corresponda ao paramêtro passado
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException e) // Caso haja um erro de concorrência ao tentar atualizar o banco
            {
                throw new DbConcurrencyException(e.Message); //Nesse caso estamos lançando uma exceção da camada de serviços quando é disparado uma execeção da camada de accesso
                                                             // ao banco para manter as camadas segregadas
            }




        }

    }
}
