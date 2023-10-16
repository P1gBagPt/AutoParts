<%@ Page Title="" Language="C#" MasterPageFile="~/admin_master.Master" AutoEventWireup="true" CodeBehind="bo_marcas.aspx.cs" Inherits="AutoParts.bo_marcas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main" class="main">

    <div class="pagetitle">
        <h1>Marcas</h1>
        <nav>
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="index_admin.aspx">Inicio</a></li>
                <li class="breadcrumb-item active">Marcas</li>
            </ol>
        </nav>
    </div>
    <!-- End Page Title -->

    <section class="section">
        <div class="row">
            <div class="col-lg-12">

                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">Marcas</h5>

                        <a href="bo_adicionar_marca.aspx">Adicionar marca</a>

                        <table class="table table-bordered border-primary">
                            <asp:Repeater ID="Repeater1" runat="server">

                                <HeaderTemplate>

                                    <thead>
                                        <tr>
                                            <th scope="col" style="color: blue;">Nome da Marca</th>
                                            <th scope="col" style="color: blue;">Total de Produtos associados a marca</th>
                                        </tr>
                                    </thead>

                                </HeaderTemplate>

                                <ItemTemplate>
                                    <tbody>
                                        <tr>
                                            <th scope="row"><%# Eval("nome")%></th>
                                            <th scope="row"><%# Eval("totalMarcasProdutos")%></th>                                    
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
</asp:Content>
