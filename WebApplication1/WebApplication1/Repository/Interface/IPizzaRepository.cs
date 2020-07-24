using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Entities;

namespace WebApplication1.Repository
{
    public interface IPizzaRepository
    {
        int Add(Pizza model);
        List<Pizza> GetPizza();
        Pizza Get(int id);
        int Edit(Pizza model);
        int Delete(int id);
    }
}
