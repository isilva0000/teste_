﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Entities
{
    public class SubItem
    {
        public int CodSubItem { get; set; }
        public int CodPedidoItem { get; set; }
        public int CodPizza { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string Observacao { get; set; }
        
    }
}
