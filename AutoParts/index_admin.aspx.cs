using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AutoParts
{
    public partial class index_admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ExibirTotalVendas();
            ContarEncomendas();
            ContarClientes();

        }
        private void ExibirTotalVendas()
        {
            try
            {
                // Conectar ao banco de dados
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ConnectionString))
                {
                    connection.Open();

                    // Consultar o total de vendas na tabela de encomendas
                    string query = "SELECT SUM(total) AS TotalVendas FROM encomendas";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Executar a consulta
                        object result = command.ExecuteScalar();

                        // Verificar se o resultado não é nulo
                        if (result != null)
                        {
                            // Exibir o total de vendas na página
                            h6_TotalVendas.InnerText = $"{result:C}";
                        }
                        else
                        {
                            // Se o resultado for nulo, exibir uma mensagem indicando que não há vendas
                            h6_TotalVendas.InnerText = "Nenhuma venda encontrada.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Lidar com exceções, por exemplo, registrar ou exibir uma mensagem de erro
                h6_TotalVendas.InnerText = $"Erro ao obter o total de vendas: {ex.Message}";
            }
        }

        private void ContarEncomendas()
        {
            try
            {
                // Conectar ao banco de dados
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ConnectionString))
                {
                    connection.Open();

                    // Consultar o número total de encomendas na tabela de encomendas
                    string query = "SELECT COUNT(*) AS TotalEncomendas FROM encomendas";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Executar a consulta
                        object result = command.ExecuteScalar();

                        // Verificar se o resultado não é nulo
                        if (result != null)
                        {
                            // Exibir o número total de encomendas na página
                            h6_TotalEncomendas.InnerText = $"{result} vendas";
                        }
                        else
                        {
                            // Se o resultado for nulo, exibir uma mensagem indicando que não há encomendas
                            h6_TotalEncomendas.InnerText = "Nenhuma encomenda encontrada.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Lidar com exceções, por exemplo, registrar ou exibir uma mensagem de erro
                h6_TotalEncomendas.InnerText = $"Erro ao obter o total de encomendas: {ex.Message}";
            }
        }


        private void ContarClientes()
        {
            try
            {
                // Conectar ao banco de dados
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ConnectionString))
                {
                    connection.Open();

                    // Consultar o número total de encomendas na tabela de encomendas
                    string query = "SELECT COUNT(*) AS TotalClientes FROM utilizadores";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Executar a consulta
                        object result = command.ExecuteScalar();

                        // Verificar se o resultado não é nulo
                        if (result != null)
                        {
                            // Exibir o número total de encomendas na página
                            h6_TotalClientes.InnerText = $"{result} clientes";
                        }
                        else
                        {
                            // Se o resultado for nulo, exibir uma mensagem indicando que não há encomendas
                            h6_TotalClientes.InnerText = "Nenhum cliente encontrado.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Lidar com exceções, por exemplo, registrar ou exibir uma mensagem de erro
                h6_TotalClientes.InnerText = $"Erro ao obter o total de clientes: {ex.Message}";
            }
        }


    }
}