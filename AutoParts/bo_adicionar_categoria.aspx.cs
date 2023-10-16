using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace AutoParts
{
    public partial class bo_adicionar_categoria : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_adicioar_categoria_Click(object sender, EventArgs e)
        {

            try
            {
                SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ConnectionString);

                SqlCommand myCommand = new SqlCommand();
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "adicionar_categoria";

                myCommand.Connection = myConn;

                string input = tb_nome.Text.ToLower(); // Convert to lowercase
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo; // You can change "en-US" to the appropriate culture if needed
                string capitalizedInput = textInfo.ToTitleCase(input);
                myCommand.Parameters.AddWithValue("@nome", capitalizedInput);

                SqlParameter valor = new SqlParameter();
                valor.ParameterName = "@retorno";
                valor.Direction = ParameterDirection.Output;
                valor.SqlDbType = SqlDbType.Int;
                myCommand.Parameters.Add(valor);

                myConn.Open();
                myCommand.ExecuteNonQuery();

                int resposta = Convert.ToInt32(myCommand.Parameters["@retorno"].Value);

                if (resposta == 0)
                {
                    lbl_erro.Text = "Esta categoria já existe";
                    lbl_erro.ForeColor = System.Drawing.Color.Red;
                }
                else if (resposta == 1)
                {
                    lbl_erro.Text = "Categoria adicionada com sucesso";
                    lbl_erro.ForeColor = System.Drawing.Color.Green;
                    //Response.Redirect("bo_produtos.aspx");
                }

                myConn.Close();
            }catch (Exception ex)
            {
                lbl_erro.Text = ex.Message;
            }
            
        }
    }
}