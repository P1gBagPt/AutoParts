using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AutoParts
{
    public partial class checkout : System.Web.UI.Page
    {

        public static int id_user;
        public static decimal total = 0;
        public static int encomenda_id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["userId"] = 7;

            id_user = Convert.ToInt32(Session["userId"].ToString());

            try
            {
                SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ConnectionString);

                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "total_carrinho";

                cmd.Connection = myConn;

                cmd.Parameters.AddWithValue("@userId", id_user);

                SqlParameter totalRetorno = new SqlParameter("@total", SqlDbType.Float);
                totalRetorno.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(totalRetorno);

                myConn.Open();
                cmd.ExecuteNonQuery();

                // Verifique se o valor de retorno não é DBNull antes de convertê-lo
                total = (cmd.Parameters["@total"].Value != DBNull.Value) ? Convert.ToDecimal(cmd.Parameters["@total"].Value) : 0;

                if (total == 0)
                {
                    total = 0;
                    ltTotal.Text = total.ToString();
                    btn_submeter.Visible = false;
                    btn_submeter.Enabled = false;
                }
                else
                {
                    ltTotal.Text = total.ToString();
                }

                myConn.Close();
            }
            catch (Exception ex)
            {
                ltTotal.Text = ex.Message;
            }


        }




        protected void btn_submeter_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int rand_num = rnd.Next(1, 999999);



            try
            {

                string selectedValue = listGroupRadios.SelectedValue;


                SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ConnectionString);

                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "encomenda_pro";

                cmd.Connection = myConn;

                cmd.Parameters.AddWithValue("@userId", id_user);
                cmd.Parameters.AddWithValue("@total", total);
                cmd.Parameters.AddWithValue("@data_encomenda", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@pagamento", selectedValue);
                cmd.Parameters.AddWithValue("@numeroEncomenda", rand_num);


                SqlParameter retorno = new SqlParameter("@retorno", SqlDbType.Int);
                retorno.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retorno);



                myConn.Open();
                cmd.ExecuteNonQuery();
                encomenda_id = Convert.ToInt32(cmd.Parameters["@retorno"].Value);

                myConn.Close();

                try
                {
                    string selectedCountry = ddl_pais.SelectedValue;

                    SqlConnection myConn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ConnectionString);


                    SqlCommand cmd2 = new SqlCommand();

                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.CommandText = "faturacao_encomenda";

                    cmd2.Connection = myConn2;

                    cmd2.Parameters.AddWithValue("@userId", id_user);
                    cmd2.Parameters.AddWithValue("@encomenda_id", encomenda_id);
                    cmd2.Parameters.AddWithValue("@nome", tb_nome.Text);
                    cmd2.Parameters.AddWithValue("@alcunha", tb_alcunha.Text);
                    cmd2.Parameters.AddWithValue("@rua", tb_rua.Text);
                    cmd2.Parameters.AddWithValue("@apartamento", tb_apartamento.Text);
                    cmd2.Parameters.AddWithValue("@pais", selectedCountry);
                    cmd2.Parameters.AddWithValue("@cidade", tb_cidade.Text);
                    cmd2.Parameters.AddWithValue("@codigoPostal", tb_codigo_postal.Text);
                    cmd2.Parameters.AddWithValue("@telemovel", tb_telemovel.Text);

                    SqlParameter retorno2 = new SqlParameter("@retorno", SqlDbType.Int);
                    retorno2.Direction = ParameterDirection.Output;
                    cmd2.Parameters.Add(retorno2);



                    myConn2.Open();
                    cmd2.ExecuteNonQuery();

                    myConn2.Close();




                    Response.Redirect("");



                }
                catch (Exception ex)
                {
                    lbl_erro.Text = ex.Message;
                }


            }
            catch (Exception ex)
            {
                lbl_erro.Text = ex.Message;
            }

        }


    }
}