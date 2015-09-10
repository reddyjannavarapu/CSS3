$(document).ready(function () {
    $GlobalBatchData = {};
    $GlobalBatchData.startPage = 0;
    $GlobalBatchData.resultPerPage = 10;
    $GlobalBatchData.totalRow = 10;
    $GlobalBatchData.InsertedID = 0;
    $GlobalBatchData.BatchType = '';
    $GlobalBatchData.BatchID = '';
    $GlobalBatchData.FromDate = '';
    $GlobalBatchData.ToDate = '';
    $GlobalBatchData.InvoiceDate = '';
    $GlobalBatchData.OrderBy = 'CreatedDate ASC';
    SetPageAttributes('liMasters', 'Masters', 'Cab Batch', 'liCABBatch');
    BindBatchType();

    ApplyDatePickerForFromDate();
    ApplyDatePickerForToDate();

    $("#btnSaveCabBatch").click(function () {
        $GlobalBatchData.BatchType = $("#ddlBatchType").val();
        $GlobalBatchData.BatchID = $("#txtBatchID").val();
        $GlobalBatchData.FromDate = $("#txtFromDate").val();
        $GlobalBatchData.ToDate = $("#txtToDate").val();

        var count = 0;
        count += ControlEmptyNess(true, $("#ddlBatchType"), 'Please select BatchType.');
        count += ControlEmptyNess(true, $("#txtBatchID"), 'Please enter Batch ID.');
        count += ControlEmptyNess(true, $("#txtFromDate"), 'Please enter From Date.');
        count += ControlEmptyNess(true, $("#txtToDate"), 'Please enter To Date.');

        if (count > 0) {
            ShowNotify('Please select/enter all mandatory fields.', 'error', 3000);
            return false;
        }
        var hdnBatchID = $("#hdnBatchID").val();
        if (hdnBatchID == '')
            hdnBatchID = 0;
        CallBatchService("SaveCabBatchDetails", '', '', "{'ID':" + hdnBatchID + ",'BatchType':'" + $GlobalBatchData.BatchType + "','BatchID':'" + $GlobalBatchData.BatchID + "','FromDate':'" + $GlobalBatchData.FromDate + "','ToDate':'" + $GlobalBatchData.ToDate + "'}", false, SaveBatchDetailsCallBack);

    });
    $("#btnCabBatchSearch").click(function () {
        $GlobalBatchData.BatchType = $("#ddlBatchTypeS").val();
        $GlobalBatchData.OrderBy = $("#ddlOrderBy").val();
        $GlobalBatchData.startPage = 0;
        $GlobalBatchData.resultPerPage = $("#ddlPageSize").val();
        if ($GlobalBatchData.OrderBy == '-1') {
            $GlobalBatchData.OrderBy = '';
        }

        BindCabBatchDetails();

    });
    $("#btnClear").click(function () {
        ClearBatchDetails();
    });
    $("#btnViewGap").click(function () {
        $GlobalBatchData.BatchType = $("#ddlBatchTypeS").val();
        if ($GlobalBatchData.BatchType == '') {
            ShowNotify('Please select Batch Type.', 'error', 3000);
            return false;
        }

        if ($GlobalBatchData.BatchType != '' && $GlobalBatchData.BatchType != 'undefined')
            CallBatchService("GetBabBatchGapByBatchType", "scriptGapCabBatchDetails", "trGapBody", "{'BatchType':'" + $GlobalBatchData.BatchType + "'}", true, ShowGapAnalysisTable);



    });
});

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
        $('#modal-form1').modal({
            "backdrop": "static",
            "show": "true"
        });

    } catch (e) {
        console.log(e);
    }
}
function ClearBatchDetails() {
    $("#ddlBatchType").val('');
    $("#txtBatchID").val('');
    $("#txtFromDate").val('');
    $("#txtToDate").val('');
    $("#hdnBatchID").val('');

    ApplyDatePickerForFromDate();
    ApplyDatePickerForToDate();
}

function ApplyDatePickerForFromDate() {
    $('#txtFromDate').datepicker("destroy").datepicker({
        showOtherMonths: true,
        selectOtherMonths: true,
        showWeek: true,
        changeYear: true,
        changeMonth: true,
        dateFormat: 'dd/mm/yy',
        yearRange: "-10:+50",
        onClose: function (selectedDate) {
            $("#txtToDate").datepicker("option", "minDate", selectedDate);
        }
    });
}

function ApplyDatePickerForToDate() {
    $('#txtToDate').datepicker("destroy").datepicker({
        showOtherMonths: true,
        selectOtherMonths: true,
        showWeek: true,
        changeYear: true,
        changeMonth: true,
        dateFormat: 'dd/mm/yy',
        yearRange: "-10:+50",
        onClose: function (selectedDate) {
            $("#txtFromDate").datepicker("option", "maxDate", selectedDate);
        }
    });
}

function SaveBatchDetailsCallBack() {
    if ($GlobalBatchData.InsertedID == 1) {
        ShowNotify('Success.', 'success', 3000);
        $GlobalBatchData.BatchType = '';
        ClearBatchDetails();
        BindCabBatchDetails();
        return false
    }
    if ($GlobalBatchData.InsertedID == -1) {
        ShowNotify('Cab Batch details already exists.', 'error', 3000);
        return false
    }

}
function BindBatchType() {
    CallBatchService("GetMBatchType", 'scriptBatchType', 'ddlBatchType', "{}", false, BindCabBatchDetails);
    CallBatchService("GetMBatchType", 'scriptBatchType', 'ddlBatchTypeS', "{}", false, '');
}

function BindCabBatchDetails() {
    CallBatchService("GetCabBatchDetails", 'CABBatchTemplate', 'trData', "{'BatchType':'" + $GlobalBatchData.BatchType + "','OrderBy':'" + $GlobalBatchData.OrderBy + "','startpage':" + $GlobalBatchData.startPage + ",'rowsperpage':" + $GlobalBatchData.resultPerPage + "}", true, DataLoadCallBack);
}

function DataLoadCallBack() {
    try {
        var trLength = $('#trData').find('tr').length;
        if (trLength == 0) {
            $('#divPagination').hide();
            $('#divCabBatchData').show();
        }
        else {
            $('#divCabBatchData').hide();
            $('#divPagination').show();

            $('#trData').find('.aEdit').unbind('click');
            $('#trData').find('.aEdit').click(EditCAbBatchDetails);

            $('#trData').find('.aView').unbind('click');
            $('#trData').find('.aView').click(ViewCAbBatchDetails);

            //$('#trData').find('.aDelete').unbind('click');
            //$('#trData').find('.aDelete').click(DeleteCAbBatchDetails);

            GenerateNumericPaging();
        }

    } catch (e) {
        console.log(e);
    }
}


function EditCAbBatchDetails() {
    try {
        scrollToTop();
        $('#hdnBatchID').val($(this).attr('id'));
        $('#ddlBatchType').val($(this).attr('type'));
        $('#txtBatchID').val($(this).attr('batchID'));
        $('#txtFromDate').val($(this).attr('fromdate'));
        $('#txtToDate').val($(this).attr('todate'));
        ApplyDatePickerForFromDate();
        ApplyDatePickerForToDate();
        $("#txtToDate").datepicker("option", "minDate", $(this).attr('fromdate'));
        $("#txtFromDate").datepicker("option", "maxDate", $(this).attr('todate'));

    } catch (e) {
        console.log(e);
    }

}

function ViewCAbBatchDetails() {
    try {
        $('#spnID').text($(this).attr('id'));
        $('#spnBatchType').text($(this).attr('batchtype'));
        $('#spnBatchID').text($(this).attr('batchID'));
        $('#spnFromDate').text($(this).attr('fromdate'));
        $('#spnToDate').text($(this).attr('todate'));
        $('#modal-form').modal('show');

    } catch (e) {
        console.log(e);
    }
}

function DeleteCAbBatchDetails() {
    try {
        var ID = $(this).attr('id');
        if (ID != '' || ID != undefined)
            CallBatchService("DeleteCabBatchDetailsByID", '', '', "{'ID':" + ID + "}", true, BindCabBatchDetails);

    } catch (e) {
        console.log(e);
    }
}

function CallBatchService(path, templateId, containerId, parameters, clearContent, callBack) {
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
                $GlobalBatchData.InsertedID = msg;
                if (msg.CABBatchCount != null && msg.CABBatchCount != 'undefined') {

                    $GlobalBatchData.totalRow = msg.CABBatchCount;
                }
                if (templateId != '' && containerId != '' && msg != '') {

                    if (!clearContent) {
                        $.tmpl($('#' + templateId).html(), msg.CABBatchList).appendTo("#" + containerId);
                    }
                    else {
                        $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg.CABBatchList));
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
}

function GenerateNumericPaging() {
    try {
        var numericcontainer = $('#numericcontainer');
        setListCount($GlobalBatchData.totalRow)
        var pagesize = parseInt($GlobalBatchData.resultPerPage);
        var total = parseInt($GlobalBatchData.totalRow);
        var start = parseInt($GlobalBatchData.startPage);

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
            paginghtml = paginghtml + "<a  href='javascript:void(0)' id='page" + (i * pagesize) + "' class='mappager numpage'>...</a>";
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
                $GlobalBatchData.startPage = $(this).attr('id').replace('page', '');
            else if ($(this).hasClass('first')) {
                $GlobalBatchData.startPage = 0;
            }
            else if ($(this).hasClass('next')) {

                if ($GlobalBatchData.startPage < total)
                    $GlobalBatchData.startPage = parseInt($GlobalBatchData.startPage) + parseInt($GlobalBatchData.resultPerPage);
                else
                    return false;

            }
            else if ($(this).hasClass('last')) {
                var modulovalue = (total % $GlobalBatchData.resultPerPage);
                $GlobalBatchData.startPage = (modulovalue == '0') ? (total - $GlobalBatchData.resultPerPage) : (total - modulovalue);

                //$GlobalBatchData.startPage = total - (total % $GlobalBatchData.resultPerPage);

                if ($(this).hasClass('lastInactive'))
                    return false;
            }
            else if ($(this).hasClass('previous')) {
                $GlobalBatchData.startPage = $GlobalBatchData.startPage - $GlobalBatchData.resultPerPage;
                if ($GlobalBatchData.startPage < 0)
                    $GlobalBatchData.startPage = 0;
            }
            if ($(this).hasClass('nextInactive'))
                return false;
            if ($(this).hasClass('previousInactive'))
                return false;
            if ($(this).hasClass('firstInactive', 'previousInactive', 'lastInactive', 'nextInactive'))
                return false;
            BindCabBatchDetails();
        });
        $('#txtPageToGo').keypress(function (e) {
            if (e.which == 13) {
                var page = parseInt($(this).val());
                var lastpage = total - (total % $GlobalBatchData.resultPerPage);
                if (page <= lastpage) {
                    $GlobalBatchData.startPage = $GlobalBatchData.resultPerPage * (page - 1);
                    if ($GlobalBatchData.startPage >= total)
                        $GlobalBatchData.startPage = 0;
                    BindCabBatchDetails();
                }
            }
        });

    }
    catch (err) {
        showError('Unable to create paging due to the following error occurred : ' + err.message);
    }
}

function scrollToTop() {
    $("html, body").animate({ scrollTop: 0 }, "slow");
    return false;
}