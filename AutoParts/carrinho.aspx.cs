using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AutoParts
{
    public partial class carrinho : System.Web.UI.Page
    {
        readonly PagedDataSource _pgsource = new PagedDataSource();
        int _firstIndex, _lastIndex;
        private int _pageSize = 12;
        private DataTable _dtOriginal;

        public static string query = "";

        // Don't forget to pass the @userId parameter when executing the query.

        public static string orderByClause = "";
        public static string categotiaFilter = "";
        public static string marcaFilter = "";
        public static int categoryId;
        public static string procurar;
        public static int id_user;
        public static decimal total = 0;

        private int CurrentPage
        {
            get
            {
                if (ViewState["CurrentPage"] == null)
                {
                    return 0;
                }
                return ((int)ViewState["CurrentPage"]);
            }
            set
            {
                ViewState["CurrentPage"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["userId"] = 7;
            id_user = Convert.ToInt32(Session["userId"].ToString());

            total = 0;
            if (!IsPostBack)
            {
                total = 0;
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

                    decimal totali = (cmd.Parameters["@total"].Value != DBNull.Value) ? Convert.ToDecimal(cmd.Parameters["@total"].Value) : 0;

                    if (totali == 0)
                    {
                        lbl_vazio.Text = "O Carrinho está vazio adiciona alguns produtos!";
                        lbl_vazio.Enabled = true;
                        lbl_vazio.Visible = true;
                        lbl_vazio.ForeColor = Color.Red;


                        ltTotal.Text = "";
                        btn_checkout.Visible = false;
                        btn_checkout.Enabled = false;

                        lb_esvaziar.Visible = false;
                        lb_esvaziar.Enabled = false;

                    }
                    else
                    {
                        total = 0;

                        query = "SELECT c.id_carrinho, c.quantidade, p.*, p.stock " +
                "FROM carrinho c " +
                "INNER JOIN produtos p ON c.produtoID = p.id_produto " +
                "WHERE c.userID = " + id_user + " AND c.ativo = 1";


                        BindDataIntoRepeater(query);
                    }

                }
                catch (Exception ex)
                {
                    lbl_vazio.Text = ex.Message;
                }

            }

            total = 0;
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

                decimal totali = (cmd.Parameters["@total"].Value != DBNull.Value) ? Convert.ToDecimal(cmd.Parameters["@total"].Value) : 0;

                if (totali == 0)
                {
                    lbl_vazio.Text = "O Carrinho está vazio adiciona alguns produtos!";
                    lbl_vazio.Enabled = true;
                    lbl_vazio.Visible = true;
                    lbl_vazio.ForeColor = Color.Red;


                    ltTotal.Text = "";
                    btn_checkout.Visible = false;
                    btn_checkout.Enabled = false;

                    lb_esvaziar.Visible = false;
                    lb_esvaziar.Enabled = false;

                }
                else
                {


                    query = "SELECT c.id_carrinho, c.quantidade, p.*, p.stock " +
            "FROM carrinho c " +
            "INNER JOIN produtos p ON c.produtoID = p.id_produto " +
            "WHERE c.userID = " + id_user + " AND c.ativo = 1";


                    BindDataIntoRepeater(query);
                }

            }
            catch (Exception ex)
            {
                lbl_vazio.Text = ex.Message;
            }

        }



        static DataTable GetDataFromDb(string query)
        {
            var con = new SqlConnection(ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ToString());

            var da = new SqlDataAdapter(query, con);
            var dt = new DataTable();

            try
            {
                con.Open();
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
                throw ex;
            }
            finally
            {
                con.Close();
            }

            return dt;
        }

        private void BindDataIntoRepeater(string query)
        {
            var dt = GetDataFromDb(query);
            _pgsource.DataSource = dt.DefaultView;
            _pgsource.AllowPaging = true;
            // Number of items to be displayed in the Repeater
            _pgsource.PageSize = _pageSize;
            _pgsource.CurrentPageIndex = CurrentPage;
            // Keep the Total pages in View State
            ViewState["TotalPages"] = _pgsource.PageCount;
            // Example: "Page 1 of 10"
            //lblpage.Text = "Página " + (CurrentPage + 1) + " de " + _pgsource.PageCount;
            // Enable First, Last, Previous, Next buttons
            /*lbPrevious.Enabled = !_pgsource.IsFirstPage;
            lbNext.Enabled = !_pgsource.IsLastPage;*/
            /*lbFirst.Enabled = !_pgsource.IsFirstPage;
            lbLast.Enabled = !_pgsource.IsLastPage;*/

            // Bind data into repeater
            Repeater1.DataSource = _pgsource;
            Repeater1.DataBind();

            // Call the function to do paging
            HandlePaging();
        }

        private void HandlePaging()
        {
            var dt = new DataTable();
            dt.Columns.Add("PageIndex"); //Start from 0
            dt.Columns.Add("PageText"); //Start from 1

            _firstIndex = CurrentPage - 5;
            if (CurrentPage > 5)
                _lastIndex = CurrentPage + 5;
            else
                _lastIndex = 10;

            // Check last page is greater than total page then reduced it 
            // to total no. of page is last index
            if (_lastIndex > Convert.ToInt32(ViewState["TotalPages"]))
            {
                _lastIndex = Convert.ToInt32(ViewState["TotalPages"]);
                _firstIndex = _lastIndex - 10;
            }

            if (_firstIndex < 0)
                _firstIndex = 0;

            // Now creating page number based on above first and last page index
            for (var i = _firstIndex; i < _lastIndex; i++)
            {
                var dr = dt.NewRow();
                dr[0] = i;
                dr[1] = i + 1;
                dt.Rows.Add(dr);
            }

            /*Repeater1.DataSource = dt;
            rptPaging.DataBind();*/
        }

        protected void lbFirst_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            BindDataIntoRepeater(query);
        }
        protected void lbLast_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            BindDataIntoRepeater(query);
        }
        protected void lbPrevious_Click(object sender, EventArgs e)
        {
            CurrentPage -= 1;
            BindDataIntoRepeater(query);
        }
        protected void lbNext_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            BindDataIntoRepeater(query);
        }
        protected void rptPaging_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (!e.CommandName.Equals("newPage")) return;
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            BindDataIntoRepeater(query);
        }

        protected void rptPaging_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            var lnkPage = (LinkButton)e.Item.FindControl("lbPaging");
            if (lnkPage.CommandArgument != CurrentPage.ToString()) return;
            lnkPage.Enabled = false;
            lnkPage.BackColor = Color.FromName("#0398fc");
            lnkPage.ForeColor = Color.White;
        }


        private void SetProductImage(RepeaterItem item)
        {
            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            {
                System.Web.UI.WebControls.Image productImage = item.FindControl("img_produto") as System.Web.UI.WebControls.Image;
                DataRowView dataItem = item.DataItem as DataRowView;

                string contentType = dataItem["contenttype"].ToString();
                byte[] imageBytes = (byte[])dataItem["imagem"];

                if (contentType != "application/octet-stream")
                {
                    string base64Image = Convert.ToBase64String(imageBytes);
                    string imageSource = "data:" + contentType + ";base64," + base64Image;
                    productImage.ImageUrl = imageSource;
                }
                else
                {
                    productImage.ImageUrl = "admin_assets/img/default_image_product.png";
                }
            }
        }

        protected void btn_continuar_comprar_Click(object sender, EventArgs e)
        {
            Response.Redirect("montra.aspx");
        }

        protected void lb_remover_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Remover")
            {
                total = 0;

                int idCarrinho = Convert.ToInt32(e.CommandArgument);

                // Construa a consulta SQL DELETE com base no ID do carrinho
                string deleteQuery = "DELETE FROM carrinho WHERE id_carrinho = @IdCarrinho AND userID = " + id_user;

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@IdCarrinho", idCarrinho);

                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();

                        if (rowsAffected > 0)
                        {
                            // A linha foi excluída com sucesso. Você pode adicionar qualquer lógica adicional aqui, se necessário.
                        }
                        else
                        {
                            // Não foi possível excluir a linha. Você pode tratar isso de acordo com seus requisitos.
                        }

                        // Após a exclusão, você pode atualizar o Repeater para refletir as mudanças.
                        BindDataIntoRepeater(query);

                    }
                }
            }
        }

        protected void Repeater1_ItemDataBound1(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dataItem = e.Item.DataItem as DataRowView;
                int quantidade = Convert.ToInt32(dataItem["quantidade"]);
                decimal preco = Convert.ToDecimal(dataItem["preco"]);
                decimal subtotal = quantidade * preco;
                total += subtotal;

                Label ltSubtotal = e.Item.FindControl("lbl_subtotal") as Label;
                ltSubtotal.Text = subtotal.ToString("C", new CultureInfo("pt-PT"));

                ltTotal.Text = total.ToString();

            }
        }


        protected void lb_atualizar_Command(object sender, CommandEventArgs e)
        {
            

        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Atualizar")
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    // Find the TextBox within the current item
                    TextBox tb_quantidade = e.Item.FindControl("tb_quantidade") as TextBox;

                    if (tb_quantidade != null)
                    {
                        string textBoxText = tb_quantidade.Text;
                        // Use textBoxText as needed
                    }
                }
            }
        }

        protected void btn_checkout_Click(object sender, EventArgs e)
        {
            Response.Redirect("checkout.aspx");
        }

        protected void lb_esvaziar_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Esvaziar")
            {
                string esvaziarQuery = "DELETE FROM carrinho WHERE userID = " + id_user + " AND ativo = 1";

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand(esvaziarQuery, con))
                    {
                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();

                        if (rowsAffected > 0)
                        {
                            // A linha foi excluída com sucesso. Você pode adicionar qualquer lógica adicional aqui, se necessário.
                        }
                        else
                        {
                            // Não foi possível excluir a linha. Você pode tratar isso de acordo com seus requisitos.
                        }

                        // Após a exclusão, você pode atualizar o Repeater para refletir as mudanças.
                        ltTotal.Text = "0";

                        BindDataIntoRepeater(query);

                        //CalcularTotal();


                    }
                }
            }
        }

  

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            SetProductImage(e.Item);
        }

    }
}