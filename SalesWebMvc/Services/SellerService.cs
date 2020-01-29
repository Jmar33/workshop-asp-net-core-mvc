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

        public List<Seller> FindAll()
        {
            return _context.Sellers.ToList();
        }

        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindById(int id)
        {
            return _context.Sellers.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            var obj = _context.Sellers.Find(id);
            _context.Sellers.Remove(obj);
            _context.SaveChanges();
        }
        public void Update(Seller obj)
        {
            if (!_context.Sellers.Any(x => x.Id == obj.Id)) //O comando Any serve para dizer se existe algo no banco de dados que corresponda ao paramêtro passado
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                _context.SaveChanges();

            }
            catch (DbUpdateConcurrencyException e) // Caso haja um erro de concorrência ao tentar atualizar o banco
            {
                throw new DbConcurrencyException(e.Message); //Nesse caso estamos lançando uma exceção da camada de serviços quando é disparado uma execeção da camada de accesso
                                                             // ao banco para manter as camadas segregadas
            }




        }

    }
}
