<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AllCorporateTables.aspx.cs" Inherits="AllCorporateTables" EnableEventValidation="false" %>
<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Corporate Tables</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="shortcut icon" href="about:blank" />
    <%--<script src="js/jquery-ui.min.js"></script>--%>
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/jquery.hideseek.min.js"></script>
    <%--<link href="css/bootstrap.theme.min.css" rel="stylesheet" />--%>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/custom.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.4.2/chosen.css" />
</head>
<body class="container">
    <section>
        <form id="form1" class="well" runat="server">
            <script type="text/javascript">
                function pageLoad() {
                    jQuery('input#search').hideseek({
                        highlight: true
                    });
                }
            </script>
            <asp:ScriptManager ID="_ScriptManager" runat="server" EnablePageMethods="true" EnablePartialRendering="true"></asp:ScriptManager>

            <h1 runat="server" id="CorpTablesHeader">CEDA iMIS Event Registrations</h1>
            <br />
            <br />

            <div class="form-inline">
                <div class="col-md-12">
                    <%--<asp:TextBox ID="txt_events" type="text" class="form-control" runat="server"></asp:TextBox>--%>
                    <asp:DropDownList runat="server" ID="txt_events" CssClass="chosen-select form-control" AppendDataBoundItems="true" 
                        OnSelectedIndexChanged="BTN_DisplayTableRegs_Click" AutoPostBack="true">
                        <%--<asp:ListItem Text="--Select Event--" ></asp:ListItem>--%>
                    </asp:DropDownList>
                </div>
            </div>
            <br /><br />
            <input id="search" name="search" style="width: 97.4%; margin-left: 1.3%;" placeholder="Filter records... start typing here!" type="text" data-list=".list" class="form-control" />
            <br />

            <ul class="nav nav-tabs">
                <li class="active">
                    <a href="#1" id="CorpAHref" data-toggle="tab" onclick="PageRefresh(); return false;">Corporate Tables</a>
                </li>
                <li>
                    <a href="#2" data-toggle="tab">Table Allocation</a>
                </li>
                <li>
                    <a href="#3" data-toggle="tab">Reports</a> 
                </li>
                <%--<li>
                    <a href="#4" data-toggle="tab"></a>
                </li>--%>
            </ul>
            <br />

            <div class="tab-content clearfix">
                <div class="tab-pane active" id="1">

                    <div class="text-center" id="buttonRegoDiv" runat="server">
                        <asp:Button ID="BTN_DisplayTableRegs" type="submit" CssClass="btn btn-lg btn-success" Text="Display table registrations for event" runat="server" 
                            OnClick="BTN_DisplayTableRegs_Click" UseSubmitBehavior="false" CausesValidation="false"></asp:Button>
                    </div>
                    <%--<h4>Show all corp tables</h4>--%>
                    <!-- FONT AWESOME ICONS <i class="fa fa-camera-retro"></i> fa-camera-retro -->

                    <%--<input id="search" name="search" placeholder="Filter records... start typing here!" type="text" data-list=".list" class="form-control" />--%>

                    <div id="corpTableDiv" class="">
                        <asp:UpdatePanel ID="_UpdatePanel" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

                                <div class="container content">
                                    <div class="row">
                                        <div id="EventInformation" class="well-sm text-center text-info text-uppercase" runat="server">
                                            <b><asp:Label ID="lbl_EventTitle" runat="server"></asp:Label></b>
                                            <p>
                                                <asp:Label ID="lbl_EventDate" runat="server"></asp:Label>
                                                <asp:Label ID="lbl_EventTime" runat="server"></asp:Label>
                                                <asp:Label ID="lbl_EventCoordinator" runat="server"></asp:Label>
                                            </p>
                                        </div>

                                        <div class="col-md-12">
                                            <!-- ============================================================================================================= -->
                                            <!-- REGISTERED EVENTS -->
                                            <!-- ============================================================================================================= -->
                                            <div id="div_RegisteredEventsContainer" class="well-sm">
                                                <div id="div_RegisteredEvents">
                                                    <%--<h4><span>Select an event</span></h4>--%> 
                                                    <asp:Label ID="lbl_NoTableRegos" runat="server" style="display: none; color: red; font-weight: bold; text-align: center;">No table registrations found for this event</asp:Label>
                                            
                                                    <asp:Repeater ID="RPT_RegisteredEvents" runat="server">
                                                        <ItemTemplate>
                                                            <div class="row list">

                                                                <div class="col-md-12 form-group">
                                                                    <span><p>
                                                                        <i class="fa fa-user"></i>&nbsp;<%# Eval("Last_First").ToString() %>|
                                                                        <i class="fa fa-hashtag"></i>&nbsp;<%# Eval("BT_ID").ToString() %>|
                                                                        <i class="fa fa-university"></i>&nbsp;<%# Eval("COMPANY").ToString() %>|
                                                                        <i class="fa fa-users"></i>&nbsp;<%# Eval("NumPeople").ToString() %>|
                                                                        <i class="fa fa-pie-chart"></i>&nbsp;<%# Eval("RegistrationType").ToString() %>|
                                                                        <span style="color: red">Table Number </span>
                                                                        <asp:TextBox ID="TXT_TableNum" CssClass="updateTableInput" runat="server" Text='<%# Int32.Parse(Eval("TableNumber").ToString()) > 0 ? Eval("TableNumber").ToString() :  "" %>' />
                                                                        <input type="button" class="btn btn-primary btn-sm" value="Update table number" onclick="StoreUpdateTableInfo('<%# Eval("OrderNumber").ToString() %>', jQuery(this).prev().val());UpdateTableNumber();" />
                                                                    </p></span>
                                                                    <%--<p>
                                                                        <%# (Convert.ToDateTime(Eval("RegistrationCutOffDate")) - DateTime.Now).Ticks <= 0 ? "Event registration is closed, please contact the event coordinator for any changes to your registration on: " + String.Format("<a href='mailto:{0}' style='color:#008cc1;'>{0}</a>", Eval("CoordinatorEmail")) + ", " + Eval("CoordinatorPhone") : "" %>
                                                                    </p>--%>

                                                                    <!-- Buttons -->
                                                                    <p>
                                                                        <input type="button" class="btn btn-primary" id="<%# (Convert.ToDateTime(Eval("RegistrationCutOffDate")) - DateTime.Now).Ticks %>" value="Attendees" onclick="StoreEvent('<%# Eval("EventCode").ToString() %>    ', '<%# Eval("Title").ToString().Replace("'", "~") %>    ', '<%# Eval("OrderNumber").ToString() %>    ', '<%# Eval("MaxAttendees").ToString() %>    ', '<%# Eval("StartDate").ToString() %>    ', '<%# Eval("CoordinatorEmail").ToString() %>    ', '<%# Eval("CoordinatorPhone").ToString() %>    ', '<%# Eval("RegistrationCutOffDate").ToString() %>    ', '<%# Eval("Last_First").ToString() %>', '<%# Eval("BT_ID").ToString() %>'); DisplayBadges();" />
                                                                    </p>
                                                                </div>

                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                        </div>


                                        <a name="a_Badges"></a>
                                        <div id="div_BadgesAndAttendeesContainer" class="well-sm" style="color: #000000; display: none;">
                                            <h3><span style="text-decoration: underline; color: #31708f;">Add/edit attendees</span></h3>

                                            <!-- ============================================================================================================= -->
                                            <!-- BADGES -->
                                            <!-- ============================================================================================================= -->
                                            <div id="div_Badges" style="color: #000000;">
                                                <p style="display: none;">
                                                    <asp:Literal ID="LT_EventAlerts" runat="server" />
                                                    <b>Selected event:</b>
                                                    <asp:Label ID="LB_SelectedEvent" runat="server" />
                                                    <br />
                                                    <b>Table of:</b><span id="span_MaximumAttendees" runat="server" />
                                                    <br />
                                                    <b>Event date:</b><asp:Label ID="LB_StartDate" runat="server" />
                                                    <br />
                                                    <b>Event coordinator email:</b><asp:Label ID="Lb_CoordinatorEmail" runat="server" />
                                                    <br />
                                                    <b>Event coordinator phone:</b><asp:Label ID="Lb_CoordinatorPhone" runat="server" />
                                                    <br />
                                                </p>
                                                <p>
                                                    <b>Table registered under: </b>
                                                    <i class="fa fa-user"></i>&nbsp;<asp:Label ID="LB_Registrant" runat="server" />
                                                    <i class="fa fa-hashtag"></i>&nbsp;<asp:Label ID="LB_BTID" runat="server" />
                                                </p>
                                                <br />

                                                <div id="BadgesWrapper">
                                                    <asp:Repeater ID="RPT_Badges" runat="server">
                                                        <ItemTemplate>
                                                            <div class="row">
                                                                <div class="col-md-10">
                                                                    <p style="margin-left: 15px;">
                                                                        <a href="#nogo" onclick='LoadAttendee(<%# Eval("BadgeNumber").ToString() %>);'>Edit</a> | 
                                                                <a href="#nogo" onclick='DeleteBadge(<%# Eval("BadgeNumber").ToString() %>, "<%# Eval("Delegate").ToString() %>");'>Delete</a> | 
                                                                <i class="icon-tags"></i>&nbsp;<%# String.Format("{0} {1} {2}", Eval("Prefix"), Eval("FirstName"), Eval("LastName")).Trim() %>- <%# Eval("InstituteName").ToString() %>
                                                                    </p>
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>

                                                    <div class="control-group">
                                                        <label class="control-label"></label>
                                                        <div class="controls">
                                                            <input type="button" class="btn btn-primary" id="btnAddDisabled" onclick="ClearAttendeeForm();" value="Add Attendee" />
                                                            <input type="button" class="btn btn-success" onclick="ChangeTab(0); ShowAttendeeForm(false);" value="Back To Table Registrations" /> <br />
                                                            <%--<span style="color: red;">Add option disabled till we fix an issue</span>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <!-- ============================================================================================================= -->
                                            <!-- ATTENDEE -->
                                            <!-- ============================================================================================================= -->
                                            <a name="a_Attendee"></a>
                                            <div id="div_Attendee" style="display: none; color: #000000;">
                                                <asp:ValidationSummary ID="vsAttendees"
                                                    ValidationGroup="CreateContact"
                                                    runat="server"
                                                    HeaderText="You must enter a valid value in the following fields:"
                                                    DisplayMode="BulletList"
                                                    EnableClientScript="true" ForeColor="Red" />

                                                <asp:Panel ID="pnlRestrictedFields" runat="server">
                                                    <%--<p>
                                                        Please contact your local <a href="mailto:it.support@ceda.com.au">CEDA Office</a> if you wish to update the company, contact details or special requirements.
                                                    </p>--%>
                                                </asp:Panel>
                                                <div class="form-horizontal">

                                                    <div class="form-group">
                                                        <label class="control-label col-sm-2">Email Address</label>
                                                        <div class="col-sm-10">
                                                            <asp:TextBox ID="TBEmailAddress" runat="server" CssClass="form-control" /><div id="loading" style="display: none;"><br /><img src="fonts/ajax-loader.gif" />Please wait, checking if user exists...</div>
                                                            <asp:RegularExpressionValidator ID="RFV_EmailAddress_InvalidFormat" ForeColor="Red" runat="server" ErrorMessage="Invalid Email Format." ControlToValidate="TBEmailAddress" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="CreateContact" SetFocusOnError="true" Display="Dynamic" />

                                                            <%--<asp:Panel ID="pnlEmailRequirements" runat="server">
                                                                <p>
                                                                    Please enter the email address of the person attending the event.
                                                                </p>
                                                                <p>
                                                                    The registration process relies on an individual email address to identify each attendee. Using a duplicate
                                                email address (such as a generic or personal assistant’s email address) will prevent you from registering
                                                more than one person.
                                                                </p>
                                                            </asp:Panel>--%>

                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <label class="control-label asterix col-sm-2">Title</label>
                                                        <div class="col-sm-10">
                                                            <asp:DropDownList ID="DDL_Prefix" runat="server" CssClass="chosen-select form-control" />
                                                            <asp:RequiredFieldValidator ID="RFV_Prefix" runat="server" ForeColor="Red" ErrorMessage="Title required." ControlToValidate="DDL_Prefix" ValidationGroup="CreateContact" SetFocusOnError="true" Display="Dynamic" />
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <label class="control-label asterix col-sm-2">First Name</label>
                                                        <div class="col-sm-10">
                                                            <asp:TextBox ID="TB_FirstName" runat="server" CssClass="form-control" />
                                                            <asp:RequiredFieldValidator ID="RFV_FirstName" runat="server" ForeColor="Red" ErrorMessage="First name required." ControlToValidate="TB_FirstName" ValidationGroup="CreateContact" SetFocusOnError="true" Display="Dynamic" />
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <label class="control-label asterix col-sm-2">Last Name</label>
                                                        <div class="col-sm-10">
                                                            <asp:TextBox ID="TB_LastName" runat="server" CssClass="form-control" />
                                                            <asp:RequiredFieldValidator ID="RFV_LastName" runat="server" ForeColor="Red" ErrorMessage="Last name required." ControlToValidate="TB_LastName" ValidationGroup="CreateContact" SetFocusOnError="true" Display="Dynamic" />
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <label class="control-label asterix col-sm-2">Position</label>
                                                        <div class="col-sm-10">
                                                            <asp:TextBox ID="TB_Position" runat="server" CssClass="form-control" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" runat="server" ErrorMessage="Position required." ControlToValidate="TB_Position" ValidationGroup="CreateContact" SetFocusOnError="true" Display="Dynamic" />
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <label class="control-label asterix col-sm-2">Company</label>
                                                        <div class="col-sm-10">
                                                            <asp:Label class="radio-inline" id="RadioMember" runat="server">
                                                                <asp:RadioButton ID="RB_Member" GroupName="MemberType" Checked="true" runat="server" />Member
                                                            </asp:Label>
                                                            <asp:Label class="radio-inline" id="RadioNonMember" runat="server">
                                                                <asp:RadioButton ID="RB_NonMember" GroupName="MemberType" runat="server" />Non-member
                                                            </asp:Label>

                                                            <div style="margin-top: 4px;">
                                                                <div id="div_Institutes" runat="server">
                                                                    <asp:DropDownList ID="DDL_Institutes" CssClass="chosen-select control-select-medium form-control" runat="server" />
                                                                    <%--<br />
                                                                    If your guest’s company and state do not appear in the list they are not a CEDA member. Please click the non member option and type the company name into the textbox. <br />
                                                    Please note corporate table prices are based on the host’s membership status and not that of their guests.--%>
                                                                </div>
                                                                <div id="div_InstituteName" style="display: none;" runat="server">
                                                                    <asp:TextBox ID="TB_InstituteName" CssClass="form-control" MaxLength="80" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                            

                                                    <asp:Panel CssClass="form-group" runat="server" ID="pPhone">
                                                        <label class="control-label col-sm-2">Phone</label>
                                                        <div class="col-sm-10">
                                                            <asp:TextBox ID="TB_Phone" runat="server" CssClass="form-control" />
                                                            <asp:RegularExpressionValidator ID="REV_PhoneNumberValidation" ForeColor="Red" runat="server" ErrorMessage="Please enter a phone number with 10 digits or more." ControlToValidate="TB_Phone" ValidationExpression="^(?=(?: *\d){10})[ \d]+$" ValidationGroup="CreateContact" SetFocusOnError="true" Display="Dynamic" />
                                                        </div>
                                                        <div class="controls" style="display: none;">
                                                            Your phone number must include 10 digits including the area code with no spaces.
                                                        </div>
                                                    </asp:Panel>

                                                    <div class="form-group">
                                                        <label class="control-label asterix col-sm-2">State</label>
                                                        <div class="col-sm-10">
                                                            <asp:DropDownList ID="DDL_States" runat="server" CssClass="chosen-select form-control" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ForeColor="Red" ErrorMessage="State required." ControlToValidate="DDL_States" ValidationGroup="CreateContact" Display="Dynamic" />
                                                        </div>
                                                    </div>

                                                    <asp:Panel CssClass="form-group" runat="server" ID="pEventPreferences">
                                                        <label class="control-label col-sm-2">Special requirements</label>
                                                        <div class="col-sm-10">
                                                            <asp:TextBox ID="TB_EventPreferences" TextMode="MultiLine" Height="100px" runat="server" CssClass="form-control" />
                                                        </div>
                                                    </asp:Panel>

                                                    <%--<asp:Panel ID="pnlSaveNote" runat="server">
                                                        <div class="control-group">
                                                            <div class="controls">
                                                                Please note that some information you entered may not have saved because you are unable to overwrite existing information in our database. For questions regarding this, please contact the event coordinator.
                                                            </div>
                                                        </div>
                                                    </asp:Panel>--%>

                                                    <div class="control-group">
                                                        <label class="control-label"></label>
                                                        <div class="controls">
                                                            <asp:Button ID="BTN_SaveContact" CssClass="btn btn-primary" OnClientClick="BTN_SaveContact_OnClientSideClick();ShowAttendeeForm(true);" OnClick="BTN_SaveContact_Click" Text="Save" ValidationGroup="CreateContact" runat="server" />
                                                            <input type="button" class="btn btn-danger" onclick="ShowAttendeeForm(false); location.hash='#a_Badges'; return true;" value="Cancel" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>

                                <asp:Button ID="BTN_PopulateBadges" OnClick="BTN_PopulateBadges_Click" Style="display: none;" runat="server" />
                                <asp:Button ID="BTN_ClearAttendeeForm" OnClick="BTN_ClearAttendeeForm_Click" Style="display: none;" runat="server" />
                                <asp:Button ID="BTN_LoadAttendee" OnClick="BTN_LoadAttendee_Click" Style="display: none;" runat="server" />
                                <asp:Button ID="BTN_DeleteBadge" OnClick="BTN_DeleteBadge_Click" Style="display: none;" runat="server" />
                                <asp:Button ID="BTN_UpdateTableNumber" OnClick="BTN_UpdateTableNumber_Click" Style="display: none;" runat="server" />

                                <asp:HiddenField ID="HF_OrderNumber" runat="server" />
                                <asp:HiddenField ID="HF_BadgeNumber" runat="server" />
                                <asp:HiddenField ID="HF_DelegateNumber" runat="server" />
                                <asp:HiddenField ID="HF_EventCode" runat="server" />
                                <asp:HiddenField ID="HF_EventTitle" runat="server" />
                                <asp:HiddenField ID="HF_RegistrationCutOffDate" runat="server" />
                                <asp:HiddenField ID="HF_MaxAttendees" runat="server" />
                                <asp:HiddenField ID="HF_CurrentAttendees" runat="server" />
                                <asp:HiddenField ID="HF_StartDate" runat="server" />
                                <asp:HiddenField ID="HF_ContactID" runat="server" />
                                <asp:HiddenField ID="HF_RegistrationClosed" runat="server" />
                                <asp:HiddenField ID="HF_CoordinatorEmail" runat="server" />
                                <asp:HiddenField ID="HF_CoordinatorPhone" runat="server" />
                                <asp:HiddenField ID="HF_Registrant" runat="server" />
                                <asp:HiddenField ID="HF_BTID" runat="server" />
                                <asp:HiddenField ID="HF_TableNumber" runat="server" />
                                <asp:HiddenField ID="HF_TableUpdateRequired" runat="server" />

                                <!-- Hidden fields for autofill -->
                                <asp:HiddenField ID="HF_Att_Firstname" runat="server" />
                                <asp:HiddenField ID="HF_Att_Lastname" runat="server" />
                                <asp:HiddenField ID="HF_Att_Position" runat="server" />
                                <asp:HiddenField ID="HF_Att_Company" runat="server" />
                                <asp:HiddenField ID="HF_Att_Phone" runat="server" />
                                <asp:HiddenField ID="HF_Att_State" runat="server" />
                                <asp:HiddenField ID="HF_Att_Reqts" runat="server" />
                                </span>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txt_events"  EventName="SelectedIndexChanged" />
                                <%--<asp:AsyncPostBackTrigger ControlID="TBEmailAddress" EventName="TextChanged" />--%>
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                </div> <!-- end tab 1 content -->

                <div class="tab-pane list" id="2">

                    <%--<div class="col-md-12">
                        <asp:DropDownList runat="server" ID="DDL_EventCodes" CssClass="chosen-select form-control" AppendDataBoundItems="true"
                             OnSelectedIndexChanged="TableAllocationsRefresh" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <br /><br />--%>
                    <%--<asp:Button ID="btn_exportToExcel" CssClass="btn btn-success btn-xs" OnClick="BTN_ExportToExcel_Click" runat="server" Text="Export to Excel" /> <br /><br />--%>
                    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1" ></dx:ASPxGridViewExporter>
                    <%--<dx:ASPxButton ID="btnPdfExport" CssClass="btn btn-success btn-xs" runat="server" Text="Export to Excel" UseSubmitBehavior="False" OnClick="btnPdfExport_Click" Visible="false" />--%>

                    <asp:UpdatePanel ID="_GridUpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <%--<asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AllowSorting="True" CellPadding="4" ForeColor="#333333"
                                 GridLines="None" CssClass="table table-bordered" EnableSortingAndPagingCallbacks="true" AutoGenerateColumns="False"
                                OnRowUpdated="GridView1_RowUpdated" OnRowDataBound="GridView1_RowDataBound" Visible="false">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>

                                <Columns>
                                    <asp:CommandField ShowEditButton="True"></asp:CommandField>
                                    <asp:BoundField DataField="OrderNumber" HeaderText="OrderNumber" />
                                    <asp:BoundField DataField="Registration Type" HeaderText="Reg. Type" ReadOnly="True" SortExpression="Registration Type"></asp:BoundField>
                                    <asp:BoundField DataField="TableNumber" HeaderText="Table No." SortExpression="TableNumber"></asp:BoundField>
                                    <asp:BoundField DataField="BT_ID" HeaderText="BT_ID" SortExpression="BT_ID" ReadOnly="True"></asp:BoundField>
                                    <asp:BoundField DataField="Member_Type" HeaderText="Mem. Type" SortExpression="Member_Type" ReadOnly="True"></asp:BoundField>
                                    <asp:BoundField DataField="Company" HeaderText="Company" SortExpression="Company" ReadOnly="True"></asp:BoundField>
                                    <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" ReadOnly="True"></asp:BoundField>
                                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" ReadOnly="True"></asp:BoundField>
                                </Columns>

                                <EditRowStyle BackColor="#999999"></EditRowStyle>
                                <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True"></FooterStyle>
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                <PagerStyle HorizontalAlign="Center" BackColor="#284775" ForeColor="White"></PagerStyle>
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                                <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
                                <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>
                                <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                            </asp:GridView>--%>

                            <div style="text-align: left">
                                <dx:ASPxHyperLink ID="hlSave" runat="server" Text="[SAVE]">
                                    <ClientSideEvents Click="function(s, e){ ASPxGridView1.UpdateEdit(); }" />
                                </dx:ASPxHyperLink>
                                <dx:ASPxHyperLink ID="hlCancel" runat="server" Text="[CANCEL]">
                                    <ClientSideEvents Click="function(s, e){ ASPxGridView1.CancelEdit(); }" />
                                </dx:ASPxHyperLink>
                            </div>
                            <br />

                            <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" KeyFieldName="OrderNumber;LineNumber" Theme="DevEx" EnableTheming="True"
                                Width="100%" SettingsPager-PageSize="1500" Settings-ShowStatusBar="Hidden" SettingsPager-Position="Top">
                                <SettingsEditing Mode="Batch"></SettingsEditing>
                                <Columns>
                                    <dx:GridViewDataTextColumn FieldName="OrderNumber" ReadOnly="True" VisibleIndex="1" CellStyle-HorizontalAlign="Left">
                                        <EditFormSettings Visible="False"></EditFormSettings>
                                        <PropertiesTextEdit DisplayFormatString="F0"></PropertiesTextEdit>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn ReadOnly="True" FieldName="Registration Type" VisibleIndex="2">
                                        <EditFormSettings Visible="False"></EditFormSettings>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="TableNumber" VisibleIndex="3">
                                        <HeaderStyle BackColor="#ff6666" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn ReadOnly="True" FieldName="BT_ID" VisibleIndex="4">
                                        <EditFormSettings Visible="False"></EditFormSettings>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn ReadOnly="True" FieldName="MEMBER_TYPE" VisibleIndex="5">
                                        <EditFormSettings Visible="False"></EditFormSettings>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn ReadOnly="True" FieldName="COMPANY" VisibleIndex="6">
                                        <EditFormSettings Visible="False"></EditFormSettings>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn ReadOnly="True" FieldName="Title" VisibleIndex="7">
                                        <EditFormSettings Visible="False"></EditFormSettings>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn ReadOnly="True" FieldName="Name" VisibleIndex="8">
                                        <EditFormSettings Visible="False"></EditFormSettings>
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                            </dx:ASPxGridView>

                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString='<%$ ConnectionStrings:iMISConnectionString %>'
                                SelectCommand="CEDA_TableAllocation" SelectCommandType="StoredProcedure" UpdateCommand="UPDATE Order_Meet SET
                                [UF_9] = @TableNumber WHERE Order_Number = @OrderNumber">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="Q151119" Name="EventCode" Type="String"></asp:Parameter>
                                </SelectParameters>
                                <UpdateParameters>
                                    <asp:Parameter Type="String" Name="TableNumber" />
                                    <asp:Parameter Type="String" Name="OrderNumber" />
                                </UpdateParameters>
                            </asp:SqlDataSource>
                            </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txt_events"  EventName="SelectedIndexChanged" />
                        </Triggers>
                     </asp:UpdatePanel>
                </div>

                <div class="tab-pane" id="3">
                    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:HyperLink ID="HL_EventRegistrations" CssClass="btn btn-success btn-sm" runat="server" role="button"
                                 NavigateUrl="http://vicsql04/reportserver/?%2fEvents%2fRegistrations+by+Event+Code&rs:Command=Render&Event="
                                 Target="_blank">Event Registrations</asp:HyperLink>
                            
                            <asp:HyperLink ID="HL_NameBadges" CssClass="btn btn-success btn-sm" runat="server" role="button" 
                                NavigateUrl="http://vicsql04/reportserver/?%2fEvents%2fCEDA+Name+Badges&rs:Command=Render&Event="
                                Target="_blank">Name Badges
                            </asp:HyperLink>                                
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txt_events" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>

                <%--<div class="tab-pane" id="4">
                    <h4>TBD</h4>
                </div>--%>

            </div> <!-- end tab content -->
        </form>
    </section>

    <script type="text/javascript" src='js/CorporateTables.aspx.js'></script>
    <script src="js/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        jQuery(document).ready(function(){
            jQuery(".chosen-select").chosen();

            // JS -> C# -> JS calls
            jQuery("input#TBEmailAddress").change(function () {
                PageMethods.AutoFillFields("temp", OnSuccess);

                function OnSuccess(response, userContext, methodName) {
                    var userFields = response.split("::");
                    jQuery("input#TB_FirstName").val(userFields[0]);
                    jQuery("input#TB_LastName").val(userFields[1]);
                }
            });
        });

        function filterMethod()
        {
            jQuery('input#search').hideseek({
                highlight: true
            });
        }
    </script>
</body>
</html>
