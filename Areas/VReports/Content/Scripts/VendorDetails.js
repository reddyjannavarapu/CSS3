$(document).ready(function () {

    SetPageAttributes('liReports', 'Vendor Report', 'To Upload Vendor Reports', 'liVendorUpload');

    $GlobalData = {};

    $GlobalData.VendorId = 0;
    $GlobalData.VendorData = '';

    $GlobalData.startPage = 0;
    $GlobalData.resultPerPage = 10;
    $GlobalData.totalRow1 = 10;
    $GlobalData.totalRow2 = 10;
    $GlobalData.OrderBy = ''



    $('#mainDiv').show();
    $('#divVreportFormat').show();

    BindMainDiv();
    function BindMainDiv() {

        try {
            var queryString = [];
            if (window.location.search.split('?').length > 1) {
                var params = window.location.search.split('?')[1].split('&');
                for (var i = 0; i < params.length; i++) {
                    var key = params[i].split('=')[0];
                    if (key.toUpperCase() == "VRID") {
                        $('#mainDiv').hide();
                        $('#divVreportFormat').hide();
                    }

                    SetPageAttributes('liReports', 'Vendor Report', 'Vendor Report History', 'liVendorReport');
                }
            }
            else
                BindTypeDropDown();

        } catch (e) {
            console.log(e);
        }
    }


    $('#ddlVendorList').change(function () {
        try {
            ddlVendorListBind();
        } catch (e) {
            console.log(e);
        }
    });


    //Billed Discrepancy Verify Button Click
    $('#btnOverBilledVerify').click(function () {
        try {
            UpdateDIItems('VERIFY', 'OBDISC');
        } catch (e) {
            console.log(e);
        }
    });

    //Billed Discrepancy Verify & Send to ACCPAc FOR ADHOC Button Click
    $('#btnOverBilledVerifyAndADHOC').click(function () {
        try {
            UpdateDIItems('VERIFYADHOC', 'OBDISC');
        } catch (e) {
            console.log(e);
        }
    });

    //Billed Discrepancy Verify Button Click
    $('#btnBilledDisVerify').click(function () {
        try {
            UpdateDIItems('VERIFY', 'BDISC');
        } catch (e) {
            console.log(e);
        }
    });

    //Billed Discrepancy Verify & Send to ACCPAc FOR ADHOC Button Click
    $('#btnBilledDisVerifyAndADHOC').click(function () {
        try {
            UpdateDIItems('VERIFYADHOC', 'BDISC');
        } catch (e) {
            console.log(e);
        }
    });

    //UnBilled Discrepancy Verify Button Click
    $('#btnUnBilledDisVerify').click(function () {
        try {
            UpdateDIItems('VERIFY', 'UBDISC');
        } catch (e) {
            console.log(e);
        }
    });

    //UnBilled Discrepancy Verify & Send to ACCPAc FOR ADHOC Button Click
    $('#btnUnBilledDisVerifyAndADHOC').click(function () {
        try {
            UpdateDIItems('VERIFYADHOC', 'UBDISC');
        } catch (e) {
            console.log(e);
        }
    });

    //Multiple Discrepancy Verify & Send to ACCPAc FOR ADHOC Button Click
    $('#btnMultipleDisVerifyAndADHOC').click(function () {
        try {
            UpdateMultipleDIItems('VERIFYADHOC');
        } catch (e) {
            console.log(e);
        }
    });

    //Multiple Discrepancy Verify Button Click
    $('#btnMultipleDisVerify').click(function () {
        try {
            UpdateMultipleDIItems('VERIFY');
        } catch (e) {
            console.log(e);
        }
    });


    function UpdateMultipleDIItems(Action) {
        try {
            var VRID = '';
            var DIFields = new Array();
            var Checked = ''; var IstextNull = 'False';

            $("#chkMultiDisc:checked").each(function (index) {
                if ($(this).is(':checked')) {
                    Checked = 'true';
                    Vendor = {};

                    Vendor.DI = $(this).attr('MVenderId');
                    Vendor.VRDID = $(this).attr('VenderReferenceID');
                    Vendor.DescAmount = $("#txtDis" + Vendor.DI).val();
                    Vendor.Status = $(this).attr('Status').toUpperCase() == 'BILLED' ? 'INSERT' : 'UPDATE';
                    VRID = $(this).attr('VRID');
                    IstextNull == 'False' ? (Vendor.DescAmount == '' ? IstextNull = 'True' : 'False') : '';
                    DIFields[index] = Vendor;
                }
            });

            var jsonText = JSON.stringify({ DIFields: DIFields });

            if (Checked == '') {
                ShowNotify('Please select at least one checkbox to verify.', 'error', 6000);
                return false;
            }

            if (IstextNull == 'True' || IstextNull == '') {
                ShowNotify('Please enter discrepancy amount.', 'error', 6000);
                return false;
            }

            var jsonText = JSON.stringify({ DIFields: DIFields, VendorID: VRID, Action: Action });

            CallVendor("UpdateMultipleDI", 'scriptVendorDetails1', 'tblVendorBody1', 'scriptVendorDetails2', 'tblVendorBody2', 'scriptVendorDetails3',
                   'tblVendorBody3', 'scriptVendorDetails4', 'tblVendorBody4', 'scriptVendorDetails5', 'tblVendorBody5', 'scriptVendorDetails6', 'tblVendorBody6',
                   'scriptVendorDetails7', 'tblVendorBody7',
                     jsonText, true, VendorDetailsCallBack);

        } catch (e) {
            console.log(e);
        }
    }

    function UpdateDIItems(Action, DiscType) {
        try {
            var VRID = '';
            var txtDisc = '';
            var DIFields = new Array();
            var Checked = ''; var IstextNull = 'False';
            var checkBoxID = '';

            if (DiscType == 'BDISC') {
                checkBoxID = 'chkBilledDisc';
                txtDisc = 'txtBilledDisc';
            }
            else if (DiscType == 'UBDISC') {
                checkBoxID = 'chkUnBilledDisc';
                txtDisc = 'txtUnBilledDisc';
            }
            else if (DiscType == 'OBDISC') {
                checkBoxID = 'chkOverBilled';

            }

            if (DiscType != 'OBDISC') {

                $("#" + checkBoxID + ":checked").each(function (index) {
                    if ($(this).is(':checked')) {
                        Checked = 'true';
                        Vendor = {};
                        Vendor.DI = $(this).attr('di');
                        Vendor.VRDID = $(this).attr('VRDID');
                        VRID = $(this).attr('VRID');
                        Vendor.DescAmount = $(this).closest("tr").find($("[id*=" + txtDisc + "]")).val();
                        IstextNull == 'False' ? (Vendor.DescAmount == '' ? IstextNull = 'True' : 'False') : '';
                        DIFields[index] = Vendor;
                    }
                });
            }
            else {

                $("#" + checkBoxID + ":checked").each(function (index) {
                    if ($(this).is(':checked')) {
                        Checked = 'true';
                        Vendor = {};
                        Vendor.DI = 0;
                        Vendor.VRDID = $(this).attr('VenderReferenceID');
                        VRID = $(this).attr('VRID');
                        Vendor.DescAmount = '0';
                        IstextNull == 'False' ? (Vendor.DescAmount == '' ? IstextNull = 'True' : 'False') : '';
                        DIFields[index] = Vendor;
                    }
                });

            }

            if (Checked == '') {
                ShowNotify('Please select at least one checkbox to verify.', 'error', 6000);
                return false;
            }

            if (IstextNull == 'True') {
                ShowNotify('Please enter discrepancy.', 'error', 6000);
                return false;
            }

            var jsonText = JSON.stringify({ DIFields: DIFields, VendorID: VRID, Action: Action, DiscType: DiscType });

            CallVendor("UpdateDI", 'scriptVendorDetails1', 'tblVendorBody1', 'scriptVendorDetails2', 'tblVendorBody2', 'scriptVendorDetails3',
                    'tblVendorBody3', 'scriptVendorDetails4', 'tblVendorBody4', 'scriptVendorDetails5', 'tblVendorBody5', 'scriptVendorDetails6', 'tblVendorBody6',
                    'scriptVendorDetails7', 'tblVendorBody7',
                      jsonText, true, VendorDetailsCallBack);

        } catch (e) {
            console.log(e);
        }
    }


    $('#ddlType').change(function () {
        try {
            var ddlType = $("#ddlType").val();
            $("#Title").val(ddlType);
        } catch (e) {
            console.log(e);
        }
    });

    $('#btnUpload').click(function () {
        try {
            if ($("#Title").val() == "") {
                ShowNotify('Please select Type.', 'error', 6000);
                return false;
            }

            if ($("#fileupload").val() == "") {
                ShowNotify('Please upload file.', 'error', 6000);
                return false;
            }
        } catch (e) {
            console.log(e);
        }
    });



    function CallServices(path, templateId, containerId, parameters, clearContent, callBack) {
        try {
            $.ajax({
                type: "POST",
                url: path,
                data: parameters,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                cache: true,
                success: function (msg) {

                    if (templateId != '' && containerId != '') {

                        if (!clearContent) {
                            $.tmpl($('#' + templateId).html(), msg.VendorList).appendTo("#" + containerId);
                        }
                        else {
                            $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg.VendorList));
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

    function BindTypeDropDown() {
        try {
            CallServices("GetVendorReportType", 'TypeDropdownTemplate', 'ddlType', "{}", false, '');
        } catch (e) {
            console.log(e);
        }
    }

    $("#btnVreportFormat").click(function () {
        try {

            $('.ace-scroll').ace_scroll({ size: 400, styleClass: 'scroll-visible' });

            $('#dvbtnVreportFromat').modal({
                "backdrop": "static",
                "show": "true"
            });


        } catch (e) {
            console.log(e);
        }
    });

    $('#btnSearchVendorDetails').click(function () {
        LoadVendorDetailsHistory();
    });

    $('#btnClear').click(function () {
        $('#ddlGroupCode_InCWO').val('').trigger('chosen:updated');
        $('#ddlChosenClient_ddlClient').val('').trigger('chosen:updated');
        $('#txtReferencwNo').val('');
    });


});


LoadVendorDetailsHistory();

function LoadVendorDetailsHistory() {
    try {
        var queryString = [];
        if (window.location.search.split('?').length > 1) {
            var params = window.location.search.split('?')[1].split('&');
            for (var i = 0; i < params.length; i++) {
                var key = params[i].split('=')[0];
                var value = decodeURIComponent(params[i].split('=')[1]);
                queryString[key] = value;
            }
            var IsInteger = $.isNumeric(queryString["VRID"]);
            if (IsInteger == false) {
                return false;
            }
            else {
                LoadVendorReport(queryString["VRID"])
            }

        }
    } catch (e) {
        console.log(e);
    }
}

function CallVendor(path, templateId1, containerId1, templateId2, containerId2,
                    templateId3, containerId3, templateId4, containerId4, templateId5,
                    containerId5, templateId6, containerId6, templateId7, containerId7, parameters, clearContent,
                    callBack) {
    try {
        $.ajax({
            type: "POST",
            url: path,
            data: parameters,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            cache: true,
            success: function (msg) {
                try {

                    if (msg == '0') {
                        ShowNotify('Invalid session login again.', 'error', 6000);
                        return false;
                    }

                    if (msg.ExceptionMessage != null) {
                        ShowNotify('Please upload a valid .xlsx file.', 'error', 6000);
                        return false;
                    }

                    if (msg.IsAlreadyUploaded == 'TRUE') {
                        ShowNotify('In file not find new records or already uploaded.', 'error', 6000);
                        return false;
                    }
                    else if (msg.IsAlreadyUploaded == 'FALSE')
                        ShowNotify('Success.', 'success', 3000);


                    if (templateId1 != '' && containerId1 != '' && msg.MatchedDIList != undefined) {
                        if (!clearContent) {
                            $.tmpl($('#' + templateId1).html(), msg.MatchedDIList).appendTo("#" + containerId1);
                            $.tmpl($('#' + templateId2).html(), msg.UnMatchedDIList).appendTo("#" + containerId2);
                            $.tmpl($('#' + templateId3).html(), msg.OverBilledList).appendTo("#" + containerId3);
                            $.tmpl($('#' + templateId4).html(), msg.BilledDiscrepancyList).appendTo("#" + containerId4);
                            $.tmpl($('#' + templateId5).html(), msg.UnbilledDiscrepancyList).appendTo("#" + containerId5);
                            $.tmpl($('#' + templateId6).html(), msg.MultpleUpdatedDiscrepancy).appendTo("#" + containerId6);
                            $.tmpl($('#' + templateId7).html(), msg.ErrorList).appendTo("#" + containerId7);

                            $("#spnFileName").text(msg.FileName);
                            if (callBack != undefined && callBack != '')
                                callBack();
                        }
                        else {
                            $("#" + containerId1).html($.tmpl($('#' + templateId1).html(), msg.MatchedDIList));
                            $("#" + containerId2).html($.tmpl($('#' + templateId2).html(), msg.UnMatchedDIList));
                            $("#" + containerId3).html($.tmpl($('#' + templateId3).html(), msg.OverBilledList));
                            $("#" + containerId4).html($.tmpl($('#' + templateId4).html(), msg.BilledDiscrepancyList));
                            $("#" + containerId5).html($.tmpl($('#' + templateId5).html(), msg.UnbilledDiscrepancyList));
                            $("#" + containerId6).html($.tmpl($('#' + templateId6).html(), msg.MultpleUpdatedDiscrepancy));
                            $("#" + containerId7).html($.tmpl($('#' + templateId7).html(), msg.ErrorList));

                            $("#spnFileName").text(msg.FileName);
                            if (callBack != undefined && callBack != '')
                                callBack();
                        }
                    }
                    else {

                        var result = $.parseJSON(msg);

                        $.each(result, function (index, data) {

                            if (data[0] != undefined) {
                                IsEmpty = '1';

                                var Title = '<div style="margin:15px 0px 10px;"><h5 class="blue bigger"></h5></div>';
                                $('#dvErrorVReport').append(Title);

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

                                    for (var j = 0; j < cols.length; j++) {
                                        if (i == 0) {
                                            th.append('<th>' + cols[j] + '</th>');
                                        }
                                        var columnName = cols[j];
                                        tr.append('<th>' + (row[columnName] == null ? " " : row[columnName]) + '</th>');
                                    }

                                    if (i == 0) {
                                        tr2 = $('<tr></tr>');
                                        tr2.append('<td colspan=' + cols.length + '"><span style="color: red;text-align:center;">We are expecting the above headers in excel but uploaded excel is having below headers. Please upload valid excel</span></td>');
                                        table.append(thbody.append(th));
                                        table.append(thbody.append(tr2));
                                    }

                                    table.append(thbody.append(tr));

                                    $('#dvErrorVReport').append(table);

                                }
                            }
                        });

                        $('.ace-scroll').ace_scroll({ size: 400, styleClass: 'scroll-visible' });
                        $('#dvErrorVReportFromat').modal({
                            "backdrop": "static",
                            "show": "true"
                        });


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


function VendorDetailsCallBack() {
    try {

        $('.divFU').hide();
        $('#btnVreportFormat').hide();

        var vendorMatchedTableLength = $("#tblVendorDetails1").find("tr").length;
        var vendorUnmatchedTableLength = $("#tblVendorDetails2").find("tr").length;
        var vendorOverBilledTableLength = $("#tblVendorDetails3").find("tr").length;
        var vendorSingleDiscrepancyTableLength = $("#tblVendorDetails4").find("tr").length;
        var SingleUnbilledDiscrepancyTableLength = $("#tblVendorDetails5").find("tr").length;
        var MultipleDiscrepancyTableLength = $("#tblVendorDetails6").find("tr").length;
        var ErrorTabLength = $("#tblVendorDetails7").find("tr").length;


        $('#ddlVendorList').empty();
        if (vendorMatchedTableLength > 2) {

            $('#divFilter').show();
            $('#dvSearchfilter').show();
            $('#ddlVendorList')
                    .append($("<option></option>")
                    .attr("value", "MDI")
                    .text("Matched DI Records (" + (vendorMatchedTableLength - 2) + ")"));

        }
        else {
            $("#tblVendorDetails1").hide();
            $("#DivMatched").hide();
        }


        if (vendorUnmatchedTableLength > 1) {
            $('#divFilter').show();
            $('#dvSearchfilter').show();
            $('#ddlVendorList')
                   .append($("<option></option>")
                   .attr("value", "UMDI")
                   .text("UnMatched DI Records (" + (vendorUnmatchedTableLength - 1) + ")"));
        }
        else {
            $("#tblVendorDetails2").hide();
            $("#DivUnMatched").hide();
        }

        if (vendorOverBilledTableLength > 2) {
            $('#divFilter').show();
            $('#dvSearchfilter').show();
            $('#ddlVendorList')
                  .append($("<option></option>")
                  .attr("value", "OBDI")
                  .text("Over Billed (" + (vendorOverBilledTableLength - 2) + ")"));
        }
        else {
            $("#tblVendorDetails3").hide();
            $("#DivOverBilled").hide();

            $("#btnOverBilledVerifyAndADHOC").hide();
            $("#btnOverBilledVerify").hide();

        }


        if (vendorSingleDiscrepancyTableLength > 2) {
            $('#divFilter').show();
            $('#dvSearchfilter').show();
            $('#ddlVendorList')
                 .append($("<option></option>")
                 .attr("value", "SBD")
                 .text("Billed Discrepancy (" + (vendorSingleDiscrepancyTableLength - 2) + ")"));
        }
        else {
            $("#tblVendorDetails4").hide();
            $("#DivSingleDiscrepancy").hide();
            $('#btnBilledDisVerify').hide();
            $('#btnBilledDisVerifyAndADHOC').hide();
        }


        if (SingleUnbilledDiscrepancyTableLength > 2) {
            $('#divFilter').show();
            $('#dvSearchfilter').show();
            $('#ddlVendorList')
               .append($("<option></option>")
               .attr("value", "SUBD")
               .text("Unbilled Discrepancy (" + (SingleUnbilledDiscrepancyTableLength - 2) + ")"));

        }
        else {
            $("#tblVendorDetails5").hide();
            $("#DivSingleUnbilledDiscrepancy").hide();

            $('#btnUnBilledDisVerify').hide();
            $('#btnUnBilledDisVerifyAndADHOC').hide();
        }


        if (MultipleDiscrepancyTableLength > 2) {
            $('#divFilter').show();
            $('#dvSearchfilter').show();
            $('#ddlVendorList')
              .append($("<option></option>")
              .attr("value", "MD")
              .text("Multiple Discrepancy (" + (MultipleDiscrepancyTableLength - 2) + ")"));


        }
        else {
            $("#tblVendorDetails6").hide();
            $("#DivMultipleDiscrepancy").hide();

            $('#btnMultipleDisVerify').hide();
            $('#btnMultipleDisVerifyAndADHOC').hide();
        }

        if (ErrorTabLength > 1) {
            $('#divFilter').show();
            $('#dvSearchfilter').show();
            $('#ddlVendorList')
              .append($("<option></option>")
              .attr("value", "EL")
              .text("Error List (" + (ErrorTabLength - 1) + ")"));


        }
        else {
            $("#tblVendorDetails7").hide();
            $("#DivMultipleDiscrepancy").hide();

            $('#btnMultipleDisVerify').hide();
            $('#btnMultipleDisVerifyAndADHOC').hide();
        }
        $('div#mainDiv').css("min-height", "");
        ddlVendorListBind();

    } catch (e) {
        console.log(e);
    }
}

function LoadVendorReport(VRID) {
    try {
        var GroupCode = $("#ddlGroupCode_InCWO").find("option:Selected").text();
        var ClientName = $.trim($("#divClientOne .chosen-select1").find("option:Selected").text());

        var Client = [];
        Client = ClientName.split('-');
        ClientName = (Client[1] != undefined) ? $.trim(Client[1]) : '';

        var VendorRefNo = $.trim($('#txtReferencwNo').val());

        CallVendor("LoadVendorUploadHistory", 'scriptVendorDetails1', 'tblVendorBody1', 'scriptVendorDetails2', 'tblVendorBody2', 'scriptVendorDetails3',
                       'tblVendorBody3', 'scriptVendorDetails4', 'tblVendorBody4', 'scriptVendorDetails5', 'tblVendorBody5', 'scriptVendorDetails6', 'tblVendorBody6',
                       'scriptVendorDetails7', 'tblVendorBody7',
                       "{'VRID':'" + VRID + "','GroupCode':'" + GroupCode + "','ClientName':'" + ClientName + "','VendorRef':'" + escape(VendorRefNo) + "'}", true, VendorDetailsCallBack);

    } catch (e) {
        console.log(e);
    }
}

function UploadCall(FileName) {
    try {
        var fName = FileName.split('|');

        CallVendor("GetVendorUploadDetails", 'scriptVendorDetails1', 'tblVendorBody1', 'scriptVendorDetails2', 'tblVendorBody2', 'scriptVendorDetails3',
                     'tblVendorBody3', 'scriptVendorDetails4', 'tblVendorBody4', 'scriptVendorDetails5', 'tblVendorBody5', 'scriptVendorDetails6', 'tblVendorBody6',
                     'scriptVendorDetails7', 'tblVendorBody7',
                     "{'fileName':'" + fName[0] + "','type':'" + fName[1] + "','file':'" + fName[2] + "'}", true, VendorDetailsCallBack);
    } catch (e) {
        console.log(e);
    }
}

function ddlVendorListBind() {

    try {
        HideAllVendorLists();
        var List = $("#ddlVendorList").val();

        if (List == "MDI") {
            $("#tblVendorDetails1").show();
            $("#DivMatched").show();

        } else if (List == "UMDI") {
            $("#tblVendorDetails2").show();
            $("#DivUnMatched").show();
        }
        else if (List == "OBDI") {
            $("#tblVendorDetails3").show();
            $("#DivOverBilled").show();

            $("#btnOverBilledVerify").show();
            //$("#btnOverBilledVerifyAndADHOC").show();

        }
        else if (List == "SBD") {
            $("#tblVendorDetails4").show();
            $("#DivSingleDiscrepancy").show();

            $('#btnBilledDisVerify').show();
            $('#btnBilledDisVerifyAndADHOC').show();
        }
        else if (List == "SUBD") {
            $("#tblVendorDetails5").show();
            $("#DivSingleUnbilledDiscrepancy").show();

            $('#btnUnBilledDisVerify').show();
            $('#btnUnBilledDisVerifyAndADHOC').show();
        }
        else if (List == "MD") {
            $('#btnMultipleDisVerify').show();
            $('#btnMultipleDisVerifyAndADHOC').show();

            $("#tblVendorDetails6").show();
            $("#DivMultipleDiscrepancy").show();
        }
        else if (List == "EL") {
            $("#tblVendorDetails7").show();
            $("#ErrorList").show();
        }

    } catch (e) {
        console.log(e);
    }
}

function HideAllVendorLists() {
    try {
        $('#btnBilledDisVerify').hide();
        $('#btnBilledDisVerifyAndADHOC').hide();


        $('#btnOverBilledVerify').hide();
        $('#btnOverBilledVerifyAndADHOC').hide();

        $('#btnUnBilledDisVerify').hide();
        $('#btnUnBilledDisVerifyAndADHOC').hide();

        $('#btnMultipleDisVerify').hide();
        $('#btnMultipleDisVerifyAndADHOC').hide();

        $("#tblVendorDetails1").hide();
        $("#DivMatched").hide();

        $("#tblVendorDetails2").hide();
        $("#DivUnMatched").hide();

        $("#tblVendorDetails3").hide();
        $("#DivOverBilled").hide();

        $("#tblVendorDetails4").hide();
        $("#DivSingleDiscrepancy").hide();

        $("#tblVendorDetails5").hide();
        $("#DivSingleUnbilledDiscrepancy").hide();

        $("#tblVendorDetails6").hide();
        $("#DivMultipleDiscrepancy").hide();

        $("#tblVendorDetails7").hide();
        $("#ErrorList").hide();

    } catch (e) {
        console.log(e);
    }

}

