
$(document).ready(function () {
    SetPageAttributes('liSystemReports', 'System Reports', 'Nominee Secretary Reports', 'liNomineeSecretaryReport');

    ApplyDatePickerFromAndToDate($('#txtFromDate'), $('#txtToDate'));

    //$('#txtClientNo').keypress(function (event) {
    //    try {
    //        var checkCharater = AllowNumbersOnly($(this).val(), event);
    //        if (!checkCharater) {
    //            event.preventDefault();
    //        }
    //    } catch (e) {
    //        console.log(e);
    //    }
    //});

    BindnomineeSecretary();

    $("#btnDownLoadNomineeReport").click(function () {
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

        $("#hdnCompayID").val($.trim($("#divClientOne .chosen-select1").find("option:Selected").attr('clientcode')));
        $("#hdnSource").val($.trim($("#divClientOne .chosen-select1").find("option:Selected").attr('sourceid')));
        $("#hdnGroupCode").val(GroupCode);
    });

    $("#btnClear").click(function () {
        removeAllClients();
        $('#ddlChosenClient_ddlClient').val('-1').trigger('chosen:updated');
        $('#ddlGroupCode_InCWO').val('-1').trigger('chosen:updated');
        $("#txtClientNo").val('');
        $("#ddlNomineeSecretary").val('-1');
        ApplyDatePickerFromAndToDate($('#txtFromDate'), $('#txtToDate'));
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

function BindnomineeSecretary() {
    CallServices('GetMFeesByFeeType', 'ScriptNomineeSecretary', 'ddlNomineeSecretary', "{'FeeType':'NS'}", false, '');
}

function removeAllClients() {
    $("#ddlChosenClient_ddlClient > option:not(:eq(0))").remove();
    $('#ddlChosenClient_ddlClient').val('-1').trigger('chosen:updated');
    $('#ddlChosenClient_ddlClient_chosen').find(".chosen-results").find("li").remove();
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
                            $.tmpl($('#' + templateId).html(), msg).appendTo("#" + containerId);
                        }
                        else {
                            $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg));
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