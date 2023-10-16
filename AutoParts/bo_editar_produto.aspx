<%@ Page Title="" Language="C#" MasterPageFile="~/admin_master.Master" AutoEventWireup="true" CodeBehind="bo_editar_produto.aspx.cs" Inherits="AutoParts.bo_editar_produto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <main id="main" class="main">
    <section class="section">
        <div class="row">
            <div class="col-lg-12">
                <div class="card">
                    <div class="card-body text-center">
                        <h1 class="card-title">Editar Produto</h1>

                        <div class="row">
                            <div class="col-lg-4" style="text-align: left;">
                                <p>Nome</p>
                                <asp:TextBox ID="tb_nome" class="form-control" runat="server" MaxLength="75"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Style="color: red;" runat="server" ErrorMessage="Nome obrigatório!" ControlToValidate="tb_nome"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-lg-4" style="text-align: left;">
                                <p>Código do artigo</p>
                                <asp:TextBox ID="tb_numero_artigo" class="form-control" runat="server" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Style="color: red;" runat="server" ErrorMessage="Código do artigo obrigatório!" ControlToValidate="tb_numero_artigo"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-lg-4" style="text-align: left;">
                                <p>Preço</p>
                                <asp:TextBox ID="tb_preco" class="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Style="color: red;" runat="server" ErrorMessage="Preço obrigatório!" ControlToValidate="tb_preco"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-lg-4" style="text-align: left;">
                                <p style="text-align: left;">Stock</p>
                                <asp:TextBox ID="tb_stock" class="form-control" runat="server" MaxLength="5" onkeypress="return isNumber(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Style="color: red;" runat="server" ErrorMessage="Stock inicial obrigatório!" ControlToValidate="tb_stock"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-lg-4">
                                <p style="text-align: left;">Categoria</p>
                                <asp:DropDownList ID="ddl_categoria" class="form-select" runat="server" DataTextField="categoria" DataValueField="id_categoria" DataSourceID="SqlDataSource1"></asp:DropDownList><asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString="<%$ ConnectionStrings:autoparts_ConnectionString %>" SelectCommand="SELECT * FROM [categoria]"></asp:SqlDataSource>
                            </div>
                            <div class="col-lg-4">
                                <p style="text-align: left;">Imagem</p>
                                <asp:FileUpload class="form-control" ID="fu_imagem" runat="server" />
                            </div>
                        </div>

                        <div class="row">

                            <div class="col-lg-4" style="text-align: left;">
                                <asp:DropDownList ID="ddl_marca" class="form-select" runat="server" DataTextField="nome" DataValueField="id_marca" DataSourceID="SqlDataSource2"></asp:DropDownList><asp:SqlDataSource runat="server" ID="SqlDataSource2" ConnectionString="<%$ ConnectionStrings:autoparts_ConnectionString %>" SelectCommand="SELECT * FROM [marcas]"></asp:SqlDataSource>
                            </div>
                            <div class="col-lg-4">
                                <p style="text-align: left;">Descrição do produto</p>
                                <asp:TextBox ID="tb_descricao" class="form-control" runat="server" TextMode="MultiLine" MaxLength="100"></asp:TextBox>
                            </div>
                            
                            <div class="col-lg-4">
                                <p style="text-align: center;">Imagem Atual</p>
                                <p>
                                <asp:Label ID="lbl_nao_imagem" runat="server" Text="(Não tem Imagem)" Enabled="False" Visible="False"></asp:Label></p>
                                <asp:Image ID="productImage" style="border: 1px solid black; width: 128px; height: 128px;"  runat="server" />
                            </div>
                        </div>

                        <asp:Label ID="lbl_erro" runat="server"></asp:Label>
                        
                        <asp:Button ID="btn_editar" runat="server" class="btn btn-primary" Text="Editar Produto" OnClick="btn_editar_Click"/>


                    </div>
                </div>
                <!-- End Default Card -->
            </div>
        </div>
    </section>

</main>


<script>
function isNumber(event) {
    var charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}
</script>

</asp:Content>
