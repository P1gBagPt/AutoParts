<%@ Page Title="" Language="C#" MasterPageFile="~/admin_master.Master" AutoEventWireup="true" CodeBehind="bo_categorias.aspx.cs" Inherits="AutoParts.bo_categorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main" class="main">

        <div class="pagetitle">
            <h1>Categorias</h1>
            <nav>
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="index_admin.aspx">Inicio</a></li>
                    <li class="breadcrumb-item active">Categorias</li>
                </ol>
            </nav>
        </div>
        <!-- End Page Title -->

        <section class="section">
            <div class="row">
                <div class="col-lg-12">

                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">Categorias</h5>

                            <a href="bo_adicionar_categoria.aspx">Adicionar categoria</a>

                            <table class="table table-bordered border-primary">
                                <asp:Repeater ID="Repeater1" runat="server">

                                    <HeaderTemplate>

                                        <thead>
                                            <tr>
                                                <th scope="col" style="color: blue;">Nome da Categoria</th>
                                                <th scope="col" style="color: blue;">Total de Produtos associados</th>
                                            </tr>
                                        </thead>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <tbody>
                                            <tr>
                                                <th scope="row"><%# Eval("nome")%></th>
                                                <th scope="row"><%# Eval("totalProdutos")%></th>                                    
                                            </tr>

                                        </tbody>

                                    </ItemTemplate>

                                </asp:Repeater>
                            </table>
                        </div>
                    </div>

                </div>
            </div>
        </section>

    </main>
    <!-- End #main -->
</asp:Content>
