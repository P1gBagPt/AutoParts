<%@ Page Title="" Language="C#" MasterPageFile="~/main_master.Master" AutoEventWireup="true" CodeBehind="ativacao_conta.aspx.cs" Inherits="AutoParts.ativacao_conta" %>

<%@ MasterType VirtualPath="~/main_master.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="margin-bottom: 350px;">
        <div class="row">
            <div class="col-md-12 align-center" style="margin-top: 55px;">
                <h2 style="color:green;">CONTA ATIVADA COM SUCESSO</h2>

                <br />

                <h3 style="color: blue;"><a href="login.aspx">LOGIN</a></h3>
            </div>
        </div>
    </div>
</asp:Content>
