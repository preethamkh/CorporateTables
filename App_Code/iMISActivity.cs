using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for iMISActivity
/// </summary>
public class iMISActivity
{
    private decimal amountField;

    private decimal quantityField;

    private string contactIDField;

    private string eventCodeField;

    private string descriptionField;

    private string activityCodeField;

    private System.DateTime transactionDateField;

    private System.DateTime eventDateField;

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
    public decimal Quantity
    {
        get
        {
            return this.quantityField;
        }
        set
        {
            this.quantityField = value;
        }
    }

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
    public string EventCode
    {
        get
        {
            return this.eventCodeField;
        }
        set
        {
            this.eventCodeField = value;
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
    public string ActivityCode
    {
        get
        {
            if (this.activityCodeField == String.Empty)
                return "CORPGUEST";

            return this.activityCodeField;
        }
        set
        {
            this.activityCodeField = value;
        }
    }

    /// <remarks/>
    public System.DateTime TransactionDate
    {
        get
        {
            return this.transactionDateField;
        }
        set
        {
            this.transactionDateField = value;
        }
    }

    /// <remarks/>
    public System.DateTime EventDate
    {
        get
        {
            return this.eventDateField;
        }
        set
        {
            this.eventDateField = value;
        }
    }
}