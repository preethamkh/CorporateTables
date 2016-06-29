using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for iMISSubscription
/// </summary>
public class iMISSubscription
{
	public iMISSubscription()
	{
		
	}

    private decimal amountField;

    private string productCodeField;

    private string titleField;

    private string descriptionField;

    private string netPriceField;

    private System.DateTime billBeginDateField;

    private System.DateTime billThruDateField;

    private bool sellOnWebField;

    private bool isMembershipPaymentField;

    private bool isResearchPaymentField;

    /// <remarks/>
    public decimal Amount
    {
        get
        {
            return this.amountField;
        }
        set
        {
            this.amountField = value;
        }
    }

    /// <remarks/>
    public string ProductCode
    {
        get
        {
            return this.productCodeField;
        }
        set
        {
            this.productCodeField = value;
        }
    }

    /// <remarks/>
    public string Title
    {
        get
        {
            return this.titleField;
        }
        set
        {
            this.titleField = value;
        }
    }

    /// <remarks/>
    public string Description
    {
        get
        {
            return this.descriptionField;
        }
        set
        {
            this.descriptionField = value;
        }
    }

    /// <remarks/>
    public string NetPrice
    {
        get
        {
            return this.netPriceField;
        }
        set
        {
            this.netPriceField = value;
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
    public System.DateTime BillThruDate
    {
        get
        {
            return this.billThruDateField;
        }
        set
        {
            this.billThruDateField = value;
        }
    }

    /// <remarks/>
    public bool SellOnWeb
    {
        get
        {
            return this.sellOnWebField;
        }
        set
        {
            this.sellOnWebField = value;
        }
    }

    /// <remarks/>
    public bool IsMembershipPayment
    {
        get
        {
            return this.isMembershipPaymentField;
        }
        set
        {
            this.isMembershipPaymentField = value;
        }
    }

    /// <remarks/>
    public bool IsResearchPayment
    {
        get
        {
            return this.isResearchPaymentField;
        }
        set
        {
            this.isResearchPaymentField = value;
        }
    }
}