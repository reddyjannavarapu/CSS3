$(document).ready(function () {

    $GlobalData = {};

    SetPageAttributes('liNewDI', 'DI', 'New DI', 'liDI');
    $("#dvNote").hide();
    $('#Clear').show();
    $("#btnCancel").hide();
    $('#divPartial').show();
    $('#btnAdd').unbind('click').click(function () {
        try {
            $GlobalData.IsArchived = $("#chkArchivedReadOnly").is(":checked");
            $GlobalData.IsAdhocBilling = $("#chkAdhocBillingReadOnly").is(":checked");
            var AtrrValue1 = $(this).attr('Value');
            DisbursementItemsCall();
        } catch (e) {
            console.log(e);
        }
    });

    $("#btnArchive").unbind('click').click(function () {
        try {
            $GlobalData.IsArchived = true;
            var AtrrValue = $("#btnAdd").attr('Value');

            $GlobalData.IsAdhocBilling = false;
            DisbursementItemsCall();

        } catch (e) {
            console.log(e);
        }
    });

    $("#btnAdhocBilling").unbind('click').click(function () {
        try {
            $GlobalData.IsAdhocBilling = true;
            var AtrrValue = $("#btnAdd").attr('Value');

            $GlobalData.IsArchived = false;
            DisbursementItemsCall();

        } catch (e) {
            console.log(e);
        }
    });



    $("#btnCancel").click(function () {
        $('#ddlChosenClient_ddlCompany').val('').trigger('chosen:updated');
    });
    $('#Clear').click(function () {
        $('#ddlChosenClient_ddlCompany').val('').trigger('chosen:updated');
    });

    function DisbursementItemsCall() {
        try {
            var ItemNumber = 0;
            var Amount = 0;
            var unitPrice = 0;
            var Quantity = 0;
            var DocRef = '';
            var WorkOrderID = '';

            var DateInoccured = '';
            var Description = '';

            var AtrrValue = $("#btnAdd").attr('Value');
            ItemNumber = $("#ddlDIItemChosen_tempItemNOParent").find("option:selected").val();
            Amount = $("#txtAmount").val();
            unitPrice = $("#txtUnitPrice").val();
            Quantity = $("#txtQuantity").val();
            DocRef = $("#txtvenderRef").val();

            DateInoccured = $("#txtDateIncurred").val();

            var NeedVerification = $("#hdnNeedVerification").val();
            if (NeedVerification == "false")
                $GlobalData.NeedVerification = false;
            else $GlobalData.NeedVerification = true;

            var CompanySource = $('#ddlChosenClient_ddlCompany').find('option:selected').attr('sourceid');
            var CompanyID = $('#ddlChosenClient_ddlCompany').find('option:selected').attr('clientcode');

            if (CompanyID == undefined || CompanySource == undefined) {
                ShowNotify('Please select Company.', 'error', 3000);
                return false;
            }

            if (ItemNumber == -1 || ItemNumber == undefined || ItemNumber == '') {
                ShowNotify('Please select Item Number.', 'error', 3000);
                return false;
            }

            if (unitPrice == '' || unitPrice == undefined) {
                ShowNotify('Please select Unit Price.', 'error', 3000);
                return false;
            }

            var UnitPriceForMaxlength = $("#txtUnitPrice");
            var UnitPriceMaxLengthValidation = ValidateMaxLength(UnitPriceForMaxlength, 18);
            if (UnitPriceMaxLengthValidation == 1) {
                return false;
            }

            if (Quantity == '') {
                ShowNotify('Please enter Quantity.', 'error', 3000);
                return false;
            }

            var QuantityForMaxlength = $("#txtQuantity");
            var QuantityMaxLengthValidation = ValidateMaxLength(QuantityForMaxlength, 6);
            if (QuantityMaxLengthValidation == 1) {
                return false;
            }

            if (Amount == '') {
                ShowNotify('Please enter Amount.', 'error', 3000);
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
            if ($GlobalData.IsArchived == true) {
                ArchivedActionOnNewDI(ItemNumber, Quantity, Amount, $GlobalData.IsAdhocBilling, $GlobalData.IsArchived, DocRef, Description, unitPrice, DateInoccured, $GlobalData.NeedVerification, CompanyID, CompanySource);
            }
                //if (WOCodeReadOnly != '' && WOCodeReadOnly != undefined && ClientReadOnly != '' && ClientReadOnly != undefined)
            else
                CallDIForClient("InsertDIForClient", '', '', "{'ItemNumber':" + ItemNumber + ",'Quantity':" + Quantity + ",'Amount':" + Amount + ",'IsAdhocBilling':" + $GlobalData.IsAdhocBilling + ",'IsArchived':" + $GlobalData.IsArchived + ",'VenderRefID':'" + DocRef + "','Description':'" + escape($.trim(Description)) + "','UnitPrice':" + unitPrice + ",'DateIncurred':'" + DateInoccured + "','NeedVerification':" + $GlobalData.NeedVerification + ",'CompanyID':" + CompanyID + ",'CompanySource':'" + CompanySource + "','InHouseComment':''}", '', CreateCallBack);

        } catch (e) {
            console.log(e);
        }
    }

});





function ArchivedActionOnNewDI(ItemNumber, Quantity, Amount, IsAdhocBilling, IsArchived, DocRef, Description, unitPrice, DateInoccured, NeedVerification, CompanyID, CompanySource) {
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
                        CallDIForClient("InsertDIForClient", '', '', "{'ItemNumber':" + ItemNumber + ",'Quantity':" + Quantity + ",'Amount':" + Amount + ",'IsAdhocBilling':" + IsAdhocBilling + ",'IsArchived':" + IsArchived + ",'VenderRefID':'" + DocRef + "','Description':'" + escape($.trim(Description)) + "','UnitPrice':" + unitPrice + ",'DateIncurred':'" + DateInoccured + "','NeedVerification':" + NeedVerification + ",'CompanyID':" + CompanyID + ",'CompanySource':'" + CompanySource + "','InHouseComment':'" + escape($.trim(txtInhouseComment)) + "'}", '', CreateCallBack);
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














function CreateCallBack() {
    try {
        if ($GlobalData.InsertedID == '1') {
            ClearAllPartialValues();

            ShowNotify('Success.', 'success', 2000);
            return false;
        }
        else if ($GlobalData.InsertedID == '2') {
            ShowNotify('Doc RefID already Verified.', 'error', 3000);
            return false;
        }
    } catch (e) {
        console.log(e);
    }
}

function funToAssignVRIDAndAmount(Amount, VRFID) {
    if (Amount != "" && VRFID != "")
        $("#txtQuantity").val('1');

    $("#txtvenderRef").val(VRFID);
    $("#txtUnitPrice").val(Amount);
    $("#txtAmount").val(Amount);

    if (Amount != undefined && Amount != 'undefined' && Amount != '') {
        $("#spnAmountOfEditVendorRef").text('(Vendor Amount : ' + Amount + ')');
    }
    else {
        $("#spnAmountOfEditVendorRef").text('');
    }

}
function CallDIForClient(path, templateId, containerId, parameters, clearContent, callBack) {
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