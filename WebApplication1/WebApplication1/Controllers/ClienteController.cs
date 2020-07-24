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
    public class ClienteController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IClienteRepository _clienteRepository;

        public ClienteController(IConfiguration config, IClienteRepository clienteRepository)
        {
            _configuration = config;
            _clienteRepository = clienteRepository;
        }


        [HttpPost]
        public ActionResult<Cliente> Cadastrar(Cliente model)
        {
            try
            {
                return Ok(_clienteRepository.Add(model));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut]
        public ActionResult<Cliente> Atualizar(Cliente model)
        {
            try
            {
                return Ok(_clienteRepository.Edit(model));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Cliente>> Get()
        {
            try
            {
                return Ok( _clienteRepository.GetAll());
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
           
        }


        [HttpGet("{id}")]
        public ActionResult<Cliente> Get(int id)
        {
            try
            {
                return Ok(_clienteRepository.Get(id));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
            
        }
    }
}