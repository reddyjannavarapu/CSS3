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
                    //else if (OutputDiv == 'Output_DirectorDetails')
                    //    path = "/WODI/DeleteWOTakeOverDirectorDetails";
                    //else if (OutputDiv == 'Output_ShareholderDetails')
                    //    path = "/WODI/DeleteWOTakeOverShareholderDetails";


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
                            //else if (OutputDiv == 'Output_DirectorDetails')
                            //    BindDirectorDetails();
                            //else if (OutputDiv == 'Output_ShareholderDetails')
                            //    BindShareholderDetails();


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

//function BindDirectorDetails() {

//    var WOID = $("#hdnWOID").val();
//    $.ajax({
//        type: "POST",
//        url: "/WODI/GetWOTakeOverDirectorDetails",
//        data: "{'WOID':'" + WOID + "'}",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        async: false,
//        cache: true,
//        success: function (msg) {

//            if (msg == '0') {
//                ShowNotify('Invalid Session login again.', 'error', 3000);
//                return false;
//            }
//            else {
//                BindIncropFSDetails(msg, "Output_DirectorDetails");
//            }
//        },
//        error: function (xhr, ajaxOptions, thrownError) {
//            //throw new Error(xhr.statusText);
//        }
//    });
//}

//function BindShareholderDetails() {

//    var WOID = $("#hdnWOID").val();
//    $.ajax({
//        type: "POST",
//        url: "/WODI/GetWOTakeOverShareholderDetails",
//        data: "{'WOID':'" + WOID + "'}",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        async: false,
//        cache: true,
//        success: function (msg) {

//            if (msg == '0') {
//                ShowNotify('Invalid Session login again.', 'error', 3000);
//                return false;
//            }
//            else {
//                BindIncropFSDetails(msg, "Output_ShareholderDetails");
//            }
//        },
//        error: function (xhr, ajaxOptions, thrownError) {
//            //throw new Error(xhr.statusText);
//        }
//    });
//}

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

    //Takeover
    $GlobalClientTakeOverData = {};
    $GlobalClientTakeOverData.TakeOverList = '';
    $GlobalClientTakeOverData.WOID = '';
    $GlobalClientTakeOverData.CompanyName = '';
    $GlobalClientTakeOverData.RegistrationNo = '';
    $GlobalClientTakeOverData.DateAccBeLaid = '';
    // $GlobalClientTakeOverData.AllotmentShares = '';
    $GlobalClientTakeOverData.Currency = '';
    $GlobalClientTakeOverData.ClassOfShare = '';
    $GlobalClientTakeOverData.NewAllottedShares = '';
    $GlobalClientTakeOverData.EachShare = '';
    $GlobalClientTakeOverData.AmountPaidToEachShare = '';
    $GlobalClientTakeOverData.TotalConsideration = '';
    $GlobalClientTakeOverData.NoOfIssuedShares = '';
    $GlobalClientTakeOverData.IssuedCapital = '';
    $GlobalClientTakeOverData.IsFMRegisteredAddress = '';
    $GlobalClientTakeOverData.AddressLine1 = '';
    $GlobalClientTakeOverData.AddressLine2 = '';
    $GlobalClientTakeOverData.AddressLine3 = '';
    $GlobalClientTakeOverData.AddressCountry = '';
    $GlobalClientTakeOverData.AddressPostalCode = '';
    $GlobalClientTakeOverData.OutGoingAuditor = '';
    $GlobalClientTakeOverData.Auditor = '';
    //$GlobalClientTakeOverData.MeetingNoticeSource = '';
    $GlobalClientTakeOverData.MeetingNotice = '';
    //$GlobalClientTakeOverData.MeetingMinutesSource = '';
    $GlobalClientTakeOverData.MeetingMinutes = '';
    //$GlobalClientTakeOverData.OthersMeetingMinutes = '';
    $GlobalClientTakeOverData.ResultantPaidupCapital = '';
    $GlobalClientTakeOverData.Designation = '';
    //$GlobalClientTakeOverData.NoticeResolutionSource = '';
    $GlobalClientTakeOverData.NoticeResolution = '';
    //$GlobalClientTakeOverData.F24F25Source = '';
    $GlobalClientTakeOverData.F24F25ID = '';
    $GlobalClientTakeOverData.ShareHoldingStructure = '';

    //$GlobalClientTakeOverData.NomineeDirectorServiceAgreement = '';

    //$GlobalClientTakeOverData.NomineeSecretaryServiceAgreement1 = '';
    //$GlobalClientTakeOverData.NomineeSecretaryServiceAgreement2 = '';

    $FSPrincipalDetails = {};
    $FSPrincipalDetails.data = '';

    $FSAuthorizedPersonPrincipalDetails = {};
    $FSAuthorizedPersonPrincipalDetails.data = '';

    $FSDirectorDetails = {};
    $FSDirectorDetails.data = '';

    $FSShareholderDetails = {};
    $FSShareholderDetails.data = '';

    if ($('#hdnWOCloseStatus').val() == 'hide') {
        $('.btnWOClose').hide();
    }

    BindDropdowns();

    //---------------------Director Details--------------
    // BindDirectorDetails();

    //$("#btnDirectorsDetails").click(function () {

    //    $('#txtDirector_Name').val('');

    //    $('#divPupup_DirectorDetails').modal({
    //        "backdrop": "static",
    //        "show": "true"
    //    });
    //});

    //$("#btnSave_DirectorDetails").click(function () {

    //    var Mandatory = 0;

    //    if ($("#txtDirector_Name").val() == '')
    //        Mandatory += 1;

    //    if (Mandatory > 0) {
    //        ShowNotify('Please Enter Director Name.', 'error', 3000);
    //        return false;
    //    }

    //    var DirectorName = $('#txtDirector_Name').val();
    //    var WOID = $("#hdnWOID").val();
    //    CallForDirectorAddressDetailsForFS("/WODI/InsertTakeOverDirectorDetails", "", "", "{'WOID':'" + WOID + "', 'DirectorName':'" + DirectorName + "'}", CallbackDirectorDetails, 'DirectorDetails');

    //});

    //function CallbackDirectorDetails() {
    //    if ($FSDirectorDetails.data == '1') {
    //        $('#divPupup_DirectorDetails').modal("hide");
    //        //BindDirectorDetails();
    //        ShowNotify('Success.', 'success', 2000);
    //    }
    //    else if ($FSDirectorDetails.data == '0')
    //        ShowNotify('Session expired.', 'error', 3000);
    //}

    //---------------------End Director Details------------


    //---------------------Shareholder Details--------------
    // BindShareholderDetails();

    //$("#btnShareholderDetails").click(function () {
    //    $('#txtShareholder_Name').val('');
    //    $('#divPupup_ShareholderDetails').modal({
    //        "backdrop": "static",
    //        "show": "true"
    //    });
    //});

    //$("#btnSave_ShareholderDetails").click(function () {

    //    var Mandatory = 0;

    //    if ($("#txtShareholder_Name").val() == '')
    //        Mandatory += 1;

    //    if (Mandatory > 0) {
    //        ShowNotify('Please Enter Shareholder Name.', 'error', 3000);
    //        return false;
    //    }

    //    var ShareholderName = $('#txtShareholder_Name').val();
    //    var WOID = $("#hdnWOID").val();
    //    CallForDirectorAddressDetailsForFS("/WODI/InsertTakeOverShareholderDetails", "", "", "{'WOID':'" + WOID + "', 'ShareholderName':'" + ShareholderName + "'}", CallbackShareholderDetails, 'ShareholderDetails');

    //});

    //function CallbackShareholderDetails() {
    //    if ($FSShareholderDetails.data == '1') {
    //        $('#divPupup_ShareholderDetails').modal("hide");
    //       // BindShareholderDetails();
    //        ShowNotify('Success.', 'success', 2000);
    //    }
    //    else if ($FSShareholderDetails.data == '0')
    //        ShowNotify('Session expired.', 'error', 3000);
    //}


    //---------------------End Shareholder Details------------









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
        var NDsourcecode = ''; $('#ddlNomineeDirectorDetailsInPD').find('option:selected').attr('PersonSource');

        var DirectorEmail = $('#txtDirectorEmail_PrincipalDetails').val();
        var DirectorContactNo = $('#txtDirectorContactNo_PrincipalDetails').val();
        var DirectorFax = $('#txtDirectorFax_PrincipalDetails').val();
        var ContactPerson = $('#txtContactPerson_PrincipalDetails').val();

        CallForDirectorAddressDetailsForFS("/PartialContent/InsertOrUpdateDirectorAddress", "", "", "{'PersonId':" + personid + ",'SourceCode':'" + sourcecode + "','DirectorEmail':'" + DirectorEmail + "','DirectorContactNo':'" + DirectorContactNo + "','DirectorFax':'" + DirectorFax + "'}", '', 'AuthorizedPersonFSDetails');


        var WOID = $("#hdnWOID").val();
        CallForDirectorAddressDetailsForFS("/WODI/InsertWOINCORPPrincipalDetails", "", "", "{'WOID':'" + WOID + "', 'personid':'" + personid + "','sourcecode':'" + sourcecode + "','NDPersonId':" + NDpersonid + ",'NDSourceCode':'" + NDsourcecode + "','ContactPerson':'" + ContactPerson + "'}", CallbackPrincipalDetails, 'PrincipalDetails');


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
                else if (CallingFrom == 'DirectorDetails')
                    $FSDirectorDetails.data = msg;
                else if (CallingFrom == 'ShareholderDetails')
                    $FSShareholderDetails.data = msg;

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



    $('#txtDateOfNextAccToBeLaid').datepicker({
        showOtherMonths: true,
        selectOtherMonths: true,
        showWeek: true,
        changeYear: true,
        changeMonth: true,
        dateFormat: 'dd/mm/yy',
        yearRange: "-10:+50"
    });





    $("#txtAddressPostalCode").keypress(function (event) {
        try {
            var checkCharater = AllowNumbersCharactersOnly($(this).val(), event);
            if (!checkCharater) {
                event.preventDefault();
            }
        }
        catch (ex) {
            throw ex;
        }

    });



    $("#chkFMHasRegisteredAddress").change(function () {

        if ($(this).is(":checked")) {

            ClientEngagingCallServices("GetCompanyAddressesByWOID", '', '', "{'WOID':" + parseInt($("#hdnWOID").val()) + ",'IsFMGAddress':" + true + "}", false, CompanyDetailscallBack);
        }
        else ClearFMHasRegisteredAddressFields();
    });

    $("#btnClear").click(function () {

        var IsCompanyReadOnly = $('#txtCompanyName').prop("disabled");
        if (IsCompanyReadOnly == false)
            $('#txtCompanyName').val("");
        var IsRegReadOnly = $('#txtRegistrationNo').prop("disabled");
        if (IsRegReadOnly == false)
            $('#txtRegistrationNo').val("");
        $("#txtDateOfNextAccToBeLaid").val('');
        //$("#txtReturnOfAllotedShares").val('');
        CurrencyCallBack();
        CountryCallBack();
        $("#ddlClassOfShare").val('11');
        $("#txtAllotedShares").val('');
        $("#txtConsiderationOfEachShare").val('');
        $("#txtPaidOnEachShare").val('');
        $("#txtTotalConsideration").val('');
        $("#txtTotalIssuedShares").val('');
        $("#txtResultantIssuedCapital").val('');
        $("#chkFMHasRegisteredAddress").prop("checked", false);
        $("#txtAddressLine1").val('');
        $("#txtAddressLine2").val('');
        $("#txtAddressLine3").val('');
        $("#txtAddressPostalCode").val('');
        $("#txtOutGoingAuditor").val('');
        $("#txtAuditor").val('');
        $('#ddlDirectorChosen_MeetingNotice').val('').trigger('chosen:updated');
        $("#ddlDirectorChosen_MeetingMinutes").val('').trigger('chosen:updated');
        $("#txtOtherMeetingMinutes").val('');
        $("#txtDesignation").val('');
        $("#ddlDirectorChosen_NoticeofResolution").val('').trigger('chosen:updated');
        $("#ddlDirectorChosen_F24ByF25").val('').trigger('chosen:updated');
        $("#ddlShareHoldingStructure").val('-1');
        //$("#txtOtherMeetingMinutes").attr("disabled", false);    
        //$("#txtNomineeDirectorServiceAgreement").val('');
        //$("#txtNomineeSecretaryServiceAgreement1").val(''); $("#txtNomineeSecretaryServiceAgreement2").val('');
        $("#txtResultantPaidUpCapital").val('');
        $("#txtMeetingNotice").val('');
        $("#txtMeetingMinutes").val('');
        $("#txtNoticeResolution").val('');
        $("#txtF24ByF25").val('');

    });
    $("#btnSaveTakeOver").click(function () {
        try {
            $GlobalClientTakeOverData.CompanyName = $("#txtCompanyName").val();
            $GlobalClientTakeOverData.RegistrationNo = $("#txtRegistrationNo").val();
            $GlobalClientTakeOverData.DateAccBeLaid = $("#txtDateOfNextAccToBeLaid").val();
            //  $GlobalClientTakeOverData.AllotmentShares = $("#txtReturnOfAllotedShares").val();
            $GlobalClientTakeOverData.Currency = $("#ddlCurrency").val();
            $GlobalClientTakeOverData.ClassOfShare = $("#ddlClassOfShare").val();
            $GlobalClientTakeOverData.NewAllottedShares = $("#txtAllotedShares").val();
            $GlobalClientTakeOverData.EachShare = $("#txtConsiderationOfEachShare").val();
            $GlobalClientTakeOverData.AmountPaidToEachShare = $("#txtPaidOnEachShare").val();
            $GlobalClientTakeOverData.TotalConsideration = $("#txtTotalConsideration").val();
            $GlobalClientTakeOverData.NoOfIssuedShares = $("#txtTotalIssuedShares").val();
            $GlobalClientTakeOverData.IssuedCapital = $("#txtResultantIssuedCapital").val();
            $GlobalClientTakeOverData.ResultantPaidupCapital = $("#txtResultantPaidUpCapital").val();
            $GlobalClientTakeOverData.IsFMRegisteredAddress = $("#chkFMHasRegisteredAddress").is(":checked");
            $GlobalClientTakeOverData.AddressLine1 = $("#txtAddressLine1").val();
            $GlobalClientTakeOverData.AddressLine2 = $("#txtAddressLine2").val();
            $GlobalClientTakeOverData.AddressLine3 = $("#txtAddressLine3").val();
            $GlobalClientTakeOverData.AddressCountry = $("#ddlAddressCountry").val();
            $GlobalClientTakeOverData.AddressPostalCode = $("#txtAddressPostalCode").val();
            $GlobalClientTakeOverData.OutGoingAuditor = $("#txtOutGoingAuditor").val();
            $GlobalClientTakeOverData.Auditor = $("#txtAuditor").val();
            $GlobalClientTakeOverData.MeetingNotice = $("#txtMeetingNotice").val(); //$("#ddlDirectorChosen_MeetingNotice").find("option:Selected").attr("personid");
            //$GlobalClientTakeOverData.MeetingNoticeSource = $("#ddlDirectorChosen_MeetingNotice").find("option:Selected").attr("sourcecode");
            $GlobalClientTakeOverData.MeetingMinutes = $("#txtMeetingMinutes").val(); //$("#ddlDirectorChosen_MeetingMinutes").find("option:Selected").attr("personid");
            //$GlobalClientTakeOverData.MeetingMinutesSource = $("#ddlDirectorChosen_MeetingMinutes").find("option:Selected").attr("sourcecode");
            //if ($GlobalClientTakeOverData.MeetingMinutes == undefined)
            //    $GlobalClientTakeOverData.OthersMeetingMinutes = $("#txtOtherMeetingMinutes").val();
            //else $GlobalClientTakeOverData.OthersMeetingMinutes = '';
            $GlobalClientTakeOverData.Designation = $("#txtDesignation").val();
            $GlobalClientTakeOverData.NoticeResolution = $("#txtNoticeResolution").val(); //$("#ddlDirectorChosen_NoticeofResolution").find("option:Selected").attr("personid");
            //$GlobalClientTakeOverData.NoticeResolutionSource = $("#ddlDirectorChosen_NoticeofResolution").find("option:Selected").attr("sourcecode");
            $GlobalClientTakeOverData.F24F25ID = $("#txtF24ByF25").val(); //$("#ddlDirectorChosen_F24ByF25").find("option:Selected").attr("personid");
            //$GlobalClientTakeOverData.F24F25Source = $("#ddlDirectorChosen_F24ByF25").find("option:Selected").attr("sourcecode");
            $GlobalClientTakeOverData.ShareHoldingStructure = $("#ddlShareHoldingStructure").val();

            //$GlobalClientTakeOverData.NomineeDirectorServiceAgreement = $("#txtNomineeDirectorServiceAgreement").val();
            //$GlobalClientTakeOverData.NomineeSecretaryServiceAgreement1 = $("#txtNomineeSecretaryServiceAgreement1").val();
            //$GlobalClientTakeOverData.NomineeSecretaryServiceAgreement2 = $("#txtNomineeSecretaryServiceAgreement2").val();


            $GlobalClientTakeOverData.WOID = parseInt($("#hdnWOID").val());
            var JsonData = JSON.stringify({ TakeOverData: $GlobalClientTakeOverData });
            ClientEngagingCallServices("SaveWOTakeOverDetailsByWOID", '', '', JsonData, false, SaveStatusCallBack);
        }
        catch (ex) {
            throw ex;
        }

    });

});

function BindDropdowns() {
    try {
        CallMasterData("/PartialContent/GetCurrencyDetails", 'scriptCurrency', 'ddlCurrency', "{}", false, CurrencyCallBack);
        //CallMasterData("/WODI/GetAllShareHoldingStructures", 'ShareHoldingStructure', 'ddlShareHoldingStructure', "{}", false, '');
        CallMasterData("/PartialContent/GetCountryDetails", 'scriptCountry', 'ddlAddressCountry', "{}", false, CountryCallBack);
        var WOID = parseInt($("#hdnWOID").val());
        CallMasterData("/PartialContent/GetShareClassDetails", 'scriptShareClass', 'ddlClassOfShare', "{'WOID':" + WOID + "}", false, BindWOTakeOVerDetailsByWOID);
    }
    catch (ex) {
        throw ex;
    }
}
function BindWOTakeOVerDetailsByWOID() {
    $GlobalClientTakeOverData.WOID = parseInt($("#hdnWOID").val());
    $("#ddlClassOfShare").val('11');
    ClientEngagingCallServices("GetWOTakeOverDetailsByWOID", '', '', "{'WOID':" + $GlobalClientTakeOverData.WOID + "}", '', BindWOTakeOVerDetailsByWOIDCallBack);
}
function BindWOTakeOVerDetailsByWOIDCallBack() {

    if ($GlobalClientTakeOverData.TakeOverList.WOID != null && $GlobalClientTakeOverData.TakeOverList.WOID != undefined) {

        if ($GlobalClientTakeOverData.TakeOverList.ClientID != '0' && $GlobalClientTakeOverData.TakeOverList.ClientSource != '') {
            //$('#txtCompanyName').val($GlobalClientTakeOverData.TakeOverList.CompanyName);
            //$('#txtRegistrationNo').val($GlobalClientTakeOverData.TakeOverList.RegistrationNo);
            $('#txtCompanyName').attr("disabled", "disabled");
            $('#txtRegistrationNo').attr("disabled", "disabled");

        }
        else {
            $('#txtCompanyName').removeAttr("disabled", "disabled");
            $('#txtRegistrationNo').removeAttr("disabled", "disabled");
        }


        $("#txtCompanyName").val($GlobalClientTakeOverData.TakeOverList.CompanyName);
        $("#txtRegistrationNo").val($GlobalClientTakeOverData.TakeOverList.RegistrationNo);

        $("#txtDateOfNextAccToBeLaid").val($GlobalClientTakeOverData.TakeOverList.DateAccBeLaid);

        if ($GlobalClientTakeOverData.TakeOverList.Currency == -1 || $GlobalClientTakeOverData.TakeOverList.Currency == 0)
            CurrencyCallBack();
        else
            $("#ddlCurrency").val($GlobalClientTakeOverData.TakeOverList.Currency);

        if ($GlobalClientTakeOverData.TakeOverList.ClassOfShare == -1 || $GlobalClientTakeOverData.TakeOverList.ClassOfShare == 0)
            $("#ddlClassOfShare").val('11');
        else
            $("#ddlClassOfShare").val($GlobalClientTakeOverData.TakeOverList.ClassOfShare);

        if ($GlobalClientTakeOverData.TakeOverList.NewAllottedShares == 0)
            $("#txtAllotedShares").val('2');
        else
            $("#txtAllotedShares").val($GlobalClientTakeOverData.TakeOverList.NewAllottedShares);

        if ($GlobalClientTakeOverData.TakeOverList.EachShare == 0)
            $("#txtConsiderationOfEachShare").val('1');
        else
            $("#txtConsiderationOfEachShare").val($GlobalClientTakeOverData.TakeOverList.EachShare);

        if ($GlobalClientTakeOverData.TakeOverList.AmountPaidToEachShare == 0)
            $("#txtPaidOnEachShare").val('1');
        else
            $("#txtPaidOnEachShare").val($GlobalClientTakeOverData.TakeOverList.AmountPaidToEachShare);


        if ($GlobalClientTakeOverData.TakeOverList.TotalConsideration == 0)
            $("#txtTotalConsideration").val('2');
        else
            $("#txtTotalConsideration").val($GlobalClientTakeOverData.TakeOverList.TotalConsideration);

        if ($GlobalClientTakeOverData.TakeOverList.NoOfIssuedShares == 0)
            $("#txtTotalIssuedShares").val('');
        else
            $("#txtTotalIssuedShares").val($GlobalClientTakeOverData.TakeOverList.NoOfIssuedShares);

        if ($GlobalClientTakeOverData.TakeOverList.IssuedCapital == 0)
            $("#txtResultantIssuedCapital").val('');
        else
            $("#txtResultantIssuedCapital").val($GlobalClientTakeOverData.TakeOverList.IssuedCapital);

        if ($GlobalClientTakeOverData.TakeOverList.ResultantPaidupCapital == 0)
            $("#txtResultantPaidUpCapital").val('');
        else
            $("#txtResultantPaidUpCapital").val($GlobalClientTakeOverData.TakeOverList.ResultantPaidupCapital);

        $("#chkFMHasRegisteredAddress").prop("checked", $GlobalClientTakeOverData.TakeOverList.IsFMRegisteredAddress);
        $("#txtAddressLine1").val($GlobalClientTakeOverData.TakeOverList.AddressLine1);
        $("#txtAddressLine2").val($GlobalClientTakeOverData.TakeOverList.AddressLine2);
        $("#txtAddressLine3").val($GlobalClientTakeOverData.TakeOverList.AddressLine3);

        if ($GlobalClientTakeOverData.TakeOverList.AddressCountry == 0 || $GlobalClientTakeOverData.TakeOverList.AddressCountry == -1)
            CountryCallBack();
        else
            $("#ddlAddressCountry").val($GlobalClientTakeOverData.TakeOverList.AddressCountry);


        $("#txtAddressPostalCode").val($GlobalClientTakeOverData.TakeOverList.AddressPostalCode);
        $("#txtOutGoingAuditor").val($GlobalClientTakeOverData.TakeOverList.OutGoingAuditor);
        $("#txtAuditor").val($GlobalClientTakeOverData.TakeOverList.Auditor);

        if ($GlobalClientTakeOverData.TakeOverList.ShareHoldingStructure == 0 || $GlobalClientTakeOverData.TakeOverList.ShareHoldingStructure == -1)
            $("#ddlShareHoldingStructure").val('-1');
        else $("#ddlShareHoldingStructure").val($GlobalClientTakeOverData.TakeOverList.ShareHoldingStructure);

        //$("#txtNomineeDirectorServiceAgreement").val($GlobalClientTakeOverData.TakeOverList.NomineeDirectorServiceAgreement);
        //$("#txtNomineeSecretaryServiceAgreement1").val($GlobalClientTakeOverData.TakeOverList.NomineeSecretaryServiceAgreement1);
        //$("#txtNomineeSecretaryServiceAgreement2").val($GlobalClientTakeOverData.TakeOverList.NomineeSecretaryServiceAgreement2);

        //if ($GlobalClientTakeOverData.TakeOverList.MeetingNotice != 0) {
        //    var MeetingNoticeVal = $("#ddlDirectorChosen_MeetingNotice option[personid=" + $GlobalClientTakeOverData.TakeOverList.MeetingNotice + "]option[sourcecode=" + $GlobalClientTakeOverData.TakeOverList.MeetingNoticeSource + "]").attr("value");
        //    $('#ddlDirectorChosen_MeetingNotice').val(MeetingNoticeVal).trigger('chosen:updated');
        //}
        //else $('#ddlDirectorChosen_MeetingNotice').val('').trigger('chosen:updated');
        $("#txtMeetingNotice").val($GlobalClientTakeOverData.TakeOverList.MeetingNotice);
        $("#txtMeetingMinutes").val($GlobalClientTakeOverData.TakeOverList.MeetingMinutes);
        if ($GlobalClientTakeOverData.TakeOverList.Designation != '' && $GlobalClientTakeOverData.TakeOverList.Designation != null)
            $("#txtDesignation").val($GlobalClientTakeOverData.TakeOverList.Designation);

        $("#txtNoticeResolution").val($GlobalClientTakeOverData.TakeOverList.NoticeResolution);
        $("#txtF24ByF25").val($GlobalClientTakeOverData.TakeOverList.F24F25ID);



    }
}
function SaveStatusCallBack() {
    if ($GlobalClientTakeOverData.TakeOverList >= 1) {
        BindWOTakeOVerDetailsByWOID();
        ShowNotify('Success.', 'success', 2000);
        return false;
    }
}

function CurrencyCallBack() {
    $("#ddlCurrency").val("14");
}
function CountryCallBack() {
    $("#ddlAddressCountry").val("109");
}
function ClearFMHasRegisteredAddressFields() {
    $("#txtAddressLine1").val('');
    $("#txtAddressLine2").val('');
    $("#txtAddressLine3").val('');
    CountryCallBack();
    $("#txtAddressPostalCode").val('');
}

function CompanyDetailscallBack() {
    if ($GlobalClientTakeOverData.TakeOverList.MeetingAddressCountry != null && $GlobalClientTakeOverData.TakeOverList.MeetingAddressCountry != undefined) {
        $("#txtAddressLine1").val($GlobalClientTakeOverData.TakeOverList.MeetingAddressLine1);
        $("#txtAddressLine2").val($GlobalClientTakeOverData.TakeOverList.MeetingAddressLine2);
        $("#txtAddressLine3").val($GlobalClientTakeOverData.TakeOverList.MeetingAddressLine3);
        if ($GlobalClientTakeOverData.TakeOverList.MeetingAddressCountry == 0 || $GlobalClientTakeOverData.TakeOverList.MeetingAddressCountry == -1)
            CountryCallBack();
        else
            $("#ddlAddressCountry").val($GlobalClientTakeOverData.TakeOverList.MeetingAddressCountry);
        $("#txtAddressPostalCode").val($GlobalClientTakeOverData.TakeOverList.MeetingAddressPostalCode);
    }

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
            $GlobalClientTakeOverData.TakeOverList = msg;
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