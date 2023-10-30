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



                            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                <ItemTemplate>
                                    <div class="accordion" id="accordionExample">
                                        <div class="accordion-item">
                                            <h2 class="accordion-header" id='<%# "heading" + Container.ItemIndex %>'>
                                                <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target='<%# "#collapse" + Container.ItemIndex %>' aria-expanded="false" aria-controls='<%# "collapse" + Container.ItemIndex %>'>
                                                    Order #<%# Eval("id_encomenda") %> - Codigo: <%# Eval("codigo") %>
                                                </button>

                                            </h2>
                                            <div id='<%# "collapse" + Container.ItemIndex %>' class="accordion-collapse collapse" aria-labelledby='<%# "heading" + Container.ItemIndex %>' data-bs-parent="#accordionExample">
                                                <div class="accordion-body">
                                                    <p>ID: <%# Eval("id_encomenda") %></p>
                                                    <p>Data: <%# Eval("data_encomenda", "{0:MM/dd/yyyy}") %></p>
                                                    <p>Total: <%# Eval("total") %> €</p>
                                                    <p>Status: <%# Eval("status") %></p>
                                                    <p>Método de Pagamento: <%# Eval("metodoPagamento") %></p>

                                                    <h3>Products</h3>

                                                    <asp:Repeater ID="Repeater2" runat="server">
                                                        <ItemTemplate>
                                                            <p>
                                                                Nome do Produto: <%# Eval("nome") %><br />
                                                                Subtotal: <%# Eval("subtotal") %> €<br />
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
                            <!-- End Default Accordion Example -->
                        </div>
                    </div>

                </div>
            </div>
        </section>

    </main>

</asp:Content>
