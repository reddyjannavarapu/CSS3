$(document).ready(function () {

    if ($('#hdnWOCloseStatus').val() == 'hide') {
        $('.btnWOClose').hide();
    }

    $GlobalWODuplicateDetailsData = {};
    $GlobalWODuplicateDetailsData.InsertedID = 0;
    $GlobalWODuplicateDetailsData.WODuplicateDetailsData = '';

    $('#txtDateOfIssue').datepicker({
        showOtherMonths: true,
        selectOtherMonths: true,
        showWeek: true,
        changeYear: true,
        changeMonth: true,
        dateFormat: 'dd/mm/yy',
        yearRange: "-10:+50"
    });

    $('#txtNoOfShares').keypress(function (event) {
        try {
            var checkCharater = AllowNumbersOnly($(this).val(), event);
            if (!checkCharater) {
                event.preventDefault();
            }
        } catch (e) {
            console.log(e);
        }
    });

    $('#txtNoOfNewCertToBeIssued').keypress(function (event) {
        try {
            var checkCharater = AllowNumbersOnly($(this).val(), event);
            if (!checkCharater) {
                event.preventDefault();
            }
        } catch (e) {
            console.log(e);
        }
    });

    BindShareClass();

    function BindShareClass() {
        try {
            var WOID = parseInt($("#hdnWOID").val());
            CallMasterData("/PartialContent/GetShareClassDetails", 'DropDownShareClassTemplateForDuplicate', 'ddlShareClassForDuplicate', "{'WOID':" + WOID + "}", false, BindDuplicate);
        } catch (e) {
            console.log(e);
        }
    }

    function BindDuplicate() {
        try {
            GetWODuplicateDetailsByWOID();
            //BindDuplicateShareHoldersDetails();
        } catch (e) {
            console.log(e);
        }
    }

    function GetWODuplicateDetailsByWOID() {
        try {

            var WOID = $('#hdnWOID').val();
            CallWODuplicateDetails("/WODI/GetWODuplicateDetailsByWOID", '', '', "{'WOID':" + WOID + "}", '', GetWODuplicateDetailsByWOIDCallBack);
        } catch (e) {
            console.log(e);
        }
    }

    function GetWODuplicateDetailsByWOIDCallBack() {
        try {
            if ($GlobalWODuplicateDetailsData.WODuplicateDetailsData.WOID != null && $GlobalWODuplicateDetailsData.WODuplicateDetailsData.WOID != undefined && $GlobalWODuplicateDetailsData.WODuplicateDetailsData.WOID != '0') {

                var WODuplicateDetails = $GlobalWODuplicateDetailsData.WODuplicateDetailsData;

                $('#ddlShareClassForDuplicate').val(WODuplicateDetails.ClassOfShare);

                BindDuplicateShareHoldersDetails();
            }
            else {
                WODuplicateShareHolderCallBack();
            }
        } catch (e) {
            console.log(e);
        }
    }

    function BindDuplicateShareHoldersDetails() {
        try {
            var WOID = $('#hdnWOID').val();
            CallWODuplicateDetails("/WODI/GetWODuplicateShareHoldersDetailsByWOID", 'scriptDuplicateDetails', 'tblDuplicateDetails', "{'WOID':" + WOID + "}", true, WODuplicateShareHolderCallBack);
        } catch (e) {
            console.log(e);
        }
    }

    function WODuplicateShareHolderCallBack() {
        try {
            var tblLength = $("#tblDuplicateDetails").find("tr").length;
            if (tblLength > 0) {
                $("#tblDuplicateDetails").find(".aDelete").unbind("click");
                $("#tblDuplicateDetails").find(".aDelete").click(DeleteDuplicateShareholderByID);
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

    function DeleteDuplicateShareholderByID() {
        try {
            var DuplicateID = $(this).attr("id");
            $("#dialog-confirm").removeClass('hide').dialog({
                resizable: false,
                modal: true,
                title_html: true,
                buttons: [
                    {
                        html: "<i class='ace-icon fa fa-trash-o bigger-110'></i>&nbsp; Delete item",
                        "class": "btn btn-danger btn-xs",
                        click: function () {
                            CallWODuplicateDetails("/WODI/DeleteWODuplicateShareholderDetailsByID", '', '', "{'DuplicateID':" + DuplicateID + "}", true, BindDuplicateShareHoldersDetails)
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

    $('#btnSaveWoDuplicateDetails').click(function () {
        try {
            WODuplicateDetails = {};
            WODuplicateDetails.WOID = $('#hdnWOID').val();
            WODuplicateDetails.ClassOfShare = $('#ddlShareClassForDuplicate option:selected').val();

            var count = 0;
            count += ControlEmptyNess(false, $('#ddlShareClassForDuplicate option:selected'), 'Please select Class of Share.');

            if (count > 0) {
                ShowNotify('Please Enter values for all mandatory fields.', 'error', 3000);
                return false;
            }
            else {
                var jsonWODuplicateDetailsText = JSON.stringify({ WODuplicateDetails: WODuplicateDetails });
                CallWODuplicateDetails("/WODI/InsertWODuplicateDetails", '', '', jsonWODuplicateDetailsText, true, CreateWODuplicateDetailsCallBack);
            }

        } catch (e) {
            console.log(e);
        }
    });

    function CreateWODuplicateDetailsCallBack() {
        try {
            if ($GlobalWODuplicateDetailsData.WODuplicateDetailsData.WOID != null) {
                ShowNotify('Success.', 'success', 2000);
                ClearWoDuplicateValues();
            }
            else {
                ShowNotify('Success.', 'success', 2000);
            }
        } catch (e) {
            console.log(e);
        }
    }

    $('#btnCancelWoDuplicateDetails').click(function () {
        try {
            ClearWoDuplicateValues();
        } catch (e) {
            console.log(e);
        }
    });

    function ClearWoDuplicateValues() {
        try {
            $('#ddlShareClassForDuplicate').val('-1');
        } catch (e) {
            console.log(e);
        }
    }

    $('#AddDuplicateShareHolder').click(function () {
        ClearPopupValues();
        $('#ddlDirectorChosen_ShareHolder').attr('ClassOfShare', $('#ddlShareClassForDuplicate').val())

        $('#divAddDuplicateShareholder').modal({
            "backdrop": "static",
            "show": "true"
        });
    });

    $('#btnSaveDuplicateShareHolder').click(function () {
        var WOID = $('#hdnWOID').val();
        var ShareHolderOptionRowId = $('#ddlDirectorChosen_ShareHolder option:selected').val();
        var personid = $("#ddlDirectorChosen_ShareHolder option[value=" + ShareHolderOptionRowId + "]").attr("personid");
        var sourcecode = $("#ddlDirectorChosen_ShareHolder option[value=" + ShareHolderOptionRowId + "]").attr("sourcecode");

        var CertNo = $('#txtCertNo').val();
        var NoOfShares = $('#txtNoOfShares').val();
        if (NoOfShares == '' || NoOfShares == undefined) {
            NoOfShares = 0;
        }
        var DateOfIssue = $('#txtDateOfIssue').val();
        var NoOfNewCertTobeIssued = $('#txtNoOfNewCertToBeIssued').val();
        if (NoOfNewCertTobeIssued == '' || NoOfNewCertTobeIssued == undefined) {
            NoOfNewCertTobeIssued = 0;
        }
        var count = 0;
        count += ControlEmptyNess(true, $('#ddlDirectorChosen_ShareHolder option:selected'), 'Please select ShareHolder.');
        //count += ControlEmptyNess(true, $('#txtCertNo'), 'Please select CertNo.');
        //count += ControlEmptyNess(true, $('#txtNoOfShares'), 'Please select No Of Shares.');
        //count += ControlEmptyNess(true, $('#txtDateOfIssue'), 'Please select Date Of Issue.');
        //count += ControlEmptyNess(true, $('#txtNoOfNewCertToBeIssued'), 'Please select No Of New Cert to be Issued.');
        
        if (count > 0) {
            ShowNotify('Please Enter values for all mandatory fields.', 'error', 3000);
            return false;
        }
        else {
            CallWODuplicateDetails("/WODI/InsertWODuplicateShareHolderDetails", '', '', "{'WOID':" + WOID + ",'personId':" + personid + ",'sourcecode':'" + sourcecode + "','CertNo':'" + CertNo + "','NoOfShares':'" + NoOfShares + "','DateOfIssue':'" + DateOfIssue + "','NoOfNewCertTobeIssued':" + NoOfNewCertTobeIssued + "}", '', InsertWODuplicateShareHolderCallBack);
        }
    });

    function InsertWODuplicateShareHolderCallBack() {
        try {
            if ($GlobalWODuplicateDetailsData.InsertedID >= 1) {
                ShowNotify('Success.', 'success', 3000);
                BindDuplicateShareHoldersDetails();
                ClearPopupValues();
                $('#divAddDuplicateShareholder').modal('hide');
                return false;
            }
        } catch (e) {
            console.log(e);
        }
    }

    $('#btnClear').click(function () {
        ClearPopupValues();
    });

    function ClearPopupValues() {
        $("#ddlDirectorChosen_ShareHolder > option:not(:eq(0))").remove();
        $('#ddlDirectorChosen_ShareHolder').val('').trigger('chosen:updated');
        $('#txtCertNo').val('');
        $('#txtNoOfShares').val('');
        $('#txtDateOfIssue').val('');
        $('#txtNoOfNewCertToBeIssued').val('');
    }

    function CallWODuplicateDetails(path, templateId, containerId, parameters, clearContent, callBack) {
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
                        $GlobalWODuplicateDetailsData.WODuplicateDetailsData = msg;
                        $GlobalWODuplicateDetailsData.InsertedID = msg;

                        if (templateId != '' && containerId != '') {
                            if (!clearContent) {
                                $.tmpl($('#' + templateId).html(), msg.WODuplicateList).appendTo("#" + containerId);
                            }
                            else {
                                $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg.WODuplicateList));
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