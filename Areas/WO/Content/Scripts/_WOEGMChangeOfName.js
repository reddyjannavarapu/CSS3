$(document).ready(function () {
    $GlobalEGMChangeName = {};
    $GlobalEGMChangeName.EGMList = '';
    $GlobalEGMChangeName.WOID = '';
    $GlobalEGMChangeName.NewName = '';
    $GlobalEGMChangeName.CompanyName = '';
    $GlobalEGMChangeName.RegistrationNo = '';
    $GlobalEGMChangeName.IsROPlaceOfMeeting = '';
    $GlobalEGMChangeName.MAddressLine1 = '';
    $GlobalEGMChangeName.MAddressLine2 = '';
    $GlobalEGMChangeName.MAddressLine3 = '';
    $GlobalEGMChangeName.MAddressCountry = '';
    $GlobalEGMChangeName.MAddressPostalCode = '';
    $GlobalEGMChangeName.MeetingNoticeSource = '';
    $GlobalEGMChangeName.MeetingNotice = '';
    $GlobalEGMChangeName.MeetingMinutesSource = '';
    $GlobalEGMChangeName.MeetingMinutes = '';
    $GlobalEGMChangeName.OthersMeetingMinutes = '';
    $GlobalEGMChangeName.Designation = '';
    $GlobalEGMChangeName.NoticeResolutionSource = '';
    $GlobalEGMChangeName.NoticeResolution = '';
    $GlobalEGMChangeName.ShareHoldingStructure = '';

    if ($('#hdnWOCloseStatus').val() == 'hide') {
        $('.btnWOClose').hide();
    }

    BindDropdowns();
    GetEGMChangeNameDetailsByWOID();

    $("#txtMeetingAddressPostalCode").keypress(function (event) {
        try {
            var checkCharater = AllowNumbersCharactersOnly($(this).val(), event);
            if (!checkCharater) {
                event.preventDefault();
            }
        }
        catch (ex) {
            console.log(ex);
        }
    });

    $("#chkROPlaceOfMeeting").change(function () {
        try {
            if ($(this).is(":checked")) {

                EGMChangeNameCallServices("GetCompanyAddressesByWOID", '', '', "{'WOID':" + parseInt($("#hdnWOID").val()) + ",'IsFMGAddress':" + false + "}", false, CompanyDetailscallBack);
            }
            else ClearROPlaceOfMeetingFields();
        }
        catch (ex) {
            console.log(ex);
        }
    });

    $("#btnSaveEGM").click(function () {
        try {
            $GlobalEGMChangeName.NewName = $("#txtNewName").val();
            $GlobalEGMChangeName.ShareHoldingStructure = $("#ddlShareHoldingStructure").val();
            $GlobalEGMChangeName.IsROPlaceOfMeeting = $("#chkROPlaceOfMeeting").is(":checked");
            $GlobalEGMChangeName.MAddressLine1 = $("#txtMeetingAddressLine1").val();
            $GlobalEGMChangeName.MAddressLine2 = $("#txtMeetingAddressLine2").val();
            $GlobalEGMChangeName.MAddressLine3 = $("#txtMeetingAddressLine3").val();
            $GlobalEGMChangeName.MAddressCountry = $("#ddlMeetingAddressCountry").val();
            $GlobalEGMChangeName.MAddressPostalCode = $("#txtMeetingAddressPostalCode").val();
            $GlobalEGMChangeName.MeetingNotice = $("#ddlDirectorChosen_MeetingNotice").find("option:selected").attr("personid");
            $GlobalEGMChangeName.MeetingNoticeSource = $("#ddlDirectorChosen_MeetingNotice").find("option:selected").attr("sourcecode");
            $GlobalEGMChangeName.MeetingMinutes = $("#ddlDirectorChosen_MeetingMinutes").find("option:selected").attr("personid");
            $GlobalEGMChangeName.MeetingMinutesSource = $("#ddlDirectorChosen_MeetingMinutes").find("option:selected").attr("sourcecode");
            $GlobalEGMChangeName.NoticeResolution = $("#ddlDirectorChosen_NoticeofResolution").find("option:selected").attr("personid");
            $GlobalEGMChangeName.NoticeResolutionSource = $("#ddlDirectorChosen_NoticeofResolution").find("option:selected").attr("sourcecode");
            // if ($GlobalEGMChangeName.MeetingMinutes == undefined)
            $GlobalEGMChangeName.OthersMeetingMinutes = $("#txtOtherMeetingMinutes").val();
            // else $GlobalEGMChangeName.OthersMeetingMinutes = '';
            $GlobalEGMChangeName.Designation = $("#txtDesignation").val();
            $GlobalEGMChangeName.WOID = $("#hdnWOID").val();
            var JsonString = JSON.stringify({ EGMChangeOfName: $GlobalEGMChangeName });
            EGMChangeNameCallServices("SaveWOEGMChangeOfNameDetailsByWOID", '', '', JsonString, false, SavedStatusCallBack);
        }
        catch (ex) {
            console.log(ex);
        }
    });
    $("#btnClear").click(function () {
        try {
            $("#txtNewName").val('');
            $("#ddlShareHoldingStructure").val('-1');
            $("#chkROPlaceOfMeeting").prop("checked", false);
            $("#txtMeetingAddressLine1").val('');
            $("#txtMeetingAddressLine2").val('');
            $("#txtMeetingAddressLine3").val('');
            CountryCallBack();
            $("#txtMeetingAddressPostalCode").val('');
            $("#ddlDirectorChosen_MeetingNotice").val('').trigger('chosen:updated');
            $("#ddlDirectorChosen_MeetingMinutes").val('').trigger('chosen:updated');
            $("#ddlDirectorChosen_NoticeofResolution").val('').trigger('chosen:updated');
            $("#txtOtherMeetingMinutes").val('');
            $("#txtOtherMeetingMinutes").attr("disabled", false);
            $("#txtDesignation").val('');
        } catch (ex) {
            console.log(ex);
        }
    });


});

function BindDropdowns() {
    try {
        //CallMasterData("/WODI/GetAllShareHoldingStructures", 'ShareHoldingStructure', 'ddlShareHoldingStructure', "{}", false, '');
        CallMasterData("/PartialContent/GetCountryDetails", 'scriptCountry', 'ddlMeetingAddressCountry', "{}", false, CountryCallBack);
    }
    catch (ex) {
        console.log(ex);
    }
}

function CountryCallBack() {
    try {
        $("#ddlMeetingAddressCountry").val("109");
    } catch (e) {
        console.log(e);
    }
}

function GetEGMChangeNameDetailsByWOID() {
    try {
        EGMChangeNameCallServices("GetWOEGMChangeOfNameDetailsByWOID", '', '', "{'WOID':" + parseInt($("#hdnWOID").val()) + "}", false, WOEGMChangeOfNameDetailscallBack);
    }
    catch (ex) {
        console.log(ex);
    }
}

function WOEGMChangeOfNameDetailscallBack() {
    try {
        if ($GlobalEGMChangeName.EGMList.WOID != null && $GlobalEGMChangeName.EGMList.WOID != undefined && $GlobalEGMChangeName.EGMList.WOID != '0') {
            $("#txtNewName").val($GlobalEGMChangeName.EGMList.NewName);
            if ($GlobalEGMChangeName.EGMList.ShareHoldingStructure == 0 || $GlobalEGMChangeName.EGMList.ShareHoldingStructure == -1)
                $("#ddlShareHoldingStructure").val('-1');

            else
                $("#ddlShareHoldingStructure").val($GlobalEGMChangeName.EGMList.ShareHoldingStructure);

            $("#chkROPlaceOfMeeting").prop("checked", $GlobalEGMChangeName.EGMList.IsROPlaceOfMeeting);
            $("#txtMeetingAddressLine1").val($GlobalEGMChangeName.EGMList.MAddressLine1);
            $("#txtMeetingAddressLine2").val($GlobalEGMChangeName.EGMList.MAddressLine2);
            $("#txtMeetingAddressLine3").val($GlobalEGMChangeName.EGMList.MAddressLine3);

            if ($GlobalEGMChangeName.EGMList.MAddressCountry == 0 || $GlobalEGMChangeName.EGMList.MAddressCountry == -1)
                CountryCallBack();
            else
                $("#ddlMeetingAddressCountry").val($GlobalEGMChangeName.EGMList.MAddressCountry);
            if ($GlobalEGMChangeName.EGMList.MAddressPostalCode == 0)
                $("#txtMeetingAddressPostalCode").val('');
            else
                $("#txtMeetingAddressPostalCode").val($GlobalEGMChangeName.EGMList.MAddressPostalCode);


            if ($GlobalEGMChangeName.EGMList.MeetingNotice != 0) {
                var MeetingNoticeVal = $("#ddlDirectorChosen_MeetingNotice option[personid=" + $GlobalEGMChangeName.EGMList.MeetingNotice + "]option[sourcecode=" + $GlobalEGMChangeName.EGMList.MeetingNoticeSource + "]").attr("value");
                $('#ddlDirectorChosen_MeetingNotice').val(MeetingNoticeVal).trigger('chosen:updated');
            }
            else $('#ddlDirectorChosen_MeetingNotice').val('').trigger('chosen:updated');

            var MeetingMinsVal = $("#ddlDirectorChosen_MeetingMinutes option[personid=" + $GlobalEGMChangeName.EGMList.MeetingMinutes + "]option[sourcecode=" + $GlobalEGMChangeName.EGMList.MeetingMinutesSource + "]").attr("value");
            $('#ddlDirectorChosen_MeetingMinutes').val(MeetingMinsVal).trigger('chosen:updated');
            $("#txtOtherMeetingMinutes").val($GlobalEGMChangeName.EGMList.OthersMeetingMinutes);


            $("#txtDesignation").val($GlobalEGMChangeName.EGMList.Designation);

            if ($GlobalEGMChangeName.EGMList.NoticeResolution != 0) {
                var NoticeofResolution = $("#ddlDirectorChosen_NoticeofResolution option[personid=" + $GlobalEGMChangeName.EGMList.NoticeResolution + "]option[sourcecode=" + $GlobalEGMChangeName.EGMList.NoticeResolutionSource + "]").attr("value");
                $('#ddlDirectorChosen_NoticeofResolution').val(NoticeofResolution).trigger('chosen:updated');
            }
            else
                $('#ddlDirectorChosen_NoticeofResolution').val('').trigger('chosen:updated');
        }
        else {
            $("#txtDesignation").val('Chairman');
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

function CompanyDetailscallBack() {
    try {
        if ($GlobalEGMChangeName.EGMList.MeetingAddressCountry != null && $GlobalEGMChangeName.EGMList.MeetingAddressCountry != undefined) {
            $("#txtMeetingAddressLine1").val($GlobalEGMChangeName.EGMList.MeetingAddressLine1);
            $("#txtMeetingAddressLine2").val($GlobalEGMChangeName.EGMList.MeetingAddressLine2);
            $("#txtMeetingAddressLine3").val($GlobalEGMChangeName.EGMList.MeetingAddressLine3);
            if ($GlobalEGMChangeName.EGMList.MeetingAddressCountry == 0 || $GlobalEGMChangeName.EGMList.MeetingAddressCountry == -1)
                CountryCallBack();
            else
                $("#ddlMeetingAddressCountry").val($GlobalEGMChangeName.EGMList.MeetingAddressCountry);
            $("#txtMeetingAddressPostalCode").val($GlobalEGMChangeName.EGMList.MeetingAddressPostalCode);
        }
    } catch (e) {
        console.log(e);
    }
}

function SavedStatusCallBack() {
    try {
        if ($GlobalEGMChangeName.EGMList >= 1) {
            ShowNotify('Success.', 'success', 2000);
            return false;
        }
    } catch (e) {
        console.log(e);
    }
}

function EGMChangeNameCallServices(path, templateId, containerId, parameters, clearContent, callBack) {
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
                if (msg == '0') {
                    ShowNotify('Invalid session login again.', 'error', 3000);
                    return false;
                }
                $GlobalEGMChangeName.EGMList = msg;
                if (templateId != '' && containerId != '') {

                    if (!clearContent) {
                        $.tmpl($('#' + templateId).html(), msg).appendTo("#" + containerId);
                    }
                    else {
                        $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg));
                    }
                }

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