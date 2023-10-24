<%@ Page Title="" Language="C#" MasterPageFile="~/main_master.Master" AutoEventWireup="true" CodeBehind="produto.aspx.cs" Inherits="AutoParts.produto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta1/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta1/dist/js/bootstrap.bundle.min.js" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/boxicons@latest/css/boxicons.min.css" rel="stylesheet" />

    <link href="assets/css/produto.css" rel="stylesheet" />

    <style>
        .categoriaA a{
            text-decoration: none;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <hr />
    <div class="container mt-5 mb-5">
        <div class="card">
            <div class="row g-0">
                <div class="col-md-6 border-end">
                    <div class="d-flex flex-column justify-content-center" style="width: 450px; height: 500px;">
                        <div class="main_image">
                            <asp:Image ID="main_product_image" Width="100%" Height="100%" runat="server" />
                        </div>
                    </div>

                </div>
                <div class="col-md-6">
                    <div class="p-3 right-side">
                        <div class="d-flex justify-content-between align-items-center">
                            <h3>
                                <asp:Label ID="lbl_nome" runat="server"></asp:Label>
                            </h3>
                        </div>
                        <div class="mt-2 categoriaA">
                                <p>
                                    <asp:LinkButton ID="lb_categoria" runat="server" OnCommand="lb_categoria_Command" CommandName="CategoriaMontra">LinkButton</asp:LinkButton>
                                </p>
                            
                        </div>
                        <div class="mt-2 pr-3 content">
                            Descrição:
                            <p>
                                <asp:Label ID="lbl_descricao" runat="server"></asp:Label>
                            </p>
                        </div>
                        <h3>
                            <asp:Label ID="lbl_preco" runat="server"></asp:Label>
                            €</h3>

                        <div class="mt-2">
                            <h7>
                                Código do artigo: <b>
                                    <asp:Label ID="lbl_codigo_artigo" runat="server"></asp:Label></b></h7>
                        </div>

                        <div class="mt-2">
                            <h7>
                                Marca: <b>
                                    <asp:Label ID="lbl_marca" runat="server"></asp:Label></b></h7>
                        </div>
                        <div class="mt-2">
                            <asp:TextBox ID="tb_quantidade" runat="server" TextMode="Number" min="1"></asp:TextBox>
                        </div>

                        <div class="buttons d-flex flex-row mt-5 gap-3">
                            <asp:Button ID="btn_adicionar_carrinho" runat="server" Text="Adicionar Carrinho" CssClass="btn btn-dark" OnClick="btn_adicionar_carrinho_Click" />
                        </div>

                        <div class="mt-2">
                            <asp:Label ID="lbl_erro" runat="server"></asp:Label>
                        </div>

                    </div>

                </div>
            </div>
        </div>
    </div>


</asp:Content>
