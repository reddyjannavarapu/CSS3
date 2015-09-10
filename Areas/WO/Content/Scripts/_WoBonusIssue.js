$(document).ready(function () {
    $GlobalBonusIssue = {};
    $GlobalBonusIssue.BonusList = '';
    $GlobalBonusIssue.WOID = '';
    $GlobalBonusIssue.BonusIssuePerShare = '';
    $GlobalBonusIssue.RegisterOfMembersOn = '';
    $GlobalBonusIssue.AmountPaidOnEachShare = '';
    $GlobalBonusIssue.TotalNoOfIssuedShares = '';
    $GlobalBonusIssue.ResultantIssuedCapital = '';
    $GlobalBonusIssue.ClassOfShare = '';
    $GlobalBonusIssue.ResultantPaidUpCapital = '';
    $GlobalBonusIssue.IsRegisteredAddressAsPlaceOfMeeting = '';
    $GlobalBonusIssue.AddressLine1 = '';
    $GlobalBonusIssue.AddressLine2 = '';
    $GlobalBonusIssue.AddressLine3 = '';
    $GlobalBonusIssue.AddressCountry = '';
    $GlobalBonusIssue.AddressPostalCode = '';
    $GlobalBonusIssue.MeetingNotice = '';
    $GlobalBonusIssue.MeetingNoticeSource = '';
    $GlobalBonusIssue.MeetingMinutes = '';
    $GlobalBonusIssue.MeetingMinutesSource = '';
    $GlobalBonusIssue.OthersMeetingMinutes = '';
    $GlobalBonusIssue.Designation = '';
    $GlobalBonusIssue.NoticeOfResolution = '';
    $GlobalBonusIssue.NoticeOfResolutionSource = '';
    $GlobalBonusIssue.LetterOfAllotment = '';
    $GlobalBonusIssue.LetterOfAllotmentSource = '';
    $GlobalBonusIssue.ReturnOfAllotment = '';
    $GlobalBonusIssue.ReturnOfAllotmentSource = '';
    $GlobalBonusIssue.ShareHoldingStructure = '';
    $GlobalBonusIssue.ConsiderationOfEachShare = '';
    $GlobalBonusIssue.TotalNoOfNewSharesToBeAllotted = '';
    $GlobalBonusIssue.TotalConsideration = '';
    $GlobalBonusIssue.ConsiderationOfEachShareInList = '';
    $GlobalBonusIssue.Currency = '';
    $ShareHolderFlag = '';

    if ($('#hdnWOCloseStatus').val() == 'hide') {
        $('.btnWOClose').hide();
    }

    BindBonusDropdowns();
    

    $("#showSaveShareHoldersDetails").click(function () {
        BindShareholdersByWOID();
        $('#divShareholdersList').show();
        $("#showSaveShareHoldersDetails").hide();
    });

    $("#txtDateRegisterOfMembersOn").datepicker({
        changeYear: true,
        changeMonth: true,
        showOtherMonths: true,
        selectOtherMonths: true,
        showWeek: true,
        dateFormat: 'dd/mm/yy',
        yearRange: "-10:+50"
    });

    $("#chkIsRegisteredAddressasPlaceOfMeeting").change(function () {
        try {
            if ($(this).is(":checked")) {
                WoBonusIssueCallServices("GetCompanyAddressesByWOID", '', '', "{'WOID':" + parseInt($("#hdnWOID").val()) + ",'IsFMGAddress':" + false + "}", false, CompanyDetailscallBack);
            }
            else ClearRegisteredAddressPlaceOfMeetingFields();
        }
        catch (ex) {
            console.log(ex);
        }
    });

    $("#btnSaveBonusIssue").click(function () {

        try {
            $GlobalBonusIssue.WOID = $("#hdnWOID").val();
            $GlobalBonusIssue.BonusIssuePerShare = $("#txtNoOfBonusIssuePerShare").val();
            $GlobalBonusIssue.RegisterOfMembersOn = $("#txtDateRegisterOfMembersOn").val();
            $GlobalBonusIssue.AmountPaidOnEachShare = $("#txtAmtToBeTreatedAsPaidOnEachShare").val();
            $GlobalBonusIssue.TotalNoOfIssuedShares = $("#txtResultantTotalIssuedShares").val();
            $GlobalBonusIssue.ResultantIssuedCapital = $("#txtResultantIssuedCapital").val();
            //$GlobalBonusIssue.ClassOfShare = $("#ddlBonusIssueClassOfShare").val();
            $GlobalBonusIssue.ResultantPaidUpCapital = $("#txtResultantPaidUpCapital").val();
            $GlobalBonusIssue.IsRegisteredAddressAsPlaceOfMeeting = $("#chkIsRegisteredAddressasPlaceOfMeeting").is(":checked");
            $GlobalBonusIssue.MeetingAddressLine1 = $("#txtAddressLine1").val();
            $GlobalBonusIssue.MeetingAddressLine2 = $("#txtAddressLine2").val();
            $GlobalBonusIssue.MeetingAddressLine3 = $("#txtAddressLine3").val();
            $GlobalBonusIssue.MeetingAddressCountry = $("#ddlAddressCountry").val();
            $GlobalBonusIssue.MeetingAddressPostalCode = $("#txtAddressPostalCode").val();

            var SelectedVals = new Array();
            SelectedVals = CheckDropDownIDAndSource("ddlDirectorChosen_MeetingNotice");
            $GlobalBonusIssue.MeetingNotice = SelectedVals[0];
            $GlobalBonusIssue.MeetingNoticeSource = SelectedVals[1];

            var SelectedVals = new Array();
            SelectedVals = CheckDropDownIDAndSource("ddlDirectorChosen_MeetingMinutes");
            $GlobalBonusIssue.MeetingMinutes = SelectedVals[0];
            $GlobalBonusIssue.MeetingMinutesSource = SelectedVals[1];

            $GlobalBonusIssue.OthersMeetingMinutes = $("#txtOtherMeetingMinutes").val();
            $GlobalBonusIssue.Designation = $("#txtDesignation").val();

            var SelectedVals = new Array();
            SelectedVals = CheckDropDownIDAndSource("ddlDirectorChosen_NoticeOfResolution");
            $GlobalBonusIssue.NoticeOfResolution = SelectedVals[0];
            $GlobalBonusIssue.NoticeOfResolutionSource = SelectedVals[1];


            var SelectedVals = new Array();
            SelectedVals = CheckDropDownIDAndSource("ddlDirectorChosen_LetterofAllotment");
            $GlobalBonusIssue.LetterOfAllotment = SelectedVals[0];
            $GlobalBonusIssue.LetterOfAllotmentSource = SelectedVals[1];

            var SelectedVals = new Array();
            SelectedVals = CheckDropDownIDAndSource("ddlDirectorChosen_ReturnofAllotment");
            $GlobalBonusIssue.ReturnOfAllotment = SelectedVals[0];
            $GlobalBonusIssue.ReturnOfAllotmentSource = SelectedVals[1];

            $GlobalBonusIssue.ShareHoldingStructure = $("#ddlShareHoldingStructure").val();

            var JsonData = JSON.stringify({ WOBonusData: $GlobalBonusIssue });

            WoBonusIssueCallServices("SaveWOBonusIssueDetailsByWOID", '', '', JsonData, true, BonusSavedStatus);

        }
        catch (ex) {
            console.log(ex);
        }
    });

    $("#btnSaveShareHoldersDetails").click(function () {
        try {
            saveShareholdersDetails();
        }
        catch (ex) {
            console.log(ex);
        }
    });

    $("#btnClearBonusIssue").click(function () {
        try {
            $("#txtNoOfBonusIssuePerShare").val('');
            $("#txtDateRegisterOfMembersOn").val();
            $("#txtAmtToBeTreatedAsPaidOnEachShare").val('')
            $("#txtResultantTotalIssuedShares").val('');
            $("#txtResultantIssuedCapital").val('');
            //$("#ddlBonusIssueClassOfShare").val('-1');
            $("#txtResultantPaidUpCapital").val('');
            $("#chkIsRegisteredAddressasPlaceOfMeeting").prop("checked", false);
            $("#txtAddressLine1").val('');
            $("#txtAddressLine2").val('');
            $("#txtAddressLine3").val('');
            CountryCallBack();
            $("#txtAddressPostalCode").val('');
            $('#ddlDirectorChosen_MeetingNotice').val('').trigger('chosen:updated');
            $('#ddlDirectorChosen_MeetingMinutes').val('').trigger('chosen:updated');
            $("#txtOtherMeetingMinutes").val('');
            // $("#txtOtherMeetingMinutes").attr("disabled", false);
            $('#ddlDirectorChosen_NoticeOfResolution').val('').trigger('chosen:updated');
            $('#ddlDirectorChosen_LetterofAllotment').val('').trigger('chosen:updated');
            $('#ddlDirectorChosen_ReturnofAllotment').val('').trigger('chosen:updated');
            $("#ddlShareHoldingStructure").val('');
            //   $("#txtConsiderationOfEachShare").val('');
            // CurrencyCallBack();
            //  $("#txtTotalNoOfNewSharesToBeAllotted").val('');
            //  $("#txtTotalConsideration").val('');

        } catch (e) {
            console.log(e);
        }
    });

    $('#txtAmtToBeTreatedAsPaidOnEachShare').change(function () {
        var Consideration = $('#txtConsiderationOfEachShare');
        //if ($.trim(Consideration.val()) == '') {
        Consideration.val($(this).val());
        //}
    });

    $("#txtConsiderationOfEachShare").keyup(function (e) {
        try {

            var Dividend = '';
            var DividendPerShare = $(this).val();

            $("#tblShareHoldersDevident").find("#trBonusShareholdersData").find("tr").each(function () {
                var SharesHeld = $(this).find(".txtShareHeld").val();
                var Result = (DividendPerShare == "" ? "0" : DividendPerShare) * SharesHeld;

                if (Result != "")
                    $(this).find(".txtNetCashDividend").val(Result.toFixed(2));
                else
                    $(this).find(".txtNetCashDividend").val("");
            });

            var TotalNetAmount = 0;
            var TotalNoOfShares = 0;

            $("#tblShareHoldersDevident").find("#trBonusShareholdersData").find("tr").each(function () {
                var NetCashDividend = $(this).find(".txtNetCashDividend").val();
                var ShareHeld = $(this).find(".txtShareHeld").val();
                var NoofBonusSharesToIssue = $(this).find(".NoOfBonusShareToIssue").val();

                if (NetCashDividend == "") {
                    NetCashDividend = 0;
                }
                if (ShareHeld == "") {
                    ShareHeld = 0;
                }

                NoofBonusSharesToIssue = (NoofBonusSharesToIssue == "") ? 0 : NoofBonusSharesToIssue;

                TotalNetAmount = parseFloat(TotalNetAmount) + parseFloat(NetCashDividend);
                //TotalNoOfShares = parseInt(TotalNoOfShares) + parseInt(ShareHeld);    //Old logic
                TotalNoOfShares = parseInt(TotalNoOfShares) + parseInt(NoofBonusSharesToIssue);

            });

            if (TotalNetAmount == 0)
                $("#txtTotalConsideration").val('');
            else
                $("#txtTotalConsideration").val(TotalNetAmount.toFixed(2));
            if (TotalNoOfShares == 0)
                $("#txtTotalNoOfNewSharesToBeAllotted").val('');
            else
                $("#txtTotalNoOfNewSharesToBeAllotted").val(TotalNoOfShares);

        }
        catch (ex) {
            console.log(ex);
        }
    });
    $("#txtAddressPostalCode").keypress(function (event) {
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
    $('#chkSelectAllBOISSH').click(function () {
        try {
            if ($(this).is(':checked')) {
                $('#trBonusShareholdersData').find('.chkShareholders').prop('checked', true);
            }
            else {
                $('#trBonusShareholdersData').find('.chkShareholders').prop('checked', false);
            }
        } catch (e) {
            console.log(e);
        }
    });


    $('#ddlBonusIssueClassOfShare').change(function () {
        BindShareholdersByWOID();
    });

});
function saveShareholdersDetails() {
    try {

        if ($ShareHolderFlag == 0) {

            ShowNotify('Please save Work Order Bonus Issue details first.', 'error', 3000);
            return false;

        } else {
            var classofshare = $('#ddlBonusIssueClassOfShare').val();
            if (classofshare == '-1' || classofshare == 'undefined') {
                ShowNotify('Please select Class of Share.', 'error', 3000);
                return false;
            }
            else {
                var ConsiderationOfEachShare = $('#txtConsiderationOfEachShare').val();
                $GlobalBonusIssue.ConsiderationOfEachShareInList = $('#txtConsiderationOfEachShare').val();
                var TotalNoOfNewSharesToBeAllotted = $('#txtTotalNoOfNewSharesToBeAllotted').val();
                var Currency = $('#ddlDividentPerShareCurrency option:selected').val();
                var TotalNetAmount = $('#txtTotalConsideration').val();
                var WOID = $('#hdnWOID').val();

                var Checked = ''; var ISSharesHeldNull = 'False'; var ISNetCashDividend = 'False';
                var NoOfBonusShareToIssue = 'False';
                var ShareDividendFields = new Array();
                $("#chkShareholders:checked").each(function (index) {
                    if ($(this).is(':checked')) {
                        Checked = 'true';
                        ShareDividend = {};
                        ShareDividend.PersonId = $(this).attr('personid');
                        ShareDividend.PersonSource = $(this).attr('PersonSource');
                        ShareDividend.WOID = $(this).attr('WOID');
                        ShareDividend.SharesHeld = $(this).closest("tr").find($("[id*=txtShareHeld]")).val();
                        ShareDividend.NetCashDividend = $(this).closest("tr").find($("[id*=txtNetCashDividend]")).val();
                        ShareDividend.NoOfBonusShareToIssue = $(this).closest("tr").find($("[id*=NoOfBonusShareToIssue]")).val();
                        ShareDividend.ClassofShare = classofshare;
                        ISSharesHeldNull == 'False' ? (ShareDividend.SharesHeld == '' ? ISSharesHeldNull = 'True' : 'False') : '';
                        //  ISNetCashDividend == 'False' ? (ShareDividend.NetCashDividend == '' ? ISNetCashDividend = 'True' : 'False') : '';
                        NoOfBonusShareToIssue == 'False' ? (ShareDividend.NoOfBonusShareToIssue == '' ? NoOfBonusShareToIssue = 'True' : 'False') : '';

                        var a = ShareDividend.NetCashDividend == '' ? 0 : ShareDividend.NetCashDividend
                        ISNetCashDividend == 'False' ? (parseInt(a) > 0 ? ISNetCashDividend = 'False' : ISNetCashDividend = 'True') : '';

                        ShareDividendFields[index] = ShareDividend;
                    }
                });


                if (ISSharesHeldNull == 'True' || ISSharesHeldNull == '') {
                    ShowNotify('Please enter shares held value.', 'error', 3000);
                    return false;
                }

                if (ISSharesHeldNull == 'True' || ISSharesHeldNull == '') {
                    ShowNotify('Please enter shares held value.', 'error', 3000);
                    return false;
                }
                if (ISNetCashDividend == 'True' || ISNetCashDividend == '') {
                    ShowNotify('Please enter net cash dividend value.', 'error', 3000);
                    return false;
                }
                if (NoOfBonusShareToIssue == 'True' || NoOfBonusShareToIssue == '') {
                    ShowNotify('Please enter No Of Bonus Share To Issue value.', 'error', 3000);
                    return false;
                }
                var JsonBonusWithShareHolders = JSON.stringify({ ShareDividendFields: ShareDividendFields, ConsiderationOfEachShare: ConsiderationOfEachShare, TotalNoOfNewSharesToBeAllotted: TotalNoOfNewSharesToBeAllotted, Currency: Currency, TotalNetAmount: TotalNetAmount, WOID: WOID, ClassofShare: classofshare });
                WoBonusIssueCallServices("/WODI/InsertFromWoBonusIssueAndShareholdersDetailsWOID", '', '', JsonBonusWithShareHolders, '', CallbackAfterInsertShareHolders);
            }
        }

    } catch (e) {
        console.log(e);
    }
}
function CallbackAfterInsertShareHolders() {
    try {
        if ($GlobalBonusIssue.BonusList == 2) {
            ShowNotify('Please save Work Order Bonus Issue details first.', 'error', 3000);
            return false;
        }
        else {
            ShowNotify('Success.', 'success', 3000);
            BindShareholdersByWOID();
            return false;
        }
    } catch (e) {
        console.log(e);
    }
}
function BindBonusDropdowns() {
    try {
        CallMasterData("/PartialContent/GetCurrencyDetails", 'scriptCurrency', 'ddlDividentPerShareCurrency', "{}", false, CurrencyCallBack);
        //CallMasterData("/WODI/GetAllShareHoldingStructures", 'ShareHoldingStructure', 'ddlShareHoldingStructure', "{}", false, ''); // not using 
        CallMasterData("/PartialContent/GetCountryDetails", 'scriptCountry', 'ddlAddressCountry', "{}", false, CountryCallBack);
        var WOID = parseInt($("#hdnWOID").val());
        CallMasterData("/PartialContent/GetShareClassDetails", 'scriptShareClass', 'ddlBonusIssueClassOfShare', "{'WOID':" + WOID + "}", false, GetBonusIssueDataByWOID);
    }
    catch (ex) {
        console.log(ex);
    }
}
function GetBonusIssueDataByWOID() {
    try {
        $GlobalBonusIssue.WOID = $("#hdnWOID").val();
        WoBonusIssueCallServices("GetWOBonusIssueDetailsByWOID", '', '', "{'WOID':" + parseInt($GlobalBonusIssue.WOID) + "}", '', GetBonusIssueDataByWOIDCallBack);
    } catch (e) {
        console.log(e);
    }
}
function GetBonusIssueDataByWOIDCallBack() {
    try {
        $ShareHolderFlag = $GlobalBonusIssue.BonusList.ID;

        if ($GlobalBonusIssue.BonusList.BonusIssuePerShare == 0)
            $("#txtNoOfBonusIssuePerShare").val('');
        else
            $("#txtNoOfBonusIssuePerShare").val($GlobalBonusIssue.BonusList.BonusIssuePerShare);
        $("#txtDateRegisterOfMembersOn").val($GlobalBonusIssue.BonusList.RegisterOfMembersOn);
        if ($GlobalBonusIssue.BonusList.AmountPaidOnEachShare == 0)
            $("#txtAmtToBeTreatedAsPaidOnEachShare").val('');
        else
            $("#txtAmtToBeTreatedAsPaidOnEachShare").val($GlobalBonusIssue.BonusList.AmountPaidOnEachShare);
        if ($GlobalBonusIssue.BonusList.TotalNoOfIssuedShares == 0)
            $("#txtResultantTotalIssuedShares").val('');
        else
            $("#txtResultantTotalIssuedShares").val($GlobalBonusIssue.BonusList.TotalNoOfIssuedShares);
        if ($GlobalBonusIssue.BonusList.ResultantIssuedCapital == 0)
            $("#txtResultantIssuedCapital").val('');
        else
            $("#txtResultantIssuedCapital").val($GlobalBonusIssue.BonusList.ResultantIssuedCapital);
        
        //if ($GlobalBonusIssue.BonusList.ClassOfShare == 0)
        //    $("#ddlBonusIssueClassOfShare").val('-1');
        //else
        //$("#ddlBonusIssueClassOfShare").val($GlobalBonusIssue.BonusList.ClassOfShare);

        if ($GlobalBonusIssue.BonusList.ClassOfShare > 0)
            $("#ddlBonusIssueClassOfShare").val($GlobalBonusIssue.BonusList.ClassOfShare);


        if ($GlobalBonusIssue.BonusList.ResultantPaidUpCapital == 0)
            $("#txtResultantPaidUpCapital").val('');

        else
            $("#txtResultantPaidUpCapital").val($GlobalBonusIssue.BonusList.ResultantPaidUpCapital);

        $("#chkIsRegisteredAddressasPlaceOfMeeting").prop("checked", $GlobalBonusIssue.BonusList.IsRegisteredAddressAsPlaceOfMeeting);
        $("#txtAddressLine1").val($GlobalBonusIssue.BonusList.MeetingAddressLine1);
        $("#txtAddressLine2").val($GlobalBonusIssue.BonusList.MeetingAddressLine2);
        $("#txtAddressLine3").val($GlobalBonusIssue.BonusList.MeetingAddressLine3);

        if ($GlobalBonusIssue.BonusList.MeetingAddressCountry == 0)
            CountryCallBack();
        else
            $("#ddlAddressCountry").val($GlobalBonusIssue.BonusList.MeetingAddressCountry);

        $("#txtAddressPostalCode").val($GlobalBonusIssue.BonusList.MeetingAddressPostalCode);

        if ($GlobalBonusIssue.BonusList.MeetingNotice != 0) {
            var MeetingNoticeVal = $("#ddlDirectorChosen_MeetingNotice option[personid=" + $GlobalBonusIssue.BonusList.MeetingNotice + "]option[sourcecode=" + $GlobalBonusIssue.BonusList.MeetingNoticeSource + "]").attr("value");
            $('#ddlDirectorChosen_MeetingNotice').val(MeetingNoticeVal).trigger('chosen:updated');
        }
        else
            $('#ddlDirectorChosen_MeetingNotice').val('').trigger('chosen:updated');

        if ($GlobalBonusIssue.BonusList.MeetingMinutes != 0) {
            var MeetingMinutesVal = $("#ddlDirectorChosen_MeetingMinutes option[personid=" + $GlobalBonusIssue.BonusList.MeetingMinutes + "]option[sourcecode=" + $GlobalBonusIssue.BonusList.MeetingMinutesSource + "]").attr("value");
            $('#ddlDirectorChosen_MeetingMinutes').val(MeetingMinutesVal).trigger('chosen:updated');
        }

        $("#txtOtherMeetingMinutes").val($GlobalBonusIssue.BonusList.OthersMeetingMinutes);


        if ($GlobalBonusIssue.BonusList.Designation != '' && $GlobalBonusIssue.BonusList.Designation != null)
            $("#txtDesignation").val($GlobalBonusIssue.BonusList.Designation);

        if ($GlobalBonusIssue.BonusList.NoticeOfResolution != 0) {
            var NoticeOfResolutionVal = $("#ddlDirectorChosen_NoticeOfResolution option[personid=" + $GlobalBonusIssue.BonusList.NoticeOfResolution + "]option[sourcecode=" + $GlobalBonusIssue.BonusList.NoticeOfResolutionSource + "]").attr("value");
            $('#ddlDirectorChosen_NoticeOfResolution').val(NoticeOfResolutionVal).trigger('chosen:updated');
        }
        else
            $('#ddlDirectorChosen_NoticeOfResolution').val('').trigger('chosen:updated');

        if ($GlobalBonusIssue.BonusList.LetterOfAllotment != 0) {
            var LetterofAllotmentVal = $("#ddlDirectorChosen_LetterofAllotment option[personid=" + $GlobalBonusIssue.BonusList.LetterOfAllotment + "]option[sourcecode=" + $GlobalBonusIssue.BonusList.LetterOfAllotmentSource + "]").attr("value");
            $('#ddlDirectorChosen_LetterofAllotment').val(LetterofAllotmentVal).trigger('chosen:updated');
        }
        else
            $('#ddlDirectorChosen_LetterofAllotment').val('').trigger('chosen:updated');

        if ($GlobalBonusIssue.BonusList.ReturnOfAllotment != 0) {
            var ReturnofAllotmentVal = $("#ddlDirectorChosen_ReturnofAllotment option[personid=" + $GlobalBonusIssue.BonusList.ReturnOfAllotment + "]option[sourcecode=" + $GlobalBonusIssue.BonusList.ReturnOfAllotmentSource + "]").attr("value");
            $('#ddlDirectorChosen_ReturnofAllotment').val(ReturnofAllotmentVal).trigger('chosen:updated');
        }
        else
            $('#ddlDirectorChosen_ReturnofAllotment').val('').trigger('chosen:updated');


        if ($GlobalBonusIssue.BonusList.ShareHoldingStructure == 0)
            $("#ddlShareHoldingStructure").val('');

        else
            $("#ddlShareHoldingStructure").val($GlobalBonusIssue.BonusList.ShareHoldingStructure);
        if ($GlobalBonusIssue.BonusList.ConsiderationOfEachShare == 0)
            $("#txtConsiderationOfEachShare").val('');
        else {
            $("#txtConsiderationOfEachShare").val($GlobalBonusIssue.BonusList.ConsiderationOfEachShare);
            $GlobalBonusIssue.ConsiderationOfEachShareInList = $GlobalBonusIssue.BonusList.ConsiderationOfEachShare;
        }
        if ($GlobalBonusIssue.BonusList.Currency == 0)
            CurrencyCallBack();
        else
            $("#ddlDividentPerShareCurrency").val($GlobalBonusIssue.BonusList.Currency);
        if ($GlobalBonusIssue.BonusList.TotalNoOfNewSharesToBeAllotted == 0)
            $("#txtTotalNoOfNewSharesToBeAllotted").val('');
        else
            $("#txtTotalNoOfNewSharesToBeAllotted").val($GlobalBonusIssue.BonusList.TotalNoOfNewSharesToBeAllotted);
        if ($GlobalBonusIssue.BonusList.TotalConsideration == 0)
            $("#txtTotalConsideration").val('');
        else
            $("#txtTotalConsideration").val($GlobalBonusIssue.BonusList.TotalConsideration);

    } catch (e) {
        console.log(e);
    }
}
function BonusSavedStatus() {

    if ($GlobalBonusIssue.BonusList >= 1) {

        $ShareHolderFlag = $GlobalBonusIssue.BonusList;

        ShowNotify('Success.', 'success', 2000);
        return false;
    }
}
function BindShareholdersByWOID() {
    try {
        $GlobalBonusIssue.WOID = $('#hdnWOID').val();
        var ShareClassID = $('#ddlBonusIssueClassOfShare').val();
        WoBonusIssueCallServices("/WODI/GetShareholdersByWOID", 'ShareholdersTemplate', 'trBonusShareholdersData', "{'WOID':" + $GlobalBonusIssue.WOID + ",'ShareClassID':" + ShareClassID + "}", true, callbackShareHolders);
    }
    catch (ex) {
        console.log(ex);
    }
}
function callbackShareHolders() {
    try {
        var LengthOfShareHolders = $("#trBonusShareholdersData").find("tr").length;
        $(".chkSelectAllBOISSH").prop("checked", false);
        if (LengthOfShareHolders >= 1) {
            $("#divShareholdersNoData").hide();
            $("#trBonusShareholdersData").find('.DeleteShareHolders').unbind('click');
            $("#trBonusShareholdersData").find('.DeleteShareHolders').click(DeleteShareHolders);
            //$("#trBonusShareholdersData").find('.txtShareHeld').unbind('keypress');
            //$("#trBonusShareholdersData").find('.txtShareHeld').keypress(AllowNumericNumbers);
            $("#trBonusShareholdersData").find('.txtShareHeld').unbind('keyup');
            $("#trBonusShareholdersData").find('.txtShareHeld').keyup(CalculateEveryShareHeld);
            // $("#trBonusShareholdersData").find('.txtNetCashDividend').unbind('keypress');
            // $("#trBonusShareholdersData").find('.txtNetCashDividend').keypress(AllowDecimalInCashDividend);
            $("#trBonusShareholdersData").find('.txtNetCashDividend').unbind('keyup');
            $("#trBonusShareholdersData").find('.txtNetCashDividend').keyup(CalculateEveryNetCashDividend);
            $("#trBonusShareholdersData").find('.NoOfBonusShareToIssue').unbind('keyup');
            $("#trBonusShareholdersData").find('.NoOfBonusShareToIssue').keyup(CalculateBonusShares);

            $("#tblShareHoldersDevident").find('.chkShareholders').unbind('click').click(function () {
                var chkCheckedCount = $('#trBonusShareholdersData').find('input.chkShareholders:checkbox:checked').length;
                var chkTotalCount = $('#trBonusShareholdersData').find('.chkShareholders').length;
                if (chkCheckedCount != chkTotalCount) {
                    $('#chkSelectAllBOISSH').prop('checked', false)
                }
                else {
                    $('#chkSelectAllBOISSH').prop('checked', true)
                }
            });
        }
        else
            $("#divShareholdersNoData").show();


        if ($('#hdnWOCloseStatus').val() == 'hide') {
            $('.btnWOClose').hide();
        }

    } catch (e) {
        console.log(ex);
    }
}

function CalculateBonusShares() {

    //var TotalNetAmount = 0;
    var TotalNoOfShares = 0;

    $("#tblShareHoldersDevident").find("#trBonusShareholdersData").find("tr").each(function () {
        //var NetCashDividend = $(this).find(".txtNetCashDividend").val();
        //var ShareHeld = $(this).find(".txtShareHeld").val();
        var NoofBonusSharesToIssue = $(this).find(".NoOfBonusShareToIssue").val();

        //if (NetCashDividend == "") {
        //    NetCashDividend = 0;
        //}
        //if (ShareHeld == "") {
        //    ShareHeld = 0;
        //}

        NoofBonusSharesToIssue = (NoofBonusSharesToIssue == "") ? 0 : NoofBonusSharesToIssue;

        //TotalNetAmount = parseFloat(TotalNetAmount) + parseFloat(NetCashDividend);
        //TotalNoOfShares = parseInt(TotalNoOfShares) + parseInt(ShareHeld);    //Old logic
        TotalNoOfShares = parseInt(TotalNoOfShares) + parseInt(NoofBonusSharesToIssue);

    });

    //if (TotalNetAmount == 0)
    //    $("#txtTotalConsideration").val('');
    //else
    //    $("#txtTotalConsideration").val(TotalNetAmount.toFixed(2));
    if (TotalNoOfShares == 0)
        $("#txtTotalNoOfNewSharesToBeAllotted").val('');
    else
        $("#txtTotalNoOfNewSharesToBeAllotted").val(TotalNoOfShares);

}

function CalculateEveryNetCashDividend() {
    try {
        var TotalNetAmount = 0;
        var TotalNoOfShares = 0;
        $("#tblShareHoldersDevident").find("#trBonusShareholdersData").find("tr").each(function () {
            var NetCashDividend = $(this).find(".txtNetCashDividend").val();
            var ShareHeld = $(this).find(".txtShareHeld").val();

            if (NetCashDividend == "")
                NetCashDividend = 0;

            if (ShareHeld == "")
                ShareHeld = 0;

            TotalNetAmount = parseFloat(TotalNetAmount) + parseFloat(NetCashDividend);
            TotalNoOfShares = parseInt(TotalNoOfShares) + parseInt(ShareHeld);

        });

        // var TotalNetCheck = TotalNetAmount.toFixed(2);
        if (TotalNetAmount == 0)
            $("#txtTotalConsideration").val('');
        else
            $("#txtTotalConsideration").val(TotalNetAmount.toFixed(2));

        if (TotalNoOfShares == 0)
            $("#txtTotalNoOfNewSharesToBeAllotted").val('');
        else
            $("#txtTotalNoOfNewSharesToBeAllotted").val(TotalNoOfShares);
    }
    catch (ex) {
        console.log(ex);
    }
}
function CalculateEveryShareHeld() {
    try {

        var Dividend = '';
        var DividendPerShare = $("#txtConsiderationOfEachShare").val();
        var SharesHeld = '';
        SharesHeld = $(this).val();

        if (DividendPerShare != '' && SharesHeld != '') {
            Dividend = parseFloat((parseFloat(SharesHeld)) * DividendPerShare);
            $(this).closest('tr').find(".txtNetCashDividend").val(Dividend.toFixed(2));
        }
        else {
            Dividend = "";
            $(this).closest('tr').find(".txtNetCashDividend").val("");
        }

        var TotalNetAmount = 0;
        var TotalNoOfShares = 0;
        $("#tblShareHoldersDevident").find("#trBonusShareholdersData").find("tr").each(function () {
            var NetCashDividend = $(this).find(".txtNetCashDividend").val();
            var ShareHeld = $(this).find(".txtShareHeld").val();

            if (NetCashDividend == "")
                NetCashDividend = 0;


            if (ShareHeld == "")
                ShareHeld = 0;

            TotalNetAmount = parseFloat(TotalNetAmount) + parseFloat(NetCashDividend);
            TotalNoOfShares = parseInt(TotalNoOfShares) + parseInt(ShareHeld);

        });

        if (TotalNetAmount == 0)
            $("#txtTotalConsideration").val('');
        else
            $("#txtTotalConsideration").val(TotalNetAmount.toFixed(2));
        if (TotalNoOfShares == 0)
            $("#txtTotalNoOfNewSharesToBeAllotted").val('');
        else
            $("#txtTotalNoOfNewSharesToBeAllotted").val(TotalNoOfShares);

    }
    catch (ex) {
        console.log(ex);
    }
}
function DeleteShareHolders() {
    try {
        //var PersonID = $(this).attr('personid');
        //var WOID = $(this).attr('woid');
        //var PersonSource = $(this).attr('PersonSource');
        var WOShareHoldersDividentID = $(this).attr('WOShareHoldersDividentID');


        $("#dialog-confirm").removeClass('hide').dialog({
            resizable: false,
            modal: true,
            title_html: true,
            buttons: [
                {
                    html: "<i class='ace-icon fa fa-trash-o bigger-110'></i>&nbsp; Delete Item",
                    "class": "btn btn-danger btn-xs",
                    click: function () {
                        //******Start*********

                        var jsonText = JSON.stringify({ WOShareHoldersDividentID: WOShareHoldersDividentID });
                        WoBonusIssueCallServices("/WODI/DeleteShareholdersByWOID", '', '', jsonText, false, InsertShareHoldersCallback);

                        //*******End*************

                        $(this).dialog("close");
                    }
                },
                {
                    html: "<i class='ace-icon fa fa-times bigger-110'></i>&nbsp; Cancel",
                    "class": "btn btn-xs",
                    click: function () {
                        $(this).dialog("close");
                    }
                }
            ]
        });

    } catch (e) {
        console.log(e);
    }
}
function InsertShareHoldersCallback() {
    try {
        if ($GlobalBonusIssue.BonusList == 1) {
            ShowNotify('Success.', 'success', 2000);
            BindShareholdersByWOID();
            return false;
        }
    } catch (e) {
        console.log(e);
    }
}
function CountryCallBack() {
    try {
        $("#ddlAddressCountry").val("109");
    } catch (e) {
        console.log(e);
    }
}
function CurrencyCallBack() {
    try {
        $("#ddlDividentPerShareCurrency").val("14");
    } catch (e) {
        console.log(e);
    }
}
function CompanyDetailscallBack() {
    try {
        if ($GlobalBonusIssue.BonusList.MeetingAddressCountry != null && $GlobalBonusIssue.BonusList.MeetingAddressCountry != undefined && $GlobalBonusIssue.BonusList.MeetingAddressCountry != 0) {
            $("#txtAddressLine1").val($GlobalBonusIssue.BonusList.MeetingAddressLine1);
            $("#txtAddressLine2").val($GlobalBonusIssue.BonusList.MeetingAddressLine2);
            $("#txtAddressLine3").val($GlobalBonusIssue.BonusList.MeetingAddressLine3);
            if ($GlobalBonusIssue.BonusList.MeetingAddressCountry == 0 || $GlobalBonusIssue.BonusList.MeetingAddressCountry == -1)
                CountryCallBack();
            else
                $("#ddlAddressCountry").val($GlobalBonusIssue.BonusList.MeetingAddressCountry);
            $("#txtAddressPostalCode").val($GlobalBonusIssue.BonusList.MeetingAddressPostalCode);
        }
    } catch (e) {
        console.log(e);
    }
}
function ClearRegisteredAddressPlaceOfMeetingFields() {
    try {
        $("#txtAddressLine1").val('');
        $("#txtAddressLine2").val('');
        $("#txtAddressLine3").val('');
        CountryCallBack();
        $("#txtAddressPostalCode").val('');
    } catch (e) {
        console.log(e);
    }
}

function WoBonusIssueCallServices(path, templateId, containerId, parameters, clearContent, callBack) {
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
                    if (msg == '0') {
                        ShowNotify('Invalid session login again.', 'error', 3000);
                        return false;
                    }
                    $GlobalBonusIssue.BonusList = msg;
                    if (templateId != '' && containerId != '') {

                        if (!clearContent) {
                            $.tmpl($('#' + templateId).html(), msg.shereHoldersList).appendTo("#" + containerId);
                        }
                        else {
                            $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg.shereHoldersList));
                        }
                    }

                    if (callBack != undefined && callBack != '')
                        callBack();

                } catch (e) {

                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                //throw new Error(xhr.statusText);
            }
        });

    } catch (e) {

    }
}
