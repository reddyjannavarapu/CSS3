function DeletePrinipalDetails(val1, OutputDiv) {

    val1 = val1.replace(/,$/g, '');
    var jsonText = JSON.stringify('{' + val1 + '}');

    $("#dialog-multiple-confirm").removeClass('hide').dialog({
        resizable: false,
        modal: true,
        title_html: true,
        buttons: [
            {
                html: "<i class='ace-icon fa fa-trash-o bigger-110'></i>&nbsp; Delete Item",
                "class": "btn btn-danger btn-xs",
                click: function () {
                    //******Start*********

                    var path = '';
                    if (OutputDiv == 'Output_PrincipalDetails')
                        path = "/WODI/DeleteWOINCORPPrincipalDetails";
                    else if (OutputDiv == 'Output_AuthorizedPersonPrincipalDetails')
                        path = "/WODI/DeleteWOINCORPAuthorizedPersonPrincipalDetails";


                    $.ajax({
                        type: "POST",
                        url: path,
                        data: '{' + val1 + '}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: false,
                        cache: true,
                        success: function (msg) {
                            $GlobalWOInfoDetails.ReturnValue = msg;
                            if (OutputDiv == 'Output_PrincipalDetails') {
                                PrincipalDetails();
                                BindddlPrincipalDetails();
                                AuthorizedPersonPrincipalDetails();
                                BindddlPrincipalDetailsForNominee();
                            }
                            else if (OutputDiv == 'Output_AuthorizedPersonPrincipalDetails')
                                AuthorizedPersonPrincipalDetails();



                        },
                        error: function (xhr, ajaxOptions, thrownError) {
                            //throw new Error(xhr.statusText);
                        }
                    });

                    //*******End*************

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

}

function PrincipalDetails() {
    var WOID = $("#hdnWOID").val();
    $.ajax({
        type: "POST",
        url: "/WODI/GetWOINCORPPrincipalDetails",
        data: "{'WOID':'" + WOID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        cache: true,
        success: function (msg) {

            if (msg == '0') {
                ShowNotify('Invalid Session login again.', 'error', 3000);
                return false;
            }
            else {
                BindIncropFSDetails(msg, "Output_PrincipalDetails");
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            //throw new Error(xhr.statusText);
        }
    });
}

function AuthorizedPersonPrincipalDetails() {

    var WOID = $("#hdnWOID").val();
    $.ajax({
        type: "POST",
        url: "/WODI/GetWOINCORPAuthorizedPersonPrincipalDetails",
        data: "{'WOID':'" + WOID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        cache: true,
        success: function (msg) {

            if (msg == 0) {
                ShowNotify('Invalid Session login again.', 'error', 3000);
                return false;
            }
            else {
                BindIncropFSDetails(msg, 'Output_AuthorizedPersonPrincipalDetails');
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            //throw new Error(xhr.statusText);
        }
    });
}

function BindddlPrincipalDetails() {

    $('#ddlPrincipalDetails').find('option:gt(0)').remove();
    var WOID = $("#hdnWOID").val();
    CallMasterData("/WODI/GetddlPrincipalDetails", 'ddlAuthorizedPersonPrincipalDetailsTemplate', 'ddlPrincipalDetails', "{'WOID':'" + WOID + "'}", false, '');
}

function BindIncropFSDetails(msg, OutputDiv) {
    $('#' + OutputDiv).html('');

    var p = '';
    var result = jQuery.parseJSON(msg);

    var count = 0;
    for (var a in result) {
        count++;

        if (a == 'Table')
            p = result.Table;


        if (p.length > 0) {

            var cols = new Array();

            for (var key in p[0]) {
                cols.push(key);
            }

            var table = $('<table class="table table-striped table-bordered table-hover" style="background-color: white !important;"></table>');
            var thbody = $('<thead></thead>');

            var th = $('<tr></tr>');
            for (var i = 0; i <= cols.length; i++) {
                if (i == cols.length) {
                    th.append('<th>Delete</th>');
                }
                else {
                    if (cols[i] == 'WOID' || cols[i] == 'InfoCode' || cols[i] == 'PersonID' || cols[i] == 'PersonSource' || cols[i] == 'FSID') {
                        th.append('<th style="display:none;>' + cols[i] + '</th>');
                    }
                    else
                        th.append('<th>' + cols[i] + '</th>');
                }
            }
            table.append(thbody.append(th));

            for (var j = 0; j < p.length; j++) {
                var player = p[j];
                var tr = $('<tr></tr>');
                var DeleteParams = '';
                for (var k = 0; k <= cols.length; k++) {
                    var Ids = '';

                    if (k == cols.length) {
                        var d = '<td style="text-align:center"><a class="red btnWOClose" href="javascript:void(0)"';
                        d += ' onclick=DeletePrinipalDetails("' + DeleteParams + '","' + OutputDiv + '"); >';
                        d += '<i class=" ace-icon fa fa-trash-o bigger-130"></i></a></td>';
                        tr.append(d);
                    }
                    else {
                        var columnName = cols[k];

                        if (columnName.toUpperCase() == 'WOID' || columnName.toUpperCase() == 'PERSONID' || columnName.toUpperCase() == 'PERSONSOURCE' || columnName.toUpperCase() == 'FSID') {
                            tr.append('<td style="display:none;">' + player[columnName] + '</td>');
                            DeleteParams = DeleteParams + '\'' + columnName + '\'' + ':' + '\'' + player[columnName] + '\'' + ',';

                        }
                        else
                            tr.append('<td>' + player[columnName] + '</td>');

                    }
                }
                table.append(tr);
            }
            $('#' + OutputDiv).html('');
            $('#' + OutputDiv).append(table);

            if ($('#hdnWOCloseStatus').val() == 'hide') {
                $('.btnWOClose').hide();
            }

        }
    }

}

function BindddlPrincipalDetailsForNominee() {

    $('#ddlPrincipalDetailsForNominee').find('option:gt(0)').remove();
    var WOID = $("#hdnWOID").val();
    CallMasterData("/WODI/GetddlPrincipalDetails", 'ddlAuthorizedPersonPrincipalDetailsTemplate', 'ddlPrincipalDetailsForNominee', "{'WOID':'" + WOID + "'}", false, '');
}

function BindNomineeDirectorsInPD() {

    $('#ddlNomineeDirectorDetailsInPD').find('option:gt(0)').remove();
    var WOID = parseInt($("#hdnWOID").val());
    var GridCodeOfNomineeDirector = "NDD";
    CallMasterData("/WODI/GetNomineeDirectorsInPrincipalDetails", 'ddlNomineeDirectorDetailsInPDTemplate', 'ddlNomineeDirectorDetailsInPD', "{'WOID':" + WOID + ",'InfoCode':'" + GridCodeOfNomineeDirector + "'}", false, '');

}

$(document).ready(function () {
    //Existing
    $GlobalClientExistingData = {};
    $GlobalClientExistingData.EGMList = '';
    $GlobalClientExistingData.WOID = '';

    $GlobalClientExistingData.Currency = '';
    $GlobalClientExistingData.ClassOfShare = '';
    $GlobalClientExistingData.NewAllottedShares = '';
    $GlobalClientExistingData.EachShare = '';
    $GlobalClientExistingData.AmountPaidToEachShare = '';
    $GlobalClientExistingData.TotalConsideration = '';
    $GlobalClientExistingData.NoOfIssuedShares = '';
    $GlobalClientExistingData.IssuedCapital = '';
    //$GlobalClientExistingData.IsFMRegisteredAddress = '';

    $GlobalClientExistingData.MeetingNoticeSource = '';
    $GlobalClientExistingData.MeetingNotice = '';
    $GlobalClientExistingData.MeetingMinutesSource = '';
    $GlobalClientExistingData.MeetingMinutes = '';
    $GlobalClientExistingData.OthersMeetingMinutes = '';
    $GlobalClientExistingData.ResultantPaidupCapital = '';
    $GlobalClientExistingData.Designation = '';
    $GlobalClientExistingData.NoticeResolutionSource = '';
    $GlobalClientExistingData.NoticeResolution = '';
    $GlobalClientExistingData.F24F25Source = '';
    $GlobalClientExistingData.F24F25ID = '';
    $GlobalClientExistingData.ShareHoldingStructure = '';

    $FSPrincipalDetails = {};
    $FSPrincipalDetails.data = '';

    $FSAuthorizedPersonPrincipalDetails = {};
    $FSAuthorizedPersonPrincipalDetails.data = '';

    $GlobalWOAppointmentOrCessationDetailsData = {};
    $GlobalWOAppointmentOrCessationDetailsData.InsertedID = 0;
    $GlobalWOAppointmentOrCessationDetailsData.WOTransferDetailsData = '';

    if ($('#hdnWOCloseStatus').val() == 'hide') {
        $('.btnWOClose').hide();
    }

    BindDropdowns();

    $("#chkROPlaceOfMeeting").change(function () {
        try {
            if ($(this).is(":checked")) {

                ClientEngagingCallServices("GetCompanyAddressesByWOID", '', '', "{'WOID':" + parseInt($("#hdnWOID").val()) + ",'IsFMGAddress':" + false + "}", false, CompanyDetailscallBack);
            }
            else ClearROPlaceOfMeetingFields();
        }
        catch (ex) {
            console.log(ex);
        }
    });




    //---------------------Principal Details--------------

    PrincipalDetails();

    $("#btnPrincipalDetails").click(function () {

        //BindNomineeDirectorsInPD();

        $('#ddlDirectorChosen_PrincipalDetails').val('-1').trigger("chosen:updated");
        $('#txtDirectorEmail_PrincipalDetails').val('');
        $('#txtDirectorContactNo_PrincipalDetails').val('');
        $('#txtDirectorFax_PrincipalDetails').val('');
        $('#txtContactPerson_PrincipalDetails').val('');

        $('#divPupup_PrincipalDetails').modal({
            "backdrop": "static",
            "show": "true"
        });
    });

    $("#btnSave_PrincipalDetails").click(function () {

        var Mandatory = 0;

        //if ($('#ddlNomineeDirectorDetailsInPD').val() == '-1')
        //    Mandatory += 1;

        if ($("#ddlDirectorChosen_PrincipalDetails").val() == '')
            Mandatory += 1;

        if (Mandatory > 0) {
            ShowNotify('Please Enter Data For All Mandatory Feild.', 'error', 3000);
            return false;
        }

        var checkEmailFormat = validEmail($('#txtDirectorEmail_PrincipalDetails'), false);
        if (checkEmailFormat == 1) {
            ShowNotify('Please enter valid Email.', 'error', 3000);
            return false;
        }
        var Directory = $("#ddlDirectorChosen_PrincipalDetails").val();
        if (Directory == -1 || Directory == undefined || Directory == '') {
            ShowNotify('Please select Director.', 'error', 3000);
            return false;
        }

        //$("#dialog-Submit_confirm").removeClass('hide').dialog({
        //    resizable: false,
        //    modal: true,
        //    title_html: true,
        //    buttons: [
        //        {
        //            html: "<i class='ace-icon fa fa-check bigger-110'></i>&nbsp; Save",
        //            "class": "btn btn-danger btn-xs",
        //            click: function () {



        //******Start*********
        var personid = $('#ddlDirectorChosen_PrincipalDetails').find('option:selected').attr('personid');
        var sourcecode = $('#ddlDirectorChosen_PrincipalDetails').find('option:selected').attr('sourcecode');
        var NDpersonid = '-1';//$('#ddlNomineeDirectorDetailsInPD').find('option:selected').attr('PersonID');
        var NDsourcecode = '';//$('#ddlNomineeDirectorDetailsInPD').find('option:selected').attr('PersonSource');

        var DirectorEmail = $('#txtDirectorEmail_PrincipalDetails').val();
        var DirectorContactNo = $('#txtDirectorContactNo_PrincipalDetails').val();
        var DirectorFax = $('#txtDirectorFax_PrincipalDetails').val();
        var ContactPerson = $('#txtContactPerson_PrincipalDetails').val();

        CallForDirectorAddressDetailsForFS("/PartialContent/InsertOrUpdateDirectorAddress", "", "", "{'PersonId':" + personid + ",'SourceCode':'" + sourcecode + "','DirectorEmail':'" + DirectorEmail + "','DirectorContactNo':'" + DirectorContactNo + "','DirectorFax':'" + DirectorFax + "'}", '', 'AuthorizedPersonFSDetails');


        var WOID = $("#hdnWOID").val();
        CallForDirectorAddressDetailsForFS("/WODI/InsertWOINCORPPrincipalDetails", "", "", "{'WOID':'" + WOID + "', 'personid':'" + personid + "','sourcecode':'" + sourcecode + "','NDPersonId':'" + NDpersonid + "','NDSourceCode':'" + NDsourcecode + "','ContactPerson':'" + ContactPerson + "'}", CallbackPrincipalDetails, 'PrincipalDetails');


        //*******End*************

        //                $(this).dialog("close");
        //            }
        //        },
        //        {
        //            html: "<i class='ace-icon fa fa-times bigger-110'></i>&nbsp; cancel",
        //            "class": "btn btn-xs",
        //            click: function () {
        //                $(this).dialog("close");
        //            }
        //        }
        //    ]
        //});

    });

    $('#btnIndividual_PrincipalDetails').unbind('click').click(function () {
        $('.divIndividualOrCompany').empty();
        $("#divIndividualOrCompany_PrincipalDetails").load('/WO/partialcontent/_CreateIndividual', function () {
            $("#divIndividualOrCompany_PrincipalDetails").find('.modalIndividual').modal({
                "backdrop": "static",
                "show": "true"
            });
        });

    });

    $('#btnCompany_PrincipalDetails').unbind('click').click(function () {
        $('.divIndividualOrCompany').empty();
        $("#divIndividualOrCompany_PrincipalDetails").load('/WO/partialcontent/_CreateCompany', function () {
            $("#divIndividualOrCompany_PrincipalDetails").find('.modalCompany').modal({
                "backdrop": "static",
                "show": "true"
            });
        });

    });

    function CallForDirectorAddressDetailsForFS(path, templateId, containerId, parameters, callBack, CallingFrom) {
        $.ajax({
            type: "POST",
            url: path,
            data: parameters,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            cache: true,
            success: function (msg) {

                if (CallingFrom == 'PrincipalDetails')
                    $FSPrincipalDetails.data = msg;
                else if (CallingFrom == 'AuthorizedPersonPrincipalDetails')
                    $FSAuthorizedPersonPrincipalDetails.data = msg;

                if (callBack != undefined && callBack != '')
                    callBack();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                //throw new Error(xhr.statusText);
            }
        });
    }

    function CallbackPrincipalDetails() {
        if ($FSPrincipalDetails.data == '1') {
            $('#divPupup_PrincipalDetails').modal("hide");
            PrincipalDetails();
            BindddlPrincipalDetails();
            BindddlPrincipalDetailsForNominee();
            ShowNotify('Success.', 'success', 2000);

        }
        else if ($FSPrincipalDetails.data == '0')
            ShowNotify('Session expired.', 'error', 3000);
    }


    //---------------------end PrincipalDetails------------------------

    $('#ddlDirectorChosen_AuthorizedPersonPrincipalDetails').change(function () {

        var personid = $(this).find('option:selected').attr('personid');
        var sourcecode = $(this).find('option:selected').attr('sourcecode');
        if (personid != '' && personid != undefined) {
            GetAddressDetailsForPrincipalDetails(personid, sourcecode, 'AuthorizedPersonPrincipalDetails');
        }
    });

    $('#ddlDirectorChosen_PrincipalDetails').change(function () {

        var personid = $(this).find('option:selected').attr('personid');
        var sourcecode = $(this).find('option:selected').attr('sourcecode');
        if (personid != '' && personid != undefined) {
            GetAddressDetailsForPrincipalDetails(personid, sourcecode, 'PrincipalDetails');
        }
    });

    function GetAddressDetailsForPrincipalDetails(personid, sourcecode, CallingFrom) {

        CallForDirectorAddressDetailsForFS("/PartialContent/GetDirectorAddressDetails", "", "", "{'PersonId':" + personid + ",'SourceCode':'" + sourcecode + "'}", CallBackDirectorAddressDetails, CallingFrom);
    }

    function CallBackDirectorAddressDetails() {

        if ($FSPrincipalDetails.data.length > 0) {
            $('#txtDirectorEmail_PrincipalDetails').val($FSPrincipalDetails.data[0].Email);
            $('#txtDirectorContactNo_PrincipalDetails').val($FSPrincipalDetails.data[0].Phone);
            $('#txtDirectorFax_PrincipalDetails').val($FSPrincipalDetails.data[0].Fax);

        }
        else {
            $('#txtDirectorEmail_PrincipalDetails').val("");
            $('#txtDirectorContactNo_PrincipalDetails').val("");
            $('#txtDirectorFax_PrincipalDetails').val("");
        }


        if ($FSAuthorizedPersonPrincipalDetails.data.length > 0) {
            $('#txtDirectorEmail_AuthorizedPersonPrincipalDetails').val($FSAuthorizedPersonPrincipalDetails.data[0].Email);
            $('#txtDirectorContactNo_AuthorizedPersonPrincipalDetails').val($FSAuthorizedPersonPrincipalDetails.data[0].Phone);
            $('#txtDirectorFax_AuthorizedPersonPrincipalDetails').val($FSAuthorizedPersonPrincipalDetails.data[0].Fax);

        }
        else {
            $('#txtDirectorEmail_AuthorizedPersonPrincipalDetails').val("");
            $('#txtDirectorContactNo_AuthorizedPersonPrincipalDetails').val("");
            $('#txtDirectorFax_AuthorizedPersonPrincipalDetails').val("");
        }



    }

    //---------------------AuthorizedPersonPrincipalDetails--------------

    AuthorizedPersonPrincipalDetails();
    BindddlPrincipalDetails();

    $("#btnAuthorizedPersonPrincipalDetails").click(function () {

        $('#ddlPrincipalDetails').val('');
        $('#ddlDirectorChosen_AuthorizedPersonPrincipalDetails').val('-1').trigger("chosen:updated");
        $('#txtDirectorEmail_AuthorizedPersonPrincipalDetails').val('');
        $('#txtDirectorContactNo_AuthorizedPersonPrincipalDetails').val('');
        $('#txtDirectorFax_AuthorizedPersonPrincipalDetails').val('');

        $('#divPupup_AuthorizedPersonPrincipalDetails').modal({
            "backdrop": "static",
            "show": "true"
        });
    });

    $("#btnSave_AuthorizedPersonPrincipalDetails").click(function () {

        var Mandatory = 0;

        if ($("#ddlDirectorChosen_AuthorizedPersonPrincipalDetails").val() == '')
            Mandatory += 1;
        if ($("#ddlPrincipalDetails").val() == '')
            Mandatory += 1;

        if (Mandatory > 0) {
            ShowNotify('Please Enter Data For All Mandatory Feilds.', 'error', 3000);
            return false;
        }

        var checkEmailFormat = validEmail($('#txtDirectorEmail_AuthorizedPersonPrincipalDetails'), false);
        if (checkEmailFormat == 1) {
            ShowNotify('Please enter valid Email.', 'error', 3000);
            return false;
        }
        var Directory = $("#ddlDirectorChosen_AuthorizedPersonPrincipalDetails").val();
        if (Directory == -1 || Directory == undefined || Directory == '') {
            ShowNotify('Please select Director.', 'error', 3000);
            return false;
        }

        //$("#dialog-Submit_confirm").removeClass('hide').dialog({
        //    resizable: false,
        //    modal: true,
        //    title_html: true,
        //    buttons: [
        //        {
        //            html: "<i class='ace-icon fa fa-check bigger-110'></i>&nbsp; Save",
        //            "class": "btn btn-danger btn-xs",
        //            click: function () {



        //******Start*********
        var personid = $('#ddlDirectorChosen_AuthorizedPersonPrincipalDetails').find('option:selected').attr('personid');
        var sourcecode = $('#ddlDirectorChosen_AuthorizedPersonPrincipalDetails').find('option:selected').attr('sourcecode');

        var DirectorEmail = $('#txtDirectorEmail_AuthorizedPersonPrincipalDetails').val();
        var DirectorContactNo = $('#txtDirectorContactNo_AuthorizedPersonPrincipalDetails').val();
        var DirectorFax = $('#txtDirectorFax_AuthorizedPersonPrincipalDetails').val();

        CallForDirectorAddressDetailsForFS("/PartialContent/InsertOrUpdateDirectorAddress", "", "", "{'PersonId':" + personid + ",'SourceCode':'" + sourcecode + "','DirectorEmail':'" + DirectorEmail + "','DirectorContactNo':'" + DirectorContactNo + "','DirectorFax':'" + DirectorFax + "'}", '', 'AuthorizedPersonPrincipalDetails');



        var WOID = $("#hdnWOID").val();
        var FSID = $("#ddlPrincipalDetails").val();

        CallForDirectorAddressDetailsForFS("/WODI/InsertWOINCORPAuthorizedPersonPrincipalDetails", "", "", "{'WOID':'" + WOID + "', 'personid':'" + personid + "','sourcecode':'" + sourcecode + "','FSID':'" + FSID + "'}", CallbackAuthorizedPersonPrincipalDetails, 'AuthorizedPersonPrincipalDetails');


        //*******End*************

        //                $(this).dialog("close");
        //            }
        //        },
        //        {
        //            html: "<i class='ace-icon fa fa-times bigger-110'></i>&nbsp; cancel",
        //            "class": "btn btn-xs",
        //            click: function () {
        //                $(this).dialog("close");
        //            }
        //        }
        //    ]
        //});

    });

    $('#btnIndividual_AuthorizedPersonPrincipalDetails').unbind('click').click(function () {
        $('.divIndividualOrCompany').empty();
        $("#divIndividualOrCompany_AuthorizedPersonPrincipalDetails").load('/WO/partialcontent/_CreateIndividual', function () {
            $("#divIndividualOrCompany_AuthorizedPersonPrincipalDetails").find('.modalIndividual').modal({
                "backdrop": "static",
                "show": "true"
            });
        });

    });

    $('#btnCompany_AuthorizedPersonPrincipalDetails').unbind('click').click(function () {
        $('.divIndividualOrCompany').empty();
        $("#divIndividualOrCompany_AuthorizedPersonPrincipalDetails").load('/WO/partialcontent/_CreateCompany', function () {
            $("#divIndividualOrCompany_AuthorizedPersonPrincipalDetails").find('.modalCompany').modal({
                "backdrop": "static",
                "show": "true"
            });
        });

    });

    function CallbackAuthorizedPersonPrincipalDetails() {
        if ($FSAuthorizedPersonPrincipalDetails.data == '1') {
            $('#divPupup_AuthorizedPersonPrincipalDetails').modal("hide");
            AuthorizedPersonPrincipalDetails();
            ShowNotify('Success.', 'success', 2000);

        }
        else if ($FSAuthorizedPersonPrincipalDetails.data == '0')
            ShowNotify('Session expired.', 'error', 3000);
    }

    //---------------------AuthorizedPersonPrincipalDetails end------------------------


    $("#btnClear").click(function () {

        CurrencyCallBack();
        CountryCallBack();
        $("#ddlClassOfShare").val('-1');
        $("#txtAllotedShares").val('2');
        $("#txtConsiderationOfEachShare").val('1');
        $("#txtPaidOnEachShare").val('1');
        $("#txtTotalConsideration").val('2');
        $("#txtTotalIssuedShares").val('');
        $("#txtResultantIssuedCapital").val('');

        $('#ddlDirectorChosen_MeetingNotice').val('').trigger('chosen:updated');
        $("#ddlDirectorChosen_MeetingMinutes").val('').trigger('chosen:updated');
        $("#txtOtherMeetingMinutes").val('');
        $("#txtDesignation").val('Chairman');
        $("#ddlDirectorChosen_NoticeofResolution").val('').trigger('chosen:updated');
        $("#ddlDirectorChosen_F24ByF25").val('').trigger('chosen:updated');

        //$("#ddlDirectorChosen_NomineeDirectorServiceAgreement").val('').trigger('chosen:updated');
        //$("#ddlDirectorChosen_NomineeSecretaryServiceAgreement1").val('').trigger('chosen:updated');
        //$("#ddlDirectorChosen_NomineeSecretaryServiceAgreement2").val('').trigger('chosen:updated');


        $("#ddlShareHoldingStructure").val('-1');
        $("#txtResultantPaidUpCapital").val('');
        ClearROPlaceOfMeetingFields();
    });
    $("#btnSaveECE").click(function () {
        try {

            $GlobalClientExistingData.Currency = $("#ddlCurrency").val();
            $GlobalClientExistingData.ClassOfShare = $("#ddlClassOfShare").val();
            $GlobalClientExistingData.NewAllottedShares = $("#txtAllotedShares").val();
            $GlobalClientExistingData.EachShare = $("#txtConsiderationOfEachShare").val();
            $GlobalClientExistingData.AmountPaidToEachShare = $("#txtPaidOnEachShare").val();
            $GlobalClientExistingData.TotalConsideration = $("#txtTotalConsideration").val();
            $GlobalClientExistingData.NoOfIssuedShares = $("#txtTotalIssuedShares").val();
            $GlobalClientExistingData.IssuedCapital = $("#txtResultantIssuedCapital").val();
            $GlobalClientExistingData.ResultantPaidupCapital = $("#txtResultantPaidUpCapital").val();

            $GlobalClientExistingData.MeetingNotice = $("#ddlDirectorChosen_MeetingNotice").find("option:Selected").attr("personid");
            $GlobalClientExistingData.MeetingNoticeSource = $("#ddlDirectorChosen_MeetingNotice").find("option:Selected").attr("sourcecode");
            $GlobalClientExistingData.MeetingMinutes = $("#ddlDirectorChosen_MeetingMinutes").find("option:Selected").attr("personid");
            $GlobalClientExistingData.MeetingMinutesSource = $("#ddlDirectorChosen_MeetingMinutes").find("option:Selected").attr("sourcecode");
            //if ($GlobalClientExistingData.MeetingMinutes == undefined)
            $GlobalClientExistingData.OthersMeetingMinutes = $("#txtOtherMeetingMinutes").val();
            // else $GlobalClientExistingData.OthersMeetingMinutes = '';
            $GlobalClientExistingData.Designation = $("#txtDesignation").val();
            $GlobalClientExistingData.NoticeResolution = $("#ddlDirectorChosen_NoticeofResolution").find("option:Selected").attr("personid");
            $GlobalClientExistingData.NoticeResolutionSource = $("#ddlDirectorChosen_NoticeofResolution").find("option:Selected").attr("sourcecode");
            $GlobalClientExistingData.F24F25ID = $("#ddlDirectorChosen_F24ByF25").find("option:Selected").attr("personid");
            $GlobalClientExistingData.F24F25Source = $("#ddlDirectorChosen_F24ByF25").find("option:Selected").attr("sourcecode");
            $GlobalClientExistingData.ShareHoldingStructure = $("#ddlShareHoldingStructure").val();

            //$GlobalClientExistingData.NomineeDirectorServiceAgreement = $("#ddlDirectorChosen_NomineeDirectorServiceAgreement").find("option:Selected").attr("personid");
            //$GlobalClientExistingData.NomineeDirectorServiceAgreementSource = $("#ddlDirectorChosen_NomineeDirectorServiceAgreement").find("option:Selected").attr("sourcecode");

            //$GlobalClientExistingData.NomineeSecretaryServiceAgreement1 = $("#ddlDirectorChosen_NomineeSecretaryServiceAgreement1").find("option:Selected").attr("personid");
            //$GlobalClientExistingData.NomineeSecretaryServiceAgreement1Source = $("#ddlDirectorChosen_NomineeSecretaryServiceAgreement1").find("option:Selected").attr("sourcecode");

            //$GlobalClientExistingData.NomineeSecretaryServiceAgreement2 = $("#ddlDirectorChosen_NomineeSecretaryServiceAgreement2").find("option:Selected").attr("personid");
            //$GlobalClientExistingData.NomineeSecretaryServiceAgreement2Source = $("#ddlDirectorChosen_NomineeSecretaryServiceAgreement2").find("option:Selected").attr("sourcecode");

            $GlobalClientExistingData.WOID = parseInt($("#hdnWOID").val());

            $GlobalClientExistingData.IsROPlaceOfMeeting = $("#chkROPlaceOfMeeting").is(":checked");
            $GlobalClientExistingData.MAddressLine1 = $("#txtMeetingAddressLine1").val();
            $GlobalClientExistingData.MAddressLine2 = $("#txtMeetingAddressLine2").val();
            $GlobalClientExistingData.MAddressLine3 = $("#txtMeetingAddressLine3").val();
            $GlobalClientExistingData.MAddressCountry = $("#ddlMeetingAddressCountry").val();
            $GlobalClientExistingData.MAddressPostalCode = $("#txtMeetingAddressPostalCode").val();


            var JsonData = JSON.stringify({ ECEData: $GlobalClientExistingData });
            ClientEngagingCallServices("SaveWOECEDetailsByWOID", '', '', JsonData, false, SaveStatusCallBack);
        }
        catch (ex) {
            throw ex;
        }

    });

});

function ClearROPlaceOfMeetingFields() {
    try {
        $("#txtMeetingAddressLine1").val('');
        $("#txtMeetingAddressLine2").val('');
        $("#txtMeetingAddressLine3").val('');
        $("#chkROPlaceOfMeeting").prop("checked", false);
        CountryCallBack();
        $("#txtMeetingAddressPostalCode").val('');
    } catch (e) {
        console.log(e);
    }
}

function CompanyDetailscallBack() {
    try {
        if ($GlobalWOAppointmentOrCessationDetailsData.WOTransferDetailsData.MeetingAddressCountry != null && $GlobalWOAppointmentOrCessationDetailsData.WOTransferDetailsData.MeetingAddressCountry != undefined) {
            $("#txtMeetingAddressLine1").val($GlobalWOAppointmentOrCessationDetailsData.WOTransferDetailsData.MeetingAddressLine1);
            $("#txtMeetingAddressLine2").val($GlobalWOAppointmentOrCessationDetailsData.WOTransferDetailsData.MeetingAddressLine2);
            $("#txtMeetingAddressLine3").val($GlobalWOAppointmentOrCessationDetailsData.WOTransferDetailsData.MeetingAddressLine3);
            if ($GlobalWOAppointmentOrCessationDetailsData.WOTransferDetailsData.MeetingAddressCountry == 0 || $GlobalWOAppointmentOrCessationDetailsData.WOTransferDetailsData.MeetingAddressCountry == -1)
                CountryCallBack();
            else
                $("#ddlMeetingAddressCountry").val($GlobalWOAppointmentOrCessationDetailsData.WOTransferDetailsData.MeetingAddressCountry);
            $("#txtMeetingAddressPostalCode").val($GlobalWOAppointmentOrCessationDetailsData.WOTransferDetailsData.MeetingAddressPostalCode);
        }
    } catch (e) {
        console.log(e);
    }
}

function BindDropdowns() {
    try {
        CallMasterData("/PartialContent/GetCurrencyDetails", 'scriptCurrency', 'ddlCurrency', "{}", false, CurrencyCallBack);
        //CallMasterData("/WODI/GetAllShareHoldingStructures", 'ShareHoldingStructure', 'ddlShareHoldingStructure', "{}", false, '');
        CallMasterData("/PartialContent/GetCountryDetails", 'scriptCountry', 'ddlMeetingAddressCountry', "{}", false, CountryCallBack);
        var WOID = parseInt($("#hdnWOID").val());
        CallMasterData("/PartialContent/GetShareClassDetails", 'scriptShareClass', 'ddlClassOfShare', "{'WOID':" + WOID + "}", false, BindWOECEDetailsByWOID);
    }
    catch (ex) {
        throw ex;
    }
}
function BindWOECEDetailsByWOID() {
    $GlobalClientExistingData.WOID = parseInt($("#hdnWOID").val());
    ClientEngagingCallServices("GetWOECEDetailsByWOID", '', '', "{'WOID':" + $GlobalClientExistingData.WOID + "}", '', BindWOECEDetailsByWOIDCallBack);
}
function BindWOECEDetailsByWOIDCallBack() {

    if ($GlobalClientExistingData.EGMList.WOID != null && $GlobalClientExistingData.EGMList.WOID != undefined) {
        if ($GlobalClientExistingData.EGMList.Currency == -1 || $GlobalClientExistingData.EGMList.Currency == 0)
            CurrencyCallBack();
        else
            $("#ddlCurrency").val($GlobalClientExistingData.EGMList.Currency);

        if ($GlobalClientExistingData.EGMList.ClassOfShare == -1 || $GlobalClientExistingData.EGMList.ClassOfShare == 0)
            $("#ddlClassOfShare").val('-1');
        else
            $("#ddlClassOfShare").val($GlobalClientExistingData.EGMList.ClassOfShare);

        if ($GlobalClientExistingData.EGMList.NewAllottedShares == 0)
            $("#txtAllotedShares").val('2');
        else
            $("#txtAllotedShares").val($GlobalClientExistingData.EGMList.NewAllottedShares);

        if ($GlobalClientExistingData.EGMList.EachShare == 0)
            $("#txtConsiderationOfEachShare").val('1');
        else
            $("#txtConsiderationOfEachShare").val($GlobalClientExistingData.EGMList.EachShare);

        if ($GlobalClientExistingData.EGMList.AmountPaidToEachShare == 0)
            $("#txtPaidOnEachShare").val('1');
        else
            $("#txtPaidOnEachShare").val($GlobalClientExistingData.EGMList.AmountPaidToEachShare);


        if ($GlobalClientExistingData.EGMList.TotalConsideration == 0)
            $("#txtTotalConsideration").val('2');
        else
            $("#txtTotalConsideration").val($GlobalClientExistingData.EGMList.TotalConsideration);

        if ($GlobalClientExistingData.EGMList.NoOfIssuedShares == 0)
            $("#txtTotalIssuedShares").val('');
        else
            $("#txtTotalIssuedShares").val($GlobalClientExistingData.EGMList.NoOfIssuedShares);

        if ($GlobalClientExistingData.EGMList.IssuedCapital == 0)
            $("#txtResultantIssuedCapital").val('');
        else
            $("#txtResultantIssuedCapital").val($GlobalClientExistingData.EGMList.IssuedCapital);

        if ($GlobalClientExistingData.EGMList.ResultantPaidupCapital == 0)
            $("#txtResultantPaidUpCapital").val('');
        else
            $("#txtResultantPaidUpCapital").val($GlobalClientExistingData.EGMList.ResultantPaidupCapital);

        $("#chkROPlaceOfMeeting").prop("checked", $GlobalClientExistingData.EGMList.IsROPlaceOfMeeting);
        $("#txtMeetingAddressLine1").val($GlobalClientExistingData.EGMList.MAddressLine1);
        $("#txtMeetingAddressLine2").val($GlobalClientExistingData.EGMList.MAddressLine2);
        $("#txtMeetingAddressLine3").val($GlobalClientExistingData.EGMList.MAddressLine3);

        if ($GlobalClientExistingData.EGMList.MAddressCountry == 0 || $GlobalClientExistingData.EGMList.MAddressCountry == -1)
            CountryCallBack();
        else
            $("#ddlMeetingAddressCountry").val($GlobalClientExistingData.EGMList.MAddressCountry);
        if ($GlobalClientExistingData.EGMList.MAddressPostalCode == 0)
            $("#txtMeetingAddressPostalCode").val('');
        else
            $("#txtMeetingAddressPostalCode").val($GlobalClientExistingData.EGMList.MAddressPostalCode);


        if ($GlobalClientExistingData.EGMList.MeetingNotice != 0) {
            var MeetingNoticeVal = $("#ddlDirectorChosen_MeetingNotice option[personid=" + $GlobalClientExistingData.EGMList.MeetingNotice + "]option[sourcecode=" + $GlobalClientExistingData.EGMList.MeetingNoticeSource + "]").attr("value");
            $('#ddlDirectorChosen_MeetingNotice').val(MeetingNoticeVal).trigger('chosen:updated');
        }
        else $('#ddlDirectorChosen_MeetingNotice').val('').trigger('chosen:updated');



        var MeetingMinsVal = $("#ddlDirectorChosen_MeetingMinutes option[personid=" + $GlobalClientExistingData.EGMList.MeetingMinutes + "]option[sourcecode=" + $GlobalClientExistingData.EGMList.MeetingMinutesSource + "]").attr("value");
        $('#ddlDirectorChosen_MeetingMinutes').val(MeetingMinsVal).trigger('chosen:updated');
        $("#txtOtherMeetingMinutes").val($GlobalClientExistingData.EGMList.OthersMeetingMinutes);

        if ($GlobalClientExistingData.EGMList.Designation != '' && $GlobalClientExistingData.EGMList.Designation != null)
            $("#txtDesignation").val($GlobalClientExistingData.EGMList.Designation);


        if ($GlobalClientExistingData.EGMList.NoticeResolution != 0) {
            var NoticeofResolution = $("#ddlDirectorChosen_NoticeofResolution option[personid=" + $GlobalClientExistingData.EGMList.NoticeResolution + "]option[sourcecode=" + $GlobalClientExistingData.EGMList.NoticeResolutionSource + "]").attr("value");
            $('#ddlDirectorChosen_NoticeofResolution').val(NoticeofResolution).trigger('chosen:updated');
        }
        else
            $('#ddlDirectorChosen_NoticeofResolution').val('').trigger('chosen:updated');

        if ($GlobalClientExistingData.EGMList.F24F25ID != 0) {
            var NoticeofResolution = $("#ddlDirectorChosen_F24ByF25 option[personid=" + $GlobalClientExistingData.EGMList.F24F25ID + "]option[sourcecode=" + $GlobalClientExistingData.EGMList.F24F25Source + "]").attr("value");
            $('#ddlDirectorChosen_F24ByF25').val(NoticeofResolution).trigger('chosen:updated');
        }
        else
            $('#ddlDirectorChosen_F24ByF25').val('').trigger('chosen:updated');


        //if ($GlobalClientExistingData.EGMList.NomineeDirectorServiceAgreement != 0) {
        //    var NomineeDirectorServiceAgreement = $("#ddlDirectorChosen_NomineeDirectorServiceAgreement option[personid=" + $GlobalClientExistingData.EGMList.NomineeDirectorServiceAgreement + "]option[sourcecode=" + $GlobalClientExistingData.EGMList.NomineeDirectorServiceAgreementSource + "]").attr("value");
        //    $('#ddlDirectorChosen_NomineeDirectorServiceAgreement').val(NomineeDirectorServiceAgreement).trigger('chosen:updated');
        //}
        //else
        //    $('#ddlDirectorChosen_NomineeDirectorServiceAgreement').val('').trigger('chosen:updated');

        //if ($GlobalClientExistingData.EGMList.NomineeSecretaryServiceAgreement1 != 0) {
        //    var NomineeSecretaryServiceAgreement1 = $("#ddlDirectorChosen_NomineeSecretaryServiceAgreement1 option[personid=" + $GlobalClientExistingData.EGMList.NomineeSecretaryServiceAgreement1 + "]option[sourcecode=" + $GlobalClientExistingData.EGMList.NomineeSecretaryServiceAgreement1Source + "]").attr("value");
        //    $('#ddlDirectorChosen_NomineeSecretaryServiceAgreement1').val(NomineeSecretaryServiceAgreement1).trigger('chosen:updated');
        //}
        //else
        //    $('#ddlDirectorChosen_NomineeSecretaryServiceAgreement1').val('').trigger('chosen:updated');

        //if ($GlobalClientExistingData.EGMList.NomineeSecretaryServiceAgreement2 != 0) {
        //    var NomineeSecretaryServiceAgreement2 = $("#ddlDirectorChosen_NomineeSecretaryServiceAgreement2 option[personid=" + $GlobalClientExistingData.EGMList.NomineeSecretaryServiceAgreement2 + "]option[sourcecode=" + $GlobalClientExistingData.EGMList.NomineeSecretaryServiceAgreement2Source + "]").attr("value");
        //    $('#ddlDirectorChosen_NomineeSecretaryServiceAgreement2').val(NomineeSecretaryServiceAgreement2).trigger('chosen:updated');
        //}
        //else
        //    $('#ddlDirectorChosen_NomineeSecretaryServiceAgreement2').val('').trigger('chosen:updated');


        if ($GlobalClientExistingData.EGMList.ShareHoldingStructure == 0 || $GlobalClientExistingData.EGMList.ShareHoldingStructure == -1)
            $("#ddlShareHoldingStructure").val('-1');
        else $("#ddlShareHoldingStructure").val($GlobalClientExistingData.EGMList.ShareHoldingStructure);
    }
}
function SaveStatusCallBack() {
    if ($GlobalClientExistingData.EGMList >= 1) {
        ShowNotify('Success.', 'success', 2000);
        return false;
    }
}

function CurrencyCallBack() {
    $("#ddlCurrency").val("14");
}
function CountryCallBack() {
    $("#ddlMeetingAddressCountry").val("109");
}
function ClearFMHasRegisteredAddressFields() {
    $("#txtAddressLine1").val('');
    $("#txtAddressLine2").val('');
    $("#txtAddressLine3").val('');
    CountryCallBack();
    $("#txtAddressPostalCode").val('');
}


function ClientEngagingCallServices(path, templateId, containerId, parameters, clearContent, callBack) {
    $.ajax({
        type: "POST",
        url: path,
        data: parameters,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        cache: true,
        success: function (msg) {
            if (msg == 0) {
                ShowNotify('Invalid session login again.', 'error', 3000);
                return false;
            }

            $GlobalWOAppointmentOrCessationDetailsData.WOTransferDetailsData = msg;
            $GlobalWOAppointmentOrCessationDetailsData.InsertedID = msg;

            $GlobalClientExistingData.EGMList = msg;
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

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {
            //throw new Error(xhr.statusText);
        }
    });
}