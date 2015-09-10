$(document).ready(function () {

    SetPageAttributes('liDI', 'Fee', 'Search Fee Items', 'liSearchFee');

    ApplyDatePickerFromAndToDate($('#txtFromDate'), $('#txtToDate'));

    $GlobalFeeData = {};
    $GlobalFeeData.ID = 0;
    $GlobalFeeData.WOID = 0;
    $GlobalFeeData.Quanity = 0;
    $GlobalFeeData.UnitPrice = 0;
    $GlobalFeeData.Code = '';
    $GlobalFeeData.Amount = 0;
    $GlobalFeeData.Quanity = 0;
    $GlobalFeeData.FeeType = 0;
    $GlobalFeeData.Units = 0;
    $GlobalFeeData.Description = '';
    $GlobalFeeData.IsAdhoc = false;
    $GlobalFeeData.IsArchived = false;
    $GlobalFeeData.IsBilled = false;
    $GlobalFeeData.SavedStatus = 0;
    $GlobalFeeData.UpdatedStatus = false;
    //$GlobalFeeData.AdhocListAction = false;
    $GlobalFeeData.ListAction = false;
    $GlobalFeeData.DeleteListAction = false;

    $GlobalFeeData.startPage = 0;
    $GlobalFeeData.totalRow = 0;
    $GlobalFeeData.resultPerPage = 10;
    $GlobalFeeData.FeeInState = '';
    $GlobalFeeData.ActionRule = '';

    $GlobalFeeData.TotalAmount = '';
    $GlobalFeeData.ArrAmount = '';
    $GlobalFeeData.ArrItem = '';
    $GlobalFeeData.ArrDesc = '';
    $GlobalFeeData.InsertedID = '';


    $GlobalFeeData.ClientId = '';
    $GlobalFeeData.SourceID = '';
    $GlobalFeeData.WO = '';
    $GlobalFeeData.FeeType = '';
    $GlobalFeeData.FromDate = '';
    $GlobalFeeData.ToDate = '';
    $GlobalFeeData.OrderBy = '';
    $GlobalFeeData.IsBilled = '';
    $GlobalFeeData.IsArchived = '';
    $GlobalFeeData.IsAdhoc = '';
    $GlobalFeeData.ACCPACStatus = 0;


    ApplyMaxLengthForFeeDescription();

    $("#ddlFeeTypeForSearch > option:not(:first)").remove();
    FeeCallServices("GetMFeeType", 'scriptFeeType', 'ddlFeeTypeForSearch', "{}", false);

    $('#btnSearch').on('click', function () {

        $("#divMaintainPartial").empty();
        $("#divWOOperations").hide();
        $("#divDIOperations").hide();
        $("#divDocument").hide();
        $("#divInvoicePreview").hide();

        // $("#divCreateNewFee").show();
        $("#divFeeDialog").hide();
        $("#divFee").show();
        $('#divSearchFeeList').show();
        $('#divFEEAdhocAndArchiveButtons').show();


        $GlobalFeeData.startPage = 0;

        BindMFeeData();

    });

    $('#btnResetSearchFee').click(function () {
        $('#ddlChosenClient_ddlClient').val('-1').trigger('chosen:updated');
        $('#ddlFeeTypeForSearch').val('-1');
        $('#txtWO').val('');
        $('#drpOrderBy').val('CreatedDateDESC');
        $("#ddlPageSize").val("10");
        $('#chkBilled').attr('checked', false);
        $('#chkArchived').attr('checked', false);
        $('#chkAdhoc').attr('checked', false);
        $('#chkACCPACStatus').attr('checked', false);
        ApplyDatePickerFromAndToDate($('#txtFromDate'), $('#txtToDate'));
    });

    checkQueryStringofFee();

    function checkQueryStringofFee() {
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
                    $('#btnSearch').trigger('click');
                }
                else {
                    window.location.href = '/WO/WODI/SearchFeeItems';
                }
            }

        } catch (e) {
            console.log(e);
        }
    }

    $('#dvheaderFeeItems').click(function () {
        try {
            if ($('#widget').hasClass('collapsed')) {
                $('#widget').removeClass('collapsed');
                $('#widget').find('.fa-chevron-down').removeClass('fa-chevron-down').addClass('fa-chevron-up');
            }
            else {
                $('#widget').addClass('collapsed');
                $('#widget').find('.fa-chevron-up').removeClass('fa-chevron-up').addClass('fa-chevron-down');
            }
        }
        catch (ex) {
            console.log(ex);
        }
    });

    $("#btnCreateNewFee").click(function () {
        try {
            ClearFeeItems();
            $("#divFeeDialog").show();
            $("#divCreateNewFee").hide();
        } catch (e) {
            console.log(e);
        }
    });

    $("#ddlFeeType").change(function () {
        try {
            $GlobalFeeData.ID = $(this).find("option:Selected").val();
            if ($GlobalFeeData.ID != -1) {
                FeeCallServices("GetMFeeTypeDetailsByItemNumber", '', '', "{FeeID:" + $GlobalFeeData.ID + "}", '', MFeeTypeDetailsCallBack);
            }
            else {
                $("#txtFeeQuantity").val('');
                $("#txtFeeUnitPrice").val('');
                $("#txtFeeAmount").val('');
                $("#txtFeeCode").val('');
                $("#txtFeedesc").val('');
                ClearFeeDescriptionToOneTextArea();
            }
        }
        catch (ex) {
            console.log(ex);
        }
    });

    $("#txtFeeQuantity").keyup(function () {
        try {
            CalculateTotalAmount();
        }
        catch (ex) {
            console.log(ex);
        }
    });
    $("#txtFeeUnitPrice").keyup(function () {
        try {
            CalculateTotalAmount();
        }
        catch (ex) {
            console.log(ex);
        }
    });

    $('#btnFixFeeAccpacStatus').click(function () {
        var FeeID = $("#hdnFeeID").val();
        FeeCallServices('UpdateFeeAccpacStatusByFeeID', '', '', "{'FeeID':" + FeeID + "}", '', FeeACCPACStatusCallBack);

    });
    function FeeACCPACStatusCallBack() {
        if ($GlobalFeeData.InsertedID > 0) {
            ShowNotify('Success.', 'success', 2000);
            $('#btnFixFeeAccpacStatus').hide();
            ClearFeeFields();
        }
    }

    $("#btnAddFee").click(function () {
        try {
            $GlobalFeeData.IsAdhoc = $("#chkFeeAdhocBillingReadOnly").is(":checked");
            $GlobalFeeData.IsArchived = $("#chkFeeArchivedReadOnly").is(":checked");

            $GlobalFeeDataFee = {};
            $GlobalFeeDataFee.IsAdhoc = $GlobalFeeData.IsAdhoc;
            $GlobalFeeDataFee.IsArchived = $GlobalFeeData.IsArchived;


            FeeItemCall();
        }
        catch (ex) {
            console.log(ex);
        }
    });
    $("#btnArchiveFee").click(function () {
        try {
            $GlobalFeeData.IsArchived = true;
            var AttributeVal = $("#btnAddFee").attr('Value');
            if (AttributeVal == 'ADD') {
                $GlobalFeeDataFee = {};
                $GlobalFeeDataFee.IsAdhoc = false;
                $GlobalFeeDataFee.IsArchived = true;
                FeeItemCall();
            }
            else {
                var Action = 'AR';
                $GlobalFeeData.ID = MaintainFeeID();
                FeeItemsListArchivedAction(parseInt($GlobalFeeData.ID), $GlobalFeeData.IsArchived, Action)
            }
        }
        catch (ex) {
            console.log(ex);
        }
    });
    $("#btnAdhocBillingFee").click(function () {
        try {
            $GlobalFeeData.IsAdhoc = true;
            var AttributeVal = $("#btnAddFee").attr('Value');
            if (AttributeVal == 'ADD') {
                $GlobalFeeDataFee = {};
                $GlobalFeeDataFee.IsAdhoc = true;
                FeeItemCall();
            }
            else {
                var Action = 'AD';
                $GlobalFeeData.ID = MaintainFeeID();
                FeeItemsListAdhocAction(parseInt($GlobalFeeData.ID), $GlobalFeeData.IsAdhoc, Action);
            }
        }
        catch (ex) {
            console.log(ex);
        }
    });
    $("#btnUNArchiveFee").click(function () {
        try {
            $GlobalFeeData.IsArchived = false;
            var Action = 'AR';
            $GlobalFeeData.ID = MaintainFeeID();
            FeeItemsListUnArchivedAction(parseInt($GlobalFeeData.ID), $GlobalFeeData.IsArchived, Action)
        }
        catch (ex) {
            console.log(ex);
        }
    });
    $("#btnUNAdhocBillingFee").click(function () {
        try {
            var Action = 'AD';
            $GlobalFeeData.IsAdhoc = false;
            $GlobalFeeData.ID = MaintainFeeID();
            FeeItemsListAdhocAction(parseInt($GlobalFeeData.ID), $GlobalFeeData.IsAdhoc, Action);
        }
        catch (ex) {
            console.log(ex);
        }
    });
    $("#btnDeleteFee").click(function () {
        try {
            $GlobalFeeData.ID = $("#hdnFeeID").val();
            CommonDeleteFunction($GlobalFeeData.ID);
            return false;
        }
        catch (ex) {
            console.log(ex);
        }
    });
    $("#btnCancelFee").click(function () {
        try {
            $("#divFeeDialog").hide();
            //$("#divCreateNewFee").show();
            $("#hdnFeeID").val('');
            $("#dvFeeNote").hide();
        } catch (e) {
            console.log(e);
        }
    });
    $("#ClearFee").click(function () {
        try {
            ClearFeeItems();
        } catch (e) {
            console.log(e);
        }
    });

    $("#btnFEESendToACCPACForADHOCBilling").click(function () {
        try {
            var checkedIDs = GetIDsOfCheckedFee();
            if (checkedIDs == false) {
                ShowNotify('Please select at least one Fee Item.', 'error', 3000);
                return false;
            }
            else {
                $GlobalFeeData.IsAdhoc = true;
                $GlobalFeeData.ListAction = true;
                var Action = 'AD';
                FeeItemsListAdhocAction(checkedIDs, $GlobalFeeData.IsAdhoc, Action);
            }
        }
        catch (ex) {
            console.log(ex);
        }
    });

    $("#btnFEERemoveFromACCPACForADHOCBilling").click(function () {
        try {
            var checkedIDs = GetIDsOfCheckedFee();
            if (checkedIDs == false) {
                ShowNotify('Please select at least one Fee Item.', 'error', 3000);
                return false;
            }
            else {
                $GlobalFeeData.IsAdhoc = false;
                $GlobalFeeData.ListAction = true;
                var Action = 'AD';
                FeeItemsListAdhocAction(checkedIDs, $GlobalFeeData.IsAdhoc, Action);
            }
        }
        catch (ex) {
            console.log(ex);
        }
    });
    $("#btnFEEListARCHIVE").click(function () {
        try {
            var checkedIDs = GetIDsOfCheckedFee();
            if (checkedIDs == false) {
                ShowNotify('Please select at least one Fee Item.', 'error', 3000);
                return false;
            }
            else {
                $GlobalFeeData.IsArchived = true;
                $GlobalFeeData.ListAction = true;
                var Action = 'AR';
                FeeItemsListArchivedAction(checkedIDs, $GlobalFeeData.IsArchived, Action)
            }
        }
        catch (ex) {
            console.log(ex);
        }
    });
    $("#btnFEEListUnARCHIVE").click(function () {
        try {
            var checkedIDs = GetIDsOfCheckedFee();
            if (checkedIDs == false) {
                ShowNotify('Please select at least one Fee Item.', 'error', 3000);
                return false;
            }
            else {
                $GlobalFeeData.IsArchived = false;
                $GlobalFeeData.ListAction = true;
                var Action = 'AR';
                FeeItemsListUnArchivedAction(checkedIDs, $GlobalFeeData.IsArchived, Action)
            }
        }
        catch (ex) {
            console.log(ex);
        }
    });
    $("#btnFEEListDELETE").click(function () {
        try {
            var checkedIDs = GetIDsOfCheckedFee();
            if (checkedIDs == false) {
                ShowNotify('Please select at least one Fee Item.', 'error', 3000);
                return false;
            }
            else {
                $GlobalFeeData.DeleteListAction = true;
                $GlobalFeeData.ListAction = true;
                CommonDeleteFunction(checkedIDs);
                return false;
            }
        }
        catch (ex) {
            console.log(ex);
        }

    });

    $("#btnViewInvoicePreviewForFee").click(function () {
        try {
            var strFeeIDs = GetIDsOfCheckedFeeAndAmount()
            if (strFeeIDs == '' || strFeeIDs == undefined) {
                ShowNotify('Please select at least one Fee Item.', 'error', 3000);
                return false;
            }
            else {
                //GetInvoicePreviewCallFee(strFeeIDs);
                InvoicePreviewDetailsByFee();
                return false;
            }
        }
        catch (ex) {
            console.log(ex);
        }
    });

    $('#btnAddDynamicFeeDescription').click(function () {
        try {
            var checkforempty = 0;

            $('.FeeMultiLine').each(function () {
                var val = $.trim($(this).val());
                if (val == '') {
                    checkforempty = 1;
                }
            });
            if (checkforempty != 1) {
                var index = $(this).closest('tr').index();
                var txtCount = $('.FeeMultiLine').length;
                var eq = index + (txtCount - 1);

                $('#tblFee tr:eq(' + eq + ')').after('<tr><td></td><td><br/><textarea id="txtFeedesc" maxlength="250" class="FeeMultiLine form-control special" style="resize:none;"></textarea></td></td></tr>');
            }
            ApplyMaxLengthForFeeDescription();
        } catch (e) {
            console.log(e);
        }
    });


    $('#btnFeeDescView').click(function () {
        try {
            var desc;
            $('.FeeMultiLine').each(function (index, value) {
                if (index == 0)
                    desc = $(this).val();
                else desc = desc + '<br/>' + $(this).val();
            });
            $('#spnFeeDescription').html(desc);

            $('#divFeeDescription').modal({
                "backdrop": "static",
                "show": "true"
            });

        } catch (e) {
            console.log(e);
        }
    });

});


function GetIDsOfCheckedFeeAndAmount() {
    try {
        var FeeIDS = '';
        var TotalAmount = '';
        var ArrayListAmount = new Array();
        var ArrayListItem = new Array();
        var ArrayListDesc = new Array();
        $('.chkFeeChange').each(function () {
            if ($(this).is(':checked')) {

                var Feeid = $(this).attr('fid');
                var amount = $(this).attr('amount');
                var desc = $(this).attr('desc');
                var itemnumber = $(this).attr('itemnumber');

                if (FeeIDS == '') {
                    FeeIDS = Feeid;
                    TotalAmount = parseInt(amount);
                    ArrayListAmount.push(amount);
                    ArrayListItem.push(itemnumber);
                    ArrayListDesc.push(desc);

                }
                else {
                    TotalAmount = TotalAmount + parseInt(amount);
                    FeeIDS = FeeIDS + ',' + Feeid;
                    ArrayListAmount.push(amount);
                    ArrayListItem.push(itemnumber);
                    ArrayListDesc.push(desc);
                }
            }
        });
        $GlobalFeeData.TotalAmount = TotalAmount;
        $GlobalFeeData.ArrAmount = ArrayListAmount;
        $GlobalFeeData.ArrItem = ArrayListItem;
        $GlobalFeeData.ArrDesc = ArrayListDesc;
        return FeeIDS;
    } catch (e) {
        console.log(e);
    }
}

function InvoicePreviewDetailsByFee() {

    try {
        $("#ViewInvoiceFee").load("/Billing/Billing/_InvoicePreviewDetails", function () {
            InvoicePreviewDetailsFee("", "", "", "", $GlobalFeeData.TotalAmount, $GlobalFeeData.ArrAmount, $GlobalFeeData.ArrItem, $GlobalFeeData.ArrDesc);
        });

    } catch (e) {
        console.log(e);
    }
}

function ApplyMaxLengthForFeeDescription() {
    try {
        var txtareaDescription = $('.FeeMultiLine');
        textareaLimiter(txtareaDescription, 250);
    } catch (e) {
        console.log(e);
    }
}
function ClearFeeItems() {
    try {
        $("#ddlFeeType").val('-1');
        $("#txtFeeCode").val('');
        $("#txtFeeQuantity").val('');
        $("#txtFeeUnitPrice").val('');
        $("#txtFeeAmount").val('');
        $("#txtFeedesc").val('');
        $("#chkFeeAdhocBillingReadOnly").prop("checked", false);
        $("#chkFeeArchivedReadOnly").prop("checked", false);
        $("#chkFeeBilledReadOnly").prop("checked", false);

        $("#btnUNArchiveFee").hide();
        $("#btnUNAdhocBillingFee").hide();
        $("#btnDeleteFee").hide();
        $("#btnAddFee").show();
        $("#btnAddFee").val('ADD');
        $("#btnAdhocBillingFee").show();
        $("#btnArchiveFee").show();
        $("#btnCancelFee").show();
        $("#ClearFee").show();
        $("#hdnFeeID").val('');
        $("#dvFeeNote").hide();
        $("#btnFeeDescView").hide();
        $("#FeeLabel").text('Add Fee Item');
        $('#btnFixFeeAccpacStatus').hide();
        ClearFeeDescriptionToOneTextArea();
    } catch (e) {
        console.log(e);
    }
}
function OpenFeeDialog() {
    try {
        $("#divCreateNewFee").hide();
        $('#widget').removeClass('collapsed');
        $('#widget').find('.fa-chevron-down').removeClass('fa-chevron-down').addClass('fa-chevron-up');
        $("#divFeeDialog").show();
    } catch (e) {
        console.log(e);
    }
}
function FeeItemsListAdhocAction(FeeIDs, Adhoc, ForState) {
    try {
        FeeCallServices("AdhocActionOnFeeItems", '', '', "{'FeeIDs':'" + FeeIDs + "','Adhoc':" + Adhoc + ",'ForState':'" + ForState + "'}", '', FeeItemsListAdhocActionCallBack);
    } catch (e) {
        console.log(e);
    }
}
function FeeItemsListArchivedAction(FeeIDs, ARCHIVE, ForState) {

    $("#txtFeeInhouseComments").val('')
    try {
        $(".ui-dialog").css("height: 277px; width: 430px");
        $("#dialog-confirmForFeeInhouse").removeClass('hide').dialog({
            width: "450",
            resizable: false,
            modal: true,
            title_html: true,
            buttons: [
                {
                    html: "<i class='ace-icon fa fa-check bigger-110'></i>&nbsp; Save",
                    "class": "Add btn btn-info btn-xs",
                    click: function () {
                        var txtInhouseComment = $.trim($("#txtFeeInhouseComments").val());
                        FeeCallServices("ArchivedActionOnFeeItems", '', '', "{'FeeIDs':'" + FeeIDs + "','Archived':" + ARCHIVE + ",'ForState':'" + ForState + "','Comment':'" + escape($.trim(txtInhouseComment)) + "'}", '', FeeItemsListArchivedActionCallBack);
                        $(this).dialog("close");
                    }
                },
                {
                    html: "<i class='ace-icon fa fa-times bigger-110'></i>&nbsp; Cancel",
                    "class": "btn btn-xs",
                    click: function () {
                        $(this).dialog("close");
                    }
                }
            ]
        });
    } catch (e) {
        console.log(e);
    }

}


function FeeItemsListUnArchivedAction(FeeIDs, ARCHIVE, ForState) {
    FeeCallServices("ArchivedActionOnFeeItems", '', '', "{'FeeIDs':'" + FeeIDs + "','Archived':" + ARCHIVE + ",'ForState':'" + ForState + "','Comment':'" + "" + "'}", '', FeeItemsListArchivedActionCallBack);

}
function FeeItemsListAdhocActionCallBack() {
    try {
        if ($GlobalFeeData.SavedStatus == -1) {
            ShowNotify('You do not have permission on this action.', 'error', 3000);
            return false;
        }
        if ($GlobalFeeData.ListAction == true) {
            //$("#divCreateNewFee").show();
            $("#divFeeDialog").hide();
        }
        ClearFeeItems();
        FeeDataCall();
        ShowNotify('Success.', 'success', 2000);
        $GlobalFeeData.AdhocListAction = false;
        return false;
    } catch (e) {
        console.log(e);
    }
}
function FeeItemsListArchivedActionCallBack() {
    try {
        if ($GlobalFeeData.SavedStatus == -1) {
            ShowNotify('You do not have permission on this action.', 'error', 3000);
            return false;
        }
        if ($GlobalFeeData.ListAction == true) {
            //$("#divCreateNewFee").show();
            $("#divFeeDialog").hide();
        }

        ClearFeeItems();
        FeeDataCall();

        ShowNotify('Success.', 'success', 2000);
        $GlobalFeeData.ArchiveListAction = false;
        return false;
    } catch (e) {
        console.log(e);
    }
}

function GetIDsOfCheckedFee() {
    try {
        var FeeIDS = false;
        $('.chkFeeChange').each(function (index) {
            if ($(this).is(':checked')) {
                var FeeID = $(this).attr('fid');
                if (FeeIDS == false) {
                    FeeIDS = FeeID;
                }
                else {
                    FeeIDS = FeeIDS + ',' + FeeID;
                }
            }
        });
        return FeeIDS;

    } catch (e) {
        console.log(e);
    }
}
function MaintainFeeID() {
    try {
        $GlobalFeeData.ID = $("#hdnFeeID").val();
        return $GlobalFeeData.ID;
    } catch (e) {
        console.log(e);
    }
}

function FeeItemCall() {
    try {

        $GlobalFeeDataFee.ID = $("#hdnFeeID").val();
        if ($GlobalFeeDataFee.ID == '' || $GlobalFeeDataFee.ID == undefined) {
            $GlobalFeeDataFee.ID = 0;
            $GlobalFeeData.UpdatedStatus = false;
        }
        else $GlobalFeeData.UpdatedStatus = true;
        $GlobalFeeDataFee.WOID = $("#hdnWOID").val();
        $GlobalFeeDataFee.FeeType = $("#ddlFeeType").val();
        $GlobalFeeDataFee.Units = $("#txtFeeQuantity").val();
        $GlobalFeeDataFee.UnitPrice = $("#txtFeeUnitPrice").val();
        $GlobalFeeDataFee.Amount = $("#txtFeeAmount").val();
        var Description = '';
        $('.FeeMultiLine').each(function (index, Value) {
            var val = $.trim($(this).val());
            if (val != '') {
                if (index == 0)
                    Description = val;
                else
                    Description = Description + '~^' + val;
            }
        });
        $GlobalFeeDataFee.Description = Description;
        var count = 0;

        count += ControlEmptyNess(true, $("#ddlFeeType"), 'Please select Item Number.');
        count += ControlEmptyNess(true, $("#txtFeeQuantity"), 'Please enter Quantity.');
        count += ControlEmptyNess(true, $("#txtFeeUnitPrice"), 'Please enter UnitPrice.');
        count += ControlEmptyNess(false, $("#txtFeedesc"), 'Please select Description.');

        if (count > 0) {
            ShowNotify('Please select/enter all mandatory fields.', 'error', 3000);
            return false
        }
        if ($GlobalFeeDataFee.Units == 0) {
            ShowNotify('Quantity must not be zero, Please enter valid Quantity.', 'error', 3000);
            return false
        }
        if ($GlobalFeeDataFee.UnitPrice == 0) {
            ShowNotify('Unit Price must not be zero, Please enter valid Unit Price.', 'error', 3000);
            return false
        }
        if ($GlobalFeeDataFee.IsArchived == true) {
            FeeItemListArchivedAction($GlobalFeeDataFee);
        }
        else {
            $GlobalFeeDataFee.FeeInhouseComment = '';
            var JsonString = JSON.stringify({ FeeDetails: $GlobalFeeDataFee });
            FeeCallServices("InsertOrUpdateFeeItem", '', '', JsonString, '', FeeItemCallBack);
        }

    } catch (e) {
        console.log(e);
    }
}

function FeeItemListArchivedAction($GlobalFeeDataFee) {

    $("#txtFeeInhouseComments").val('')
    try {
        $(".ui-dialog").css("height: 277px; width: 430px");
        $("#dialog-confirmForFeeInhouse").removeClass('hide').dialog({
            width: "450",
            resizable: false,
            modal: true,
            title_html: true,
            buttons: [
                {
                    html: "<i class='ace-icon fa fa-check bigger-110'></i>&nbsp; Save",
                    "class": "Add btn btn-info btn-xs",
                    click: function () {
                        $GlobalFeeDataFee.FeeInhouseComment = $.trim($("#txtFeeInhouseComments").val());
                        var JsonString = JSON.stringify({ FeeDetails: $GlobalFeeDataFee });
                        FeeCallServices("InsertOrUpdateFeeItem", '', '', JsonString, '', FeeItemCallBack);
                        $(this).dialog("close");
                    }
                },
                {
                    html: "<i class='ace-icon fa fa-times bigger-110'></i>&nbsp; Cancel",
                    "class": "btn btn-xs",
                    click: function () {
                        $(this).dialog("close");
                    }
                }
            ]
        });
    } catch (e) {
        console.log(e);
    }
}

function FeeItemCallBack() {
    try {
        if ($GlobalFeeData.SavedStatus == 1) {
            ShowNotify('Success.', 'success', 2000);
            $GlobalFeeData.IsAdhoc = false;
            $GlobalFeeData.IsArchived = false;

            if ($GlobalFeeData.UpdatedStatus == true) {
                $("#divFeeDialog").hide();
                //$("#divCreateNewFee").show();
                $("#btnAddFee").val('ADD');
            }
            else {
                $("#divFeeDialog").show();
                $("#divCreateNewFee").hide();
            }
            ClearFeeFields();
            $("#hdnFeeID").val('')
            return false;
        }
    } catch (e) {
        console.log(e);
    }
}
function CalculateTotalAmount() {
    try {
        $GlobalFeeData.Quanity = $("#txtFeeQuantity").val();
        $GlobalFeeData.UnitPrice = $("#txtFeeUnitPrice").val();
        $GlobalFeeData.Amount = 0;
        if ($GlobalFeeData.Quanity != 0 && $GlobalFeeData.UnitPrice != 0 && $GlobalFeeData.Quanity != '' && $GlobalFeeData.UnitPrice != '') {
            $GlobalFeeData.Amount = $GlobalFeeData.UnitPrice * $GlobalFeeData.Quanity;
            $("#txtFeeAmount").val($GlobalFeeData.Amount);
        }
        else {
            $("#txtFeeAmount").val('');
        }
    } catch (e) {
        console.log(e);
    }
}

function MFeeTypeDetailsCallBack() {
    try {
        $("#txtFeeUnitPrice").val($GlobalFeeData.UnitPrice);
        $("#txtFeeCode").val($GlobalFeeData.Code)

        var arrDescription = [];
        arrDescription = $GlobalFeeData.SavedStatus.FeeList[0].Description.split('~^');

        var ArrLength = arrDescription.length;
        ClearFeeDescriptionToOneTextArea();
        if (ArrLength > 1) {

            var tableRowValue = 0;
            $.each(arrDescription, function (index, value) {
                if (index >= 1) {
                    if (value != '') {
                        $('#tblFee tr:eq(' + tableRowValue + ')').after('<tr><td></td><td><br/><textarea id="txtFeedesc" maxlength="250" class="FeeMultiLine form-control special" style="resize: none;">' + value + '</textarea></td></td></tr>');
                        tableRowValue++;
                    }
                }
                else {
                    $("#txtFeedesc").val(value);
                }

            });
        }
        else
            $("#txtFeedesc").val($GlobalFeeData.SavedStatus.FeeList[0].Description);

        ApplyMaxLengthForFeeDescription();

        $("#txtFeeQuantity").val($GlobalFeeData.Units);
        var Quantity = $("#txtFeeQuantity").val();
        if (Quantity != '') {
            $("#txtFeeAmount").val(Quantity * $GlobalFeeData.UnitPrice);
        }
    } catch (e) {
        console.log(e);
    }
}
function BindMFeeData() {
    try {
        $("#ddlFeeType > option:not(:first)").remove();
        FeeCallServices("GetMFeeType", 'scriptFeeType', 'ddlFeeType', "{}", false, ClearFeeFields);
        //OpenFeeDialog();
    }
    catch (ex) {
        console.log(ex);
    }
}
function ClearFeeFields() {
    try {
        ClearFeeItems();
        $GlobalFeeData.startPage = 0;
        $GlobalFeeData.totalRow = 0;
        $GlobalFeeData.resultPerPage = 10;
        FeeDataCall();
    } catch (e) {
        console.log(e);
    }
}
function FeeDataCall() {
    try {

        $GlobalFeeData.ClientId = $.trim($("#divClientOne .chosen-select1").find("option:Selected").attr('clientcode'));
        $GlobalFeeData.SourceID = $.trim($("#divClientOne .chosen-select1").find("option:Selected").attr('sourceid'));
        $GlobalFeeData.WO = $('#txtWO').val();
        $GlobalFeeData.FeeType = $('#ddlFeeTypeForSearch').val();
        $GlobalFeeData.FromDate = $.trim($("#txtFromDate").val());
        $GlobalFeeData.ToDate = $.trim($("#txtToDate").val());
        $GlobalFeeData.OrderBy = $('#drpOrderBy option:selected').val();

        $GlobalFeeData.resultPerPage = $("#ddlPageSize").val();

        $GlobalFeeData.IsBilled = ($('#chkBilled').is(':checked')) ? '1' : '';
        $GlobalFeeData.IsArchived = ($('#chkArchived').is(':checked')) ? '1' : '';
        $GlobalFeeData.IsAdhoc = ($('#chkAdhoc').is(':checked')) ? '1' : '';
        $GlobalFeeData.ACCPACStatus = ($('#chkACCPACStatus').is(':checked')) ? 1 : 0;

        //FeeCallServices("GetAllFeeItems", 'ScriptFeeItems', 'trFeeItemsData', "{'startPage':" + $GlobalFeeData.startPage + ",'resultPerPage':" + $GlobalFeeData.resultPerPage + "}", true, FeeDataListCallBack);
        FeeCallServices("GetAllFeeItems", 'ScriptFeeItems', 'trFeeItemsData', "{'clientID':'" + $GlobalFeeData.ClientId + "','SourceID':'" + $GlobalFeeData.SourceID + "','WO':'" + escape($.trim($GlobalFeeData.WO)) + "','Type':'" + $GlobalFeeData.FeeType + "','IsBilled':'" + $GlobalFeeData.IsBilled + "','IsArchived':'" + $GlobalFeeData.IsArchived + "','IsAdhoc':'" + $GlobalFeeData.IsAdhoc + "','OrderBy':'" + $GlobalFeeData.OrderBy + "','startPage':" + $GlobalFeeData.startPage + ",'resultPerPage':" + $GlobalFeeData.resultPerPage + ",'FromDate':'" + $GlobalFeeData.FromDate + "','ToDate':'" + $GlobalFeeData.ToDate + "','ACCPACStatus':" + $GlobalFeeData.ACCPACStatus + "}", true, FeeDataListCallBack);
    } catch (e) {
        console.log(e);
    }
}

function FeeDataListCallBack() {
    try {
        var tblLength = $("#trFeeItemsData").find("tr").length;
        $(".chkSelectAllFee").prop("checked", false);
        if (tblLength > 0) {
            $('.lblSelectAllFee').show();//for Select All Checkbox
            $("#trFeeItemsData").find(".aView").unbind("click");
            $("#trFeeItemsData").find(".aView").click(ViewFeeItemByID);

            $("#trFeeItemsData").find(".aEdit").unbind("click");
            $("#trFeeItemsData").find(".aEdit").click(EditFeeItemByID);

            $('#trFeeItemsData').find('.chkFeeChange').unbind('click');
            $('#trFeeItemsData').find('.chkFeeChange').click(FeeItemActionRules);
            $("#trFeeItemsData").find("td:eq(6)").each(function () {
                var description = $(this).attr('title')
                var arrToolTipDesc = [];
                arrToolTipDesc = description.split('~^');
                var resultTooltip;

                $.each(arrToolTipDesc, function (index, value) {
                    if (index == 0)
                        resultTooltip = value;
                    else resultTooltip = resultTooltip + '</br>' + value;
                });

                $(this).closest(".spanFeeDescription").html(resultTooltip);
                $(this).attr("title", $(this).closest(".spanFeeDescription").html());
                var descLen = description.length;
                if (descLen <= 10) {
                    $(this).removeAttr("title");
                }
            });


            $("#trFeeItemsData").find('.dvFeeACCPACStatus').tooltip({ html: true });


            GenerateFeeNumericPaging()


            var exists = $('.NotePartialJS').length;
            if (exists > 0) {
                $('.NotePartialJS').remove();
            }

            var headID = document.getElementsByTagName("head")[0];
            var newScript = document.createElement('script');
            newScript.type = 'text/javascript';
            newScript.src = '/Areas/WO/Content/Scripts/_NotePartial.js';
            newScript.className = "NotePartialJS";
            headID.appendChild(newScript);

            $("#divSearchFeeNoData").hide();
            $("#divFeePaging").show();
            $("#btnFEESendToACCPACForADHOCBilling").show();
            $("#btnFEERemoveFromACCPACForADHOCBilling").show();
            $("#btnFEEListARCHIVE").show();
            $("#btnFEEListUnARCHIVE").show();
            $("#btnFEEListDELETE").show();
            $("#btnViewInvoicePreviewForFee").show();
        }
        else {
            $("#divSearchFeeNoData").show();
            $("#divFeePaging").hide();
            $("#btnFEESendToACCPACForADHOCBilling").hide();
            $("#btnFEERemoveFromACCPACForADHOCBilling").hide();
            $("#btnFEEListARCHIVE").hide();
            $("#btnFEEListUnARCHIVE").hide();
            $("#btnFEEListDELETE").hide();
            $('.lblSelectAllFee').hide();//for Select All Checkbox
            $("#btnViewInvoicePreviewForFee").hide();
        }
        if ($GlobalFeeData.IsPreviewTab) {
            $("#divFEEAdhocAndArchiveButtons").hide();
            $(".HeadFeeAction").hide();
            $(".HeadFeeAction").hide();
            $(".divFeelstActions").hide();

        }
        else {
            $("#divFEEAdhocAndArchiveButtons").show();
            $(".HeadFeeAction").show();
            $(".HeadFeeAction").show();
            $(".divFeelstActions").show();
        }

    } catch (e) {
        console.log(e);
    }
}

$('#chkSelectAllFee').click(function () {
    try {
        if ($(this).is(':checked')) {
            $('#trFeeItemsData').find('.chkFeeChange').prop('checked', true);
        }
        else {
            $('#trFeeItemsData').find('.chkFeeChange').prop('checked', false);
        }
        FeeItemActionRules();
    } catch (e) {
        console.log(e);
    }
});

function FeeItemActionRules() {
    try {
        var chkCheckedCount = $('#trFeeItemsData').find('input.chkFeeChange:checkbox:checked').length;
        var chkTotalCount = $('#trFeeItemsData').find('.chkFeeChange').length;
        if (chkCheckedCount != chkTotalCount) {
            $('#chkSelectAllFee').prop('checked', false)
        }
        else {
            $('#chkSelectAllFee').prop('checked', true)
        }

        var str = new Array();
        $('#trFeeItemsData').find('input.chkFeeChange:checkbox:checked').each(function () {
            str.push($(this).attr('fid'));
        });
        ApplyFeeActionRulesOnItems(str, true);
    } catch (e) {
        console.log(e);
    }
}

function ApplyFeeActionRulesOnItems(str, isList) {
    try {
        var selectedDOData = FeeeventManagement(str, $GlobalFeeData.ActionRule);
        var selectedUNDOData = FeeeventManagement(str, $GlobalFeeData.FeeInState);
        validateFeeActions(selectedDOData, selectedUNDOData, isList);
    } catch (e) {
        console.log(e);
    }
}
function FeeeventManagement(FID, data) {
    try {
        return $.grep(data, function (n, i) {
            var isFIDFound = $.inArray("" + n.FeeID, FID) != -1;
            return isFIDFound;
        });
    } catch (e) {
        console.log(e);
    }
}

function validateFeeActions(DOActionData, UNDOActionData, isList) {
    try {
        if (isList) {
            $("#btnFEESendToACCPACForADHOCBilling, #btnFEERemoveFromACCPACForADHOCBilling, #btnFEEListARCHIVE, #btnFEEListUnARCHIVE, #btnFEEListDELETE").hide();
        }
        else {
            $("#btnAddFee, #btnArchiveFee, #btnUNArchiveFee, #btnAdhocBillingFee, #btnUNAdhocBillingFee, #btnDeleteFee").hide();
        }

        $.each(new Array('AD', 'AR', 'UP', 'DE', 'BI'), function (index, value) {
            var actionDO = true;
            var actionUnDO = true;
            var ActionDataDO = filterByForStateOnFee(DOActionData, value);
            $.each(ActionDataDO, function (index, ActionDataDObject) {
                actionDO = actionDO && (ActionDataDObject.DO == 1 ? true : false);
                actionUnDO = actionUnDO && (ActionDataDObject.UNDO == 1 ? true : false);
            });
            if (isList) {
                if (value == 'AD' && actionDO) {
                    $("#btnFEESendToACCPACForADHOCBilling").show();
                }
                if (value == 'DE' && actionDO) {
                    $("#btnFEEListDELETE").show();
                }
                if (value == 'AR' && actionDO) {
                    $("#btnFEEListARCHIVE").show();
                }

                if (value == 'AR' && actionUnDO) {
                    $("#btnFEEListUnARCHIVE").show();
                }
                if (value == 'AD' && actionUnDO) {
                    $("#btnFEERemoveFromACCPACForADHOCBilling").show();
                }
            }
            else {
                if (value == 'AD' && actionDO) {
                    $("#btnAdhocBillingFee").show();
                }
                if (value == 'DE' && actionDO) {
                    $("#btnDeleteFee").show();
                }
                if (value == 'AR' && actionDO) {
                    $("#btnArchiveFee").show();
                }
                if (value == 'UP' && actionDO) {
                    $("#btnAddFee").show();
                }
                if (value == 'AR' && actionUnDO) {
                    $("#btnUNArchiveFee").show();
                }

                if (value == 'AD' && actionUnDO) {
                    $("#btnUNAdhocBillingFee").show();
                }
            }
        });

    } catch (e) {
        console.log(e);
    }
}

function filterByForStateOnFee(ActionData, ForStateValue) {
    try {
        return $.grep(ActionData, function (n, i) {
            return (n.ForState == ForStateValue);
        });
    } catch (e) {
        console.log(e);
    }
}

function filterByINStateOnFee(ActionData, INStateValue) {
    try {
        return $.grep(ActionData, function (n, i) {
            return (n.InState == INStateValue);
        });
    } catch (e) {
        console.log(e);
    }
}
function ViewFeeItemByID() {
    try {
        $GlobalFeeData.ID = $(this).attr("feeid");
        FeeCallServices("GetFeeItemByFeeID", '', '', "{FeeID:" + parseInt($GlobalFeeData.ID) + "}", '', ViewFeeDataByIDCallBack);
    } catch (e) {
        console.log(e);
    }
}
function ViewFeeDataByIDCallBack() {
    try {
        $("#lblWoCodeInFee").text($GlobalFeeData.SavedStatus.FeeList[0].WOCode);
        $("#lblClientInFee").text($GlobalFeeData.SavedStatus.FeeList[0].ClientName);
        $("#labelItemNOInFee").text($GlobalFeeData.SavedStatus.FeeList[0].ItemNumber);
        $("#labelFeeCodeInFee").text($GlobalFeeData.SavedStatus.FeeList[0].Code);
        $("#labelQuantityInFee").text($GlobalFeeData.SavedStatus.FeeList[0].Units);
        $("#labelUnitPriceInFee").text($GlobalFeeData.SavedStatus.FeeList[0].UnitPrice);
        $("#labelAmountInFee").text($GlobalFeeData.SavedStatus.FeeList[0].Amount);
        $("#labelInvoiceNumberInFee").text($GlobalFeeData.SavedStatus.FeeList[0].InvoiceNumber);
        var arrDescription = [];
        var Description = '';
        arrDescription = $GlobalFeeData.SavedStatus.FeeList[0].Description.split('~^');
        var ArrLength = arrDescription.length;
        var desc;
        if (ArrLength > 1) {
            $.each(arrDescription, function (index, value) {
                if (index == 0)
                    desc = value;
                else desc = desc + '<br/>' + value;
            });
            Description = desc;
        }
        else Description = $GlobalFeeData.SavedStatus.FeeList[0].Description;
        $("#labelCommentsInFee").html(Description);

        $('.chkshowAdhocBillingReadOnlyInFee').prop("checked", $GlobalFeeData.SavedStatus.FeeList[0].IsAdhoc);

        $('.chkshowArchivedReadOnlyInFee').prop("checked", $GlobalFeeData.SavedStatus.FeeList[0].IsArchived);

        $('.chkshowBilledReadOnlyInFee').prop("checked", $GlobalFeeData.SavedStatus.FeeList[0].IsBilled);
        $('#modal-formFee').modal('show');

        return false;

    } catch (e) {
        console.log(e);
    }
}

function EditFeeItemByID() {
    try {
        $("#FeeLabel").text('Edit Fee Item');
        $GlobalFeeData.ID = $(this).attr("feeid");
        FeeCallServices("GetFeeItemByFeeID", '', '', "{FeeID:" + parseInt($GlobalFeeData.ID) + "}", '', EditFeeItemByIDCallBack);
        var str = new Array();
        str.push($GlobalFeeData.ID);
        ApplyFeeActionRulesOnItems(str, false);
    } catch (e) {
        console.log(e);
    }
}
function ClearFeeDescriptionToOneTextArea() {
    try {
        var checkforempty = 1;
        $('.FeeMultiLine').each(function () {
            if (checkforempty > 1) {

                $(this).closest('tr').remove();
            }
            checkforempty++;
        });
    } catch (e) {
        console.log(e);
    }
}
function EditFeeItemByIDCallBack() {
    try {
        $('#ClearFee').hide();
        $("#btnAddFee").val("UPDATE");
        $("#divFeeDialog").show();
        $("#divCreateNewFee").hide();
        $("#hdnFeeID").val($GlobalFeeData.SavedStatus.FeeList[0].ID);
        $("#ddlFeeType").val($GlobalFeeData.SavedStatus.FeeList[0].FeeType);
        $("#txtFeeCode").val($GlobalFeeData.SavedStatus.FeeList[0].Code);
        $("#txtFeeQuantity").val($GlobalFeeData.SavedStatus.FeeList[0].Units);
        $("#txtFeeUnitPrice").val($GlobalFeeData.SavedStatus.FeeList[0].UnitPrice);
        $("#txtFeeAmount").val($GlobalFeeData.SavedStatus.FeeList[0].Amount);
        $("#chkFeeAdhocBillingReadOnly").prop("checked", $GlobalFeeData.SavedStatus.FeeList[0].IsAdhoc);
        $("#chkFeeArchivedReadOnly").prop("checked", $GlobalFeeData.SavedStatus.FeeList[0].IsArchived);
        $("#chkFeeBilledReadOnly").prop("checked", $GlobalFeeData.SavedStatus.FeeList[0].IsBilled);

        $('#dvFeeNote').find('.NoteAddInList').show();
        $('#dvFeeNote').find('.NoteAddInList').attr('refid', $GlobalFeeData.SavedStatus.FeeList[0].RefID);
        $('#dvFeeNote').find('.NoteAddInList').attr('notetype', $GlobalFeeData.SavedStatus.FeeList[0].NoteType);
        $('#dvFeeNote').find('.spnNoteCount').text($GlobalFeeData.SavedStatus.FeeList[0].NoteCount);
        $("#dvFeeNote").show();
        if ($GlobalFeeData.SavedStatus.FeeList[0].NoteCount == 0) {
            $('#dvFeeNote').find('.spnNoteCount').hide();

        }
        else {
            $('#dvFeeNote').find('.spnNoteCount').show();

        }
        $GlobalFeeData.ListAction = true;
        if ($GlobalFeeData.SavedStatus.FeeList[0].ACCPACStatus != '0') {
            $('#btnFixFeeAccpacStatus').show();
            $('#btnFixFeeAccpacStatus').attr('data-original-title', $GlobalFeeData.SavedStatus.FeeList[0].ACCPACDescription + ' <br/>' + $GlobalFeeData.SavedStatus.FeeList[0].ACCPACExplanation);
            $('#btnFixFeeAccpacStatus').tooltip({ html: true });
        }
        else {
            $('#btnFixFeeAccpacStatus').hide();
        }

        ClearFeeDescriptionToOneTextArea();
        var arrDescription = [];
        arrDescription = $GlobalFeeData.SavedStatus.FeeList[0].Description.split('~^');
        var ArrLength = arrDescription.length;
        ClearDescriptionToOneTextArea();
        $("#btnFeeDescView").show();
        if (ArrLength > 1) {

            var tableRowValue = 0;
            $.each(arrDescription, function (index, value) {
                if (index >= 1) {
                    if (value != '') {
                        $('#tblFee tr:eq(' + tableRowValue + ')').after('<tr><td></td><td><br/><textarea id="txtFeedesc" maxlength="250" class="FeeMultiLine form-control special" style="resize: none;">' + value + '</textarea></td></td></tr>');
                        tableRowValue++;
                    }
                }
                else {
                    $("#txtFeedesc").val(value);
                }

            });
        }
        else
            $("#txtFeedesc").val($GlobalFeeData.SavedStatus.FeeList[0].Description);

        ApplyMaxLengthForFeeDescription();

        scrollToTop();

    } catch (e) {
        console.log(e);
    }
}
function DeleteFeeItemByID() {
    try {
        $GlobalFeeData.ID = $(this).attr("feeid");
        CommonDeleteFunction($GlobalFeeData.ID);
    } catch (e) {
        console.log(e);
    }
}

function CommonDeleteFunction(IDs) {
    try {
        DialogForDelete(IDs);
    } catch (e) {
        console.log(e);
    }
}

function DialogForDelete(IDs) {
    try {
        $("#dialog-confirm").removeClass('hide').dialog({
            resizable: false,
            modal: true,
            title_html: true,
            buttons: [
                {
                    html: "<i class='ace-icon fa fa-trash-o bigger-110'></i>&nbsp; Delete item",
                    "class": "btn btn-danger btn-xs",
                    click: function () {

                        FeeCallServices("DeleteFeeItemByFeeID", '', '', "{FeeIDs:'" + IDs + "'}", '', DeleteFeeItemByIDCallBack);
                        $(this).dialog("close");
                    }
                },
                {
                    html: "<i class='ace-icon fa fa-times bigger-110'></i>&nbsp; Cancel",
                    "class": "btn btn-xs",
                    click: function () {
                        $(this).dialog("close");
                    }
                }
            ]
        });
    } catch (e) {
        console.log(e);
    }
}

function DeleteFeeItemByIDCallBack() {
    try {
        if ($GlobalFeeData.SavedStatus >= 1) {
            $("#divFeeDialog").hide();
            if ($GlobalFeeData.DeleteListAction = true) {
                $("#divFeeDialog").hide();
                //$("#divCreateNewFee").show();
            }

            ClearFeeItems();
            FeeDataCall();
            ShowNotify('Success.', 'success', 2000);
            return false;
        }
    } catch (e) {
        console.log(e);
    }
}

function ClearDescriptionToOneTextArea() {
    try {
        var checkforempty = 1;
        $('.DescriptionMultiLine').each(function () {
            if (checkforempty > 1) {

                $(this).closest('tr').remove();
            }
            checkforempty++;
        });
    } catch (e) {
        console.log(e);
    }
}

function FeeCallServices(path, templateId, containerId, parameters, clearContent, callBack) {
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
                    $GlobalFeeData.SavedStatus = msg;
                    $GlobalFeeData.InsertedID = msg;
                    if (msg.FEEInstate != null && msg.FEEInstate != 'undefined') {
                        if (msg.FEEInstate.length > 0 && msg.FEEActionRules.length > 0) {
                            $GlobalFeeData.FeeInState = msg.FEEInstate;
                            $GlobalFeeData.ActionRule = msg.FEEActionRules;
                        }
                    }
                    if (msg.FeeCount != null && msg.FeeCount != 'undefined') {
                        $GlobalFeeData.totalRow = msg.FeeCount;
                    }
                    if (msg.FeeList != null && msg.FeeList != 'undefined') {
                        var FeeLength = msg.FeeList.length;
                        if (FeeLength > 0) {
                            $GlobalFeeData.UnitPrice = msg.FeeList[0].UnitPrice;
                            $GlobalFeeData.Code = msg.FeeList[0].Code;
                            $GlobalFeeData.Description = msg.FeeList[0].Description;
                            $GlobalFeeData.Units = msg.FeeList[0].Units;
                        }
                    }
                    if (templateId != '' && containerId != '') {

                        if (!clearContent) {
                            $.tmpl($('#' + templateId).html(), msg.FeeList).appendTo("#" + containerId);
                        }
                        else {
                            $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg.FeeList));
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

function setListCountFee(count) {
    $('#listNameFee').text('Total Entries');
    $('#listCountFee').text(count);
}

function GenerateFeeNumericPaging() {
    try {
        var numericcontainer = $('#numericcontainerFee');
        setListCountFee($GlobalFeeData.totalRow)
        var pagesize = parseInt($GlobalFeeData.resultPerPage);
        var total = parseInt($GlobalFeeData.totalRow);
        var start = parseInt($GlobalFeeData.startPage);

        var currentpagenumber = Math.ceil((start / pagesize));
        var startpagenumber = currentpagenumber - currentpagenumber % 10;
        var numberofpages = Math.ceil((total - startpagenumber * pagesize) / pagesize);


        numberofpages = numberofpages > 10 ? 10 : (numberofpages == 0 ? 1 : numberofpages);
        var paginghtml = '';

        if (startpagenumber * pagesize <= start && start > 0 && startpagenumber > 0)
            paginghtml = paginghtml + "<li><a id='page" + ((startpagenumber * pagesize) - pagesize) + "' class='mappager numpage'>...</a></li>";
        for (var i = startpagenumber; i < startpagenumber + numberofpages; i++) {
            if (i == currentpagenumber)
                paginghtml = paginghtml + "<li class='active'><a>" + (i + 1) + "</a></li>";
            else
                paginghtml = paginghtml + "<li class='normal'><a id='page" + (i * pagesize) + "' class='mappager numpage'>" + (i + 1) + "</a></li>";
        }
        if (((startpagenumber + 10) * pagesize) < total)
            paginghtml = paginghtml + "<li><a  href='javascript:void(0)' id='page" + (i * pagesize) + "' class='mappager numpage'>...</a></li>";

        var prevsection = '<li class="mappager first"><a href="javascript:void(0)">« First</a></li><li class="mappager previous"><a href="javascript:void(0)">« Previous</a></li>';
        var nextsection = '<li class="mappager next"><a href="javascript:void(0)">Next »</a></li><li class="mappager last"><a href="javascript:void(0)">Last »</a></li>';
        numericcontainer.find('ul').html(prevsection + paginghtml + nextsection);
        if (currentpagenumber == 0) {
            var _id = $('#numericcontainerFee ul li.first');
            _id.addClass('firstInactive');
            _id = $('#numericcontainerFee ul li.previous');
            _id.addClass('previousInactive');

        }
        else {
            var _id = $('#numericcontainerFee ul li.first');
            _id.removeClass('firstInactive');
            _id = $('#numericcontainerFee ul li.previous');
            _id.removeClass('previousInactive');
        }
        if ((start + pagesize) >= total) {
            var _id = $('#numericcontainerFee ul li.last');
            _id.addClass('lastInactive');
            _id = $('#numericcontainerFee ul li.next');
            _id.addClass('nextInactive');
            _id.removeClass('next');
        }
        else {
            var _id = $('#numericcontainerFee ul li.last');
            _id.removeClass('lastInactive');
            _id = $('#numericcontainerFee ul li.next');
            _id.removeClass('nextInactive');
        }

        $('.mappager').click(function () {
            if ($(this).hasClass('numpage'))
                $GlobalFeeData.startPage = $(this).attr('id').replace('page', '');
            else if ($(this).hasClass('first')) {
                $GlobalFeeData.startPage = 0;
            }
            else if ($(this).hasClass('next')) {

                if ($GlobalFeeData.startPage < total)
                    $GlobalFeeData.startPage = parseInt($GlobalFeeData.startPage) + parseInt($GlobalFeeData.resultPerPage);
                else
                    return false;

            }
            else if ($(this).hasClass('last')) {
                var modulovalue = (total % $GlobalFeeData.resultPerPage);
                $GlobalFeeData.startPage = (modulovalue == '0') ? (total - $GlobalFeeData.resultPerPage) : (total - modulovalue);

                //$GlobalFeeData.startPage = total - (total % $GlobalFeeData.resultPerPage);

                if ($(this).hasClass('lastInactive'))
                    return false;
            }
            else if ($(this).hasClass('previous')) {
                $GlobalFeeData.startPage = $GlobalFeeData.startPage - $GlobalFeeData.resultPerPage;
                if ($GlobalFeeData.startPage < 0)
                    $GlobalFeeData.startPage = 0;
            }
            if ($(this).hasClass('nextInactive'))
                return false;
            if ($(this).hasClass('previousInactive'))
                return false;
            if ($(this).hasClass('firstInactive', 'previousInactive', 'lastInactive', 'nextInactive'))
                return false;
            FeeDataCall();
        });
        $('#txtPageToGo').keypress(function (e) {
            if (e.which == 13) {
                var page = parseInt($(this).val());
                var lastpage = total - (total % $GlobalFeeData.resultPerPage);
                if (page <= lastpage) {
                    $GlobalFeeData.startPage = $GlobalFeeData.resultPerPage * (page - 1);
                    if ($GlobalFeeData.startPage >= total)
                        $GlobalFeeData.startPage = 0;
                    FeeDataCall();
                }
            }
        });

    }
    catch (err) {
        showError('Unable to create paging due to the following error occurred : ' + err.message);
    }
}

function scrollToTop() {
    $("html, body").animate({ scrollTop: 0 }, "slow");
    return false;
}