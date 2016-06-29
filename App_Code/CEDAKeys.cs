using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EvoParameter
/// </summary>
public class CEDAKeys
{
    Int32 iID = 0;
        Int32 iTypeID = 0;
        Int32 iCount = 1;
        String sCode = String.Empty;
        String sDescription = String.Empty;
        List<String> lValues = null;

        public CEDAKeys()
        {

        }

        #region PROPERTIES

            public Int32 ID
            {
                get { return this.iID; }
                set { this.iID = value; }
            }

            public Int32 TypeID
            {
                get { return this.iTypeID; }
                set { this.iTypeID = value; }
            }

            public Int32 Count
            {
                get { return this.iCount; }
                set { this.iCount = value; }
            }

            public String Code
            {
                get { return this.sCode.Trim(); }
                set { this.sCode = value.Trim(); }
            }

            public String Description
            {
                get { return this.sDescription.Trim(); }
                set { this.sDescription = value.Trim(); }
            }

            public List<String> Values
            {
                get
                {
                    if (this.lValues == null)
                        this.lValues = new List<String>();

                    return this.lValues;
                }
                set { this.lValues = value; }
            }

        #endregion

        #region METHODS

            #region public Int32 CompareTo()
            public Int32 CompareTo(CEDAKeys objEP)
            {
                return Decimal.Parse(objEP.Description).CompareTo(Decimal.Parse(this.Description));
            }
            #endregion

        #endregion

    }