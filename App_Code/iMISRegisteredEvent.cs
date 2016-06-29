using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class iMISRegisteredEvent : iMISEventBase
{
	public iMISRegisteredEvent()
	{
		
	}

    DateTime dtRegistrationCutOffDate = DateTime.MinValue;
    String sOrderNumber = String.Empty;
    String sCoordinatorEmail = String.Empty;
    String sCoordinatorPhone = String.Empty;
    String sStartTime = String.Empty;
    String sEndTime = String.Empty;
    String attendee = String.Empty;
    String urlAlias = String.Empty;
    String eventInvoicePath = String.Empty;
    String allRelatedInvoices = String.Empty;
    String totalCharges = String.Empty;
    String bt_id = String.Empty;
    String last_first = String.Empty;
    String Company = String.Empty;
    String numPeople = String.Empty;
    String registrationType = String.Empty;
    int tableNumber = 0;

    #region PROPERTIES

    public String RegistrationType
    {
        get { return this.registrationType; }
        set { this.registrationType = value; }
    }

    public Int32 TableNumber
    {
        get { return this.tableNumber; }
        set { this.tableNumber = value; }
    }

    public String NumPeople
    {
        get { return this.numPeople; }
        set { this.numPeople = value; }
    }

    public String COMPANY
    {
        get { return this.Company; }
        set { this.Company = value; }
    }

    public String BT_ID
    {
        get { return this.bt_id; }
        set { this.bt_id = value; }
    }

    public String Last_First
    {
        get { return this.last_first; }
        set { this.last_first = value; }
    }

    public String TotalCharges
    {
        get { return this.totalCharges; }
        set { this.totalCharges = value; }
    }

    public String AllRelatedInvoices
    {
        get { return this.allRelatedInvoices; }
        set { this.allRelatedInvoices = value; }
    }

    public String UrlAlias
    {
        get { return this.urlAlias; }
        set { this.urlAlias = value; }
    }

    public String EventInvoicePath
    {
        get { return this.eventInvoicePath; }
        set { this.eventInvoicePath = value; }
    }


    public String Attendee
    {
        get { return this.attendee; }
        set { this.attendee = value; }
    }

    public DateTime RegistrationCutOffDate
    {
        get { return this.dtRegistrationCutOffDate; }
        set { this.dtRegistrationCutOffDate = value; }
    }

    public String OrderNumber
    {
        get { return this.sOrderNumber; }
        set { this.sOrderNumber = value; }
    }

    public String CoordinatorEmail
    {
        get { return this.sCoordinatorEmail; }
        set { this.sCoordinatorEmail = value; }
    }

    public String CoordinatorPhone
    {
        get { return this.sCoordinatorPhone; }
        set { this.sCoordinatorPhone = value; }
    }

    public String StartTime
    {
        get { return this.sStartTime; }
        set { this.sStartTime = value; }
    }

    public String EndTime
    {
        get { return this.sEndTime; }
        set { this.sEndTime = value; }
    }

    #endregion
}