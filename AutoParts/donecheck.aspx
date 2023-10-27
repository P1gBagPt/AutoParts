<%@ Page Title="" Language="C#" MasterPageFile="~/main_master.Master" AutoEventWireup="true" CodeBehind="donecheck.aspx.cs" Inherits="AutoParts.donecheck" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="shopify-cart checkout-wrap padding-large">
        <div class="container">
            <div class="form-group">
                <div class="row d-flex flex-wrap">
                    <h2 class="section-title" style="text-transform: none;">Obrigado pela sua compra, encomenda numero <asp:Label ID="lbl_num_encomenda" runat="server"></asp:Label></h2>
                    <h3 style="text-transform: none;">Foi enviado um PDF com os detalhes da encomenda para o email <asp:Label ID="lbl_email_utilizador" runat="server"></asp:Label></h3>


                </div>
            </div>
        </div>
    </section>
</asp:Content>
