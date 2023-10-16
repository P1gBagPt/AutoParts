<%@ Page Title="" Language="C#" MasterPageFile="~/admin_master.Master" AutoEventWireup="true" CodeBehind="bo_adicionar_categoria.aspx.cs" Inherits="AutoParts.bo_adicionar_categoria" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <main id="main" class="main">
    <section class="section">
        <div class="row">
            <div class="col-lg-12">
                <div class="card">
                    <div class="card-body text-center">
                        <h1 class="card-title">Adicionar Produto</h1>

                        <div class="row">
                            <div class="col-lg-12">
                                <p style="text-align: left;">Nome da Categoria</p>
                                <asp:TextBox ID="tb_nome" class="form-control" runat="server" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Style="color: red;" runat="server" ErrorMessage="Nome da Categoria obrigatória!" ControlToValidate="tb_nome"></asp:RequiredFieldValidator>
                            </div>
                            
                        </div>                     

                        <asp:Label ID="lbl_erro" runat="server"></asp:Label>

                        <asp:Button ID="btn_adicioar_categoria" runat="server" class="btn btn-primary" Text="Adicionar Categoria" OnClick="btn_adicioar_categoria_Click"/>


                    </div>
                </div>
                <!-- End Default Card -->
            </div>
        </div>
    </section>
</main>
</asp:Content>
