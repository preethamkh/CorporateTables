<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Goback.aspx.cs" Inherits="Goback" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Error Page</title>
    <link href="css/login.css" rel="stylesheet" />
</head>
<body>
    <form id="login" runat="server">
        <div>
            <h2>Oops!! An error occured</h2>
            <div style="font-size: 14px;">
                Something wrong about the data for <strong>the event</strong> or a file has been locked, IT has been notified.
                Alternatively you may wish to try accessing the site at a later stage.<br /><br />
                Else, click <a href="AllCorporateTables.aspx">here</a> to go back.
            </div>
        </div>
    </form>
</body>
</html>
