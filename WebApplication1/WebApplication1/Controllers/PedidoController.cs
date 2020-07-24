using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApplication1.Entities;
using WebApplication1.Repository;
namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IClienteRepository _clienteRepository;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IPedidoItemRepository _pedidoItemRepository;
        private readonly IPizzaRepository _pizzaRepository;
        private readonly ISubItemRepository _subItemRepository;

        public PedidoController(IConfiguration config,
                                IClienteRepository clienteRepository,
                                IPedidoRepository pedidoRepository,
                                IPedidoItemRepository pedidoItemRepository,
                                IPizzaRepository pizzaRepository,
                                ISubItemRepository subItemRepository)
        {
            _configuration = config;
            _clienteRepository = clienteRepository;
            _pedidoRepository = pedidoRepository;
            _pedidoItemRepository = pedidoItemRepository;
            _pizzaRepository = pizzaRepository;
            _subItemRepository = subItemRepository;
        }

        [HttpPost]
        public ActionResult<Pedido> Cadastrar(Pedido model)
        {
            try
            {
                if (model.Itens == null ||model.Itens.Count == 0 || model.Itens.Count > 10)
                {
                    return BadRequest("Um pedido pode ter no mínimo uma pizza e no máximo 10.");
                }

                if (model.CodCliente != 0)
                {
                    var endereco = _clienteRepository.Get(model.CodCliente);
                    if (endereco != null)
                    {
                        model.Bairro_Entrega = endereco.Bairro;
                        model.Cidade_Entrega = endereco.Cidade;
                        model.Complemento_Entrega = endereco.Complemento;
                        model.Estado_Entrega = endereco.Estado;
                        model.Logradouro_Entrega = endereco.Logradouro;
                        model.Numero_Entrega = endereco.Numero;
                        model.NomeCliente = endereco.Nome;
                        model.Email = endereco.Email;
                        model.Telefone = endereco.Telefone;
                    }
                    else
                    {
                        return BadRequest("O cliente informado, não foi encontrado;");
                    }

                }

                #region calcula Preço
                decimal valorTotal = 0;
                foreach (var item in model.Itens)
                {
                    if (item.Pizza == null || item.Pizza.Count ==0)
                    {
                        return BadRequest("Nenhuma pizza foi adicionada!");
                    }
                    if ( item.Pizza.Count>2 )
                    {
                        return BadRequest("Cada pizza deve ter no mínimo dois sabores!");
                    }
                    foreach (var subItem in item.Pizza)
                    {
                        var pizza = _pizzaRepository.Get(subItem.CodPizza);
                        subItem.Nome = pizza.Nome;
                        if (item.Pizza.Count == 1)
                        {
                            valorTotal += pizza.Preco;
                            subItem.Preco = pizza.Preco;
                        }
                        else
                        {
                            var valorItem = (pizza.Preco / 2);
                            valorTotal += valorItem;
                            subItem.Preco = valorItem;
                        }
                    }
                }
                model.PrecoTotal = valorTotal;

                #endregion
                model.CodPedido = _pedidoRepository.Add(model).CodPedido;
                foreach (var item in model.Itens)
                {
                    item.CodPedido = model.CodPedido;
                    item.CodPedidoItem = _pedidoItemRepository.Add(item).CodPedidoItem;

                    foreach (var subItem in item.Pizza)
                    {
                        subItem.CodPedidoItem = item.CodPedidoItem;
                        
                        subItem.CodSubItem = _subItemRepository.Add(subItem).CodSubItem;
                    }
                }

                return Ok(model);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Pedido> Get(int id)
        {
            try
            {
                var model = _pedidoRepository.Get(id);

                model.Itens = _pedidoItemRepository.GetAll(model.CodPedido);

                foreach(var item in model.Itens)
                {
                    item.Pizza = _subItemRepository.GetAll(item.CodPedidoItem);
                }

                return Ok(model);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

        }

        [HttpGet("GetPedidoClienteSemCadastro/{name}")]
        public ActionResult<List<Pedido>> GetPedidoClienteSemCadastro(string name)
        {
            try
            {
                var listModel = _pedidoRepository.GetAll(name);

                foreach (var model in listModel)
                {
                    model.Itens = _pedidoItemRepository.GetAll(model.CodPedido);

                    foreach (var item in model.Itens)
                    {
                        item.Pizza = _subItemRepository.GetAll(item.CodPedidoItem);
                    }
                }

                return Ok(listModel);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

        }

        [HttpGet("GetPedidoCliente/{id}")]
        public ActionResult<List<Pedido>> GetPedidoCliente(int id)
        {
            try
            {
                var listModel = _pedidoRepository.GetAllCliente(id);

                foreach (var model in listModel)
                {
                    model.Itens = _pedidoItemRepository.GetAll(model.CodPedido);

                    foreach (var item in model.Itens)
                    {
                        item.Pizza = _subItemRepository.GetAll(item.CodPedidoItem);
                    }
                }

                return Ok(listModel);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

        }

    }
}