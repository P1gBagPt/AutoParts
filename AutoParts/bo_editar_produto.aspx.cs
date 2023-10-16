using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static AutoParts.bo_produtos;

namespace AutoParts
{

    public class Produto
    {
        public int id_produto { get; set; }
        public int stock { get; set; }
        public string nome { get; set; }
        public string codigoArtigo { get; set; }
        public decimal preco { get; set; }
        public string descricao { get; set; }
        public int categoria { get; set; }
        public int marca { get; set; }
    }
    public partial class bo_editar_produto : System.Web.UI.Page
    {
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
                        tb_nome.Text = product.nome;
                        tb_numero_artigo.Text = product.codigoArtigo;
                        tb_preco.Text = product.preco.ToString();
                        tb_stock.Text = product.stock.ToString();
                        tb_descricao.Text = product.descricao;
                        ddl_categoria.SelectedValue = product.categoria.ToString();
                        ddl_marca.SelectedValue = product.marca.ToString();


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
                                    productImage.ImageUrl = imageSource;
                                }
                                else
                                {
                                    lbl_nao_imagem.Enabled = true;
                                    lbl_nao_imagem.Visible = true;
                                    productImage.ImageUrl = "admin_assets/img/default_image_product.png";
                                }
                            }
                            else
                            {
                                lbl_nao_imagem.Enabled = true;
                                lbl_nao_imagem.Visible = true;
                                productImage.ImageUrl = "admin_assets/img/default_image_product.png";
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
                        btn_editar.Enabled = false;
                    }
                }
                else
                {
                    lbl_erro.Text = "ID do produto nao fornecido!";
                    btn_editar.Enabled = false;
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
                    p.categoria,
                    p.marca
                FROM produtos p
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
                            categoria = Convert.ToInt32(reader["categoria"]),
                            marca = Convert.ToInt32(reader["marca"]),
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


        protected void btn_editar_Click(object sender, EventArgs e)
        {

            if (fu_imagem.HasFile)
            {
                Stream imgStream = fu_imagem.PostedFile.InputStream;
                int imgTamanho = fu_imagem.PostedFile.ContentLength;

                string contentType = fu_imagem.PostedFile.ContentType;

                byte[] imgBinary = new byte[imgTamanho];

                imgStream.Read(imgBinary, 0, imgTamanho);

                SqlConnection myConnn = new SqlConnection(ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ConnectionString);

                SqlCommand myCommandd = new SqlCommand();
                myCommandd.CommandType = CommandType.StoredProcedure;
                myCommandd.CommandText = "editar_produto";

                myCommandd.Connection = myConnn;

                myCommandd.Parameters.AddWithValue("@id_produto", productId);
                myCommandd.Parameters.AddWithValue("@nome", tb_nome.Text);
                myCommandd.Parameters.AddWithValue("@descricao", tb_descricao.Text);
                myCommandd.Parameters.AddWithValue("@numero_artigo", tb_numero_artigo.Text);
                myCommandd.Parameters.AddWithValue("@preco", tb_preco.Text);
                myCommandd.Parameters.AddWithValue("@stock", tb_stock.Text);
                myCommandd.Parameters.AddWithValue("@categoria", ddl_categoria.SelectedValue);
                myCommandd.Parameters.AddWithValue("@imagem", imgBinary);
                myCommandd.Parameters.AddWithValue("@ct", contentType);
                myCommandd.Parameters.AddWithValue("@marca", ddl_marca.SelectedValue);



                myConnn.Open();
                myCommandd.ExecuteNonQuery();

                lbl_erro.Text = "Produto atualizado com sucesso!";
                lbl_erro.ForeColor = System.Drawing.Color.Green;

                myConnn.Close();
            }
            else
            {
                SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ConnectionString);

                SqlCommand myCommand = new SqlCommand();
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "editar_produto_sem_img";

                myCommand.Connection = myConn;

                myCommand.Parameters.AddWithValue("@id_produto", productId);
                myCommand.Parameters.AddWithValue("@nome", tb_nome.Text);
                myCommand.Parameters.AddWithValue("@descricao", tb_descricao.Text);
                myCommand.Parameters.AddWithValue("@numero_artigo", tb_numero_artigo.Text);
                myCommand.Parameters.AddWithValue("@preco", tb_preco.Text);
                myCommand.Parameters.AddWithValue("@stock", tb_stock.Text);
                myCommand.Parameters.AddWithValue("@categoria", ddl_categoria.SelectedValue);
                myCommand.Parameters.AddWithValue("@marca", ddl_marca.SelectedValue);



                myConn.Open();
                myCommand.ExecuteNonQuery();

                lbl_erro.Text = "Produto atualizado com sucesso!";
                lbl_erro.ForeColor = System.Drawing.Color.Green;

                myConn.Close();
            }

            

            
        }
    }
}