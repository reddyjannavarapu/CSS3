$(document).ready(function () {
    $GlobalDataDL = {};
    $GlobalDataDL.WOID = ''
    $GlobalDataDL.startPage = 0
    $GlobalDataDL.rowsperpage = 10;
    $GlobalDataDL.totalRow = 0
    $GlobalDataDL.resultPerPage = 50;
    $GlobalDataDL.FromDate = '';
    $GlobalDataDL.ToDate = '';
    $GlobalDataDL.clientName = ''
    $GlobalDataDL.clientID = ''
    $GlobalDataDL.SourceID = ''
    $GlobalDataDL.venderRefId = ''
    $GlobalDataDL.Type = ''
    $GlobalDataDL.IsVerified = ''
    $GlobalDataDL.IsBilled = ''
    $GlobalDataDL.IsArchived = ''
    $GlobalDataDL.IsAdhoc = ''
    $GlobalDataDL.OrderBy = ''
    $GlobalDataDL.InsertedID = ''
    $GlobalData.DIInState;
    $GlobalData.ActionRule;
    $GlobalData.Action = '';

    $GlobalDataDL.ACCPACStatus = 0;

    var txtInHouseDescription = $('#txtInhouseComments');
    textareaLimiter(txtInHouseDescription, 500);

    function LoadCall(clientName, WOID, venderRefId, Type, IsVerified, IsBilled, IsArchived, IsAdhoc, OrderBy, startpage, rowsperpage) {
        try {
            PartialAjax("GetSearchDisbursementItemsData", 'ScriptDisbursementItems', 'trDisbursementItemsData', "{'clientName':'" + clientName + "','clientID':'" + clientID + "','WO':'" + WOID + "','venderRefId':'" + venderRefId + "','Type':'" + Type + "','IsVerified':'" + IsVerified + "','IsBilled':'" + IsBilled + "','IsArchived':'" + IsArchived + "','IsAdhoc':'" + IsAdhoc + "','OrderBy':'" + OrderBy + "','startpage':" + startpage + ",'rowsperpage':" + rowsperpage + "}", true, LoadCallBack);
        } catch (e) {
            console.log(e);
        }
    }

    function CheckQueryString() {
        try {
            var queryString = [];
            if (window.location.search.split('?').length > 1) {
                var params = window.location.search.split('?')[1].split('&');
                for (var i = 0; i < params.length; i++) {
                    var key = params[i].split('=')[0];
                    var value = decodeURIComponent(params[i].split('=')[1]);
                    queryString[key] = value;
                }

                var WorkOrderID = queryString["ID"]
                var IsInteger = $.isNumeric(WorkOrderID);
                if (IsInteger == false)
                    return false;
                $("#hdnWorkOrderID").val(WorkOrderID);

            }
        }
        catch (ex) {
            console.log(ex);
        }
    }
    $('#btnSendToACCPACForADHOCBilling').click(function () {
        try {
            var disbursementids = GetIDsOfCheckedDI();
            if (disbursementids == '' || disbursementids == undefined) {
                ShowNotify('Please select at least one Disbursement Item.', 'error', 3000);
                return false;
            }
            else {
                $GlobalDataDL.IsAdhoc = true;
                $GlobalData.Action = 'AD';
                AdhocActionOnDisbursementItems(disbursementids, $GlobalDataDL.IsAdhoc, $GlobalData.Action);

            }
        }
        catch (ex) {
            console.log(ex);
        }
    });

    $('#btnRemoveFromACCPACForADHOCBilling').click(function () {
        try {
            var disbursementids = GetIDsOfCheckedDI();
            if (disbursementids == '' || disbursementids == undefined) {
                ShowNotify('Please select at least one Disbursement Item.', 'error', 3000);
                return false;
            }
            else {
                $GlobalDataDL.IsAdhoc = false;
                $GlobalData.Action = 'AD';
                AdhocActionOnDisbursementItems(disbursementids, $GlobalDataDL.IsAdhoc, $GlobalData.Action);
            }
        }
        catch (ex) {
            console.log(ex);
        }
    });

    $('#btnListARCHIVE').click(function () {
        try {
            var disbursementids = GetIDsOfCheckedDI();
            if (disbursementids == '' || disbursementids == undefined) {
                ShowNotify('Please select at least one Disbursement Item.', 'error', 3000);
                return false;
            }
            else {
                $GlobalDataDL.IsArchived = true;
                $GlobalData.Action = 'AR';
                ArchivedActionOnDisbursementItems(disbursementids, $GlobalDataDL.IsArchived, $GlobalData.Action);
            }
        }
        catch (ex) {
            console.log(ex);
        }
    });

    $('#btnListUnARCHIVE').click(function () {
        try {
            var disbursementids = GetIDsOfCheckedDI();
            if (disbursementids == '' || disbursementids == undefined) {
                ShowNotify('Please select at least one Disbursement Item.', 'error', 3000);
                return false;
            }
            else {
                $GlobalDataDL.IsArchived = false;
                $GlobalData.Action = 'AR';
                UnArchivedActionOnDisbursementItems(disbursementids, $GlobalDataDL.IsArchived, $GlobalData.Action);
            }
        }
        catch (ex) {
            console.log(ex);
        }
    });

    $('#chkSelectAllDI').click(function () {
        try {
            if ($(this).is(':checked')) {
                $('#trDisbursementItemsData').find('.chkChangeAdhocOrArchived').prop('checked', true);
            }
            else {
                $('#trDisbursementItemsData').find('.chkChangeAdhocOrArchived').prop('checked', false);
            }
            GetCheckedDisbursementIDs();
        } catch (e) {
            console.log(e);
        }
    });

    $('.close-btn').click(function () {
        try {
            Popup.hide('divShowDisbursementItem');
        } catch (e) {
            console.log(e);
        }
    });

    $("#btnListDELETE").click(function () {
        try {
            var strDID = GetIDsOfCheckedDI()
            if (strDID == '' || strDID == undefined) {
                ShowNotify('Please select at least one Disbursement Item.', 'error', 3000);
                return false;
            }
            else {
                functionDeleteDisbursementData(strDID);
                NewDiInPartial();
                return false;
            }
        }
        catch (ex) {
            console.log(ex);
        }
    });


    $("#btnViewInvoicePreviewForDI").click(function () {
        try {
            var strDID = GetIDsOfCheckedDIAndAmount()
            if (strDID == '' || strDID == undefined) {
                ShowNotify('Please select at least one Disbursement Item.', 'error', 3000);
                return false;
            }
            else {
                InvoicePreviewDetailsByDI();
                //GetInvoicePreviewCall(strDID);
                return false;
            }
        }
        catch (ex) {
            console.log(ex);
        }
    });

});

var queryString = [];
$GlobalDataDL = {};
$GlobalDataDL.startPage = 0
$GlobalDataDL.resultPerPage = 10;
$GlobalDataDL.InsertedID = 0
$GlobalDataDL.TotalAmount = 0;
$GlobalDataDL.ArrAmount = '';
$GlobalDataDL.ArrItem = '';
$GlobalDataDL.ArrDesc = '';

var DIDS = '';

function LoadPartialDIList() {
    try {
        if (window.location.search.split('?').length > 1) {
            var params = window.location.search.split('?')[1].split('&');
            for (var i = 0; i < params.length; i++) {
                var key = params[i].split('=')[0];
                var value = decodeURIComponent(params[i].split('=')[1]);
                queryString[key] = value;
            }

            var WorkOrderID = queryString["ID"]
            var IsInteger = $.isNumeric(WorkOrderID);
            if (IsInteger == false)
                return false;
            GetQueryString(WorkOrderID)
        }
    } catch (e) {
        console.log(e);
    }
}

function GetIDsOfCheckedDI() {
    try {
        var disbursementids = '';
        $('.chkChangeAdhocOrArchived').each(function () {
            if ($(this).is(':checked')) {
                var disbursementid = $(this).attr('disbursementid');
                if (disbursementids == '') {
                    disbursementids = disbursementid;
                }
                else {
                    disbursementids = disbursementids + ',' + disbursementid;
                }
            }
        });
        return disbursementids;
    } catch (e) {
        console.log(e);
    }
}
function GetIDsOfCheckedDIAndAmount() {
    try {
        var disbursementids = '';
        var TotalAmount = '';
        var ArrayListAmount = new Array();
        var ArrayListItem = new Array();
        var ArrayListDesc = new Array();

        $('.chkChangeAdhocOrArchived').each(function () {
            if ($(this).is(':checked')) {
                var disbursementid = $(this).attr('disbursementid');
                var amount = $(this).attr('amount');
                var desc = $(this).attr('desc');
                var itemnumber = $(this).attr('itemnumber');

                if (disbursementids == '') {
                    disbursementids = disbursementid;
                    TotalAmount = parseInt(amount);
                    ArrayListAmount.push(amount);
                    ArrayListItem.push(itemnumber);
                    ArrayListDesc.push(desc);

                }
                else {
                    TotalAmount = TotalAmount + parseInt(amount);
                    disbursementids = disbursementids + ',' + disbursementid;
                    ArrayListAmount.push(amount);
                    ArrayListItem.push(itemnumber);
                    ArrayListDesc.push(desc);
                }
            }
        });
        $GlobalDataDL.TotalAmount = TotalAmount;
        $GlobalDataDL.ArrAmount = ArrayListAmount;
        $GlobalDataDL.ArrItem = ArrayListItem;
        $GlobalDataDL.ArrDesc = ArrayListDesc;
        return disbursementids;
    } catch (e) {
        console.log(e);
    }
}
function ShowDiPartialDialog() {
    try {
        $('#widget1').removeClass('collapsed');
        $('#widget1').find('.fa-chevron-down').removeClass('fa-chevron-down').addClass('fa-chevron-up');
    } catch (e) {
        console.log(e);
    }
}

function AdhocActionOnDisbursementItems(DIDS, ADHOC, ForState) {
    try {
        PartialAjax1("AdhocActionOnDisbursementItems", "", "", "{'DisbursementIds':'" + DIDS + "','Adhoc':" + ADHOC + ",'ForState':'" + ForState + "'}", '', LoadCallForAllActions);
    } catch (e) {
        console.log(e);
    }
}

function ArchivedActionOnDisbursementItems(DIDS, ARCHIVE, ForState) {
    $("#txtInhouseDIComments").val('')
    try {
        $("#dialog-confirmForDIInhouse").removeClass('hide').dialog({
            width: "450",
            resizable: false,
            modal: true,
            title_html: true,
            buttons: [
                {
                    html: "<i class='ace-icon fa fa-check bigger-110'></i>&nbsp; Save",
                    "class": "Add btn btn-info btn-xs",
                    click: function () {
                        var txtInhouseComment = $.trim($("#txtInhouseDIComments").val());
                        PartialAjax1("ArchivedActionOnDisbursementItems", "", "", "{'DisbursementIds':'" + DIDS + "','Archived':" + ARCHIVE + ",'InHouseComment':'" + escape(txtInhouseComment) + "','ForState':'" + ForState + "'}", '', LoadCallForAllActions);
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

function UnArchivedActionOnDisbursementItems(DIDS, ARCHIVE, ForState) {
    try {
        PartialAjax1("ArchivedActionOnDisbursementItems", "", "", "{'DisbursementIds':'" + DIDS + "','InHouseComment':'" + "" + "','Archived':" + ARCHIVE + ",'ForState':'" + ForState + "'}", '', LoadCallForAllActions);
    } catch (e) {
        console.log(e);
    }
}

function functionDeleteDisbursementData(DIDs) {
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
                        PartialAjax1("DeleteDisbursementItemsByID", "", "", "{'DIDs':'" + DIDs + "'}", '', LoadCallAfterDelete);
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

function LoadCallAfterDelete() {
    try {
        var Action = window.location.href;
        Action = Action.split('/').pop();
        if (Action == 'SearchDisbursementItems')
            DICallForAllActions();
        else CallDataByWorkOrderIDAfterCallBack();
        hidePartialDItems();
        showAllListButtonsAfterEvents();
        ShowNotify('Success.', 'success', 2000);
        return false;
    } catch (e) {
        console.log(e);
    }
}
//function GetInvoicePreviewCall(DIDs) {
//    PartialAjax1("GetInvoicePreviewDataByDI", "", "", "{'DIDs':'" + DIDs + "'}", '', InvoicePreviewDetailsByDI);

//}

function InvoicePreviewDetailsByDI() {
    try {

        $("#ViewInvoiceDI").load("/Billing/Billing/_InvoicePreviewDetails", function () {
            InvoicePreviewDetailsDI("", "", "", "", $GlobalDataDL.TotalAmount, $GlobalDataDL.ArrAmount, $GlobalDataDL.ArrItem, $GlobalDataDL.ArrDesc);
        });

    } catch (e) {
        console.log(e);
    }
}


function DICallForAllActions() {
    LoadCallForSearch($GlobalDataDL.clientID, $GlobalDataDL.SourceID, $GlobalDataDL.WOID, $GlobalDataDL.venderRefId, $GlobalDataDL.Type, $GlobalDataDL.IsVerified, $GlobalDataDL.IsBilled, $GlobalDataDL.IsArchived, $GlobalDataDL.IsAdhoc, $GlobalDataDL.OrderBy, $GlobalDataDL.startPage, $GlobalDataDL.resultPerPage, $GlobalDataDL.FromDate, $GlobalDataDL.ToDate, $GlobalData.ACCPACStatus);

}

function LoadCallForAllActions() {
    try {
        if ($GlobalDataDL.InsertedID == -1) {
            ShowNotify('You do not have permission on this action.', 'error', 3000);
            return false;
        }
        var Action = window.location.href;
        Action = Action.split('/').pop();
        if (Action == 'SearchDisbursementItems')
            DICallForAllActions();
        else CallDataByWorkOrderIDAfterCallBack();
        hidePartialDItems();
        showAllListButtonsAfterEvents();
        NewDiInPartial();
        ShowNotify('Success.', 'success', 2000);
        return false;
    } catch (e) {
        console.log(e);
    }
}
function showAllListButtonsAfterEvents() {
    try {
        $("#btnSendToACCPACForADHOCBilling").show();
        $("#btnRemoveFromACCPACForADHOCBilling").show();
        $("#btnListARCHIVE").show();
        $("#btnListUnARCHIVE").show();
        $("#btnListDELETE").show();
    } catch (e) {
        console.log(e);
    }
}
function PartialAjax(path, templateId, containerId, parameters, clearContent, callBack) {
    try {
        $.ajax({
            type: "POST",
            url: path,
            data: parameters,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            cache: true,
            success: function (msg) {
                try {
                    if (msg == '0') {
                        ShowNotify('Invalid Session login again.', 'error', 3000);
                        return false;
                    }

                    $GlobalData.DIInState = msg.DiInstate;
                    $GlobalData.ActionRule = msg.DiActionRules;
                    $GlobalDataDL.InsertedID = msg;

                    if (msg.OrdersCount != null && msg.OrdersCount != 'undefined') {
                        $GlobalDataDL.totalRow = msg.OrdersCount;
                    }

                    if (templateId != '' && containerId != '') {
                        if (!clearContent) {
                            $.tmpl($('#' + templateId).html(), msg.OrdersList).appendTo("#" + containerId);
                        }
                        else {
                            $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg.OrdersList));
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
function PartialAjax1(path, templateId, containerId, parameters, clearContent, callBack) {
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
                        ShowNotify('Invalid Session login again.', 'error', 3000);
                        return false;
                    }

                    $GlobalDataDL.InsertedID = msg;
                    if (msg.OrdersCount != null && msg.OrdersCount != 'undefined') {
                        $GlobalDataDL.totalRow = msg.OrdersCount;
                    }

                    if (templateId != '' && containerId != '') {
                        if (!clearContent) {
                            $.tmpl($('#' + templateId).html(), msg.OrdersList).appendTo("#" + containerId);
                        }
                        else {
                            $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg.OrdersList));
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
function LoadCallForSearch(clientID, SourceID, WOID, venderRefId, Type, IsVerified, IsBilled, IsArchived, IsAdhoc, OrderBy, startpage, rowsperpage, FromDate, ToDate, ACCPACStatus) {
    try {
        $GlobalDataDL.resultPerPage = rowsperpage;
        $GlobalDataDL.SourceID = SourceID;
        $GlobalDataDL.clientID = clientID;
        $GlobalDataDL.FromDate = FromDate;
        $GlobalDataDL.ToDate = ToDate;
        $GlobalDataDL.startPage = startpage;
        $GlobalDataDL.Type = Type;
        $GlobalDataDL.WOID = WOID;
        $GlobalDataDL.venderRefId = venderRefId;
        $GlobalDataDL.IsVerified = IsVerified;
        $GlobalDataDL.IsBilled = IsBilled;
        $GlobalDataDL.IsArchived = IsArchived;
        $GlobalDataDL.IsAdhoc = IsAdhoc;
        $GlobalDataDL.OrderBy = OrderBy;
        $GlobalDataDL.ACCPACStatus = ACCPACStatus;
        
        PartialAjax("GetSearchDisbursementItemsData", 'ScriptDisbursementItems', 'trDisbursementItemsData', "{'clientID':'" + clientID + "','SourceID':'" + SourceID + "','WO':'" + escape($.trim(WOID)) + "','venderRefId':'" + $.trim(escape(venderRefId)) + "','Type':'" + Type + "','IsVerified':'" + IsVerified + "','IsBilled':'" + IsBilled + "','IsArchived':'" + IsArchived + "','IsAdhoc':'" + IsAdhoc + "','OrderBy':'" + OrderBy + "','startpage':" + startpage + ",'rowsperpage':" + rowsperpage + ",'FromDate':'" + FromDate + "','ToDate':'" + ToDate + "','ACCPACStatus':" + ACCPACStatus + "}", true, LoadCallBack);
    } catch (e) {
        console.log(e);
    }
}
function LoadCall(clientName, WOID, venderRefId, Type, IsVerified, IsBilled, IsArchived, IsAdhoc, OrderBy, startpage, rowsperpage) {
    try {
        PartialAjax("GetSearchDisbursementItemsData", 'ScriptDisbursementItems', 'trDisbursementItemsData', "{'clientName':'" + clientName + "','WO':'" + WOID + "','venderRefId':'" + venderRefId + "','Type':'" + Type + "','IsVerified':'" + IsVerified + "','IsBilled':'" + IsBilled + "','IsArchived':'" + IsArchived + "','IsAdhoc':'" + IsAdhoc + "','OrderBy':'" + OrderBy + "','startpage':" + $GlobalDataDL.startPage + ",'rowsperpage':" + $GlobalDataDL.resultPerPage + "}", true, LoadCallBack);
    } catch (e) {
        console.log(e);
    }
}
function LoadCallBack() {
    try {
        var tblDisbursementItemsLength = $("#tblSearchDisbursementItem").find("tr").length;
        var UrlArray = [];
        var url = window.location.href;
        UrlArray = url.split('/');
        var lengthOfArray = UrlArray.length;
        url = UrlArray[UrlArray.length - 1];
        $("#tblSearchDisbursementItem").show();
        $(".chkSelectAllDI").prop("checked", false);
        if (tblDisbursementItemsLength >= 2) {
            $('.lblSelectAllDI').show();//select All CheckBox
            $("#divSearchNoData").hide();
            $('#trDisbursementItemsData').find('.aEdit').unbind('click');
            $('#trDisbursementItemsData').find('.aEdit').click(EditDetails);
            $('#trDisbursementItemsData').find('.aView').unbind('click');
            $('#trDisbursementItemsData').find('.aView').click(ShowDetails);
            $('#trDisbursementItemsData').find('.chkChangeAdhocOrArchived').unbind('click');
            $('#trDisbursementItemsData').find('.chkChangeAdhocOrArchived').click(GetCheckedDisbursementIDs);
            $('#trDisbursementItemsData').find('.dvACCPACStatus').tooltip({ html: true });

            $("#divPaging").show();
            $('#divAdhocAndArchiveButtons').show();
            $('#btnSendToACCPACForADHOCBilling').show();
            $('#btnRemoveFromACCPACForADHOCBilling').show();
            $('#btnListARCHIVE').show();
            $('#btnListUnARCHIVE').show();
            $('#btnListDELETE').show();

            $("#trDisbursementItemsData").find("td:eq(7)").each(function () {
                var description = $(this).attr('title')
                var arrToolTipDesc = [];
                arrToolTipDesc = description.split('~^');
                var resultTooltip;

                $.each(arrToolTipDesc, function (index, value) {
                    if (index == 0)
                        resultTooltip = value;
                    else resultTooltip = resultTooltip + '</br>' + value;
                });

                $(this).closest(".spanDescriptionToolTip").html(resultTooltip);
                $(this).attr("title", $(this).closest(".spanDescriptionToolTip").html());

                var descLen = description.length;
                if (descLen <= 10) {
                    $(this).removeAttr("title");
                }
            });

            var exists = $('.NotePartialJS').length;
            if (exists > 0) {
                $('.NotePartialJS').remove();
            }

            var headID = document.getElementsByTagName("head")[0];
            var newScript = document.createElement('script');
            newScript.type = 'text/javascript';
            newScript.src = '/Areas/WO/Content/Scripts/_NotePartial.js';
            newScript.className = "NotePartialJS";
            headID.appendChild(newScript);

            GenerateNumericPaging();
        }
        else {
            $("#divPaging").hide();
            $('#divAdhocAndArchiveButtons').hide();
            $("#divSearchNoData").show();
            $('.lblSelectAllDI').hide();//select All CheckBox
        }

        if ($GlobalData.IsPreviewTab) {
            $('#divAdhocAndArchiveButtons').hide();
            $(".divHeadAction").hide();
            $(".divDIActions").hide();
            //$(".apencil").hide();
        }
        else {
            $('#divAdhocAndArchiveButtons').show();
            $(".divHeadAction").show();
            $(".divDIActions").show();
        }
    }
    catch (e) {
        console.log(e);
    }
}
function ShowDetails() {
    try {
        // $("#divPartial").hide();
        $('#btnAdd').attr('value', 'Add');
        var DID = $(this).attr("DIID");
        var VenderRefID = $(this).attr('venderrefid');
        if (DID != '' && DID != undefined && VenderRefID != undefined) {
            GetDisbursementItemsByIDforDetails(DID, VenderRefID, ShowCallBack);
        }
        return false;
    } catch (e) {
        console.log(e);
    }
}
function EditDetails() {
    try {
        var DID = $(this).attr("DIID");
        var VenderRefID = $(this).attr('venderrefid');

        var NoteCount = $(this).closest('tr').find('.spnNoteCount').text();
        var NoteCountfromDB = $(this).attr('notecount');
        NoteCount = (NoteCount == undefined) ? NoteCountfromDB : NoteCount;

        var NoteType = $(this).attr('notetype');
        var NoteRefId = $(this).attr('refid');
        $("#diLabel").text('Edit Disbursement Item');
        $("#hdnDisbursementItemID").val(DID);
        var woid = $(this).attr('woid');
        $("#hdnWorkOrderID").val(woid);
        scrollToTop();
        if (DID != '' && DID != undefined) {
            BindItemNoInEdit(DID, VenderRefID, NoteRefId, NoteType, NoteCount, EditCallBack);
            var href = window.location.href;
            href = href.split('/').pop();
            if (href == "SearchDisbursementItems") {
                changeAttribueValue();
            }
            else {
                changeAttribueValue();
            }
        }

        var str = new Array();
        str.push(DID);
        CallActionRulesMethods(str, false);
        return false;
    } catch (e) {
        console.log(e);
    }
}
function GetQueryString(WorkOrderID) {
    try {
        PartialAjax("GetDisbursementItemsData", "ScriptDisbursementItems", "trDisbursementItemsData", "{'WID':" + parseInt(WorkOrderID) + ",'startpage':" + $GlobalDataDL.startPage + ",'rowsperpage':" + $GlobalDataDL.resultPerPage + "}", true, LoadCallBack);
    } catch (e) {
        console.log(e);
    }
}
function changeAttribueValue() {
    try {
        $("#btnAdd").attr("value", "Update");
    } catch (e) {
        console.log(e);
    }
}
function GenerateNumericPaging() {
    try {
        var numericcontainer = $('#numericcontainerDL');
        setListCount($GlobalDataDL.totalRow)
        var pagesize = parseInt($GlobalDataDL.resultPerPage);
        var total = parseInt($GlobalDataDL.totalRow);
        var start = parseInt($GlobalDataDL.startPage);

        var currentpagenumber = Math.ceil((start / pagesize));
        var startpagenumber = currentpagenumber - currentpagenumber % 10;
        var numberofpages = Math.ceil((total - startpagenumber * pagesize) / pagesize);


        numberofpages = numberofpages > 10 ? 10 : (numberofpages == 0 ? 1 : numberofpages);
        var paginghtml = '';

        if (startpagenumber * pagesize <= start && start > 0 && startpagenumber > 0)
            paginghtml = paginghtml + "<li><a id='page" + ((startpagenumber * pagesize) - pagesize) + "' class='mappager numpage'>...</a></li>";
        for (var i = startpagenumber; i < startpagenumber + numberofpages; i++) {
            if (i == currentpagenumber)
                paginghtml = paginghtml + "<li class='active'><a>" + (i + 1) + "</a></li>";
            else
                paginghtml = paginghtml + "<li class='normal'><a id='page" + (i * pagesize) + "' class='mappager numpage'>" + (i + 1) + "</a></li>";
        }
        if (((startpagenumber + 10) * pagesize) < total)
            paginghtml = paginghtml + "<li><a  href='javascript:void(0)' id='page" + (i * pagesize) + "' class='mappager numpage'>...</a></li>";

        var prevsection = '<li class="mappager first"><a href="javascript:void(0)">« First</a></li><li class="mappager previous"><a href="javascript:void(0)">« Previous</a></li>';
        var nextsection = '<li class="mappager next"><a href="javascript:void(0)">Next »</a></li><li class="mappager last"><a href="javascript:void(0)">Last »</a></li>';
        numericcontainer.find('ul').html(prevsection + paginghtml + nextsection);
        if (currentpagenumber == 0) {
            var _id = $('#numericcontainerDL ul li.first');
            _id.addClass('firstInactive');
            _id = $('#numericcontainerDL ul li.previous');
            _id.addClass('previousInactive');

        }
        else {
            var _id = $('#numericcontainerDL ul li.first');
            _id.removeClass('firstInactive');
            _id = $('#numericcontainerDL ul li.previous');
            _id.removeClass('previousInactive');
        }
        if ((start + pagesize) >= total) {
            var _id = $('#numericcontainerDL ul li.last');
            _id.addClass('lastInactive');
            _id = $('#numericcontainerDL ul li.next');
            _id.addClass('nextInactive');
            _id.removeClass('next');
        }
        else {
            var _id = $('#numericcontainerDL ul li.last');
            _id.removeClass('lastInactive');
            _id = $('#numericcontainerDL ul li.next');
            _id.removeClass('nextInactive');
        }

        $('.mappager').click(function () {
            if ($(this).hasClass('numpage'))
                $GlobalDataDL.startPage = $(this).attr('id').replace('page', '');
            else if ($(this).hasClass('first')) {
                $GlobalDataDL.startPage = 0;
            }
            else if ($(this).hasClass('next')) {

                if ($GlobalDataDL.startPage < total)
                    $GlobalDataDL.startPage = parseInt($GlobalDataDL.startPage) + parseInt($GlobalDataDL.resultPerPage);
                else
                    return false;

            }
            else if ($(this).hasClass('last')) {
                var modulovalue = (total % $GlobalDataDL.resultPerPage);
                $GlobalDataDL.startPage = (modulovalue == '0') ? (total - $GlobalDataDL.resultPerPage) : (total - modulovalue);

                //$GlobalDataDL.startPage = total - (total % $GlobalDataDL.resultPerPage);

                if ($(this).hasClass('lastInactive'))
                    return false;
            }
            else if ($(this).hasClass('previous')) {
                $GlobalDataDL.startPage = $GlobalDataDL.startPage - $GlobalDataDL.resultPerPage;
                if ($GlobalDataDL.startPage < 0)
                    $GlobalDataDL.startPage = 0;
            }
            if ($(this).hasClass('nextInactive'))
                return false;
            if ($(this).hasClass('previousInactive'))
                return false;
            if ($(this).hasClass('firstInactive', 'previousInactive', 'lastInactive', 'nextInactive'))
                return false;
            //LoadData();
            //$GlobalDataDL.rowsperpage = 10;
            var href = window.location.href;
            href = href.split('/').pop();
            //if (href == "SearchDisbursementItems")
            if (href.toLowerCase().indexOf("searchdisbursementitems") >= 0) {
                if (href.toLowerCase().indexOf("accpacstatus") >= 0) {
                    LoadCallForSearch($GlobalDataDL.clientID, $GlobalDataDL.SourceID, $GlobalDataDL.WOID, $GlobalDataDL.venderRefId, $GlobalDataDL.Type, $GlobalDataDL.IsVerified, $GlobalDataDL.IsBilled, $GlobalDataDL.IsArchived, $GlobalDataDL.IsAdhoc, $GlobalDataDL.OrderBy, $GlobalDataDL.startPage, $GlobalDataDL.resultPerPage, $GlobalDataDL.FromDate, $GlobalDataDL.ToDate, '1');
                }
                else {
                    LoadCallForSearch($GlobalDataDL.clientID, $GlobalDataDL.SourceID, $GlobalDataDL.WOID, $GlobalDataDL.venderRefId, $GlobalDataDL.Type, $GlobalDataDL.IsVerified, $GlobalDataDL.IsBilled, $GlobalDataDL.IsArchived, $GlobalDataDL.IsAdhoc, $GlobalDataDL.OrderBy, $GlobalDataDL.startPage, $GlobalDataDL.resultPerPage, $GlobalDataDL.FromDate, $GlobalDataDL.ToDate, $GlobalDataDL.ACCPACStatus);
                }
            }
            else {
                LoadPartialDIList();
            }
        });
        $('#txtPageToGo').keypress(function (e) {
            if (e.which == 13) {
                var page = parseInt($(this).val());
                var lastpage = total - (total % $GlobalDataDL.resultPerPage);
                if (page <= lastpage) {
                    $GlobalDataDL.startPage = $GlobalDataDL.resultPerPage * (page - 1);
                    if ($GlobalDataDL.startPage >= total)
                        $GlobalDataDL.startPage = 0;
                    LoadData();
                }
            }
        });

    }
    catch (err) {
        showError('Unable to create paging due to the following error occurred : ' + err.message);
    }
}
function GetCheckedDisbursementIDs() {
    try {
        var chkCheckedCount = $('#trDisbursementItemsData').find('input.chkChangeAdhocOrArchived:checkbox:checked').length;
        var chkTotalCount = $('#trDisbursementItemsData').find('.chkChangeAdhocOrArchived').length;
        if (chkCheckedCount != chkTotalCount) {
            $('#chkSelectAllDI').prop('checked', false)
        }
        else {
            $('#chkSelectAllDI').prop('checked', true)
        }

        var str = new Array();
        $('#trDisbursementItemsData').find('input.chkChangeAdhocOrArchived:checkbox:checked').each(function () {
            str.push($(this).attr('disbursementid'));
        });
        CallActionRulesMethods(str, true);
    } catch (e) {
        console.log(e);
    }
}
function CallActionRulesMethods(str, isList) {
    try {
        var selectedDOData = eventManagement(str, $GlobalData.ActionRule);
        var selectedUNDOData = eventManagement(str, $GlobalData.DIInState);
        validateActions(selectedDOData, selectedUNDOData, isList);
    } catch (e) {
        console.log(e);
    }
}
function eventManagement(DID, data) {
    try {
        return $.grep(data, function (n, i) {
            var isDIDFound = $.inArray("" + n.DIID, DID) != -1;
            return isDIDFound;
        });
    } catch (e) {
        console.log(e);
    }
}
function validateActions(DOActionData, UNDOActionData, isList) {
    try {

        if (isList) {
            $("#btnSendToACCPACForADHOCBilling, #btnRemoveFromACCPACForADHOCBilling, #btnListARCHIVE, #btnListUnARCHIVE, #btnListDELETE").hide();
        }
        else {
            $("#btnAdd, #btnArchive, #btnUNArchive, #btnAdhocBilling, #btnUNAdhocBilling, #btnDelete, #btnUNAdhocBilling").hide();
        }

        $.each(new Array('AD', 'AR', 'UP', 'DE', 'BI'), function (index, value) {
            var actionDO = true;
            var actionUnDO = true;

            var ActionDataDO = filterByForState(DOActionData, value);
            $.each(ActionDataDO, function (index, ActionDataDObject) {
                actionDO = actionDO && (ActionDataDObject.DO == 1 ? true : false);
                actionUnDO = actionUnDO && (ActionDataDObject.UNDO == 1 ? true : false);
            });

            if (isList) {
                if (value == 'AD' && actionDO) {
                    $("#btnSendToACCPACForADHOCBilling").show();
                }
                if (value == 'DE' && actionDO) {
                    $("#btnListDELETE").show();
                }
                if (value == 'AR' && actionDO) {
                    $("#btnListARCHIVE").show();
                }

                if (value == 'AR' && actionUnDO) {
                    $("#btnListUnARCHIVE").show();
                }
                if (value == 'AD' && actionUnDO) {
                    $("#btnRemoveFromACCPACForADHOCBilling").show();
                }
            }
            else {
                if (value == 'AD' && actionDO) {
                    $("#btnAdhocBilling").show();
                }
                if (value == 'DE' && actionDO) {
                    $("#btnDelete").show();
                }
                if (value == 'AR' && actionDO) {
                    $("#btnArchive").show();
                }
                if (value == 'UP' && actionDO) {
                    $("#btnAdd").show();
                }
                if (value == 'AR' && actionUnDO) {
                    $("#btnUNArchive").show();
                }

                if (value == 'AD' && actionUnDO) {
                    $("#btnUNAdhocBilling").show();
                }
            }
        });

    } catch (e) {
        console.log(e);
    }
}
function filterByForState(ActionData, ForStateValue) {
    try {
        return $.grep(ActionData, function (n, i) {
            return (n.ForState == ForStateValue);
        });
    } catch (e) {
        console.log(e);
    }
}
function filterByINState(ActionData, INStateValue) {
    try {
        return $.grep(ActionData, function (n, i) {
            return (n.InState == INStateValue);
        });
    } catch (e) {
        console.log(e);
    }
}

function scrollToTop() {
    $("html, body").animate({ scrollTop: 0 }, "slow");
    return false;
}