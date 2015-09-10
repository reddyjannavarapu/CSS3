
$(document).ready(function () {
    SetPageAttributes('liFeeDIBilledThroughScheduledBillings', 'System Reports', 'UnBilled Items - Fee/DI', 'FeeDisbursementItemsReportBilledThroughScheduledBillings');

    ApplyDatePickerFromAndToDate($('#txtFromDate'), $('#txtToDate'));

    $("#btnDownLoadFeeAndDI").click(function () {
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
        $('#ddlChosenClient_ddlClient').val('-1').trigger('chosen:updated');
        $('#ddlGroupCode_InCWO').val('-1').trigger('chosen:updated');
        $("#txtFromDate").val('');
        $("#txtToDate").val('');
        $("#txtClientNo").val('');
        
        ApplyDatePickerFromAndToDate($('#txtFromDate'), $('#txtToDate'));
    });

});