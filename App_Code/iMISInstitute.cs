using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for iMISInstitute
/// </summary>
public class iMISInstitute
{
    String sCode = String.Empty;
    String sInstituteName = String.Empty;
    String sCompany = String.Empty;
    String sCategoryCode = String.Empty;
    String sCategoryDescription = String.Empty;
    String sMemberState = String.Empty;

    public iMISInstitute()
    {

    }


    public String Code
    {
        get { return this.sCode; }
        set { this.sCode = value; }
    }

    public String InstituteName
    {
        get { return this.sInstituteName; }
        set { this.sInstituteName = value; }
    }

    public String Company
    {
        get { return this.sCompany; }
        set { this.sCompany = value; }
    }

    public String CategoryCode
    {
        get { return this.sCategoryCode; }
        set { this.sCategoryCode = value; }
    }

    public String CategoryDescription
    {
        get { return this.sCategoryDescription; }
        set { this.sCategoryDescription = value; }
    }

    public String MemberState
    {
        get { return this.sMemberState; }
        set { this.sMemberState = value; }
    }

}