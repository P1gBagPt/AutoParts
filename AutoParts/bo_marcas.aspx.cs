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
    public partial class bo_marcas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                List<marcas> list = new List<marcas>();



                string query = @"
SELECT m.nome AS brand_name, COUNT(p.id_produto) AS total_products
FROM marcas m
LEFT JOIN produtos p ON m.id_marca = p.marca
GROUP BY m.nome;";


                SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ConnectionString);

                SqlCommand cmd = new SqlCommand(query, myConn);


                myConn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var marca = new marcas();

                    marca.nome = dr.GetString(0);
                    marca.totalMarcasProdutos = dr.GetInt32(1);

                    list.Add(marca);
                }


                myConn.Close();
                Repeater1.DataSource = list;
                Repeater1.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        public class marcas
        {
            public string nome { get; set; }
            public int totalMarcasProdutos { get; set; }
        }
    }
}