
$(document).ready(function () {
    BindDIitem();
    SetPageAttributes('liSystemReports', 'System Reports', 'Vendor Verification Report', 'liVendorVerificationReport');

    ApplyDatePickerFromAndToDate($('#txtFromDate'), $('#txtToDate'));


    $("#btnClear").click(function () {
        $("#ddlDIitem").val("");
        $("#ddlVRUpload").val("");
        $("#ddlDIItemStatus").val("");
        ApplyDatePickerFromAndToDate($('#txtFromDate'), $('#txtToDate'));

    });

    $("#btnDownLoadVVR").click(function () {
        var FromDate = $("#txtFromDate").val();
        var ToDate = $("#txtToDate").val();
        if (FromDate == '' || ToDate == '') {
            ShowNotify('Please select valid date range.', 'error', 3000);
            return false;
        }

        var VendorReportType = $("#ddlDIitem").find("option:Selected").val();
        if (VendorReportType == '' || VendorReportType == undefined || VendorReportType == 'undefined') {
            ShowNotify('Please select Vendor Report Type.', 'error', 3000);
            return false;
        }

        $("#DIitem").val(VendorReportType);
        $("#VRUpload").val($("#ddlVRUpload").find("option:Selected").val());
        $("#DIStatus").val($("#ddlDIItemStatus").find("option:Selected").val());
    });

    $('#ddlDIitem').change(function () {
        $("#ddlVRUpload > option:not(:eq(0))").remove();
        var Type = $(this).val();
        CallServices("/VReports/VReports/GetVendorUploadedDetailsByType", 'VRUploadTemplate', 'ddlVRUpload', "{'Type':'" + Type + "'}", false, '');
    });

});

function BindDIitem() {
    try {
        CallServices("/VReports/VReports/GetVendorReportType", 'TypeDropdownTemplate', 'ddlDIitem', "{}", false, '');
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
                            $.tmpl($('#' + templateId).html(), msg.VendorList).appendTo("#" + containerId);
                        }
                        else {
                            $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg.VendorList));
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
