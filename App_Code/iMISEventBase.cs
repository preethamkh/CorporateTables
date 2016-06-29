using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class iMISEventBase
{
    public iMISEventBase()
    {

    }

    Int32 iMaxAttendees = 0;
    String sEventCode = String.Empty;
    String sTitle = String.Empty;
    String sVenue = String.Empty;
    DateTime dtStartDate = DateTime.MinValue;

    #region PROPERTIES

    public Int32 MaxAttendees
    {
        get { return this.iMaxAttendees; }
        set { this.iMaxAttendees = value; }
    }

    public String EventCode
    {
        get { return this.sEventCode; }
        set { this.sEventCode = value; }
    }

    public String Title
    {
        get { return this.sTitle; }
        set { this.sTitle = value; }
    }

    public String Venue
    {
        get { return this.sVenue; }
        set { this.sVenue = value; }
    }

    public DateTime StartDate
    {
        get { return this.dtStartDate; }
        set { this.dtStartDate = value; }
    }

    #endregion
}