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
    public class PedidoItemRepository : IPedidoItemRepository
    {
        IConfiguration _configuration;

        public PedidoItemRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnection()
        {
            var connection = _configuration.GetSection("ConnectionStrings").GetSection("PizzaConnection").Value;
            return connection;
        }

        public PedidoItem Add(PedidoItem model)
        {
            var connectionString = this.GetConnection();
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = @"INSERT INTO PedidoItem(   CodPedido
                                                        ) 
                                VALUES(             @CodPedido
                                                       ); SELECT CAST(SCOPE_IDENTITY() as INT);";
                    model.CodPedidoItem = con.QuerySingle<int>(query, model);
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

        public int Delete(int id)
        {
            var connectionString = this.GetConnection();
            var count = 0;

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "DELETE FROM Pedido WHERE CodPedido =" + id;
                    count = con.Execute(query);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
                return count;
            }
        }

        public PedidoItem Edit(PedidoItem model)
        {
            var connectionString = this.GetConnection();

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = @"UPDATE Pedido SET 
                                Nome = @Nome
                                ,Email = @Email
                                ,Telefone = @Telefone
                                ,Logradouro = @Logradouro
                                ,Complemento = @Complemento
                                ,Numero = @Numero
                                ,Bairro = @Bairro
                                ,Cidade = @Cidade
                                ,Estado = @Estado
                                WHERE CodPedido = @CodPedido";
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

        public PedidoItem Get(int id)
        {
            var connectionString = this.GetConnection();
            PedidoItem model = new PedidoItem();

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "SELECT * FROM PedidoItem WHERE CodPedido =" + id;
                    model = con.Query<PedidoItem>(query).FirstOrDefault();
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

        
        public List<PedidoItem> GetAll(int codPedido)
        {
            var connectionString = this.GetConnection();
            List<PedidoItem> retorno = new List<PedidoItem>();

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "SELECT * FROM PedidoItem where CodPedido = @CodPedido";
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    dictionary.Add("@CodPedido", codPedido);


                    retorno = con.Query<PedidoItem>(query, new DynamicParameters(dictionary)).ToList();

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