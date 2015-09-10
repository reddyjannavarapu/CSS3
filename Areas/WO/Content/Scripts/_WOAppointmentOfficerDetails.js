$(document).ready(function () {
    $Status = '';

    BindddlPossion();
    BindWOAppointmentOfficerDetails();

    if ($('#hdnWOCloseStatus').val() == 'hide') {
        $('.btnWOClose').hide();
    }

    $("#txtDirectorContactNo_ApptOfcrDetails").keypress(function (event) {
        try {
            var checkCharater = AllowNumbersOnly($(this).val(), event);
            if (!checkCharater) {
                event.preventDefault();
            }

        } catch (e) {
            console.log(e);
        }
    });

    $("#txtDirectorFax_ApptOfcrDetails").keypress(function (event) {
        try {
            var checkCharater = AllowNumbersOnly($(this).val(), event);
            if (!checkCharater) {
                event.preventDefault();
            }

        } catch (e) {
            console.log(e);
        }
    });

    $('#btnIndividual_ApptOfOfficers').unbind('click').click(function () {
        $('.divIndividualOrCompany').empty();
        $("#divIndividualOrCompany_ApptOfOfficers").load('/WO/partialcontent/_CreateIndividual', function () {
            $("#divIndividualOrCompany_ApptOfOfficers").find('.modalIndividual').modal({
                "backdrop": "static",
                "show": "true"
            });
        });

    });

    function BindddlPossion() {
        CallServicesData("/PartialContent/GetDDLValues", 'ddlTemplate', 'ddlPosition', "{'from':" + 2 + ",'to':" + 2 + "}", '');
    }

    function BindWOAppointmentOfficerDetails() {
        try {
            CallServicesData("/WODI/GetWOAppointmentOfficerDetailsByWOID", 'scriptAppointmentDetails', 'tblAppointmentOfficers', "{'WOID':" + parseInt($("#hdnWOID").val()) + "}", true, AppointmentOfficerCallBack);
        } catch (e) {
            console.log(e);
        }
    }

    function AppointmentOfficerCallBack() {
        var tblLength = $("#tblAppointmentOfficers").find("tr").length;

        $("#tblAppointmentOfficers").find(".AddTransfer").unbind('click');
        $("#tblAppointmentOfficers").find(".AddTransfer").click(ShowPositionDetails);

        if (tblLength > 0) {
            $("#tblAppointmentOfficers").find(".aDelete").unbind("click");
            $("#tblAppointmentOfficers").find(".aDelete").click(DeleteWOApptOfficersDetailsByID);
            $("#divNoData").hide();

        }
        else {
            $("#divNoData").show();
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

    function BindPossionDetails(ApptID) {
        if (ApptID == "") {
            $("#divNoData1").show();
        } else {
            $("#divNoData1").hide();
            try {
                CallServicesData("/WODI/GetWOAppointmentPossionDetailsByApptId", 'scriptAppointmentPositionDetails', 'tblAppointmentPositionOfficers', "{'ApptId':" + parseInt(ApptID) + "}", true, '');
            } catch (e) {
                console.log(e);
            }
        }
    }

    function DeleteWOApptOfficersDetailsByID() {
        try {
            var ApptID = $(this).attr("id");
            $("#dialog-confirm").removeClass('hide').dialog({
                resizable: false,
                modal: true,
                title_html: true,
                buttons: [
                    {
                        html: "<i class='ace-icon fa fa-trash-o bigger-110'></i>&nbsp; Delete item",
                        "class": "btn btn-danger btn-xs",
                        click: function () {
                            CallServicesData("/WODI/DeleteWOApptOfficerDetailsByWOID", '', '', "{'ID':" + ApptID + "}", true, ApptofcrDeleteCallBack)
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

    function ApptofcrDeleteCallBack() {
        if ($Status == 1) {
            ShowNotify('Success.', 'success', 2000);
            BindWOAppointmentOfficerDetails();
            return false;
        }
    }


    $("#ddlPosition").change(function () {
        
        $('#ddlDirectorChosen_ddlMainDirector').val('-1').trigger('chosen:updated');
        if ($("#ddlPosition option:selected").attr('id') == 1) { //|| $("#ddlPosition option:selected").attr('id') == 9) {

            

            var ClientID = $("#ddlDirectorChosen_AppointmentOfficerDetails").find("option:Selected").attr("personid");
            var ClientSourceID = $("#ddlDirectorChosen_AppointmentOfficerDetails").find("option:Selected").attr("sourcecode");
            var NatureAppoint = $("#ddlPosition").find("option:Selected").attr('id');
            var WOID = $("#hdnWOID").val();

            if (ClientID == undefined && ClientSourceID == undefined) {
                ShowNotify('Please select appointment Officer.', 'error', 3000);
                //ShowNotify('Please Enter values for all mandatory fields.', 'error', 3000);
                return false;
            }
            else if (ClientID != '' && ClientSourceID != '' && WOID != '') {
                $("#divMainDirector").show();
                $('#divMainchoosenDirector .chosen-select').val('-1').trigger('chosen:updated');

                //$("#ddlMainDirector > option:not(:eq(0))").remove();
                // CallServicesData("/WODI/GetCessationDirectors", 'ddlDirctorTemplate', 'ddlMainDirector', "{'WOID':" + WOID + ",'DirectorID':" + ClientID + ",'DirectorSource':'" + ClientSourceID + "','NatureAppoint':" + NatureAppoint + "}", '');
            }
        }
        else {
            $("#divMainDirector").hide();
            //  $('#divClientOne .chosen-select').val('-1').trigger('chosen:updated');
        }
    });


    $("#ddlDirectorChosen_AppointmentOfficerDetails").change(function () {
        var ClientID = $("#ddlDirectorChosen_AppointmentOfficerDetails").find("option:Selected").attr("personid");
        var ClientSourceID = $("#ddlDirectorChosen_AppointmentOfficerDetails").find("option:Selected").attr("sourcecode");
        if (ClientID != undefined && ClientSourceID != undefined) {
            GetApptOfcrAddressDetails(ClientID, ClientSourceID);

            $("#divMainDirector").hide();
            $("#ddlPosition").val("");
            $("#ddlMainDirector > option:not(:eq(0))").remove();
        }
        else {
            $('#txtDirectorEmail_ApptOfcrDetails').val("");
            $('#txtDirectorContactNo_ApptOfcrDetails').val("");
            $('#txtDirectorFax_ApptOfcrDetails').val("");
        }

    });

    function GetApptOfcrAddressDetails(personid, sourcecode) {
        CallServicesData("/PartialContent/GetDirectorAddressDetails", "", "", "{'PersonId':" + personid + ",'SourceCode':'" + sourcecode + "'}", "", ApptOfcrAddressDetailsCallBack);
    }

    function ApptOfcrAddressDetailsCallBack() {
        if ($Status.length > 0) {
            $('#txtDirectorEmail_ApptOfcrDetails').val($Status[0].Email);
            $('#txtDirectorContactNo_ApptOfcrDetails').val($Status[0].Phone);
            $('#txtDirectorFax_ApptOfcrDetails').val($Status[0].Fax);
        }
        else {
            $('#txtDirectorEmail_ApptOfcrDetails').val("");
            $('#txtDirectorContactNo_ApptOfcrDetails').val("");
            $('#txtDirectorFax_ApptOfcrDetails').val("");
        }
    }


    $("#btnSaveApptOfficers").click(function () {
        var WOID = $("#hdnWOID").val();

        if (WOID != "" && WOID != undefined) {            
            var ClientID = $("#ddlDirectorChosen_AppointmentOfficerDetails").find("option:Selected").attr("personid");
            var ClientSourceID = $("#ddlDirectorChosen_AppointmentOfficerDetails").find("option:Selected").attr("sourcecode");
            var NatureAppoint = $("#ddlPosition").find("option:Selected").attr('id');
            var WOID = $("#hdnWOID").val();

            var DependingDirectorID = '';
            DependingDirectorID = $("#ddlDirectorChosen_ddlMainDirector").find("option:Selected").attr("personid");
            var DependingDirectorSource = $("#ddlDirectorChosen_ddlMainDirector").find("option:Selected").attr("sourcecode");


            var Email = $("#txtDirectorEmail_ApptOfcrDetails").val();
            var ContactNo = $("#txtDirectorContactNo_ApptOfcrDetails").val();
            var Fax = $("#txtDirectorFax_ApptOfcrDetails").val();


            var count = 0;

            count += ControlEmptyNess(true, $("#ddlDirectorChosen_AppointmentOfficerDetails"), '');
            count += ControlEmptyNess(true, $("#ddlPosition"), '');

            if ($("#ddlPosition option:selected").attr('id') == 1) { //|| $("#ddlPosition option:selected").attr('id') == 9) {
                //  count += ControlEmptyNess(true, $("#ddlDirectorChosen_ddlMainDirector"), '');
                if (DependingDirectorID == undefined || DependingDirectorID == '')
                    count += 1;

            }


            if (NatureAppoint == 1 || NatureAppoint == 9) {

                $(".divMainDirector").find("#ddlDirectorChosen_ddlMainDirector").find("option").each(function () {
                    if ($(this).attr("personid") == ClientID && $(this).attr("sourcecode") == ClientSourceID) {
                        count = 10;
                    }
                });
                if (count == 10) {
                    ShowNotify('He is already director to this company.', 'error', 3000);
                    return false;
                }
            }


            if (count > 0) {
                ShowNotify('Please Enter values for all mandatory fields.', 'error', 3000);
                return false;
            }
            else {

                var checkEmailFormat = validEmail($("#txtDirectorEmail_ApptOfcrDetails"), false);
                if (checkEmailFormat == 1) {
                    ShowNotify('Please enter valid Email.', 'error', 3000);
                    return false;
                }

                var ApptOfcrdetails = {};
                ApptOfcrdetails.ClientID = ClientID;
                ApptOfcrdetails.ClientSourceID = ClientSourceID;
                ApptOfcrdetails.NatureAppoint = NatureAppoint;
                ApptOfcrdetails.WOID = WOID;

                ApptOfcrdetails.DependingDirectorID = DependingDirectorID;
                ApptOfcrdetails.DependingDirectorSource = DependingDirectorSource;


                ApptOfcrdetails.Email = Email;
                ApptOfcrdetails.ContactNo = ContactNo;
                ApptOfcrdetails.Fax = Fax;



                var jsonText = JSON.stringify({ ApptOfcrdetails: ApptOfcrdetails });

                $Status = '';
                CallServicesData("InsertApptOfcrDetails", '', '', jsonText, '', CallbackInsertApptOfcrDetails);
            }
        }

    });

    function CallbackInsertApptOfcrDetails() {
        if ($Status == 2) {
            ShowNotify('Record exists', 'error', 3000);
            return false;
        }
        else if ($Status == 3) {
            ShowNotify('He is already alternative director to this company.', 'error', 6000);
            return false;
        }

        else if ($Status == 1) {
            BindWOAppointmentOfficerDetails();
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
        $('#txtDirectorEmail_ApptOfcrDetails').val("");
        $('#txtDirectorContactNo_ApptOfcrDetails').val("");
        $('#txtDirectorFax_ApptOfcrDetails').val("");
        $('#divClientOne .chosen-select').val('-1').trigger('chosen:updated');
        $('#divMainchoosenDirector .chosen-select').val('-1').trigger('chosen:updated');
        $('#ddlPosition').val("");
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



});