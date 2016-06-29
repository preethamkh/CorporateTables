<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login using your iMIS credentials</title>
    <link href="css/login.css" rel="stylesheet" />
</head>
<body>
    <form id="login" runat="server">
    <div>
        <h1>Log In</h1>
			<fieldset id="inputs">
				<asp:TextBox id="txt_username" type="text" placeholder="iMIS Username" EnableViewState="true" required="" runat="server" />
				<asp:TextBox id="txt_password" type="password" placeholder="Password" EnableViewState="true" required="" runat="server" />
			</fieldset>
			<fieldset id="actions">
				<asp:Button type="submit" ID="BTN_Submit" Text="Log in" runat="server" OnClick="BTN_Login_Click" />
                <asp:Label runat="server" ID="lbl_LoginStatus"></asp:Label>
				<%--<a href="#">Forgot your password?</a><a href="#">Register</a>--%>
			</fieldset>
    </div>
    </form>
    <script src="http://code.jquery.com/jquery-1.6.3.min.js"></script>
</body>
</html>
