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

                SqlParameter retorno = new SqlParameter("@total", SqlDbType.Float);
                retorno.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retorno);

                myConn.Open();
                cmd.ExecuteNonQuery();
                
                total = Convert.ToDecimal(cmd.Parameters["@total"].Value);


                ltTotal.Text = total.ToString();
                myConn.Close();
            }
            catch (Exception ex) { 
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
                // Use DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") to format the date.
                cmd.Parameters.AddWithValue("@data_encomenda", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@pagamento", selectedValue);
                cmd.Parameters.AddWithValue("@numeroEncomenda", rand_num);


                SqlParameter retorno = new SqlParameter("@retorno", SqlDbType.Int);
                retorno.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retorno);

                myConn.Open();
                cmd.ExecuteNonQuery();

                myConn.Close();
            }
            catch (Exception ex)
            {
                lbl_erro.Text = ex.Message;
            }

        }

       
    }
}