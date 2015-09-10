
$(document).ready(function () {
    SetPageAttributes('liSystemReports', 'System Reports', 'Disbursement Items Reports', 'liDisbursementItemsReport');
    
    ApplyDatePickerFromAndToDate($('#txtFromDate'), $('#txtToDate'));

    var txtareaDescription = $('#txtDIDescription');
    textareaLimiter(txtareaDescription, 250);

   
    $("#btnDownLoadDI").click(function () {
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
        $("#ItemNo").val($("#ddlDIItemChosen_ddlDIItem").find("option:Selected").text());
    });

    $("#btnClear").click(function () {
        //$('#ddlChosenClient_ddlClient').val('-1').trigger('chosen:updated');
        //$('#ddlGroupCode_InCWO').val('-1').trigger('chosen:updated');
        $('#ddlDIItemChosen_ddlDIItem').val('-1').trigger('chosen:updated');
        $("#txtFromDate").val('');
        $("#txtToDate").val('');
        $("#txtDIDescription").val('');
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