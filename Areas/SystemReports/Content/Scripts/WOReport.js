
$(document).ready(function () {
    SetPageAttributes('liSystemReports', 'System Reports', 'WO Report', 'liWOReport');

    CallServiceForWOReport("/WODI/GetMWOStatus", "scriptStatus", "ddlWOStatus", "{}", false, '');
    CallServiceForWOReport("/WODI/GetCSS1UserDetailsForWOAssignment", "ScriptAssignee", "ddlAssignee", "{}", false, '');

    ApplyDatePickerFromAndToDate($('#txtFromDate'), $('#txtToDate'));


    $("#btnDownLoadVVR").click(function () {
        var FromDate = $("#txtFromDate").val();
        var ToDate = $("#txtToDate").val();
        if (FromDate == '' || ToDate == '') {
            ShowNotify('Please select valid date range.', 'error', 3000);
            return false;
        }

        var GroupCode = $("#ddlGroupCode_InCWO").find("option:Selected").text();
        //if (GroupCode == '' || GroupCode == undefined || GroupCode == '-1') {
        //    ShowNotify('Please select SIC Group.', 'error', 3000);
        //    return false;
        //}

        $("#hdnCompayID").val($.trim($("#ddlChosenClient_ddlClient").find("option:Selected").attr('clientcode')));
        $("#hdnSource").val($.trim($("#ddlChosenClient_ddlClient").find("option:Selected").attr('sourceid')));
        $("#hdnGroupCode").val(GroupCode);
    });
    $("#btnClear").click(function () {
        //$('#ddlChosenClient_ddlClient').val('-1').trigger('chosen:updated');
        //$('#ddlGroupCode_InCWO').val('-1').trigger('chosen:updated');
        $("#txtFromDate").val('');
        $("#txtToDate").val('');
        $("#ddlAssignee").val('');
        $("#ddlWOStatus").val('');
        $("#ddlBillable").val('');
        $("#txtClientNo").val('');
        ApplyDatePickerFromAndToDate($('#txtFromDate'), $('#txtToDate'));

        removeAllClients();
        $("#ddlGroupCode_InCWO").val('-1').trigger('chosen:updated');
        $('#ddlChosenClient_ddlClient').attr('groupcode', '-1');
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

function CallServiceForWOReport(path, templateId, containerId, parameters, clearContent, callBack) {
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
}