<%@ Page Title="" Language="C#" MasterPageFile="~/main_master.Master" AutoEventWireup="true" CodeBehind="teste.aspx.cs" Inherits="AutoParts.teste" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="form-group">
    <div class="google-btn">
        <div class="google-icon-wrapper">
            <img class="google-icon" src="https://upload.wikimedia.org/wikipedia/commons/5/53/Google_%22G%22_Logo.svg" />
        </div>
        <asp:LinkButton ID="btn_googleLogin" runat="server" OnClick="btn_googleLogin_Click" CausesValidation="False"> <p class="btn-text"><b>Register with google</b></p> </asp:LinkButton>
    </div>
</div>
</asp:Content>
