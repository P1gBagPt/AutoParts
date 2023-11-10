<%@ Page Title="" Language="C#" MasterPageFile="~/main_master.Master" AutoEventWireup="true" CodeBehind="conta.aspx.cs" Inherits="AutoParts.conta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container rounded bg-white">
        <div class="row">

            <div class="col-md-6 border-right">
                <div class="p-3 py-5">                  
                    <div class="d-flex justify-content-between align-items-center mb-3">
                        <h4 class="text-right">Detalhes da Conta</h4>
                    </div>
                    <div class="row mt-2">
                        <div class="col-md-6">
                            <label class="labels">Nome</label>
                            <asp:TextBox ID="tb_nome" runat="server" placeholder="Nome" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <label class="labels">Nome de utilizador</label>
                            <asp:TextBox ID="tb_username" runat="server" placeholder="Username" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row mt-3">
                        <div class="col-md-6">
                            <label class="labels">Email</label>
                            <asp:TextBox ID="tb_email" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>

                        <div class="col-md-6">
                            <label class="labels">Vendedor</label>
                            <asp:Button ID="btn_vendedor" runat="server" Text="Pedir conta vendedor" OnClick="btn_vendedor_Click" />
                        </div>
                        <div class="col-md-6">
                            <label class="labels">Guardar Alterações</label>
                            <asp:Button ID="btn_guardar_altera" runat="server" CssClass="btn btn-primary profile-button" Text="Guardar Alterações" OnClick="btn_guardar_altera_Click" />
                        </div>
                        <asp:Label ID="lbl_guardar" runat="server" Visible="False" Enabled="False"></asp:Label>
                        <div class="col-md-6">
                            <label class="labels">Logout</label>
                            <asp:Button ID="btn_logout" runat="server" CssClass="btn btn-primary profile-button" Text="Logout" OnClick="btn_logout_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="p-3 py-5">
                    <div class="d-flex justify-content-between align-items-center experience">
                        <h4 class="text-right">Encomendas</h4>
                    </div>
                    <br>
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
                                                    <div class="container">
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <img src='data:<%# Eval("contenttype") %>;base64,<%# Convert.ToBase64String((byte[])Eval("imagem")) %>' alt='<%# Eval("nome") %>' class="img-fluid">
                                                            </div>
                                                            <div class="col-md-8">
                                                                <p>
                                                                    Nome do Produto: <%# Eval("nome") %><br />
                                                                    Subtotal: <%# Eval("subtotal") %> €<br />
                                                                    Marca: <%# Eval("marca_nome") %>
                                                                </p>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
