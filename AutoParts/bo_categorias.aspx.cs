using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace AutoParts
{
    public partial class bo_categorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {             
                List<categorias> list = new List<categorias>();



                string query = @"
                SELECT c.categoria AS category_name, COUNT(p.id_produto) AS total_products
                FROM categoria c
                LEFT JOIN produtos p ON c.id_categoria = p.categoria
                GROUP BY c.categoria;";

                SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ConnectionString);

                SqlCommand cmd = new SqlCommand(query, myConn);


                myConn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var categoria = new categorias();

                    categoria.nome = dr.GetString(0);
                    categoria.totalProdutos = dr.GetInt32(1);

                    list.Add(categoria);
                }


                myConn.Close();
                Repeater1.DataSource = list;
                Repeater1.DataBind();


            }
            catch (Exception ex) { 

            }
        }


        public class categorias
        {
            public string nome { get; set; }
            public int totalProdutos { get; set; }
        }
    }
}