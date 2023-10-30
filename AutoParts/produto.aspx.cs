using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

namespace AutoParts
{
    public partial class produto : System.Web.UI.Page
    {
        public class Produto
        {
            public int id_produto { get; set; }
            public int stock { get; set; }
            public string nome { get; set; }
            public string codigoArtigo { get; set; }
            public decimal preco { get; set; }
            public string descricao { get; set; }
            public string categoria { get; set; }
            public string marca { get; set; }
        }

        public static int productId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["productId"] != null)
                {
                    productId = Convert.ToInt32(Request.QueryString["productId"]);
                    Produto product = GetProductDetails(productId);

                    if (product != null)
                    {
                        lbl_nome.Text = product.nome;
                        lbl_descricao.Text = product.descricao;
                        lbl_preco.Text = product.preco.ToString();
                        lbl_codigo_artigo.Text = product.codigoArtigo;
                        lbl_marca.Text = product.marca.ToString();
                        
                        lb_categoria.Text = product.categoria.ToString();
                        lb_categoria.CommandArgument = product.categoria.ToString();

                        tb_quantidade.Text = "1"; // Define o valor padrão como 1
                        int availableStock = product.stock;
                        tb_quantidade.Attributes["max"] = availableStock.ToString();




                        try
                        {
                            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ConnectionString);

                            SqlCommand myCommand = new SqlCommand();
                            myCommand.CommandType = CommandType.StoredProcedure;
                            myCommand.CommandText = "ler_imagem";
                            myCommand.Connection = myConn;
                            myCommand.Parameters.AddWithValue("@id_produto", productId);

                            myConn.Open();

                            SqlDataReader reader = myCommand.ExecuteReader();

                            if (reader.Read())
                            {
                                // Get the image data from the database
                                byte[] imageBytes = reader["imagem"] as byte[];
                                string contentType = reader["contenttype"].ToString();

                                // Check if the content type is "application/octet-stream" to indicate no image
                                if (contentType != "application/octet-stream")
                                {
                                    // If the image exists, display it
                                    string base64Image = Convert.ToBase64String(imageBytes);
                                    string imageSource = "data:" + contentType + ";base64," + base64Image;
                                    main_product_image.ImageUrl = imageSource;
                                }
                                else
                                {
                                    main_product_image.ImageUrl = "admin_assets/img/default_image_product.png";
                                }
                            }
                            else
                            {
                                main_product_image.ImageUrl = "admin_assets/img/default_image_product.png";
                            }

                            myConn.Close();
                        }
                        catch (Exception ex)
                        {
                           lbl_erro.Text = ex.Message;
                        }

                    }
                    else
                    {
                        lbl_erro.Text = "Produto não encontrado!";
                        lbl_erro.ForeColor = System.Drawing.Color.Red;
                        //btn_editar.Enabled = false;
                    }
                }
                else
                {
                    lbl_erro.Text = "ID do produto nao fornecido!";
                    //btn_editar.Enabled = false;
                    lbl_erro.ForeColor = System.Drawing.Color.Red;
                }

            }

        }

        private Produto GetProductDetails(int productId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ConnectionString;
            Produto product = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
    SELECT
        p.id_produto,
        p.stock,
        p.nome,
        p.numero_artigo AS codigoArtigo,
        p.preco,
        p.descricao,
        c.categoria AS categoria_nome,
        m.nome AS marca_nome
    FROM produtos p
    JOIN categoria c ON p.categoria = c.id_categoria
    JOIN marcas m ON p.marca = m.id_marca
    WHERE p.id_produto = @productId";




                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@productId", productId);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        product = new Produto
                        {
                            id_produto = Convert.ToInt32(reader["id_produto"]),
                            stock = Convert.ToInt32(reader["stock"]),
                            nome = reader["nome"].ToString(),
                            codigoArtigo = reader["codigoArtigo"].ToString(),
                            preco = Convert.ToDecimal(reader["preco"]),
                            descricao = reader["descricao"].ToString(),
                            categoria = reader["categoria_nome"].ToString(),
                            marca = reader["marca_nome"].ToString(), // Obtém o nome da marca
                        };
                    }


                    reader.Close();
                }
                catch (Exception ex)
                {
                    lbl_erro.Text = ex.Message;
                }
            }

            return product;
        }

        protected void lb_categoria_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "CategoriaMontra")
            {
                int idCategoriaValue = 0;

                string idCategoria = e.CommandArgument.ToString();

                string connectionString = ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT id_categoria FROM categoria WHERE categoria = @categoria";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@categoria", idCategoria);
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            idCategoriaValue = reader.GetInt32(0); // Retrieve the value from the first column (assuming it's an integer)

                            // Now you have the idCategoriaValue, and you can use it as needed.
                        }
                        else
                        {
                            // Handle the case where no matching category is found.
                        }
                    }
                }

                Response.Redirect("montra.aspx?categoryID="+ idCategoriaValue);
            }
        }

        protected void btn_adicionar_carrinho_Click(object sender, EventArgs e)
        {
            //TEMP
            Session["userId"] = 7;
            try
            {

                int id_user = Convert.ToInt32(Session["userId"].ToString());
                int quantidade = Convert.ToInt32(tb_quantidade.Text);

                SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ConnectionString);

                SqlCommand myCommand = new SqlCommand();
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "inserir_carrinho";

                myCommand.Connection = myConn;

                myCommand.Parameters.AddWithValue("@idUser", id_user);
                myCommand.Parameters.AddWithValue("@idProduto", productId);
                myCommand.Parameters.AddWithValue("@quantidade", quantidade);

                SqlParameter valor = new SqlParameter();
                valor.ParameterName = "@retorno";
                valor.Direction = ParameterDirection.Output;
                valor.SqlDbType = SqlDbType.Int;

                myCommand.Parameters.Add(valor);

                myConn.Open();
                myCommand.ExecuteNonQuery();

                int resposta = Convert.ToInt32(myCommand.Parameters["@retorno"].Value);

                myConn.Close();

                if(resposta == 3)
                {
                    lbl_erro.Enabled = true;
                    lbl_erro.Visible = true;
                    lbl_erro.Text = "A quantidade do carrinho é o stock existente!";
                    lbl_erro.ForeColor = System.Drawing.Color.Red;
                }

                if (resposta == 2)
                {
                    lbl_erro.Enabled = true;
                    lbl_erro.Visible = true;
                    lbl_erro.Text = "Carrinho atualizado com sucesso!";
                    lbl_erro.ForeColor = System.Drawing.Color.Green;
                }

                if (resposta == 1)
                {
                    lbl_erro.Enabled = true;
                    lbl_erro.Visible = true;
                    lbl_erro.Text = "Produto adicionado ao carrinho!";
                    lbl_erro.ForeColor = System.Drawing.Color.Green;
                }
            }
            catch (Exception ex)
            {

            }

            
        }
    }
}