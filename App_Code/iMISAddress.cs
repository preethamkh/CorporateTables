using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for iMISAddress
/// </summary>
public class iMISAddress
{
	public iMISAddress()
	{
	}

    private string streetAddress1Field;

    private string streetAddress2Field;

    private string streetAddress3Field;

    private string cityField;

    private string countryField;

    private string postCodeField;

    private string stateField;

    private string fullAddressField;

    private bool isPreferredMailingField;

    /// <remarks/>
    public string StreetAddress1
    {
        get
        {
            return this.streetAddress1Field;
        }
        set
        {
            this.streetAddress1Field = value;
        }
    }

    /// <remarks/>
    public string StreetAddress2
    {
        get
        {
            return this.streetAddress2Field;
        }
        set
        {
            this.streetAddress2Field = value;
        }
    }

    /// <remarks/>
    public string StreetAddress3
    {
        get
        {
            return this.streetAddress3Field;
        }
        set
        {
            this.streetAddress3Field = value;
        }
    }

    /// <remarks/>
    public string City
    {
        get
        {
            return this.cityField;
        }
        set
        {
            this.cityField = value;
        }
    }

    /// <remarks/>
    public string Country
    {
        get
        {
            return this.countryField;
        }
        set
        {
            this.countryField = value;
        }
    }

    /// <remarks/>
    public string PostCode
    {
        get
        {
            return this.postCodeField;
        }
        set
        {
            this.postCodeField = value;
        }
    }

    /// <remarks/>
    public string State
    {
        get
        {
            return this.stateField;
        }
        set
        {
            this.stateField = value;
        }
    }

    /// <remarks/>
    public string FullAddress
    {
        get
        {
            return this.fullAddressField;
        }
        set
        {
            this.fullAddressField = value;
        }
    }

    /// <remarks/>
    public bool IsPreferredMailing
    {
        get
        {
            return this.isPreferredMailingField;
        }
        set
        {
            this.isPreferredMailingField = value;
        }
    }
}