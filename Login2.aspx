<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login2.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/login.css" rel="stylesheet" />
    <link rel="stylesheet" href="http://fonts.googleapis.com/css?family=Open+Sans:400,700" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">

            <div id="login">

                <div>

                    <fieldset class="clearfix">

                        <div><span style="display: block!important;">Test</span></div>
                        <p>
                            <span class="fontawesome-user"></span>
                            <asp:TextBox type="text" placeholder="iMIS Username" onblur="if(this.value == '') this.value = 'Username'" onfocus="if(this.value == 'Username') this.value = ''" required="" runat="server"></asp:TextBox>
                        </p>
                        <!-- JS because of IE support; better: placeholder="Username" -->
                        <p>
                            <span class="fontawesome-lock"></span>
                            <asp:TextBox type="password" placeholder="Password" onblur="if(this.value == '') this.value = 'Password'" onfocus="if(this.value == 'Password') this.value = ''" required="" runat="server"></asp:TextBox>
                            <%--<asp:TextBox type="password" value="Password" onblur="if(this.value == '') this.value = 'Password'" onfocus="if(this.value == 'Password') this.value = ''" required="" runat="server"></asp:TextBox>--%>
                        </p>
                        <!-- JS because of IE support; better: placeholder="Password" -->
                        <p>
                            <asp:Button type="submit" Text="Sign In" runat="server" />
                        </p>

                    </fieldset>

                </div>

                <%--<p>Not a member? <a href="#">Sign up now</a><span class="fontawesome-arrow-right"></span></p>--%>
            </div>
            <!-- end login -->

        </div>
    </form>
</body>
</html>
