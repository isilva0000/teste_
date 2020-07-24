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
    public class SubItemRepository : ISubItemRepository
    {
        IConfiguration _configuration;

        public SubItemRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnection()
        {
            var connection = _configuration.GetSection("ConnectionStrings").GetSection("PizzaConnection").Value;
            return connection;
        }

        public SubItem Add(SubItem model)
        {
            var connectionString = this.GetConnection();
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = @"INSERT INTO SubItem(  CodPedidoItem
                                                        ,CodPizza
                                                        ,Nome
                                                        ,Preco
                                                        ,Observacao
                                                        
                                                        ) 
                                VALUES(                  @CodPedidoItem
                                                        ,@CodPizza
                                                        ,@Nome
                                                        ,@Preco
                                                        ,@Observacao
                                                        
                                                       ); SELECT CAST(SCOPE_IDENTITY() as INT);";
                    model.CodSubItem = con.QuerySingle<int>(query, model);
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

        public SubItem Edit(SubItem model)
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

        public SubItem Get(int id)
        {
            var connectionString = this.GetConnection();
            SubItem model = new SubItem();

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "SELECT * FROM SubItem WHERE CodPedido =" + id;
                    model = con.Query<SubItem>(query).FirstOrDefault();
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

        public List<SubItem> GetAll(int codPedidoItem)
        {
            var connectionString = this.GetConnection();
            List<SubItem> retorno = new List<SubItem>();

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "SELECT * FROM SubItem where CodPedidoItem = @CodPedidoItem";

                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    dictionary.Add("@CodPedidoItem", codPedidoItem);


                    retorno = con.Query<SubItem>(query, new DynamicParameters(dictionary)).ToList();
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