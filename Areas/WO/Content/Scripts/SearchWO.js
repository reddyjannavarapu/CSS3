$(document).ready(function () {
    $GlobalData = {};
    $GlobalData.serviceId = 0;
    $GlobalData.serviceData = '';
    $GlobalData.status = '-1';
    $GlobalData.startPage = 0;
    $GlobalData.resultPerPage = 10;
    $GlobalData.totalRow = 10;
    $GlobalData.InsertedID = '';
    $GlobalData.Type = '-1';
    $GlobalData.ClientId = '-1';
    $GlobalData.WorkOrderID = '';
    $GlobalData.SourceID = '';
    $GlobalData.OrderBy = ''
    $GlobalData.FromDate = '';
    $GlobalData.ToDate = '';
    $GlobalData.IsAdhoc = '';
    $GlobalData.AssignedTo = '-1';

    SetPageAttributes('liWorkOrder', 'WO', 'Search Work Orders', 'liSearchWorkOrder');

    ApplyDatePickerFromAndToDate($('#txtFromDate'), $('#txtToDate'));

    $("input").keypress(function (event) {

        if (event.which == 13) {
            event.preventDefault();
            $('.btnSearchWO').click();
        }
    });

    $('.btnSearchWO').on('click', function () {

        try {
            $GlobalData.status = $.trim($('#ddlStatusCode').val());
            $GlobalData.Type = $.trim($("#ddlDIType").find("option:Selected").val());
            $GlobalData.WorkOrderID = $.trim($('#txtWorkOrderID').val());
            //$GlobalData.ClientId = $.trim($("#ddlClient").find("option:Selected").val());
            $GlobalData.ClientId = $.trim($("#divClientOne .chosen-select1").find("option:Selected").attr('clientcode'));
            $GlobalData.SourceID = $.trim($("#divClientOne .chosen-select1").find("option:Selected").attr('sourceid'));
            $GlobalData.OrderBy = $.trim($("#ddlOrderBy").find("option:Selected").val());
            $GlobalData.FromDate = $.trim($("#txtFromDate").val());
            $GlobalData.ToDate = $.trim($("#txtToDate").val());
            $GlobalData.startPage = 0;
            $GlobalData.resultPerPage = $("#ddlPageSize").val();
            $GlobalData.AssignedTo = $('#ddlAssignedTo').find("option:Selected").val();


            if ($('#chkAdhoc').is(':checked')) {
                $GlobalData.IsAdhoc = '1';
            }
            else {
                $GlobalData.IsAdhoc = '';
            }

            if (($GlobalData.FromDate == '' && $GlobalData.ToDate != '') || ($GlobalData.ToDate == '' && $GlobalData.FromDate != '')) {
                ShowNotify('Please enter From Date and ToDate.', 'error', 3000);
                return false;
            }

            LoadData();

        } catch (e) {
            console.log(e);
        }
    });

    BindWorkOrderTypeDropDown();


    $('#btnClear').click(function () {
        try {
            ClearValues();
        } catch (e) {
            console.log(e);
        }
    });

    $('.close-btn').click(function () {
        try {
            Popup.hide('divWorkorderInfo');
        } catch (e) {
            console.log(e);
        }
    });

});

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

            //if (queryString["Type"] == undefined) {
            //    $GlobalData.status = queryString["Status"]

            //    if (($GlobalData.status != 'undefined') && ($GlobalData.status == 'DOC' || $GlobalData.status == 'COM' || $GlobalData.status == 'WIP' || $GlobalData.status == 'CLD' || $GlobalData.status == 'OPN')) {
            //        $GlobalData.OrderBy = 'CreatedDateDESC';
            //        $GlobalData.FromDate = $('#txtFromDate').val();
            //        $GlobalData.ToDate = $('#txtToDate').val();
            //        LoadData();
            //    }
            //    else {
            //        window.location.href = '/WO/WODI/SearchWorkOrder';
            //    }
            //}
            //else {
            //    var Type = queryString["Type"];
            //    if ($("#ddlDIType option[value='" + Type + "']").length > 0) {
            //        $('#ddlDIType').val(Type);
            //        $('#btnSearchWO').trigger('click');
            //    }
            //    else {
            //        window.location.href = '/WO/WODI/SearchWorkOrder';
            //    }
            //}

            if (queryString["Type"] == undefined && queryString["Status"] == undefined && queryString["Group"] == undefined && queryString["AssignedTo"] == undefined) {
                window.location.href = '/WO/WODI/SearchWorkOrder';
            }
            else {

                if (queryString["Type"] != undefined) {
                    var Type = queryString["Type"];
                    if ($("#ddlDIType option[value='" + Type + "']").length > 0) {
                        $('#ddlDIType').val(Type);
                        $('#btnSearchWO').trigger('click');
                    }
                    else {
                        window.location.href = '/WO/WODI/SearchWorkOrder';
                    }
                }
                else if (queryString["Status"] != undefined) {
                    $GlobalData.status = queryString["Status"]

                    if (($GlobalData.status != 'undefined') && ($GlobalData.status == 'DOC' || $GlobalData.status == 'COM' || $GlobalData.status == 'WIP' || $GlobalData.status == 'CLD' || $GlobalData.status == 'OPN')) {
                        $GlobalData.OrderBy = 'CreatedDateDESC';
                        $GlobalData.FromDate = $('#txtFromDate').val();
                        $GlobalData.ToDate = $('#txtToDate').val();

                        LoadData();
                    }
                    else {
                        window.location.href = '/WO/WODI/SearchWorkOrder';
                    }
                }
                else if (queryString["Group"] != undefined) {
                    $('#txtWorkOrderID').val(queryString["Group"]);
                    $('#btnSearchWO').trigger('click');

                }
                else if (queryString["AssignedTo"] != undefined) {
                    if ($("#ddlAssignedTo option[value='" + queryString["AssignedTo"] + "']").length > 0) {
                        $('#ddlAssignedTo').val(queryString["AssignedTo"]);
                        $('#btnSearchWO').trigger('click');
                    }
                    else {
                        window.location.href = '/WO/WODI/SearchWorkOrder';
                    }
                }

            }

        }

    } catch (e) {
        console.log(e);
    }

}




function ClearValues() {
    try {
        $('#ddlChosenClient_ddlClient').val('-1').trigger('chosen:updated');
        $('#ddlStatusCode').val('-1');
        $("#ddlPageSize").val("10");
        $('#ddlDIType').val('-1');
        $('#txtWorkOrderID').val('');
        $('#ddlOrderBy').val('CreatedDateDESC');
        ApplyDatePickerFromAndToDate($('#txtFromDate'), $('#txtToDate'));
        $('#chkAdhoc').attr('checked', false);
        $('#ddlAssignedTo').val('-1');

    } catch (e) {
        console.log(e);
    }
}

function LoadData() {
    try {
        SearchWOCallServices("GetSerchWODataBind", "WorkorderTemplate", "trWorkorderData", "{'ClientId':'" + $GlobalData.ClientId + "','SourceID':'" + $GlobalData.SourceID + "','WorkOrderID':'" + escape($.trim($GlobalData.WorkOrderID)) + "','statusCode':'" + $GlobalData.status + "','startpage':" + $GlobalData.startPage + ",'rowsperpage':" + $GlobalData.resultPerPage + ",'Type':'" + $GlobalData.Type + "','OrderBy':'" + $GlobalData.OrderBy + "','FromDate':'" + $GlobalData.FromDate + "','ToDate':'" + $GlobalData.ToDate + "','IsAdhoc':'" + $GlobalData.IsAdhoc + "','AssignedTo':'" + $GlobalData.AssignedTo + "'}", true, DataLoadCallBack);
    } catch (e) {
        console.log(e);
    }
}
function DataLoadCallBack() {
    try {
        var tblSearchDisbursementItemLength = $("#tblSearchWO").find("tr").length;
        $("#tblSearchWO").show();
        $("#ddlStatusCode").val($GlobalData.status);

        if (tblSearchDisbursementItemLength > 1) {
            $("#divSearchNoData").hide();
            $("#divPaging").show();
            $('#trWorkorderData').find('.aEdit').unbind('click');
            $('#trWorkorderData').find('.aEdit').click(EditDetails);
            $('#trWorkorderData').find('.aView').unbind('click');
            $('#trWorkorderData').find('.aView').click(ShowDetails);
            $('#trWorkorderData').find('.aDelete').unbind('click');
            $('#trWorkorderData').find('.aDelete').click(DeleteWorkorder);

            $("#trWorkorderData").find(".spanDescriptionToolTip").each(function () {
                var description = $(this).attr('desc')
                $(".spanDescription").html(description);
                $(this).attr("title", $(".spanDescription").html());

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
            $("#divSearchNoData").show();
        }

    } catch (e) {
        console.log(e);
    }
}

function EditDetails() {
    try {
        if ($GlobalData.InsertedID == 0) {
            ShowNotify('Invalid Session login again.', 'error', 3000);
            return false;
        }
        else {
            scrollToTop();
            var ID = $(this).attr("ID");
            window.location.href = "WorkOrder?ID=" + ID;
            return false;
        }
    } catch (e) {
        console.log(e);
    }
}

function ShowDetails() {
    try {
        var id = $(this).attr('ID');
        $("#labelID").text(id);

        var Type = $(this).attr('workordertypetext');
        $("#labelType").text(Type);

        var Description = $(this).attr('description');
        $("#labelDescription").text(Description);

        var ClientID = $(this).attr('clientid');
        $("#labelClientID").text(ClientID);

        var WorkorderID = $(this).attr('workorderid');
        $("#labelWorkOrderID").text(WorkorderID);

        var statusCode = $(this).attr('statusCode');

        $('#labelStatusCode').text(statusCode);

        $('#modal-form').modal('show');
        return false;

    } catch (e) {
        console.log(e);
    }
}

function DeleteWorkorder() {
    try {
        var id = $(this).attr('ID');
        var StatusCode = 'CLD';
        $("#dialog-confirm").removeClass('hide').dialog({
            resizable: false,
            modal: true,
            title_html: true,
            buttons: [
                {
                    html: "<i class='ace-icon fa fa-trash-o bigger-110'></i>&nbsp; Delete",
                    "class": "btn btn-danger btn-xs",
                    click: function () {

                        SearchWOCallServices("DeleteWorkOrderById", '', '', "{'ID':" + id + ",'StatusCode':'" + StatusCode + "'}", true, DeleteCallBack);
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

function DeleteCallBack() {
    try {
        $GlobalData.startPage = 0;
        $GlobalData.resultPerPage = 10;
        $GlobalData.Type = '';
        $GlobalData.ClientId = '-1';
        $GlobalData.WorkOrderID = '';
        $GlobalData.status = '-1';
        LoadData();
        ShowNotify('Success.', 'success', 2000);
        return false;
    } catch (e) {
        console.log(e);
    }
}

function scrollToTop() {
    $("html, body").animate({ scrollTop: 0 }, "slow");
    return false;
}
function BindWorkOrderTypeDropDown() {
    try {
        WOStatusAndAssignmentCallServices("GetCSS1UserDetailsForWOAssignment", "ScriptAssignedTo", "ddlAssignedTo", "{}", false, '');
        SearchWOCallServices("GetWorkOrderType", "DITypeScript", "ddlDIType", '{}', false, CheckQueryString);
        //CallServicesForMWOStatus("GetMWOStatus", "scriptStatusCode", "ddlStatusCode", '{}', false, CheckQueryString); // on 12-July-2015, changed to static values 
    } catch (e) {
        console.log(e);
    }
}

function SearchWOCallServices(path, templateId, containerId, parameters, clearContent, callBack) {
    ShowLoadNotify();
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

                    $GlobalData.InsertedID = msg;
                    if (msg.workorderCount != null && msg.workorderCount != 'undefined') {
                        $GlobalData.totalRow = msg.workorderCount;
                    }

                    if (templateId != '' && containerId != '') {
                        if (!clearContent) {
                            $.tmpl($('#' + templateId).html(), msg.WorkOrdersList).appendTo("#" + containerId);
                        }
                        else {
                            $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg.WorkOrdersList));
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

    HideLoadNotify();
}

function CallServices1(path, templateId, containerId, parameters, clearContent, callBack) {
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
function CallServicesForMWOStatus(path, templateId, containerId, parameters, clearContent, callBack) {
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
                    ShowNotify('Invalid Session login again.', 'error', 3000);
                    return false;
                }
                if (templateId != '' && containerId != '') {
                    if (!clearContent) {
                        $.tmpl($('#' + templateId).html(), msg.WOStatusAndAssignmentInfoList).appendTo("#" + containerId);
                    }
                    else {
                        $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg.WOStatusAndAssignmentInfoList));
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
function WOStatusAndAssignmentCallServices(path, templateId, containerId, parameters, clearContent, callBack) {
    ShowLoadNotify();
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

                    if (templateId != '' && containerId != '') {
                        if (!clearContent) {
                            $.tmpl($('#' + templateId).html(), msg.WOStatusAndAssignmentInfoList).appendTo("#" + containerId);
                        }
                        else {
                            $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg.WOStatusAndAssignmentInfoList));
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
    HideLoadNotify();
}
function GenerateNumericPaging() {
    try {
        var numericcontainer = $('#numericcontainer');
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