<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="alterar_pass_input.aspx.cs" Inherits="AutoParts.alterar_pass_input" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Autoparts Alterar Password</title>
    <link href="assets/css/formsRLAPass.css" rel="stylesheet" />

    <style>
        .home a {
            text-decoration: none;
        }

            .home a:visited {
                text-decoration: none;
            }

            .home a:hover {
                text-decoration: none;
                color: black;
            }

            .home a:focus {
                text-decoration: none;
                color: black;
            }

            .home a:active {
                text-decoration: none;
                color: black;
            }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div class="main">
            <section class="signup">
                <div class="container">
                    <div class="signup-content">
                        <div class="home">
                            <h3>
                                <a href="index.aspx" class="form-title">Início</a>
                            </h3>
                        </div>
                        <div id="signup-form" class="signup-form">
                            <h2 class="form-title">Alterar Password</h2>
                            <div class="form-group">
                                <asp:TextBox ID="tb_email" class="form-input" runat="server" placeholder="Email da conta a alterar password" TextMode="Email" MaxLength="255"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Style="color: red;" runat="server" ErrorMessage="Email Obrigatório" ControlToValidate="tb_email"></asp:RequiredFieldValidator>
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
