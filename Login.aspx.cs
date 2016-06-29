using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using iMISWebReference;

public partial class Login : System.Web.UI.Page
{

    MembershipWebService membershipService = new MembershipWebService();
    Helper helper = new Helper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.Form.DefaultButton = this.BTN_Submit.UniqueID;

            var objIU = helper.GetUser();
            if (objIU != null)
            {
                Response.Redirect("~/AllCorporateTables.aspx", false);
            }
        }
    }


    protected void BTN_Login_Click(object sender, EventArgs e)
    {
        var result = CheckUserLogin();

        if (result)
        {
            // Redirect to AllCorporateTables.aspx
            Response.Redirect("~/AllCorporateTables.aspx", true);
        }
        else
            lbl_LoginStatus.Text = "failed to login";
    }


    private bool CheckUserLogin()
    {
        string username = "";
        string password = "";
        bool loginResult = false;

        //username = ConfigurationManager.AppSettings["Username"].ToString();
        //password = ConfigurationManager.AppSettings["Password"].ToString();

        //or    

        String txtUsername = this.txt_username.Text.ToString().Trim();
        String txtPassword = this.txt_password.Text.ToString().Trim();
        
        if (!String.IsNullOrEmpty(txtUsername) && !String.IsNullOrEmpty(txtPassword))
        {
            username = txtUsername;
            password = txtPassword;
        }

        // Get title, firstname and lastname and print

        //var result = membershipService.LoginUser(username, password, true);
        iMISUser objUser = helper.AuthenticateUser(username, password);

        //if (result.ToString().Equals("succeeded", StringComparison.InvariantCultureIgnoreCase))
        if(objUser != null)
        {
            loginResult = true;            
            helper.StoreUser(objUser);
        }
        else
        {
            loginResult = false;
        }

        return loginResult;
    }
}