
$(document).ready(function () {

    SetPageAttributes('liSystemReports', 'System Reports', 'Future Bill Report', 'liFutureBillReport');

    CallFutureBillReport("GetMSchedule", "scriptBillingFrequency", "ddlBillingFrequency", "{}", false, '');

    CallFutureBillReport("/Billing/Billing/GetMFEE", 'FeeServiceTypeTemplate', 'ddlFeeServiceType', "{}", false, '');

    var date = new Date();
    for (var i = 0; i <= 5; i++) {
        var year = date.getFullYear() + i;
        $('#ddlYear').append($('<option>', { value: year, text: year }));
    }

    $("#btnDownLoadFutureBill").click(function () {

        var ClientCode = $("#divClientOne .chosen-select1").find("option:Selected").attr('clientcode');
        var SourceID = $("#divClientOne .chosen-select1").find("option:Selected").attr('sourceid');
        var BillingFreq = $('#ddlBillingFrequency').val();
        var GroupCode = $("#ddlGroupCode_InCWO").find("option:Selected").val();

        //if ((ClientCode == '' || ClientCode == undefined) || (SourceID == '' || SourceID == undefined)) {
        //    ShowNotify('Please select all the Manditory fields.', 'error', 3000);
        //    return false;
        //}
        //else if (BillingFreq == '') {
        //    ShowNotify('Please select all the Manditory fields.', 'error', 3000);
        //    return false;
        //}
        //else {        

        if (GroupCode == '' || GroupCode == undefined || GroupCode == '-1') {
            ShowNotify('Please select SIC Group', 'error', 3000);
            return false;
        }

        if (BillingFreq == '') {
            ShowNotify('Please select Frequency.', 'error', 3000);
            return false;
        }

        $("#hdnCompayID").val(ClientCode);
        $("#hdnSource").val(SourceID);
        $("#hdnFeeServiceType").val($.trim($('#ddlFeeServiceType').val()));
        $("#hdnBillingFreq").val(BillingFreq);
        $("#hdnGroupCode").val(GroupCode);

        var Month = $("#ddlMonth").val();
        var Year = $("#ddlYear").val();

        if (Month == '0') {
            ShowNotify('Please select Month.', 'error', 3000);
            return false
        }
        if (Year == '0') {
            ShowNotify('Please select Year.', 'error', 3000);
            return false
        }

        var Date = '1/' + Month + '/' + Year;
        $("#hdnDate").val(Date);
        $("#hdnMonth").val(Month);

        //}

    });

    $("#btnClear").click(function () {
        //$('#ddlChosenClient_ddlClient').val('-1').trigger('chosen:updated');
        removeAllClients();
        $("#ddlBillingFrequency").val('');
        $("#ddlFeeServiceType").val('');
        $("#ddlGroupCode_InCWO").val('-1').trigger('chosen:updated');
        $('#ddlChosenClient_ddlClient').attr('groupcode', '-1');
        $("#ddlMonth").val('0');
        $("#ddlYear").val('0');
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

function CallFutureBillReport(path, templateId, containerId, parameters, clearContent, callBack) {
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