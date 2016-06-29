using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iMISWebReference;
using System.Configuration;
using System.Text.RegularExpressions;
using ClientService;
using System.IO;
using System.Drawing;
using DevExpress.XtraPrinting;

public partial class AllCorporateTables : System.Web.UI.Page
{
    MembershipWebService membershipService = new MembershipWebService();
    Helper helper = new Helper();
    protected void Page_Load(object sender, EventArgs e)
    {
        _ScriptManager.RegisterAsyncPostBackControl(txt_events);

        this.EventInformation.Style.Add("display", "none");
        if (!this.IsPostBack)
        {
            var objIU = helper.GetUser();
            if (objIU != null)
            {
                AttachAttributes();
                PopulateEventCodes();
                PopulateDropDownLists();
                BTN_DisplayTableRegs_Click(sender, e);
            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        this.pPhone.Visible = this.TB_Phone.Enabled;
        this.pEventPreferences.Visible = this.TB_EventPreferences.Enabled;
    }

    private void PopulateEventCodes()
    {
        try
        {
            this.txt_events.Items.Insert(0, new ListItem(String.Empty, string.Empty));
            this.txt_events.DataSource = helper.GetEventCodes();
            this.txt_events.DataTextField = "EventCode";
            this.txt_events.DataValueField = "EventCode";
            this.txt_events.DataBind();
            this.txt_events.SelectedIndex = 0;
        }
        finally { }
    }

    private void PopulateRegisteredEvents(string contactId)
    {
        try
        {
            var source = helper.GetRegisteredEvents(contactId);

            if(source.Count > 0)
            {
                this.RPT_RegisteredEvents.Visible = true;
                this.EventInformation.Visible = true;
                this.lbl_NoTableRegos.Style.Add("display", "none");
                this.RPT_RegisteredEvents.DataSource = source;
                // this.RPT_RegisteredEvents.DataSource = helper.GetRegisteredEvents();
                this.RPT_RegisteredEvents.DataBind();

                // Populate event information, outside repeater
                this.lbl_EventTitle.Text = (from element in source select element.Title).FirstOrDefault();
                this.lbl_EventDate.Text = " <i class='fa fa-calendar'></i> " + (from element in source select element.StartDate).FirstOrDefault().ToShortDateString();
                this.lbl_EventTime.Text = "| <i class='fa fa-clock-o'></i> " + (from element in source select element.StartTime).FirstOrDefault() + " - " + (from element in source select element.EndTime).FirstOrDefault();
                this.lbl_EventCoordinator.Text = "| <i class='fa fa-user-secret'></i> " + (from element in source select element.CoordinatorEmail).FirstOrDefault();
            }
            else
            {
                this.lbl_NoTableRegos.Style.Add("display", "block");
                this.RPT_RegisteredEvents.Visible = false;
                this.EventInformation.Visible = false;
            }
            
        }
        catch (Exception ex)
        {
            // write to a log file
            helper.SaveError(ex);
        }
    }

    //protected void BTN_ExportToExcel_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //PdfExportOptions options = new PdfExportOptions();
    //        //options.Compressed = false;
    //        //ASPxGridViewExporter1.WritePdfToResponse(options);

    //        Response.Clear();
    //        Response.Buffer = true;
    //        Response.AddHeader("content-disposition", "attachment;filename=TableAllocation.xls");
    //        Response.Charset = "";
    //        Response.ContentType = "application/vnd.ms-excel";
    //        StringWriter sw = new StringWriter();
    //        HtmlTextWriter hw = new HtmlTextWriter(sw);

    //        GridView1.AllowPaging = false;
    //        GridView1.DataBind();

    //        //Change the Header Row back to white color
    //        GridView1.HeaderRow.Style.Add("background-color", "#FFFFFF");

    //        //Apply style to Individual Cells
    //        GridView1.HeaderRow.Cells[0].Style.Add("background-color", "#366092");
    //        GridView1.HeaderRow.Cells[1].Style.Add("background-color", "#366092");
    //        GridView1.HeaderRow.Cells[2].Style.Add("background-color", "#366092");
    //        GridView1.HeaderRow.Cells[3].Style.Add("background-color", "#366092");
    //        GridView1.HeaderRow.Cells[4].Style.Add("background-color", "#366092");
    //        GridView1.HeaderRow.Cells[5].Style.Add("background-color", "#366092");
    //        GridView1.HeaderRow.Cells[6].Style.Add("background-color", "#366092");
    //        GridView1.HeaderRow.Cells[7].Style.Add("background-color", "#366092");
    //        GridView1.HeaderRow.Cells[8].Style.Add("background-color", "#366092");

    //        GridView1.Columns.RemoveAt(0);
    //        GridView1.HeaderRow.Cells[0].Visible = false;

    //        for (int i = 0; i < GridView1.Rows.Count; i++)
    //        {
    //            GridViewRow row = GridView1.Rows[i];

    //            //Change Color back to white
    //            row.BackColor = System.Drawing.Color.White;
    //            row.Cells[0].Visible = false;

    //            //Apply text style to each Row
    //            row.Attributes.Add("class", "textmode");

    //            //Apply style to Individual Cells of Alternating Row
    //            if (i % 2 != 0)
    //            {
    //                //row.Cells[0].Style.Add("background-color", "#C2D69B");
    //                row.Cells[1].Style.Add("background-color", "#C2D69B");
    //                row.Cells[2].Style.Add("background-color", "#C2D69B");
    //                row.Cells[3].Style.Add("background-color", "#C2D69B");
    //                row.Cells[4].Style.Add("background-color", "#C2D69B");
    //                row.Cells[5].Style.Add("background-color", "#C2D69B");
    //                row.Cells[6].Style.Add("background-color", "#C2D69B");
    //                row.Cells[7].Style.Add("background-color", "#C2D69B");
    //                row.Cells[8].Style.Add("background-color", "#C2D69B");

    //            }
    //        }

    //        foreach (TableCell c in GridView1.HeaderRow.Cells)
    //        {
    //            if (c.HasControls())
    //            {
    //                c.Text = (c.Controls[0] as LinkButton).Text;
    //                c.Controls.Clear();
    //            }
    //        }

    //        GridView1.RenderControl(hw);

    //        //style to format numbers to string
    //        string style = @"<style> .textmode { mso-number-format:\@; } </style>";
    //        Response.Write(style);
    //        Response.Output.Write(sw.ToString());
    //        Response.Flush();
    //        Response.End();
    //    }
    //    catch (Exception ex)
    //    {
    //        helper.SaveError(ex);
    //    }
    //}

    // Response.End() exception
    public override void  VerifyRenderingInServerForm(Control control)
    {
        return;
    }

    //protected void btnPdfExport_Click(object sender, EventArgs e)
    //{
    //    string[] tempEventCode = txt_events.Text.Split(' ');
    //    string eventCode = tempEventCode[0];

    //    ASPxGridViewExporter1.Landscape = true;
    //    XlsxExportOptions options = new XlsxExportOptions();
    //    options.FitToPrintedPageWidth = true;

    //    if (!String.IsNullOrEmpty(eventCode))
    //    {
    //        ASPxGridViewExporter1.WriteXlsxToResponse("TableAllocation_" + eventCode, options);
    //    }
    //    else
    //        ASPxGridViewExporter1.WriteXlsxToResponse("TableAllocation", options);
    //}
    
    protected void BTN_DisplayTableRegs_Click(object sender, EventArgs e)
    {
        //ScriptManager.RegisterStartupScript(this, typeof(String), "CheckSelectedValue", "CheckSelectedValue();", true);
        string[] tempEventCode = txt_events.Text.Split(' ');

        string eventCode = tempEventCode[0];

        try
        {
            if (!String.IsNullOrEmpty(eventCode))
            {
                // Populate registered events   
                // Need not pass the contact id
                PopulateRegisteredEvents(eventCode);
                this.BTN_DisplayTableRegs.Style.Add("display", "none");
                this.EventInformation.Style.Add("display", "block");
                //ScriptManager.RegisterStartupScript(this, typeof(String), "FilterFunction", "filterMethod();", true);

                this.SqlDataSource1.SelectParameters.Remove(SqlDataSource1.SelectParameters["EventCode"]);
                this.SqlDataSource1.SelectParameters.Add("EventCode", eventCode);
                //this.GridView1.DataSource = SqlDataSource1;
                //this.GridView1.DataBind();
                this.HL_EventRegistrations.NavigateUrl = ConfigurationManager.AppSettings["RPT.EventRegistrations.Live"].ToString() + eventCode;
                this.HL_NameBadges.NavigateUrl = ConfigurationManager.AppSettings["RPT.NameBadges.Live"].ToString() + eventCode + "&PrintTableNumbers=" + true;

                if (this.HF_TableUpdateRequired.Value.Equals("1"))
                {
                    //update table number - zzzzzz grrrrrrr

                    string orderNumber = this.HF_OrderNumber.Value;
                    string tableNumber = this.HF_TableNumber.Value;

                    helper.UpdateTableNumber(orderNumber, tableNumber);

                    this.SqlDataSource1.SelectParameters.Remove(SqlDataSource1.SelectParameters["EventCode"]);
                    this.SqlDataSource1.SelectParameters.Add("EventCode", eventCode);
                    //this.GridView1.DataSource = SqlDataSource1;
                    //this.GridView1.DataBind();
                    PopulateRegisteredEvents(eventCode);
                    this.HF_TableUpdateRequired.Value = "0";
                }
            }
        }
        catch (Exception ex)
        {
            helper.SaveError(ex);
        }
    }

    private void PopulateDropDownLists()
    {
        try
        {
            List<ParameterType> lParameterTypes = new List<ParameterType>();
            lParameterTypes.Add(ParameterType.State);
            lParameterTypes.Add(ParameterType.Prefix);
            lParameterTypes.Add(ParameterType.Institute);

            IList<CEDAKeys> lParameters = helper.GetParameters(lParameterTypes.ToArray<ParameterType>());

            if ((lParameters != null) && (lParameters.Count > 0))
            {
                LoadDropDownList(lParameters, this.DDL_States, (Int32)ParameterType.State);
                LoadDropDownList(lParameters, this.DDL_Prefix, (Int32)ParameterType.Prefix);
                LoadDropDownList(lParameters, this.DDL_Institutes, (Int32)ParameterType.Institute);
            }
        }
        catch (Exception ex)
        {
            helper.SaveError(ex);
        }
    }

    private void LoadDropDownList(IList<CEDAKeys> lParameters, DropDownList objDDL, Int32 iTypeID)
    {
        try
        {
            if (lParameters.Where(p => p.TypeID == iTypeID).Count() > 0)
            {
                objDDL.Items.Add(new ListItem("--- Select ---", String.Empty));
                objDDL.Items.AddRange
                    (
                        lParameters.AsEnumerable().Where(row => row.TypeID == iTypeID).Select(
                            row => new ListItem { Text = row.Description, Value = row.Code }
                        ).ToArray<ListItem>()
                    );
            }
        }
        finally
        {

        }
    }

    public void ShowMessage(String text, System.Web.UI.Page page)
    {
        try
        {
            ScriptManager.RegisterStartupScript(page, typeof(String), "MessageBoxJS", "ShowMessage('" + text + "');", true);
        }
        finally { }
    }

    protected void BTN_SaveContact_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.RB_Member.Checked && this.DDL_Institutes.SelectedValue == String.Empty)
            {
                ShowMessage("Please select your company from the list.", this);
                SetFocus();
                return;
            }
            else if (this.RB_NonMember.Checked && this.TB_InstituteName.Text == String.Empty)
            {
                ShowMessage("Please enter your company name.", this);
                SetFocus();
                return;
            }

            // Check if the attendee is already in the badge list or not.
            iMISContact objIC = null;
            iMISBadge objBadge = null;

            int badgeNumber = 0;            
            Int32.TryParse(this.HF_BadgeNumber.Value, out badgeNumber);
            IList<iMISBadge> lBadges = helper.GetBadges(this.HF_OrderNumber.Value);

            if(badgeNumber == 0) // is it a new attendee?
            {
                if((lBadges != null) && (lBadges.Count > 0))
                {
                    if (this.TBEmailAddress.Text != String.Empty)
                        objIC = helper.GetContact(this.TBEmailAddress.Text);

                    if (objIC != null)
                    {
                        iMISBadge objIB = lBadges.Where(badge => badge.Delegate == objIC.ContactID).FirstOrDefault();
                        if (objIB != null)
                        {
                            ShowMessage("Attendee is already in the list.", this);
                            SetFocus();
                            return;
                        }
                    }
                    else
                        this.HF_DelegateNumber.Value = "";
                }
                objIC = SaveiMISContact(); // always update contact info
            }
            else
            {
                if((lBadges != null) && (lBadges.Count > 0))
                {
                    objBadge = lBadges.Where(badge => badge.BadgeNumber == badgeNumber).FirstOrDefault();
                }
            }

            // Now save the attendee
            Boolean bDisplayError = true;

            if(objBadge != null)
            {
                objBadge.Prefix = this.DDL_Prefix.SelectedValue;
                objBadge.FirstName = this.TB_FirstName.Text;
                objBadge.LastName = this.TB_LastName.Text;
                objBadge.Title = this.TB_Position.Text;
                objBadge.StateProvince = this.DDL_States.SelectedValue;

                if (RB_Member.Checked)
                {
                    objBadge.InstituteName = this.DDL_Institutes.SelectedItem.Text;
                }
                else
                {
                    objBadge.InstituteName = this.TB_InstituteName.Text;
                }

                if (objBadge.Delegate != string.Empty)
                    this.HF_DelegateNumber.Value = objBadge.Delegate;

                bDisplayError = !(helper.SaveBadge(objBadge));
                var temp = SaveiMISContact();
            }
            else if(objIC != null)
            {
                // contact id - registered under the person - not logged in user as the website
                var orderNumber = this.HF_OrderNumber.Value.ToString();
                var contactID = helper.GetBTID(orderNumber);

                if(SaveBadge(objIC.ContactID))
                {
                    bDisplayError = !SaveActivity(objIC.ContactID, contactID);
                }
            }
           

            if (bDisplayError)
            {
                ShowMessage("An error has occurred, please contact IT", this);
            }
            else
            {
                PopulateBadges();
                ClearAttendeeForm();
            }

            ScriptManager.RegisterStartupScript(this, typeof(String), "ChangeTabJS", "ChangeTab(1);", true);

        }
        catch(Exception ex)
        {
            helper.SaveError(ex);
        }
    }

    private Boolean SaveActivity(string sContactID, string BillToContactID)
    {
        Boolean bSaved = false;
        
        try
        {
            iMISActivity objIA = new iMISActivity();
            objIA.ContactID = sContactID;
            objIA.EventCode = this.HF_EventCode.Value;
            objIA.TransactionDate = DateTime.Now;
            objIA.Description = this.HF_EventTitle.Value;
            objIA.EventDate = DateTime.Parse(this.HF_StartDate.Value);

            helper.SaveActivity(objIA, BillToContactID);
            bSaved = true;
        }
        catch(Exception ex)
        {
            helper.SaveError(ex);
        }

        return bSaved;
    }

    private bool SaveBadge(String sContactID)
    {
        bool bSaved = false;

        try
        {
            // The bill to contact person -the signed-in user, doesn't have a delegate value in iMIS.
            iMISUser objIU = helper.GetUser();
            if(objIU != null)
            {
                if (sContactID == objIU.ContactID)
                    sContactID = String.Empty;
            }

            iMISBadge objIB = new iMISBadge();

            objIB.OrderNumber = Double.Parse(this.HF_OrderNumber.Value);

            if (this.HF_BadgeNumber.Value != String.Empty)
                objIB.BadgeNumber = Int32.Parse(this.HF_BadgeNumber.Value);

            objIB.Delegate = sContactID;
            objIB.Prefix = this.DDL_Prefix.SelectedValue;
            objIB.FirstName = this.TB_FirstName.Text;
            objIB.LastName = this.TB_LastName.Text;
            objIB.InstituteName = this.RB_Member.Checked ? this.DDL_Institutes.SelectedItem.Text : this.TB_InstituteName.Text;
            objIB.Title = this.TB_Position.Text;
            objIB.StateProvince = this.DDL_States.SelectedValue;

            // Remove the state value from institute name.
            //foreach (ListItem objLI in this.DDL_States.Items)
            //{
            //    if (objLI.Value != String.Empty)
            //        objIB.InstituteName = objIB.InstituteName.Replace(objLI.Value, String.Empty);
            //}

            //objIB.InstituteName = objIB.InstituteName.Trim().TrimEnd(new char[] { ' ', '-' });


            // CWC-61
            objIB.InstituteName = objIB.InstituteName.Trim();

            var states = ConfigurationManager.AppSettings["States"].ToString();
            foreach(var state in states.Split(','))
            {
                if (objIB.InstituteName.EndsWith(" - " + state))
                {
                    int replaceIndex = objIB.InstituteName.LastIndexOf(" - " + state);
                    objIB.InstituteName = objIB.InstituteName.Substring(0, replaceIndex);
                }
            }




            bSaved = helper.SaveBadge(objIB);
        }
        catch(Exception ex)
        {
            helper.SaveError(ex);
        }

        return bSaved;
    }

    public iMISContact SaveiMISContact()
    {
        iMISContact objIC = null;

        try
        {
            if (this.TBEmailAddress.Text != String.Empty)
                objIC = helper.GetContact(this.TBEmailAddress.Text);
            else if (this.HF_DelegateNumber.Value != String.Empty && this.HF_DelegateNumber.Value.Length > 1)
                objIC = helper.GetContactDetails(this.HF_DelegateNumber.Value);

            if(objIC == null && this.HF_ContactID.Value == String.Empty) // Save this contact
            {
                iMISContactDetails objICD = new iMISContactDetails();
                objICD.ContactID = this.HF_ContactID.Value;
                objICD.FirstName = this.TB_FirstName.Text;
                objICD.LastName = this.TB_LastName.Text;
                objICD.MemberType = this.RB_Member.Checked ? "ME" : String.Empty;
                objICD.InstituteID = this.RB_Member.Checked ? this.DDL_Institutes.SelectedValue : String.Empty;
                objICD.InstituteName = this.RB_Member.Checked ? this.DDL_Institutes.SelectedItem.Text : this.TB_InstituteName.Text;
                objICD.Email = this.TBEmailAddress.Text;
                objICD.WorkPhone = this.TB_Phone.Text.Replace(" ", "");
                objICD.Title = this.TB_Position.Text;
                objICD.CreateNewUser = false;

                List<iMISAddress> lAddresses = new List<iMISAddress>();
                iMISAddress objIA = new iMISAddress();
                objIA.State = this.DDL_States.SelectedValue;
                objIA.IsPreferredMailing = true;

                lAddresses.Add(objIA);
                //objICD.Addresses = lAddresses.ToArray<iMISAddress>();
                objICD.Addresses = lAddresses;

                List<iMISCustomField> lCustomFields = new List<iMISCustomField>();
                iMISCustomField objICF = new iMISCustomField();
                objICF.iMISField = iMISField.EventPreferences;
                objICF.Value = this.TB_EventPreferences.Text;

                lCustomFields.Add(objICF);
                //objICD.CustomFields = lCustomFields.ToArray<iMISCustomField>();
                objICD.CustomFields = lCustomFields;

                objIC = helper.SaveContact(objICD);
            }
            else if(objIC != null)
            {
                var phone = TB_Phone.Text.Replace(" ", "");
                helper.UpdateContactPhoneSpecialReq(objIC.ContactID, phone, TB_EventPreferences.Text);
            }
        }
        catch(Exception ex)
        {
            helper.SaveError(ex);
        }

        return objIC;
    }

    private void SetFocus()
    {
        ScriptManager.RegisterStartupScript(this, typeof(String), "ChangeTabJS", "ChangeTab(1);", true);
        ScriptManager.RegisterStartupScript(this, typeof(String), "ShowAttendeeFormJS", "ShowAttendeeForm(true);", true);
        this.DDL_Institutes.Focus();
        SetInstituteSelection(true);
    }

    protected void BTN_PopulateBadges_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this, typeof(String), "ChangeTabJS", "ChangeTab(1);", true);

            //this.LB_SelectedEvent.Text = this.HF_EventTitle.Value;
            //this.LB_StartDate.Text = DateTime.Parse(this.HF_StartDate.Value).ToShortDateString();
            ////this.LB_RegistrationCutOff.Text = DateTime.Parse(this.HF_RegistrationCutOffDate.Value).ToShortDateString();
            //this.span_MaximumAttendees.InnerText = this.HF_MaxAttendees.Value;
            //this.Lb_CoordinatorEmail.Text = String.Format("<a href='mailto:{0}' style='color:#008cc1;'>{0}</a>", this.HF_CoordinatorEmail.Value);
            //this.Lb_CoordinatorPhone.Text = this.HF_CoordinatorPhone.Value;

            // Display table registrant info
            this.LB_Registrant.Text = this.HF_Registrant.Value;
            this.LB_BTID.Text = this.HF_BTID.Value;

            PopulateBadges();
        }
        catch (Exception ex)
        {
            helper.SaveError(ex);
        }
    }

    // update table number function

    protected void BTN_UpdateTableNumber_Click(object sender, EventArgs e)
    {
        try
        {
            string orderNumber = this.HF_OrderNumber.Value;
            string tableNumber = this.HF_TableNumber.Value;

            helper.UpdateTableNumber(orderNumber, tableNumber);

            string[] tempEventCode = txt_events.Text.Split(' ');
            string eventCode = tempEventCode[0];

            this.SqlDataSource1.SelectParameters.Remove(SqlDataSource1.SelectParameters["EventCode"]);
            this.SqlDataSource1.SelectParameters.Add("EventCode", eventCode);
            //this.GridView1.DataSource = SqlDataSource1;
            //this.GridView1.DataBind();
        }
        catch(Exception ex)
        {
            helper.SaveError(ex);
        }
    }

    protected void BTN_ClearAttendeeForm_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this, typeof(String), "ShowAttendeeFormJS", "ShowAttendeeForm(true);", true);
            ScriptManager.RegisterStartupScript(this, typeof(String), "ChangeTabJS", "ChangeTab(1);", true);

            ClearAttendeeForm();

            List<ParameterType> lParameterTypes = new List<ParameterType>();
            lParameterTypes.Add(ParameterType.Institute);

            IList<CEDAKeys> lParameters = helper.GetParameters(lParameterTypes.ToArray<ParameterType>());

            //re-populate the institute dropdown to allow selection
            this.DDL_Institutes.Items.Clear();
            LoadDropDownList(lParameters, this.DDL_Institutes, (Int32)ParameterType.Institute);

            this.TB_FirstName.Focus();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "chosen1", "$(document).ready(function(){$('.chosen-select').chosen();});", true);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AutoFillValues", "AutoFillValues();", true);
        }
        catch(Exception ex)
        {
            helper.SaveError(ex);
        }
    }

    private void ClearAttendeeForm()
    {
        try
        {
            RB_Member.Enabled = true;
            RB_NonMember.Enabled = true;
            DDL_Institutes.Enabled = true;
            TB_InstituteName.Enabled = true;
            TB_Phone.Enabled = true;
            DDL_States.Enabled = true;
            TB_EventPreferences.Enabled = true;

            pnlRestrictedFields.Visible = false;
            //pnlEmailRequirements.Visible = true;
            //pnlSaveNote.Visible = false;

            this.HF_ContactID.Value = String.Empty;
            this.TB_FirstName.Text = String.Empty;
            this.TB_LastName.Text = String.Empty;
            this.TB_InstituteName.Text = String.Empty;
            this.TB_Phone.Text = String.Empty;
            this.TBEmailAddress.Text = String.Empty;
            this.TB_Position.Text = String.Empty;
            this.TB_EventPreferences.Text = String.Empty;
            this.HF_BadgeNumber.Value = String.Empty;
            this.RB_Member.Checked = true;
            if (this.DDL_Institutes.Items.Count > 0)
            {
                this.DDL_Institutes.SelectedIndex = 0;
            }
            this.DDL_States.SelectedIndex = 0;
            this.DDL_Prefix.SelectedIndex = 0;


            this.TB_Phone.Enabled = this.TBEmailAddress.Enabled = this.TB_EventPreferences.Enabled = true;

            SetInstituteSelection(true);
        }
        catch (Exception ex)
        {
            helper.SaveError(ex);
        }
    }


    // Edit button click
    protected void BTN_LoadAttendee_Click(object sender, EventArgs e)
    {
        try
        {
            this.HF_ContactID.Value = String.Empty;
            Boolean bDisplayError = true;

            IList<iMISBadge> lBadges = helper.GetBadges(this.HF_OrderNumber.Value);
            
            // using websitelogic!?
            if ((lBadges != null) && (lBadges.Count > 0))
            {
                iMISBadge objIB = lBadges.Where(badge => badge.OrderNumber == Double.Parse(this.HF_OrderNumber.Value) &&
                    badge.BadgeNumber == Int32.Parse(this.HF_BadgeNumber.Value)).FirstOrDefault();

                if (objIB != null)
                {
                    // The bill to contact person -the signed-in user, doesn't have a delegate value in iMIS.
                    String sContactID = String.Empty;
                    if (objIB.Delegate == String.Empty)
                    {
                        iMISUser objIU = helper.GetUser();
                        if (objIU != null)
                            sContactID = objIU.ContactID;
                    }
                    else
                        sContactID = objIB.Delegate;


                    iMISContactDetails objICD = helper.GetContactDetails(sContactID); // LN 252 IN THE ORIGINAL
                    //iMISContactDetails objICD = helper.GetContactDetails(txt_contactid.Text.Trim());
                    if (objICD != null)
                    {
                        this.HF_ContactID.Value = objICD.ContactID;
                        this.DDL_Prefix.SelectedValue = objIB.Prefix;

                        if (DDL_Institutes.Items.FindByText(objIB.InstituteName.Trim() + " - " + objIB.StateProvince.Trim()) != null)
                        {
                            DDL_Institutes.SelectedValue = DDL_Institutes.Items.FindByText(objIB.InstituteName + " - " + objIB.StateProvince).Value;
                        }
                        else
                        {
                            TB_InstituteName.Text = objIB.InstituteName;
                        }

                        // if ((objICD.Addresses != null) && (objICD.Addresses.Count() > 0))
                        this.DDL_States.SelectedValue = objIB.StateProvince;

                        this.TB_FirstName.Text = objIB.FirstName;
                        this.TB_LastName.Text = objIB.LastName;
                        this.TB_InstituteName.Text = objIB.InstituteName;
                        this.TB_Position.Text = objIB.Title;
                        this.TB_Phone.Text = objICD.WorkPhone;
                        this.TBEmailAddress.Text = objICD.Email;
                        //this.RadioMember.Style.Add("display", "none");
                        //this.RadioNonMember.Style.Add("display", "none");
                        //this.TB_EventPreferences.Text = objICD.CustomFields.Where(cf => cf.iMISField == iMISField.EventPreferences).FirstOrDefault().Value.ToString();

                        this.TB_EventPreferences.Text = (objICD.CustomFields.Count == 1) ? objICD.CustomFields[0].Value.ToString() : "";

                        this.TB_Phone.Enabled = this.TBEmailAddress.Enabled = false;
                        //this.TB_InstituteName.Enabled =  this.TB_Phone.Enabled = this.TBEmailAddress.Enabled = false;

                        if (DDL_Institutes.Items.FindByText(objIB.InstituteName + " - " + objIB.StateProvince) == null)
                        {
                            SetInstituteSelection(false);
                        }
                        else
                        {
                            SetInstituteSelection(true);
                        }
                        this.TB_FirstName.Focus();
                        bDisplayError = false;
                    }

                    TB_Phone.Enabled = false;
                    TB_EventPreferences.Visible = true;

                    pnlRestrictedFields.Visible = true;
                    //pnlEmailRequirements.Visible = false;
                    //pnlSaveNote.Visible = true;
                }
            }

            if (bDisplayError)
            {
                ShowMessage("The attendee could not be found in the system.", this);
                ScriptManager.RegisterStartupScript(this, typeof(String), "ChangeTabJS", "ChangeTab(1);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(String), "ShowAttendeeFormJS", "ShowAttendeeForm(true);", true);
                ScriptManager.RegisterStartupScript(this, typeof(String), "ChangeTabJS", "ChangeTab(1);", true);
            }
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "chosen2", "$(document).ready(function(){$('.chosen-select').chosen();});", true);

        }
        catch (Exception ex)
        {
            helper.SaveError(ex);
        }
    }

    private void SetInstituteSelection(Boolean bShowInstitutes)
    {
        this.RB_Member.Checked = bShowInstitutes;
        this.RB_NonMember.Checked = !bShowInstitutes;
        this.div_Institutes.Attributes.Add("style", bShowInstitutes ? "display:block;" : "display:none;");
        this.div_InstituteName.Attributes.Add("style", bShowInstitutes ? "display:none;" : "display:block;");
    }

    protected void BTN_DeleteBadge_Click(object sender, EventArgs e)
    {
        try
        {
            if(this.HF_OrderNumber.Value != String.Empty && this.HF_BadgeNumber.Value != String.Empty)
            {
                p_DeleteBadge objParams = new p_DeleteBadge();
                objParams.OrderNumber = Double.Parse(this.HF_OrderNumber.Value);
                objParams.BadgeNumber = Int32.Parse(this.HF_BadgeNumber.Value);

                // PROODUCT_CODE / EVENT CODE
                // ID / DELEGATE
                // TOP 1 FOR SEQN

                string[] tempEventCode = txt_events.Text.Split(' ');
                string eventCode = tempEventCode[0];

                //CWC-62 - Delete CORPGUEST activity from the activity table
                if(helper.DeleteBadge(objParams, eventCode, this.HF_DelegateNumber.Value))
                {
                    PopulateBadges();
                    ClearAttendeeForm();
                }
                else
                {
                    ShowMessage("An error has occurred, please contact IT", this);
                }

                ScriptManager.RegisterStartupScript(this, typeof(String), "ChangeTabJS", "ChangeTab(1)", true);
            }
        }
        catch(Exception ex)
        {
            helper.SaveError(ex);
        }
    }

    private void PopulateBadges()
    {
        try
        {
            if (this.HF_OrderNumber.Value == string.Empty)
                return;

            this.HF_CurrentAttendees.Value = "0";

            IList<iMISBadge> lBadges = helper.GetBadges(this.HF_OrderNumber.Value);

            if (lBadges != null)
                this.HF_CurrentAttendees.Value = lBadges.Count.ToString();

            this.RPT_Badges.DataSource = lBadges;
            this.RPT_Badges.DataBind();
        }
        catch (Exception ex)
        {
            helper.SaveError(ex);
        }
    }

    private void AttachAttributes()
    {
        try
        {
            this.RB_Member.Attributes.Add("onclick", "SetInstituteSelection(true);");
            this.RB_NonMember.Attributes.Add("onclick", "SetInstituteSelection(false);");
        }
        catch (Exception ex)
        {
            helper.SaveError(ex);
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;
    }
    protected void GridView1_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        // Indicate whether the update operation succeeded.
        if (e.Exception == null)
        {
            string eventCode = txt_events.SelectedValue;
            PopulateRegisteredEvents(eventCode.Substring(0, eventCode.IndexOf(' ')));
            _UpdatePanel.Update();
        }
        else
        {
            e.ExceptionHandled = true;
            helper.SaveError(e.Exception);
        }
    }

    
    [System.Web.Services.WebMethod]
    public static string AutoFillFields(string email)
    {
        string prefix = "";
        string firstName = "";
        string lastName = "";
        string position = "";
        string coyid = "";
        string phone = "";
        string state = "";
        string specialReqts = "";
        string coyname = "";

        AuthHeader authHeader = new AuthHeader();
        authHeader.TokenID = "3ed9a25f-9f6e-4d40-a2a9-9e715191b7e0";

        ClientService.ClientService client = new ClientService.ClientService();
        client.AuthHeaderValue = authHeader;
        
        //Validate email syntax
        Regex objRegex = new Regex("\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*", RegexOptions.IgnoreCase);
        //if (!objRegex.IsMatch(email))
            //return;

        //Check iMIS for existing user based on email address
        //iMISContact imisContact = null;

        var aspNetUser = client.GetAspNetUser(email);
        var imisContact = client.GetContact(email.Trim());

        if(imisContact != null)
        {
            // Get the Title values
            //Helper helper = new Helper();
            //var lstTitles = helper.GetTitles();

            var imisContactDetails = client.GetContactDetails(imisContact.ContactID);

            if(imisContactDetails.Addresses != null && imisContactDetails.Addresses.Count() > 0)
            {
                state = imisContactDetails.Addresses[0].State;
            }
            
            // Assign values
            firstName = imisContact.FirstName;
            lastName = imisContact.LastName;
            phone = imisContactDetails.WorkPhone;
            position = imisContactDetails.Title;
            prefix = imisContactDetails.Prefix;
            coyid = imisContactDetails.InstituteID;
            coyname = imisContactDetails.InstituteName;
            //specialReqts = imisContactDetails.CustomFields.Where(cf => cf.iMISField == (ClientService.iMISField)iMISField.EventPreferences).FirstOrDefault().Value.ToString();
            foreach(var test in imisContactDetails.CustomFields)
            {
                if (test.iMISField.ToString() == "EventPreferences")
                    specialReqts = test.Value.ToString();
            }
        }


        string fieldValues = prefix + 
            "::" + firstName + 
            "::" + lastName +
            "::" + position +
            "::" + coyid +
            "::" + phone +
            "::" + state +
            "::" + specialReqts +
            "::" + coyname;

        return fieldValues;

        //return "Text 3 value from C#::Text 5 value from C#";
    }
}