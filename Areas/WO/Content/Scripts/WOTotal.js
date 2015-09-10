$(document).ready(function () {

    $GlobalDataWOStatusAndAssignment = {};

    $GlobalDataWOStatusAndAssignment.WOCode = '';
    $GlobalDataWOStatusAndAssignment.WOID = '';
    $GlobalDataWOStatusAndAssignment.AssignedTo = '';
    $GlobalDataWOStatusAndAssignment.GroupCode = '';
    $GlobalDataWOStatusAndAssignment.SavedStatus = 0;
    $GlobalDataWOStatusAndAssignment.StatusCode = '';
    $GlobalDataWOStatusAndAssignment.ClientCode = '';
    $GlobalDataWOStatusAndAssignment.ClientName = '';
    $GlobalDataWOStatusAndAssignment.SourceID = '';
    $GlobalDataWOStatusAndAssignment.Comment = '';
    $GlobalDataWOStatusAndAssignment.IsBillable


    $GlobalData = {};
    $GlobalDataWOTYPE = {};
    $GlobalFeeData = {};
    $GlobalFeeData.IsPreviewTab = false;
    $GlobalDataWOTYPE.Type = ''
    $GlobalDataWOTYPE.WOID = ''
    $GlobalDataWOTYPE.IsClientOrCustomer = '';
    $GlobalData.IsPreviewTab = false;
    $GlobalData.Type = ''
    $GlobalData.startpage = '0'
    $GlobalData.rowsperpage = '10'
    $GlobalData.InsertedID = '0'
    $GlobalData.ID = '0'
    $GlobalData.WorkOrderID = '0'
    $GlobalData.Description = ''
    $GlobalData.StatusCode = ''
    $GlobalData.WorkOrderNumber = ''
    $GlobalData.ClientNumber = ''
    $GlobalData.HiddenWO = ''
    $GlobalData.GroupName = '';
    $GlobalData.Year = '';
    $GlobalData.LatestWOID = '';
    $GlobalData.Billable = '';
    $GlobalData.CurrentWOID = '';
    $GlobalData.WOCode = '';
    $GlobalData.Billable = false
    $GlobalData.ClientName = '';
    $GlobalData.WorkOrderIDStatus = false;
    $GlobalData.PartialViewGenerator = '';
    $GlobalData.GroupCode = '';
    $GlobalData.IsBillable = '';
    $GlobalData.IsPostedToCss1 = '';
    $GlobalData.IsAdhoc = '';
    $GlobalData.CategoryCode = '';
    $GlobalData.WorkOrderType = '';
    $GlobalData.ClientCode = '';
    $GlobalData.WOList = '';
    $GlobalData.WOStatusList = '';
    $GlobalData.AssignedTo = '';
    $GlobalData.WOAssignmentList = '';
    $GlobalData.IsClient = false;
    $GlobalData.SourceID = '';
    $GlobalData.WorkOrderTypeText = '';
    $GlobalData.CompanySource = '';
    $GlobalData.BillingParty = '';
    $GlobalData.IsHavingAccess = 1;

    $GlobalDataWOStatusAndAssignment.IncorpFieldsValidationCount = 0;


    ValidateWorkOreder();

    function ValidateWorkOreder() {
        var queryString = [];
        if (window.location.search.split('?').length > 1) {
            var params = window.location.search.split('?')[1].split('&');
            for (var i = 0; i < params.length; i++) {
                var key = params[i].split('=')[0];
                var value = decodeURIComponent(params[i].split('=')[1]);
                queryString[key] = value;
            }
            var WorkOrderID = queryString["ID"]
            var IsInteger = $.isNumeric(WorkOrderID);
            $GlobalData.WorkOrderID = WorkOrderID;
            $("#hdnWOID").val($GlobalData.WorkOrderID);
            $("#hdnWorkOrderID").val($GlobalData.WorkOrderID);

            if (IsInteger == false)
                return false;

            if (WorkOrderID != '' && WorkOrderID != undefined) {
                CallServicesForValidation("ValidateWorkOrederById", '', '', "{'ID':" + WorkOrderID + "}", false, BindStatus);
            }

        }
    }

    function CallServicesForValidation(path, templateId, containerId, parameters, clearContent, callBack) {
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
                        else
                            if (msg == '3')//Invalid Access
                            {
                                //alert("This Work Order has already closed.");
                                //window.location.href = "SearchWorkOrder";
                                $('#hdnWOCloseStatus').val('hide');
                                $('.btnWOClose').hide();

                                if (callBack != undefined && callBack != '')
                                    callBack();

                                if ($('#hdnUserRole').val() == 'User') {
                                    $("#tabWO").show();
                                    $("#tabDetails").hide();   //tabDetails
                                    $("#tabDoccument").hide(); //tabDoccument
                                    $("#tabDI").hide();//tabDI
                                    $("#tabFee").hide(); //tabFee
                                    $("#tabDiFeeInvoicePreview").hide();
                                    $GlobalData.IsHavingAccess = 0;
                                }

                            }
                            else if (msg == '4')//Invalid Access
                            {
                                alert("This Work Order has already cancelled.");
                                window.location.href = "SearchWorkOrder";
                            }
                            else if (msg == '5')//Invalid Access
                            {
                                alert("This WO has already been assigned to another user");
                                window.location.href = "SearchWorkOrder";
                            }
                            else if (msg == '1')//Valid Access
                            {
                                $("#tabDetails").show();
                                $("#tabDoccument").show();
                                $("#tabDI").show();
                                $("#tabFee").show();
                                $("#tabDiFeeInvoicePreview").show();


                                if ($('#hdnUserRole').val() == 'User') {
                                    $('#btnSelfAssignment').parent().text('Assigned to you');
                                    $('.HideInfo').removeClass('HideInfo');

                                    $('.divddlAssignedTo').hide();
                                }
                                else if ($('#hdnUserRole').val() == 'Manager') {
                                    $('.divddlAssignedTo').show();
                                    $('#btnSelfAssignment').hide();
                                    $('.HideInfo').removeClass('HideInfo');

                                }

                                //$('.HideInfo').removeClass('HideInfo');

                                if (callBack != undefined && callBack != '')
                                    callBack();
                            }
                            else if (msg == '2') {

                                $("#tabWO").show();
                                $("#tabDetails").hide();   //tabDetails
                                $("#tabDoccument").hide(); //tabDoccument
                                $("#tabDI").hide();//tabDI
                                $("#tabFee").hide(); //tabFee
                                $("#tabDiFeeInvoicePreview").hide();
                                $GlobalData.IsHavingAccess = 0;
                                if (callBack != undefined && callBack != '')
                                    callBack();
                            }
                            else if (msg == '6') {

                                if (callBack != undefined && callBack != '')
                                    callBack();


                                $("#tabDetails").hide();
                                $("#tabDoccument").hide();
                                $("#tabDI").hide();
                                $("#tabFee").hide();
                                $("#tabDiFeeInvoicePreview").hide();
                                //$('#btnSelfAssignment').parent().text('Assigned to you');
                                //$('.HideInfo').removeClass('HideInfo');
                                //$('.divddlAssignedTo').hide();
                                $('#btnSelfAssignment').hide();

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

        }
    }

    function BindStatus() {
        try {
            WOStatusAndAssignmentCallServices("GetMWOStatus", "scriptStatus", "ddlStatus", "{}", false, BindWOAssignment);

        } catch (e) {
            console.log(e);
        }
    }

    function BindWOAssignment() {

        $('#ddlStatus option[value="OPN"]').remove();
        $('#ddlStatus option[value="DOC"]').remove();
        $('#ddlStatus option[value="WIP"]').remove();

        if ($('#hdnUserRole').val() == "User") {
            $('#ddlStatus option[value="CAN"]').remove();
        }

        WOStatusAndAssignmentCallServices("GetCSS1UserDetailsForWOAssignment", "ScriptAssignedTo", "ddlAssignedTo", "{}", false, GetDisbursementDataByID);
    }

    var txtareaDescription = $('#txtComments');
    textareaLimiter(txtareaDescription, 500);

    $("#btnSaveClient").click(function () {
        try {
            $GlobalDataWOStatusAndAssignment.ClientCode = $("#ddlChosenClient_ddlClientsByCustomers").find("option:Selected").attr("ClientCode");
            $GlobalDataWOStatusAndAssignment.SourceID = $("#ddlChosenClient_ddlClientsByCustomers").find("option:Selected").attr("sourceid");
            $GlobalDataWOStatusAndAssignment.WOID = $.trim($("#hdnWOID").val());

            if ($GlobalDataWOStatusAndAssignment.ClientCode == undefined || $GlobalDataWOStatusAndAssignment.SourceID == undefined) {
                $GlobalDataWOStatusAndAssignment.ClientCode = '0';
                $GlobalDataWOStatusAndAssignment.SourceID = '';
            }
            if ($GlobalDataWOStatusAndAssignment.WOID != '' && $GlobalDataWOStatusAndAssignment.WOID != undefined && $GlobalDataWOStatusAndAssignment.SourceID != undefined && $GlobalDataWOStatusAndAssignment.ClientCode != undefined)
                WOStatusAndAssignmentCallServices("UpdateBillingPartyByWOID", "", "", "{'WOID':" + $GlobalDataWOStatusAndAssignment.WOID + ",'ClientOrCustomer':" + parseInt($GlobalDataWOStatusAndAssignment.ClientCode) + ",'SourceID':'" + $GlobalDataWOStatusAndAssignment.SourceID + "'}", false, ClientUpdateCallBack);

        } catch (e) {
            console.log(e);
        }
    });

    function ClientUpdateCallBack() {
        try {
            if ($GlobalDataWOStatusAndAssignment.SavedStatus == 1) {
                ShowNotify('Success.', 'success', 3000);
                return false;
            }
            else if ($GlobalDataWOStatusAndAssignment.SavedStatus == -1) {
                ShowNotify('Client / Customer alredy exists.', 'error', 3000);
                return false;
            }
        } catch (e) {
            console.log(e);
        }
    }

    $("#btnSaveWOAssignmentGroup").click(function () {
        try {
            $GlobalDataWOStatusAndAssignment.GroupCode = $.trim($("#ddlGroupCode_InWOA").find("option:Selected").text());
            $GlobalDataWOStatusAndAssignment.WOID = $.trim($("#hdnWOID").val());
            $GlobalDataWOStatusAndAssignment.WOCode = $.trim($("#lblWoCode").text());
            $GlobalDataWOStatusAndAssignment.IsBillable = $.trim($("#hdnBillable").val());
            if ($GlobalDataWOStatusAndAssignment.GroupCode == '') {
                ShowNotify('Please select Group Code.', 'error', 3000);
                return false;
            }
            if ($GlobalDataWOStatusAndAssignment.WOCode != '' && $GlobalDataWOStatusAndAssignment.WOCode != undefined && $GlobalDataWOStatusAndAssignment.GroupCode != '' && $GlobalDataWOStatusAndAssignment.GroupCode != undefined && $GlobalDataWOStatusAndAssignment.WOID != '' && $GlobalDataWOStatusAndAssignment.WOID != undefined && $GlobalDataWOStatusAndAssignment.IsBillable != '' && $GlobalDataWOStatusAndAssignment.IsBillable != undefined)
                WOStatusAndAssignmentCallServices("SaveWOAssignmentGroup", "", "", "{'WOCode':'" + $GlobalDataWOStatusAndAssignment.WOCode + "','WOID':" + parseInt($GlobalDataWOStatusAndAssignment.WOID) + ",'AssignedGroup':'" + $GlobalDataWOStatusAndAssignment.GroupCode + "','Billable':" + $GlobalDataWOStatusAndAssignment.IsBillable + "}", false, WOAssignmentSaveStatus);

        } catch (e) {
            console.log(e);
        }
    });

    function WOAssignmentSaveStatus() {
        try {
            if ($GlobalDataWOStatusAndAssignment.SavedStatus == '-1') {
                ShowNotify('Group Code details already exist.', 'error', 3000);
                return false;
            }
            else if ($GlobalDataWOStatusAndAssignment.SavedStatus != '' && $GlobalDataWOStatusAndAssignment.SavedStatus != undefined) {
                $("#lblWoCode").text($.trim($GlobalDataWOStatusAndAssignment.SavedStatus));
                $("#hdnWOCode").val($.trim($GlobalDataWOStatusAndAssignment.SavedStatus));
                ShowNotify('Success.', 'success', 2000);
                return false;
            }
        } catch (e) {
            console.log(e);
        }
    }

    $('#btnSaveAdhoc').click(function () {
        try {
            var adhoc = $('#chkAdhoc').is(':checked');
            var WOID = $.trim($("#hdnWOID").val())
            WOStatusAndAssignmentCallServices("SaveWOAdhoc", "", "", "{'WorkOrderID':" + parseInt(WOID) + ",'Adhoc':" + adhoc + "}", '', WOAdhocCallBack);
        }
        catch (e) {
            console.log(e);
        }
    });

    function WOAdhocCallBack() {
        try {
            if ($GlobalDataWOStatusAndAssignment.SavedStatus == '1') {
                ShowNotify('Success.', 'success', 3000);
                return false;
            }
        } catch (e) {
            console.log(e);
        }
    }

    $("#btnSaveWOAssignmentAssignedTo").click(function () {
        try {
            $GlobalDataWOStatusAndAssignment.AssignedTo = $.trim($("#ddlAssignedTo").find("option:Selected").val());
            $GlobalDataWOStatusAndAssignment.WOID = $.trim($("#hdnWOID").val());
            $GlobalDataWOStatusAndAssignment.IsBillable = $.trim($("#hdnBillable").val());

            if ($GlobalDataWOStatusAndAssignment.AssignedTo != '' && $GlobalDataWOStatusAndAssignment.AssignedTo != undefined && $GlobalDataWOStatusAndAssignment.WOID != '' && $GlobalDataWOStatusAndAssignment.WOID != undefined && $GlobalDataWOStatusAndAssignment.IsBillable != '' && $GlobalDataWOStatusAndAssignment.IsBillable != undefined)
                WOStatusAndAssignmentCallServices("SaveWOAssignmentAssignedTo", "", "", "{'WorkOrderID':" + parseInt($GlobalDataWOStatusAndAssignment.WOID) + ",'AssignedTo':'" + $GlobalDataWOStatusAndAssignment.AssignedTo + "','Billable':" + $GlobalDataWOStatusAndAssignment.IsBillable + "}", false, WOAssignmentAssignedToSaveStatus);

        } catch (e) {
            console.log(e);
        }
    });

    $("#btnSelfAssignment").click(function () {
        try {
            $GlobalDataWOStatusAndAssignment.AssignedTo = 'Self';
            $GlobalDataWOStatusAndAssignment.WOID = $.trim($("#hdnWOID").val());
            $GlobalDataWOStatusAndAssignment.IsBillable = $.trim($("#hdnBillable").val());

            if ($GlobalDataWOStatusAndAssignment.AssignedTo != '' && $GlobalDataWOStatusAndAssignment.AssignedTo != undefined && $GlobalDataWOStatusAndAssignment.WOID != '' && $GlobalDataWOStatusAndAssignment.WOID != undefined && $GlobalDataWOStatusAndAssignment.IsBillable != '' && $GlobalDataWOStatusAndAssignment.IsBillable != undefined)
                WOStatusAndAssignmentCallServices("SaveWOAssignmentAssignedTo", "", "", "{'WorkOrderID':" + parseInt($GlobalDataWOStatusAndAssignment.WOID) + ",'AssignedTo':'" + $GlobalDataWOStatusAndAssignment.AssignedTo + "','Billable':" + $GlobalDataWOStatusAndAssignment.IsBillable + "}", false, WOAssignmentAssignedToSaveStatus);

        } catch (e) {
            console.log(e);
        }
    });

    function WOAssignmentAssignedToSaveStatus() {
        try {
            if ($GlobalDataWOStatusAndAssignment.SavedStatus == '-1') {
                ShowNotify('AssignmentTo details already exist.', 'error', 3000);
                return false;
            }
            else if ($GlobalDataWOStatusAndAssignment.SavedStatus != '' && $GlobalDataWOStatusAndAssignment.SavedStatus != undefined) {
                ShowNotify('Success.', 'success', 2000);
                window.location.href = "WorkOrder?ID=" + $("#hdnWOID").val();
                return false;
            }
        } catch (e) {
            console.log(e);
        }
    }

    $("#lblStatusHistory").click(function () {
        try {
            $GlobalDataWOStatusAndAssignment.WOID = $("#hdnWOID").val();
            if ($GlobalDataWOStatusAndAssignment.WOID != '' && $GlobalDataWOStatusAndAssignment.WOID != undefined)
                WOStatusAndAssignmentCallServices("GetWOStatusHistoryByWOID", "scriptStatusHistory", "trStatusBody", "{'WOID':" + $GlobalDataWOStatusAndAssignment.WOID + "}", true, WOStatusHistoryCallBack);
        } catch (e) {
            console.log(e);
        }
    });

    $("#lblAssignmentHistory").click(function () {
        try {
            $GlobalDataWOStatusAndAssignment.WOID = $("#hdnWOID").val();
            if ($GlobalDataWOStatusAndAssignment.WOID != '' && $GlobalDataWOStatusAndAssignment.WOID != undefined)
                WOStatusAndAssignmentCallServices("GetWOAssignmentHistoryByWOID", "scripAssignmentHistory", "trAssignMentBody", "{'WOID':" + $GlobalDataWOStatusAndAssignment.WOID + "}", true, WOAssignmentHistoryCallBack);
        } catch (e) {
            console.log(e);
        }
    });

    $("#lblAssignmentGroup").click(function () {
        try {
            $GlobalDataWOStatusAndAssignment.WOID = $("#hdnWOID").val();
            if ($GlobalDataWOStatusAndAssignment.WOID != '' && $GlobalDataWOStatusAndAssignment.WOID != undefined)
                WOStatusAndAssignmentCallServices("GetWOGroupHistoryByWOID", "scriptGroupHistory", "trGroupBody", "{'WOID':" + $GlobalDataWOStatusAndAssignment.WOID + "}", true, WOGroupHistoryCallBack);
        } catch (e) {
            console.log(e);
        }
    });

    function WOStatusHistoryCallBack() {
        try {
            var tblStatusBodyLength = $("#trStatusBody").find("tr").length;
            if (tblStatusBodyLength >= 1) {
                $("#divSearchNoStatusData").hide();

            }
            else {
                $("#divSearchNoStatusData").show();
            }
            $('#divSHistories').modal({
                "backdrop": "static",
                "show": "true"
            });
        } catch (e) {
            console.log(e);
        }
    }

    function WOAssignmentHistoryCallBack() {
        try {
            var tblStatusBodyLength = $("#trAssignMentBody").find("tr").length;
            if (tblStatusBodyLength >= 1) {
                $("#divSearchNoAssignmentData").hide();

            }
            else {
                $("#divSearchNoAssignmentData").show();
            }
            $('#divAHistories').modal({
                "backdrop": "static",
                "show": "true"
            });
        } catch (e) {
            console.log(e);
        }
    }

    function WOGroupHistoryCallBack() {
        try {
            var tblStatusBodyLength = $("#trGroupBody").find("tr").length;
            if (tblStatusBodyLength >= 1) {
                $("#divSearchNoGroupData").hide();
            }
            else {
                $("#divSearchNoGroupData").show();
            }
            $('#divGroupHistory').modal({
                "backdrop": "static",
                "show": "true"
            });
        } catch (e) {
            console.log(e);
        }
    }

    $("#btnSaveWOStatus").click(function () {
        try {
            $GlobalDataWOStatusAndAssignment.StatusCode = $("#ddlStatus").find("option:Selected").val();
            $GlobalDataWOStatusAndAssignment.Comment = $("#txtComments").val();
            $GlobalDataWOStatusAndAssignment.WOID = $("#hdnWOID").val();
            if ($GlobalDataWOStatusAndAssignment.StatusCode == -1) {
                ShowNotify('Please select status code.', 'error', 3000);
                return false;
            }
            
            //Incorp Validation
            if ($GlobalDataWOStatusAndAssignment.StatusCode == 'CLD' && $GlobalDataWOStatusAndAssignment.IncorpFieldsValidationCount > 0) {
                $('#modal-form-IncorpValidation').modal('show');
                return false;
            }

            if ($GlobalDataWOStatusAndAssignment.StatusCode == 'CAN' ) {
              
                if (confirm('Are you sure you want to cancel this work order?')) {
                    SaveSatusDetails();
                    window.location.href = "SearchWorkOrder";
                }
            }

            else if ($GlobalDataWOStatusAndAssignment.StatusCode == 'CLD') {
               
                if (confirm('Are you sure you want to close this work order?')) {
                    SaveSatusDetails();
                    window.location.href = "SearchWorkOrder";
                }
            }
            else SaveSatusDetails();
        } catch (e) {
            console.log(e);
        }
    });

    function SaveSatusDetails() {
        if ($GlobalDataWOStatusAndAssignment.StatusCode != '' && $GlobalDataWOStatusAndAssignment.StatusCode != undefined && $GlobalDataWOStatusAndAssignment.Comment != undefined && $GlobalDataWOStatusAndAssignment.WOID != '' && $GlobalDataWOStatusAndAssignment.WOID != undefined)
            WOStatusAndAssignmentCallServices("InsertWOStatus", "", "", "{'StatusCode':'" + $GlobalDataWOStatusAndAssignment.StatusCode + "','WorkOrderID':" + $GlobalDataWOStatusAndAssignment.WOID + ",'Comment':'" + escape($.trim($GlobalDataWOStatusAndAssignment.Comment)) + "'}", false, WOStatusSaveStatus);
    }

    function WOStatusSaveStatus() {
        try {
            if ($GlobalDataWOStatusAndAssignment.SavedStatus > 1) {
                ShowNotify('Success.', 'success', 4000);
                return false;
            }

        } catch (e) {
            console.log(e);
        }
    }

    function WOStatusAndAssignmentCallServices(path, templateId, containerId, parameters, clearContent, callBack) {
        ShowLoadNotify();
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
                        $GlobalDataWOStatusAndAssignment.SavedStatus = msg;

                        if (msg.UserList != null && msg.UserList != 'undefined') {

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
        HideLoadNotify();
    }

    CategoryBindCallBack();

    function CheckQueryString1() {
        try {
            var queryString = [];
            if (window.location.search.split('?').length > 1) {
                var params = window.location.search.split('?')[1].split('&');
                for (var i = 0; i < params.length; i++) {
                    var key = params[i].split('=')[0];
                    var value = decodeURIComponent(params[i].split('=')[1]);
                    queryString[key] = value;
                }
                $GlobalData.WorkOrderID = queryString["ID"]
                var IsInteger = $.isNumeric($GlobalData.WorkOrderID);
                if (IsInteger == false)
                    return false;
                $("#hdnWOID").val($GlobalData.WorkOrderID);
                $("#hdnWorkOrderID").val($GlobalData.WorkOrderID);

                if ($GlobalData.WorkOrderIDStatus == false) {
                    GetDisbursementDataByID();

                }

            }
            else
                $("#divWorkOrder").show();
        } catch (e) {
            console.log(e);
        }

    }

    $("#tabWO").click(function () {
        try {
            var IsActive = RestrictSecondTimeCallBack($("#tabWO"));
            if (IsActive == false)
                return false;
            GetDisbursementDataByID();
            $("#txtComments").val('');
            $("#divMaintainPartial").empty();
            $("#divDIOperations").hide();
            $("#divDocument").hide();
            $("#divFee").hide();
            $("#divInvoicePreview").hide();
            $("#divWOOperations").show();
        } catch (e) {
            console.log(e);
        }

    });
    $("#tabFee").click(function () {
        try {
            var IsActive = RestrictSecondTimeCallBack($("#tabFee"));
            if (IsActive == false)
                return false;
            $("#divMaintainPartial").empty();
            $("#divWOOperations").hide();
            $("#divDIOperations").hide();
            $("#divDocument").hide();
            $("#divInvoicePreview").hide();
            $GlobalFeeData.IsPreviewTab = false;
            BindMFeeData();
            // $("#divCreateNewFee").show();
            $("#divFeeDialog").show();
            $("#divFee").show();

            if ($('#hdnWOCloseStatus').val() == 'hide') {
                $('.btnWOClose').hide();
            }


        } catch (e) {
            console.log(e);
        }
    });
    $("#tabDoccument").click(function () {
        try {
            var IsActive = RestrictSecondTimeCallBack($("#tabDoccument"));
            if (IsActive == false)
                return false;
            BindTemplates();
            $("#divMaintainPartial").empty();
            $("#divWOOperations").hide();
            $("#divDIOperations").hide();
            $("#divFee").hide();
            $("#divInvoicePreview").hide();
            $("#divDocument").show();

        } catch (e) {
            console.log(e);
        }
    });
    $("#tabDiFeeInvoicePreview").click(function () {
        try {
            var IsActive = RestrictSecondTimeCallBack($("#tabDiFeeInvoicePreview"));
            if (IsActive == false)
                return false;
            $("#divMaintainPartial").empty();
            $("#divWOOperations").hide();
            $GlobalFeeData.IsPreviewTab = true;
            $GlobalData.IsPreviewTab = true;
            BindMFeeData();
            $("#divCreateNewDI").hide();
            $("#divDIPopUp").hide();
            $("#divDocument").hide();
            LoadPartialDIList();
            $GlobalData.WorkOrderIDStatus = true;
            $("#divDIOperations").show()
            $("#divFee").show();
            $("#divCreateNewFee").hide();
            $("#divFeeDialog").hide();
            $("#divInvoicePreview").show();

        } catch (e) {
            console.log(e);
        }
    });
    $("#tabDetails").click(function () {
        try {
            var IsActive = RestrictSecondTimeCallBack($("#tabDetails"));
            if (IsActive == false)
                return false;
            $GlobalData.PartialViewGenerator = $("#hdnPartial").val();
            $("#divMaintainPartial").show();
            if ($GlobalData.PartialViewGenerator != '' && $GlobalData.PartialViewGenerator != undefined) {
                if ($GlobalData.PartialViewGenerator == 'AGM')
                    $("#divMaintainPartial").load('_WoAGMDetails', { WoId: $("#hdnWOID").val() });

                else if ($GlobalData.PartialViewGenerator == 'INCO')
                    $("#divMaintainPartial").load('_WoInCorpDetails', { WoId: $("#hdnWOID").val() });

                else if ($GlobalData.PartialViewGenerator == 'ALLT')
                    $("#divMaintainPartial").load('_WoAllotmentDetails', { WoId: $("#hdnWOID").val() });

                else if ($GlobalData.PartialViewGenerator == 'TRAN')
                    $("#divMaintainPartial").load('_WoTransferDetails', { WoId: $("#hdnWOID").val() });

                else if ($GlobalData.PartialViewGenerator == 'APPO')
                    $("#divMaintainPartial").load('_WoAppointmentOfOfficerDetails', { WoId: $("#hdnWOID").val() });

                else if ($GlobalData.PartialViewGenerator == 'CESO')
                    $("#divMaintainPartial").load('_WoCessationOfficerDetails', { WoId: $("#hdnWOID").val() });

                else if ($GlobalData.PartialViewGenerator == 'INTD')
                    $("#divMaintainPartial").load('_WoInterimDividendDetails', { WoId: $("#hdnWOID").val() });

                else if ($GlobalData.PartialViewGenerator == 'APPA')
                    $("#divMaintainPartial").load('_WoAppointmentOrCessationOfAuditorsDetails', { WoId: $("#hdnWOID").val() });

                else if ($GlobalData.PartialViewGenerator == 'EGMA')
                    $("#divMaintainPartial").load('_WOEGMAcquisitionDisposal', { WoId: $("#hdnWOID").val() });

                else if ($GlobalData.PartialViewGenerator == 'ECEN')
                    $("#divMaintainPartial").load('_WOExistingClientEngaging', { WoId: $("#hdnWOID").val() });

                else if ($GlobalData.PartialViewGenerator == 'TAKE')
                    $("#divMaintainPartial").load('_WOTakeOver', { WoId: $("#hdnWOID").val() });

                else if ($GlobalData.PartialViewGenerator == 'EGMC')
                    $("#divMaintainPartial").load('_WOEGMChangeOfName', { WoId: $("#hdnWOID").val() });

                else if ($GlobalData.PartialViewGenerator == 'DUPL')
                    $("#divMaintainPartial").load('_WoDuplicateDetails', { WoId: $("#hdnWOID").val() });

                else if ($GlobalData.PartialViewGenerator == 'BOIS')
                    $("#divMaintainPartial").load('_WoBonusIssue', { WoId: $("#hdnWOID").val() });
                else {
                    $("#divMaintainPartial").empty();
                }
                $("#divDocument").hide();
                $("#divDIOperations").hide();
                $("#divWOOperations").hide();
                $("#divFee").hide();
                $("#divInvoicePreview").hide();
            }

        } catch (e) {
            console.log(e);
        }

    });

    function RestrictSecondTimeCallBack(event) {
        try {
            var IsActive = event.closest("li").attr("class");
            if (IsActive == 'active') {
                return false;
            }
            return true;
        } catch (e) {
            console.log(e);
        }
    }

    $("#tabDI").click(function () {
        try {
            var IsActive = RestrictSecondTimeCallBack($("#tabDI"));
            if (IsActive == false)
                return false;
            $("#divCreateNewDI").hide();
            $GlobalData.IsPreviewTab = false;
            ShowDiPartialDialog();
            DiPartialCall();
            LoadPartialDIList();

            $GlobalData.WorkOrderIDStatus = true;
            $("#divDocument").hide();
            $("#divWOOperations").hide();
            $("#divMaintainPartial").empty();
            //$("#divMaintainPartial").hide();
            $("#divFee").hide();
            $("#divInvoicePreview").hide();
            $("#divDIOperations").show();
            // $("#divCreateNewDI").show();
            $("#divDIPopUp").show();


            if ($('#hdnWOCloseStatus').val() == 'hide') {
                $('.btnWOClose').hide();
            }


        } catch (e) {
            console.log(e);
        }
    });

    function showWOOperations() {
        try {
            $("#divWorkOrder").hide();
            $("#TabEvents").show();
            $("#divWOOperations").show();
        } catch (e) {
            console.log(e);
        }
    }

    function showEvents() {
        try {
            $("#btnAdd").show();
            $("#btnArchive").show();
            $("#btnAdhocBilling").show();
            $("#btnDelete").show();
            $("#btnClear").show();
        } catch (e) {
            console.log(e);
        }
    }

    function GetDisbursementDataByID() {
        try {
            CallServices("GetWorkOrderDetailsById", '', '', "{'ID':" + parseInt($("#hdnWOID").val()) + "}", true, BindWorkOrderDetails);

        } catch (e) {
            console.log(e);
        }
    }

    function BindWorkOrderDetails() {
        try {

            if ($GlobalData.WOList[0].CategoryCode == 'A' || $GlobalData.IsHavingAccess == 0)
                $("#tabDetails").hide();
            else if ($GlobalData.IsHavingAccess == 1)
                $("#tabDetails").show();

            var WOlength = $GlobalData.WOList.length;
            if (WOlength >= 1) {
                $("#TabEvents").show();
                $GlobalData.WorkOrderID = $GlobalData.WOList[0].ID;
                $GlobalData.SourceID = $GlobalData.WOList[0].SourceID;
                $GlobalData.ClientName = $GlobalData.WOList[0].ClientName;
                $GlobalData.WOCode = $.trim($GlobalData.WOList[0].WOCode);
                $GlobalData.GroupCode = $GlobalData.WOList[0].GroupCode;
                $GlobalData.IsBillable = $GlobalData.WOList[0].IsBillable;
                $GlobalData.IsAdhoc = $GlobalData.WOList[0].IsAdhoc;
                $GlobalData.IsPostedToCss1 = $GlobalData.WOList[0].IsPostedToCss1;
                $GlobalData.CategoryCode = $GlobalData.WOList[0].CategoryCode;
                $GlobalData.WorkOrderType = $GlobalData.WOList[0].WorkOrderType;
                $GlobalData.ClientCode = $GlobalData.WOList[0].ClientId;
                $GlobalData.StatusCode = $GlobalData.WOList[0].StatusCode;
                $GlobalData.WorkOrderTypeText = $GlobalData.WOList[0].WorkOrderTypeText;
                $GlobalData.BillingParty = $GlobalData.WOList[0].BillingPartyName;
                $GlobalData.Description = $GlobalData.WOList[0].Description;

                $GlobalDataWOTYPE.Type = $GlobalData.WorkOrderType;
                $GlobalDataWOTYPE.WOID = $GlobalData.WorkOrderID;

                //Binding Billing Party Chosen
                var BillingPartyID = $GlobalData.WOList[0].BillingPartyID;
                var BillingPartySource = $GlobalData.WOList[0].BillingPartySourceID;
                if (BillingPartyID != '0') {
                    $('#ddlChosenClient_ddlClientsByCustomers > option:not(:eq(0))').remove();
                    $('#ddlChosenClient_ddlClientsByCustomers').append('<option value="1" clientcode="' + BillingPartyID + '" sourceid="' + BillingPartySource + '">' + $GlobalData.WOList[0].BillingPartyName + '</option>');
                    $('#ddlChosenClient_ddlClientsByCustomers').val('1').trigger('chosen:updated');
                }

                SetStatusCodeCall("-1");
                $('#spnWOCurrentStatus').text($GlobalData.StatusCode);

                showWOOperations();
            }

            var WOAssignmentLength = $GlobalData.WOAssignmentList.length;
            if (WOAssignmentLength >= 1) {
                $GlobalData.GroupName = $GlobalData.WOAssignmentList[0].GroupName;
                $GlobalData.AssignedTo = $GlobalData.WOAssignmentList[0].AssignedTo;

                if ($GlobalData.AssignedTo == undefined || $GlobalData.AssignedTo == '') {
                    $GlobalData.AssignedTo = '-1';
                }
                SetAssignedToCall($GlobalData.AssignedTo);


            }
            $("#lblWoCode").text($GlobalData.WOCode);
            $("#chkBillable").prop("checked", $GlobalData.IsBillable);

            $("#chkAdhoc").prop("checked", $GlobalData.IsAdhoc);
            $("#chkPostedToCSS1").prop("checked", $GlobalData.IsPostedToCss1);
            $("#lblCategory").text($GlobalData.CategoryCode);

            $("#spnWorkOrderType").text($GlobalData.WorkOrderTypeText + " (" + $GlobalData.ClientName + ")");
            $("#hdnPartial").val($GlobalData.WorkOrderType);

            $("#hdnWOID").val($GlobalData.WorkOrderID);
            $("#hdnWorkOrderID").val($GlobalData.WorkOrderID);
            $("#hdnWOCode").val($GlobalData.WOCode);
            $("#hdnClientName").val($GlobalData.ClientName)
            $("#hdnBillable").val($GlobalData.IsBillable);

            //Binding Description
            $('#spnWODescription').text($GlobalData.Description);

            MaintainClientNameAndWOCode();

            if ($GlobalData.WorkOrderType == 'INCO') {
                GetValidationForIncorp($GlobalData.WorkOrderID);
            }


        } catch (e) {
            console.log(e);
        }
    }

    function GetValidationForIncorp(WOID) {
        CallServicesForValidateIncorp("GetValidationForIncorp", 'scriptIncorpValidation', 'tblINCORPValidation', "{'WOID':" + parseInt(WOID) + "}", true, '');
    }

    $("#btnViewInvoicePreviewForDIAndFee").click(function () {
        try {
            $Invoice = {};
            $Invoice.TotalAmountDI = 0;
            var ArrAmountDI = '';
            var ArrItemDI = '';
            var ArrDescDI = '';
            $Invoice.TotalAmountFee = 0;
            var ArrAmountFee = '';
            var ArrItemFee = '';
            var ArrDescFee = '';
            var strDID = GetIDsOfCheckedDI()
            var strFeeID = GetIDsOfCheckedFee()

            if ((strFeeID == '' || strFeeID == undefined) && (strDID == '' || strDID == undefined)) {
                ShowNotify('Please select at least one DI/Fee Item.', 'error', 3000);
                return false;
            }
            else {
                InvoicePreviewDetailsByDIAndFee($Invoice.TotalAmountDI, $Invoice.TotalAmountFee);
                return false;
            }
        }
        catch (ex) {
            console.log(ex);
        }
    });

    function GetIDsOfCheckedDI() {
        try {
            var disbursementids = '';
            var ArrayListDIAmount = new Array();
            var ArrayListItemDI = new Array();
            var ArrayListDescDI = new Array();

            $('.chkChangeAdhocOrArchived').each(function () {
                if ($(this).is(':checked')) {
                    var disbursementid = $(this).attr('disbursementid');
                    var amount = $(this).attr('amount');
                    var desc = $(this).attr('desc');
                    var itemnumber = $(this).attr('itemnumber');

                    if (disbursementids == '') {
                        disbursementids = disbursementid;
                        $Invoice.TotalAmountDI = parseInt(amount);
                        ArrayListDIAmount.push(amount);
                        ArrayListItemDI.push(itemnumber);
                        ArrayListDescDI.push(desc);

                    }
                    else {
                        $Invoice.TotalAmountDI = $Invoice.TotalAmountDI + parseInt(amount);
                        disbursementids = disbursementids + ',' + disbursementid;
                        ArrayListDIAmount.push(amount);
                        ArrayListItemDI.push(itemnumber);
                        ArrayListDescDI.push(desc);
                    }
                }
            });
            ArrAmountDI = ArrayListDIAmount;
            ArrItemDI = ArrayListItemDI;
            ArrDescDI = ArrayListDescDI;
            return disbursementids;
        } catch (e) {
            console.log(e);
        }
    }
    function GetIDsOfCheckedFee() {
        try {
            var FeeIDS = '';
            var ArrayListAmountFee = new Array();
            var ArrayListItemFee = new Array();
            var ArrayListDescFee = new Array();
            $('.chkFeeChange').each(function () {
                if ($(this).is(':checked')) {

                    var Feeid = $(this).attr('fid');
                    var amount = $(this).attr('amount');
                    var desc = $(this).attr('desc');
                    var itemnumber = $(this).attr('itemnumber');

                    if (FeeIDS == '') {
                        FeeIDS = Feeid;
                        $Invoice.TotalAmountFee = parseInt(amount);
                        ArrayListAmountFee.push(amount);
                        ArrayListItemFee.push(itemnumber);
                        ArrayListDescFee.push(desc);

                    }
                    else {
                        $Invoice.TotalAmountFee = $Invoice.TotalAmountFee + parseInt(amount);
                        FeeIDS = FeeIDS + ',' + Feeid;
                        ArrayListAmountFee.push(amount);
                        ArrayListItemFee.push(itemnumber);
                        ArrayListDescFee.push(desc);
                    }
                }
            });
            ArrAmountFee = ArrayListAmountFee;
            ArrItemFee = ArrayListItemFee;
            ArrDescFee = ArrayListDescFee;
            return FeeIDS;
        } catch (e) {
            console.log(e);
        }
    }
    function InvoicePreviewDetailsByDIAndFee(DIAmount, TotalAmountFee) {
        try {

            $("#divViewInvoiceForDIandFee").load("/Billing/Billing/_InvoicePreviewDetails", function () {
                InvoicePreviewDetailsDIAndFee("", "", "", "", DIAmount, ArrAmountDI, ArrItemDI, ArrDescDI, TotalAmountFee, ArrAmountFee, ArrItemFee, ArrDescFee);
            });

        } catch (e) {
            console.log(e);
        }
    }

    function SetAssignedToCall(AssignedTo) {
        $("#ddlAssignedTo").val(AssignedTo)
    }
    function SetStatusCodeCall(StatusCode) {
        $("#ddlStatus").val(StatusCode)
    }
    function MaintainClientNameAndWOCode() {
        $("#txTwoReadOnly").val($.trim($("#hdnWOCode").val()));
        $("#txtClientReadOnly").val($.trim($("#hdnClientName").val()));
    }
    $("#btnCreateNewDI").click(function () {
        try {
            $('#divCreateNewDI').hide();
            DiPartialCall();
        } catch (e) {
            console.log(e);
        }
    });

    function DiPartialCall() {
        try {
            MaintainClientNameAndWOCode();
            $("#diLabel").text('Add Disbursement Item');
            $("#divPartial").show();


            ClearAllPartialValues();
            $('#btnAdd').show();
            $('#btnArchive').show();
            $('#btnClear').show();
            $('#btnAdhocBilling').show();
            $('#Clear').show();

            $('#btnUNArchive').hide();
            $('#btnUNAdhocBilling').hide();
            $('#btnDelete').hide();

            $('#btnAddNoteInDI').hide();

            $('#dvNote').find('.NoteAddInList').hide();
            $('#dvNote').find('.spnNoteCount').hide();
            $('#btnFixAccpacStatus').hide();

        } catch (e) {
            console.log(e);
        }
    }
    function CategoryBindCallBack() {
        try {
            CallServices("GetMWOCategory", "scriptCategory", "ddlCategory", '{}', false);
        } catch (e) {
            console.log(e);
        }
    }

    $("#btnCancel").click(function () {
        if ($('#hdnWOCloseStatus').val() != 'hide') {
            $("#divCreateNewDI").show();
        }
    });

    function scrollToTop() {
        $("html, body").animate({ scrollTop: 0 }, "slow");
        return false;
    }

    function CallServices(path, templateId, containerId, parameters, clearContent, callBack) {
        ShowLoadNotify();
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


                        $GlobalData.WOList = msg.WorkOrdersList;
                        $GlobalData.WOAssignmentList = msg.WOAssignment;

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

        }
        HideLoadNotify();
    }

    function CallServicesForValidateIncorp(path, templateId, containerId, parameters, clearContent, callBack) {
        ShowLoadNotify();
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
                        
                        $GlobalDataWOStatusAndAssignment.IncorpFieldsValidationCount = msg.length;

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

        }
        HideLoadNotify();
    }

});
