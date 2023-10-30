﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AutoParts
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ToString();
                string query = "SELECT TOP 4 id_produto, nome, preco, imagem, contenttype FROM produtos WHERE stock > 1 AND estado = 1";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                Repeater1.DataSource = reader;
                                Repeater1.DataBind();
                            }
                            else
                            {
                                // Handle the case when no rows are returned (e.g., display a message).
                            }
                        }
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {

            }

        }


        protected string GetBase64Image(object imageData, object contentType)
        {
            if (imageData != DBNull.Value)
            {
                byte[] bytes = (byte[])imageData;
                string base64Image = Convert.ToBase64String(bytes);
                string imageSource = "data:" + contentType + ";base64," + base64Image;
                return imageSource;
            }
            else
            {
                // Se a imagem não estiver disponível, você pode retornar uma imagem padrão.
                return "admin_assets/img/default_image_product.png";
            }
        }

        protected void lb_adicionar_quatroMarcas_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "AdicionarCarTopMarcas")
            {
                int idProduto = int.Parse(e.CommandArgument.ToString());

                Session["userId"] = 7;

                int id_user = Convert.ToInt32(Session["userId"].ToString());

                SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ConnectionString);

                SqlCommand myCommand = new SqlCommand();
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "carrinho_quick";

                myCommand.Connection = myConn;

                myCommand.Parameters.AddWithValue("@idUser", id_user);
                myCommand.Parameters.AddWithValue("@idProduto", idProduto);

                SqlParameter valor = new SqlParameter();
                valor.ParameterName = "@retorno";
                valor.Direction = ParameterDirection.Output;
                valor.SqlDbType = SqlDbType.Int;

                myCommand.Parameters.Add(valor);

                myConn.Open();
                myCommand.ExecuteNonQuery();
                myConn.Close();

               
            }
        }
    }
}