using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Entities;

namespace WebApplication1.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        IConfiguration _configuration;

        public ClienteRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnection()
        {
            var connection = _configuration.GetSection("ConnectionStrings").GetSection("PizzaConnection").Value;
            return connection;
        }

        public Cliente Add(Cliente model)
        {
            var connectionString = this.GetConnection();
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = @"INSERT INTO Cliente(  Nome
                                                        ,Email
                                                        ,Telefone
                                                        ,Logradouro
                                                        ,Complemento
                                                        ,Numero
                                                        ,Bairro
                                                        ,Cidade
                                                        ,Estado) 
                                VALUES( @Nome
                                        ,@Email
                                        ,@Telefone
                                        ,@Logradouro
                                        ,@Complemento
                                        ,@Numero
                                        ,@Bairro
                                        ,@Cidade
                                        ,@Estado); SELECT CAST(SCOPE_IDENTITY() as INT);";
                    model.CodCliente = con.QuerySingle<int>(query, model);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }

                return model;
            }
        }

        

        public Cliente Edit(Cliente model)
        {
            var connectionString = this.GetConnection();
           
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = @"UPDATE Cliente SET 
                                Nome = @Nome
                                ,Email = @Email
                                ,Telefone = @Telefone
                                ,Logradouro = @Logradouro
                                ,Complemento = @Complemento
                                ,Numero = @Numero
                                ,Bairro = @Bairro
                                ,Cidade = @Cidade
                                ,Estado = @Estado
                                WHERE CodCliente = @CodCliente"; 
                    con.Execute(query, model);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }

                return model;
            }
        }

        public Cliente Get(int id)
        {
            var connectionString = this.GetConnection();
            Cliente model = new Cliente();

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "SELECT * FROM Cliente WHERE CodCliente =" + id;
                    model = con.Query<Cliente>(query).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }

                return model;
            }
        }

        public List<Cliente> GetAll()
        {
            var connectionString = this.GetConnection();
            List<Cliente> retorno = new List<Cliente>();

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "SELECT * FROM Cliente";
                    retorno = con.Query<Cliente>(query).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }

                return retorno;
            }
        }
    }
}