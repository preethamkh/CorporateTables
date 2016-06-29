using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for iMISUser
/// </summary>
public class iMISUser
{
	public iMISUser()
	{
	}

    private string contactIDField;

    private string userNameField;

    private string passwordField;

    private string firstNameField;

    private string lastNameField;

    private string memberTypeField;

    private bool isStaffField;

    private System.DateTime expiresOnField;

    private iMISSubscriptionInfo subscriptionInfoField;

    /// <remarks/>
    public string ContactID
    {
        get
        {
            return this.contactIDField;
        }
        set
        {
            this.contactIDField = value;
        }
    }

    /// <remarks/>
    public string UserName
    {
        get
        {
            return this.userNameField;
        }
        set
        {
            this.userNameField = value;
        }
    }

    /// <remarks/>
    public string Password
    {
        get
        {
            return this.passwordField;
        }
        set
        {
            this.passwordField = value;
        }
    }

    /// <remarks/>
    public string FirstName
    {
        get
        {
            return this.firstNameField;
        }
        set
        {
            this.firstNameField = value;
        }
    }

    /// <remarks/>
    public string LastName
    {
        get
        {
            return this.lastNameField;
        }
        set
        {
            this.lastNameField = value;
        }
    }

    /// <remarks/>
    public string MemberType
    {
        get
        {
            return this.memberTypeField;
        }
        set
        {
            this.memberTypeField = value;
        }
    }

    /// <remarks/>
    public bool IsStaff
    {
        get
        {
            return this.isStaffField;
        }
        set
        {
            this.isStaffField = value;
        }
    }

    /// <remarks/>
    public System.DateTime ExpiresOn
    {
        get
        {
            return this.expiresOnField;
        }
        set
        {
            this.expiresOnField = value;
        }
    }

    /// <remarks/>
    public iMISSubscriptionInfo SubscriptionInfo
    {
        get
        {
            return this.subscriptionInfoField;
        }
        set
        {
            this.subscriptionInfoField = value;
        }
    }
}