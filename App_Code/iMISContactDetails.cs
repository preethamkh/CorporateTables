using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for iMISContactDetails
/// </summary>
public class iMISContactDetails : iMISContact
{
    String sEmail = String.Empty;
    String sFax = String.Empty;
    String sHomePhone = String.Empty;
    String sInstituteID = String.Empty;
    String sInstituteName = String.Empty;
    String sInstituteMemberType = String.Empty;
    String sPrefix = String.Empty;
    String sSuffix = String.Empty;
    String sTitle = String.Empty;
    String sWorkPhone = String.Empty;
    String sBillingCategory = String.Empty;
    String sMemberState = String.Empty;
    String sTollFreePhone = String.Empty;
    Boolean bCreateNewUser = false;
    Boolean bIsInstitute = false;
    iMISUser objIU = null;
    iMISSubscriptionInfo objISI = null;
    List<iMISAddress> lAddresses = null;
    List<iMISCustomField> lCustomFields = null;

    public bool IsMemberTypeReadOnly { get; set; }

    public iMISContactDetails()
    {
        IsMemberTypeReadOnly = false;
    }


    public String Email
    {
        get { return this.sEmail; }
        set { this.sEmail = value; }
    }

    public String Fax
    {
        get { return this.sFax; }
        set { this.sFax = value; }
    }

    public String HomePhone
    {
        get { return this.sHomePhone; }
        set { this.sHomePhone = value; }
    }

    public String InstituteID
    {
        get { return this.sInstituteID; }
        set { this.sInstituteID = value; }
    }

    public String InstituteName
    {
        get { return this.sInstituteName; }
        set { this.sInstituteName = value; }
    }

    public String InstituteMemberType
    {
        get { return this.sInstituteMemberType; }
        set { this.sInstituteMemberType = value; }
    }

    public String Prefix
    {
        get { return this.sPrefix; }
        set { this.sPrefix = value; }
    }

    public String Suffix
    {
        get { return this.sSuffix; }
        set { this.sSuffix = value; }
    }

    public String Title
    {
        get { return this.sTitle; }
        set { this.sTitle = value; }
    }

    public String WorkPhone
    {
        get { return this.sWorkPhone; }
        set { this.sWorkPhone = value; }
    }

    public String BillingCategory
    {
        get { return this.sBillingCategory; }
        set { this.sBillingCategory = value; }
    }

    internal String MemberState
    {
        get { return this.sMemberState; }
        set { this.sMemberState = value; }
    }

    public String TollFreePhone
    {
        get { return this.sTollFreePhone; }
        set { this.sTollFreePhone = value; }
    }

    public Boolean CreateNewUser
    {
        get { return this.bCreateNewUser; }
        set { this.bCreateNewUser = value; }
    }

    public Boolean IsInstitute
    {
        get { return this.bIsInstitute; }
        set { this.bIsInstitute = value; }
    }

    public iMISUser UserInfo
    {
        get
        {
            if (this.objIU == null)
                this.objIU = new iMISUser();

            return this.objIU;
        }
        set { this.objIU = value; }
    }

    public iMISSubscriptionInfo SubscriptionInfo
    {
        get
        {
            if (this.objISI == null)
                this.objISI = new iMISSubscriptionInfo();

            return this.objISI;
        }
        set { this.objISI = value; }
    }

    public List<iMISAddress> Addresses
    {
        get
        {
            if (this.lAddresses == null)
                this.lAddresses = new List<iMISAddress>();

            return this.lAddresses;
        }
        set { this.lAddresses = value; }
    }

    public List<iMISCustomField> CustomFields
    {
        get
        {
            if (this.lCustomFields == null)
                this.lCustomFields = new List<iMISCustomField>();

            return this.lCustomFields;
        }
        set { this.lCustomFields = value; }
    }            

}

