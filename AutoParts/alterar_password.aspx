<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="alterar_password.aspx.cs" Inherits="AutoParts.alterar_password" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Autoparts Alterar Password</title>
    <link href="assets/css/formsRLAPass.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="main">
            <section class="signup">
                <!-- <img src="images/signup-bg.jpg" alt=""> -->
                <div class="container">
                    <div class="signup-content">
                        <div id="signup-form" class="signup-form">
                            <h2 class="form-title">Alterar Password</h2>
                            <div class="form-group">
                                <asp:TextBox ID="tb_pass_atual" class="form-input" runat="server" placeholder="Password Atual" TextMode="Password" MaxLength="255"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" style="color: red;" runat="server" ErrorMessage="Password Atual obrigatória" ControlToValidate="tb_pass_atual"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <asp:TextBox ID="tb_password1" class="form-input" placeholder="Nova Password" runat="server" TextMode="Password" MaxLength="255"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" style="color: red;" runat="server" ErrorMessage="Password Nova obrigatória" ControlToValidate="tb_password1"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <asp:TextBox ID="tb_password2" class="form-input" placeholder="Nova Password Novamente" runat="server" MaxLength="255" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" style="color: red;" runat="server" ErrorMessage="Introduza a password novamente" ControlToValidate="tb_password2"></asp:RequiredFieldValidator>
                            </div>
                            <asp:Label ID="lbl_erro" runat="server" Enabled="False" Visible="False" ForeColor="red"></asp:Label>
                            <div class="form-group">
                                <asp:Button name="submit" ID="submit" class="form-submit" runat="server" Text="Alterar Password" OnClick="submit_Click" Style="background: red; cursor: pointer; color: white;" />
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </div>
    </form>
</body>
</html>
