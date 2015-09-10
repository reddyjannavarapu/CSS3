$(document).ready(function () {

    if ($('#hdnWOCloseStatus').val() == 'hide') {
        $('.btnWOClose').hide();
    }

    $('#chkROPaceofAGM').change(function () {
        try {
            if ($(this).is(':checked')) {
                var WOID = $('#hdnWOID').val();

                CallWOROPlaceofAGMDetails("GetCompanyAddressesByWOID", "{'WOID':" + WOID + ",'IsFMGAddress':" + false + "}");
            }
            else {
                $('#txtPlaceOfAGM').val('');
            }
        } catch (e) {
            console.log(e);
        }
    });

    $GlobalWOAGMDetailsData = {};
    $GlobalWOAGMDetailsData.InsertedID = 0;
    $GlobalWOAGMDetailsData.WOAGMDetailsData = '';
    $GlobalWOAGMDetailsData.ShareHolderDevident = '';
    $GlobalShareHoldersFlag = '';

    $GlobalWOAGMDetailsDividendpershare = 0;

    $("#btnSaveShareHoldersDetails").click(function () {
        try {
            saveShareholdersDividendDetails();
        } catch (e) {
            console.log(e);
        }
    });

    $('#txtDateofAGM').datepicker({
        showOtherMonths: true,
        selectOtherMonths: true,
        showWeek: true,
        changeYear: true,
        changeMonth: true,
        dateFormat: 'dd/mm/yy',
        yearRange: "-10:+50"
    });

    $('#txtFinancialYearEnd').datepicker({
        showOtherMonths: true,
        selectOtherMonths: true,
        showWeek: true,
        changeYear: true,
        changeMonth: true,
        dateFormat: 'dd/mm/yy',
        yearRange: "-10:+50"
    });

    $('#txtFinancialStatement').datepicker({
        showOtherMonths: true,
        selectOtherMonths: true,
        showWeek: true,
        changeYear: true,
        changeMonth: true,
        dateFormat: 'dd/mm/yy',
        yearRange: "-10:+50"
    });

    $.mask.definitions['H'] = "[0-1]";
    $.mask.definitions['h'] = "[0-9]";
    $.mask.definitions['M'] = "[0-5]";
    $.mask.definitions['m'] = "[0-9]";
    $.mask.definitions['P'] = "[AaPp]";
    $.mask.definitions['p'] = "[Mm]";
    $("#txtTimeOfAGM").mask("Hh:Mm Pp");

    $('#txtTimeOfAGM').blur(function () {
        try {
            var TimeOfAGM = $(this).val();
            var Time = TimeOfAGM.substring(0, 2);

            if (Time > 12) {
                alert('Entered Time is incorrect, Hours Should be <= 12');
                //var AgmTime = $('#txtTimeOfAGM');
                //AgmTime.focus();
                $(this).val('');
                return false;
            }

        } catch (e) {
            console.log(e);
        }
    });


    $("#txtDividentPerShare").keyup(function (e) {
        try {

            var Dividend = '';
            var DividendPerShare = $(this).val();

            $("#tblShareHoldersDevident").find("#trSholderData").find("tr").each(function () {
                var SharesHeld = $(this).find(".txtShareHeld").val();
                var Result = (DividendPerShare == "" ? "0" : DividendPerShare) * SharesHeld;

                if (Result != "")
                    $(this).find(".txtNetCashDividend").val(Result.toFixed(2));
                else
                    $(this).find(".txtNetCashDividend").val("");
            });

            var TotalNetAmount = 0;
            var TotalNoOfShares = 0;

            $("#tblShareHoldersDevident").find("#trSholderData").find("tr").each(function () {
                var NetCashDividend = $(this).find(".txtNetCashDividend").val();
                var ShareHeld = $(this).find(".txtShareHeld").val();

                if (NetCashDividend == "")
                    NetCashDividend = 0;
                if (TotalNoOfShares == "")
                    TotalNoOfShares = 0;

                TotalNetAmount = parseFloat(TotalNetAmount) + parseFloat(NetCashDividend);
                TotalNoOfShares = parseInt(TotalNoOfShares) + parseInt(ShareHeld);

            });

            $("#txtTotalNetAmount").val(TotalNetAmount.toFixed(2));
            $("#txtTotalNoOfShares").val(TotalNoOfShares);

        } catch (e) {
            console.log(e);
        }
    });

    BindCurrencyinWOAGMDetails();

    $("#showSaveShareHoldersDetails").click(function () {
        BindShareHoldersDetails();
        $('#divShareholdersList').show();
        $("#showSaveShareHoldersDetails").hide();
    });


    $('#btnSaveWoAGMDetails').unbind('click').click(function () {
        try {
            AGMDetails = {};
            AGMDetails.WOIDForWoAGM = $('#hdnWOID').val();
            AGMDetails.DateOfAGM = $('#txtDateofAGM').val();
            AGMDetails.PlaceOfAGM = $('#txtPlaceOfAGM').val();
            AGMDetails.TimeOfAGM = $('#txtTimeOfAGM').val();
            AGMDetails.FinancialYearEnd = $('#txtFinancialYearEnd').val();
            AGMDetails.DateOfFinancialStatement = $('#txtFinancialStatement').val();
            AGMDetails.IsAuditor = $('#chkAuditors').is(':checked');
            AGMDetails.Auditors = $('#ddlAuditors option:selected').val();
            AGMDetails.ShareHoldingStructure = $('#ddlShareHoldingStructure option:selected').val();
            AGMDetails.IsDirectorsFeeAmount = $('#chkDirectorFeeAmount').is(':checked');
            AGMDetails.DirectorFeeAmount = $('#txtDirectorFeeAmount').val();
            AGMDetails.DirectorCurrency = $('#ddlDirectorCurrencyInWOAGMDetails option:selected').val();
            AGMDetails.IsRemunerationAmount = $('#chkRemuneration').is(':checked');
            AGMDetails.RemunerationAmount = $('#txtRemunerationAmount').val();
            AGMDetails.RemunerationCurrency = $('#ddlRemunerationCurrencyAmount option:selected').val();
            AGMDetails.OtherMeetingMinutes = $("#txtOtherMeetingMinutes").val();
            AGMDetails.ApprovalFS = $('#chkIsApprovalFS').is(':checked');
            var SelectedVals = new Array();
            SelectedVals = CheckDropDownIDAndSource("ddlDirectorChosen_MeetingNotice");
            AGMDetails.MeetingNotice = SelectedVals[0];
            AGMDetails.MeetingNoticeSource = SelectedVals[1];



            var SelectedVals = new Array();
            SelectedVals = CheckDropDownIDAndSource("ddlDirectorChosen_MeetingMinutes");
            AGMDetails.MeetingMinutes = SelectedVals[0];
            AGMDetails.MeetingMinutesSource = SelectedVals[1];

            var SelectedVals = new Array();
            SelectedVals = CheckDropDownIDAndSource("ddlDirectorChosen_S197Certificate");
            AGMDetails.S197Certificate = SelectedVals[0];
            AGMDetails.S197CertificateSource = SelectedVals[1];

            AGMDetails.DesignationofPersonSigningAGM = $('#txtDesignationOfPersonSigningAGMMinutes').val();

            var SelectedVals = new Array();
            SelectedVals = CheckDropDownIDAndSource("ddlDirectorChosen_S197Certificate");
            AGMDetails.S197Certificate = SelectedVals[0];
            AGMDetails.S197CertificateSource = SelectedVals[1];


            AGMDetails.IsS161toIssueShares = $('#chkS161toIssueShares').is(':checked');


            var SelectedVals = new Array();
            SelectedVals = CheckDropDownIDAndSource("ddlDirectorChosen_S161NoticeOfResolution");
            AGMDetails.S161NoticeofResolution = SelectedVals[0];
            AGMDetails.S161NoticeofResolutionSource = SelectedVals[1];


            var SelectedVals = new Array();
            SelectedVals = CheckDropDownIDAndSource("ddlDirectorChosen_DividentVoucher");
            AGMDetails.DividentVoucher = SelectedVals[0];
            AGMDetails.DividentVoucherSource = SelectedVals[1];

            AGMDetails.IsDirectorsdueforRetirement = $('#chkDirectorsDueForRetirement').is(':checked');

            AGMDetails.IsDividend = $('#chkDivident').is(':checked');

            AGMDetails.ROPlaceOfAGM = $('#chkROPaceofAGM').is(':checked');

            AGMDetails.MeetingAddressLine1 = $('#txtMeetingAddressLine1').val();
            AGMDetails.MeetingAddressLine2 = $('#txtMeetingAddressLine2').val();
            AGMDetails.MeetingAddressLine3 = $('#txtMeetingAddressLine3').val();
            AGMDetails.MeetingAddressCountry = $('#ddlMeetingAddressCountry').val();
            AGMDetails.MeetingAddressPostalCode = $('#txtMeetingAddressPostalCode').val();

            var count = 0;

            count += ControlEmptyNess(false, $("#txtDateofAGM"), 'Please enter Date of AGM.');
            count += ControlEmptyNess(false, $("#txtPlaceOfAGM"), 'Please enter Place of AGM.');
            count += ControlEmptyNess(false, $("#txtTimeOfAGM"), 'Please enter Time of AGM.');
            count += ControlEmptyNess(false, $("#txtFinancialYearEnd"), 'Please enter Financial Year End.');
            count += ControlEmptyNess(false, $("#txtFinancialStatement"), 'Please enter Date of Financial Statement.');

            count += ControlEmptyNess(false, $('#ddlAuditors option:selected'), 'Please select Auditors.');
            count += ControlEmptyNess(false, $('#ddlShareHoldingStructure option:selected'), 'Please select Share Holding Structure.');

            count += ControlEmptyNess(false, $("#txtDesignationOfPersonSigningAGMMinutes"), 'Please enter Designation.');

            count += ControlEmptyNess(false, $("#txtFinancialStatement"), 'Please enter Date of Financial Statement.');
            count += ControlEmptyNess(false, $("#txtFinancialStatement"), 'Please enter Date of Financial Statement.');


            if (AGMDetails.IsDirectorsFeeAmount == true) {
                if (AGMDetails.DirectorFeeAmount == '' || AGMDetails.DirectorCurrency == '') {
                    count++;
                }
                if (AGMDetails.DirectorFeeAmount != '') {
                    var LenReturn = checkDegitsBeforeDecimal(AGMDetails.DirectorFeeAmount, 30);
                    if (LenReturn == false) {
                        ShowNotify('Please Enter only 30 degits before decimal.', 'error', 3000);
                        return false;
                    }
                }
            }
            if (AGMDetails.IsRemunerationAmount == true) {
                if (AGMDetails.RemunerationAmount == '' || AGMDetails.RemunerationCurrency == '') {
                    count++;
                }
                if (AGMDetails.RemunerationAmount != '') {
                    var LenReturn = checkDegitsBeforeDecimal(AGMDetails.RemunerationAmount, 30);
                    if (LenReturn == false) {
                        ShowNotify('Please Enter only 30 degits before decimal.', 'error', 3000);
                        return false;
                    }
                }
            }


            else if (AGMDetails.IsS161toIssueShares == true) {
                if (AGMDetails.S161NoticeofResolution == '') {
                    count++;
                }
            }


            if (count > 0) {
                ShowNotify('Please Enter values for all mandatory fields.', 'error', 3000);
                return false;
            }
            else {
                var jsonAGMDetailsText = JSON.stringify({ AGMDetails: AGMDetails });
                CallWOAGMDetails("/WODI/InsertWOAGMDetails", '', '', jsonAGMDetailsText, true, CreateWOAGMDetailsCallBack);
            }

        } catch (e) {
            console.log(e);
        }
    });


    $('#btnCancelAGMDetails').unbind('click').click(function () {
        try {
            ClearWoAGMVAlues();
        } catch (e) {
            console.log(e);
        }
    });


});

function CreateWOAGMDetailsCallBack() {
    try {
        $GlobalShareHoldersFlag = $GlobalWOAGMDetailsData.WOAGMDetailsData;

        if ($GlobalWOAGMDetailsData.InsertedID >= 1) {
            ShowNotify('Success.', 'success', 2000);
            // ClearWoAGMVAlues();
        }

    } catch (e) {
        console.log(e);
    }
}

function BindShareHoldersDetails() {
    try {
        var WOID = $('#hdnWOID').val();
        ShareHoldersCallServices("/WODI/GetShareholdersByWOID", 'ShareholdersTemplate', 'trSholderData', "{'WOID':" + WOID + ",'ShareClassID': -1}", true, callbackShareHolders);
    } catch (e) {
        console.log(e);
    }
}

function CallbackAfterInsertShareHolders() {
    try {
        if ($GlobalWOAGMDetailsData.ShareHolderDevident == '2') {
            ShowNotify('Please Insert AGM details first.', 'error', 3000);
            return false;
        }
        else if ($GlobalWOAGMDetailsData.ShareHolderDevident == '1') {
            ShowNotify('Success.', 'success', 2000);
            BindShareHoldersDetails();
        }

    } catch (e) {
        console.log(e);
    }
}

function callbackShareHolders() {
    try {
        $(".chkSelectAllAGMSH").prop("checked", false);
        if ($("#trSholderData").find("tr").length == 0) {
            $("#divShareholdersList").hide();
            return false;
        }

        $("#tblShareHoldersDevident").find('.txtShareHeld').unbind('keyup');
        $("#tblShareHoldersDevident").find('.chkShareholders').unbind('click').click(function () {
            var chkCheckedCount = $('#trSholderData').find('input.chkShareholders:checkbox:checked').length;
            var chkTotalCount = $('#trSholderData').find('.chkShareholders').length;
            if (chkCheckedCount != chkTotalCount) {
                $('#chkSelectAllAGMSH').prop('checked', false)
            }
            else {
                $('#chkSelectAllAGMSH').prop('checked', true)
            }
        });


        $("#tblShareHoldersDevident").find('.txtNetCashDividend').unbind('keyup');
        $("#tblShareHoldersDevident").find('.txtNetCashDividend').keyup(function (event) {

            var TotalNetAmount = 0;
            var TotalNoOfShares = 0;
            $("#tblShareHoldersDevident").find("#trSholderData").find("tr").each(function () {
                var NetCashDividend = $(this).find(".txtNetCashDividend").val();
                var ShareHeld = $(this).find(".txtShareHeld").val();

                if (NetCashDividend == "")
                    NetCashDividend = 0;

                if (ShareHeld == "")
                    ShareHeld = 0;

                TotalNetAmount = parseFloat(TotalNetAmount) + parseFloat(NetCashDividend);
                TotalNoOfShares = parseInt(TotalNoOfShares) + parseInt(ShareHeld);

            });

            $("#txtTotalNetAmount").val(TotalNetAmount.toFixed(2));
            $("#txtTotalNoOfShares").val(TotalNoOfShares);
        });

        $('#chkSelectAllAGMSH').click(function () {
            try {
                if ($(this).is(':checked')) {
                    $('#trSholderData').find('.chkShareholders').prop('checked', true);
                }
                else {
                    $('#trSholderData   ').find('.chkShareholders').prop('checked', false);
                }
            } catch (e) {
                console.log(e);
            }
        });

        $("#tblShareHoldersDevident").find('.txtShareHeld').keyup(function (event) {

            var Dividend = '';
            var DividendPerShare = $("#txtDividentPerShare").val();
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
            $("#tblShareHoldersDevident").find("#trSholderData").find("tr").each(function () {
                var NetCashDividend = $(this).find(".txtNetCashDividend").val();
                var ShareHeld = $(this).find(".txtShareHeld").val();

                if (NetCashDividend == "")
                    NetCashDividend = 0;


                if (ShareHeld == "")
                    ShareHeld = 0;

                TotalNetAmount = parseFloat(TotalNetAmount) + parseFloat(NetCashDividend);
                TotalNoOfShares = parseInt(TotalNoOfShares) + parseInt(ShareHeld);

            });

            $("#txtTotalNetAmount").val(TotalNetAmount.toFixed(2));
            $("#txtTotalNoOfShares").val(TotalNoOfShares);

        });

        $("#tblShareHoldersDevident").find('.DeleteShareHolders').unbind('click');
        $("#tblShareHoldersDevident").find('.DeleteShareHolders').click(DeleteShareHolders);

        if ($('#hdnWOCloseStatus').val() == 'hide') {
            $('.btnWOClose').hide();
        }

    } catch (e) {
        console.log(e);
    }
}

function saveShareholdersDividendDetails() {
    try {
        if ($GlobalShareHoldersFlag == 0) {
            ShowNotify('Please Insert AGM details first.', 'error', 3000);
            return false;
        } else {

            var IsDivident = $('#chkDivident').is(':checked');
            //var IsDivident = $('#chkAuditors').is(':checked');
            var DividendPerShare = $('#txtDividentPerShare').val();

            $GlobalWOAGMDetailsDividendpershare = $('#txtDividentPerShare').val();

            var DividentShareCurrency = $('#ddlDividentPerShareCurrency option:selected').val();
            var TotalNetAmount = $('#txtTotalNetAmount').val();
            var TotalNoShare = $('#txtTotalNoOfShares').val();
            var WOID = $('#hdnWOID').val();


            var Checked = ''; var ISSharesHeldNull = 'False'; var ISNetCashDividend = 'False';
            var ShareDividendFields = new Array();
            $("#chkShareholders:checked").each(function (index) {
                if ($(this).is(':checked')) {
                    Checked = 'true';
                    ShareDividend = {};
                    ShareDividend.PersonId = $(this).attr('personid');
                    ShareDividend.PersonSource = $(this).attr('PersonSource');
                    ShareDividend.WOID = $(this).attr('WOID');
                    ShareDividend.ClassofShare = $(this).attr('ClassofShare');
                    ShareDividend.SharesHeld = $(this).closest("tr").find($("[id*=txtShareHeld]")).val();
                    ShareDividend.NetCashDividend = $(this).closest("tr").find($("[id*=txtNetCashDividend]")).val();
                    ISSharesHeldNull == 'False' ? (ShareDividend.SharesHeld == '' ? ISSharesHeldNull = 'True' : 'False') : '';
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
            var WOTypeName = 'AGM';

            var jsonText = JSON.stringify({ ShareDividendFields: ShareDividendFields, IsDivident: IsDivident, DividendPerShare: DividendPerShare, DividentShareCurrency: DividentShareCurrency, TotalNetAmount: TotalNetAmount, TotalNoOfShares: TotalNoShare, WOID: WOID, WOTypeName: WOTypeName, ClassofShare: -1 });

            ShareHoldersCallServices("/WODI/InsertShareholdersByWOID", '', '', jsonText, false, CallbackAfterInsertShareHolders);
        }

    } catch (e) {
        console.log(e);
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
                        ShareHoldersCallServices("/WODI/DeleteShareholdersByWOID", '', '', jsonText, false, CallbackAfterInsertShareHolders);
                        if ($GlobalWOAGMDetailsData.ShareHolderDevident == '1') {
                            ShowNotify('Success.', 'success', 2000);
                        }
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

function keyupCalculation() {
    try {
        var checkCharater = AllowNumbersOnly($(this).val(), event);
        if (!checkCharater) {
            event.preventDefault();
        }

        var Dividend = '';
        var DividendPerShare = $("#txtDividentPerShare").val();
        var SharesHeld = '';
        SharesHeld = $(this).val();

        if (DividendPerShare != '' && SharesHeld != '')
            Dividend = parseFloat((parseFloat(SharesHeld)) * DividendPerShare);
        $(this).closest('tr').find(".txtNetCashDividend").val(Dividend.toFixed(2));

    } catch (e) {
        console.log(e);
    }
}

function BindCurrencyinWOAGMDetails() {
    CallServicesCurrencyDropdown("/PartialContent/GetCurrencyDetails", 'CurrencyDropDownTemplateinWOAGMDetails', 'ddlDirectorCurrencyInWOAGMDetails', "{}", false, CallBackCurrency);
    CallServicesCurrencyDropdown("/PartialContent/GetCurrencyDetails", 'CurrencyDropDownTemplateinWOAGMDetails', 'ddlDividentPerShareCurrency', "{}", false, CallBackShareHoldersCurrency);
    CallServicesCurrencyDropdown("/PartialContent/GetCurrencyDetails", 'CurrencyDropDownTemplateinWOAGMDetails', 'ddlRemunerationCurrencyAmount', "{}", false, CurrencyDropDownsBindingCallBack);
    CallMasterData("/PartialContent/GetCountryDetails", 'scriptCountry', 'ddlMeetingAddressCountry', "{}", false, CountryCallBack);
}

function CallBackCurrency(value) {
    try {
        $('#' + value).val(14);
    } catch (e) {
        console.log(e);
    }
}
function CallBackShareHoldersCurrency(value) {
    try {
        $('#ddlDividentPerShareCurrency').val(14);
    } catch (e) {
        console.log(e);
    }
}

function CallServicesCurrencyDropdown(path, templateId, containerId, parameters, clearContent, callBack) {
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
                    if (templateId != '' && containerId != '') {

                        if (!clearContent) {
                            $.tmpl($('#' + templateId).html(), msg).appendTo("#" + containerId);
                        }
                        else {
                            $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg));
                        }
                    }

                    if (callBack != undefined && callBack != '')
                        callBack(containerId);

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

function CurrencyDropDownsBindingCallBack(value) {
    try {
        $('#' + value).val(14);
        CallBackCurrency('ddlRemunerationCurrencyAmount');        
        //BindShareHoldingAndAuditor(); // on 12-July-2015, changed to static values       
    } catch (e) {
        console.log(e);
    }
}
function CountryCallBack() {
    try {
        $("#ddlMeetingAddressCountry").val("109");

        GetWOAGMDetailsByWOID();

    } catch (e) {
        console.log(e);
    }
}

function BindShareHoldingAndAuditor() {
    try {
        CallMasterData("/WODI/GetAllAuditorsStatus", 'AuditorsStatusDropDownTemplateinWOAGMDetails', 'ddlAuditors', "{}", false, '');
        //CallMasterData("/WODI/GetAllShareHoldingStructures", 'ShareHoldingStructureDropDownTemplateinWOAGMDetails', 'ddlShareHoldingStructure', "{}", false, BindShareHoldingAndAuditorCallBack);
    } catch (e) {
        console.log(e);
    }
}

function BindShareHoldingAndAuditorCallBack() {
    try {
        GetWOAGMDetailsByWOID();
    } catch (e) {
        console.log(e);
    }
}

function GetWOAGMDetailsByWOID() {
    try {
        var WOID = $('#hdnWOID').val();
        CallWOAGMDetails("/WODI/GetWOAGMDetailsByWOID", '', '', "{'WOID':" + WOID + "}", '', GetWOAGMDetailsByWOIDCallBack);
    } catch (e) {
        console.log(e);
    }
}

function GetWOAGMDetailsByWOIDCallBack() {
    try {

        $GlobalShareHoldersFlag = $GlobalWOAGMDetailsData.WOAGMDetailsData.ID;

        if ($GlobalWOAGMDetailsData.WOAGMDetailsData.WOIDForWoAGM != null) {

            $('#txtDateofAGM').val($GlobalWOAGMDetailsData.WOAGMDetailsData.DateOfAGM);
            $('#chkROPaceofAGM').prop('checked', $GlobalWOAGMDetailsData.WOAGMDetailsData.ROPlaceOfAGM);
            $('#txtPlaceOfAGM').val($GlobalWOAGMDetailsData.WOAGMDetailsData.PlaceOfAGM);
            $('#txtTimeOfAGM').val($GlobalWOAGMDetailsData.WOAGMDetailsData.TimeOfAGM);
            $('#txtFinancialYearEnd').val($GlobalWOAGMDetailsData.WOAGMDetailsData.FinancialYearEnd);
            $('#txtFinancialStatement').val($GlobalWOAGMDetailsData.WOAGMDetailsData.DateOfFinancialStatement);

            $('#chkAuditors').prop('checked', $GlobalWOAGMDetailsData.WOAGMDetailsData.IsAuditor);
            $('#chkIsApprovalFS').prop('checked', $GlobalWOAGMDetailsData.WOAGMDetailsData.ApprovalFS);


            if ($GlobalWOAGMDetailsData.WOAGMDetailsData.Auditors == '0')
                $GlobalWOAGMDetailsData.WOAGMDetailsData.Auditors = '';

            $('#ddlAuditors').val($GlobalWOAGMDetailsData.WOAGMDetailsData.Auditors);

            if ($GlobalWOAGMDetailsData.WOAGMDetailsData.ShareHoldingStructure == '0')
                $GlobalWOAGMDetailsData.WOAGMDetailsData.ShareHoldingStructure = '';

            $('#ddlShareHoldingStructure').val($GlobalWOAGMDetailsData.WOAGMDetailsData.ShareHoldingStructure);

            $('#chkDirectorFeeAmount').prop('checked', $GlobalWOAGMDetailsData.WOAGMDetailsData.IsDirectorsFeeAmount);

            if ($GlobalWOAGMDetailsData.WOAGMDetailsData.IsDirectorsFeeAmount) {
                $('#dvDirectorFeeAmountContent').show();
            }
            $('#txtDirectorFeeAmount').val($GlobalWOAGMDetailsData.WOAGMDetailsData.DirectorFeeAmount);

            if ($GlobalWOAGMDetailsData.WOAGMDetailsData.DirectorCurrency == '0')
                $GlobalWOAGMDetailsData.WOAGMDetailsData.DirectorCurrency = '';

            $('#ddlDirectorCurrencyInWOAGMDetails').val($GlobalWOAGMDetailsData.WOAGMDetailsData.DirectorCurrency);

            $('#chkRemuneration').prop('checked', $GlobalWOAGMDetailsData.WOAGMDetailsData.IsRemunerationAmount);
            if ($GlobalWOAGMDetailsData.WOAGMDetailsData.IsRemunerationAmount) {
                $('#dvRemunerationAmount').show();
            }
            $('#txtRemunerationAmount').val($GlobalWOAGMDetailsData.WOAGMDetailsData.RemunerationAmount);

            if ($GlobalWOAGMDetailsData.WOAGMDetailsData.RemunerationCurrency == '0')
                $GlobalWOAGMDetailsData.WOAGMDetailsData.RemunerationCurrency = '';

            $('#ddlRemunerationCurrencyAmount').val($GlobalWOAGMDetailsData.WOAGMDetailsData.RemunerationCurrency);

            var MeetingNoticevalueFrompersonidAndsourcecode = $("#ddlDirectorChosen_MeetingNotice option[personid=" + $GlobalWOAGMDetailsData.WOAGMDetailsData.MeetingNotice + "]option[sourcecode=" + $GlobalWOAGMDetailsData.WOAGMDetailsData.MeetingNoticeSource + "]").attr("value");
            $('#ddlDirectorChosen_MeetingNotice').val(MeetingNoticevalueFrompersonidAndsourcecode).trigger('chosen:updated');


            $("#txtOtherMeetingMinutes").val($GlobalWOAGMDetailsData.WOAGMDetailsData.OtherMeetingMinutes);

            var MeetingMinutesvalueFrompersonidAndsourcecode = $("#ddlDirectorChosen_MeetingMinutes option[personid=" + $GlobalWOAGMDetailsData.WOAGMDetailsData.MeetingMinutes + "]option[sourcecode=" + $GlobalWOAGMDetailsData.WOAGMDetailsData.MeetingMinutesSource + "]").attr("value");
            $('#ddlDirectorChosen_MeetingMinutes').val(MeetingMinutesvalueFrompersonidAndsourcecode).trigger('chosen:updated');

            if ($GlobalWOAGMDetailsData.WOAGMDetailsData.DesignationofPersonSigningAGM != '' && $GlobalWOAGMDetailsData.WOAGMDetailsData.DesignationofPersonSigningAGM != null)
                $('#txtDesignationOfPersonSigningAGMMinutes').val($GlobalWOAGMDetailsData.WOAGMDetailsData.DesignationofPersonSigningAGM);

            var S197CertificatevalueFrompersonidAndsourcecode = $("#ddlDirectorChosen_S197Certificate option[personid=" + $GlobalWOAGMDetailsData.WOAGMDetailsData.S197Certificate + "]option[sourcecode=" + $GlobalWOAGMDetailsData.WOAGMDetailsData.S197CertificateSource + "]").attr("value");
            $('#ddlDirectorChosen_S197Certificate').val(S197CertificatevalueFrompersonidAndsourcecode).trigger('chosen:updated');

            $('#chkS161toIssueShares').prop('checked', $GlobalWOAGMDetailsData.WOAGMDetailsData.IsS161toIssueShares);
            if ($GlobalWOAGMDetailsData.WOAGMDetailsData.IsS161toIssueShares) {
                $('.S161toIssueSharesContent').show();
            }

            var S161NoticeOfResolutionvalueFrompersonidAndsourcecode = $("#ddlDirectorChosen_S161NoticeOfResolution option[personid=" + $GlobalWOAGMDetailsData.WOAGMDetailsData.S161NoticeofResolution + "]option[sourcecode=" + $GlobalWOAGMDetailsData.WOAGMDetailsData.S161NoticeofResolutionSource + "]").attr("value");
            $('#ddlDirectorChosen_S161NoticeOfResolution').val(S161NoticeOfResolutionvalueFrompersonidAndsourcecode).trigger('chosen:updated');

            var DividentVouchervalueFrompersonidAndsourcecode = $("#ddlDirectorChosen_DividentVoucher option[personid=" + $GlobalWOAGMDetailsData.WOAGMDetailsData.DividentVoucher + "]option[sourcecode=" + $GlobalWOAGMDetailsData.WOAGMDetailsData.DividentVoucherSource + "]").attr("value");
            $('#ddlDirectorChosen_DividentVoucher').val(DividentVouchervalueFrompersonidAndsourcecode).trigger('chosen:updated');

            $('#chkDirectorsDueForRetirement').prop('checked', $GlobalWOAGMDetailsData.WOAGMDetailsData.IsDirectorsdueforRetirement);
            $('#chkDivident').prop('checked', $GlobalWOAGMDetailsData.WOAGMDetailsData.IsDividend);

            $GlobalWOAGMDetailsDividendpershare = $GlobalWOAGMDetailsData.WOAGMDetailsData.Dividendpershare;

            $('#txtDividentPerShare').val($GlobalWOAGMDetailsData.WOAGMDetailsData.Dividendpershare);
            if ($GlobalWOAGMDetailsData.WOAGMDetailsData.DividendCurrency == '0')
                $GlobalWOAGMDetailsData.WOAGMDetailsData.DividendCurrency = '';
            $('#ddlDividentPerShareCurrency').val($GlobalWOAGMDetailsData.WOAGMDetailsData.DividendCurrency);
            $('#txtTotalNetAmount').val($GlobalWOAGMDetailsData.WOAGMDetailsData.TotalNetAmountofDividend);
            $('#txtTotalNoOfShares').val($GlobalWOAGMDetailsData.WOAGMDetailsData.TotalNoOfShares);

            $('#txtMeetingAddressLine1').val($GlobalWOAGMDetailsData.WOAGMDetailsData.MeetingAddressLine1);
            $('#txtMeetingAddressLine2').val($GlobalWOAGMDetailsData.WOAGMDetailsData.MeetingAddressLine2);
            $('#txtMeetingAddressLine3').val($GlobalWOAGMDetailsData.WOAGMDetailsData.MeetingAddressLine3);

            if ($GlobalWOAGMDetailsData.WOAGMDetailsData.MeetingAddressCountry == '0' || $GlobalWOAGMDetailsData.WOAGMDetailsData.MeetingAddressCountry == '-1') {
                CountryCallBack();
            }
            else {
                $('#ddlMeetingAddressCountry').val($GlobalWOAGMDetailsData.WOAGMDetailsData.MeetingAddressCountry);
            }

            $('#txtMeetingAddressPostalCode').val($GlobalWOAGMDetailsData.WOAGMDetailsData.MeetingAddressPostalCode);

        }
        else {
            var WOID = $('#hdnWOID').val();
            CallWOROPlaceofAGMDetails("GetCompanyAddressesByWOID", "{'WOID':" + WOID + ",'IsFMGAddress':" + false + "}");
        }



    } catch (e) {
        console.log(e);
    }
}
function ClearWoAGMVAlues() {
    try {
        $('#txtDateofAGM').val('');
        $('#txtPlaceOfAGM').val('');
        $('#chkROPaceofAGM').prop('checked', false);
        $('#txtTimeOfAGM').val('10:00 am');
        $('#txtFinancialYearEnd').val('');
        $('#txtFinancialStatement').val('');
        $('#chkAuditors').prop('checked', false);
        $('#ddlAuditors').val('');
        $('#ddlShareHoldingStructure').val('');
        $('#chkDirectorFeeAmount').prop('checked', false);
        $('#txtDirectorFeeAmount').val('');
        $('#chkRemuneration').prop('checked', false);
        $('#txtRemunerationAmount').val('');
        $('#ddlDirectorChosen_MeetingNotice').val("").trigger('chosen:updated');
        $('#ddlDirectorChosen_MeetingMinutes').val("").trigger('chosen:updated');
        $('#txtDesignationOfPersonSigningAGMMinutes').val('Chairman');
        $('#ddlDirectorChosen_S197Certificate').val("").trigger('chosen:updated');
        $('#chkS161toIssueShares').prop('checked', false);
        $('#ddlDirectorChosen_S161NoticeOfResolution').val("").trigger('chosen:updated');
        $('#ddlDirectorChosen_DividentVoucher').val("").trigger('chosen:updated');
        $('#chkDirectorsDueForRetirement').prop('checked', false);
        $('#chkDividentPerShare').val('');
        $('.ddlCurrencyWOAGMDetails').val('14');
        $('#txtDirectorsDueForRetirement').val('');
        $("#txtOtherMeetingMinutes").val('');

        $('#txtMeetingAddressLine1').val('');
        $('#txtMeetingAddressLine2').val('');
        $('#txtMeetingAddressLine3').val('');
        $('#ddlMeetingAddressCountry').val('109');
        $('#txtMeetingAddressPostalCode').val('');

    } catch (e) {
        console.log(e);
    }
}


function ShareHoldersCallServices(path, templateId, containerId, parameters, clearContent, callBack) {
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

                    if (templateId != '' && containerId != '') {

                        if (!clearContent) {
                            $.tmpl($('#' + templateId).html(), msg.shereHoldersList).appendTo("#" + containerId);
                        }
                        else {
                            $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg.shereHoldersList));
                        }
                    }
                    else {

                        $GlobalWOAGMDetailsData.ShareHolderDevident = msg;
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

function CallWOAGMDetails(path, templateId, containerId, parameters, clearContent, callBack) {
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
                    $GlobalWOAGMDetailsData.WOAGMDetailsData = msg;
                    $GlobalWOAGMDetailsData.InsertedID = msg;


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

function CallWOROPlaceofAGMDetails(path, parameters) {
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

                var count = 0;
                if (msg.MeetingAddressLine1 != '' && msg.MeetingAddressLine1 != 'null' && msg.MeetingAddressLine1 != null) {
                    $('#txtMeetingAddressLine1').val(msg.MeetingAddressLine1);
                    count++;
                }
                if (msg.MeetingAddressLine2 != '' && msg.MeetingAddressLine2 != 'null' && msg.MeetingAddressLine2 != null) {
                    $('#txtMeetingAddressLine2').val(msg.MeetingAddressLine2);
                    count++;
                }
                if (msg.MeetingAddressLine3 != '' && msg.MeetingAddressLine3 != 'null' && msg.MeetingAddressLine3 != null) {
                    $('#txtMeetingAddressLine3').val(msg.MeetingAddressLine3);
                    count++;
                }
                if (msg.MeetingAddressCountry != '' && msg.MeetingAddressCountry != 'null' && msg.MeetingAddressCountry != null) {
                    $('#ddlMeetingAddressCountry').val(msg.MeetingAddressCountry);
                    count++;
                }
                if (msg.MeetingAddressPostalCode != '' && msg.MeetingAddressPostalCode != 'null' && msg.MeetingAddressPostalCode != null) {
                    $('#txtMeetingAddressPostalCode').val(msg.MeetingAddressPostalCode);
                    count++;
                }
                //$('#txtPlaceOfAGM').val(PlaceOfAGM);
                if (count > 0)
                    $("#chkROPaceofAGM").prop("checked", true);

            },
            error: function (xhr, ajaxOptions, thrownError) {
                //throw new Error(xhr.statusText);
            }
        });

    } catch (e) {
        console.log(e);
    }
}