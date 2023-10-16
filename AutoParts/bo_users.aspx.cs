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
    public partial class bo_users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                List<utilizadores> list = new List<utilizadores>();


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
FROM utilizadores u";


                SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ConnectionString);

                SqlCommand cmd = new SqlCommand(query, myConn);

                myConn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var utilizador = new utilizadores();

                    utilizador.userId = dr.GetInt32(0);
                    utilizador.nome = dr.GetString(1);
                    utilizador.email = dr.GetString(2);
                    utilizador.username = dr.GetString(3);
                    utilizador.tipoCliente = dr.GetInt32(4);
                    utilizador.verificado = dr.GetBoolean(5);
                    utilizador.revenda = dr.GetBoolean(6);
                    utilizador.role = dr.GetString(7);

                    list.Add(utilizador);
                }

                myConn.Close();
                Repeater1.DataSource = list;
                Repeater1.DataBind();
            }
            catch(Exception ex)
            {
            }
        }

        public class utilizadores
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

        protected void editar_produto_Command(object sender, CommandEventArgs e)
        {

            if (e.CommandName == "Edit")
            {
                int userId = Convert.ToInt32(e.CommandArgument);
                Response.Redirect($"bo_editar_user.aspx?userId={userId}");
            }
        }
    }
}