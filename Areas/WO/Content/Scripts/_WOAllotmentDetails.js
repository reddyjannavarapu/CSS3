$(document).ready(function () {
    $GlobalAllotmentData = {};
    $GlobalAllotmentData.AllotData = '';

    if ($('#hdnWOCloseStatus').val() == 'hide') {
        $('.btnWOClose').hide();
    }

    BindCurrencyforAlloment();
    BindShareClassforAlloment();

    //BindShareHoldingStructure();
    BindAllotmentDetails(); // on 12-July-2015, changed to static values  


    $("#btnSubmitAlloment").click(function () {
        try {
            SaveAllotMentDetails();
        } catch (e) {
            console.log(e);
        }
    });

    $("#chkROPlaceOfMeeting").change(function () {
        try {
            if ($(this).is(":checked")) {

                AllotmentDetailsCallService("GetCompanyAddressesByWOID", '', '', "{'WOID':" + parseInt($("#hdnWOID").val()) + ",'IsFMGAddress':" + false + "}", false, CompanyDetailscallBack);
            }
            else ClearROPlaceOfMeetingFields();
        }
        catch (ex) {
            console.log(ex);
        }
    });

    $('#btnCancelAlloment').unbind('click').click(function () {
        try {
            ClearWoAllomentValues();
        } catch (e) {
            console.log(e);
        }
    });



});

function SaveAllotMentDetails() {
    try {
        AllotmentDetails = {};
        AllotmentDetails.WOID = $("#hdnWOID").val();
        AllotmentDetails.ReturnOfAllotmentOfShares = $('#dllReturnOfAllotmentOfShares').find("option:selected").val();
        AllotmentDetails.Currency = $('#ddlCurrencyForAlloment').find("option:selected").val();
        AllotmentDetails.ClassOfShare = $('#ddlShareClassForAllotment').find("option:selected").val();
        AllotmentDetails.NumberOfNewSharesToBeAllotted = $("#txtTotalNoOfNewSharesToBeAllotted").val();
        AllotmentDetails.ConsiderationOfEachShare = $("#txtConsiderationOfEachShare").val();
        AllotmentDetails.AmountToBeTreatedAsPaidOnEachShare = $("#txtAmountToBeTreatedAsPaidOnEachShare").val();
        AllotmentDetails.TotalConsideration = $("#txtTotalConsideration").val();
        AllotmentDetails.ResultantTotalNoOfIssuedShares = $("#txtResultantTotalNoOfIssuedShares").val();
        AllotmentDetails.ResultantIssuedCapital = $("#txtResultantIssuedCapital").val();
        AllotmentDetails.ResultantPaidUpCapital = $("#txtResultantPaidUpCapital").val();
        AllotmentDetails.OtherMeetingMinutes = $("#txtOtherMeetingMinutes").val();
        var SelectedVals = new Array();
        SelectedVals = CheckDropDownIDAndSource("ddlDirectorChosen_ddlMeetingNotice");
        AllotmentDetails.MeetingNotice = SelectedVals[0];
        AllotmentDetails.MeetingNoticeSource = SelectedVals[1];

        AllotmentDetails.OtherMeetingMinutes = $.trim($("#txtOtherMeetingMinutes").val());

        var SelectedVals = new Array();
        SelectedVals = CheckDropDownIDAndSource("ddlDirectorChosen_ddlMeetingMinutes");
        AllotmentDetails.MeetingMinutes = SelectedVals[0];
        AllotmentDetails.MeetingMinutesSource = SelectedVals[1];


        AllotmentDetails.Designation = $("#txtDesignationOfThePersonSigningTheAllotment").val();

        var SelectedVals = new Array();
        SelectedVals = CheckDropDownIDAndSource("ddlDirectorChosen_ddlNoticeOfResolution");
        AllotmentDetails.NoticeOfResolution = SelectedVals[0];
        AllotmentDetails.NoticeOfResolutionSource = SelectedVals[1];

        var SelectedVals = new Array();
        SelectedVals = CheckDropDownIDAndSource("ddlDirectorChosen_ddlF24F25");
        AllotmentDetails.F24F25 = SelectedVals[0];
        AllotmentDetails.F24F25Source = SelectedVals[1];

        AllotmentDetails.ShareholdingStructure = $("#ddlShareholdingStructure").val();



        AllotmentDetails.IsROPlaceOfMeeting = $("#chkROPlaceOfMeeting").is(":checked");
        AllotmentDetails.MAddressLine1 = $("#txtMeetingAddressLine1").val();
        AllotmentDetails.MAddressLine2 = $("#txtMeetingAddressLine2").val();
        AllotmentDetails.MAddressLine3 = $("#txtMeetingAddressLine3").val();
        AllotmentDetails.MAddressCountry = $("#ddlMeetingAddressCountry").val();
        AllotmentDetails.MAddressPostalCode = $("#txtMeetingAddressPostalCode").val();
        var count = 0;

        count += ControlEmptyNess(false, $("#dllReturnOfAllotmentOfShares"), 'Please select Return of Allotment of Shares.');
        count += ControlEmptyNess(false, $("#ddlCurrencyForAlloment"), 'Please select Currency.');
        count += ControlEmptyNess(false, $("#ddlShareClassForAllotment"), 'Please select Class of Share .');

        if (count > 0) {
            ShowNotify('Please enter all mandatory fields.', 'error', 3000);
            return false;
        }
        else {
            var jsonText = JSON.stringify({ AllotmentDetails: AllotmentDetails });
            AllotmentDetailsCallService("/WODI/InsertWOAllotmentDetails", "", "", jsonText, false, CallbackAllotmentDetails);
        }

    } catch (e) {
        console.log(e);
    }
}


function BindCurrencyforAlloment() {
    try {
        CallMasterData("/PartialContent/GetCurrencyDetails", 'DropDownCurrencyTemplateForAllotment', 'ddlCurrencyForAlloment', "{}", false, CurrencyCallBack);
        CallMasterData("/PartialContent/GetCountryDetails", 'scriptCountry', 'ddlMeetingAddressCountry', "{}", false, CountryCallBack);

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

function BindShareHoldingStructure() {
    try {
        //CallMasterData("/WODI/GetAllShareHoldingStructures", 'ShareHoldingStructureTemplateinWOAllotmentDetails', 'ddlShareholdingStructure', "{}", false, BindAllotmentDetails);
    } catch (e) {
        console.log(e);
    }
}

function CurrencyCallBack() {
    try {
        $("#ddlCurrencyForAlloment").val("14");
    } catch (e) {
        console.log(e);
    }
}

function BindShareClassforAlloment() {
    try {
        var WOID = parseInt($("#hdnWOID").val());
        CallMasterData("/PartialContent/GetShareClassDetails", 'DropDownShareClassTemplateForAllotment', 'ddlShareClassForAllotment', "{'WOID':" + WOID + "}", false, "");
    } catch (e) {
        console.log(e);
    }
}

function AllotmentDetailsCallService(path, templateId, containerId, parameters, clearContent, callBack) {
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
                $GlobalAllotmentData.AllotData = msg;

                if (callBack != undefined && callBack != '')
                    callBack();

            },
            error: function (xhr, ajaxOptions, thrownError) {
                //throw new Error(xhr.statusText);
            }
        });
    } catch (e) {
        console.log(e);
    }
}

function CallbackAllotmentDetails() {
    try {
        if ($GlobalAllotmentData.AllotData > 1) {
            ShowNotify('success.', 'success', 3000);
            // BindAllotmentDetails();
        }
    } catch (e) {
        console.log(e);
    }
}


function BindAllotmentDetails() {
    try {
        var WOID = $("#hdnWOID").val();
        AllotmentDetailsCallService("/WODI/BindWOAllotmentDetails", '', '', "{'WOID':" + WOID + "}", false, BindAllotDetails);
    } catch (e) {
        console.log(e);
    }
}

function BindAllotDetails() {
    try {
        if ($GlobalAllotmentData.AllotData.WOID != 0) {
            if ($GlobalAllotmentData.AllotData.ReturnOfAllotmentOfShares == '0')
                $GlobalAllotmentData.AllotData.ReturnOfAllotmentOfShares = '';

            $("#dllReturnOfAllotmentOfShares").val($GlobalAllotmentData.AllotData.ReturnOfAllotmentOfShares);

            if ($GlobalAllotmentData.AllotData.Currency == '0')
                $GlobalAllotmentData.AllotData.Currency = '';

            $("#ddlCurrencyForAlloment").val($GlobalAllotmentData.AllotData.Currency);

            if ($GlobalAllotmentData.AllotData.ClassOfShare == '0')
                $GlobalAllotmentData.AllotData.ClassOfShare = '';

            $("#ddlShareClassForAllotment").val($GlobalAllotmentData.AllotData.ClassOfShare);

            if ($GlobalAllotmentData.AllotData.NumberOfNewSharesToBeAllotted == 0)
                $("#txtTotalNoOfNewSharesToBeAllotted").val('');
            else
                $("#txtTotalNoOfNewSharesToBeAllotted").val($GlobalAllotmentData.AllotData.NumberOfNewSharesToBeAllotted);


            if ($GlobalAllotmentData.AllotData.ConsiderationOfEachShare == 0)
                $("#txtConsiderationOfEachShare").val('');
            else
                $("#txtConsiderationOfEachShare").val($GlobalAllotmentData.AllotData.ConsiderationOfEachShare);

            if ($GlobalAllotmentData.AllotData.AmountToBeTreatedAsPaidOnEachShare == 0)
                $("#txtAmountToBeTreatedAsPaidOnEachShare").val('');
            else
                $("#txtAmountToBeTreatedAsPaidOnEachShare").val($GlobalAllotmentData.AllotData.AmountToBeTreatedAsPaidOnEachShare);

            if ($GlobalAllotmentData.AllotData.TotalConsideration == 0)
                $("#txtTotalConsideration").val('');
            else
                $("#txtTotalConsideration").val($GlobalAllotmentData.AllotData.TotalConsideration);

            if ($GlobalAllotmentData.AllotData.ResultantTotalNoOfIssuedShares == 0)
                $("#txtResultantTotalNoOfIssuedShares").val('');
            else
                $("#txtResultantTotalNoOfIssuedShares").val($GlobalAllotmentData.AllotData.ResultantTotalNoOfIssuedShares);


            if ($GlobalAllotmentData.AllotData.ResultantIssuedCapital == 0)
                $("#txtResultantIssuedCapital").val('');
            else
                $("#txtResultantIssuedCapital").val($GlobalAllotmentData.AllotData.ResultantIssuedCapital);

            if ($GlobalAllotmentData.AllotData.ResultantPaidUpCapital == 0)
                $("#txtResultantPaidUpCapital").val('');
            else
                $("#txtResultantPaidUpCapital").val($GlobalAllotmentData.AllotData.ResultantPaidUpCapital);

            if ($GlobalAllotmentData.AllotData.ShareholdingStructure == '0')
                $GlobalAllotmentData.AllotData.ShareholdingStructure = '';

            $("#ddlShareholdingStructure").val($GlobalAllotmentData.AllotData.ShareholdingStructure);

            if ($GlobalAllotmentData.AllotData.Designation != '' && $GlobalAllotmentData.AllotData.Designation != null)
                $("#txtDesignationOfThePersonSigningTheAllotment").val($GlobalAllotmentData.AllotData.Designation);

            if ($GlobalAllotmentData.AllotData.MeetingNotice == 0) {
                $('#ddlDirectorChosen_ddlMeetingNotice').val('').trigger('chosen:updated');
            }
            else {
                var MeetingNoticevalueFrompersonidAndsourcecode = $("#ddlDirectorChosen_ddlMeetingNotice option[personid=" + $GlobalAllotmentData.AllotData.MeetingNotice + "]option[sourcecode=" + $GlobalAllotmentData.AllotData.MeetingNoticeSource + "]").attr("value");
                $('#ddlDirectorChosen_ddlMeetingNotice').val(MeetingNoticevalueFrompersonidAndsourcecode).trigger('chosen:updated');
            }

            $("#txtOtherMeetingMinutes").val($GlobalAllotmentData.AllotData.OtherMeetingMinutes);
            if ($GlobalAllotmentData.AllotData.MeetingMinutes == 0)
                $('#ddlDirectorChosen_ddlMeetingMinutes').val('').trigger('chosen:updated');
            else {
                var MeetingMinutesvalueFrompersonidAndsourcecode = $("#ddlDirectorChosen_ddlMeetingMinutes option[personid=" + $GlobalAllotmentData.AllotData.MeetingMinutes + "]option[sourcecode=" + $GlobalAllotmentData.AllotData.MeetingMinutesSource + "]").attr("value");
                $('#ddlDirectorChosen_ddlMeetingMinutes').val(MeetingMinutesvalueFrompersonidAndsourcecode).trigger('chosen:updated');

            }

            if ($GlobalAllotmentData.AllotData.NoticeOfResolution == 0) {
                $('#ddlDirectorChosen_ddlNoticeOfResolution').val('').trigger('chosen:updated');
            }
            else {
                var NoticeOfResolutionvalueFrompersonidAndsourcecode = $("#ddlDirectorChosen_ddlNoticeOfResolution option[personid=" + $GlobalAllotmentData.AllotData.NoticeOfResolution + "]option[sourcecode=" + $GlobalAllotmentData.AllotData.NoticeOfResolutionSource + "]").attr("value");
                $('#ddlDirectorChosen_ddlNoticeOfResolution').val(NoticeOfResolutionvalueFrompersonidAndsourcecode).trigger('chosen:updated');
            }

            if ($GlobalAllotmentData.AllotData.F24F25 == 0) {
                $('#ddlDirectorChosen_ddlF24F25').val('').trigger('chosen:updated');
            }
            else {
                var F24F25valueFrompersonidAndsourcecode = $("#ddlDirectorChosen_ddlF24F25 option[personid=" + $GlobalAllotmentData.AllotData.F24F25 + "]option[sourcecode=" + $GlobalAllotmentData.AllotData.F24F25Source + "]").attr("value");
                $('#ddlDirectorChosen_ddlF24F25').val(F24F25valueFrompersonidAndsourcecode).trigger('chosen:updated');
            }

            $("#chkROPlaceOfMeeting").prop("checked", $GlobalAllotmentData.AllotData.IsROPlaceOfMeeting);
            $("#txtMeetingAddressLine1").val($GlobalAllotmentData.AllotData.MAddressLine1);
            $("#txtMeetingAddressLine2").val($GlobalAllotmentData.AllotData.MAddressLine2);
            $("#txtMeetingAddressLine3").val($GlobalAllotmentData.AllotData.MAddressLine3);

            if ($GlobalAllotmentData.AllotData.MAddressCountry == 0 || $GlobalAllotmentData.AllotData.MAddressCountry == -1)
                CountryCallBack();
            else
                $("#ddlMeetingAddressCountry").val($GlobalAllotmentData.AllotData.MAddressCountry);
            if ($GlobalAllotmentData.AllotData.MAddressPostalCode == 0)
                $("#txtMeetingAddressPostalCode").val('');
            else
                $("#txtMeetingAddressPostalCode").val($GlobalAllotmentData.AllotData.MAddressPostalCode);


        }

    } catch (e) {
        console.log(e);
    }
}

//function keypressvalidation() {
//    try {
//        var checkCharater = AllowNumbersOnly($(this).val(), event);
//        if (!checkCharater) {
//            event.preventDefault();
//        }
//    } catch (e) {
//        console.log(e);
//    }
//}
function ClearWoAllomentValues() {
    try {
        $('#dllReturnOfAllotmentOfShares').val('-1');
        $('#ddlCurrencyForAlloment').val('14');
        $('#ddlShareClassForAllotment').val('');
        $('#txtTotalNoOfNewSharesToBeAllotted').val('');
        $('#txtConsiderationOfEachShare').val('');
        $('#txtAmountToBeTreatedAsPaidOnEachShare').val('');
        $('#txtTotalConsideration').val('');
        $('#txtResultantTotalNoOfIssuedShares').val('');
        $('#txtResultantIssuedCapital').val('');
        $('#ddlDirectorChosen_ddlMeetingNotice').val("").trigger('chosen:updated');
        $('#ddlDirectorChosen_ddlMeetingMinutes').val("").trigger('chosen:updated');
        $('#txtOtherMeetingMinutes').val('');
        $('#txtDesignationOfThePersonSigningTheAllotment').val('Chairman');
        $('#ddlDirectorChosen_ddlNoticeOfResolution').val("").trigger('chosen:updated');
        $('#ddlDirectorChosen_ddlF24F25').val("").trigger('chosen:updated');
        $('#ddlShareholdingStructure').val('-1');
        ClearROPlaceOfMeetingFields();
        $("#chkROPlaceOfMeeting").prop("checked", false);
        $('#txtResultantPaidUpCapital').val('');

    } catch (e) {
        console.log(e);
    }
}

function CompanyDetailscallBack() {
    try {
        if ($GlobalAllotmentData.AllotData.MeetingAddressCountry != null && $GlobalAllotmentData.AllotData.MeetingAddressCountry != undefined) {
            $("#txtMeetingAddressLine1").val($GlobalAllotmentData.AllotData.MeetingAddressLine1);
            $("#txtMeetingAddressLine2").val($GlobalAllotmentData.AllotData.MeetingAddressLine2);
            $("#txtMeetingAddressLine3").val($GlobalAllotmentData.AllotData.MeetingAddressLine3);
            if ($GlobalAllotmentData.AllotData.MeetingAddressCountry == 0 || $GlobalAllotmentData.AllotData.MeetingAddressCountry == -1)
                CountryCallBack();
            else
                $("#ddlMeetingAddressCountry").val($GlobalAllotmentData.AllotData.MeetingAddressCountry);
            $("#txtMeetingAddressPostalCode").val($GlobalAllotmentData.AllotData.MeetingAddressPostalCode);
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
        CountryCallBack();
        $("#txtMeetingAddressPostalCode").val('');
    } catch (e) {
        console.log(e);
    }
}