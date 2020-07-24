using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Entities
{
    public class Pedido
    {
        public int CodPedido { get; set; }
        public int CodCliente { get; set; }
        public string NomeCliente { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public decimal PrecoTotal { get; set; }
        public string Logradouro_Entrega { get; set; }
        public string Complemento_Entrega { get; set; }
        public string Numero_Entrega { get; set; }
        public string Bairro_Entrega { get; set; }
        public string Cidade_Entrega { get; set; }
        public string Estado_Entrega { get; set; }
        public List<PedidoItem> Itens { get; set; }
        public DateTime Data { get; set; }
    }
}
