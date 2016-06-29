using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

/// <summary>
/// Summary description for iMISCustomField
/// </summary>
public class iMISCustomField
{
    private iMISField iMISFieldField;

    private object valueField;

    /// <remarks/>
    public iMISField iMISField
    {
        get
        {
            return this.iMISFieldField;
        }
        set
        {
            this.iMISFieldField = value;
        }
    }

    /// <remarks/>
    public object Value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }

    public void UpdateiMISField(String sDescription)
    {
        try
        {
            var type = typeof(iMISField);
            foreach (FieldInfo objField in type.GetFields())
            {
                EvoDescription objED = Attribute.GetCustomAttribute(objField, typeof(EvoDescription)) as EvoDescription;
                if ((objED != null) && (objED.Description == sDescription))
                    this.iMISField = (iMISField)objField.GetValue(null);
            }
        }
        finally
        {

        }
    }
}