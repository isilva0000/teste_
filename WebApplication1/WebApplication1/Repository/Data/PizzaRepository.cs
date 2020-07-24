using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Entities;
using Dapper;

namespace WebApplication1.Repository
{
    public class PizzaRepository : IPizzaRepository
    {
        IConfiguration _configuration;

        public PizzaRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnection()
        {
            var connection = _configuration.GetSection("ConnectionStrings").GetSection("PizzaConnection").Value;
            return connection;
        }

        public int Add(Pizza model)
        {
            var connectionString = this.GetConnection();
            int count = 0;
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "INSERT INTO Produtos(Nome, Estoque, Preco) VALUES(@Nome, @Estoque, @Preco); SELECT CAST(SCOPE_IDENTITY() as INT);";
                    count = con.QuerySingle<int>(query, model);
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

        public int Delete(int id)
        {
            var connectionString = this.GetConnection();
            var count = 0;

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "DELETE FROM Produtos WHERE ProdutoId =" + id;
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

        public int Edit(Pizza model)
        {
            var connectionString = this.GetConnection();
            var count = 0;

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "UPDATE Produtos SET Nome = @Nome, Estoque = @Estoque, Preco = @Preco WHERE ProdutoId = " + model.CodPizza;
                    count = con.Execute(query, model);
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

        public Pizza Get(int id)
        {
            var connectionString = this.GetConnection();
            Pizza model = new Pizza();

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "SELECT * FROM pizza WHERE CodPizza =" + id;
                    model = con.Query<Pizza>(query).FirstOrDefault();
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

        public List<Pizza> GetPizza()
        {
            var connectionString = this.GetConnection();
            List<Pizza> produtos = new List<Pizza>();

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "SELECT * FROM pizza";
                    produtos = con.Query<Pizza>(query).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }

                return produtos;
            }
        }
    }
}