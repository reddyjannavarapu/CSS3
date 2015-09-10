$(document).ready(function () {
    $GlobalEGMData = {};
    $GlobalEGMData.MeetingNotice = '';
    $GlobalEGMData.MeetingNoticeSource = '';
    $GlobalEGMData.MeetingMinutes = '';
    $GlobalEGMData.MeetingMinutesSource = '';
    $GlobalEGMData.OthersMeetingMinutes = '';
    $GlobalEGMData.Designation = '';
    $GlobalEGMData.ShareHoldingStructure = '';
    $GlobalEGMData.ConsiderationCurrency = '';
    $GlobalEGMData.ConsiderationAmount = '';
    $GlobalEGMData.NameVendor = '';
    $GlobalEGMData.IsROPlaceOfMeeting = '';
    $GlobalEGMData.MeetingAddressLine1 = '';
    $GlobalEGMData.MeetingAddressLine2 = '';
    $GlobalEGMData.MeetingAddressLine3 = '';
    $GlobalEGMData.WOID = '';
    $GlobalEGMData.MeetingAddressCountry = '';
    $GlobalEGMData.MeetingAddressPostalCode = '';
    $GlobalEGMData.PropertyAddressLine1 = '';
    $GlobalEGMData.PropertyAddressLine2 = '';
    $GlobalEGMData.PropertyAddressLine3 = '';
    $GlobalEGMData.PropertyAddressCountry = '';
    $GlobalEGMData.PropertyAddressPostalCode = '';
    $GlobalEGMData.SavedStatus = '';
    $GlobalEGMData.EGMList = '';

    if ($('#hdnWOCloseStatus').val() == 'hide') {
        $('.btnWOClose').hide();
    }

    BindDropdowns();

    $("#chkROPlaceOfMeeting").change(function () {
        try {
            if ($(this).is(":checked")) {

                EGMCallServices("GetCompanyAddressesByWOID", '', '', "{'WOID':" + parseInt($("#hdnWOID").val()) + ",'IsFMGAddress':" + false + "}", false, CompanyDetailscallBack);
            }
            else ClearROPlaceOfMeetingFields();
        }
        catch (ex) {
            throw ex;
        }
    });
   
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
    $("#txtPropertyAddressPostalCode").keypress(function (event) {
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
    $("#btnSaveEGM").click(function () {
        try {
            $GlobalEGMData.TypeOfTransaction = $('#ddlTypeOfTransaction').find("option:selected").val();
            $GlobalEGMData.MeetingNoticeSource = $("#ddlDirectorChosen_MeetingNotice").find("option:selected").attr("sourcecode");
            $GlobalEGMData.MeetingNotice = $("#ddlDirectorChosen_MeetingNotice").find("option:Selected").attr("personid");
            $GlobalEGMData.MeetingMinutesSource = $("#ddlDirectorChosen_MeetingMinutes").find("option:Selected").attr("sourcecode");
            $GlobalEGMData.MeetingMinutes = $("#ddlDirectorChosen_MeetingMinutes").find("option:Selected").attr("personid");
            //   if ($GlobalEGMData.MeetingMinutes == undefined)
            $GlobalEGMData.OthersMeetingMinutes = $("#txtOtherMeetingMinutes").val();
            // else $GlobalEGMData.OthersMeetingMinutes = '';
            $GlobalEGMData.Designation = $("#txtDesignation").val();
            $GlobalEGMData.ShareHoldingStructure = $("#ddlShareHoldingStructure").find("option:selected").val();
            $GlobalEGMData.ConsiderationCurrency = $("#ddlConsiderationCurrency").find("option:selected").val();
            $GlobalEGMData.ConsiderationAmount = $("#txtConsiderationAmount").val();
            $GlobalEGMData.NameVendor = $("#txtNameVendor").val();
            $GlobalEGMData.IsROPlaceOfMeeting = $("#chkROPlaceOfMeeting").is(":checked");
            $GlobalEGMData.MeetingAddressLine1 = $("#txtMeetingAddressLine1").val();
            $GlobalEGMData.MeetingAddressLine2 = $("#txtMeetingAddressLine2").val();
            $GlobalEGMData.MeetingAddressLine3 = $("#txtMeetingAddressLine3").val();
            $GlobalEGMData.MeetingAddressCountry = $("#ddlMeetingAddressCountry").find("option:selected").val();
            $GlobalEGMData.MeetingAddressPostalCode = $("#txtMeetingAddressPostalCode").val();
            $GlobalEGMData.PropertyAddressLine1 = $("#txtPropertyAddressLine1").val();
            $GlobalEGMData.PropertyAddressLine2 = $("#txtPropertyAddressLine2").val();
            $GlobalEGMData.PropertyAddressLine3 = $("#txtPropertyAddressLine3").val();
            $GlobalEGMData.PropertyAddressCountry = $("#ddlPropertyAddressCountry").find("option:selected").val();
            $GlobalEGMData.PropertyAddressPostalCode = $("#txtPropertyAddressPostalCode").val();
            $GlobalEGMData.WOID = $("#hdnWOID").val();
            var JsonParam = JSON.stringify({ EGMData: $GlobalEGMData });

            EGMCallServices("SaveWOEGMDetails", "", "", JsonParam, false, EGMSavedStatus);
        }
        catch (ex) {
            console.log(ex);
        }
    });
    $("#btnClear").click(function () {
        try {
            $('#ddlDirectorChosen_MeetingNotice').val('').trigger('chosen:updated');
            $('#ddlDirectorChosen_MeetingMinutes').val('').trigger('chosen:updated');
            // $("#txtOtherMeetingMinutes").attr("disabled", false);
            $("#txtDesignation").val('');
            $("#ddlShareHoldingStructure").val('-1');
            $("#ddlConsiderationCurrency").val('14');
            $("#txtConsiderationAmount").val('');
            $("#txtNameVendor").val('');
            $("#chkROPlaceOfMeeting").prop("checked", false);
            $("#txtMeetingAddressLine1").val('');
            $("#txtMeetingAddressLine2").val('');
            $("#txtMeetingAddressLine3").val('');
            $("#txtMeetingAddressPostalCode").val('');
            $("#txtPropertyAddressLine1").val('');
            $("#txtPropertyAddressLine2").val('');
            $("#txtPropertyAddressLine3").val('');
            $("#txtPropertyAddressPostalCode").val('');
            $("#txtOtherMeetingMinutes").val('');
            PropertyAddressCountryCallBack();
            MeetingAddressCountryCallBack();
        } catch (e) {
            console.log(e);
        }
    });
});


function BindDropdowns() {
    try {
        //CallMasterData("/WODI/GetAllShareHoldingStructures", 'ShareHoldingStructure', 'ddlShareHoldingStructure', "{}", false, '');
        CallMasterData("/PartialContent/GetCountryDetails", 'scriptCountry', 'ddlMeetingAddressCountry', "{}", false, MeetingAddressCountryCallBack);
        CallMasterData("/PartialContent/GetCountryDetails", 'scriptCountry', 'ddlPropertyAddressCountry', "{}", false, PropertyAddressCountryCallBack);
        CallMasterData("/PartialContent/GetCurrencyDetails", 'scriptCurrency', 'ddlConsiderationCurrency', "{}", false, CurrencyCallBack);
    }
    catch (ex) {
        console.log(ex);
    }
}
function BindEGMDetailsByWOIDCallBack() {
    try {
        if ($GlobalEGMData.EGMList.WOID != null && $GlobalEGMData.EGMList.WOID != undefined && $GlobalEGMData.EGMList.WOID != '0') {

            if ($GlobalEGMData.EGMList.MeetingNotice != null && $GlobalEGMData.EGMList.MeetingNotice != undefined && $GlobalEGMData.EGMList.MeetingNotice != 0) {
                var MeetingNoticeVal = $("#ddlDirectorChosen_MeetingNotice option[personid=" + $GlobalEGMData.EGMList.MeetingNotice + "]option[sourcecode=" + $GlobalEGMData.EGMList.MeetingNoticeSource + "]").attr("value");
                $('#ddlDirectorChosen_MeetingNotice').val(MeetingNoticeVal).trigger('chosen:updated');
            }
            var MeetingMinsVal = $("#ddlDirectorChosen_MeetingMinutes option[personid=" + $GlobalEGMData.EGMList.MeetingMinutes + "]option[sourcecode=" + $GlobalEGMData.EGMList.MeetingMinutesSource + "]").attr("value");
            $('#ddlDirectorChosen_MeetingMinutes').val(MeetingMinsVal).trigger('chosen:updated');
            $("#txtOtherMeetingMinutes").val($GlobalEGMData.EGMList.OthersMeetingMinutes);
            $("#txtDesignation").val($GlobalEGMData.EGMList.Designation);

            if ($GlobalEGMData.EGMList.ShareHoldingStructure == 0 || $GlobalEGMData.EGMList.ShareHoldingStructure == -1)
                $("#ddlShareHoldingStructure").val('-1');
            else
                $("#ddlShareHoldingStructure").val($GlobalEGMData.EGMList.ShareHoldingStructure);

            if ($GlobalEGMData.EGMList.ConsiderationCurrency == 0 || $GlobalEGMData.EGMList.ConsiderationCurrency == -1)
                CurrencyCallBack();
            else
                $("#ddlConsiderationCurrency").val($GlobalEGMData.EGMList.ConsiderationCurrency);

            if ($GlobalEGMData.EGMList.ConsiderationAmount == 0 || $GlobalEGMData.EGMList.ConsiderationAmount == -1)
                $("#txtConsiderationAmount").val('');
            else
                $("#txtConsiderationAmount").val($GlobalEGMData.EGMList.ConsiderationAmount);

            $("#txtNameVendor").val($GlobalEGMData.EGMList.NameVendor);


            $("#chkROPlaceOfMeeting").prop("checked", $GlobalEGMData.EGMList.IsROPlaceOfMeeting);
            $("#txtMeetingAddressLine1").val($GlobalEGMData.EGMList.MeetingAddressLine1);
            $("#txtMeetingAddressLine2").val($GlobalEGMData.EGMList.MeetingAddressLine2);
            $("#txtMeetingAddressLine3").val($GlobalEGMData.EGMList.MeetingAddressLine3);

            if ($GlobalEGMData.EGMList.MeetingAddressCountry == 0 || $GlobalEGMData.EGMList.MeetingAddressCountry == -1)
                MeetingAddressCountryCallBack();
            else
                $("#ddlMeetingAddressCountry").val($GlobalEGMData.EGMList.MeetingAddressCountry);


            $("#txtMeetingAddressPostalCode").val($GlobalEGMData.EGMList.MeetingAddressPostalCode);


            $("#txtPropertyAddressLine1").val($GlobalEGMData.EGMList.PropertyAddressLine1);
            $("#txtPropertyAddressLine2").val($GlobalEGMData.EGMList.PropertyAddressLine2);
            $("#txtPropertyAddressLine3").val($GlobalEGMData.EGMList.PropertyAddressLine3);

            if ($GlobalEGMData.EGMList.PropertyAddressCountry == 0 || $GlobalEGMData.EGMList.PropertyAddressCountry == -1)
                PropertyAddressCountryCallBack();
            else
                $("#ddlPropertyAddressCountry").val($GlobalEGMData.EGMList.PropertyAddressCountry);

            $("#txtPropertyAddressPostalCode").val($GlobalEGMData.EGMList.PropertyAddressPostalCode);
        }
        else {
            $('#txtDesignation').val('Chairman');
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
        MeetingAddressCountryCallBack();
        $("#txtMeetingAddressPostalCode").val('');
    } catch (e) {
        console.log(e);
    }
}
function CompanyDetailscallBack() {
    try {
        if ($GlobalEGMData.EGMList.MeetingAddressCountry != null && $GlobalEGMData.EGMList.MeetingAddressCountry != undefined) {
            $("#txtMeetingAddressLine1").val($GlobalEGMData.EGMList.MeetingAddressLine1);
            $("#txtMeetingAddressLine2").val($GlobalEGMData.EGMList.MeetingAddressLine2);
            $("#txtMeetingAddressLine3").val($GlobalEGMData.EGMList.MeetingAddressLine3);
            if ($GlobalEGMData.EGMList.MeetingAddressCountry == 0 || $GlobalEGMData.EGMList.MeetingAddressCountry == -1)
                MeetingAddressCountryCallBack();
            else
                $("#ddlMeetingAddressCountry").val($GlobalEGMData.EGMList.MeetingAddressCountry);
            $("#txtMeetingAddressPostalCode").val($GlobalEGMData.EGMList.MeetingAddressPostalCode);
        }
    } catch (e) {
        console.log(e);
    }
}
function PropertyAddressCountryCallBack() {
    try {
        $("#ddlPropertyAddressCountry").val("109");
    } catch (e) {
        console.log(e);
    }
}
function MeetingAddressCountryCallBack() {
    try {
        $("#ddlMeetingAddressCountry").val("109");
    } catch (e) {
        console.log(e);
    }
}
function CurrencyCallBack() {
    try {
        $("#ddlConsiderationCurrency").val("14");
        BindEGMDetailsByWOID();
    } catch (e) {
        console.log(e);
    }
}
function BindEGMDetailsByWOID() {
    try {
        EGMCallServices("GetEGMDetailsByWOID", '', '', "{'WOID':" + parseInt($("#hdnWOID").val()) + "}", false, BindEGMDetailsByWOIDCallBack);
    }
    catch (ex) {
        console.log(ex);
    }
}

function EGMSavedStatus() {
    try {
        if ($GlobalEGMData.EGMList >= 1) {
            ShowNotify('Success.', 'success', 3000);
            return false;
        }
    } catch (e) {
        console.log(e);
    }
}

function EGMCallServices(path, templateId, containerId, parameters, clearContent, callBack) {
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
                    if (msg == 0) {
                        ShowNotify('Invalid session login again.', 'error', 3000);
                        return false;
                    }
                    $GlobalEGMData.EGMList = msg;
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
