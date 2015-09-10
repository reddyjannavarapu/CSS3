$(document).ready(function () {
    SetPageAttributes('liBilling', 'Billing', 'View Billing Data', 'liViewBillingData');
    $GlobalData = {};
    $GlobalData.ClientCode = '';
    $GlobalData.SourceID = '';
    $GlobalData.ClientName = '';
    $GlobalViewBillingData = {};
    $GlobalViewBillingData.ClientCode = '';
    $GlobalViewBillingData.SourceID = '';
    $GlobalViewBillingData.FeeCode = '';
    $GlobalViewBillingData.CABMasterListLength = '';
    $GlobalViewBillingData.CABMasterID = '';
    $GlobalViewBillingData.CABFeeScheduleID = '';
    $GlobalViewBillingData.BillFromDate = '';
    $GlobalViewBillingData.BillToDate = '';
    $GlobalViewBillingData.InvoicePreview = '';
    $GlobalViewBillingData.startPage = 0;
    $GlobalViewBillingData.resultPerPage = 10;
    $GlobalViewBillingData.totalRow = 0;
    $GlobalViewBillingData.InvoiceNo = '';
    $GlobalViewBillingData.BillType = -1;

     
    ApplyDatePickerFromAndToDate($('#txtBillFromDate'), $('#txtBillToDate'));


    $("#btnBilling").click(function () {
        $GlobalViewBillingData.ClientCode = $("#divClientOne .chosen-select1").find("option:selected").attr("clientcode");
        $GlobalViewBillingData.SourceID = $("#divClientOne .chosen-select1").find("option:selected").attr("sourceid");
        $GlobalViewBillingData.FeeCode = $("#ddlFee").val();
        $GlobalViewBillingData.BillFromDate = $("#txtBillFromDate").val();
        $GlobalViewBillingData.BillToDate = $("#txtBillToDate").val();
        $GlobalViewBillingData.startPage = 0;
        $GlobalViewBillingData.resultPerPage = $("#ddlPageSize").val();
        $GlobalViewBillingData.BillType = $('#ddlBillType').val();

        if ($GlobalViewBillingData.ClientCode == '' || $GlobalViewBillingData.ClientCode == undefined) {
            $GlobalViewBillingData.ClientCode = '';
            $GlobalViewBillingData.SourceID = '';

        }
        if ($GlobalViewBillingData.FeeCode == '-1' || $GlobalViewBillingData.FeeCode == undefined) {
            $GlobalViewBillingData.FeeCode = '';
        }
        CabMasterCall();

    });

});

function CabMasterCall() {
    CABMasterServiceCall("GetAllBillingDetailsByCompanyAndFee", "scripCABMaster", "tbodyCABMaster", "{'CompanyID':'" + $GlobalViewBillingData.ClientCode + "','CompanySource':'" + $GlobalViewBillingData.SourceID + "','FeeCode':'" + $GlobalViewBillingData.FeeCode + "','BillFromDate':'" + $GlobalViewBillingData.BillFromDate + "','BillToDate':'" + $GlobalViewBillingData.BillToDate + "','StartPage':" + $GlobalViewBillingData.startPage + ",'ResultPerPage':" + $GlobalViewBillingData.resultPerPage + ",'BillType':" + $GlobalViewBillingData.BillType + "}", false, true, CabMasterCallBack);
}

function CabMasterCallBack() {
    if ($GlobalViewBillingData.CABMasterListLength > 0) {
        $("#tbodyCABMaster").find(".CABMasterID").unbind("click");
        $("#tbodyCABMaster").find(".CABMasterID").click(GetScheduleByCABMasterID);

        $("#tbodyCABMaster").find(".btnViewInvoicePreview").unbind("click");
        $("#tbodyCABMaster").find(".btnViewInvoicePreview").click(GetCabMasterInvoicePreview);

        $("#tbodyCABMaster").find('.dvHSReceivedStatus').tooltip({ html: true });

        $('#divPagination').show();
        $("#divBillingByCompanyAndFee").show();
        $("#divSearchNoData").hide();
        GenerateNumericPaging();

    }
    else {
        $("#divBillingByCompanyAndFee").show();
        $("#divSearchNoData").show();
        $('#divPagination').hide();
    }
}


function GetCabMasterInvoicePreview() {

    $GlobalViewBillingData.CABMasterID = $(this).attr('CABMasterID');
    $GlobalViewBillingData.InvoiceNo = $(this).text();
    $GlobalViewBillingData.InvoiceNo = ($GlobalViewBillingData.InvoiceNo == 'INVOICE PREVIEW') ? '' : $GlobalViewBillingData.InvoiceNo;

    CABMasterServiceCall1("GetInvoicePreviewDetailsByCabMasterID", "", "", "{'CABMasterID':" + $GlobalViewBillingData.CABMasterID + "}", "", "", InvoicePreviewDetailsByCabMasterIDCallBack);

}

function InvoicePreviewDetailsByCabMasterIDCallBack() {
    var Name = $GlobalViewBillingData.InvoicePreview.CABMasterInvoicePreviewList1[0].Name;
    var Address1 = $GlobalViewBillingData.InvoicePreview.CABMasterInvoicePreviewList1[0].AddressLine1;
    var Address2 = $GlobalViewBillingData.InvoicePreview.CABMasterInvoicePreviewList1[0].AddressLine2;
    var Address3 = $GlobalViewBillingData.InvoicePreview.CABMasterInvoicePreviewList1[0].AddressLine3;
    var Amount = $GlobalViewBillingData.InvoicePreview.CABMasterInvoicePreviewList1[0].Amount;
    try {

        $("#ViewInvoice").load("_InvoicePreviewDetails", function () {
            InvoicePreviewDetails(Name, Address1, Address2, Address3, Amount, $GlobalViewBillingData.InvoicePreview.CABMasterInvoicePreviewList2, $GlobalViewBillingData.InvoiceNo);
        });

    } catch (e) {
        console.log(e);
    }
}


function GetScheduleByCABMasterID() {
    $GlobalViewBillingData.CABMasterID = $(this).attr('CABMasterID');
    var trScheduleID = 'trSchedule' + $GlobalViewBillingData.CABMasterID;
    $('.ScheduleDataInfo').addClass('hideinfo');
    $('.ScheduleDetailsDataInfo').addClass('hideinfo');
    var trScheduleDataID = 'trScheduleData' + $GlobalViewBillingData.CABMasterID;
    $('#' + trScheduleDataID).removeClass('hideinfo');


    if ($GlobalViewBillingData.CABMasterID != '' && $GlobalViewBillingData.CABMasterID != undefined)
        CABMasterServiceCall1("GetCABFeeScheduleReportByMasterID", "CABFeeSchedule", trScheduleID, "{'CABMasterID':'" + $GlobalViewBillingData.CABMasterID + "'}", false, true, CabScheduleCallBack);

}
function CabScheduleCallBack() {

    $("#tbodyCABMaster").find(".CABFeeScheduleID").unbind("click");
    $("#tbodyCABMaster").find(".CABFeeScheduleID").click(GetScheduleDetailsByCABFeeScheduleID);

}

function GetScheduleDetailsByCABFeeScheduleID() {
    $GlobalViewBillingData.CABFeeScheduleID = $(this).attr('cabfeescheduleid');
    var trScheduleDetailsID = 'trScheduleDetails' + $GlobalViewBillingData.CABFeeScheduleID;

    var trScheduleDetailsDataID = 'trScheduleDetailsData' + $GlobalViewBillingData.CABFeeScheduleID;

    $('.ScheduleDetailsDataInfo').addClass('hideinfo');
    $('#' + trScheduleDetailsDataID).removeClass('hideinfo');

    if ($GlobalViewBillingData.CABFeeScheduleID != '' && $GlobalViewBillingData.CABFeeScheduleID != undefined)
        CABMasterServiceCall2("GetCABFeeScheduleDetailsReportByCABFeeScheduleID", "CABFeeScheduleDetails", trScheduleDetailsID, "{'CABFeeScheduleID':'" + $GlobalViewBillingData.CABFeeScheduleID + "'}", false, true, '');

    $('.ace-scroll').ace_scroll({ size: 360, styleClass: 'scroll-visible', mouseWheelLock: true });

}

function CABMasterServiceCall(path, templateId, containerId, parameters, IsCalss, clearContent, callBack) {
    $.ajax({
        type: "POST",
        url: path,
        data: parameters,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (msg) {
            if (msg == 0) {
                ShowNotify('Invalid session login again.', 'error', 3000);
                return false;
            }
            $GlobalViewBillingData.CABMasterListLength = msg.CABMasterList.length;
            if (msg.BillingCount != null && msg.BillingCount != 'undefined') {
                $GlobalViewBillingData.totalRow = msg.BillingCount;
            }
            if (templateId != '' && containerId != '') {
                if (IsCalss == true) {

                    if (!clearContent) {
                        $.tmpl($('.' + templateId).html(), msg.CABMasterList).appendTo("#" + containerId);
                    }
                    else {
                        $("." + containerId).html($.tmpl($('#' + templateId).html(), msg.CABMasterList));
                    }
                }
                else {

                    if (!clearContent) {
                        $.tmpl($('#' + templateId).html(), msg.CABMasterList).appendTo("#" + containerId);
                    }
                    else {
                        $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg.CABMasterList));
                    }
                }


                //var len = $("#" + containerId).find("tr").length;
                //alert(len);
                //if (len == 0) {
                //    $("#divBillingByCompanyAndFee").show();
                //    $("#divSearchNoData").show();
                //}
            }

            if (callBack != undefined && callBack != '')
                callBack();

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {
            throw new Error(xhr.statusText);
        }
    });
}
function CABMasterServiceCall1(path, templateId, containerId, parameters, IsCalss, clearContent, callBack) {
    $.ajax({
        type: "POST",
        url: path,
        data: parameters,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (msg) {
            if (msg == 0) {
                ShowNotify('Invalid session login again.', 'error', 3000);
                return false;
            }

            $GlobalViewBillingData.CABMasterListLength = msg.CABMasterList.length;
            $GlobalViewBillingData.InvoicePreview = msg;
            if (templateId != '' && containerId != '') {
                if (IsCalss == true) {

                    if (!clearContent) {
                        $.tmpl($('.' + templateId).html(), msg.CABFeeScheduleList).appendTo("#" + containerId);
                    }
                    else {
                        $("." + containerId).html($.tmpl($('#' + templateId).html(), msg.CABFeeScheduleList));
                    }
                }
                else {

                    if (!clearContent) {
                        $.tmpl($('#' + templateId).html(), msg.CABFeeScheduleList).appendTo("#" + containerId);
                    }
                    else {
                        $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg.CABFeeScheduleList));
                    }

                    var len = $("#" + containerId).find("tr").length;

                    if (len == 0) {
                        $("#" + containerId).html($("#divSearchNoData").html());

                    }
                }




            }

            if (callBack != undefined && callBack != '')
                callBack();

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {
            throw new Error(xhr.statusText);
        }
    });
}
function CABMasterServiceCall2(path, templateId, containerId, parameters, IsCalss, clearContent, callBack) {
    $.ajax({
        type: "POST",
        url: path,
        data: parameters,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (msg) {
            if (msg == 0) {
                ShowNotify('Invalid session login again.', 'error', 3000);
                return false;
            }
            $GlobalViewBillingData.CABMasterListLength = msg.CABMasterList.length;
            if (templateId != '' && containerId != '') {
                if (IsCalss == true) {

                    if (!clearContent) {
                        $.tmpl($('.' + templateId).html(), msg.CABFeeScheduleDetailsList).appendTo("#" + containerId);
                    }
                    else {
                        $("." + containerId).html($.tmpl($('#' + templateId).html(), msg.CABFeeScheduleDetailsList));
                    }
                }
                else {

                    if (!clearContent) {
                        $.tmpl($('#' + templateId).html(), msg.CABFeeScheduleDetailsList).appendTo("#" + containerId);
                    }
                    else {
                        $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg.CABFeeScheduleDetailsList));
                    }
                }
                var len = $("#" + containerId).find("tr").length;

                if (len == 0) {
                    $("#" + containerId).html($("#divSearchNoData").html());

                }
            }
            if (callBack != undefined && callBack != '')
                callBack();

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {
            throw new Error(xhr.statusText);
        }
    });
}

function GenerateNumericPaging() {
    try {
        setListCount($GlobalViewBillingData.totalRow)
        var numericcontainer = $('#numericcontainer');
        var pagesize = parseInt($GlobalViewBillingData.resultPerPage);
        var total = parseInt($GlobalViewBillingData.totalRow);
        var start = parseInt($GlobalViewBillingData.startPage);

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
            var _id = $('#numericcontainer ul li.first');
            _id.addClass('firstInactive');
            _id = $('#numericcontainer ul li.previous');
            _id.addClass('previousInactive');

        }
        else {
            var _id = $('#numericcontainer ul li.first');
            _id.removeClass('firstInactive');
            _id = $('#numericcontainer ul li.previous');
            _id.removeClass('previousInactive');
        }
        if ((start + pagesize) >= total) {
            var _id = $('#numericcontainer ul li.last');
            _id.addClass('lastInactive');
            _id = $('#numericcontainer ul li.next');
            _id.addClass('nextInactive');
            _id.removeClass('next');
        }
        else {
            var _id = $('#numericcontainer ul li.last');
            _id.removeClass('lastInactive');
            _id = $('#numericcontainer ul li.next');
            _id.removeClass('nextInactive');
        }

        $('.mappager').click(function () {
            if ($(this).hasClass('numpage'))
                $GlobalViewBillingData.startPage = $(this).attr('id').replace('page', '');
            else if ($(this).hasClass('first')) {
                $GlobalViewBillingData.startPage = 0;
            }
            else if ($(this).hasClass('next')) {

                if ($GlobalViewBillingData.startPage < total)
                    $GlobalViewBillingData.startPage = parseInt($GlobalViewBillingData.startPage) + parseInt($GlobalViewBillingData.resultPerPage);
                else
                    return false;

            }
            else if ($(this).hasClass('last')) {
                var modulovalue = (total % $GlobalViewBillingData.resultPerPage);
                $GlobalViewBillingData.startPage = (modulovalue == '0') ? (total - $GlobalViewBillingData.resultPerPage) : (total - modulovalue);

                //$GlobalViewBillingData.startPage = total - (total % $GlobalViewBillingData.resultPerPage);

                if ($(this).hasClass('lastInactive'))
                    return false;
            }
            else if ($(this).hasClass('previous')) {
                $GlobalViewBillingData.startPage = $GlobalViewBillingData.startPage - $GlobalViewBillingData.resultPerPage;
                if ($GlobalViewBillingData.startPage < 0)
                    $GlobalViewBillingData.startPage = 0;
            }
            if ($(this).hasClass('nextInactive'))
                return false;
            if ($(this).hasClass('previousInactive'))
                return false;
            if ($(this).hasClass('firstInactive', 'previousInactive', 'lastInactive', 'nextInactive'))
                return false;
            CabMasterCall();
        });
        $('#txtPageToGo').keypress(function (e) {
            if (e.which == 13) {
                var page = parseInt($(this).val());
                var lastpage = total - (total % $GlobalViewBillingData.resultPerPage);
                if (page <= lastpage) {
                    $GlobalViewBillingData.startPage = $GlobalViewBillingData.resultPerPage * (page - 1);
                    if ($GlobalViewBillingData.startPage >= total)
                        $GlobalViewBillingData.startPage = 0;
                    CabMasterCall();
                }
            }
        });

    }
    catch (err) {
        showError('Unable to create paging due to the following error occurred : ' + err.message);
    }
}