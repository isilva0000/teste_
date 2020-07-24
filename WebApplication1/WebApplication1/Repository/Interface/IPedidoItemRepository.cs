using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Entities;

namespace WebApplication1.Repository
{
    public interface IPedidoItemRepository
    {
        PedidoItem Add(PedidoItem model);
        List<PedidoItem> GetAll(int id);
        PedidoItem Get(int id);
        PedidoItem Edit(PedidoItem model);
        int Delete(int id);
    }
}
