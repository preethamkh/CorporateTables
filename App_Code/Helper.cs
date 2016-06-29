using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Configuration;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Data.Sql;
using Asi.iBO;
using Asi.iBO.ContactManagement;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Net;

/// <summary>
/// Summary description for Helper
/// </summary>
public class Helper
{
    private static string connString = ConfigurationManager.ConnectionStrings["iMISConnectionString"].ToString();

    // Save error to log file
    public void SaveError(Exception ex)
    {
        try
        {
            if (ex.Message == "Thread was being aborted.")
                return;

            // Error message to log
            StringBuilder sb = new StringBuilder();

            sb.Append("-------------------------------------------------------- \r\n");
            sb.Append(ex.Message + " \r\n");
            sb.Append("-------------------------------------------------------- \r\n");
            sb.Append("(*)DATE: " + DateTime.Now + " <br>");
            sb.Append("(*)ERROR MESSAGE: \r\n");

            if (ex.InnerException != null)
            {
                sb.Append(ex.InnerException.ToString() + " \r\n");
            }

            sb.Append("(*)SERVER ADDRESS: \r\n");
            sb.Append(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] + " \r\n");
            sb.Append("(*)SOURCE: \r\n");
            sb.Append(ex.StackTrace + " \r\n\r\n");

            // Append stringbuilder to log file
            string sFileName = DateTime.Now.ToString("yyyMMdd") + ".txt";
            string sPath = @ConfigurationManager.AppSettings["Path.Logs"].ToString() + sFileName;

            if (!File.Exists(sPath))
            {
                File.Create(sPath);
                TextWriter tw = new StreamWriter(sPath);
                tw.WriteLine(sb.ToString());
                tw.Close();
            }
            else if (File.Exists(sPath))
            {
                TextWriter tw = new StreamWriter(sPath, true);
                tw.WriteLine(sb.ToString());
                tw.Close();
            }
        }
        finally
        {
            Console.WriteLine("BHP to end up in teens!");
        }
    }

    public DataTable GetEventCodes()
    {
        DataTable subjects = new DataTable();

        using(SqlConnection con = new SqlConnection(connString))
        {
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("CEDA_GetEventCodes", con);
                adapter.Fill(subjects);
            }
            finally
            {

            }

            return subjects;
        }
    }

    public void UpdateTableNumber(string orderNumber, string tableNumber)
    {
        if (String.IsNullOrEmpty(orderNumber))
            throw new Exception("The 'order number' parameter is empty!");

        if (String.IsNullOrEmpty(tableNumber))
            throw new Exception("The 'table number' parameter is empty!");

        try
        {
            SqlConnection sqlConnection = new SqlConnection(connString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("dbo.CEDA_UpdateTableNumber", sqlConnection)
            {
                CommandType = CommandType.StoredProcedure
            };

            sqlCommand.Parameters.AddWithValue("@OrderNumber", orderNumber);
            sqlCommand.Parameters.AddWithValue("@TableNumber", tableNumber);

            sqlCommand.ExecuteNonQuery();
        }
        catch(Exception ex)
        {
            SaveError(ex);
        }
    }

    public List<iMISRegisteredEvent> GetRegisteredEvents(string eventCode)
    //public List<iMISRegisteredEvent> GetRegisteredEvents()
    {
        if (String.IsNullOrEmpty(eventCode))
            throw new Exception("The 'EventCode' parameter is empty!");

        List<iMISRegisteredEvent> lRegisteredEvents = new List<iMISRegisteredEvent>();

        try
        {
            DateTime startDate = new DateTime();
            DateTime fStartDate = new DateTime();
            DateTime fEndDate = new DateTime();
            DateTime registrationCutOff = new DateTime();
            int tableNum = 0;

            SqlConnection sqlConnection = new SqlConnection(connString);
            sqlConnection.Open();

            //SqlCommand sqlCommand = new SqlCommand("evo.sp_GetRegisteredEvents", sqlConnection)
            SqlCommand sqlCommand = new SqlCommand("CEDA_AllCorporateTables", sqlConnection)
            {
                CommandType = CommandType.StoredProcedure
            };

            //sqlCommand.Parameters.AddWithValue("@ContactID", sContactID);
            sqlCommand.Parameters.AddWithValue("@EventCode", eventCode); //ignore case

            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
            DataSet objDS = new DataSet();
            da.Fill(objDS);

            // Check if the dataset returned is null
            if ((objDS == null) || (objDS.Tables == null) || (objDS.Tables.Count <= 0) || (objDS.Tables[0].Rows == null))
                return null;


            lRegisteredEvents = objDS.Tables[0].AsEnumerable().Select(row => new iMISRegisteredEvent
            {
                OrderNumber = row["OrderNumber"].ToString(),
                RegistrationCutOffDate = DateTime.TryParse(row["CutOffDate"].ToString(), out registrationCutOff) ? DateTime.Parse(row["CutOffDate"].ToString()) : DateTime.Now,
                EventCode = row["EventCode"].ToString(),
                Title = row["Title"].ToString(),
                StartDate = DateTime.TryParse(row["StartDate"].ToString(), out startDate) ? DateTime.Parse(row["StartDate"].ToString()) : DateTime.Now,
                CoordinatorEmail = row["CoordinatorEmail"].ToString(),
                CoordinatorPhone = row["CoordinatorPhone"].ToString(),
                StartTime = DateTime.TryParse(row["FunctionStartDate"].ToString(), out fStartDate) ? DateTime.Parse(row["FunctionStartDate"].ToString()).ToString("hh:mm tt") : "",
                EndTime = DateTime.TryParse(row["FunctionEndDate"].ToString(), out fEndDate) ? DateTime.Parse(row["FunctionEndDate"].ToString()).ToString("hh:mm tt") : "",
                Venue = String.Concat(row["StreetAddress1"].ToString(), ",", row["StreetAddress2"].ToString(), ",", row["StreetAddress3"].ToString(), ",", row["City"].ToString(), ",", row["State"].ToString(), ",", row["PostCode"].ToString(), ",", row["Country"].ToString()),
                BT_ID = row["ST_ID"].ToString(), // Using BT_ID - requested by SB
                Last_First = row["LAST_FIRST"].ToString(),
                COMPANY = row["Company"].ToString(),
                NumPeople = row["NumPeople"].ToString(),
                MaxAttendees = Int32.Parse(row["MaxAttendees"].ToString()),
                RegistrationType = row["RegistrationType"].ToString(),
                TableNumber = Int32.TryParse(row["UF_9"].ToString(), out tableNum) ? Int32.Parse(row["UF_9"].ToString()) : 0
            }).ToList();


            // Append stringbuilder to log file
            var sb = new StringBuilder();
            foreach(var events in lRegisteredEvents)
            {
                sb.Append(events.OrderNumber);
            }
            
            string sFileName = "temporaryfile.txt";
            string sPath = @"C:\inetpub\wwwroot\Mockups\ErrorLogs\" + sFileName;

            if (!File.Exists(sPath))
            {
                File.Create(sPath);
                TextWriter tw = new StreamWriter(sPath);
                tw.WriteLine(sb.ToString());
                tw.Close();
            }
            else if (File.Exists(sPath))
            {
                TextWriter tw = new StreamWriter(sPath, true);
                tw.WriteLine(sb.ToString());
                tw.Close();
            }

        }
        catch (Exception ex)
        {
            SaveError(ex);
        }

        return lRegisteredEvents;
    }


    // Retrieves Badges
    public List<iMISBadge> GetBadges(string sOrderNumber)
    {
        if (String.IsNullOrEmpty(sOrderNumber))
            throw new Exception("The 'OrderNumber' parameter is empty!");

        List<iMISBadge> lBadges = new List<iMISBadge>();

        try
        {
            SqlConnection sqlConnection = new SqlConnection(connString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("evo.sp_GetBadges", sqlConnection)
            {
                CommandType = CommandType.StoredProcedure
            };

            sqlCommand.Parameters.AddWithValue("@OrderNumber", Double.Parse(sOrderNumber));

            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
            DataSet objDS = new DataSet();
            da.Fill(objDS);

            if ((objDS == null) || (objDS.Tables == null) || (objDS.Tables.Count <= 0) || (objDS.Tables[0].Rows == null))
                return null;

            lBadges = objDS.Tables[0].AsEnumerable().Select(row => new iMISBadge
            {
                OrderNumber = Double.Parse(row["ORDER_NUMBER"].ToString()),
                BadgeNumber = Int32.Parse(row["BADGE_NUMBER"].ToString()),
                Delegate = row["DELEGATE"].ToString(),
                Prefix = row["PREFIX"].ToString(),
                Title = row["TITLE"].ToString(),
                FirstName = row["FIRST_NAME"].ToString(),
                LastName = row["LAST_NAME"].ToString(),
                InstituteName = row["COMPANY"].ToString(),
                StateProvince = row["STATE_PROVINCE"].ToString()
            }).ToList();

            foreach(var badges in lBadges)
            {
                if (badges.Delegate == string.Empty && badges.BadgeType.Equals("PRIMARY"))
                {
                    //badges.Delegate = "iron-man";
                    var orderNumber = Int32.Parse(badges.OrderNumber.ToString());

                    SqlCommand sqlCommand2 = new SqlCommand("SELECT top 1 BT_ID FROM ORDERS WHERE ORDER_NUMBER = '" + orderNumber.ToString() + "';", sqlConnection);

                    SqlDataReader reader = sqlCommand2.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            badges.Delegate = reader["BT_ID"].ToString();
                        }
                    }
                    sqlConnection.Close();    
                }
            }

            lBadges.ToList();

        }
        catch (Exception ex)
        {
            SaveError(ex);
        }

        return lBadges;
    }

    public string GetBTID(string orderNumber)
    {
        string btID = "";

        try
        {
            SqlConnection sqlConnection = new SqlConnection(connString);
            sqlConnection.Open();

            SqlCommand sqlCommand2 = new SqlCommand("SELECT top 1 BT_ID FROM ORDERS WHERE ORDER_NUMBER = '" + orderNumber.ToString() + "';", sqlConnection);

            SqlDataReader reader = sqlCommand2.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    btID = reader["BT_ID"].ToString();
                }
            }

        }
        catch(Exception ex)
        {
            SaveError(ex);
        }

        return btID;
    }

    public void StoreUser(iMISUser user)
    {
        try
        {
            HttpContext.Current.Session[SessionKeys.User] = user;
        }
        finally
        {

        }
    }

    //public String GetUserSession()
    //{
    //    try
    //    {
    //        return (String)HttpContext.Current.Session["loggedInUser"];
    //    }
    //    finally
    //    { }
    //}

    public iMISUser GetUser()
    {
        try
        {
            return (iMISUser)HttpContext.Current.Session[SessionKeys.User];
        }
        catch (Exception ex)
        {
            SaveError(ex);
            return null;
        }
    }

    public iMISContactDetails GetContactDetails(string sContactID)
    {
        if (String.IsNullOrEmpty(sContactID))
            throw new Exception("The 'sContactID' parameter is empty!");

        iMISContactDetails objICD = null;

        try
        {
            CContact objCC = new CContact(this.AdminUser, sContactID);

            if (objCC != null)
            {
                objICD = new iMISContactDetails();
                objICD.ContactID = objCC.ContactId;
                objICD.Email = objCC.EmailAddress;
                objICD.Fax = objCC.Fax;
                objICD.FirstName = objCC.FirstName;
                objICD.LastName = objCC.LastName;
                objICD.HomePhone = objCC.HomePhone;
                objICD.WorkPhone = objCC.WorkPhone;
                objICD.InstituteMemberType = objCC.InstituteCustomerTypeCode;
                objICD.InstituteID = objCC.InstituteContactId;
                objICD.InstituteName = objCC.InstituteName;
                objICD.MemberType = objCC.CustomerTypeCode;
                objICD.Prefix = objCC.Prefix;
                objICD.Suffix = objCC.Suffix;
                objICD.Title = objCC.Title;
                objICD.IsInstitute = objCC.IsInstitute;
                objICD.TollFreePhone = objCC.TollFreePhone;
                objICD.MemberState = objCC.Chapter;
                //objICD.SubscriptionInfo = GetSubscriptionDetails(objICD.ContactID);
                objICD.BillingCategory = objCC.BillingCategory;
            }

            if ((objCC.Addresses != null) && (objCC.Addresses.Count() > 0))
            {
                for (int i = 0; i < objCC.Addresses.Count(); i++)
                {
                    iMISAddress objIA = new iMISAddress();
                    objIA.StreetAddress1 = objCC.Addresses[i].Address1;
                    objIA.StreetAddress2 = objCC.Addresses[i].Address2;
                    objIA.StreetAddress3 = objCC.Addresses[i].Address3;
                    objIA.City = objCC.Addresses[i].City;
                    objIA.PostCode = objCC.Addresses[i].PostalCode;
                    objIA.State = objCC.Addresses[i].StateProvince;
                    objIA.Country = objCC.Addresses[i].Country;
                    objIA.IsPreferredMailing = objCC.Addresses[i].IsPreferredMail;

                    objICD.Addresses.Add(objIA);
                }
            }

            if ((objCC.ExtTables != null) && (objCC.ExtTables.Count() >= 1))
            {
                Int32 iIndex = objCC.ExtTables[0].Fields.Count() > Int32.Parse(ConfigurationManager.AppSettings["Count.CustomFields"].ToString()) ? 0 : 1;
                for (int i = 0; i < objCC.ExtTables[iIndex].Fields.Count(); i++)
                {
                    iMISCustomField objICF = new iMISCustomField();
                    objICF.UpdateiMISField(objCC.GetExtTableByName("Name_General").Fields[i].FieldName);

                    if (objCC.GetExtTableByName("Name_General").Fields[i].FieldName.Equals("Meal_Prefs", StringComparison.InvariantCultureIgnoreCase))
                    {
                        objICF.Value = objCC.GetExtTableByName("Name_General").Fields[i].FieldValue;
                        objICD.CustomFields.Add(objICF);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            SaveError(ex);
        }

        return objICD;
    }

    public CStaffUser AdminUser
    {
        get
        {
            //if (!this.IsiMISConnectionEnable)
            InitializeSystem();

            //this.IsiMISConnectionEnable = false;
            var test = CStaffUser.Login(ConfigurationManager.AppSettings["AdminImisLogin_Username"], ConfigurationManager.AppSettings["AdminImisLogin_Password"]);
            return CStaffUser.Login(ConfigurationManager.AppSettings["AdminImisLogin_Username"], ConfigurationManager.AppSettings["AdminImisLogin_Password"]);
        }
    }

    private void InitializeSystem()
    {
        iboAdmin.InitializeSystem(ConfigurationManager.ConnectionStrings["DataSource.iMIS.Connection"].ToString());
    }

    public CEDAKeys[] GetParameters(ParameterType[] lParameterTypes)
    {
        //object[] results = GetParams(new object[] {lParameterTypes});
        List<ParameterType> lstParam = lParameterTypes.ToList();
        object[] results = GetParams(lstParam).ToArray();

        return ((CEDAKeys[])(results));
    }

    public List<CEDAKeys> GetParams(List<ParameterType> lParameterTypes)
    {
        List<CEDAKeys> lParameters = new List<CEDAKeys>();

        try
        {
            foreach (ParameterType objPT in lParameterTypes)
                lParameters.AddRange(GetParamss(objPT));
        }
        finally
        {
            //this.IsiMISConnectionEnable = false;
        }

        return lParameters;
    }

    public List<CEDAKeys> GetParamss(ParameterType objPT)
    {
        List<CEDAKeys> lParameters = new List<CEDAKeys>();

        try
        {
            InitializeSystem();
            SqlConnection sqlConnection = new SqlConnection(connString);
            DataSet objDS = new DataSet();
            SqlDataAdapter da = null;

            switch (objPT)
            {
                case ParameterType.State:
                    String[] lStates = iboAdmin.ReferenceData.StatesProvinces;
                    if (lStates != null)
                    {
                        lParameters.AddRange(
                                lStates.AsEnumerable().Select(row => new CEDAKeys
                                {
                                    Code = row.ToString(),
                                    Description = row.ToString(),
                                    TypeID = (Int32)objPT
                                }
                                ).ToList()
                            );

                        // The description of the NAT option should be "National".
                        CEDAKeys objEP = lParameters.Where(row => row.Code == "NAT").FirstOrDefault();
                        if (objEP != null)
                            objEP.Description = "National";
                    }
                    break;

                case ParameterType.Institute:
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand("evo.sp_GetInstitutesWithMemberType", sqlConnection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@State", String.Empty);

                    da = new SqlDataAdapter(sqlCommand);
                    da.Fill(objDS);

                    if ((objDS != null) && (objDS.Tables != null) && (objDS.Tables.Count > 0) && (objDS.Tables[0].Rows != null) && (objDS.Tables[0].Rows.Count > 0))
                    {
                        lParameters.AddRange(
                                objDS.Tables[0].AsEnumerable().Select(row => new CEDAKeys
                                {
                                    Code = row["Code"].ToString(),
                                    Description = row["CompanyWithState"].ToString(),
                                    TypeID = (Int32)objPT
                                }
                                ).ToList()
                            );
                    }
                    sqlConnection.Close();

                    break;

                case ParameterType.Prefix:
                    String sTableName = String.Empty;
                    sTableName = objPT.ToString();

                    sqlConnection.Open();
                    SqlCommand sqlCommand2 = new SqlCommand("SELECT code, [description] FROM gen_tables WHERE table_name = '" + sTableName + "'", sqlConnection)
                    {
                        CommandType = CommandType.Text
                    };

                    da = new SqlDataAdapter(sqlCommand2);
                    da.Fill(objDS);


                    if ((objDS != null) && (objDS.Tables != null) && (objDS.Tables.Count > 0) && (objDS.Tables[0].Rows != null) && (objDS.Tables[0].Rows.Count > 0))
                    {
                        lParameters.AddRange(
                                objDS.Tables[0].AsEnumerable().Select(row => new CEDAKeys
                                    {
                                        Code = row["code"].ToString(),
                                        Description = objPT == ParameterType.Suffix ? row["code"].ToString() : row["description"].ToString(),
                                        TypeID = (Int32)objPT
                                    }
                                ).ToList()
                            );
                    }
                    break;
            }
        }
        catch (Exception ex)
        {
            SaveError(ex);
        }

        return lParameters;
        //return ((CEDAKeys[])(lParameters.ToArray()));
    }

    public iMISContact GetContact(string sEmail)
    {
        iMISContact objIC = null;
        string contactID = "";

        try
        {
            SqlConnection sqlConnection = new SqlConnection(connString);
            sqlConnection.Open();

            SqlCommand cmd = new SqlCommand("evo.sp_GetContact", sqlConnection)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Email", sEmail);

            SqlParameter outputParam = new SqlParameter();
            outputParam.ParameterName = "@ContactID";
            outputParam.SqlDbType = System.Data.SqlDbType.VarChar;
            outputParam.Size = 10;
            outputParam.Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add(outputParam);
            cmd.ExecuteNonQuery();

            contactID = outputParam.Value.ToString();

            if(contactID.Length > 0)
            {
                CContact objCC = new CContact(this.AdminUser, contactID);
                if(objCC != null)
                {
                    objIC = new iMISContact();
                    objIC.ContactID = objCC.ContactId;
                    objIC.FirstName = objCC.FirstName;
                    objIC.LastName = objCC.LastName;
                    objIC.MemberType = objCC.CustomerTypeCode;
                }
            }
        }
        catch(Exception ex)
        {
            SaveError(ex);
        }

        return objIC;
    }

    public iMISContact SaveContact(iMISContactDetails objICD)
    {
        InitializeSystem();
        iMISContact objIC = null;

        try
        {
            CContact objCC = objICD.ContactID == String.Empty ? new CContact(this.AdminUser) : new CContact(this.AdminUser, objICD.ContactID);
            if (objCC != null)
            {
                objCC.EmailAddress = objICD.Email;
                objCC.Fax = objICD.Fax;
                objCC.FirstName = objICD.FirstName;
                objCC.LastName = objICD.LastName;
                objCC.HomePhone = objICD.HomePhone;
                objCC.WorkPhone = objICD.WorkPhone;

                // CWC-52
                if (objICD.InstituteName.Length > 80)
                    objICD.InstituteName = objICD.InstituteName.Substring(0, 79);

                objCC.InstituteName = objICD.InstituteName;

                if (!objICD.IsMemberTypeReadOnly)
                {
                    objCC.CustomerTypeCode = objICD.MemberType == String.Empty ? "NME" : objICD.MemberType;
                }

                objCC.Prefix = objICD.Prefix;
                objCC.Suffix = objICD.Suffix;
                objCC.Title = objICD.Title;
                objCC.IsInstitute = objICD.IsInstitute;

                // CO_ID fix, carries over the company id of the person who is logged in and trying to register a non-member.
                // If CustomerTypeCode = NME, set the InstituteContactId = ""

                objCC.InstituteContactId = objCC.CustomerTypeCode == "NME" ? "" : objICD.InstituteID;

                if (!String.IsNullOrEmpty(objICD.InstituteID) && objCC.InstituteContactId != "") // does this contact belong to a member company?
                {
                    iMISInstitute objII = GetInstituteDetails(objICD.InstituteID);
                    if (objII != null)
                    {
                        objCC.BillingCategory = objII.CategoryCode;
                        objCC.InstituteName = objII.Company; // company field should show company name only, not company name and state.
                    }
                }
                else if (objCC.CustomerTypeCode == "NME" && String.IsNullOrEmpty(objCC.BillingCategory))
                {
                    objCC.BillingCategory = "NA";
                }

                if (objCC.Addresses != null)
                {
                    for (int i = 0; i < objICD.Addresses.Count; i++)
                    {
                        objICD.Addresses[i].StreetAddress1 = (objICD.Addresses[i].StreetAddress1 == null) ? "" : objICD.Addresses[i].StreetAddress1;
                        objICD.Addresses[i].StreetAddress2 = (objICD.Addresses[i].StreetAddress2 == null) ? "" : objICD.Addresses[i].StreetAddress2;
                        objICD.Addresses[i].StreetAddress3 = (objICD.Addresses[i].StreetAddress3 == null) ? "" : objICD.Addresses[i].StreetAddress3;
                        objICD.Addresses[i].City = (objICD.Addresses[i].City == null) ? "" : objICD.Addresses[i].City;
                        objICD.Addresses[i].PostCode = (objICD.Addresses[i].PostCode == null) ? "" : objICD.Addresses[i].PostCode;
                        objICD.Addresses[i].State = (objICD.Addresses[i].State == null) ? "" : objICD.Addresses[i].State;
                        objICD.Addresses[i].Country = (objICD.Addresses[i].Country == null) ? "" : objICD.Addresses[i].Country;
                        objICD.Addresses[i].IsPreferredMailing = (objICD.Addresses[i].IsPreferredMailing == null) ? false : objICD.Addresses[i].IsPreferredMailing;
                        objICD.WorkPhone = (objICD.WorkPhone == null) ? "" : objICD.WorkPhone;
                        objICD.Fax = (objICD.Fax == null) ? "" : objICD.Fax;

                        objCC.Addresses[i].Address1 = objICD.Addresses[i].StreetAddress1;
                        objCC.Addresses[i].Address2 = objICD.Addresses[i].StreetAddress2;
                        objCC.Addresses[i].Address3 = objICD.Addresses[i].StreetAddress3;
                        objCC.Addresses[i].City = objICD.Addresses[i].City;
                        objCC.Addresses[i].PostalCode = objICD.Addresses[i].PostCode;
                        objCC.Addresses[i].StateProvince = objICD.Addresses[i].State;
                        objCC.Addresses[i].Country = objICD.Addresses[i].Country;
                        objCC.Addresses[i].IsPreferredMail = objICD.Addresses[i].IsPreferredMailing;
                        objCC.Addresses[i].Phone = objICD.WorkPhone; // this will save the work phone above!
                        objCC.Addresses[i].Fax = objICD.Fax; // this will save the fax above!

                        if (i == 0)
                            objCC.Chapter = objICD.Addresses[i].State;
                    }
                }
                // CWC-37
                objCC.TollFreePhone = objICD.TollFreePhone;

                if (objCC.Validate())
                {
                    //check if there is already an iMIS internal account with this email address
                    iMISContact imisContact = null;

                    if (!string.IsNullOrWhiteSpace(objCC.EmailAddress))
                    {
                        imisContact = GetContact(objCC.EmailAddress);
                    }

                    string contactId = null;
                    if (imisContact == null)
                    {
                        // CWS-68
                        objCC.SourceCode = "WEB-CT";
                        objCC.Save();
                        contactId = objCC.ContactId;
                    }
                    else
                    {
                        objCC.Save(); // updates user record
                        contactId = imisContact.ContactID;
                    }
                    InitializeSystem();

                    objCC = new CContact(this.AdminUser, contactId);

                    if (objCC != null)
                    {
                        // The custom fields of the contact.
                        if(objICD.CustomFields != null)
                        {
                            objCC.GetExtTableByName("Name_General").GetField("Meal_Prefs").FieldValue = (objICD.CustomFields[0].Value.ToString().Trim().Length > 0) ? objICD.CustomFields[0].Value : "";
                        }

                        if (objCC.Validate())
                            objCC.Save();
                        else
                            throw new Exception(objCC.Errors.PrimaryErrorMessage);

                        // Set the return value.
                        if (imisContact == null)
                        {
                            objIC = new iMISContact();
                            objIC.ContactID = contactId;
                            objIC.FirstName = objCC.FirstName;
                            objIC.LastName = objCC.LastName;
                            objIC.MemberType = objCC.CustomerTypeCode;
                        }
                        else
                        {
                            objIC = imisContact;
                        }
                    }
                }
                else
                  {
                    throw new Exception(objCC.Errors.PrimaryErrorMessage);
                }
            }
        }
        catch(Exception ex)
        {
            SaveError(ex);
        }

        return objIC;
    }

    
    public bool SaveBadge(iMISBadge objIB)
    {
        bool result = false;

        try
        {
            if (objIB.BadgeNumber < 1)
                objIB.BadgeNumber = Int32.Parse(GetBadgeNumber(objIB.OrderNumber));


            SqlConnection conn = new SqlConnection(connString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("evo.sp_SaveBadge", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
                cmd.Parameters.AddWithValue("@OrderNumber", objIB.OrderNumber);
                cmd.Parameters.AddWithValue("@BadgeNumber", objIB.BadgeNumber);
                cmd.Parameters.AddWithValue("@BadgeType", objIB.BadgeType);
                cmd.Parameters.AddWithValue("@Delegate", objIB.Delegate);
                cmd.Parameters.AddWithValue("@Prefix", objIB.Prefix);
                cmd.Parameters.AddWithValue("@Title", objIB.Title);
                cmd.Parameters.AddWithValue("@FirstName", objIB.FirstName);
                cmd.Parameters.AddWithValue("@LastName", objIB.LastName);
                cmd.Parameters.AddWithValue("@FullName", objIB.FullName);
                cmd.Parameters.AddWithValue("@InstituteName", objIB.InstituteName);
                cmd.Parameters.AddWithValue("@StateProvince", objIB.StateProvince);

                int res = cmd.ExecuteNonQuery();

                //if (res.Equals(1))
                    result = true;
        }
        catch(Exception ex)
        {
            SaveError(ex);
        }

        return result;
    }

    public void SaveActivity(iMISActivity objIA, string BillToContactID)
    {
        InitializeSystem();

        if (String.IsNullOrEmpty(objIA.ContactID))
            throw new Exception("The 'ContactID' parameter is empty!");

        if (String.IsNullOrEmpty(objIA.EventCode))
            throw new Exception("The 'EventCode' parameter is empty!");

        if (objIA.TransactionDate == DateTime.MinValue)
            throw new Exception("The 'TransactionDate' parameter is empty!");


        try
        {
            CContact objCC = new CContact(this.AdminUser, objIA.ContactID);
            if(objCC != null)
            {
                //CActivity objCA = objCC.NewActivity(objIA.ActivityCode);
                CActivity objCA = objCC.NewActivity("CORPGUEST");
                objCA.ProductCode = objIA.EventCode;
                objCA.ContactId = objIA.ContactID;
                objCA.TransactionDate = objIA.TransactionDate;
                objCA.ThruDate = objIA.EventDate;
                objCA.Description = objIA.Description;
                objCA.Units = objIA.Quantity;
                objCA.Amount = objIA.Amount;

                if (!string.IsNullOrWhiteSpace(BillToContactID))
                    objCA.OtherCode = BillToContactID;
                // objCA.OtherCode = objCC.ContactId; --guest id

                if (objCA.Validate())
                    objCA.Save();
                else
                    throw new Exception(objCA.Errors.PrimaryErrorMessage);
            }
        }
        catch(Exception ex)
        {
            SaveError(ex);
        }
    }

    private String GetBadgeNumber(Double dOrderNumber)
    {
        String sBadgeNumber = String.Empty;

        try
        {
            SqlConnection conn = new SqlConnection(connString);

            SqlCommand cmd = new SqlCommand("evo.sp_GetBadgeNumber", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@OrderNumber", dOrderNumber);

            SqlParameter outPutParam = new SqlParameter();
            outPutParam.ParameterName = "@BadgeNumber";
            outPutParam.SqlDbType = System.Data.SqlDbType.VarChar;
            outPutParam.Size = 2;
            outPutParam.Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add(outPutParam);

            
            conn.Open();
            cmd.ExecuteNonQuery();
            sBadgeNumber = outPutParam.Value.ToString();

        }
        catch(Exception ex)
        {
            SaveError(ex);
        }

        return sBadgeNumber;
    }


    private iMISInstitute GetInstituteDetails(string sInstituteID)
    {
        if (String.IsNullOrEmpty(sInstituteID))
        {
            throw new Exception("The 'sInstituteID' parameter is empty!");
        }

        iMISInstitute objII = null;
        
        try
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("evo.sp_GetInstitutesWithMemberType", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@State", String.Empty);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet objDS = new DataSet();
            da.Fill(objDS);

            if ((objDS == null) || (objDS.Tables == null) || (objDS.Tables.Count <= 0) || (objDS.Tables[0].Rows == null))
                return null;

            DataRow objDR = objDS.Tables[0].AsEnumerable().Where(row => row["Code"].ToString() == sInstituteID).FirstOrDefault();
            if(objDR != null)
            {
                objII = new iMISInstitute();
                objII.Code = objDR["Code"].ToString();
                objII.InstituteName = objDR["InstituteName"].ToString();
                objII.Company = objDR["Company"].ToString();
                objII.CategoryCode = objDR["CategoryCode"].ToString();
                objII.MemberState = objDR["MemberState"].ToString();

                if (objDS.Tables[0].Columns.Contains("CategoryDescription"))
                    objII.CategoryDescription = objDR["CategoryDescription"].ToString();
            }
        }
        catch(Exception ex)
        {
            SaveError(ex);
        }

        return objII;
    }

    public void UpdateContactPhoneSpecialReq(string contactId, string phone, string specReq)
    {
        if(!String.IsNullOrWhiteSpace(phone) || !String.IsNullOrWhiteSpace(specReq))
        {
            iMISContactDetails objICD = GetContactDetails(contactId);

            if(objICD != null)
            {
                bool updateDetails = false;

                // Related to CWS-15
                if(phone.Length >= 10 && !String.IsNullOrWhiteSpace(phone))
                {
                    objICD.WorkPhone = phone.Trim();
                    updateDetails = true;
                }

                if(!String.IsNullOrWhiteSpace(specReq))
                {
                    List<iMISCustomField> custFields = objICD.CustomFields.ToList();
                    //iMISCustomField mealPref = custFields.Where(cf => cf.iMISField == iMISField.EventPreferences).FirstOrDefault();
                    iMISCustomField mealPref = new iMISCustomField();
                    mealPref.iMISField = iMISField.EventPreferences;

                    if(custFields.Count == 1)
                    {
                        mealPref.Value = custFields[0].Value.ToString();
                        custFields.Clear();
                    }

                    if (mealPref.Value.ToString().Trim() == null || mealPref.Value.ToString().Trim() == "")
                    {
                        //mealPref = new iMISCustomField();
                        mealPref.iMISField = iMISField.EventPreferences;
                        mealPref.Value = specReq.Trim();

                        custFields.Add(mealPref);
                        objICD.CustomFields = custFields;
                        updateDetails = true;
                    }
                    else if(String.IsNullOrWhiteSpace(mealPref.Value.ToString()) || specReq.Length.ToString().Trim().Length >= 0)
                    {
                        if (custFields.Count == 1)
                            custFields.Clear();

                        mealPref.Value = specReq.Trim();
                        mealPref.iMISField = iMISField.EventPreferences;
                        custFields.Add(mealPref);
                        objICD.CustomFields = custFields;
                        updateDetails = true;
                    }
                }

                if(updateDetails)
                {
                    SaveContact(objICD);
                }
            }
        }
    }

    public bool DeleteBadge(p_DeleteBadge objParams, string eventCode, string delegateNumber)
    {
        bool result = false;
        try
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("evo.sp_DeleteBadge", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@OrderNumber", objParams.OrderNumber);
            cmd.Parameters.AddWithValue("@BadgeNumber", objParams.BadgeNumber);

            cmd.ExecuteNonQuery();

            // CWC-62 Delete CORPGUEST activity
            if(delegateNumber.Length > 1)
            {
                SqlCommand cmd2 = new SqlCommand("dbo.sp_DeleteActivity", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd2.Parameters.AddWithValue("@ID", delegateNumber);
                cmd2.Parameters.AddWithValue("@ProductCode", eventCode);

                cmd2.ExecuteNonQuery();
            }
            
            // if (value.Equals(-1))
            result = true;
        }
        catch(Exception ex)
        {
            SaveError(ex);
        }

        return result;
    }

    public iMISUser AuthenticateUser(string username, string password)
    {
        iMISUser objIU = null;

        try
        {
            CContactUser objCCU = CContactUser.LoginByWebLogin(username, password);

            if(objCCU != null)
            {
                InitializeSystem();

                objIU = new iMISUser();
                objIU.ContactID = objCCU.ContactId;
                objIU.ExpiresOn = objCCU.ExpiresOn;
                objIU.UserName = objCCU.WebLoginId;

                iMISContact objIC = GetContact(objCCU.WebLoginId);
                if(objIC != null)
                {
                    objIU.FirstName = objIC.FirstName;
                    objIU.LastName = objIC.LastName;
                    objIU.MemberType = objIC.MemberType;
                    objIU.IsStaff = (objIU.MemberType == "STAFF");
                }
            }
        }
        catch(Exception ex)
        {
            SaveError(ex);
        }

        return objIU;
    }

    public List<String> GetTitles()
    {
        List<String> lstTitles = new List<String>();

        try
        {
            SqlConnection sqlConnection = new SqlConnection(connString);
            sqlConnection.Open();
            
            SqlCommand sqlCommand = new SqlCommand("SELECT DESCRIPTION FROM Gen_Tables WHERE TABLE_NAME = 'PREFIX' ", sqlConnection)
            {
                CommandType = CommandType.Text
            };

            SqlDataReader reader = sqlCommand.ExecuteReader();
            if(reader.HasRows)
            {
                while(reader.Read())
                {
                    lstTitles.Add(reader["DESCRIPTION"].ToString());
                }
            }
            sqlConnection.Close();          
        }
        catch(Exception ex)
        {
            SaveError(ex);
        }

        return lstTitles;
    }

    public void SendErrorEmail(string errorMessage)
    {
        try
        {
            using(SmtpClient client = new SmtpClient())
            {
                Int32 iPort = 0;
                Int32.TryParse(ConfigurationManager.AppSettings["Email.Port"].ToString(), out iPort);
                if (iPort > 0)
                    client.Port = iPort;

                client.Host = ConfigurationManager.AppSettings["Email.Host"].ToString();
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = Boolean.Parse(ConfigurationManager.AppSettings["Email.EnableSSL"].ToString());
                //client.Port = Int32.Parse(ConfigurationManager.AppSettings["Email.Port"].ToString());

                String username = ConfigurationManager.AppSettings["Email.Username"].ToString();
                String password = ConfigurationManager.AppSettings["Email.Password"].ToString();

                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(username, password);
                }
                else
                    client.UseDefaultCredentials = true;


                using(MailMessage mm = new MailMessage())
                {
                    string[] emailToList = ConfigurationManager.AppSettings["Email.ToList"].ToString().Split(';');
                    foreach(var email in emailToList)
                    {
                        mm.To.Add(email);
                    }

                    mm.From = new MailAddress(ConfigurationManager.AppSettings["Email.From"].ToString());
                    mm.Subject = ConfigurationManager.AppSettings["Email.ContentNotification.Subject"].ToString();
                    mm.IsBodyHtml = true;
                    mm.BodyEncoding = Encoding.GetEncoding(1254);
                    mm.Body = errorMessage;

                    client.Send(mm);
                }
            }
        }
        catch(Exception ex)
        {

        }
    }
}

public enum ParameterType
{
    /// <remarks/>
    Skip,

    /// <remarks/>
    Template,

    /// <remarks/>
    FileType,

    /// <remarks/>
    FilterOperator,

    /// <remarks/>
    UserRole,

    /// <remarks/>
    AuthorizationElement,

    /// <remarks/>
    Permission,

    /// <remarks/>
    State,

    /// <remarks/>
    Suffix,

    /// <remarks/>
    Country,

    /// <remarks/>
    Occupation,

    /// <remarks/>
    Prefix,

    /// <remarks/>
    EventTopic,

    /// <remarks/>
    CashAccount,

    /// <remarks/>
    Interest,

    /// <remarks/>
    NewsPreference,

    /// <remarks/>
    EmailPreference,

    /// <remarks/>
    NotificationType,

    /// <remarks/>
    Category,

    /// <remarks/>
    Institute,

    /// <remarks/>
    EventType,
}

public enum iMISField
{

    /// <remarks/>
    Skip,

    /// <remarks/>
    MobileNumber,

    /// <remarks/>
    Occupation,

    /// <remarks/>
    SendEmailOnlyAssistant,

    /// <remarks/>
    SendEmailBoth,

    /// <remarks/>
    AssistantName,

    /// <remarks/>
    AssistantPhoneNumber,

    /// <remarks/>
    AssistantEmail,

    /// <remarks/>
    Interests,

    /// <remarks/>
    ReceiveEmail,

    /// <remarks/>
    EmailPreferences,

    /// <remarks/>
    EventPreferences,

    /// <remarks/>
    NewsPreferences,
}