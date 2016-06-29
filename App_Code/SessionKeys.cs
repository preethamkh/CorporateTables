using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SessionKeys
/// </summary>
public class SessionKeys
{
    public static String User
    {
        get { return "User"; }
    }

    public static String RedirectUrl
    {
        get { return "RedirectUrl"; }
    }

    public static String SearchParameters
    {
        get { return "SearchParameters"; }
    }

    public static String LastVisitedPage
    {
        get { return "LastVisitedPage"; }
    }

    /// <summary>
    /// Has the user informed regarding the membership renewal?
    /// </summary>
    public static String IsUserInformed
    {
        get { return "IsUserInformed"; }
    }
}