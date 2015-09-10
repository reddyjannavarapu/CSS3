$(document).ready(function () {

    SetPageAttributes('liDI', 'DI', 'Search Disbursement Items', 'liSearchDI');

    $GlobalData = {};
    $GlobalData.resultPerPage = 10;
    $GlobalData.totalRow = 10;
    $GlobalData.startPage = 0;

    $GlobalData.clientName = '-1';
    $GlobalData.clientID = '';
    $GlobalData.WO = '';
    $GlobalData.venderRefId = '';
    $GlobalData.Type = '';
    $GlobalData.IsVerified = '';
    $GlobalData.IsBilled = '';
    $GlobalData.IsArchived = '';
    $GlobalData.IsAdhoc = '';
    $GlobalData.SourceID = '';
    $GlobalData.OrderBy = '';

    $GlobalData.ID = 0;
    $GlobalData.WorkOrderID = 0;
    $GlobalData.InsertedID = 0;

    $GlobalData.DisbursementIds = '';

    $GlobalData.IsAdhocBilling = false;
    $GlobalData.IsArchived = false;
    $GlobalData.DIInStates;
    $GlobalData.ActionRules;
    $GlobalData.FromDate = '';
    $GlobalData.ToDate = '';

    $GlobalData.ACCPACStatus = 0;

    ApplyDatePickerFromAndToDate($('#txtFromDate'), $('#txtToDate'));
        

    $('#btnSearch').on('click',function () {
        try {
            // $GlobalData.clientName = $("#divClientOne .chosen-select1").val();
            $GlobalData.ClientId = $.trim($("#divClientOne .chosen-select1").find("option:Selected").attr('clientcode'));
            $GlobalData.SourceID = $.trim($("#divClientOne .chosen-select1").find("option:Selected").attr('sourceid'));
            // $GlobalData.clientID = $('#txtClient').val();
            $GlobalData.WO = $('#txtWO').val();
            $GlobalData.venderRefId = $('#txtVenderRef').val();
            $GlobalData.Type = $("#ddlDIItemChosen_ddlDIItem").find("option:selected").val(); //$('#drpTypeData option:selected').val();
            $("#divPartial").hide();
            $GlobalData.startPage = 0;
            $GlobalData.resultPerPage = $("#ddlPageSize").val();
            if ($('#chkVerified').is(':checked')) {
                $GlobalData.IsVerified = '1';
            }
            else {
                $GlobalData.IsVerified = '';
            }

            if ($('#chkBilled').is(':checked')) {
                $GlobalData.IsBilled = '1';
            }
            else {
                $GlobalData.IsBilled = '';
            }

            if ($('#chkArchived').is(':checked')) {
                $GlobalData.IsArchived = '1';
            }
            else {
                $GlobalData.IsArchived = '';
            }

            if ($('#chkAdhoc').is(':checked')) {
                $GlobalData.IsAdhoc = '1';
            }
            else {
                $GlobalData.IsAdhoc = '';
            }
            
            if ($('#chkACCPACStatus').is(':checked')) {
                $GlobalData.ACCPACStatus = 1;
            }
            else {
                $GlobalData.ACCPACStatus = 0;
            }

            $GlobalData.OrderBy = $('#drpOrderBy option:selected').val();

            $GlobalData.FromDate = $.trim($("#txtFromDate").val());
            $GlobalData.ToDate = $.trim($("#txtToDate").val());
            $GlobalData.startPage = 0;
            LoadCallForSearch($GlobalData.ClientId, $GlobalData.SourceID, $GlobalData.WO, $GlobalData.venderRefId, $GlobalData.Type, $GlobalData.IsVerified, $GlobalData.IsBilled, $GlobalData.IsArchived, $GlobalData.IsAdhoc, $GlobalData.OrderBy, $GlobalData.startPage, $GlobalData.resultPerPage, $GlobalData.FromDate, $GlobalData.ToDate, $GlobalData.ACCPACStatus);

        } catch (e) {
            console.log(e);
        }
    });

    $('#btnReset').click(function () {
        try {
            ClearValues();
        } catch (e) {
            console.log(e);
        }
    });

    checkQueryStringofDI();

    function checkQueryStringofDI() {
        try {
            var queryString = [];
            if (window.location.search.split('?').length > 1) {
                var params = window.location.search.split('?')[1].split('&');
                for (var i = 0; i < params.length; i++) {
                    var key = params[i].split('=')[0];
                    var value = decodeURIComponent(params[i].split('=')[1]);
                    queryString[key] = value;
                }
                var ACCPACStatusofDI = queryString["ACCPACStatus"]
                
                if ((ACCPACStatusofDI != 'undefined') && (Math.floor(ACCPACStatusofDI) == ACCPACStatusofDI && $.isNumeric(ACCPACStatusofDI))) {
                    $('#chkACCPACStatus').attr('checked', true);
                    $("#ddlPageSize").val('50');
                    $('#btnSearch').trigger('click');
                }
                else {
                    window.location.href = '/WO/WODI/SearchDisbursementItems';
                }
            }

        } catch (e) {
            console.log(e);
        }
    }

});

function LoadData() {
    try {
        LoadCall($GlobalData.clientName, $GlobalData.WO, $GlobalData.venderRefId, $GlobalData.Type, $GlobalData.IsVerified, $GlobalData.IsBilled, $GlobalData.IsArchived, $GlobalData.IsAdhoc, $GlobalData.OrderBy, $GlobalData.startPage, $GlobalData.resultPerPage);
        ClearValues();
    } catch (e) {
        console.log(e);
    }
}

function ClearValues() {
    try {
        $('#ddlChosenClient_ddlClient').val('-1').trigger('chosen:updated');
        $('#ddlDIItemChosen_ddlDIItem').val('').trigger('chosen:updated');
        $('#drpOrderBy').val('CreatedDateDESC');
        $('#txtWO').val('');
        $('#txtVenderRef').val('');
        $("#ddlPageSize").val("10");
        $('#chkVerified').attr('checked', false);
        $('#chkBilled').attr('checked', false);
        $('#chkArchived').attr('checked', false);
        $('#chkAdhoc').attr('checked', false);
        $('#chkACCPACStatus').attr('checked', false);
        
        ApplyDatePickerFromAndToDate($('#txtFromDate'), $('#txtToDate'));

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
                    $GlobalData.InsertedID = msg;

                    if (msg.OrdersCount != null && msg.OrdersCount != 'undefined') {

                        $GlobalData.totalRow = msg.OrdersCount;
                    }
                    if (templateId != '' && containerId != '') {

                        if (!clearContent) {
                            $.tmpl($('#' + templateId).html(), msg.OrdersList).appendTo("#" + containerId);
                        }
                        else {
                            $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg.OrdersList));
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
function scrollToTop() {
    $("html, body").animate({ scrollTop: 0 }, "slow");
    return false;
}