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
    public class PedidoRepository : IPedidoRepository
    {
        IConfiguration _configuration;

        public PedidoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnection()
        {
            var connection = _configuration.GetSection("ConnectionStrings").GetSection("PizzaConnection").Value;
            return connection;
        }

        public Pedido Add(Pedido model)
        {
            var connectionString = this.GetConnection();

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    model.Data = DateTime.Now;
                    con.Open();
                    var query = @"INSERT INTO Pedido(   CodCliente
                                                        ,NomeCliente
                                                        ,PrecoTotal
                                                        ,Logradouro_Entrega
                                                        ,Complemento_Entrega
                                                        ,Numero_Entrega
                                                        ,Bairro_Entrega
                                                        ,Cidade_Entrega
                                                        ,Estado_Entrega
                                                        ,Data) 
                                VALUES(                 
                                                        @CodCliente
                                                        ,@NomeCliente
                                                        ,@PrecoTotal
                                                        ,@Logradouro_Entrega
                                                        ,@Complemento_Entrega
                                                        ,@Numero_Entrega
                                                        ,@Bairro_Entrega
                                                        ,@Cidade_Entrega
                                                        ,@Estado_Entrega
                                                        ,@Data); SELECT @@identity;";
                    model.CodPedido = con.QuerySingle<int>(query, model);
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

        public Pedido Edit(Pedido model)
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

        public Pedido Get(int id)
        {
            var connectionString = this.GetConnection();
            Pedido model = new Pedido();

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "SELECT * FROM Pedido WHERE CodPedido = @CodPedido";

                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    dictionary.Add("@CodPedido", id);
                   

                     model = con.Query<Pedido>(query, new DynamicParameters(dictionary)).FirstOrDefault();
                    
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

        public List<Pedido> GetAll()
        {
            var connectionString = this.GetConnection();
            List<Pedido> retorno = new List<Pedido>();

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "SELECT * FROM Pedido order by Data desc";
                    retorno = con.Query<Pedido>(query).ToList();
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

        public List<Pedido> GetAll(string name)
        {
            var connectionString = this.GetConnection();
            List<Pedido> retorno = new List<Pedido>();

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "SELECT * FROM Pedido where CodCliente = 0 and NomeCliente = @Nome cccc";
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    dictionary.Add("@Nome", name);


                    retorno = con.Query<Pedido>(query, new DynamicParameters(dictionary)).ToList();
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

        public List<Pedido> GetAllCliente(int id)
        {
            var connectionString = this.GetConnection();
            List<Pedido> retorno = new List<Pedido>();

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "SELECT * FROM Pedido where CodCliente = @CodCliente Order by data desc";
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    dictionary.Add("@CodCliente", id);


                    retorno = con.Query<Pedido>(query, new DynamicParameters(dictionary)).ToList();
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