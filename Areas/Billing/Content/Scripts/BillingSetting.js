$(document).ready(function () {

    SetPageAttributes('liBilling', 'Billing', 'To Manage Billing Settings', 'liBillingSettings');

    $GlobalData = {};
    $GlobalDataWOTYPE = {};
    $GlobalDataWOTYPE.IsClientOrCustomer = true;
    $GlobalData.ClientName = '';
    $GlobalData.ClientCode = '';
    $GlobalDataBilling = {};
    $GlobalDataBilling.searchText = '';
    $GlobalDataBilling.OrderBy = ''
    $GlobalDataBilling.InsertedID = 0;
    $GlobalDataBilling.startPage = 0;
    $GlobalDataBilling.resultPerPage = 10;
    $GlobalDataBilling.totalRow = 10;
    $GlobalDataBilling.BillTo = '-1';
    $GlobalDataBilling.BillingParty = '';
    $GlobalDataBilling.AccountCode = '';
    $GlobalDataBilling.ClubFee = false;
    $GlobalDataBilling.BillInArrears = false;
    $GlobalDataBilling.ClientCode = '';
    $GlobalDataBilling.ClientName
    $GlobalDataBilling.InsertedID = 0;
    $GlobalDataBilling.FeeCode = '';
    $GlobalDataBilling.BillingFrequency = '-1';
    $GlobalDataBilling.BillingMonth = '';
    $GlobalDataBilling.ClientFeeMapID = 0;
    $GlobalDataBilling.ID = 0;
    $GlobalDataBilling.Status = false;
    $GlobalDataBilling.FromDate = '';
    $GlobalDataBilling.CurrentStateOfObject = true;
    $GlobalDataBilling.GlobalFee = '';
    $GlobalDataBilling.ToDate = '';
    $GlobalDataBilling.SGD$ = '';
    $GlobalDataBilling.SelectedDate = '';
    $GlobalDataBilling.Action = '';
    $GlobalDataBilling.ClientFeeScheduleList = ''
    $GlobalDataBilling.ScheduleArrLength = 0;
    $GlobalDataBilling.MaintainFromDate = '';
    $GlobalDataBilling.SourceID = '';
    $GlobalDataBilling.FeeCall = false;
    $GlobalDataBilling.FeeCodeStatus = '';
    $GlobalDataBilling.FeeCodeForBilling = '';
    $GlobalDataBilling.SecurityDeposit = '';
    $GlobalDataBilling.SecurityDepositInvoiceNo = '';
    $GlobalDataBilling.NeedSecurityDeposit = '';
    $GlobalDataBilling.FeeDueToNominee = '';

    LoadData();
    $("#imgMappingClear").click(function () {
        try {
            $("#ddlBillingFrequency").val("-1");
            $("#chkBillInArrears").prop("checked", false);
            $("#trBillingMonth").hide();
            $("#ddlMonth").val("-1");
            $("#txtSecurityDepositInvoiceNo").val('');
            $('#txtSecurityDeposit').val('');
            $('#txtFeeDueToNominee').val('');
        } catch (e) {
            console.log(e);
        }
    });

    $("#ddlBillingFrequency").change(function () {
        try {
            $GlobalDataBilling.BillingFrequency = $("#ddlBillingFrequency").find("option:Selected").val();
            if ($GlobalDataBilling.BillingFrequency == 'SYS' || $GlobalDataBilling.BillingFrequency == 'SAS') {
                $("#ddlMonth").val("-1");
                $("#trBillingMonth").show();
            }
            else
                $("#trBillingMonth").hide();

        } catch (e) {
            console.log(e);
        }
    });

    $("#imgMappingSave").click(function () {
        try {
            $GlobalDataBilling.ClientCode = $("#divClientOne .chosen-select1").find("option:selected").attr("clientcode");
            $GlobalDataBilling.SourceID = $("#divClientOne .chosen-select1").find("option:selected").attr("sourceid");

            if ($GlobalDataBilling.ClientCode == '' || $GlobalDataBilling.ClientCode == undefined) {
                ShowNotify('Please select Client.', 'error', 3000);
                return false;
            }
            $GlobalDataBilling.FeeCode = $("#hdnFee").val();
            if ($GlobalDataBilling.FeeCode == '' || $GlobalDataBilling.FeeCode == undefined) {
                ShowNotify('Please select Fee.', 'error', 3000);
                return false;
            }
            $GlobalDataBilling.BillingFrequency = $("#ddlBillingFrequency").find("option:Selected").val();
            if ($GlobalDataBilling.BillingFrequency == '' || $GlobalDataBilling.BillingFrequency == undefined || $GlobalDataBilling.BillingFrequency == -1) {
                ShowNotify('Please select Billing Frequency.', 'error', 3000);
                return false;
            }
            var MonthStatus = false;
            if ($GlobalDataBilling.BillingFrequency == 'SYS' || $GlobalDataBilling.BillingFrequency == 'SAS') {

                $GlobalDataBilling.BillingMonth = $("#ddlMonth").find("option:Selected").val();
                MonthStatus = true;
            }
            else {
                $("#ddlMonth").val("-1");
                MonthStatus = false;
                $GlobalDataBilling.BillingMonth = '';
            }

            if (($GlobalDataBilling.BillingMonth == -1 || $GlobalDataBilling.BillingMonth == undefined) && (MonthStatus == true)) {
                ShowNotify('Please select Month.', 'error', 3000);
                return false;
            }
            $GlobalDataBilling.Status = true;
            $GlobalDataBilling.BillInArrears = $("#chkBillInArrears").prop("checked");
            var SecurityDeposit = '';
            var SecurityDepositInvoiceNo = '';
            if ($GlobalDataBilling.NeedSecurityDeposit == 'true') {
                var SecurityDeposit = $.trim($("#txtSecurityDeposit").val());
                var SecurityDepositInvoiceNo = $.trim($("#txtSecurityDepositInvoiceNo").val());
                if (SecurityDeposit == '') {
                    ShowNotify('Please enter Security Deposit.', 'error', 3000);
                    return false;
                }
                if (SecurityDepositInvoiceNo == '') {
                    ShowNotify('Please enter Security Deposit Invoice No.', 'error', 3000);
                    return false;
                }
            }

            var Feetype = $('#trFee').find('.active').find('a').attr('feetype');
            var Feeduetonominee = $.trim($('#txtFeeDueToNominee').val());
            if (Feeduetonominee == '' && (Feetype == 'ND' || Feetype == 'NS')) {
                ShowNotify('Please enter Fee Due to Nominee.', 'error', 3000);
                return false;
            }

            if ($GlobalDataBilling.SourceID != '' && $GlobalDataBilling.SourceID != undefined)
                CallBillingServices("InsertOrUpdateClientFeeMapping", "", "", "{'ClientCode':" + parseInt($GlobalDataBilling.ClientCode) + ",'SourceID':'" + $GlobalDataBilling.SourceID + "','FeeCode':'" + $GlobalDataBilling.FeeCode + "','BillingFrequency':'" + $GlobalDataBilling.BillingFrequency + "','BillingMonth':'" + $GlobalDataBilling.BillingMonth + "','IsBillArrears':" + $GlobalDataBilling.BillInArrears + ",'Status':" + $GlobalDataBilling.Status + ",'SecurityDeposit':'" + SecurityDeposit + "','SecurityDepositInvoiceNo':'" + escape($.trim(SecurityDepositInvoiceNo)) + "','FeeDueToNominee':'" + Feeduetonominee + "'}", false, FeeMappingsInsertedOrUpdatedStatus);
        }
        catch (ex) {
            console.log(ex);
        }

    });

    $("#chkClubFee").change(function () {
        try {
            $GlobalDataBilling.ClubFee = $(this).prop('checked');
            if ($GlobalDataBilling.ClubFee == true) {
                $("#divAccCode").show();
                $("#txtAccountCode").val('');
            }
            else
                $("#divAccCode").hide();

        } catch (e) {
            console.log(e);
        }
    });
    $("#btnViewGap").click(function () {
        try {
            $GlobalDataBilling.FeeCode = $("#hdnFee").val();
            if ($GlobalDataBilling.FeeCode != '' && $GlobalDataBilling.FeeCode != undefined && $GlobalDataBilling.ClientCode != '' && $GlobalDataBilling.ClientCode != undefined && $GlobalDataBilling.SourceID != undefined && $GlobalDataBilling.SourceID != '')
                CallBillingServices2("GetClientScheduleGapByFeeAndClientCode", "scriptGapScheduleDetails", "trGapBody", "{'ClientCode':" + parseInt($GlobalDataBilling.ClientCode) + ",'SourceID':'" + $GlobalDataBilling.SourceID + "','FeeCode':'" + $GlobalDataBilling.FeeCode + "'}", true, ShowGapAnalysisTable);
        }
        catch (ex) {
            console.log(ex);
        }
    });


    $("#ddlBillTo").change(function () {
        try {
            $GlobalDataBilling.BillTo = $(this).find("option:Selected").val();
            if ($GlobalDataBilling.BillTo == 1) {
                $("#divBillingParty").show();
                $("#ddlBillingParty").val('-1');
            }
            else
                $("#divBillingParty").hide();
        } catch (e) {
            console.log(e);
        }
    });

    $("#imgSettingSave").click(function () {
        try {
            $GlobalDataBilling.ClientCode = $("#divClientOne .chosen-select1").find("option:Selected").attr("clientcode");
            $GlobalDataBilling.SourceID = $("#divClientOne .chosen-select1").find("option:Selected").attr("sourceid");
            if ($GlobalDataBilling.ClientCode == '' || $GlobalDataBilling.ClientCode == undefined) {
                ShowNotify('Please select Client.', 'error', 3000);
                return false;
            }
            $GlobalDataBilling.BillTo = $("#ddlBillTo").find("option:Selected").val();
            if ($GlobalDataBilling.BillTo == -1 || $GlobalDataBilling.BillTo == undefined) {
                ShowNotify('Please select Bill To.', 'error', 3000);
                return false;
            }

            $GlobalDataBilling.BillingParty = $("#ddlBillingParty").find("option:Selected").val();
            if ($GlobalDataBilling.BillTo == "1" && $GlobalDataBilling.BillingParty == '-1') {
                ShowNotify('Please select Billing Party.', 'error', 3000);
                return false;
            }
            else if ($GlobalDataBilling.BillTo != "1") {
                $GlobalDataBilling.BillingParty = '';
            }
            $GlobalDataBilling.ClubFee = $("#chkClubFee").prop("checked");
            $GlobalDataBilling.AccountCode = $("#txtAccountCode").val();
            if ($GlobalDataBilling.ClubFee == true && ($GlobalDataBilling.AccountCode == '' || $GlobalDataBilling.AccountCode == undefined)) {
                ShowNotify('Please enter Account Code.', 'error', 3000);
                return false;
            }
            else if ($GlobalDataBilling.ClubFee == false) {
                $GlobalDataBilling.AccountCode = '';
            }
            if ($GlobalDataBilling.SourceID != '' && $GlobalDataBilling.SourceID != undefined)
                CallBillingServices("InsertOrUpdateClientFeeSettings", "", "", "{'ClientCode':" + parseInt($GlobalDataBilling.ClientCode) + ",'SourceID':'" + $GlobalDataBilling.SourceID + "','BillTo':" + $GlobalDataBilling.BillTo + ",'BillingParty':'" + $GlobalDataBilling.BillingParty + "','IsClubFee':" + $GlobalDataBilling.ClubFee + ",'AccountCode':'" + $GlobalDataBilling.AccountCode + "','Status':" + true + "}", false, FeeSettingsInsertedOrUpdatedStatus);
        }
        catch (ex) {
            console.log(ex);
        }
    });

    $("#imgSettingClear").click(function () {
        try {
            $("#ddlBillTo").val("-1");
            $("#chkClubFee").prop("checked", false);
            $("#divBillingParty").hide();
            $("#divAccCode").hide();
            $("#ddlBillingParty").val('-1');
            $("#txtAccountCode").val('');
        } catch (e) {
            console.log(e);
        }
    });

    $("#divClientOne .chosen-select1").on('change', function () {
        try {
            $GlobalDataBilling.ClientCode = $(this).find("option:selected").attr("clientcode");
            $GlobalDataBilling.SourceID = $(this).find("option:selected").attr("sourceid");
            $GlobalDataBilling.ClientName = $(this).find("option:selected").text();
            if ($GlobalDataBilling.ClientCode != undefined && $GlobalDataBilling.ClientCode != '' && $GlobalDataBilling.ClientName != '' && $GlobalDataBilling.ClientName != undefined && $GlobalDataBilling.SourceID != '' && $GlobalDataBilling.SourceID != undefined) {

                $("#spanClientName").text($GlobalDataBilling.ClientName);
                $("#divClientSettings").show()
                $("#divCMAndCS").show();
                CallBillingServices("GetMFEEforClient", "scriptFee", "trFee", "{'ClientCode':" + parseInt($GlobalDataBilling.ClientCode) + "}", true, BindEvents);
                CallBillingClentServices("GetFeeSettingAndMappingsByClientCode", "", "", "{'ClientCode':" + parseInt($GlobalDataBilling.ClientCode) + ",'SourceID':'" + $GlobalDataBilling.SourceID + "'}", '', FeeSettingAndMappingsByClientCodeCallBack);
            }
            else {
                $("#divClientSettings").hide()
                $("#divCMAndCS").hide();
                $('#btnDeleteAllFeeSettings').hide();
            }
        }

        catch (ex) {
            console.log(ex);
        }
    });

    $('#btnDeleteAllFeeSettings').click(function () {
        $("#dialog-confirm").removeClass('hide').dialog({
            resizable: false,
            modal: true,
            title_html: true,
            buttons: [
                {
                    html: "<i class='ace-icon fa fa-trash-o bigger-110'></i>&nbsp; Delete",
                    "class": "btn btn-danger btn-xs",
                    click: function () {
                        var ClientCode = $GlobalDataBilling.ClientCode;
                        var SourceID = $GlobalDataBilling.SourceID;
                        CallMasterData("DeleteAllSettingsByClient", '', '', "{'ClientCode':" + parseInt($GlobalDataBilling.ClientCode) + ",'SourceID':'" + $GlobalDataBilling.SourceID + "'}", true, CallBackDeleteAllSettings);
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
    });

    $('#imgDeleteFeeSettings').click(function () {
        $("#dialog-confirm").removeClass('hide').dialog({
            resizable: false,
            modal: true,
            title_html: true,
            buttons: [
                {
                    html: "<i class='ace-icon fa fa-trash-o bigger-110'></i>&nbsp; Delete",
                    "class": "btn btn-danger btn-xs",
                    click: function () {
                        var ClientCode = $GlobalDataBilling.ClientCode;
                        var SourceID = $GlobalDataBilling.SourceID;
                        var FeeCode = $("#hdnFee").val();
                        if (FeeCode == '' || FeeCode == undefined || FeeCode == 'undefined') {
                            ShowNotify('Please select Fee Type.', 'error', 3000);
                            return false;
                        }

                        CallMasterData("DeleteFeeSettings", '', '', "{'ClientCode':" + parseInt($GlobalDataBilling.ClientCode) + ",'SourceID':'" + $GlobalDataBilling.SourceID + "','FeeCode':'" + FeeCode + "'}", true, CallBackDeleteAllSettings);
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
    });

});

function CallBackDeleteAllSettings() {
    ShowNotify('Success.', 'success', 3000);
    $("#divClientOne .chosen-select1").trigger('change');
}

function FeeMappingsInsertedOrUpdatedStatus() {
    try {
        if ($GlobalDataBilling.InsertedID == 1) {
            ShowNotify('Success.', 'success', 3000);
            return false;
        }
    } catch (e) {
        console.log(e);
    }
}
function ShowGapAnalysisTable() {
    try {
        var tblLength = $("#gapTable").find("tr").length;
        if (tblLength <= 1) {
            $("#gapTable").hide();
            $("#dataFound").hide();
            $("#noData").show();
        }
        else {
            $("#gapTable").show();
            $("#dataFound").show();
            $("#noData").hide();
        }
        $('#modal-form').modal({
            "backdrop": "static",
            "show": "true"
        });

    } catch (e) {
        console.log(e);
    }
}
function FeeSettingsInsertedOrUpdatedStatus() {
    if ($GlobalDataBilling.InsertedID == 1) {
        ShowNotify('Success.', 'success', 3000);
        return false;
    }
}
function LoadData() {
    try {
        CallBillingServices("GetMSchedule", "scriptBillingFrequency", "ddlBillingFrequency", "{}", false, '');
        CallBillingServices("GetMMonth", "scriptMonth", "ddlMonth", "{}", false, '');
        CallBillingServices3("GetCompanyFromBillingThirdParty", "scriptBillingParty", "ddlBillingParty", "{}", false, '');

    }
    catch (ex) {
        console.log(ex);
    }
}
function BindEvents() {
    try {
        $(".Fee").unbind('click');
        $(".Fee").click(FeeEventHandling);
    }
    catch (ex) {
        console.log(ex);
    }
}
function FeeEventHandling() {
    try {
        $GlobalDataBilling.FeeCall = true;
        if ($GlobalDataBilling.ClientCode == '' || $GlobalDataBilling.ClientCode == undefined) {
            ShowNotify('Please select Client.', 'error', 3000);
            return false;
        }
        var indexVal = $(this).index();

        $(".Fee").each(function (index, val) {

            $(this).removeClass('active');
            if (index == indexVal) {
                $(this).addClass('active');
                $GlobalDataBilling.FeeCode = $(this).find("#aFee").attr("code");

                $("#hdnFee").val($GlobalDataBilling.FeeCode);
            }
            else {
                $(this).css('background-color', '#D9D9D9');
            }

        });

        //$('#txtFeeDueToNominee').val('');
        var FeeType = $(this).find("#aFee").attr("FeeType");
        if (FeeType == 'NS' || FeeType == 'ND') {
            $('#divFeeDueToNominee').show();
        }
        else {
            $('#divFeeDueToNominee').hide();
        }

        if ($GlobalDataBilling.FeeCode == $GlobalDataBilling.FeeCodeStatus)
            return false;
        else {
            $GlobalDataBilling.FeeCodeStatus = $GlobalDataBilling.FeeCode;
            CallBillingServices1("GetFeeMappingsByFeeAndClientCode", "", "", "{'FeeCode':'" + $GlobalDataBilling.FeeCode + "',ClientCode:" + parseInt($GlobalDataBilling.ClientCode) + ",'SourceID':'" + $GlobalDataBilling.SourceID + "'}", '', FeeSettingAndMappingsByClientCodeCallBack);
        }
    } catch (e) {
        console.log(e)
    }
}
function SaveScheduleDetails() {
    try {


        $GlobalDataBilling.FromDate = $(this).closest("tr").find("td:eq(0)").find(".txtFromDate").val();
        $GlobalDataBilling.ToDate = $(this).closest("tr").find("td:eq(1)").find(".txtToDate").val();
        $GlobalDataBilling.SGD$ = $(this).closest("tr").find("td:eq(2)").find(".txtSGD").val();
        $GlobalDataBilling.ID = $(this).attr("sid");

        if ($GlobalDataBilling.ClientCode == '' || $GlobalDataBilling.ClientCode == undefined || $GlobalDataBilling.ClientCode == -1 || $GlobalDataBilling.SourceID == undefined && $GlobalDataBilling.SourceID == '') {
            ShowNotify('Please select Client.', 'error', 3000);
            return false;
        }
        $GlobalDataBilling.FeeCode = $("#hdnFee").val();
        if ($GlobalDataBilling.FeeCode == '' || $GlobalDataBilling.FeeCode == undefined || $GlobalDataBilling.FeeCode == -1) {
            ShowNotify('Please select Fee.', 'error', 3000);
            return false;
        }
        if ($GlobalDataBilling.FromDate == '' || $GlobalDataBilling.FromDate == undefined) {
            ShowNotify('Please enter FromDate.', 'error', 3000);
            return false;
        }

        if ($GlobalDataBilling.ToDate == '' || $GlobalDataBilling.ToDate == undefined) {
            ShowNotify('Please enter ToDate.', 'error', 3000);
            return false;
        }
        if ($GlobalDataBilling.SGD$ == '' || $GlobalDataBilling.SGD$ == undefined || $GlobalDataBilling.SGD$ == 0) {
            ShowNotify('Please enter SGD $.', 'error', 3000);
            return false;
        }
        $GlobalDataBilling.ClientFeeMapID = $("#hdnCFMID").val();
        if ($GlobalDataBilling.ClientFeeMapID == '' || $GlobalDataBilling.ClientFeeMapID == undefined) {
            ShowNotify('Please select Fee.', 'error', 3000);
            return false;
        }

        var FromDateCheck;//= new Date(SelectedDate);
        var ToDateCheck;
        var DateSelected;
        FromDateCheck = $GlobalDataBilling.FromDate.split("/");
        DateSelected = new Date(FromDateCheck[2], FromDateCheck[1] - 1, FromDateCheck[0]);
        FromDateCheck = DateSelected;

        ToDateCheck = $GlobalDataBilling.ToDate.split("/");
        DateSelected = new Date(ToDateCheck[2], ToDateCheck[1] - 1, ToDateCheck[0]);
        ToDateCheck = DateSelected;
        if (ToDateCheck <= FromDateCheck) {
            ShowNotify('Please select valid ToDate.', 'error', 3000);
            return false;
        }

        var Current = $(this).attr("sid");
        $GlobalDataBilling.FeeCode = $("#hdnFee").val();
        $GlobalDataBilling.ClientCode = $("#divClientOne .chosen-select1").find("option:Selected").attr("clientcode");

        // var OrginalFromDate=$(this).closest("tr").find("td:eq(0)").find(".txtFromDate").attr("fdate");
        // var OrginalToDate=$(this).closest("tr").find("td:eq(1)").find(".txtToDate").attr("tdate");
        // if (OrginalFromDate != undefined && OrginalToDate != undefined) {
        $GlobalDataBilling.Action = "ToDate";
        var ActionOnClientSchedule = filterByDateAndCodes1($GlobalDataBilling.ClientFeeScheduleList, $GlobalDataBilling.FeeCode, $GlobalDataBilling.ClientCode, $GlobalDataBilling.ToDate, $GlobalDataBilling.FromDate, Current);
        $GlobalDataBilling.ScheduleArrLength = ActionOnClientSchedule.length;

        if ($GlobalDataBilling.ScheduleArrLength > 0) {
            ShowNotify('Please select the valid Date range.', 'error', 3000);
            $GlobalDataBilling.CurrentStateOfObject = false
            return false;
        }
        else {
            $GlobalDataBilling.CurrentStateOfObject = true
        }
        // }
        if ($GlobalDataBilling.ID != '' && $GlobalDataBilling.ID != undefined)
            CallBillingServices("InsertOrUpdateClientFeeSchedule", "", "", "{'ID':" + $GlobalDataBilling.ID + ",'CFMID':" + $GlobalDataBilling.ClientFeeMapID + ",'ClientCode':" + parseInt($GlobalDataBilling.ClientCode) + ",'SourceID':'" + $GlobalDataBilling.SourceID + "','FeeCode':'" + $GlobalDataBilling.FeeCode + "','FromDate':'" + $GlobalDataBilling.FromDate + "','ToDate':'" + $GlobalDataBilling.ToDate + "','Amount':" + $GlobalDataBilling.SGD$ + ",'Status':" + true + "}", "", InsertOrUpdateClientFeeScheduleCallBack);

    } catch (e) {
        console.log(e);
    }
}



function filterByDateAndCodes1(ActionData, FeeCode, ClientCode, SelectedDate, SelFromDate, Current) {
    try {


        return $.grep(ActionData, function (n, i) {
            var SelParseDate;//= new Date(SelectedDate);
            var FromParseDate;// = new Date(n.FromDate);
            SelDate = SelectedDate.split("/");
            DateSelected = new Date(SelDate[2], SelDate[1] - 1, SelDate[0]);
            SelParseDate = DateSelected;

            FromD = n.FromDate.split("/");
            ToDateSelected = new Date(FromD[2], FromD[1] - 1, FromD[0]);
            FromParseDate = ToDateSelected;

            var ToParseDate; //= new Date(n.ToDate);


            ToParseD = n.ToDate.split("/");
            ToParseDate = new Date(ToParseD[2], ToParseD[1] - 1, ToParseD[0]);
            ToParseDate = ToParseDate;


            var ParseMaintainFromDate;// = new Date($GlobalDataBilling.MaintainFromDate);

            ParseMaintain = SelFromDate.split("/");
            ParseDate = new Date(ParseMaintain[2], ParseMaintain[1] - 1, ParseMaintain[0]);
            ParseMaintainFromDate = ParseDate;
            if ($GlobalDataBilling.Action == 'FromDate' && Current != n.ID)
                return (n.ClientCode == ClientCode && n.FeeCode == FeeCode && (SelParseDate >= FromParseDate && SelParseDate <= ToParseDate));
            else if ($GlobalDataBilling.Action == 'ToDate' && Current != n.ID)
                return (n.ClientCode == ClientCode && n.FeeCode == FeeCode && ((FromParseDate > ParseMaintainFromDate && FromParseDate < SelParseDate)
                    || (ToParseDate > ParseMaintainFromDate && ToParseDate < SelParseDate)));


        });

    } catch (e) {
        console.log(e);
    }
}

function InsertOrUpdateClientFeeScheduleCallBack() {
    try {
        if ($GlobalDataBilling.InsertedID == 1) {
            ShowNotify('Success.', 'success', 3000);
            BindScheduleDataCall();
            return false;
        }
        else if ($GlobalDataBilling.InsertedID == -1) {
            ShowNotify('Please insert Client Fee Setting and Mapping details.', 'error', 3000);
            return false;
        }
    } catch (e) {
        console.log(e);
    }
}
function BindScheduleDataCall() {
    try {
        CallBillingServices1("GetClientFeeScheduleByClientAndFeeCode", "scriptScheduleDetails", "trBody", "{'FeeCode':'" + $GlobalDataBilling.FeeCode + "','ClientCode':" + parseInt($GlobalDataBilling.ClientCode) + ",'SourceID':'" + $GlobalDataBilling.SourceID + "'}", true, BindScheduleDataCallBack);
    } catch (e) {
        console.log(e);
    }
}
function CancelScheduleDetails() {
    try {
        $GlobalDataBilling.ID = $(this).attr("sid");
        $GlobalDataBilling.FeeCode = $("#hdnFee").val();
        if (($GlobalDataBilling.ID != '' && $GlobalDataBilling.ID != undefined) && $GlobalDataBilling.ID != 0) {
            DialogForDelete();

        }
        if ($GlobalDataBilling.ID == 0) {
            $(this).closest("tr").find("td:eq(0)").find(".txtFromDate").val('');
            $(this).closest("tr").find("td:eq(1)").find(".txtToDate").val('');
            $(this).closest("tr").find("td:eq(2)").find(".txtSGD").val('');
            return false;
        }

    } catch (e) {
        console.log(e);
    }
}
function DialogForDelete() {
    try {
        $("#dialog-confirm").removeClass('hide').dialog({
            resizable: false,
            modal: true,
            title_html: true,
            buttons: [
                {
                    html: "<i class='ace-icon fa fa-trash-o bigger-110'></i>&nbsp; Delete item",
                    "class": "btn btn-danger btn-xs",
                    click: function () {

                        CallBillingServices("DeleteClientScheduleByID", "", "", "{'ID':'" + $GlobalDataBilling.ID + "'}", true, BindScheduleDataCall);
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
function BindScheduleDataCallBack() {
    try {
        $(".imgSaveScheduleInGrid").unbind('click');
        $(".imgSaveScheduleInGrid").click(SaveScheduleDetails);
        $(".imgCancelScheduleInGrid").unbind('click');
        $(".imgCancelScheduleInGrid").click(CancelScheduleDetails);
        $(".txtFromDate").unbind('datepicker');
        $(".txtToDate").unbind('datepicker');


        $(".txtFromDate").datepicker({
            showOtherMonths: true,
            selectOtherMonths: true,
            showWeek: true,
            changeYear: true,
            changeMonth: true,
            yearRange: "-10:+50",
            dateFormat: 'dd/mm/yy',
            onSelect: function (dateText, inst) {
                try {
                    if (dateText != '' && dateText != undefined) {
                        var dtFromDate = new Date(dateText);
                        var selectedDay = dtFromDate.getDate();
                        from = dateText.split("/");
                        DateSelected = new Date(from[2], from[1] - 1, from[0]);

                        var Indexoftr = $(this).closest("tr").index();

                        //  var dtFromDate = new Date(dateText);
                        var selectedDay = DateSelected.getDate();

                        if (selectedDay != 1 && selectedDay != 16 && Indexoftr != 0) {
                            ShowNotify('Please select From Date either 1 or 16.', 'error', 3000);
                            var fDate = $(this).attr("fdate")
                            if (fDate != undefined)
                                $(this).val(fDate);
                            else $(this).val('');
                            return false;
                        }
                        $GlobalDataBilling.Action = 'FromDate';
                        var currentRec = $(this).closest("tr").find(".imgSaveScheduleInGrid").attr("sid");
                        FromAndToDateAttributes(dateText, currentRec);
                    }
                    if ($GlobalDataBilling.CurrentStateOfObject == false) {
                        var fDate = $(this).attr("fdate")
                        if (fDate != undefined)
                            $(this).val(fDate);
                        else $(this).val('');
                    }
                    return false;
                }
                catch (ex) {
                    console.log(ex);
                }
            }

        });

        $(".txtToDate").datepicker({
            showOtherMonths: true,
            selectOtherMonths: true,
            showWeek: true,
            changeYear: true,
            changeMonth: true,
            yearRange: "-10:+50",
            dateFormat: 'dd/mm/yy',
            onSelect: function (dateText, inst) {
                try {
                    if (dateText != '' && dateText != undefined) {
                        var SelectedFromDate = $(this).closest("tr").find(".txtFromDate").val();
                        $GlobalDataBilling.MaintainFromDate = SelectedFromDate;
                        if (SelectedFromDate == '' || SelectedFromDate == undefined) {
                            ShowNotify('Please select From Date.', 'error', 3000);
                            var tDate = $(this).attr("tdate")
                            if (tDate != undefined)
                                $(this).val(tDate);
                            else $(this).val('');
                            return false;
                        }
                        var GetDay = new Date(dateText);
                        var dtToDate = new Date(dateText);

                        var dtFromDate = new Date(SelectedFromDate);


                        if (dtToDate < dtFromDate) {
                            ShowNotify('Please select valid To Date.', 'error', 3000);
                            var tDate = $(this).attr("tdate")
                            if (tDate != undefined)
                                $(this).val(tDate);
                            else $(this).val('');
                            return false;
                        }

                        //var selectedDay = GetDay.getDate();
                        //var SelectedYear = GetDay.getFullYear();
                        //var IsLeap = isleap(SelectedYear);
                        //var SelMonth = GetDay.getMonth();


                        ToD = dateText.split("/");
                        ToDateSelected = new Date(ToD[2], ToD[1] - 1, ToD[0]);

                        //  var dtFromDate = new Date(dateText);
                        var selectedDay = ToDateSelected.getDate();


                        // var selectedDay = GetDay.getDate();
                        var SelectedYear = ToDateSelected.getFullYear();
                        var IsLeap = isleap(SelectedYear);
                        var SelMonth = ToDateSelected.getMonth();





                        if (IsLeap == true) {
                            if (SelMonth + 1 == 2 && selectedDay != 15 && selectedDay != 29) {

                                ShowNotify('Please select To Date either 15 or 29.', 'error', 3000);
                                var tDate = $(this).attr("tdate")
                                if (tDate != undefined)
                                    $(this).val(tDate);
                                else $(this).val('');
                                return false;

                            }
                        }
                        else {
                            if (SelMonth + 1 == 2 && selectedDay != 15 && selectedDay != 28) {

                                ShowNotify('Please select ToDate either 15 or 28.', 'error', 3000);
                                var tDate = $(this).attr("tdate")
                                if (tDate != undefined)
                                    $(this).val(tDate);
                                else $(this).val('');
                                return false;

                            }
                        }


                        //if (selectedDay != 15 && selectedDay != 30 && selectedDay != 31 && SelMonth + 1 != 2) {
                        //    ShowNotify('Please select To Date either 15 or 30(31).', 'error', 3000);
                        //    var tDate = $(this).attr("tdate")
                        //    if (tDate != undefined)
                        //        $(this).val(tDate);
                        //    else $(this).val('');
                        //    return false;
                        //}

                        var Month = SelMonth + 1;

                        if ((Month == '4' || Month == '6' || Month == '9' || Month == '11') && (Month != 2) && ((selectedDay != 15) && (selectedDay != 30))) {
                            ShowNotify('Please select To Date either 15 or 30.', 'error', 3000);

                            $(this).val('');
                            return false;
                        }
                        else if ((Month == '1' || Month == '3' || Month == '5' || Month == '7' || Month == '8' || Month == '10' || Month == '12') && (Month != 2) && ((selectedDay != 15) && (selectedDay != 31))) {
                            ShowNotify('Please select To Date either 15 or 31.', 'error', 3000);
                            $(this).val('');
                            return false;
                        }



                        $GlobalDataBilling.Action = 'ToDate';
                        var currentRec = $(this).closest("tr").find(".imgSaveScheduleInGrid").attr("sid");
                        FromAndToDateAttributes(dateText, currentRec);
                    }
                    if ($GlobalDataBilling.CurrentStateOfObject == false) {
                        var tDate = $(this).attr("tdate")
                        if (tDate != undefined)
                            $(this).val(tDate);
                        else $(this).val('');
                    }
                    return false;
                }
                catch (ex) {
                    console.log(ex);
                }
            }

        });

        //TerminatePickerWithRespectiveToDate();

    } catch (e) {
        console.log(e);
    }
}
function isleap(year) {
    try {
        var isLeap = false;
        if (year % 400 == 0 || (year % 100 != 0 && year % 4 == 0)) {
            isLeap = true;
        }
        else {
            isLeap = false;
        }
        return isLeap;

    } catch (e) {
        console.log(e);
    }
}
function TerminatePickerWithRespectiveToDate() {

    $("#trBody").find("tr:not(:last)").each(function (index) {
        try {
            var tDate = $GlobalDataBilling.ClientFeeScheduleList[index].TDateStatus;
            if (tDate == false) {
                $(this).find(".txtFromDate").attr("disabled", true);
                $(this).find(".txtToDate").attr("disabled", true);
                $(this).find(".txtSGD").attr("disabled", true);
            }
        }

        catch (ex) {
            console.log(ex);
        }

    });
}
function FromAndToDateAttributes(selectedDate, Current) {
    try {
        $GlobalDataBilling.SelectedDate = selectedDate;
        $GlobalDataBilling.FeeCode = $("#hdnFee").val();
        $GlobalDataBilling.ClientCode = $("#divClientOne .chosen-select1").find("option:Selected").attr("clientcode");
        var ActionOnClientSchedule = filterByDateAndCodes($GlobalDataBilling.ClientFeeScheduleList, $GlobalDataBilling.FeeCode, $GlobalDataBilling.ClientCode, $GlobalDataBilling.SelectedDate, Current);
        $GlobalDataBilling.ScheduleArrLength = ActionOnClientSchedule.length;

        if ($GlobalDataBilling.ScheduleArrLength > 0) {
            ShowNotify('Please select the valid Date range.', 'error', 3000);
            $GlobalDataBilling.CurrentStateOfObject = false
        }
        else {
            $GlobalDataBilling.CurrentStateOfObject = true
        }
    } catch (e) {
        console.log(e);
    }
}

function filterByDateAndCodes(ActionData, FeeCode, ClientCode, SelectedDate, Current) {
    try {


        return $.grep(ActionData, function (n, i) {
            var SelParseDate;//= new Date(SelectedDate);
            var FromParseDate;// = new Date(n.FromDate);
            SelDate = SelectedDate.split("/");
            DateSelected = new Date(SelDate[2], SelDate[1] - 1, SelDate[0]);
            SelParseDate = DateSelected;

            FromD = n.FromDate.split("/");
            ToDateSelected = new Date(FromD[2], FromD[1] - 1, FromD[0]);
            FromParseDate = ToDateSelected;

            var ToParseDate; //= new Date(n.ToDate);


            ToParseD = n.ToDate.split("/");
            ToParseDate = new Date(ToParseD[2], ToParseD[1] - 1, ToParseD[0]);
            ToParseDate = ToParseDate;


            var ParseMaintainFromDate;// = new Date($GlobalDataBilling.MaintainFromDate);

            ParseMaintain = $GlobalDataBilling.MaintainFromDate.split("/");
            ParseDate = new Date(ParseMaintain[2], ParseMaintain[1] - 1, ParseMaintain[0]);
            ParseMaintainFromDate = ParseDate;
            if ($GlobalDataBilling.Action == 'FromDate' && Current != n.ID)
                return (n.ClientCode == ClientCode && n.FeeCode == FeeCode && (SelParseDate >= FromParseDate && SelParseDate <= ToParseDate));
            else if ($GlobalDataBilling.Action == 'ToDate' && Current != n.ID)
                return (n.ClientCode == ClientCode && n.FeeCode == FeeCode && ((FromParseDate > ParseMaintainFromDate && FromParseDate < SelParseDate)
                    || (ToParseDate > ParseMaintainFromDate && ToParseDate < SelParseDate)));


        });

    } catch (e) {
        console.log(e);
    }
}
function FeeSettingAndMappingsByClientCodeCallBack() {
    try {
        $('#btnDeleteAllFeeSettings').show();

        $("#ddlBillTo").val($GlobalDataBilling.BillTo);
        if ($GlobalDataBilling.BillTo == 0 || $GlobalDataBilling.BillTo == '-1') {
            $("#ddlBillingParty").val('-1');
            $("#divBillingParty").hide();
        }
        else {
            $("#ddlBillingParty").val($GlobalDataBilling.BillingParty);
            $("#divBillingParty").show();
        }

        if ($GlobalDataBilling.ClubFee == true) {
            $("#txtAccountCode").val($GlobalDataBilling.AccountCode);
            $("#chkClubFee").prop("checked", true);
            $("#divAccCode").show();

        }
        else {
            $("#divAccCode").hide();
            $("#txtAccountCode").val('');
            $("#chkClubFee").prop("checked", false);
        }
        if ($GlobalDataBilling.BillInArrears == true)
            $("#chkBillInArrears").prop("checked", true);
        else $("#chkBillInArrears").prop("checked", false);

        $("#ddlBillingFrequency").val($GlobalDataBilling.BillingFrequency);


        if ($GlobalDataBilling.BillingFrequency == 'SYS' || $GlobalDataBilling.BillingFrequency == 'SAS') {
            $("#ddlMonth").val($GlobalDataBilling.BillingMonth);
            $("#trBillingMonth").show();

        }
        else {
            $("#trBillingMonth").hide();
            $("#ddlMonth").val("-1");
        }

        $("#chkStatus").prop("checked", $GlobalDataBilling.Status == "1" ? true : false);
        if ($GlobalDataBilling.FeeCode == '' && $GlobalDataBilling.FeeCall == true) {
            $GlobalDataBilling.FeeCode = $("#hdnFee").val();
        }
        if ($GlobalDataBilling.FeeCode == '') {
            $GlobalDataBilling.FeeCode = $("#trFee").find("li:first").find("#aFee").attr("code");

            $GlobalDataBilling.ClientFeeMapID = $("#trFee").find("li:first").find("#aFee").attr("feeid");
        }
        else $GlobalDataBilling.FeeCodeForBilling = $GlobalDataBilling.FeeCode;

        $(".Fee").each(function (index, val) {
            var CurrentFeeCode = $(this).find("#aFee").attr("code");
            if (CurrentFeeCode == $GlobalDataBilling.FeeCode) {
                $(this).addClass('active');
                var CFMID = $(this).find("#aFee").attr("feeid");
                $GlobalDataBilling.NeedSecurityDeposit = $(this).find("#aFee").attr("needsecuritydeposit");
                $("#hdnCFMID").val(CFMID);
                $("#hdnFee").val($GlobalDataBilling.FeeCode);
                $("#hdnNeedSecurityDeposit").val($GlobalDataBilling.NeedSecurityDeposit);

                $('#txtFeeDueToNominee').val('');
                var FeeType = $(this).find("#aFee").attr("FeeType");
                if (FeeType == 'NS' || FeeType == 'ND') {
                    $('#divFeeDueToNominee').show();
                }
                else {
                    $('#divFeeDueToNominee').hide();
                }
            }

            else {
                $(this).removeClass('active');
            }

        });
        $("#txtSecurityDeposit").val('');
        $("#txtSecurityDepositInvoiceNo").val('');
        if ($GlobalDataBilling.NeedSecurityDeposit == 'true') {
            $("#divSecurityDeposit").show();
            $("#divSecurityDepositInvoiceNo").show();
            if ($GlobalDataBilling.SecurityDeposit == 0)
                $("#txtSecurityDeposit").val('');
            else
                $("#txtSecurityDeposit").val($GlobalDataBilling.SecurityDeposit);
            $("#txtSecurityDepositInvoiceNo").val($GlobalDataBilling.SecurityDepositInvoiceNo)
        }
        else {
            $("#divSecurityDeposit").hide();
            $("#divSecurityDepositInvoiceNo").hide();
        }

        $('#txtFeeDueToNominee').val($GlobalDataBilling.FeeDueToNominee);

        $GlobalDataBilling.FeeCall = false;
        BindScheduleDataCall();
    }
    catch (ex) {
        console.log(ex);
    }
}
function CallBillingServices(path, templateId, containerId, parameters, clearContent, callBack) {
    ShowLoadNotify();
    try {
        $.ajax({
            type: "POST",
            url: path,
            data: parameters,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (msg) {
                if (msg == '0') {
                    ShowNotify('Invalid session login again.', 'error', 3000);
                    return false;
                }
                $GlobalDataBilling.InsertedID = msg;

                if (msg.BillingList != null && msg.BillingList != 'undefined') {

                    $GlobalDataBilling.totalRow = msg.BillingCount;
                }

                if (templateId != '' && containerId != '') {

                    if (!clearContent) {
                        $.tmpl($('#' + templateId).html(), msg.BillingList).appendTo("#" + containerId);
                    }
                    else {
                        $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg.BillingList));
                    }
                }

                if (callBack != undefined && callBack != '')
                    callBack();

            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw new Error(xhr.statusText);
            }
        });

    } catch (e) {
        console.log(e);
    }
    HideLoadNotify();
}
function CallBillingServices1(path, templateId, containerId, parameters, clearContent, callBack) {
    ShowLoadNotify();
    try {
        $.ajax({
            type: "POST",
            url: path,
            data: parameters,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (msg) {
                if (msg == '0') {
                    ShowNotify('Invalid session login again.', 'error', 3000);
                    return false;
                }
                if (msg.ClientFeeSettingList[0] != null && msg.ClientFeeSettingList[0] != undefined) {
                    $GlobalDataBilling.BillTo = msg.ClientFeeSettingList[0].BillTo;
                    $GlobalDataBilling.BillingParty = msg.ClientFeeSettingList[0].BillingPartyCode;
                    $GlobalDataBilling.ClubFee = msg.ClientFeeSettingList[0].IsClubFee;
                    $GlobalDataBilling.AccountCode = msg.ClientFeeSettingList[0].AccountCode;

                }

                if (msg.ClientFeeMappingList[0] != null && msg.ClientFeeMappingList[0] != undefined) {

                    $GlobalDataBilling.BillingFrequency = msg.ClientFeeMappingList[0].BillingFrequency;
                    $GlobalDataBilling.BillingMonth = msg.ClientFeeMappingList[0].BillingMonth;
                    $GlobalDataBilling.Status = msg.ClientFeeMappingList[0].Status;
                    $GlobalDataBilling.FeeCode = msg.ClientFeeMappingList[0].FeeCode;
                    //  $GlobalDataBilling.ClientFeeMapID = msg.ClientFeeMappingList[0].CFMID;
                    $GlobalDataBilling.BillInArrears = msg.ClientFeeMappingList[0].IsBillArrears;
                    $GlobalDataBilling.SecurityDeposit = msg.ClientFeeMappingList[0].SecurityDeposit;
                    $GlobalDataBilling.SecurityDepositInvoiceNo = msg.ClientFeeMappingList[0].SecurityDepositInvoiceNo;
                    $GlobalDataBilling.FeeDueToNominee = msg.ClientFeeMappingList[0].FeeDueToNominee;
                }
                else {
                    $GlobalDataBilling.BillingFrequency = '-1';
                    $GlobalDataBilling.BillingMonth = '-1';
                    $GlobalDataBilling.Status = false;
                    $GlobalDataBilling.FeeCode = '';
                    //  $GlobalDataBilling.ClientFeeMapID = 0;
                    $GlobalDataBilling.BillInArrears = false;
                    $GlobalDataBilling.SecurityDeposit = '';
                    $GlobalDataBilling.SecurityDepositInvoiceNo = '';
                    $GlobalDataBilling.FeeDueToNominee = '';
                }
                if (templateId != '' && containerId != '') {


                    if (!clearContent) {
                        $.tmpl($('#' + templateId).html(), msg.ClientFeeScheduleList).appendTo("#" + containerId);
                    }
                    else {
                        $GlobalDataBilling.ClientFeeScheduleList = msg.ClientFeeScheduleList;
                        $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg.ClientFeeScheduleList));

                        var ScheduleFromDate = "<tr id='trAddAchedule'><td><input type='text'  readonly='true' class='txtFromDate special' style='width: 110px;' /></td>";
                        var ScheduleToDate = "<td><input type='text' class='txtToDate special' readonly='true' style='width: 110px;' /></td>";
                        var ScheduleSGD = "<td><input type='text' class='txtSGD special DisableCutCopyPaste' style='width: 100%;' onkeypress='return AllowDecimalNumbersOnly(this,event,17,2)' /></td>";
                        var ScheduleCancelAction = "<button class='imgSaveScheduleInGrid btn btn-xs btn-success'  sid='0' ><i class='ace-icon fa fa-check bigger-120'></i></button>";
                        var ScheduleActions = "<td>" + ScheduleCancelAction + " <button class='imgCancelScheduleInGrid btn btn-xs btn-danger' sid='0' ><i class='ace-icon fa fa-trash-o bigger-120'></i></button></td></tr>";
                        var AppendScheduleToAddNewRecord = ScheduleFromDate + ScheduleToDate + ScheduleSGD + ScheduleActions;
                        $("#" + containerId).append(AppendScheduleToAddNewRecord);
                    }
                    $("#tblScheduleData").show();
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
    HideLoadNotify();
}
function CallBillingClentServices(path, templateId, containerId, parameters, clearContent, callBack) {
    ShowLoadNotify();
    try {
        $.ajax({
            type: "POST",
            url: path,
            data: parameters,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (msg) {
                if (msg == '0') {
                    ShowNotify('Invalid session login again.', 'error', 3000);
                    return false;
                }
                if (msg.ClientFeeSettingList[0] != null && msg.ClientFeeSettingList[0] != undefined) {
                    $GlobalDataBilling.BillTo = msg.ClientFeeSettingList[0].BillTo;
                    $GlobalDataBilling.BillingParty = msg.ClientFeeSettingList[0].BillingPartyCode;
                    $GlobalDataBilling.ClubFee = msg.ClientFeeSettingList[0].IsClubFee;
                    $GlobalDataBilling.AccountCode = msg.ClientFeeSettingList[0].AccountCode;

                }
                else {
                    $GlobalDataBilling.BillTo = '-1';
                    $GlobalDataBilling.BillingParty = '';
                    $GlobalDataBilling.ClubFee = false
                    $GlobalDataBilling.AccountCode = '';
                }

                if (msg.ClientFeeMappingList[0] != null && msg.ClientFeeMappingList[0] != undefined) {

                    $GlobalDataBilling.BillingFrequency = msg.ClientFeeMappingList[0].BillingFrequency;
                    $GlobalDataBilling.BillingMonth = msg.ClientFeeMappingList[0].BillingMonth;
                    $GlobalDataBilling.Status = msg.ClientFeeMappingList[0].Status;
                    $GlobalDataBilling.FeeCode = msg.ClientFeeMappingList[0].FeeCode;
                    // $GlobalDataBilling.ClientFeeMapID = msg.ClientFeeMappingList[0].CFMID;
                    $GlobalDataBilling.BillInArrears = msg.ClientFeeMappingList[0].IsBillArrears;
                    $GlobalDataBilling.SecurityDeposit = msg.ClientFeeMappingList[0].SecurityDeposit;
                    $GlobalDataBilling.SecurityDepositInvoiceNo = msg.ClientFeeMappingList[0].SecurityDepositInvoiceNo;
                    $GlobalDataBilling.FeeDueToNominee = msg.ClientFeeMappingList[0].FeeDueToNominee;

                }
                else {
                    $GlobalDataBilling.BillingFrequency = '-1';
                    $GlobalDataBilling.BillingMonth = '-1';
                    $GlobalDataBilling.Status = false;
                    $GlobalDataBilling.FeeCode = '';
                    //$GlobalDataBilling.ClientFeeMapID = 0;
                    $GlobalDataBilling.BillInArrears = false;
                    $GlobalDataBilling.SecurityDeposit = '';
                    $GlobalDataBilling.SecurityDepositInvoiceNo = '';
                    $GlobalDataBilling.FeeDueToNominee = '';
                }
                if (templateId != '' && containerId != '') {


                    if (!clearContent) {
                        $.tmpl($('#' + templateId).html(), msg.ClientFeeScheduleList).appendTo("#" + containerId);
                    }
                    else {
                        $GlobalDataBilling.ClientFeeScheduleList = msg.ClientFeeScheduleList;
                        $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg.ClientFeeScheduleList));

                        var ScheduleFromDate = "<tr id='trAddAchedule'><td><input type='text'  readonly='true' class='txtFromDate' style='width: 110px;' /></td>";
                        var ScheduleToDate = "<td><input type='text' class='txtToDate' readonly='true' style='width: 110px;' /></td>";
                        var ScheduleSGD = "<td><input type='text' class='txtSGD DisableCutCopyPaste special' style='width: 100%;'  onkeypress='return AllowDecimalNumbersOnly(this,event,17,2)' /></td>";
                        var ScheduleCancelAction = "<button class='imgSaveScheduleInGrid btn btn-xs btn-success'  sid='0' ><i class='ace-icon fa fa-check bigger-120'></i></button>";
                        var ScheduleActions = "<td>" + ScheduleCancelAction + " <button class='imgCancelScheduleInGrid btn btn-xs btn-danger' sid='0' ><i class='ace-icon fa fa-trash-o bigger-120'></i></button></td></tr>";
                        var AppendScheduleToAddNewRecord = ScheduleFromDate + ScheduleToDate + ScheduleSGD + ScheduleActions;
                        $("#" + containerId).append(AppendScheduleToAddNewRecord);
                    }
                    $("#tblScheduleData").show();
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
    HideLoadNotify();
}
function CallBillingServices2(path, templateId, containerId, parameters, clearContent, callBack) {
    ShowLoadNotify();
    try {
        $.ajax({
            type: "POST",
            url: path,
            data: parameters,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (msg) {
                if (msg == '0') {
                    ShowNotify('Invalid session login again.', 'error', 3000);
                    return false;
                }
                var GapscheduleLength = msg.ClientFeeScheduleList.length;

                if (templateId != '' && containerId != '') {

                    if (!clearContent) {
                        $.tmpl($('#' + templateId).html(), msg.ClientFeeScheduleList).appendTo("#" + containerId);
                    }
                    else {
                        $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg.ClientFeeScheduleList));
                    }
                }

                if (callBack != undefined && callBack != '')
                    callBack();
            }

        });

    } catch (e) {
        console.log(e);
    }
    HideLoadNotify();
}
function CallBillingServices3(path, templateId, containerId, parameters, clearContent, callBack) {
    ShowLoadNotify();
    try {
        $.ajax({
            type: "POST",
            url: path,
            data: parameters,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (msg) {
                if (msg == '0') {
                    ShowNotify('Invalid session login again.', 'error', 3000);
                    return false;
                }

                if (templateId != '' && containerId != '') {

                    if (!clearContent) {
                        $.tmpl($('#' + templateId).html(), msg.ClientFeeSettingList).appendTo("#" + containerId);
                    }
                    else {
                        $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg.ClientFeeSettingList));
                    }
                }

                if (callBack != undefined && callBack != '')
                    callBack();
            }

        });

    } catch (e) {
        console.log(e);
    }
    HideLoadNotify();
}