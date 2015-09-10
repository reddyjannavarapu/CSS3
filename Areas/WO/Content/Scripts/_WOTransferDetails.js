$(document).ready(function () {

    $GlobalWOTransferDetailsData = {};
    $GlobalWOTransferDetailsData.InsertedID = 0;
    $GlobalWOTransferDetailsData.WOTransferDetailsData = '';
    $GlobalWOTransferDetailsData.Transferor = '';
    $GlobalWOTransferDetailsData.Transferee = '';
    $GlobalWOTransferDetailsData.TransferorSource = '';
    $GlobalWOTransferDetailsData.TransfereeSource = '';
    $GlobalWOTransferDetailsData.SharesTransfered = '';
    $GlobalWOTransferDetailsData.Currency = '';
    $GlobalWOTransferDetailsData.Consideration = '';


    $GlobalWOTransferDetailsData.WOID = '';
    $GlobalWOTransferDetailsData.IssuedAndPaidUpCapitalCurrency = '';
    $GlobalWOTransferDetailsData.IssuedAndPaidUpCapitalClassOfShare = '';
    $GlobalWOTransferDetailsData.IsPreEmptionRights = '';
    $GlobalWOTransferDetailsData.LettertoIRAS = '';
    $GlobalWOTransferDetailsData.LettertoIRASSource = '';

    if ($('#hdnWOCloseStatus').val() == 'hide') {
        $('.btnWOClose').hide();
    }

    BindCurrencyToAddTransferDetails();
    BindWOTransferTransactionDetailsByWOID();
    TransferCurrencyCallBack();

    function BindCurrencyToAddTransferDetails() {
        try {
            CallMasterData("/PartialContent/GetCurrencyDetails", 'CurrencyDropDownTemplateinWOTransferDetails', 'ddlCurrency', "{}", false, AddTransferCurrencyCallBack)
        } catch (e) {
            console.log(e);
        }
    }
    function AddTransferCurrencyCallBack() {
        try {
            $('#ddlCurrency').val('14');
        } catch (e) {
            console.log(e);
        }
    }
    function BindWOTransferTransactionDetailsByWOID() {
        try {
            CallWOTransferDetails("/WODI/GetWOTransferTransactionDetailsByWOID", 'scriptTransferTransactionDetails', 'tblTransferDetails', "{'WOID':" + parseInt($("#hdnWOID").val()) + "}", true, WOTransferTransactionDetailsByWOIDCallBack);
        } catch (e) {
            console.log(e);
        }
    }
    function WOTransferTransactionDetailsByWOIDCallBack() {
        try {
            var tblLength = $("#tblTransferDetails").find("tr").length;
            if (tblLength > 0) {
                $("#tblTransferDetails").find(".aDelete").unbind("click");
                $("#tblTransferDetails").find(".aDelete").click(DeleteWOTransferTransactionDetailsByID);
                $("#divNoData").hide();
            }
            else {
                $("#divNoData").show();
            }

            if ($('#hdnWOCloseStatus').val() == 'hide') {
                $('.btnWOClose').hide();
            }

        } catch (e) {
            console.log(e);
        }
    }
    function DeleteWOTransferTransactionDetailsByID() {
        try {
            var TransferID = $(this).attr("id");
            $("#dialog-confirm").removeClass('hide').dialog({
                resizable: false,
                modal: true,
                title_html: true,
                buttons: [
                    {
                        html: "<i class='ace-icon fa fa-trash-o bigger-110'></i>&nbsp; Delete item",
                        "class": "btn btn-danger btn-xs",
                        click: function () {
                            CallMasterData("DeleteWOTransferTransactionDetailsByID", '', '', "{'TransferID':" + TransferID + "}", true, BindWOTransferTransactionDetailsByWOID)
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
    function TransferCurrencyCallBack() {
        try {
            var WOID = parseInt($("#hdnWOID").val());
            CallMasterData("/PartialContent/GetShareClassDetails", 'ShareClassDropDownTemplateinWOTransferDetails', 'ddlIssuedAndPaidupCapitalClassOfShare', "{'WOID':" + WOID + "}", false, TransferClassOfShareCallBack)
        } catch (e) {
            console.log(e);
        }
    }

    function TransferClassOfShareCallBack() {
        try {
            GetWoTransferDetail();
        } catch (e) {
            console.log(e);
        }
    }

    function GetWoTransferDetail() {
        try {
            var WOID = $('#hdnWOID').val();
            CallWOTransferDetails("/WODI/GetWOTransferDetailsByWOID", '', '', "{'WOID':" + WOID + "}", '', GetWOTransferDetailsCallBack);
        } catch (e) {
            console.log(e);
        }
    }

    function GetWOTransferDetailsCallBack() {
        try {
            var WoTransferDetail = $GlobalWOTransferDetailsData.WOTransferDetailsData;

            if (WoTransferDetail.IssuedAndPaidUpCapitalClassOfShare == 0 || WoTransferDetail.IssuedAndPaidUpCapitalClassOfShare == -1) {
                //$('#ddlIssuedAndPaidupCapitalClassOfShare').val('-1')
            }
            else
                $('#ddlIssuedAndPaidupCapitalClassOfShare').val(WoTransferDetail.IssuedAndPaidUpCapitalClassOfShare);

            if (WoTransferDetail.IsPreEmptionRights) {
                $('#chkPreemptionRights').prop('checked', true);
            }
            else {
                $('#chkPreemptionRights').prop('checked', false);
            }

            if (WoTransferDetail.LettertoIRAS != 0) {
                var LettertoIRAS = $("#ddlDirectorChosen_LettertoIRAS option[personid=" + WoTransferDetail.LettertoIRAS + "]option[sourcecode=" + WoTransferDetail.LettertoIRASSource + "]").attr("value");
                $('#ddlDirectorChosen_LettertoIRAS').val(LettertoIRAS).trigger('chosen:updated');
            }

        } catch (e) {
            console.log(e);
        }
    }

    $('#btnIndividual_Transferee').unbind('click').click(function () {
        $('.divIndividualOrCompany').empty();
        $("#divIndividualOrCompany_Transferee").load('/WO/partialcontent/_CreateIndividual', function () {
            $("#divIndividualOrCompany_Transferee").find('.modalIndividual').modal({
                "backdrop": "static",
                "show": "true"
            });
        });

    });

    $('#btnCompany_Transferee').unbind('click').click(function () {
        $('.divIndividualOrCompany').empty();
        $("#divIndividualOrCompany_Transferee").load('/WO/partialcontent/_CreateCompany', function () {
            $("#divIndividualOrCompany_Transferee").find('.modalCompany').modal({
                "backdrop": "static",
                "show": "true"
            });
        });

    });

    $('#btnSaveWoTransferDetails').click(function () {
        try {
            $GlobalWOTransferDetailsData.WOID = $('#hdnWOID').val();
            $GlobalWOTransferDetailsData.IssuedAndPaidUpCapitalClassOfShare = $('#ddlIssuedAndPaidupCapitalClassOfShare option:selected').val();
            $GlobalWOTransferDetailsData.IsPreEmptionRights = $('#chkPreemptionRights').is(':checked');

            var LetterIRASOptionRowId = $('#ddlDirectorChosen_LettertoIRAS option:selected').val();
            $GlobalWOTransferDetailsData.LettertoIRAS = $("#ddlDirectorChosen_LettertoIRAS option[value=" + LetterIRASOptionRowId + "]").attr("personid");
            $GlobalWOTransferDetailsData.LettertoIRASSource = $("#ddlDirectorChosen_LettertoIRAS option[value=" + LetterIRASOptionRowId + "]").attr("sourcecode");

            var JsonString = JSON.stringify({ SaveTransferData: $GlobalWOTransferDetailsData });
            CallWOTransferDetails("/WODI/InsertWOTransferDetails", '', '', JsonString, '', CreateWOTransferDetailsCallBack);

        } catch (e) {
            console.log(e);
        }
    });

    function CreateWOTransferDetailsCallBack() {
        try {
            if ($GlobalWOTransferDetailsData.InsertedID > 1) {
                ShowNotify('Success.', 'success', 3000);
                return false;
            }
        } catch (e) {
            console.log(e);
        }
    }

    $('#btnCancelWoTransferDetails').click(function () {
        try {
            //$('#ddlIssuedAndPaidupCapitalClassOfShare').val('-1');
            $('#chkPreemptionRights').attr('checked', false);
            $('#ddlDirectorChosen_LettertoIRAS').val('').trigger('chosen:updated');
        } catch (e) {
            console.log(e);
        }
    });

    $("#AddTransfer").click(function () {
        try {
            $("#ddlDirectorChosen_Transferor > option:not(:eq(0))").remove();
            $("#ddlDirectorChosen_Transferor").trigger("chosen:updated");
            $('#ddlDirectorChosen_Transferor').val('');
            $('#ddlDirectorChosen_Transferor').attr('ClassOfShare', $('#ddlIssuedAndPaidupCapitalClassOfShare').val())
            
            ClearTransactionDetails();
            $('#divAddTransferDetails').modal({
                "backdrop": "static",
                "show": "true"
            });
        } catch (e) {
            console.log(e);
        }
    });


    $("#btnSaveTransferTransaction").click(function () {
        try {
            var SelectedVals = new Array();
            SelectedVals = CheckDropDownIDAndSource("ddlDirectorChosen_Transferor");
            $GlobalWOTransferDetailsData.Transferor = SelectedVals[0];
            $GlobalWOTransferDetailsData.TransferorSource = SelectedVals[1];

            var SelectedVals = new Array();
            SelectedVals = CheckDropDownIDAndSource("ddlDirectorChosen_Transferee");
            $GlobalWOTransferDetailsData.Transferee = SelectedVals[0];
            $GlobalWOTransferDetailsData.TransfereeSource = SelectedVals[1];

            $GlobalWOTransferDetailsData.SharesTransfered = $("#txtNoOfSharesTransfered").val();
            $GlobalWOTransferDetailsData.Consideration = $("#txtConsideration").val();
            $GlobalWOTransferDetailsData.IssuedAndPaidUpCapitalCurrency = $("#ddlCurrency").val();
            $GlobalWOTransferDetailsData.WOID = $("#hdnWOID").val();

            var count = 0;
            count += ControlEmptyNess(true, $('#ddlDirectorChosen_Transferor option:selected'), 'Please select Transferor.');
            count += ControlEmptyNess(true, $('#ddlDirectorChosen_Transferee option:selected'), 'Please select Transferee.');
            count += ControlEmptyNess(true, $('#ddlCurrency'), 'Please select Currency.');
            count += ControlEmptyNess(true, $('#txtNoOfSharesTransfered'), 'Please enter No of Shares Transfered.');
            count += ControlEmptyNess(true, $('#txtConsideration'), 'Please select Consideration.');
            if (count > 0) {
                ShowNotify('Please enter all mandatory fields.', 'error', 3000);
                return false;
            }
            if ($GlobalWOTransferDetailsData.Transferor == $GlobalWOTransferDetailsData.Transferee) {
                ShowNotify('Transferor and Transferee are not equal.', 'error', 3000);
                return false;
            }
            if ($GlobalWOTransferDetailsData.SharesTransfered == 0) {
                ShowNotify('No of Shares Transfered must not be zero.', 'error', 3000);
                return false;
            }
            //if ($GlobalWOTransferDetailsData.Consideration == 0) {
            //    ShowNotify('Consideration must not be zero.', 'error', 3000);
            //    return false;
            //}

            var JsonString = JSON.stringify({ SaveTransferTransactionData: $GlobalWOTransferDetailsData });
            CallWOTransferDetails("/WODI/InsertWOTransferTransactionDetails", '', '', JsonString, '', SaveTransferTransactionDataCallBack);

        } catch (e) {
            console.log(e);
        }
    });

    function SaveTransferTransactionDataCallBack() {
        try {
            if ($GlobalWOTransferDetailsData.InsertedID == 1) {

                $('#divAddTransferDetails').modal('hide');

                ShowNotify('Success.', 'success', 3000);
                BindWOTransferTransactionDetailsByWOID();
                return false;
            }
        } catch (e) {
            console.log(e);
        }
    }
    $("#btnClear").click(function () {
        try {
            ClearTransactionDetails();
        } catch (e) {
            console.log(e);
        }
    });
    function ClearTransactionDetails() {
        try {
            $("#ddlDirectorChosen_Transferor").val("").trigger("chosen:updated");
            $("#ddlDirectorChosen_Transferee").val("").trigger("chosen:updated");
            AddTransferCurrencyCallBack();
            $("#txtNoOfSharesTransfered").val('');
            $("#txtConsideration").val('');
        } catch (e) {
            console.log(e);
        }
    }
    function CallWOTransferDetails(path, templateId, containerId, parameters, clearContent, callBack) {
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
                        $GlobalWOTransferDetailsData.WOTransferDetailsData = msg;
                        $GlobalWOTransferDetailsData.InsertedID = msg;
                        if (msg.ServiceCount != null && msg.ServiceCount != 'undefined') {
                            $GlobalData.totalRow = msg.ServiceCount;
                        }
                        if (templateId != '' && containerId != '') {
                            if (!clearContent) {
                                $.tmpl($('#' + templateId).html(), msg.WOTransferTransactionDetailsList).appendTo("#" + containerId);
                            }
                            else {
                                $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg.WOTransferTransactionDetailsList));
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

});