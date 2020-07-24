using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Entities;

namespace WebApplication1.Repository
{
    public interface IPedidoRepository
    {
        Pedido Add(Pedido model);
        List<Pedido> GetAll();
        List<Pedido> GetAll(string name);
        List<Pedido> GetAllCliente(int id);
        Pedido Get(int id);
        Pedido Edit(Pedido model);
        int Delete(int id);
    }
}
