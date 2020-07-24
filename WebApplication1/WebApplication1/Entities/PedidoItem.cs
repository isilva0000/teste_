using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Entities
{
    public class PedidoItem
    {
        public int CodPedidoItem { get; set; }
        public int CodPedido { get; set; }
        public List<SubItem> Pizza { get; set; }

    }
}
