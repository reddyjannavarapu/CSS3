$(document).ready(function () {

    // Yearly (Jan) Button Click Event
    $("#btnYearlyJan").click(function () {

        var ClientCode = $(".chosen-select1").find("option:Selected").attr("clientcode");
        var sourceid = $(".chosen-select1").find("option:Selected").attr("sourceid");
        if (ClientCode == '' || ClientCode == undefined) {
            ShowNotify('Please select Client.', 'error', 3000);
            return false;
        }

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

        InvoiceFromCss2CallService("ToSaveInvoiceFromCss2", "", "", "{'InvoiceSelection':'SYJ',Date:'" + Date + "','Month':" + Month + ",'ClientCode':'" + ClientCode + "','SourceID':'" + sourceid + "'}", false, InvoiceFromCss2CallBack);
    });

    //Half Yearly Button Click Event
    $("#btnYearlyHalfYearly").click(function () {
        var ClientCode = $(".chosen-select1").find("option:Selected").attr("clientcode");
        var sourceid = $(".chosen-select1").find("option:Selected").attr("sourceid");

        if (ClientCode == '' || ClientCode == undefined) {
            ShowNotify('Please select Client.', 'error', 3000);
            return false;
        }
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
        InvoiceFromCss2CallService("ToSaveInvoiceFromCss2", "", "", "{'InvoiceSelection':'SHY',Date:'" + Date + "','Month':" + Month + ",'ClientCode':'" + ClientCode + "','SourceID':'" + sourceid + "'}", false, InvoiceFromCss2CallBack);

    });

    //Anniversary Button Click Event
    $("#btnAnniversary").click(function () {
        var ClientCode = $(".chosen-select1").find("option:Selected").attr("clientcode");
        var sourceid = $(".chosen-select1").find("option:Selected").attr("sourceid");
        if (ClientCode == '' || ClientCode == undefined) {
            ShowNotify('Please select Client.', 'error', 3000);
            return false;
        }
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
        InvoiceFromCss2CallService("ToSaveInvoiceFromCss2", "", "", "{'InvoiceSelection':'SAS',Date:'" + Date + "','Month':" + Month + ",'ClientCode':'" + ClientCode + "','SourceID':'" + sourceid + "'}", false, InvoiceFromCss2CallBack);

    });

    //Yearly(Select) Button Click Event
    $("#btnYearlySelect").click(function () {
        var ClientCode = $(".chosen-select1").find("option:Selected").attr("clientcode");
        var sourceid = $(".chosen-select1").find("option:Selected").attr("sourceid");
        if (ClientCode == undefined || sourceid == undefined) {
            ClientCode = '';
            sourceid = '';
        }


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

        InvoiceFromCss2CallService("ToSaveInvoiceFromCss2", "", "", "{'InvoiceSelection':'SYS',Date:'" + Date + "','Month':" + Month + ",'ClientCode':'" + ClientCode + "','SourceID':'" + sourceid + "'}", false, InvoiceFromCss2CallBack);

    });

    //ADHOC Button Click Event
    $("#btnADHOC").click(function () {
        var ClientCode = $(".chosen-select1").find("option:Selected").attr("clientcode");
        var sourceid = $(".chosen-select1").find("option:Selected").attr("sourceid");

        if (ClientCode == undefined || sourceid == undefined) {
            sourceid = '';
            ClientCode = '';
        }

        InvoiceFromCss2CallService("ToSaveInvoiceFromCss2", "", "", "{'InvoiceSelection':'',Date:'','Month':" + 0 + ",'ClientCode':'" + ClientCode + "','SourceID':'" + sourceid + "'}", false, InvoiceFromCss2CallBack);
    });

    $("#ddlMonth").change(function () {

        var Month = $("#ddlMonth").val();

        if (Month == '1') {
            $('#btnYearlyJan').show();
            $('#btnYearlyJanSch').show();
        }
        else {
            $('#btnYearlyJan').hide();
            $('#btnYearlyJanSch').hide();
        }

        if (Month == '1' || Month == '7') {
            $('#btnYearlyHalfYearly').show();
            $('#btnYearlyHalfYearlySch').show();
        }
        else {
            $('#btnYearlyHalfYearly').hide();
            $('#btnYearlyHalfYearlySch').hide();
        }

    });

    $("#btnCSS1CSS2Integration").click(function () {
        InvoiceFromCss2CallService("InsertCSS2toCC1Data", "", "", "{}", false, InvoiceFromCss2CallBack);
    });

    // Yearly (Jan) (Schedule) Button Click Event
    $("#btnYearlyJanSch").click(function () {

        var ClientCode = $(".chosen-select1").find("option:Selected").attr("clientcode");
        var sourceid = $(".chosen-select1").find("option:Selected").attr("sourceid");
        
        ClientCode = (ClientCode == 'undefined' || ClientCode == undefined) ? '' : ClientCode;
        sourceid = (sourceid == 'undefined' || sourceid == undefined) ? '' : sourceid;

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

        InvoiceFromCss2CallService("ToSaveInvoiceFromCss2", "", "", "{'InvoiceSelection':'SYJ',Date:'" + Date + "','Month':" + Month + ",'ClientCode':'" + ClientCode + "','SourceID':'" + sourceid + "'}", false, InvoiceFromCss2CallBack);
    });

    //Half Yearly (Schedule) Button Click Event
    $("#btnYearlyHalfYearlySch").click(function () {
        var ClientCode = $(".chosen-select1").find("option:Selected").attr("clientcode");
        var sourceid = $(".chosen-select1").find("option:Selected").attr("sourceid");

        ClientCode = (ClientCode == 'undefined' || ClientCode == undefined) ? '' : ClientCode;
        sourceid = (sourceid == 'undefined' || sourceid == undefined) ? '' : sourceid;

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
        InvoiceFromCss2CallService("ToSaveInvoiceFromCss2", "", "", "{'InvoiceSelection':'SHY',Date:'" + Date + "','Month':" + Month + ",'ClientCode':'" + ClientCode + "','SourceID':'" + sourceid + "'}", false, InvoiceFromCss2CallBack);

    });

    //Anniversary Button Click Event
    $("#btnAnniversarySch").click(function () {
        var ClientCode = $(".chosen-select1").find("option:Selected").attr("clientcode");
        var sourceid = $(".chosen-select1").find("option:Selected").attr("sourceid");
        
        ClientCode = (ClientCode == 'undefined' || ClientCode == undefined) ? '' : ClientCode;
        sourceid = (sourceid == 'undefined' || sourceid == undefined) ? '' : sourceid;

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
        InvoiceFromCss2CallService("ToSaveInvoiceFromCss2", "", "", "{'InvoiceSelection':'SAS',Date:'" + Date + "','Month':" + Month + ",'ClientCode':'" + ClientCode + "','SourceID':'" + sourceid + "'}", false, InvoiceFromCss2CallBack);

    });

    //Yearly(Select) Button Click Event
    $("#btnYearlySelectSch").click(function () {
        var ClientCode = $(".chosen-select1").find("option:Selected").attr("clientcode");
        var sourceid = $(".chosen-select1").find("option:Selected").attr("sourceid");
        
        ClientCode = (ClientCode == 'undefined' || ClientCode == undefined) ? '' : ClientCode;
        sourceid = (sourceid == 'undefined' || sourceid == undefined) ? '' : sourceid;

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

        InvoiceFromCss2CallService("ToSaveInvoiceFromCss2", "", "", "{'InvoiceSelection':'SYS',Date:'" + Date + "','Month':" + Month + ",'ClientCode':'" + ClientCode + "','SourceID':'" + sourceid + "'}", false, InvoiceFromCss2CallBack);

    });

});




function InvoiceFromCss2CallBack() {
    ShowNotify('Success.', 'success', 3000);
    return false
}

function InvoiceFromCss2CallService(path, templateId, containerId, parameters, clearContent, callBack) {
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