using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for iMISSubscriptionInfo
/// </summary>
public class iMISSubscriptionInfo
{
	public iMISSubscriptionInfo()
	{
	}

    private string contactIDField;

    private string billToContactIDField;

    private string membershipTypeField;

    private System.DateTime paidThruField;

    private System.DateTime billBeginDateField;

    private System.DateTime billedDateField;

    private bool nationalOfficeExistField;

    private iMISSubscription[] subscriptionsField;

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
    public string BillToContactID
    {
        get
        {
            return this.billToContactIDField;
        }
        set
        {
            this.billToContactIDField = value;
        }
    }

    /// <remarks/>
    public string MembershipType
    {
        get
        {
            return this.membershipTypeField;
        }
        set
        {
            this.membershipTypeField = value;
        }
    }

    /// <remarks/>
    public System.DateTime PaidThru
    {
        get
        {
            return this.paidThruField;
        }
        set
        {
            this.paidThruField = value;
        }
    }

    /// <remarks/>
    public System.DateTime BillBeginDate
    {
        get
        {
            return this.billBeginDateField;
        }
        set
        {
            this.billBeginDateField = value;
        }
    }

    /// <remarks/>
    public System.DateTime BilledDate
    {
        get
        {
            return this.billedDateField;
        }
        set
        {
            this.billedDateField = value;
        }
    }

    /// <remarks/>
    public bool NationalOfficeExist
    {
        get
        {
            return this.nationalOfficeExistField;
        }
        set
        {
            this.nationalOfficeExistField = value;
        }
    }

    /// <remarks/>
    public iMISSubscription[] Subscriptions
    {
        get
        {
            return this.subscriptionsField;
        }
        set
        {
            this.subscriptionsField = value;
        }
    }
}