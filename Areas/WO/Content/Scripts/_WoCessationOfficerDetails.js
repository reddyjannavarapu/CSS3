$(document).ready(function () {
    $Status = '';

    if ($('#hdnWOCloseStatus').val() == 'hide') {
        $('.btnWOClose').hide();
    }

    BindddlPossion();
    BindWOCessationOfficerDetails();

    $("#txtDirectorContactNo_CessationOfficerDetails").keypress(function (event) {
        try {
            var checkCharater = AllowNumbersOnly($(this).val(), event);
            if (!checkCharater) {
                event.preventDefault();
            }

        } catch (e) {
            console.log(e);
        }
    });

    $("#txtDirectorFax_CessationOfficerDetails").keypress(function (event) {
        try {
            var checkCharater = AllowNumbersOnly($(this).val(), event);
            if (!checkCharater) {
                event.preventDefault();
            }

        } catch (e) {
            console.log(e);
        }
    });


    $("#ddlPosition").change(function () {

        if ($("#ddlPosition option:selected").attr('id') == 1 || $("#ddlPosition option:selected").attr('id') == 9) {

            var ClientID = $("#ddlDirectorChosen_CessationOfficerDetails").find("option:Selected").attr("personid");
            var ClientSourceID = $("#ddlDirectorChosen_CessationOfficerDetails").find("option:Selected").attr("sourcecode");
            var NatureAppoint = $("#ddlPosition").find("option:Selected").attr('id');
            var WOID = $("#hdnWOID").val();

            if (ClientID == undefined && ClientSourceID == undefined) {
                ShowNotify('Please select Cessation Officer.', 'error', 3000);
                //ShowNotify('Please Enter values for all mandatory fields.', 'error', 3000);
                return false;
            }
            else if (ClientID != '' && ClientSourceID != '' && WOID != '') {
                $("#divMainDirector").show();

                $("#ddlMainDirector > option:not(:eq(0))").remove();

                CallServicesData("/WODI/GetCessationDirectors", 'ddlDirctorTemplate', 'ddlMainDirector', "{'WOID':" + WOID + ",'DirectorID':" + ClientID + ",'DirectorSource':'" + ClientSourceID + "','NatureAppoint':" + NatureAppoint + "}", '');
            }
        }
        else {
            $("#divMainDirector").hide();
            $("#ddlMainDirector > option:not(:eq(0))").remove();
        }
    });


    function GetCessationAddressDetails(personid, sourcecode) {
        CallServicesData("/PartialContent/GetDirectorAddressDetails", "", "", "{'PersonId':" + personid + ",'SourceCode':'" + sourcecode + "'}", "", CessationDirectorAddressDetailsCallBack);
    }

    function CessationDirectorAddressDetailsCallBack() {
        if ($Status.length > 0) {
            $('#txtDirectorEmail_CessationOfficerDetails').val($Status[0].Email);
            $('#txtDirectorContactNo_CessationOfficerDetails').val($Status[0].Phone);
            $('#txtDirectorFax_CessationOfficerDetails').val($Status[0].Fax);
        }
        else {
            $('#txtDirectorEmail_CessationOfficerDetails').val("");
            $('#txtDirectorContactNo_CessationOfficerDetails').val("");
            $('#txtDirectorFax_CessationOfficerDetails').val("");
        }
    }


    $("#btnSaveCessatioOfficers").click(function () {
        var WOID = $("#hdnWOID").val();

        if (WOID != "" && WOID != undefined) {

            var ClientID = $("#ddlDirectorChosen_CessationOfficerDetails").find("option:Selected").attr("personid");
            var ClientSourceID = $("#ddlDirectorChosen_CessationOfficerDetails").find("option:Selected").attr("sourcecode");
            var NatureAppoint = $("#ddlPosition").find("option:Selected").attr('id');
            var WOID = $("#hdnWOID").val();

            var DependingDirectorID = $("#ddlMainDirector").find("option:Selected").attr('id');
            var DependingDirectorSource = $("#ddlMainDirector").find("option:Selected").attr('source');

            var DateofResignation = $("#txtDateofResignation").val();
            var Email = $("#txtDirectorEmail_CessationOfficerDetails").val();
            var ContactNo = $("#txtDirectorContactNo_CessationOfficerDetails").val();
            var Fax = $("#txtDirectorFax_CessationOfficerDetails").val();


            var count = 0;

            count += ControlEmptyNess(true, $("#ddlDirectorChosen_CessationOfficerDetails"), '');
            count += ControlEmptyNess(true, $("#ddlDirectorChosen_CessationOfficerDetails"), '');

            count += ControlEmptyNess(true, $("#ddlPosition"), '');

            //if ($("#ddlPosition option:selected").attr('id') == 1 || $("#ddlPosition option:selected").attr('id') == 9) {
            //    count += ControlEmptyNess(true, $("#ddlMainDirector"), '');
            //}

            //count += ControlEmptyNess(true, $("#txtDateofResignation"), '');




            //count += ValidateRequired(ClientID);
            //count += ValidateRequired(ClientSourceID);
            //count += ValidateRequired(NatureAppoint);
            //count += ValidateRequired(Director);
            //count += ValidateRequired(DateofResignation);            

            if (count > 0) {
                ShowNotify('Please Enter values for all mandatory fields.', 'error', 3000);
                return false;
            }
            else {

                var checkEmailFormat = validEmail($("#txtDirectorEmail_CessationOfficerDetails"), false);
                if (checkEmailFormat == 1) {
                    ShowNotify('Please enter valid Email.', 'error', 3000);
                    return false;
                }


                var cessationdetails = {};
                cessationdetails.ClientID = ClientID;
                cessationdetails.ClientSourceID = ClientSourceID;
                cessationdetails.NatureAppoint = NatureAppoint;
                cessationdetails.WOID = WOID;

                cessationdetails.DependingDirectorID = DependingDirectorID;
                cessationdetails.DependingDirectorSource = DependingDirectorSource;

                cessationdetails.DateofResignation = DateofResignation;
                cessationdetails.Email = Email;
                cessationdetails.ContactNo = ContactNo;
                cessationdetails.Fax = Fax;



                var jsonText = JSON.stringify({ cessationdetails: cessationdetails });

                $Status = '';
                CallServicesData("InsertCessationDirectors", '', '', jsonText, '', InsertCessationDirectorsCallback);
            }
        }

    });

    function InsertCessationDirectorsCallback() {
        if ($Status == 2) {
            ShowNotify('Cessation Officer and Position already Added.', 'error', 3000);
            return false;
        }
        else if ($Status == 1) {
            BindWOCessationOfficerDetails();
            ShowNotify('Success.', 'success', 2000);
            ClearElements();
            return false;
        }
        if ($Status == 0) {
            ShowNotify('Session expired please login again.', 'error', 3000);
            return false;
        }
    }

    function ClearElements() {

        $('#txtDateofResignation').val("");
        $('#txtDirectorEmail_CessationOfficerDetails').val("");
        $('#txtDirectorContactNo_CessationOfficerDetails').val("");
        $('#txtDirectorFax_CessationOfficerDetails').val("");
        $('#divClientOne .chosen-select').val('-1').trigger('chosen:updated');
        $("#ddlMainDirector > option:not(:eq(0))").remove();
        $("#divMainDirector").hide();
        $('#ddlPosition').val("");
    }


    $("#ddlDirectorChosen_CessationOfficerDetails").change(function () {
        var ClientID = $("#ddlDirectorChosen_CessationOfficerDetails").find("option:Selected").attr("personid");
        var ClientSourceID = $("#ddlDirectorChosen_CessationOfficerDetails").find("option:Selected").attr("sourcecode");
        if (ClientID != undefined && ClientSourceID != undefined) {
            GetCessationAddressDetails(ClientID, ClientSourceID);
            $("#divMainDirector").hide();
            $("#ddlPosition").val("");
            $("#ddlMainDirector > option:not(:eq(0))").remove();
        }
        else {
            $('#txtDirectorEmail_CessationOfficerDetails').val("");
            $('#txtDirectorContactNo_CessationOfficerDetails').val("");
            $('#txtDirectorFax_CessationOfficerDetails').val("");
        }
    });

    $('#txtDateofResignation').datepicker({
        showOtherMonths: true,
        selectOtherMonths: true,
        showWeek: true,
        changeYear: true,
        changeMonth: true,
        dateFormat: 'dd/mm/yy',
        yearRange: "-10:+50"
    });

    function BindddlPossion() {
        CallServicesData("/PartialContent/GetDDLValues", 'ddlTemplate', 'ddlPosition', "{'from':" + 2 + ",'to':" + 2 + "}", '');
    }

    function CallServicesData(path, templateId, containerId, parameters, clearContent, callBack) {
        $.ajax({
            type: "POST",
            url: path,
            data: parameters,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            cache: true,
            success: function (msg) {
                $Status = msg;
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

            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw new Error(xhr.statusText);
            }
        });
    }

    function CessationOfficersCallBack() {
        var tblLength = $("#tblCessationDetails").find("tr").length;

        $("#tblCessationDetails").find(".AddTransfer").unbind('click');
        $("#tblCessationDetails").find(".AddTransfer").click(ShowPositionDetails);

        if (tblLength > 0) {
            $("#tblCessationDetails").find(".aDelete").unbind("click");
            $("#tblCessationDetails").find(".aDelete").click(DeleteWOCessationDetailsByID);
            $("#divNoData").hide();

        }
        else {
            $("#divNoData").show();
        }

        if ($('#hdnWOCloseStatus').val() == 'hide') {
            $('.btnWOClose').hide();
        }

    }

    function ShowPositionDetails() {
        BindPossionDetails($(this).attr("ID"));
        try {
            $('#divAddTransferDetails').modal({
                "backdrop": "static",
                "show": "true"
            });
        } catch (e) {
            console.log(e);
        }

    }


    function BindPossionDetails(CessID) {
        if (CessID == "") {
            $("#divNoData1").show();
        } else {
            $("#divNoData1").hide();
            try {
                CallServicesData("/WODI/GetWOCessionofficersPossionDetailsByCessId", 'scriptAppointmentPositionDetails', 'tblAppointmentPositionOfficers', "{'CessID':" + parseInt(CessID) + "}", true, '');
            } catch (e) {
                console.log(e);
            }
        }
    }


    function DeleteWOCessationDetailsByID() {
        try {
            var CessationID = $(this).attr("id");
            $("#dialog-confirm").removeClass('hide').dialog({
                resizable: false,
                modal: true,
                title_html: true,
                buttons: [
                    {
                        html: "<i class='ace-icon fa fa-trash-o bigger-110'></i>&nbsp; Delete item",
                        "class": "btn btn-danger btn-xs",
                        click: function () {
                            CallServicesData("/WODI/DeleteWOCessationOfficerDetailsByWOID", '', '', "{'ID':" + CessationID + "}", true, CessationDeleteCallBack)
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

    function CessationDeleteCallBack() {
        if ($Status == 1) {
            ShowNotify('Success.', 'success', 2000);
            BindWOCessationOfficerDetails();
            return false;
        }
    }

    function BindWOCessationOfficerDetails() {
        try {
            CallServicesData("/WODI/GetWOCessationOfficerDetailsByWOID", 'scriptCessionDetails', 'tblCessationDetails', "{'WOID':" + parseInt($("#hdnWOID").val()) + "}", true, CessationOfficersCallBack);
        } catch (e) {
            console.log(e);
        }
    }
});