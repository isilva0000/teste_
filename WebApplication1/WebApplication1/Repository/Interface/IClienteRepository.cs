using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Entities;

namespace WebApplication1.Repository
{
    public interface IClienteRepository
    {
        Cliente Add(Cliente model);
        List<Cliente> GetAll();
        Cliente Get(int id);
        Cliente Edit(Cliente model);
       
    }
}
