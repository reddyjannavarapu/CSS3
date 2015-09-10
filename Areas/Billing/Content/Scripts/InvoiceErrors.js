$(document).ready(function () {

    $GlobalData = {};
    $GlobalData.startPage = 0;
    $GlobalData.resultPerPage = 10;
    $GlobalData.totalRow = 10;
    $GlobalData.ClientId = '-1';
    $GlobalData.SourceID = '';
    $GlobalData.FromDate = '';
    $GlobalData.ToDate = '';

    SetPageAttributes('liBilling', 'Billing', 'Invoice Errors', 'liInvoiceErrors');

    $GlobalInvoiceError = {};
    $GlobalInvoiceError.InsertedID = 0;

    ApplyDatePickerFromAndToDate($('#txtFromDate'), $('#txtToDate'));


    $('#btnSearchInvoiceErrors').click(function () {
        try {
            $GlobalData.ClientId = $.trim($("#divClientOne .chosen-select1").find("option:Selected").attr('clientcode'));
            $GlobalData.SourceID = $.trim($("#divClientOne .chosen-select1").find("option:Selected").attr('sourceid'));
            $GlobalData.FromDate = $.trim($("#txtFromDate").val());
            $GlobalData.ToDate = $.trim($("#txtToDate").val());
            $GlobalData.startPage = 0;
            $GlobalData.resultPerPage = $("#ddlPageSize").val();

            if (($GlobalData.FromDate == '' && $GlobalData.ToDate != '') || ($GlobalData.ToDate == '' && $GlobalData.FromDate != '')) {
                ShowNotify('Please enter From Date and ToDate.', 'error', 3000);
                return false;
            }

            LoadData();

        } catch (e) {
            console.log(e);
        }
    });

    $('#btnClear').click(function () {
        $('#ddlChosenClient_ddlClient').val('-1').trigger('chosen:updated');
        $("#ddlPageSize").val("10");
        ApplyDatePickerFromAndToDate($('#txtFromDate'), $('#txtToDate'));
    });

});

function LoadData() {
    CallCABInvoiceError("GetAllCABInvoiceErrors", 'ScriptCABInvoiceError', 'trCABInvoiceErrorData', "{'ClientId':'" + $GlobalData.ClientId + "','SourceID':'" + $GlobalData.SourceID + "','startpage':" + $GlobalData.startPage + ",'rowsperpage':" + $GlobalData.resultPerPage + ",'FromDate':'" + $GlobalData.FromDate + "','ToDate':'" + $GlobalData.ToDate + "'}", true, LoadDataCallBack);
}
function LoadDataCallBack() {
    var tblLength = $("#trCABInvoiceErrorData").find("tr").length;
    
    if (tblLength > 0) {
        $('#divSearchCABInvoiceErrorList').show();
        $('#divCABInvoiceErrorPaging').show();
        $('#divCABInvoiceErrorNoData').hide();

        $("#trCABInvoiceErrorData").find(".aView").unbind("click");
        $("#trCABInvoiceErrorData").find(".aView").click(ViewCabInvoiceErrorByCabMasterID);

        $("#trCABInvoiceErrorData").find(".aFix").unbind("click");
        $("#trCABInvoiceErrorData").find(".aFix").click(FixCabInvoiceErrorByCabMasterID);

        GenerateNumericPaging();
    }
    else {
        $('#divCABInvoiceErrorPaging').hide();
        $('#divCABInvoiceErrorNoData').show();
    }

}
function FixCabInvoiceErrorByCabMasterID() {
    try {
        var CABMasterID = $(this).attr("CABMasterID");

        $("#dialog-confirm").removeClass('hide').dialog({
            resizable: false,
            modal: true,
            title_html: true,
            buttons: [
                {
                    html: "<i class='ace-icon fa fa-check-circle bigger-110'></i>&nbsp; Yes",
                    "class": "btn btn-danger btn-xs",
                    click: function () {
                        CallCABInvoiceError("DeleteCABInvoiceErrorByCABMasterID", '', '', "{'CABMasterID':" + parseInt(CABMasterID) + "}", '', DeleteCabInvoiceErrorByCabMasterIDCallBack);
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
function DeleteCabInvoiceErrorByCabMasterIDCallBack() {
    if ($GlobalInvoiceError.InsertedID > 0) {
        ShowNotify('Success.', 'success', 3000);
        LoadData();

    }
}
function ViewCabInvoiceErrorByCabMasterID() {
    try {
        var CABMasterID = $(this).attr("CABMasterID");
        CallCABInvoiceError("GetCABInvoiceErrorByCABMasterID", '', '', "{'CABMasterID':" + parseInt(CABMasterID) + "}", '', ViewCabInvoiceErrorByCabMasterIDCallBack);
    } catch (e) {
        console.log(e);
    }
}
function ViewCabInvoiceErrorByCabMasterIDCallBack(Data) {
    var result = $.parseJSON(Data);
    var ddlID = '1';
    $('#Output_' + ddlID).html('');
    var IsEmpty = '0';

    $.each(result, function (index, data) {

        if (data[0] != undefined) {
            IsEmpty = '1';

            var Title = '<div style="margin:15px 0px 10px;"><h5 class="blue bigger">' + data[0]['TableName'] + '</h5></div>';
            $('#Output_' + ddlID).append(Title);

            var cols = new Array();
            for (var key in data[0]) {
                cols.push(key);
            }

            var table = $('<table class="table table-striped table-bordered table-hover" style="background-color: white !important;"></table>');
            var thbody = $('<thead></thead>');
            var th = $('<tr></tr>');

            for (var i = 0; i < data.length; i++) {
                var row = data[i];
                var tr = $('<tr></tr>');

                for (var j = 1; j < cols.length; j++) {
                    if (i == 0) {
                        th.append('<th>' + cols[j] + '</th>');
                    }

                    var columnName = cols[j];
                    tr.append('<td>' + row[columnName] + '</td>');
                }

                if (i == 0) {
                    table.append(thbody.append(th));
                }

                table.append(tr);
                $('#Output_' + ddlID).append(table);
            }
        }

        if (IsEmpty == '0') {
            $('#Output_' + ddlID).html('<h5 class="red medium"> No data found.</h5>');
            return false;
        }

    });

    $('#modal-formCABInvoiceError').modal('show');
}
function CallCABInvoiceError(path, templateId, containerId, parameters, clearContent, callBack) {
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

                    $GlobalInvoiceError.InsertedID = msg;
                    
                    if (msg != '' && msg != undefined) {
                        if (msg[0].InvoiceErrorsCount != null && msg[0].InvoiceErrorsCount != 'undefined') {
                            $GlobalData.totalRow = msg[0].InvoiceErrorsCount;
                        }
                    }


                    if (templateId != '' && containerId != '') {

                        if (!clearContent) {
                            $.tmpl($('#' + templateId).html(), msg).appendTo("#" + containerId);
                        }
                        else {
                            $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg));
                        }
                    }

                    if (callBack != undefined && callBack != '' && path != 'GetCABInvoiceErrorByCABMasterID') {
                        callBack();
                    }

                    if (path == 'GetCABInvoiceErrorByCABMasterID') {
                        callBack(msg);
                    }

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

function GenerateNumericPaging() {
    try {
        var numericcontainer = $('#numericcontainerCABInvoiceError');
        setListCount($GlobalData.totalRow)
        var pagesize = parseInt($GlobalData.resultPerPage);
        var total = parseInt($GlobalData.totalRow);
        var start = parseInt($GlobalData.startPage);

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
            var _id = $('#numericcontainerCABInvoiceError ul li.first');
            _id.addClass('firstInactive');
            _id = $('#numericcontainerCABInvoiceError ul li.previous');
            _id.addClass('previousInactive');

        }
        else {
            var _id = $('#numericcontainerCABInvoiceError ul li.first');
            _id.removeClass('firstInactive');
            _id = $('#numericcontainerCABInvoiceError ul li.previous');
            _id.removeClass('previousInactive');
        }
        if ((start + pagesize) >= total) {
            var _id = $('#numericcontainerCABInvoiceError ul li.last');
            _id.addClass('lastInactive');
            _id = $('#numericcontainerCABInvoiceError ul li.next');
            _id.addClass('nextInactive');
            _id.removeClass('next');
        }
        else {
            var _id = $('#numericcontainerCABInvoiceError ul li.last');
            _id.removeClass('lastInactive');
            _id = $('#numericcontainerCABInvoiceError ul li.next');
            _id.removeClass('nextInactive');
        }

        $('.mappager').click(function () {
            if ($(this).hasClass('numpage'))
                $GlobalData.startPage = $(this).attr('id').replace('page', '');
            else if ($(this).hasClass('first')) {
                $GlobalData.startPage = 0;
            }
            else if ($(this).hasClass('next')) {

                if ($GlobalData.startPage < total)
                    $GlobalData.startPage = parseInt($GlobalData.startPage) + parseInt($GlobalData.resultPerPage);
                else
                    return false;

            }
            else if ($(this).hasClass('last')) {
                var modulovalue = (total % $GlobalData.resultPerPage);
                $GlobalData.startPage = (modulovalue == '0') ? (total - $GlobalData.resultPerPage) : (total - modulovalue);

                if ($(this).hasClass('lastInactive'))
                    return false;
            }
            else if ($(this).hasClass('previous')) {
                $GlobalData.startPage = $GlobalData.startPage - $GlobalData.resultPerPage;
                if ($GlobalData.startPage < 0)
                    $GlobalData.startPage = 0;
            }
            if ($(this).hasClass('nextInactive'))
                return false;
            if ($(this).hasClass('previousInactive'))
                return false;
            if ($(this).hasClass('firstInactive', 'previousInactive', 'lastInactive', 'nextInactive'))
                return false;

            LoadData();
        });

        $('#txtPageToGo').keypress(function (e) {
            if (e.which == 13) {
                var page = parseInt($(this).val());
                var lastpage = total - (total % $GlobalData.resultPerPage);
                if (page <= lastpage) {
                    $GlobalData.startPage = $GlobalData.resultPerPage * (page - 1);
                    if ($GlobalData.startPage >= total)
                        $GlobalData.startPage = 0;
                    LoadData();
                }
            }
        });

    }
    catch (err) {
        showError('Unable to create paging due to the following error occurred : ' + err.message);
    }
}