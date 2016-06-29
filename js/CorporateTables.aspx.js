var MasterPage =
{
    html: "",
    lNationalMembers: new Array(),
    aliasCP: "ctl00_CPH_Body_",
    aliasMP: "ctl00_",
    urlAuthority: null
}

function ChangeTab(type) {
    var lSections = new Array("div_RegisteredEvents", "div_Badges");

    if (type != 1) {
        document.getElementById("div_RegisteredEventsContainer").style.display = "block";
        document.getElementById("div_BadgesAndAttendeesContainer").style.display = "none";
    }
    else {
        document.getElementById("div_RegisteredEventsContainer").style.display = "none";
        document.getElementById("div_BadgesAndAttendeesContainer").style.display = "block";

    }

    for (var i = 0; i <= 1; i++) {
        if (document.getElementById(lSections[(i)]).style.display == "block") {
            location.hash = "#" + lSections[(i)].replace("div_", "a_"); // set focus to the active tab.
            break;
        }
    }
}

function ShowAttendeeForm(show) {

    document.getElementById("div_Attendee").style.display = show ? "block" : "none";
    document.getElementById("BadgesWrapper").style.display = !show ? "block" : "none";

    jQuery(document).ready(function () {
        if (window.location.href.indexOf("/CorporateTables.aspx") > -1) {
            jQuery('input[type="button"][value="Attendees"]').each(function () {
                if (jQuery(this).attr('id') <= 0) {
                    jQuery(this).removeClass('btn-primary').addClass('btn-primary-turned-off grey');
                }
            });
        }
    });
}


function StoreEvent(eventCode, eventTitle, orderNumber, maxAttendees, startDate, coordinatorEmail, coordinatorPhone, registrationCutOffDate, registrant, btId) {
    //console.log("SSSSSSSS Event Code: " + eventCode);
    document.getElementById("HF_EventCode").value = eventCode;
    document.getElementById("HF_EventTitle").value = eventTitle;
    document.getElementById("HF_OrderNumber").value = orderNumber;
    document.getElementById("HF_RegistrationCutOffDate").value = registrationCutOffDate;
    document.getElementById("HF_MaxAttendees").value = maxAttendees;
    document.getElementById("HF_StartDate").value = startDate;
    document.getElementById("HF_CoordinatorEmail").value = coordinatorEmail;
    document.getElementById("HF_CoordinatorPhone").value = coordinatorPhone;

    // Set the values of the table / person under who these attendees are added/edited/updated
    document.getElementById("HF_Registrant").value = registrant;
    document.getElementById("HF_BTID").value = btId;
    //document.getElementById("HF_NumRegistered") = numRegistered;
}

function StoreUpdateTableInfo(orderNumber, tableNumber) {
    document.getElementById("HF_OrderNumber").value = orderNumber;
    document.getElementById("HF_TableNumber").value = tableNumber;
    document.getElementById("HF_TableUpdateRequired").value = "1";
    document.getElementById("BTN_DisplayTableRegs").click();
}

//function PageRefresh() {
//    document.getElementById("BTN_DisplayTableRegs").click();
//}

function UpdateTableNumber() {
    //document.getElementById("BTN_UpdateTableNumber").click();
}

function DisplayBadges() {

    document.getElementById("BTN_PopulateBadges").click();
    //var elem = jQuery(obj).parent().prev().clone();
    //jQuery("#div_BadgesAndAttendeesContainer").prepend(elem);    
}

function ClearAttendeeForm() {
    var maxAttendees = document.getElementById("span_MaximumAttendees");
    var currentAttendees = document.getElementById("HF_CurrentAttendees");

    if (CheckIfEventIsClosed() == true)
        return;

    if (currentAttendees.value != "0") {
        if (parseInt(currentAttendees.value) >= parseInt(maxAttendees.innerText)) {
            //MasterPage.ShowMessage("Seating for this table has reached capacity.", "");
            alert("Seating for this table has reached capacity.");
            return;
        }
    }

    document.getElementById("BTN_ClearAttendeeForm").click();
}

function LoadAttendee(badgeNumber) {

    if (CheckIfEventIsClosed() == true)
        return;

    document.getElementById("HF_BadgeNumber").value = badgeNumber;
    document.getElementById("BTN_LoadAttendee").click();
}

function DeleteBadge(badgeNumber, delegateNumber) {

    if (CheckIfEventIsClosed() == true)
        return;

    if (confirm("Are you sure that you want to delete the attendee?")) {
        document.getElementById("HF_BadgeNumber").value = badgeNumber;

        if (delegateNumber.length > 1)
            document.getElementById("HF_DelegateNumber").value = delegateNumber;

        document.getElementById("BTN_DeleteBadge").click();
    }
}

function SetInstituteSelection(showInstitutes) {
    document.getElementById("div_Institutes").style.display = showInstitutes ? "block" : "none";
    document.getElementById("div_InstituteName").style.display = showInstitutes ? "none" : "block";

    if (showInstitutes) {
        document.getElementById("RB_Member").checked = true;
    }
    else {
        document.getElementById("RB_NonMember").checked = true;
        document.getElementById("TB_InstituteName").focus();
    }
}

function CheckIfEventIsClosed() {
    var eventRegistrationClosed = document.getElementById("HF_RegistrationClosed");
    var coordinatorEmail = document.getElementById("HF_CoordinatorEmail");
    var coordinatorPhone = document.getElementById("HF_CoordinatorPhone");

    if (eventRegistrationClosed.value == "true") {
        var message = "Online registration for this event is now closed, please contact the event coordinator for assistance.";
        alert(message);
        return true;
    }
    else
        return false;
}

function BTN_SaveContact_OnClientSideClick() {

    if (Page_ClientValidate("CreateContact")) {
        location.hash = '#a_Badges';
    }
    else {
        location.hash = '#a_Attendee';

    }

}

function ShowMessage(message) {
    alert(message);
}

jQuery(document).ready(function () {
    if (window.location.href.indexOf("/CorporateTables.aspx") > -1) {
        jQuery('input[type="button"][value="Attendees"]').each(function () {
            if (jQuery(this).attr('id') <= 0) {
                jQuery(this).removeClass('btn-primary').addClass('btn-primary-turned-off grey').prop("disabled", true);
            }
        });
    }


    jQuery("#btnAddDisabled").attr("title", "Disabled till we fix an issue with this option..");
    jQuery("#BTN_SaveContact").attr("title", "Disabled till we fix an issue with this option..");

    // update values on email address fill
    //jQuery("input#TBEmailAddress").change(function () {
    //    jQuery("#BTN_AutoFillUserDetails").click();
    //});
});


function AutoFillValues(firstName, lastName) {
    jQuery("input#TB_FirstName").val(firstName);
    jQuery("input#TB_LastName").val(lastName);
}

function AutoFillValues() {
    jQuery(document).ready(function () {

        jQuery("a#CorpAHref").click(function (e) {
            e.preventDefault();
        });

        jQuery("input#TBEmailAddress").change(function () {

            $("#loading").show();

            setTimeout(function () {
                $("#loading").hide();
            }, 2000);

            var email = jQuery("input#TBEmailAddress").val();
            PageMethods.AutoFillFields(email, OnSuccess);            

            function OnSuccess(response, userContext, methodName) {

                if (response.length > 16) {
                    var userFields = response.split("::");
                    var coyid = "select#DDL_Institutes [value='" + userFields[4] + "']";
                    var prefix = "select#DDL_Prefix [value='" + userFields[0] + "']";
                    var state = "select#DDL_States [value='" + userFields[6] + "']";
                    console.log("prefix:" + userFields[0]);
                    console.log("coyid:" + userFields[4]);
                    console.log("coyname:" + userFields[8]);

                    jQuery(prefix).attr("selected", "true");
                    jQuery("input#TB_FirstName").val(userFields[1]);
                    jQuery("input#TB_LastName").val(userFields[2]);
                    jQuery("input#TB_Position").val(userFields[3]);
                    jQuery(coyid).attr("selected", "true");
                    jQuery("div#div_InstituteName").css("display", "block");

                    if (userFields[4].length > 0)
                        jQuery("input#TB_InstituteName").val(jQuery("#DDL_Institutes option:selected").text());
                    else
                        jQuery("input#TB_InstituteName").val(userFields[8]);

                    jQuery("input#TB_Phone").val(userFields[5]);
                    jQuery(state).attr("selected", "true");
                    jQuery("textarea#TB_EventPreferences").val(userFields[7]);

                    //disable radio buttons
                    //jQuery("input#RB_Member");
                    //jQuery("input#RB_NonMember");

                    //jQuery("select#DDL_Institutes").css("display", "block").attr("disabled", "disabled");
                    //jQuery("select#DDL_Institutes").css("display", "block");
                    jQuery("select#DDL_Prefix").css("display", "block");
                    jQuery("select#DDL_States").css("display", "block");

                    jQuery("div#DDL_Institutes_chosen").css("display", "none");
                    jQuery("div#DDL_Prefix_chosen").css("display", "none");
                    jQuery("div#DDL_States_chosen").css("display", "none");

                    //jQuery("div#loading").hide();
                }
            }

            //if (jQuery("div#loading").is(":visible"))
            //    jQuery("div#loading").hide();
        });
    });
}