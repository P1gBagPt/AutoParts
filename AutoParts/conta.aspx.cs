using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace AutoParts
{
    public partial class conta : System.Web.UI.Page
    {
        public static int userId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["isLogged"] == null)
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                userId = int.Parse(Session["userId"].ToString());

              

                SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ConnectionString);

                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "user_info";

                cmd.Connection = myConn;

                cmd.Parameters.AddWithValue("@userID", userId);

                SqlParameter retornoNome = new SqlParameter("@retornoNome", SqlDbType.VarChar, 50);
                retornoNome.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retornoNome);

                SqlParameter retornoUser = new SqlParameter("@retornoUser", SqlDbType.VarChar, 20);
                retornoUser.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retornoUser);

                SqlParameter retornoEmail = new SqlParameter("@retornoEmail", SqlDbType.VarChar, 255);
                retornoEmail.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retornoEmail);

                SqlParameter retornoTipo = new SqlParameter("@retornoTipo", SqlDbType.Int);
                retornoTipo.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retornoTipo);

                SqlParameter retornoRevenda = new SqlParameter("@retornoRevenda", SqlDbType.Bit);
                retornoRevenda.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retornoRevenda);


                myConn.Open();
                cmd.ExecuteNonQuery();

                string respostaNome = cmd.Parameters["@retornoNome"].Value.ToString();
                string respostaUser = cmd.Parameters["@retornoUser"].Value.ToString();
                string respostaEmail = cmd.Parameters["@retornoEmail"].Value.ToString();
                int respostaTipo = Convert.ToInt32(cmd.Parameters["@retornoTipo"].Value);
                bool respostaRevenda = Convert.ToBoolean(cmd.Parameters["@retornoRevenda"].Value);

                Console.WriteLine(respostaRevenda);

                myConn.Close();

                tb_nome.Text = respostaNome;
                tb_username.Text = respostaUser;
                tb_email.Text = respostaEmail;

                if(respostaTipo == 2 && respostaRevenda == true)
                {
                    btn_vendedor.Text = "Conta de Vendedor";
                    btn_vendedor.ForeColor = Color.Green;
                    btn_vendedor.Enabled = false;
                }else if (respostaTipo == 2 && respostaRevenda == false)
                {
                    btn_vendedor.Text = "Aguarda Resposta";
                    btn_vendedor.ForeColor = Color.Yellow;
                    btn_vendedor.Enabled = false;
                }

                BindOrders(userId);



            }
        }

        protected void btn_vendedor_Click(object sender, EventArgs e)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ConnectionString);

            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "revenda_pedido";

            cmd.Connection = myConn;

            cmd.Parameters.AddWithValue("@userID", userId);

            myConn.Open();
            cmd.ExecuteNonQuery();
            myConn.Close();

            btn_vendedor.Text = "Aguarda Resposta";
            btn_vendedor.ForeColor = Color.Yellow;
            btn_vendedor.Enabled = false;

        }

        protected void btn_guardar_altera_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ConnectionString);

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "update_user_info";
                    cmd.Connection = myConn;

                    // Retrieve the latest values from the text fields
                    string newNome = tb_nome.Text;
                    string newUsername = tb_username.Text;

                    cmd.Parameters.AddWithValue("@userID", userId);
                    cmd.Parameters.AddWithValue("@nome", newNome);
                    cmd.Parameters.AddWithValue("@username", newUsername);

                    // Add the output parameter
                    SqlParameter rowsAffectedParam = new SqlParameter("@rowsAffected", SqlDbType.Int);
                    rowsAffectedParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(rowsAffectedParam);

                    myConn.Open();
                    cmd.ExecuteNonQuery();

                    // Check the value of the output parameter
                    int rowsAffected = (int)rowsAffectedParam.Value;

                    myConn.Close();

                    if (rowsAffected > 0)
                    {
                        lbl_guardar.Enabled = true;
                        lbl_guardar.Visible = true;
                        lbl_guardar.Text = "Alterações efetuadas com sucesso!";
                        lbl_guardar.ForeColor = Color.Green;
                    }
                    else
                    {
                        lbl_guardar.Enabled = true;
                        lbl_guardar.Visible = true;
                        lbl_guardar.Text = "Nenhuma alteração efetuada. Usuário não encontrado.";
                        lbl_guardar.ForeColor = Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle and log the exception
                lbl_guardar.Enabled = true;
                lbl_guardar.Visible = true;
                lbl_guardar.Text = "Erro ao efetuar alterações: " + ex.Message;
                lbl_guardar.ForeColor = Color.Red;
            }
        }


        private void BindOrders(int userID)
        {
            // Assuming you have a database connection string.
            string connectionString = ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Fetch the orders from your database, adjust the SQL query as per your database schema.
                string query = "SELECT id_encomenda, codigo, data_encomenda, total, status, metodoPagamento FROM encomendas WHERE id_utilizador = @userID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userID", userID);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable ordersTable = new DataTable();
                    adapter.Fill(ordersTable);

                    // Bind the orders to the Repeater1 control.
                    Repeater1.DataSource = ordersTable;
                    Repeater1.DataBind();
                }
            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // Get the order ID from the data item in the Repeater1 control.
                DataRowView orderRow = e.Item.DataItem as DataRowView;
                int orderID = (int)orderRow["id_encomenda"];

                // Find the Repeater2 control inside the current Repeater1 item.
                Repeater Repeater2 = e.Item.FindControl("Repeater2") as Repeater;

                // Call the DisplayProducts method to populate products for the current order.
                DisplayProducts(orderID, userId, Repeater2);
            }
        }


        private void DisplayProducts(int id_encomenda, int userID, Repeater Repeater2)
        {
            // Assuming you have a database connection string.
            string connectionString = ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Modify the SQL query to select all products in the shopping cart for the specified order.
                string query = "SELECT produtos.nome, produtos.imagem, produtos.contenttype, (produtos.preco * carrinho.quantidade) AS subtotal, marcas.nome AS marca_nome " +
                    "FROM carrinho " +
                    "INNER JOIN produtos ON carrinho.produtoID = produtos.id_produto " +
                    "INNER JOIN marcas ON produtos.marca = marcas.id_marca " +
                    "WHERE carrinho.encomenda = @id_encomenda AND carrinho.userID = @userID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id_encomenda", id_encomenda);
                    command.Parameters.AddWithValue("@userID", userID);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable productsTable = new DataTable();
                    adapter.Fill(productsTable);

                    


                    // Bind the product details to the Repeater2 control inside the accordion.
                    Repeater2.DataSource = productsTable;
                    Repeater2.DataBind();
                }
            }
        }

        protected void btn_logout_Click(object sender, EventArgs e)
        {
            Session.Clear();

            Response.Redirect("index.aspx");
        }
    }
}