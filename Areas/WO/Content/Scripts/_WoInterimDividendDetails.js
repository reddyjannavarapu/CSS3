$(document).ready(function () {
    $GlobalDataInterimDividend = {};
    $GlobalDataInterimDividend.WOID = '';
    $GlobalDataInterimDividend.FinancialPeriod = '';
    $GlobalDataInterimDividend.ClassOfShare = '';
    $GlobalDataInterimDividend.DividendPerShare = '';
    $GlobalDataInterimDividend.Currency = '';
    $GlobalDataInterimDividend.TotalAmount = '';
    $GlobalDataInterimDividend.DateOfDeclaration = '';
    $GlobalDataInterimDividend.DateOfPayment = '';
    $GlobalDataInterimDividend.DividendDirector = '';
    $GlobalDataInterimDividend.DividendSource = '';
    $GlobalDataInterimDividend.SavedStatus = 0;
    $GlobalDataInterimDividend.WOInterimDividendBYWOID;
    $GlobalDataInterimDividend.ClassOfShareInList = '';
    $GlobalDataInterimDividend.NetSharesCount = '';
    $GlobalDataInterimDividend.IsDividend = '';
    $ShareHoldersFlag = '';

    if ($('#hdnWOCloseStatus').val() == 'hide') {
        $('.btnWOClose').hide();
    }

    BindClassOfShareAndCountry();
    BindWOInterimDividendDetailsByWOID();

    $("#txtDateOfDeclaration").datepicker({
        changeYear: true,
        changeMonth: true,
        showOtherMonths: true,
        selectOtherMonths: true,
        showWeek: true,
        dateFormat: 'dd/mm/yy',
        yearRange: "-10:+50"
    });

    $("#txtDateOfPayment").datepicker({
        changeYear: true,
        changeMonth: true,
        showOtherMonths: true,
        selectOtherMonths: true,
        showWeek: true,
        dateFormat: 'dd/mm/yy',
        yearRange: "-10:+50"
    });

    $("#txtFinancialPeriod").datepicker({
        changeYear: true,
        changeMonth: true,
        showOtherMonths: true,
        selectOtherMonths: true,
        showWeek: true,
        dateFormat: 'dd/mm/yy',
        yearRange: "-10:+50"
    });


    $('#chkSelectAllIDSH').click(function () {
        try {
            if ($(this).is(':checked')) {
                $('#trShareholdersData').find('.chkShareholders').prop('checked', true);
            }
            else {
                $('#trShareholdersData').find('.chkShareholders').prop('checked', false);
            }
        } catch (e) {
            console.log(e);
        }
    });

    $("#txtDividendPerShare").keyup(function (e) {
        try {
            var Dividend = '';
            var DividendPerShare = $(this).val();

            $("#tblShareHoldersDevidend").find("#trShareholdersData").find("tr").each(function () {
                var SharesHeld = $(this).find(".txtShareHeld").val();
                var Result = (DividendPerShare == "" ? "0" : DividendPerShare) * SharesHeld;

                if (Result != "")
                    $(this).find(".txtNetCashDividend").val(Result.toFixed(2));
                else
                    $(this).find(".txtNetCashDividend").val("");
            });

            var TotalNetAmount = 0;
            var TotalNoOfShares = 0;

            $("#tblShareHoldersDevidend").find("#trShareholdersData").find("tr").each(function () {
                var NetCashDividend = $(this).find(".txtNetCashDividend").val();
                var ShareHeld = $(this).find(".txtShareHeld").val();
                if (NetCashDividend == "")
                    NetCashDividend = 0;
                if (TotalNoOfShares == "")
                    TotalNoOfShares = 0;

                TotalNetAmount = parseFloat(TotalNetAmount) + parseFloat(NetCashDividend);
                TotalNoOfShares = parseInt(TotalNoOfShares) + parseInt(ShareHeld);
            });
            $("#txtTotalAmount").val(TotalNetAmount.toFixed(2));
            $("#txtTotalNumberOfShares").val(TotalNoOfShares);

        } catch (e) {
            console.log(e);
        }
    });

    $("#btnSaveShareHoldersDetails").click(function () {
        try {
            saveShareholdersDividendDetails();
        } catch (e) {
            console.log(e);
        }
    });

    $("#btnSaveInterimDividend").click(function () {
        try {
            $GlobalDataInterimDividend.FinancialPeriod = $("#txtFinancialPeriod").val();
            //$GlobalDataInterimDividend.ClassOfShare = $("#ddlShareClassForInterimDividend").val();
            $GlobalDataInterimDividend.DateOfDeclaration = $("#txtDateOfDeclaration").val();
            $GlobalDataInterimDividend.DateOfPayment = $("#txtDateOfPayment").val();

            var SelectedVals = new Array();
            SelectedVals = CheckDropDownIDAndSource("ddlDirectorChosen_DividendVoucher");

            $GlobalDataInterimDividend.DividendDirector = SelectedVals[0];
            $GlobalDataInterimDividend.DividendSource = SelectedVals[1];
            $GlobalDataInterimDividend.WOID = $('#hdnWOID').val();
            if ($GlobalDataInterimDividend.WOID != '' && $GlobalDataInterimDividend.WOID != undefined)
                InterimDividendCallService("SaveWoInterimDividendDetails", "", "", "{'WOID':" + $GlobalDataInterimDividend.WOID + ",'FinancialPeriod':'" + $GlobalDataInterimDividend.FinancialPeriod + "','DateOfDeclaration':'" + $GlobalDataInterimDividend.DateOfDeclaration + "','DateOfPayment':'" + $GlobalDataInterimDividend.DateOfPayment + "','DividendDirector':" + $GlobalDataInterimDividend.DividendDirector + ",'DividendSource':'" + $GlobalDataInterimDividend.DividendSource + "'}", '', InsertedStatusCallBack);

        } catch (e) {
            console.log(e);
        }
    });


    $("#btnClearInterimDividend").click(function () {
        try {
            $("#txtFinancialPeriod").val('');
            //$("#ddlShareClassForInterimDividend").val('-1');
            // $("#txtDividendPerShare").val('');
            //$("#ddlCurrencyForInterimDividend").val('14');
            // $("#txtTotalAmount").val('');
            $("#txtDateOfDeclaration").val('');
            $("#txtDateOfPayment").val('');
            $('#ddlDirectorChosen_DividendVoucher').val('').trigger('chosen:updated');
        } catch (e) {
            console.log(e);
        }
    });

    $('#ddlShareClassForInterimDividend').change(function () {
        BindShareHoldersDetails();
    });

});
function InterimDividendCallService(path, templateId, containerId, parameters, clearContent, callBack) {
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
                        return false
                    }

                    $GlobalDataInterimDividend.SavedStatus = msg;
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
function InsertedStatusCallBack() {
    try {

        $ShareHoldersFlag = $GlobalDataInterimDividend.SavedStatus;

        if ($GlobalDataInterimDividend.SavedStatus >= 1) {
            ShowNotify('Success.', 'success', 3000);
            return false;
        }
    } catch (e) {
        console.log(e);
    }
}
function CalculateEveryNetCashDividend() {
    try {
        var TotalNetAmount = 0;
        var TotalNoOfShares = 0;
        $("#tblShareHoldersDevidend").find("#trShareholdersData").find("tr").each(function () {
            var NetCashDividend = $(this).find(".txtNetCashDividend").val();
            var ShareHeld = $(this).find(".txtShareHeld").val();

            if (NetCashDividend == "")
                NetCashDividend = 0;

            if (ShareHeld == "")
                ShareHeld = 0;

            TotalNetAmount = parseFloat(TotalNetAmount) + parseFloat(NetCashDividend);
            TotalNoOfShares = parseInt(TotalNoOfShares) + parseInt(ShareHeld);

        });
        $("#txtTotalAmount").val(TotalNetAmount.toFixed(2));
        $("#txtTotalNumberOfShares").val(TotalNoOfShares);

    } catch (e) {
        console.log(e);
    }
}
function CalculateEveryShareHeld() {
    try {
        var Dividend = '';
        var DividendPerShare = $("#txtDividendPerShare").val();
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
        $("#tblShareHoldersDevidend").find("#trShareholdersData").find("tr").each(function () {
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
            $("#txtTotalAmount").val('');
        else
            $("#txtTotalAmount").val(TotalNetAmount.toFixed(2));
        if (TotalNoOfShares == 0)
            $("#txtTotalNumberOfShares").val('');
        else
            $("#txtTotalNumberOfShares").val(TotalNoOfShares);

    } catch (e) {
        console.log(e);
    }
}

function saveShareholdersDividendDetails() {
    try {

        if ($ShareHoldersFlag == 0) {
            ShowNotify('Please save Work Order Interim Dividend details first.', 'error', 3000);
            return false;

        } else {

            var classofshare = $("#ddlShareClassForInterimDividend").val();
            if (classofshare == '-1' || classofshare == 'undefined') {
                ShowNotify('Please select Class of Share.', 'error', 3000);
                return false;
            }
            else {
                var IsDivident = $('#chkDivident').is(':checked');
                var TotalNoOfShares = $('#txtTotalNumberOfShares').val();
                var DividendPerShare = $('#txtDividendPerShare').val();
                $GlobalDataInterimDividend.ClassOfShareInList == $('#txtDividendPerShare').val();
                var DividentShareCurrency = $('#ddlCurrencyForInterimDividend option:selected').val();
                var TotalNetAmount = $('#txtTotalAmount').val();
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
                        ShareDividend.SharesHeld = $(this).closest("tr").find($("[id*=txtShareHeld]")).val();
                        ShareDividend.NetCashDividend = $(this).closest("tr").find($("[id*=txtNetCashDividend]")).val();
                        ISSharesHeldNull == 'False' ? (ShareDividend.SharesHeld == '' ? ISSharesHeldNull = 'True' : 'False') : '';
                        ShareDividend.ClassofShare = classofshare;
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

                var WOTypeName = 'InterimDividend';
                var jsonText = JSON.stringify({ ShareDividendFields: ShareDividendFields, IsDivident: IsDivident, DividendPerShare: DividendPerShare, DividentShareCurrency: DividentShareCurrency, TotalNetAmount: TotalNetAmount, TotalNoOfShares: TotalNoOfShares, WOID: WOID, WOTypeName: WOTypeName, ClassofShare: classofshare });
                InterimDividendCallService("/WODI/InsertShareholdersByWOID", '', '', jsonText, false, CallbackAfterInsertShareHolders);
            }
        }
    } catch (e) {
        console.log(e);
    }
}
function CallbackAfterInsertShareHolders() {
    try {

        if ($GlobalDataInterimDividend.SavedStatus == 2) {
            ShowNotify('Please save Work Order Interim Dividend details first.', 'error', 3000);
            return false;
        }
        else {
            ShowNotify('Success.', 'success', 3000);
            BindShareHoldersDetails();
            return false;
        }
    } catch (e) {
        console.log(e);
    }
}
function BindClassOfShareAndCountry() {
    try {
        var WOID = parseInt($("#hdnWOID").val());
        CallMasterData("/PartialContent/GetShareClassDetails", 'DropDownShareClassTemplateFroIncorp', 'ddlShareClassForInterimDividend', "{'WOID':" + WOID + "}", false, "");
        CallMasterData("/PartialContent/GetCurrencyDetails", 'DropDownCurrencyTemplateFroIncorp', 'ddlCurrencyForInterimDividend', "{}", false, CurrencyCallBack);
    } catch (e) {
        console.log(e);
    }
}
function CurrencyCallBack() {
    try {
        $("#ddlCurrencyForInterimDividend").val("14");
    } catch (e) {
        console.log(e);
    }
}
function BindWOInterimDividendDetailsByWOID() {

    try {
        $GlobalDataInterimDividend.WOID = $('#hdnWOID').val();
        InterimDividendCallService("GetWOInerimDividendDetailsByWOID", "", "", "{'WOID':" + $GlobalDataInterimDividend.WOID + "}", true, BindDividendDetailsbyWOID);
    } catch (e) {
        console.log(e);
    }
}
function BindDividendDetailsbyWOID() {
    try {

        $ShareHoldersFlag = $GlobalDataInterimDividend.SavedStatus.ID;

        if ($GlobalDataInterimDividend.SavedStatus.WOID != null && $GlobalDataInterimDividend.SavedStatus.WOID != undefined && $GlobalDataInterimDividend.SavedStatus.WOID != 0) {
            $GlobalDataInterimDividend.WOInterimDividendBYWOID = $GlobalDataInterimDividend.SavedStatus;
            $("#txtFinancialPeriod").val($GlobalDataInterimDividend.WOInterimDividendBYWOID.FinancialPeriod);
            //if ($GlobalDataInterimDividend.WOInterimDividendBYWOID.ClassOfShare != 0) {
            //    $("#ddlShareClassForInterimDividend").val($GlobalDataInterimDividend.WOInterimDividendBYWOID.ClassOfShare);
            //}
            //else {
            //    $("#ddlShareClassForInterimDividend").val('-1');
            //}

            if ($GlobalDataInterimDividend.WOInterimDividendBYWOID.ClassOfShare > 0)
                $("#ddlShareClassForInterimDividend").val($GlobalDataInterimDividend.WOInterimDividendBYWOID.ClassOfShare);

            if ($GlobalDataInterimDividend.WOInterimDividendBYWOID.DividendPerShare == '0')
                $GlobalDataInterimDividend.WOInterimDividendBYWOID.DividendPerShare = '';
            $("#txtDividendPerShare").val($GlobalDataInterimDividend.WOInterimDividendBYWOID.DividendPerShare);
            $GlobalDataInterimDividend.ClassOfShareInList = $GlobalDataInterimDividend.WOInterimDividendBYWOID.DividendPerShare;
            if ($GlobalDataInterimDividend.WOInterimDividendBYWOID.Currency == '0')
                $GlobalDataInterimDividend.WOInterimDividendBYWOID.Currency = '';

            $("#ddlCurrencyForInterimDividend").val($GlobalDataInterimDividend.WOInterimDividendBYWOID.Currency);

            if ($GlobalDataInterimDividend.WOInterimDividendBYWOID.TotalAmount == '0')
                $GlobalDataInterimDividend.WOInterimDividendBYWOID.TotalAmount = '';

            $("#txtTotalAmount").val($GlobalDataInterimDividend.WOInterimDividendBYWOID.TotalAmount);
            $("#txtDateOfDeclaration").val($GlobalDataInterimDividend.WOInterimDividendBYWOID.DateOfDeclaration);
            $("#txtDateOfPayment").val($GlobalDataInterimDividend.WOInterimDividendBYWOID.DateOfPayment);
            var DirectorValue = $("#ddlDirectorChosen_DividendVoucher option[personid=" + $GlobalDataInterimDividend.WOInterimDividendBYWOID.DividendDirector + "]option[sourcecode=" + $GlobalDataInterimDividend.WOInterimDividendBYWOID.DividendSource + "]").attr("value");
            $('#ddlDirectorChosen_DividendVoucher').val(DirectorValue).trigger('chosen:updated');
            if ($GlobalDataInterimDividend.WOInterimDividendBYWOID.TotalShares == 0)
                $("#txtTotalNumberOfShares").val('');
            else
                $("#txtTotalNumberOfShares").val($GlobalDataInterimDividend.WOInterimDividendBYWOID.TotalShares);
            $("#chkDivident").prop("checked", $GlobalDataInterimDividend.WOInterimDividendBYWOID.IsDividend);

        }
        else {
            $GlobalDataInterimDividend.ClassOfShareInList = '';
        }
        

    } catch (e) {
        console.log(e);
    }
}


$("#showSaveShareHoldersDetails").click(function () {
    BindShareHoldersDetails();
    $('#divShareholdersList').show();
    $("#showSaveShareHoldersDetails").hide();
});

function BindShareHoldersDetails() {
    try {
        $GlobalDataInterimDividend.WOID = $('#hdnWOID').val();
        var ShareClassID = $('#ddlShareClassForInterimDividend').val();
        InterimDividendCallService("/WODI/GetShareholdersByWOID", 'ShareholdersTemplate', 'trShareholdersData', "{'WOID':" + $GlobalDataInterimDividend.WOID + ",'ShareClassID':" + ShareClassID + "}", true, callbackShareHolders);
    } catch (e) {
        console.log(e);
    }
}
function callbackShareHolders() {
    try {
        var LengthOfShareHolders = $("#trShareholdersData").find("tr").length;
        $(".chkSelectAllIDSH").prop("checked", false);
        if (LengthOfShareHolders >= 1) {
            $("#divShareholdersNoData").hide();
            $("#trShareholdersData").find('.DeleteShareHolders').unbind('click');
            $("#trShareholdersData").find('.DeleteShareHolders').click(DeleteShareHolders);
            $("#trShareholdersData").find('.txtShareHeld').unbind('keyup');
            $("#trShareholdersData").find('.txtShareHeld').keyup(CalculateEveryShareHeld);
            $("#trShareholdersData").find('.txtNetCashDividend').unbind('keyup');
            $("#trShareholdersData").find('.txtNetCashDividend').keyup(function (event) {

                var TotalNetAmount = 0;
                var TotalNoOfShares = 0;
                $("#tblShareHoldersDevidend").find("#trShareholdersData").find("tr").each(function () {
                    var NetCashDividend = $(this).find(".txtNetCashDividend").val();
                    var ShareHeld = $(this).find(".txtShareHeld").val();

                    if (NetCashDividend == "")
                        NetCashDividend = 0;

                    if (ShareHeld == "")
                        ShareHeld = 0;

                    TotalNetAmount = parseFloat(TotalNetAmount) + parseFloat(NetCashDividend);
                    TotalNoOfShares = parseInt(TotalNoOfShares) + parseInt(ShareHeld);

                });
                $("#txtTotalAmount").val(TotalNetAmount.toFixed(2));
                $("#txtTotalNumberOfShares").val(TotalNoOfShares);
            });

            $("#trShareholdersData").find('.chkShareholders').unbind('click').click(function () {
                var chkCheckedCount = $('#trShareholdersData').find('input.chkShareholders:checkbox:checked').length;
                var chkTotalCount = $('#trShareholdersData').find('.chkShareholders').length;
                if (chkCheckedCount != chkTotalCount) {
                    $('#chkSelectAllIDSH').prop('checked', false)
                }
                else {
                    $('#chkSelectAllIDSH').prop('checked', true)
                }
            });

        }
        else
            $("#divShareholdersNoData").show();

        if ($('#hdnWOCloseStatus').val() == 'hide') {
            $('.btnWOClose').hide();
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
                        InterimDividendCallService("/WODI/DeleteShareholdersByWOID", '', '', jsonText, false, InsertShareHoldersCallback);

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
        if ($GlobalDataInterimDividend.SavedStatus == '1') {
            ShowNotify('Success.', 'success', 2000);
            BindShareHoldersDetails();
            return false;
        }
    } catch (e) {
        console.log(e);
    }
}