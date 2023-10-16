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
    public class Utilizador
    {
        public int userId { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public int tipoCliente { get; set; }
        public bool verificado { get; set; }
        public bool revenda { get; set; }
        public string role { get; set; }
    }
    public partial class bo_editar_user : System.Web.UI.Page
    {
        public static int userId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Request.QueryString["userId"] != null)
                {

                    userId = Convert.ToInt32(Request.QueryString["userId"]);
                    Utilizador utilizador = GetUserDetails(userId);

                    if (utilizador != null)
                    {
                        tb_nome.Text = utilizador.nome;
                        tb_email.Text = utilizador.email;
                        tb_username.Text = utilizador.username;
                        ddl_tipo_cliente.SelectedValue = utilizador.tipoCliente.ToString();
                        UpdatePanelContent();

                        lbl_verificado.Text = utilizador.verificado.ToString();
                        ddl_role.SelectedValue = utilizador.role.ToString();

                    }
                    else
                    {
                        lbl_erro.Text = "Utilizador não encontrado!";
                        lbl_erro.ForeColor = System.Drawing.Color.Red;
                        btn_editar.Enabled = false;
                    }
                }
                else
                {
                    lbl_erro.Text = "ID do utilizador nao fornecido!";
                    btn_editar.Enabled = false;
                    lbl_erro.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        private Utilizador GetUserDetails(int productId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ConnectionString;
            Utilizador utilizador = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                SELECT
    u.id_user,
    u.nome,
    u.email,
    u.username,
    u.tipo_cliente,
    u.verificado,
    u.revenda_verificado,
    u.perfil
FROM utilizadores u
                WHERE u.id_user = @userId";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        utilizador = new Utilizador
                        {
                            userId = Convert.ToInt32(reader["id_user"]),
                            nome = reader["nome"].ToString(),
                            email = reader["email"].ToString(),
                            username = reader["username"].ToString(),
                            tipoCliente = Convert.ToInt32(reader["tipo_cliente"]),
                            verificado = Convert.ToBoolean(reader["verificado"]),
                            revenda = Convert.ToBoolean(reader["revenda_verificado"]),
                            role = reader["perfil"].ToString(),
                        };
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    lbl_erro.Text = ex.Message;
                }
            }

            return utilizador;
        }

        protected void btn_editar_Click(object sender, EventArgs e)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand();
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "editar_user";

            myCommand.Connection = myConn;

            myCommand.Parameters.AddWithValue("@userId", userId);
            myCommand.Parameters.AddWithValue("@nome", tb_nome.Text);
            myCommand.Parameters.AddWithValue("@email", tb_email.Text);
            myCommand.Parameters.AddWithValue("@username", tb_username.Text);
            myCommand.Parameters.AddWithValue("@tipoCliente", ddl_tipo_cliente.SelectedValue);
            myCommand.Parameters.AddWithValue("@role", ddl_role.SelectedValue);


            myConn.Open();
            myCommand.ExecuteNonQuery();

            lbl_erro.Text = "Cliente atualizado com sucesso!";
            lbl_erro.ForeColor = System.Drawing.Color.Green;

            myConn.Close();
        }



        protected void btn_aceitar_Click(object sender, EventArgs e)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand();
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "ativar_deny_revendedor";

            myCommand.Connection = myConn;

            myCommand.Parameters.AddWithValue("@idUser", userId);
            myCommand.Parameters.AddWithValue("@decisao", "true");

            SqlParameter valor = new SqlParameter();
            valor.ParameterName = "@retorno";
            valor.Direction = ParameterDirection.Output;
            valor.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(valor);

            myConn.Open();
            myCommand.ExecuteNonQuery();
            myConn.Close();

            lbl_erro.Enabled = true;
            lbl_erro.Visible = true;
            lbl_erro.Text = "Revendedor Confirmado!";
            lbl_erro.ForeColor = Color.Green;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "RefreshScript", "setTimeout(function(){ location.reload(); }, 2000);", true);


        }

        protected void btn_recusar_Click(object sender, EventArgs e)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand();
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "ativar_deny_revendedor";

            myCommand.Connection = myConn;

            myCommand.Parameters.AddWithValue("@idUser", userId);
            myCommand.Parameters.AddWithValue("@decisao", "false");

            SqlParameter valor = new SqlParameter();
            valor.ParameterName = "@retorno";
            valor.Direction = ParameterDirection.Output;
            valor.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(valor);

            myConn.Open();
            myCommand.ExecuteNonQuery();
            myConn.Close();

            lbl_erro.Enabled = true;
            lbl_erro.Visible = true;
            lbl_erro.Text = "Revendedor Negado!";
            lbl_erro.ForeColor = Color.Red;

            Response.Redirect(Request.RawUrl);

        }

        protected void ddl_tipo_cliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePanelContent();
        }

        private void UpdatePanelContent()
        {
            if (ddl_tipo_cliente.SelectedValue == "1")
            {
                // Hide or modify the content as needed
                panelContent.Visible = false;
            }
            else if (ddl_tipo_cliente.SelectedValue == "2")
            {
                // Show or modify the content as needed
                panelContent.Visible = true;
            }
        }
    }
}