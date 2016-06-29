using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class ErrorPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string errorMessage = "";
        Exception ex = Server.GetLastError();
        Helper helper = new Helper();

        // send an email to it.support notifying regd. the error


        if(ex != null)
        {
            errorMessage = ex.GetBaseException().ToString();
            Response.Redirect("Goback.aspx");
            helper.SendErrorEmail(errorMessage);
        }
    }
}