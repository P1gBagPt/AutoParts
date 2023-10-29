using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AutoParts
{
    public partial class bo_encomendas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Fetch and display the list of orders when the page loads.
                BindOrders();
            }
        }


        private void BindOrders()
        {
            // Assuming you have a database connection string.
            string connectionString = ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Fetch the orders from your database, adjust the SQL query as per your database schema.
                string query = "SELECT id_encomenda, codigo, data_encomenda, total, status, metodoPagamento FROM encomendas";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable ordersTable = new DataTable();
                    adapter.Fill(ordersTable);

                    // Bind the orders to the Repeater1 control.
                    Repeater1.DataSource = ordersTable;
                    Repeater1.DataBind();

                }
            }
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ShowProducts")
            {
                int id_encomenda = Convert.ToInt32(e.CommandArgument);
                // Fetch and display the products for the selected order.
                DisplayProducts(id_encomenda, e.Item);
            }
        }


        private void DisplayProducts(int id_encomenda, RepeaterItem item)
        {
            // Assuming you have a database connection string.
            string connectionString = ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Modify the SQL query to select all products in the shopping cart for the specified user and order.


                string query = "SELECT produtos.nome, (produtos.preco * carrinho.quantidade) AS subtotal, marcas.nome AS marca_nome " +
              "FROM carrinho " +
              "INNER JOIN produtos ON carrinho.produtoID = produtos.id_produto " +
              "INNER JOIN marcas ON produtos.marca = marcas.id_marca " +
              "WHERE carrinho.encomenda = @id_encomenda";





                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id_encomenda", id_encomenda);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable productsTable = new DataTable();
                    adapter.Fill(productsTable);

                    // Bind the product details to the Repeater2 control inside the accordion.
                    Repeater Repeater2 = (Repeater)item.FindControl("Repeater2");
                    Repeater2.DataSource = productsTable;
                    Repeater2.DataBind();
                }
            }
        }

       
    }
}