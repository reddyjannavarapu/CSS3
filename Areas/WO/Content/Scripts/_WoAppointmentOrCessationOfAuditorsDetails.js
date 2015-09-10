$(document).ready(function () {

    if ($('#hdnWOCloseStatus').val() == 'hide') {
        $('.btnWOClose').hide();
    }

    $GlobalWOAppointmentOrCessationDetailsData = {};
    $GlobalWOAppointmentOrCessationDetailsData.InsertedID = 0;
    $GlobalWOAppointmentOrCessationDetailsData.WOTransferDetailsData = '';

    BindShareHoldingStructureAndAddressCountryInWOApOrCessDetails();
    $("#chkROPlaceOfMeeting").change(function () {
        try {
            if ($(this).is(":checked")) {

                CallWOAppointmentOrCessationDetails("GetCompanyAddressesByWOID", '', '', "{'WOID':" + parseInt($("#hdnWOID").val()) + ",'IsFMGAddress':" + false + "}", false, CompanyDetailscallBack);
            }
            else ClearROPlaceOfMeetingFields();
        }
        catch (ex) {
            console.log(ex);
        }
    });
    $('#btnSaveAppOrCessOfAuditorsDetails').click(function () {
        try {
            var count = 0;

            AppointmentOrCessationOfAuditors = {};
            AppointmentOrCessationOfAuditors.WOID = $('#hdnWOID').val();

            AppointmentOrCessationOfAuditors.ModeOfAppointment = $('#ddlModeOfAppointment option:selected').val();

            var Auditor = $('#ddlNameOfAuditor option:selected').val();
            var NameOfAuditor = $('#txtNameOfAuditor').val();

            AppointmentOrCessationOfAuditors.Auditor = Auditor
            AppointmentOrCessationOfAuditors.NameOfAuditor = NameOfAuditor;

            AppointmentOrCessationOfAuditors.OutgoingAuditor = $('#txtOutgoingAuditorName').val();
            AppointmentOrCessationOfAuditors.ShareHoldingStructure = $('#ddlShareHoldingStructure option:selected').val();

            var MeetingNoticeOptionRowId = $('#ddlDirectorChosen_MeetingNotice option:selected').val();
            AppointmentOrCessationOfAuditors.MeetingNotice = $("#ddlDirectorChosen_MeetingNotice option[value=" + MeetingNoticeOptionRowId + "]").attr("personid");
            AppointmentOrCessationOfAuditors.MeetingNoticeSource = $("#ddlDirectorChosen_MeetingNotice option[value=" + MeetingNoticeOptionRowId + "]").attr("sourcecode");

            var MeetingMinutesOptionRowId = $('#ddlDirectorChosen_MeetingMinutes option:selected').val();
            //  if (MeetingMinutesOptionRowId == '' || MeetingMinutesOptionRowId == undefined) {
            AppointmentOrCessationOfAuditors.OtherMeetingMinutes = $.trim($("#txtOtherMeetingMinutes").val());
            // }
            // else {
            AppointmentOrCessationOfAuditors.MeetingMinutes = $("#ddlDirectorChosen_MeetingMinutes option[value=" + MeetingMinutesOptionRowId + "]").attr("personid");
            AppointmentOrCessationOfAuditors.MeetingMinutesSource = $("#ddlDirectorChosen_MeetingMinutes option[value=" + MeetingMinutesOptionRowId + "]").attr("sourcecode");
            // }
            AppointmentOrCessationOfAuditors.Designation = $.trim($("#txtDesignation").val());

            AppointmentOrCessationOfAuditors.IsROPlaceOfMeeting = $("#chkROPlaceOfMeeting").is(":checked");
            AppointmentOrCessationOfAuditors.MAddressLine1 = $("#txtMeetingAddressLine1").val();
            AppointmentOrCessationOfAuditors.MAddressLine2 = $("#txtMeetingAddressLine2").val();
            AppointmentOrCessationOfAuditors.MAddressLine3 = $("#txtMeetingAddressLine3").val();
            AppointmentOrCessationOfAuditors.MAddressCountry = $("#ddlMeetingAddressCountry").val();
            AppointmentOrCessationOfAuditors.MAddressPostalCode = $("#txtMeetingAddressPostalCode").val();

            count += ControlEmptyNess(false, $("#ddlModeOfAppointment"), 'Please select Mode of appointment.');
            //count += ControlEmptyNess(true, $("#ddlNameOfAuditor"), 'Please select Name of Auditor.');
            count += ControlEmptyNess(false, $("#ddlShareHoldingStructure"), 'Please select Shareholding Structure.');
            count += ControlEmptyNess(false, $("#ddlDirectorChosen_MeetingNotice"), 'Please select Meeting Notice.');
            count += ControlEmptyNess(false, $("#txtDesignation"), 'Please enter Designation.');


            if (count > 0) {
                ShowNotify('Please enter all mandatory fields.', 'error', 3000);
                return false;
            }
            else {
                var jsonAppointmentOrCessationDetailsText = JSON.stringify({ AppointmentOrCessationOfAuditors: AppointmentOrCessationOfAuditors });
                CallWOAppointmentOrCessationDetails("/WODI/InsertWOAppointmentOrCessationDetails", '', '', jsonAppointmentOrCessationDetailsText, '', CreateWOAppointmentOrCessationDetailsCallBack);
            }

        } catch (e) {
            console.log(e);
        }
    });
    $('#btnCancelAppOrCessDetails').click(function () {
        try {
            $('#ddlModeOfAppointment').val('-1');
            $('#ddlNameOfAuditor').val('1');
            $('#txtOutgoingAuditorName').val('');
            $('#ddlShareHoldingStructure').val('-1');
            $('#ddlDirectorChosen_MeetingNotice').val('').trigger('chosen:updated');
            $('#ddlDirectorChosen_MeetingMinutes').val('').trigger('chosen:updated');
            $('#txtNameOfAuditor').val('');
            $('#txtOtherMeetingMinutes').val('');
            $('#txtDesignation').val('');
            $('#txtNameOfAuditor').attr("disabled", false);
            ClearROPlaceOfMeetingFields();

        } catch (e) {
            console.log(e);
        }
    });
    $('#ddlNameOfAuditor').change(function () {
        var nameofAuditor = $(this).val();
        DisableNameofAuditor(nameofAuditor);
    });

});

function DisableNameofAuditor(nameofAuditor) {
    if (nameofAuditor != '-1') {
        $('#txtNameOfAuditor').val('');
        $('#txtNameOfAuditor').attr("disabled", true);
    }
    else {
        $('#txtNameOfAuditor').attr("disabled", false);
    }
}

function BindShareHoldingStructureAndAddressCountryInWOApOrCessDetails() {
    try {
        CallMasterData("/PartialContent/GetCountryDetails", 'scriptCountry', 'ddlMeetingAddressCountry', "{}", false, CountryCallBack);
        CallMasterData("/WODI/GetAllShareHoldingStructures", 'ShareHoldingStructureTemplateinWOAppOrCessDetails', 'ddlShareHoldingStructure', "{}", false, BindShareHoldingCallBack);

    } catch (e) {
        console.log(e);
    }
}
function CountryCallBack() {
    try {
        $("#ddlMeetingAddressCountry").val("109");
    } catch (e) {
        console.log(e);
    }
}
function BindShareHoldingCallBack() {
    try {
        BindAuditorsDropdown();
    } catch (e) {
        console.log(e);
    }
}
function BindAuditorsDropdown() {
    try {
        var WOID = $('#hdnWOID').val();
        CallMasterData("/WODI/GetAuditorDetailsByWOID", 'AuditorsTemplateinWOAppOrCessDetails', 'ddlNameOfAuditor', "{'WOID':" + WOID + "}", '', BindAuditorsDropdownCallBack);
    } catch (e) {
        console.log(e);
    }

}
function BindAuditorsDropdownCallBack() {
    try {
        BindModeOfAppointment();
    } catch (e) {
        console.log(e);
    }
}
function BindModeOfAppointment() {
    try {
        CallMasterData("/WODI/GetModeOfAppointmentDetails", 'ModeOfAppointmentTemplateinWOAppOrCessDetails', 'ddlModeOfAppointment', "{}", '', BindModeOfAppointmentCallBack);
    } catch (e) {
        console.log(e);
    }
}
function BindModeOfAppointmentCallBack() {
    try {
        BindAppointmentOrCessationOfAuditorsByWOID();
    } catch (e) {
        console.log(e);
    }
}
function BindAppointmentOrCessationOfAuditorsByWOID() {
    try {
        var WOID = $('#hdnWOID').val();
        CallWOAppointmentOrCessationDetails("/WODI/GetWOAppointmentOrCessationDetailsByWOID", '', '', "{'WOID':" + WOID + "}", '', AppointmentOrCessationOfAuditorsCallBack);
    } catch (e) {
        console.log(e);
    }
}
function AppointmentOrCessationOfAuditorsCallBack() {
    try {
        var WoAppointOrCessDetail = $GlobalWOAppointmentOrCessationDetailsData.WOTransferDetailsData;
        if (WoAppointOrCessDetail.Auditor == '0') {
            $('#ddlNameOfAuditor').val('1');
        }
        if (WoAppointOrCessDetail.WOID != null && WoAppointOrCessDetail.WOID != undefined && WoAppointOrCessDetail.WOID != '0') {

            $('#ddlModeOfAppointment').val(WoAppointOrCessDetail.ModeofAppointment);

            if (WoAppointOrCessDetail.Auditor == '-1' && WoAppointOrCessDetail.NameOfAuditor == '') {
                $('#ddlNameOfAuditor').val('1');
            }
            else {
                $('#ddlNameOfAuditor').val(WoAppointOrCessDetail.Auditor);
            }

            $("#txtNameOfAuditor").val(WoAppointOrCessDetail.NameOfAuditor);
            if (WoAppointOrCessDetail.Auditor != '-1') {
                $('#txtNameOfAuditor').val('');
                $('#txtNameOfAuditor').attr("disabled", true);
            }
            else {
                $('#txtNameOfAuditor').attr("disabled", false);
            }
            $('#txtOutgoingAuditorName').val(WoAppointOrCessDetail.OutgoingAuditor);
            $('#ddlShareHoldingStructure').val(WoAppointOrCessDetail.ShareHoldingStructure);


            $("#chkROPlaceOfMeeting").prop("checked", WoAppointOrCessDetail.IsROPlaceOfMeeting);
            $("#txtMeetingAddressLine1").val(WoAppointOrCessDetail.MAddressLine1);
            $("#txtMeetingAddressLine2").val(WoAppointOrCessDetail.MAddressLine2);
            $("#txtMeetingAddressLine3").val(WoAppointOrCessDetail.MAddressLine3);

            if (WoAppointOrCessDetail.MAddressCountry == 0 || WoAppointOrCessDetail.MAddressCountry == -1)
                CountryCallBack();
            else
                $("#ddlMeetingAddressCountry").val(WoAppointOrCessDetail.MAddressCountry);
            if (WoAppointOrCessDetail.MAddressPostalCode == 0)
                $("#txtMeetingAddressPostalCode").val('');
            else
                $("#txtMeetingAddressPostalCode").val(WoAppointOrCessDetail.MAddressPostalCode);


            var MeetingNotice = $("#ddlDirectorChosen_MeetingNotice option[personid=" + WoAppointOrCessDetail.MeetingNotice + "]option[sourcecode=" + WoAppointOrCessDetail.MeetingNoticeSource + "]").attr("value");
            $('#ddlDirectorChosen_MeetingNotice').val(MeetingNotice).trigger('chosen:updated');

            $('#txtOtherMeetingMinutes').val(WoAppointOrCessDetail.OtherMeetingMinutes);

            var MeetingMinutes = $("#ddlDirectorChosen_MeetingMinutes option[personid=" + WoAppointOrCessDetail.MeetingMinutes + "]option[sourcecode=" + WoAppointOrCessDetail.MeetingMinutesSource + "]").attr("value");
            $('#ddlDirectorChosen_MeetingMinutes').val(MeetingMinutes).trigger('chosen:updated');

            $('#txtDesignation').val(WoAppointOrCessDetail.Designation);
        }

        else {
            $('#txtDesignation').val('Chairman');
        }

        var nameofAuditor = $('#ddlNameOfAuditor').val();
        DisableNameofAuditor(nameofAuditor);

    } catch (e) {
        console.log(e);
    }
}
function CompanyDetailscallBack() {
    try {
        if ($GlobalWOAppointmentOrCessationDetailsData.WOTransferDetailsData.MeetingAddressCountry != null && $GlobalWOAppointmentOrCessationDetailsData.WOTransferDetailsData.MeetingAddressCountry != undefined) {
            $("#txtMeetingAddressLine1").val($GlobalWOAppointmentOrCessationDetailsData.WOTransferDetailsData.MeetingAddressLine1);
            $("#txtMeetingAddressLine2").val($GlobalWOAppointmentOrCessationDetailsData.WOTransferDetailsData.MeetingAddressLine2);
            $("#txtMeetingAddressLine3").val($GlobalWOAppointmentOrCessationDetailsData.WOTransferDetailsData.MeetingAddressLine3);
            if ($GlobalWOAppointmentOrCessationDetailsData.WOTransferDetailsData.MeetingAddressCountry == 0 || $GlobalWOAppointmentOrCessationDetailsData.WOTransferDetailsData.MeetingAddressCountry == -1)
                CountryCallBack();
            else
                $("#ddlMeetingAddressCountry").val($GlobalWOAppointmentOrCessationDetailsData.WOTransferDetailsData.MeetingAddressCountry);
            $("#txtMeetingAddressPostalCode").val($GlobalWOAppointmentOrCessationDetailsData.WOTransferDetailsData.MeetingAddressPostalCode);
        }
    } catch (e) {
        console.log(e);
    }
}
function ClearROPlaceOfMeetingFields() {
    try {
        $("#txtMeetingAddressLine1").val('');
        $("#txtMeetingAddressLine2").val('');
        $("#txtMeetingAddressLine3").val('');
        $("#chkROPlaceOfMeeting").prop("checked", false);
        CountryCallBack();
        $("#txtMeetingAddressPostalCode").val('');
    } catch (e) {
        console.log(e);
    }
}
function CreateWOAppointmentOrCessationDetailsCallBack() {
    try {
        if ($GlobalWOAppointmentOrCessationDetailsData.InsertedID >= 1) {
            ShowNotify('Success.', 'success', 2000);
        }
    } catch (e) {
        console.log(e);
    }
}
function CallWOAppointmentOrCessationDetails(path, templateId, containerId, parameters, clearContent, callBack) {
    try {
        $.ajax({
            type: "POST",
            url: path,
            data: parameters,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            cache: true,
            success: function (msg) {
                try {

                    $GlobalWOAppointmentOrCessationDetailsData.WOTransferDetailsData = msg;
                    $GlobalWOAppointmentOrCessationDetailsData.InsertedID = msg;

                    if (callBack != undefined && callBack != '')
                        callBack();
                } catch (e) {
                    console.log(e);
                }

            },
            error: function (xhr, ajaxOptions, thrownError) {
                //throw new Error(xhr.statusText);
            }
        });
    } catch (e) {
        console.log(e);
    }
}