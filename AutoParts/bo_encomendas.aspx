<%@ Page Title="" Language="C#" MasterPageFile="~/admin_master.Master" AutoEventWireup="true" CodeBehind="bo_encomendas.aspx.cs" Inherits="AutoParts.bo_encomendas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <main id="main" class="main">

        <div class="pagetitle">
            <h1>Encomendas</h1>
            <nav>
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="index_admin.aspx">Inicio</a></li>
                    <li class="breadcrumb-item active">Encomendas</li>
                </ol>
            </nav>
        </div>


        <section class="section">
            <div class="row">
                <div class="col-lg-12">

                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">Encomendas</h5>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>


                                    <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
                                        <ItemTemplate>
                                            <div class="accordion" id="accordionExample">
                                                <div class="accordion-item">
                                                    <h2 class="accordion-header" id='<%# "heading" + Container.ItemIndex %>'>
                                                        <asp:Button runat="server" ID="btn_accordion" CssClass="accordion-button"
    data-bs-toggle="collapse"
    data-bs-target='<%# "#collapse" + Container.ItemIndex %>'
    aria-expanded="true"
    aria-controls='<%# "collapse" + Container.ItemIndex %>'
    CommandName="ShowProducts" CommandArgument='<%# Eval("id_encomenda") %>'
    Text='<%# "Order #" + Eval("id_encomenda") + " - Codigo: " + Eval("codigo") %>' />




                                                    </h2>
                                                    <div id='<%# "collapse" + Container.ItemIndex %>' class="accordion-collapse collapse" aria-labelledby='<%# "heading" + Container.ItemIndex %>' data-bs-parent="#accordionExample">
                                                        <div class="accordion-body">
                                                            <p>ID: <%# Eval("id_encomenda") %></p>
                                                            <p>Data: <%# Eval("data_encomenda", "{0:MM/dd/yyyy}") %></p>
                                                            <p>Total: <%# Eval("total", "{0:C}") %></p>
                                                            <p>Status: <%# Eval("status") %></p>
                                                            <p>Método de Pagamento: <%# Eval("metodoPagamento") %></p>

                                                            <h3>Products</h3>

                                                            <asp:Repeater ID="Repeater2" runat="server">
                                                                <ItemTemplate>
                                                                    <p>
                                                                        Nome do Produto: <%# Eval("nome") %><br />
                                                                        Subtotal: <%# Eval("subtotal", "{0:C}") %><br />
                                                                        Marca: <%# Eval("marca_nome") %>
                                                                    </p>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <!-- End Default Accordion Example -->
                        </div>
                    </div>

                </div>
            </div>
        </section>

    </main>


    <script type="text/javascript">
        $(document).ready(function () {
            // Attach a click event to the accordion buttons.
            $('.accordion-button').click(function (e) {
                e.preventDefault(); // Prevent the default behavior of the button.
                var target = $(this).data('bs-target'); // Get the target element from the data attribute.
                $(target).collapse('toggle'); // Toggle the collapse element.
            });
        });
    </script>

</asp:Content>
