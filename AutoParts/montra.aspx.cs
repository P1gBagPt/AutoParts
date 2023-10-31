using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Diagnostics;

namespace AutoParts
{
    public partial class montra : System.Web.UI.Page
    {
        readonly PagedDataSource _pgsource = new PagedDataSource();
        int _firstIndex, _lastIndex;
        private int _pageSize = 12;
        private DataTable _dtOriginal;
        public static string query = "SELECT p.id_produto, p.nome, p.numero_artigo AS codigoArtigo, p.preco, p.imagem, p.contenttype, p.marca, p.estado " +
    "FROM produtos p WHERE estado = 'true' AND stock > 0;";
        public static string orderByClause = "";
        public static string categotiaFilter = "";
        public static string marcaFilter = "";
        public static int categoryId;
        public static string procurar;

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

            lb_categoria_filtro_tudo.CssClass = "tab active";

            if (!IsPostBack)
            {

               


                if (Request.QueryString["categoryID"] != null)
                {
                    categoryId = Convert.ToInt32(Request.QueryString["categoryID"]);

                    query = "SELECT p.id_produto, p.nome, p.numero_artigo AS codigoArtigo, p.preco, p.imagem, p.contenttype, p.marca, p.estado " +
                        "FROM produtos p WHERE estado = 'true' AND categoria = " + categoryId;

                    categotiaFilter = categoryId.ToString();


                    lb_categoria_filtro_tudo.CssClass = "tab";



                    BindDataIntoRepeater(query);
                }
            }

            BindDataIntoRepeater(query);

            try
            {



                try
                {
                    var connectionString = ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ToString();
                    string query = "SELECT TOP 10 id_marca, nome FROM marcas";

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();

                        using (SqlCommand command = new SqlCommand(query, con))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    Repeater2.DataSource = reader;
                                    Repeater2.DataBind();
                                }
                                else
                                {
                                    // Handle the case when no rows are returned (e.g., display a message).
                                }
                            }
                        }
                        con.Close();
                    }




                    string query2 = "SELECT TOP 6 id_categoria, categoria FROM categoria";

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();

                        using (SqlCommand command = new SqlCommand(query2, con))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    Repeater3.DataSource = reader;
                                    Repeater3.DataBind();
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
            catch (Exception ex)
            {


            }

            if (Request.QueryString["categoryID"] != null)
            {
                lb_categoria_filtro_tudo.CssClass = "tab";

                MarkAllTabsInactive(); // Make all tabs inactive
                MarkSelectedTabActive(categotiaFilter); // Make the selected tab active
            }
            else
            {
                lb_categoria_filtro_tudo.CssClass = "tab active";

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
            lbPrevious.Enabled = !_pgsource.IsFirstPage;
            lbNext.Enabled = !_pgsource.IsLastPage;
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

            rptPaging.DataSource = dt;
            rptPaging.DataBind();
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

        protected void ddl_ordenar_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = ddl_ordenar.SelectedValue;
            orderByClause = "";

            switch (selectedValue)
            {
                case "nome_asc":
                    orderByClause = "ORDER BY p.nome ASC";
                    break;
                case "nome_desc":
                    orderByClause = "ORDER BY p.nome DESC";
                    break;
                case "preco_asc":
                    orderByClause = "ORDER BY p.preco ASC";
                    break;
                case "preco_desc":
                    orderByClause = "ORDER BY p.preco DESC";
                    break;
                default:
                    // Lidar com caso inválido ou padrão aqui
                    break;
            }



            /*query = "SELECT p.id_produto, p.nome, p.numero_artigo AS codigoArtigo, p.preco, p.imagem, p.contenttype, p.marca, p.estado " +
                "FROM produtos p WHERE estado = 'true' AND categoria = " + categotiaFilter + " " + orderByClause;*/

            if (!string.IsNullOrEmpty(categotiaFilter) && !string.IsNullOrEmpty(marcaFilter))
            {
                query = "SELECT p.id_produto, p.nome, p.numero_artigo AS codigoArtigo, p.preco, p.imagem, p.contenttype, p.marca, p.estado " +
     "FROM produtos p WHERE estado = 'true' AND categoria = " + categotiaFilter + " AND marca = " + marcaFilter + " " + orderByClause;
            }
            else if (!string.IsNullOrEmpty(categotiaFilter))
            {
                query = "SELECT p.id_produto, p.nome, p.numero_artigo AS codigoArtigo, p.preco, p.imagem, p.contenttype, p.marca, p.estado " +
               "FROM produtos p WHERE estado = 'true' AND categoria = " + categotiaFilter + " " + orderByClause;

                MarkAllTabsInactive(); // Make all tabs inactive
                lb_categoria_filtro_tudo.CssClass = "tab";


                MarkSelectedTabActive(categotiaFilter); // Make the selected tab active
            }
            else
            {
                query = "SELECT p.id_produto, p.nome, p.numero_artigo AS codigoArtigo, p.preco, p.imagem, p.contenttype, p.marca, p.estado " +
                "FROM produtos p WHERE estado = 'true' " + orderByClause;
            }

            if (categotiaFilter == null && categotiaFilter == "")
            {
                MarkAllTabsInactive(); // Make all tabs inactive
                lb_categoria_filtro_tudo.CssClass = "tab active";


                MarkSelectedTabActive(categotiaFilter); // Make the selected tab active
            }



            BindDataIntoRepeater(query);
        }

        protected void lb_categoria_filtro_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Categoria")
            {
                marcaFilter = "";
                categotiaFilter = e.CommandArgument.ToString();

                MarkAllTabsInactive();
                lb_categoria_filtro_tudo.CssClass = "tab";
                MarkSelectedTabActive(e.CommandArgument.ToString());

                query = "SELECT p.id_produto, p.nome, p.numero_artigo AS codigoArtigo, p.preco, p.imagem, p.contenttype, p.marca, p.estado " +
                        "FROM produtos p WHERE estado = 'true' AND categoria = " + categotiaFilter + orderByClause;

                // Bind products based on the selected category
                BindDataIntoRepeater(query);
            }
        }



        protected void lb_categoria_filtro_tudo_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "CategoriaTudo")
            {
                marcaFilter = "";// Clear the marca filter
                categotiaFilter = ""; // Clear the category filter

                query = "SELECT p.id_produto, p.nome, p.numero_artigo AS codigoArtigo, p.preco, p.imagem, p.contenttype, p.marca, p.estado " +
                        "FROM produtos p WHERE estado = 'true' " + orderByClause;

                BindDataIntoRepeater(query);
            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            SetProductImage(e.Item);
        }




        protected void lb_marca_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Marca")
            {
                marcaFilter = e.CommandArgument.ToString();

                // Mark the "Todas as Marcas" option as inactive
                // lb_marcas_tudo.CssClass = "tab";

                // Update the query based on whether a category filter is selected
                if (string.IsNullOrEmpty(categotiaFilter))
                {
                    query = "SELECT p.id_produto, p.nome, p.numero_artigo AS codigoArtigo, p.preco, p.imagem, p.contenttype, p.marca, p.estado " +
                        "FROM produtos p WHERE estado = 'true' AND marca = " + marcaFilter + " " + orderByClause;
                }
                else
                {
                    query = "SELECT p.id_produto, p.nome, p.numero_artigo AS codigoArtigo, p.preco, p.imagem, p.contenttype, p.marca, p.estado " +
                        "FROM produtos p WHERE estado = 'true' AND categoria = " + categotiaFilter + " AND marca = " + marcaFilter + " " + orderByClause;
                }

                // Bind data based on the selected brand
                BindDataIntoRepeater(query);
            }
        }




        protected void lb_marcas_tudo_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "MarcasTudo")
            {
                if (categotiaFilter == "")
                {
                    query = "SELECT p.id_produto, p.nome, p.numero_artigo AS codigoArtigo, p.preco, p.imagem, p.contenttype, p.marca, p.estado " +
                    "FROM produtos p WHERE estado = 'true' " + orderByClause;
                }
                else
                {
                    query = "SELECT p.id_produto, p.nome, p.numero_artigo AS codigoArtigo, p.preco, p.imagem, p.contenttype, p.marca, p.estado " +
                    "FROM produtos p WHERE estado = 'true' AND categoria = " + categotiaFilter + orderByClause;
                }



                BindDataIntoRepeater(query);
            }
        }


        private void MarkSelectedTabActive(string selectedCategory)
        {
            foreach (RepeaterItem item in Repeater3.Items)
            {
                LinkButton lb_categoria_filtro = (LinkButton)item.FindControl("lb_categoria_filtro");
                string categoryValue = lb_categoria_filtro.CommandArgument;

                if (categoryValue == selectedCategory)
                {
                    lb_categoria_filtro.CssClass = "tab active";
                }
                else
                {
                    lb_categoria_filtro.CssClass = "tab";
                }
            }
        }

        protected void btn_pesquisar_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Procurar")
            {
                procurar = tb_procurar.Text;

                query = "SELECT p.id_produto, p.nome, p.numero_artigo AS codigoArtigo, p.preco, p.imagem, p.contenttype, p.marca, p.estado " +
"FROM produtos p " +
"WHERE estado = 'true' AND (p.nome LIKE '%" + procurar + "%' OR p.numero_artigo LIKE '%" + procurar + "%')";




                BindDataIntoRepeater(query);


            }
        }

        private void MarkAllTabsInactive()
        {
            // Loop through all the tabs and set them to be inactive
            foreach (RepeaterItem item in Repeater3.Items)
            {
                LinkButton lb_categoria_filtro = (LinkButton)item.FindControl("lb_categoria_filtro");
                lb_categoria_filtro.CssClass = "tab";
            }
        }

    }
}