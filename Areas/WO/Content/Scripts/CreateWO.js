
$(document).ready(function () {
    $GlobalData = {};
    $GlobalData.WorkOrderID = '';
    $GlobalData.WOCode = '';
    $GlobalData.GroupCode = '';
    $GlobalData.TypeStatus = true;
    $GlobalData.CompanySource = 'CSS1C';

    SetPageAttributes('liWorkOrder', 'WO', 'New Work Order', 'liCreateWorkOrder');

    var txtareaDescription = $('#txtDescription');
    textareaLimiter(txtareaDescription, 250);

    //CategoryBindCallBack(); // on 12-July-2015, changed to static values 

    $('#Add').click(function () {
        try {
            CreateWorkOrder();
        } catch (e) {
            console.log(e);
        }
    });
    $("#Cancel").click(function () {
        try {
            CancelEvent();
        } catch (e) {
            console.log(e);
        }
    });

    $("#ddlWOTypes").change(function () {
        removeAllClients();

        var IsTakeOverOrIncorp = $(this).val();
        if (IsTakeOverOrIncorp == "TAKE" || IsTakeOverOrIncorp == "INCO")
            $("#trClient").hide();
        else $("#trClient").show();

    });

    $('#ddlCategory').change(function () {
        try {
            removeAllClients();
            var checkCategory = $(this).find("option:selected").val();
            if (checkCategory != "-1" && checkCategory != undefined && checkCategory != '') {
                $GlobalData.TypeStatus = false
                CallServiceforWO("GetMWOTypeByCategoryCode", "scriptCategory", "ddlWOTypes", "{'CategoryCode':'" + checkCategory + "'}", false, enableType);
            }
            else if (checkCategory == '-1') {
                $('#ddlWOTypes').prop("disabled", true);
            }
        } catch (e) {
            console.log(e);
        }
    });

    $('#chkIsAdhoc').change(function () {
        removeAllClients();
    });

});

function removeAllClients() {
    $("#ddlChosenClient_ddlClient > option:not(:eq(0))").remove();
    $('#ddlClients .chosen-select1').val('-1').trigger('chosen:updated');
    $(".chosen-results").find("li").remove();
}
function enableType() {
    try {
        var length = $('#ddlWOTypes').find("option").length;
        if (length <= 1)
            $('#ddlWOTypes').prop("disabled", true);
        else $('#ddlWOTypes').prop("disabled", false);

        $GlobalData.TypeStatus = true;
    } catch (e) {
        console.log(e);
    }
}
function CategoryBindCallBack() {
    try {
        CallServiceforWO("GetMWOCategory", "scriptCategory", "ddlCategory", '{}', false, '');
    } catch (e) {
        console.log(e);
    }
}

function CancelEvent() {
    try {
        $("#ddlBillable").val("1");
        $("#chkIsAdhoc").prop("checked", false);
        $("#ddlWOTypes").val("-1");
        $("#txtDescription").val('');
        $("#ddlCategory").val("-1");
        $('#ddlWOTypes').prop("disabled", true);
        $('#ddlWOTypes').find("option:not(:first)").remove();
        $("#ddlGroupCode").val("-1");
        $('#ddlClients .chosen-select1').val('-1').trigger('chosen:updated');
        //$('#ddlCustomer .chosen-select1').val('-1').trigger('chosen:updated');
        $('#ddlGroupCode_InCWO').val('-1').trigger('chosen:updated');
        return false;
    } catch (e) {
        console.log(e);
    }
}
function CreateWorkOrder() {
    try {
        var Type = $('#ddlWOTypes').find("option:selected").val();
        var Desc = $('#txtDescription').val();
        var Billable = $("#ddlBillable").find("option:Selected").val();
        $GlobalData.Billable = Billable;
        var IsAdhoc = $("#chkIsAdhoc").prop("checked");
        var ClientID = $("#ddlClients").find("option:Selected").attr("ClientCode");
        var ClientSourceID = $("#ddlClients").find("option:Selected").attr("sourceid");
        var Category = $("#ddlCategory").find("option:Selected").val();

        var GroupCode = $("#ddlGroupCode_InCWO").find("option:Selected").val();
        $GlobalData.GroupName = $("#ddlGroupCode_InCWO").find("option:Selected").text();

        if (Category == '-1') {
            ShowNotify('Please select Category.', 'error', 3000);
            return false;
        }

        if ((Type == '-1' || Type == undefined || Type == '')) {
            ShowNotify('Please select Type.', 'error', 3000);
            return false;
        }

        if (Type == 'TAKE' && IsAdhoc == true) {
            ShowNotify('Cannot create Takeover Work Order for Adhoc.', 'error', 3000);
            return false;
        }
        // to enable validation for Adhoc wo also
        if ((IsAdhoc == true) && (ClientSourceID == undefined && ClientID == undefined)) {
            ClientID = 0;
            ClientSourceID = '';
        }

        if (Type != 'INCO' && Type != 'TAKE') {

            if (ClientSourceID == undefined && ClientID == undefined) {
                ShowNotify('Please select Client / Customer.', 'error', 3000);
                return false;
            }
        }
        else {
            if ((ClientSourceID == undefined && ClientID == undefined)) {
                ClientID = 0;
                ClientSourceID = '';
            }
        }

        if (GroupCode == '-1' || GroupCode == undefined || GroupCode == '') {
            ShowNotify('Please select Group Code.', 'error', 3000);
            return false;
        }
        CallServiceforCWO("InsertWorkOrder", "", "", "{'Type':'" + Type + "','Desc':'" + escape($.trim(Desc)) + "','Billable':" + parseInt(Billable) + ",'ClientOrCustomerID':'" + parseInt(ClientID) + "','SourceID':'" + ClientSourceID + "','GroupCode':'" + $GlobalData.GroupName + "','IsAdhoc':" + IsAdhoc + ",'StatusCode':'OPN','IsPostedToCss1':" + false + "}", true, DataLoadMaintainHidden);

    } catch (e) {
        console.log(e);
    }

}


function DataLoadMaintainHidden() {
    try {
        ShowNotify('Success.', 'success', 2000);
        window.location.href = "WorkOrder?ID=" + $GlobalData.WorkOrderID;
    } catch (e) {
        console.log(e);
    }
}

function CallServiceforCWO(path, templateId, containerId, parameters, clearContent, callBack) {
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
                    if (msg.OrdersList[0] != null && msg.OrdersList[0] != undefined) {

                        $GlobalData.WorkOrderID = $.trim(msg.OrdersList[0].ID);
                        $GlobalData.WOCode = $.trim(msg.OrdersList[0].WOCode);
                    }
                    else {
                        ShowNotify('WO already created with this Client / Customer.', 'error', 3000);
                        return false;
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

function CallServiceforWO(path, templateId, containerId, parameters, clearContent, callBack) {
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
                    $("#" + containerId + " > option:not(:eq(0))").remove();
                    if (templateId != '' && containerId != '') {

                        if (!clearContent) {
                            $.tmpl($('#' + templateId).html(), msg.WorkOrdersList).appendTo("#" + containerId);

                        }
                        else {
                            $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg.WorkOrdersList));
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
