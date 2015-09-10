$(document).ready(function () {

    $GlobalData = {};
    $GlobalData.Type = ''
    $GlobalData.InsertedID = ''
    $GlobalData.DIID = 0
    $GlobalData.UnitPrice = 0
    $GlobalData.Code = ''
    $GlobalData.Amount = 0
    $GlobalData.Quanity = 0;
    $GlobalData.Description = '';
    $GlobalData.NeedVerification = ''
    $GlobalData.UpdatedStatus = false;
    $GlobalData.IsAdhocBilling = false;
    $GlobalData.IsArchived = false;
    $GlobalData.IsMatched = false;
    $GlobalData.Action = ''
    $GlobalData.ID = '0';
    $GlobalData.WOCode = '';
    $GlobalData.ClientName = '';

    ApplyMaxLengthForDescription();

    var txtInHouseDescription = $('#txtInhouseComment');
    textareaLimiter(txtInHouseDescription, 250);



    $('#btnAddDynamicDescription').click(function () {
        try {
            var checkforempty = 0;

            $('.DescriptionMultiLine').each(function () {
                var val = $.trim($(this).val());
                if (val == '') {
                    checkforempty = 1;
                }
            });

            if (checkforempty != 1) {
                var index = $(this).closest('tr').index();
                var txtCount = $('.DescriptionMultiLine').length;
                var eq = index + (txtCount - 1);

                $('#tblDisbursementItemsForEdit tr:eq(' + eq + ')').after('<tr><td></td><td><br/><textarea id="txtdesc" maxlength="250" class="DescriptionMultiLine form-control special" style="resize:none;"></textarea></td></td></tr>');
            }
            ApplyMaxLengthForDescription();
        } catch (e) {
            console.log(e);
        }
    });

    $("#btnAdd").click(function () {
        try {
            $GlobalData.IsArchived = $("#chkArchivedReadOnly").is(":checked");
            $GlobalData.IsAdhocBilling = $("#chkAdhocBillingReadOnly").is(":checked");
            var AtrrValue1 = $(this).attr('Value');
            DisbursementItemsCall();
        } catch (e) {
            console.log(e);
        }
    });

    $("#btnArchive").click(function () {
        try {
            $GlobalData.IsArchived = true;
            var AtrrValue = $("#btnAdd").attr('Value');
            if (AtrrValue == 'Add') {
                $GlobalData.IsAdhocBilling = false;
                DisbursementItemsCall();
            }
            else {
                $GlobalData.Action = 'AR';
                var hdnDisbursementItemID = GetHiddenDIvalue();
                $GlobalData.ID = parseInt(hdnDisbursementItemID);
                ArchivedActionOnDisbursementItems($GlobalData.ID, $GlobalData.IsArchived, $GlobalData.Action);
            }
        } catch (e) {
            console.log(e);
        }
    });

    $("#btnUNArchive").click(function () {
        try {
            $GlobalData.IsArchived = false;
            $GlobalData.Action = 'AR';
            var hdnDisbursementItemID = GetHiddenDIvalue();
            $GlobalData.ID = parseInt(hdnDisbursementItemID);
            UnArchivedActionOnDisbursementItems($GlobalData.ID, $GlobalData.IsArchived, $GlobalData.Action);
        } catch (e) {
            console.log(e);
        }
    });

    $("#btnUNAdhocBilling").click(function () {
        try {
            $GlobalData.IsAdhocBilling = false;
            $GlobalData.Action = 'AD'
            var hdnDisbursementItemID = GetHiddenDIvalue();
            $GlobalData.ID = parseInt(hdnDisbursementItemID);
            AdhocActionOnDisbursementItems($GlobalData.ID, $GlobalData.IsAdhocBilling, $GlobalData.Action);
        } catch (e) {
            console.log(e);
        }
    });

    $("#btnAdhocBilling").click(function () {
        try {
            $GlobalData.IsAdhocBilling = true;
            var AtrrValue = $("#btnAdd").attr('Value');
            if (AtrrValue == 'Add') {
                $GlobalData.IsArchived = false;
                DisbursementItemsCall();
            }
            else {
                $GlobalData.Action = 'AD';
                var hdnDisbursementItemID = GetHiddenDIvalue();
                $GlobalData.ID = parseInt(hdnDisbursementItemID);
                AdhocActionOnDisbursementItems($GlobalData.ID, $GlobalData.IsAdhocBilling, $GlobalData.Action);
            }
        } catch (e) {
            console.log(e);
        }
    });

    $("#btnDelete").click(function () {
        try {
            var DID = $("#hdnDisbursementItemID").val();
            if (DID != '' && DID != undefined && DID != 0)
                functionDeleteDisbursementData(DID);
            NewDiInPartial();
            return false;
        } catch (e) {
            console.log(e);
        }
    });

    $("#btnCancel").click(function () {
        try {
            $("#divPartial").hide();
            ClearAllPartialValues();
        } catch (e) {
            console.log(e);
        }
    });
    $("#Clear").click(function () {
        try {
            ClearAllPartialValues();
        } catch (e) {
            console.log(e);
        }
    });
    $('#btnFixAccpacStatus').click(function () {
        try {
            var DIID = $("#hdnDisbursementItemID").val();
            CallServices1('UpdateAccpacStatusByDIID', '', '', "{'DIID':" + DIID + "}", '', FixAccpacStatusCallBack);
        } catch (e) {
            console.log(e);
        }
    });

    function FixAccpacStatusCallBack() {
        if ($GlobalData.InsertedID > 0) {
            $('#btnFixAccpacStatus').hide();
            //ShowNotify('Success.', 'success', 2000);
            //ClearAllPartialValues();
            InsertedStatus();
        }
    }

    function DisbursementItemsCall() {
        try {
            var ItemNumber = 0;
            var Amount = 0;
            var unitPrice = 0;
            var Quantity = 0;
            var txtvenderRef = '';
            var WorkOrderID = '';
            var hdnWorkOrderID = 0;
            var DateInoccured = '';
            var Description = '';
            var AtrrValue = $("#btnAdd").attr('Value');
            var WOCodeReadOnly = $("#txTwoReadOnly").val();
            var ClientReadOnly = $("#txtClientReadOnly").val();

            // ItemNumber = $("#tempItemNOParent").find("option:Selected").val();
            ItemNumber = $("#ddlDIItemChosen_tempItemNOParent").find("option:selected").val();
            Amount = $("#txtAmount").val();
            unitPrice = $("#txtUnitPrice").val();
            Quantity = $("#txtQuantity").val();
            txtvenderRef = $("#txtvenderRef").val();
            hdnWorkOrderID = GetHiddenWorkOrdervalue();

            DateInoccured = $("#txtDateIncurred").val();
            if (AtrrValue == 'Update') {
                var hdnDisbursementItemID = GetHiddenDIvalue();
                $GlobalData.ID = parseInt(hdnDisbursementItemID);
                $GlobalData.UpdatedStatus = true;
            }
            else {
                $GlobalData.ID = 0;
                $GlobalData.UpdatedStatus = false;
            }

            var NeedVerification = $("#hdnNeedVerification").val();
            if (NeedVerification == "false")
                $GlobalData.NeedVerification = false;
            else $GlobalData.NeedVerification = true;

            if (ItemNumber == -1 || ItemNumber == undefined || ItemNumber == '') {
                ShowNotify('Please select/enter all mandatory fields.', 'error', 3000);
                return false;
            }

            if (Quantity == '') {
                ShowNotify('Please select/enter all mandatory fields.', 'error', 3000);
                return false;
            }

            if (unitPrice == '' || unitPrice == undefined) {
                ShowNotify('Please select/enter all mandatory fields.', 'error', 3000);
                return false;
            }

            var UnitPriceForMaxlength = $("#txtUnitPrice");
            var UnitPriceMaxLengthValidation = ValidateMaxLength(UnitPriceForMaxlength, 18);
            if (UnitPriceMaxLengthValidation == 1) {
                return false;
            }

            var QuantityForMaxlength = $("#txtQuantity");
            var QuantityMaxLengthValidation = ValidateMaxLength(QuantityForMaxlength, 6);
            if (QuantityMaxLengthValidation == 1) {
                return false;
            }

            $('.DescriptionMultiLine').each(function (index, Value) {
                var val = $.trim($(this).val());
                if (val != '') {
                    if (index == 0)
                        Description = val;
                    else
                        Description = Description + '~^' + val;
                }
            });


            if (Quantity == 0) {
                ShowNotify('Quantity must not be zero, Please enter valid Quantity.', 'error', 3000);
                return false
            }
            if (unitPrice == 0) {
                ShowNotify('Unit Price must not be zero, Please enter valid Unit Price.', 'error', 3000);
                return false
            }
            if ($GlobalData.IsArchived == true) {
                ArchivedActionOnDisbursementItem($GlobalData.ID, ItemNumber, Quantity, hdnWorkOrderID, Amount, $GlobalData.IsAdhocBilling, txtvenderRef, Description, unitPrice, DateInoccured, $GlobalData.NeedVerification);
            }
            else
                CallServices1("InsertDisbursementItems", '', '', "{'ID':" + $GlobalData.ID + ",'ItemNumber':" + ItemNumber + ",'Quantity':" + Quantity + ",'WOID':" + hdnWorkOrderID + ",'Amount':" + Amount + ",'IsAdhocBilling':" + $GlobalData.IsAdhocBilling + ",'IsArchived':" + $GlobalData.IsArchived + ",'VenderRefID':'" + escape($.trim(txtvenderRef)) + "','Description':'" + escape($.trim(Description)) + "','UnitPrice':" + unitPrice + ",'DateInoccured':'" + DateInoccured + "','NeedVerification':" + $GlobalData.NeedVerification + ",'InHouseComment':''}", '', InsertedStatus);

        } catch (e) {
            console.log(e);
        }
    }


    function ArchivedActionOnDisbursementItem(ID, ItemNumber, Quantity, hdnWorkOrderID, Amount, IsAdhocBilling, txtvenderRef, Description, unitPrice, DateInoccured, NeedVerification) {
        $("#txtInhouseDIComments").val('')
        try {
            $("#dialog-confirmForDIInhouse").removeClass('hide').dialog({
                width: "450",
                resizable: false,
                modal: true,
                title_html: true,
                buttons: [
                    {
                        html: "<i class='ace-icon fa fa-check bigger-110'></i>&nbsp; Save",
                        "class": "Add btn btn-info btn-xs",
                        click: function () {
                            var txtInhouseComment = $.trim($("#txtInhouseDIComments").val());
                            CallServices1("InsertDisbursementItems", '', '', "{'ID':" + ID + ",'ItemNumber':" + ItemNumber + ",'Quantity':" + Quantity + ",'WOID':" + hdnWorkOrderID + ",'Amount':" + Amount + ",'IsAdhocBilling':" + IsAdhocBilling + ",'IsArchived':" + true + ",'VenderRefID':'" + txtvenderRef + "','Description':'" + escape($.trim(Description)) + "','UnitPrice':" + unitPrice + ",'DateInoccured':'" + DateInoccured + "','NeedVerification':" + NeedVerification + ",'InHouseComment':'" + escape($.trim(txtInhouseComment)) + "'}", '', InsertedStatus);
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



    function InsertedStatus() {
        try {
            if ($GlobalData.InsertedID == -1) {
                ShowNotify('You do not have permission on this action.', 'error', 3000);
                return false;
            }

            else if ($GlobalData.InsertedID == 2) {
                ShowNotify('Vendor referance Number is already verified.', 'error', 3000);
                return false;
            }

            var Action = window.location.href;
            Action = Action.split('/').pop();
            if (Action == 'SearchDisbursementItems') {
                DICallForAllActions();
            }
            else {
                $GlobalData.WorkOrderID = $("#hdnWorkOrderID").val();
                GetQueryString($GlobalData.WorkOrderID);
            }
            ClearDescriptionToOneTextArea();
            var updateAttr = $("#btnAdd").attr("Value");
            if (updateAttr == "Update") {
                $("#divPartial").hide();
                if ($('#hdnWOCloseStatus').val() != 'hide') {
                    $("#divCreateNewDI").show();
                }
                $("#btnAdd").attr("Value", "Add");

                ShowNotify('Success.', 'success', 2000);
            }
            else {
                $("#divCreateNewDI").hide();
                $("#divPartial").show();
                ShowNotify('Success.', 'success', 2000);
            }
            ClearAllPartialValues();
            $("#divSearchDisbursementItemsList").show();
            return false;

        } catch (e) {
            console.log(e);
        }
    }


    $("#ddlDIItemChosen_tempItemNOParent").change(function () {
        try {
            $GlobalData.DIID = $(this).find("option:Selected").val();
            if ($GlobalData.DIID != -1 && $GlobalData.DIID != '' && $GlobalData.DIID != 'undefined') {
                CallServices("GetMDITypeDetailsByItemNumber", '', '', "{DIID:" + $GlobalData.DIID + "}", '', MDITypeDetailsCallBack);
            }
            else {
                $("#txtDICode").val('');
                $("#txtQuantity").val('');
                $("#txtUnitPrice").val('');
                $("#txtAmount").val('');
                $("#spnAmountOfEditVendorRef").text('');
                $("#txtdesc").val('');
                ClearDescriptionToOneTextArea();
            }

        } catch (e) {
            console.log(e);
        }
    });

    $("#txtQuantity").keyup(function () {
        try {
            CaliculateAmount();
        } catch (e) {
            console.log(e);
        }
    });

    $("#txtUnitPrice").keyup(function () {
        try {
            CaliculateAmount();
        } catch (e) {
            console.log(e);
        }
    });

    function CaliculateAmount() {
        try {
            $GlobalData.Quanity = $("#txtQuantity").val();
            $GlobalData.UnitPrice = $("#txtUnitPrice").val();
            $GlobalData.Amount = 0;
            if ($GlobalData.Quanity != 0 && $GlobalData.UnitPrice != 0 && $GlobalData.Quanity != '' && $GlobalData.UnitPrice != '') {
                $GlobalData.Amount = $GlobalData.UnitPrice * $GlobalData.Quanity;
                $("#txtAmount").val($GlobalData.Amount);
            }
            else {
                $("#txtAmount").val('');
            }
        } catch (e) {
            console.log(e);
        }
    }

    function MDITypeDetailsCallBack() {
        try {
            $("#txtUnitPrice").val($GlobalData.UnitPrice);
            $("#txtDICode").val($GlobalData.Code);


            var arrDescription = [];
            arrDescription = Description.split('~^');
            var ArrLength = arrDescription.length;
            ClearDescriptionToOneTextArea();

            if (ArrLength > 1) {

                var tableRowValue = 0;
                $.each(arrDescription, function (index, value) {
                    if (index >= 1) {
                        if (value != '') {
                            $('#tblDisbursementItemsForEdit tr:eq(' + tableRowValue + ')').after('<tr><td></td><td><br/><textarea id="txtdesc" maxlength="250" class="DescriptionMultiLine form-control special" style="resize: none;">' + value + '</textarea></td></td></tr>');
                            tableRowValue++;
                        }
                    }
                    else {
                        $("#txtdesc").val(value);
                    }

                });
            }
            else
                $("#txtdesc").val(Description);

            ApplyMaxLengthForDescription();


            //$("#txtdesc").val($GlobalData.Description);
            $("#hdnNeedVerification").val($GlobalData.NeedVerification);
            $('#txtQuantity').val($GlobalData.Quantity);
            var Quantity = $("#txtQuantity").val();
            if (Quantity != '' && Quantity != 0) {
                $("#txtAmount").val(Quantity * $GlobalData.UnitPrice);
            }
        } catch (e) {
            console.log(e);
        }
    }

    $('#btnView').click(function () {
        try {
            var desc;
            $('.DescriptionMultiLine').each(function (index, value) {
                if (index == 0)
                    desc = $(this).val();
                else desc = desc + '<br/>' + $(this).val();
            });
            $('#spnDescription').html(desc);

            $('#divDescription').modal('show');
        } catch (e) {
            console.log(e);
        }
    });

    $('.close-btn').click(function () {
        try {
            Popup.hide('divDescription');
        } catch (e) {
            console.log(e);
        }
    });

    $('#dvheaderDIItems').click(function () {
        try {
            if ($('#widget1').hasClass('collapsed')) {
                $('#widget1').removeClass('collapsed');
                $('#widget1').find('.fa-chevron-down').removeClass('fa-chevron-down').addClass('fa-chevron-up');
            }
            else {
                $('#widget1').addClass('collapsed');
                $('#widget1').find('.fa-chevron-up').removeClass('fa-chevron-up').addClass('fa-chevron-down');
            }
        } catch (e) {
            console.log(e);
        }
    });

});

function hidePartialDItems() {
    try {
        $('#divPartial').hide();
    } catch (e) {
        console.log(e);
    }
}

function NewDiInPartial() {
    try {
        if ($('#hdnWOCloseStatus').val() != 'hide') {
            $('#divCreateNewDI').show();
        }
    } catch (e) {
        console.log(e);
    }
}

function LoadAfterCancel() {
    try {
        CheckQueryString();
        $GlobalData.WorkOrderID = $("#hdnWorkOrderID").val();
        GetQueryString($GlobalData.WorkOrderID);
    } catch (e) {
        console.log(e);
    }
}

function CallDataByWorkOrderIDAfterCallBack() {
    try {
        $GlobalData.WorkOrderID = GetHiddenWorkOrdervalue();
        GetQueryString($GlobalData.WorkOrderID);
    } catch (e) {
        console.log(e);
    }
}

function GetHiddenWorkOrdervalue() {
    try {
        var hdnWorkOrderID = $("#hdnWorkOrderID").val();
        return hdnWorkOrderID;
    } catch (e) {
        console.log(e);
    }
}

function GetHiddenDIvalue() {
    try {
        var hdnDisbursementItemID = $("#hdnDisbursementItemID").val();
        return hdnDisbursementItemID;
    } catch (e) {
        console.log(e);
    }
}

$GlobalData = {};
$GlobalData.ID = '';
$GlobalData.CallBack = '';
$GlobalData.venderRefID = ''

//function LoadPartialData() {
//    try {
//        CallServices("GetMDIType", 'tempItemchild', 'tempItemNOParent', "{}", false, '');
//    } catch (e) {
//        console.log(e);
//    }
//}

function ClearAllPartialValues() {
    try {
        $('#txtQuantity').val('');
        $('#txtAmount').val('');
        $("#spnAmountOfEditVendorRef").text('');
        $('#txtDICode').val('');
        $('#txtUnitPrice').val('');
        $('#txtdesc').val('');
        $('#txtvenderRef').val('');
        $('#txtDateIncurred').val('');

        $('#chkverified').prop("checked", false);
        $('#chkAdhocBillingReadOnly').prop("checked", false);
        $('#chkArchivedReadOnly').prop("checked", false);
        $('#chkBilledReadOnly').prop("checked", false);

        $('#divRelatedDI').hide();
        $('#spanDICount').text('');

        $('#ddlDIItemChosen_tempItemNOParent').val('').trigger('chosen:updated');
        //$("#tempItemNOParent").val("-1");
        $('#btnAdd').attr('value', 'ADD');

        $("#btnView").hide();
        ClearDescriptionToOneTextArea();
        $("#hdnDisbursementItemID").val("0");
    } catch (e) {
        console.log(e);
    }
}

function GetDisbursementItemsByID() {
    try {
        CallServices("GetDisbursementItemsByID", "", "", "{'ID':" + parseInt($GlobalData.ID) + ",'VenderRefID':'" + $GlobalData.venderRefID + "'}", true, $GlobalData.CallBack);
    } catch (e) {
        console.log(e);
    }
}

function EditCallBack() {
    try {
        // $("#txTwoReadOnly").val(WorkOrderNumber);
        // $("#txtClientReadOnly").val(ClientNumber);
        //$("#tempItemNOParent").val(Type);
        $('#ddlDIItemChosen_tempItemNOParent').val(Type).trigger('chosen:updated');

        $("#txtDICode").val(ItemCode);
        $("#txtQuantity").val(Units);
        $("#txtUnitPrice").val(UnitPrice);
        $("#txtAmount").val(Amount);
        $("#hdnNeedVerification").val($GlobalData.NeedVerification);
        var arrDescription = [];
        arrDescription = Description.split('~^');
        var ArrLength = arrDescription.length;
        ClearDescriptionToOneTextArea();
        if (ArrLength > 1) {

            var tableRowValue = 0;
            $.each(arrDescription, function (index, value) {
                if (index >= 1) {
                    if (value != '') {
                        $('#tblDisbursementItemsForEdit tr:eq(' + tableRowValue + ')').after('<tr><td></td><td><br/><textarea id="txtdesc" maxlength="250" class="DescriptionMultiLine form-control special" style="resize: none;">' + value + '</textarea></td></td></tr>');
                        tableRowValue++;
                    }
                }
                else {
                    $("#txtdesc").val(value);
                }

            });
        }
        else
            $("#txtdesc").val(Description);

        ApplyMaxLengthForDescription();

        $("#txtvenderRef").val(VenderRefID);
        $("#txtDateIncurred").val(DateIncurred);
        if (RelatedDICount <= 0 || RelatedDICount == undefined || RelatedDICount == "") {
            $("#divRelatedDI").hide();
        }
        else {
            $("#spanDICount").text(RelatedDICount);
            $("#divRelatedDI").show();
        }

        if (IsMatched == true) {
            $("#chkMatched").prop("checked", true);
        }
        else {
            $("#chkMatched").prop("checked", false);
        }

        if (IsVerified == true) {
            $("#chkverified").prop("checked", true);
        }
        else {
            $("#chkverified").prop("checked", false);
        }

        if (IsVerified == true || IsMatched == true) {
            $('#txtvenderRef').attr("disabled", "disabled");
        }
        else {
            $('#txtvenderRef').removeAttr("disabled");
        }
        

        if (IsAdhoc == true)
            $("#chkAdhocBillingReadOnly").prop("checked", true);
        else $("#chkAdhocBillingReadOnly").prop("checked", false);
        if (IsArchived == true)
            $("#chkArchivedReadOnly").prop("checked", true);
        else $("#chkArchivedReadOnly").prop("checked", false);
        if (IsBilled == true)
            $("#chkBilledReadOnly").prop("checked", true);
        else $("#chkBilledReadOnly").prop("checked", false);

        if (VendorAmount == 0 || VendorAmount == undefined)
            VendorAmount = '';
        if (VRID == 0 || VRID == undefined)
            VRID = '';

        $("#txtVenderAmountReadOnly").val(VendorAmount);
        $("#txtVenderReportReadOnly").val(VRID);

        if (ACCPACStatus != '0') {
            $('#btnFixAccpacStatus').show();
            $('#btnFixAccpacStatus').attr('data-original-title', ACCPACDescription + ' <br/>' + ACCPACExplanation);
            $('#btnFixAccpacStatus').tooltip({ html: true });
        }
        else {
            $('#btnFixAccpacStatus').hide();
        }

        $("#btnView").show();
        $("#divPartial").show();
        $("#divEvents").show();
        $('#Clear').hide();
        $('#divCreateNewDI').hide();
        $('#btnAddNoteInDI').show();

    } catch (e) {
        console.log(e);
    }
}

function ShowCallBack() {
    try {
        $("#lblshowTwoReadOnly").text(WorkOrderNumber);
        $("#lblshowClientReadOnly").text(ClientNumber);
        $("#labelItemNO").text(ItemNumber);
        $("#labelDICode").text($GlobalData.Code);
        $("#labelQuantity").text(Units);
        $("#labelUnitPrice").text($GlobalData.UnitPrice);
        $("#labelAmount").text(Amount);
        $("#labelDateIncurred").text(DateIncurred);
        $("#labelDocRef").text(VenderRefID);
        $("#lblCreatedDate").text(CreatedDate);
        $("#labelInvoiceNumber").text(InvoiceNumber);
        var arrDescription = [];
        arrDescription = Description.split('~^');
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
        $("#labelComments").html(Description);
        if (RelatedDICount <= 0 || RelatedDICount == undefined || RelatedDICount == "") {
            $("#divDI").hide();
        }
        else {
            $("#divDI").show();
            $("#spanDICount1").text(RelatedDICount);
        }

        if (IsMatched == true) {
            $('.chkshowMatched').prop("checked", true);
        }
        if (IsVerified == true) {
            $('.chkshowverified').prop("checked", true);
        }
        else {
            $('.chkshowverified').prop("checked", false);
        }
        if (IsAdhoc == true) {
            $('.chkshowAdhocBillingReadOnly').prop("checked", true);
        }
        else {
            $('.chkshowAdhocBillingReadOnly').prop("checked", false);
        }
        if (IsArchived == true) {
            $('.chkshowArchivedReadOnly').prop("checked", true);
        }
        else {
            $('.chkshowArchivedReadOnly').prop("checked", false);
        }
        if (IsBilled == true) {
            $('.chkshowBilledReadOnly').prop("checked", true);
        }
        else {
            $('.chkshowBilledReadOnly').prop("checked", false);
        }
        $("#txtshowVenderAmountReadOnly").val(VendorAmount);
        $("#txtshowVenderReportReadOnly").val(VRID);
        $('#modal-form').modal('show');

        return false;

    } catch (e) {
        console.log(e);
    }
}

function BindItemNoInEdit(DID, venderRefID, NoteRefId, NoteType, NoteCount, callBack) {
    try {
        $GlobalData.ID = DID;
        $GlobalData.venderRefID = venderRefID;
        $GlobalData.CallBack = callBack;

        $('#dvNote').find('.NoteAddInList').show();
        $('#dvNote').find('.NoteAddInList').attr('refid', NoteRefId);
        $('#dvNote').find('.NoteAddInList').attr('notetype', NoteType);
        $('#dvNote').find('.spnNoteCount').text(NoteCount);
        if (NoteCount == 0) {
            $('#dvNote').find('.spnNoteCount').hide();
        }
        else {
            $('#dvNote').find('.spnNoteCount').show();
        }
        GetDisbursementItemsByID();
        // CallServices("GetMDIType", 'tempItemchild', 'tempItemNOParent', "{}", false, );
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

function GetDisbursementItemsByIDforDetails(DID, venderRefID, callBack) {
    try {
        $GlobalData.ID = DID;
        $GlobalData.venderRefID = venderRefID;
        $GlobalData.CallBack = callBack;
        GetDisbursementItemsByID();
    } catch (e) {
        console.log(e);
    }
}

function ApplyMaxLengthForDescription() {
    try {
        var txtareaDescription = $('.DescriptionMultiLine');
        textareaLimiter(txtareaDescription, 250);
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
                    $GlobalData.Type = msg;
                    $GlobalData.UnitPrice = msg.OrdersList[0].UnitPrice
                    $GlobalData.Code = msg.OrdersList[0].Code;
                    $GlobalData.Description = msg.OrdersList[0].Description;
                    $GlobalData.NeedVerification = msg.OrdersList[0].NeedVerification;
                    $GlobalData.Quantity = msg.OrdersList[0].Quantity;

                    ACCPACStatus = msg.OrdersList[0].ACCPACStatus;
                    ACCPACDescription = msg.OrdersList[0].ACCPACDescription;
                    ACCPACExplanation = msg.OrdersList[0].ACCPACExplanation;
                    DIID = msg.OrdersList[0].ID;
                    WOID = msg.OrdersList[0].WOID;
                    WorkOrderNumber = msg.OrdersList[0].WorkOrderNumber;
                    ClientNumber = msg.OrdersList[0].ClientNumber;
                    Type = msg.OrdersList[0].Type;
                    ItemCode = msg.OrdersList[0].Code;
                    Amount = msg.OrdersList[0].Amount;
                    Description = msg.OrdersList[0].Description;
                    DateIncurred = msg.OrdersList[0].DateIncurred;
                    VenderRefID = msg.OrdersList[0].VenderRefID;
                    IsVerified = msg.OrdersList[0].IsVerified;
                    IsAdhoc = msg.OrdersList[0].IsAdhoc;
                    IsArchived = msg.OrdersList[0].IsArchived;
                    IsBilled = msg.OrdersList[0].IsBilled;
                    Currency = msg.OrdersList[0].Currency;
                    Units = msg.OrdersList[0].Units;
                    UnitPrice = msg.OrdersList[0].UnitPrice;
                    ItemNumber = msg.OrdersList[0].ItemNumber;
                    CurrencyID = msg.OrdersList[0].CurrencyID;
                    CreatedDate = msg.OrdersList[0].CreatedDate;
                    InvoiceNumber = msg.OrdersList[0].InvoiceNumber;
                    IsMatched = msg.OrdersList[0].IsMatched;

                    RelatedDICount = msg.OrdersCount;
                    VendorAmount = msg.VendorAmount;
                    VRID = msg.VendorReport;
                    if (msg.UserList != null && msg.UserList != 'undefined') {

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

function CallServices1(path, templateId, containerId, parameters, clearContent, callBack) {
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

                    if (msg.UserList != null && msg.UserList != 'undefined') {

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