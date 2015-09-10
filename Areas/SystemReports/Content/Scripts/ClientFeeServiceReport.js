
$(document).ready(function () {

    BindFreeServiceTypeDropDown();
    SetPageAttributes('liSystemReports', 'System Reports', 'Client Fee Service Report', 'liClientFeeServiceReport');

    ApplyDatePickerFromAndToDate($('#txtFromDate'), $('#txtToDate'));

    $("#btnClear").click(function () {
        $("#txtClientNo").val("");
        $("#ddlFeeServiceType").val("");

        //$('#ddlGroupCode_InCWO').val('').trigger('chosen:updated');
        //$('#ddlChosenClient_ddlClient').val('').trigger('chosen:updated');

        ApplyDatePickerFromAndToDate($('#txtFromDate'), $('#txtToDate'));

        removeAllClients();
        $("#ddlGroupCode_InCWO").val('-1').trigger('chosen:updated');
        $('#ddlChosenClient_ddlClient').attr('groupcode', '-1');

    });

    $("#btnDownLoadCFSR").click(function () {
        var GroupCode = $("#ddlGroupCode_InCWO").find("option:Selected").text();

        var FromDate = $("#txtFromDate").val();
        var ToDate = $("#txtToDate").val();
        if (FromDate == '' || ToDate == '') {
            ShowNotify('Please select valid date range.', 'error', 3000);
            return false;
        }
        //if (GroupCode == '' || GroupCode == undefined || GroupCode == '-1') {
        //    ShowNotify('Please select SIC Group.', 'error', 3000);
        //    return false;
        //}

        $("#hdnCompayID").val($.trim($("#divClientOne .chosen-select1").find("option:Selected").attr('clientcode')));
        $("#hdnSource").val($.trim($("#divClientOne .chosen-select1").find("option:Selected").attr('sourceid')));
        $("#hdnGroupCode").val(GroupCode);
    });

    $("#ddlGroupCode_InCWO").change(function () {
        var Groupcode = $(this).val();
        //$('#ddlChosenClient_ddlClient').val('-1').trigger('chosen:updated');
        removeAllClients();
        $('#ddlChosenClient_ddlClient').attr('groupcode', Groupcode);
        $('#ddlChosenClient_ddlClient').trigger('chosen:updated');
    });

});

function removeAllClients() {
    $("#ddlChosenClient_ddlClient > option:not(:eq(0))").remove();
    $('#ddlChosenClient_ddlClient').val('-1').trigger('chosen:updated');
    $('#ddlChosenClient_ddlClient_chosen').find(".chosen-results").find("li").remove();
}

function BindFreeServiceTypeDropDown() {
    try {
        CallServices("/Billing/Billing/GetMFEE", 'FeeServiceTypeTemplate', 'ddlFeeServiceType', "{}", false, '');
    } catch (e) {
        console.log(e);
    }
}

function CallServices(path, templateId, containerId, parameters, clearContent, callBack) {

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
                    //$GlobalData.InsertedID = msg;

                    if (msg.OrdersCount != null && msg.OrdersCount != 'undefined') {

                        $GlobalData.totalRow = msg.OrdersCount;
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