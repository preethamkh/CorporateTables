using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for iMISBadge
/// </summary>
public class iMISBadge
{
    Int32 iBadgeNumber = 1;
    Double dOrderNumber = 0;
    String sDelegate = String.Empty;
    String sPrefix = String.Empty;
    String sTitle = String.Empty;
    String sFirstName = String.Empty;
    String sLastName = String.Empty;
    String sFullName = String.Empty;
    String sInstituteName = String.Empty;
    String sStateProvince = String.Empty;
    String sSpecialRequirements = String.Empty;

	public iMISBadge()
	{
	}

    private int badgeNumberField;

    private double orderNumberField;

    private string delegateField;

    private string prefixField;

    private string titleField;

    private string firstNameField;

    private string lastNameField;

    private string instituteNameField;

    private string stateProvinceField;

    private string specialRequirementsField;

    public String FullName
    {
        get { return this.Prefix + " " + this.FirstName + " " + this.LastName; }
    }

    /// <remarks/>
    public int BadgeNumber
    {
        get
        {
            return this.badgeNumberField;
        }
        set
        {
            this.badgeNumberField = value;
        }
    }

    internal String BadgeType
    {
        get { return this.Delegate == String.Empty ? "PRIMARY" : "SECONDARY"; }
    }

    /// <remarks/>
    public double OrderNumber
    {
        get
        {
            return this.orderNumberField;
        }
        set
        {
            this.orderNumberField = value;
        }
    }

    /// <remarks/>
    public string Delegate
    {
        get
        {
            return this.delegateField;
        }
        set
        {
            this.delegateField = value;
        }
    }

    /// <remarks/>
    public string Prefix
    {
        get
        {
            return this.prefixField;
        }
        set
        {
            this.prefixField = value;
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
    public string InstituteName
    {
        get
        {
            return this.instituteNameField;
        }
        set
        {
            this.instituteNameField = value;
        }
    }

    /// <remarks/>
    public string StateProvince
    {
        get
        {
            return this.stateProvinceField;
        }
        set
        {
            this.stateProvinceField = value;
        }
    }

    /// <remarks/>
    public string SpecialRequirements
    {
        get
        {
            return this.specialRequirementsField;
        }
        set
        {
            this.specialRequirementsField = value;
        }
    }
}


public class p_DeleteBadge
{
    Int32 iBadgeNumber = 1;
    Double dOrderNumber = 0;

    public p_DeleteBadge()
    {

    }

    public Int32 BadgeNumber
    {
        get { return this.iBadgeNumber; }
        set { this.iBadgeNumber = value; }
    }

    public Double OrderNumber
    {
        get { return this.dOrderNumber; }
        set { this.dOrderNumber = value; }
    }
}