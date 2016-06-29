using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EvoDescription
/// </summary>
public class EvoDescription : Attribute
{
    public String Description;
    public EvoDescription(String sDescription)
    {
        this.Description = sDescription;
    }
}