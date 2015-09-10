function DeleteFirstSubscribers(val1, OutputDiv) {

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
                    if (OutputDiv == 'Output_FirstSubscribers')
                        path = "/WODI/DeleteWOINCORPFirstSubscribers";
                    else if (OutputDiv == 'Output_AuthorizedPersonFSDetails')
                        path = "/WODI/DeleteWOINCORPAuthorizedPersonFSDetails";
                    else if (OutputDiv == 'Output_PrincipalDetails')
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
                            try {

                                $GlobalWOInfoDetails.ReturnValue = msg;

                                if (OutputDiv == 'Output_FirstSubscribers') {
                                    FirstSubscribersDetails();
                                    BindddlFirstSubscriber();
                                    AuthorisedSubscribersDetails();

                                }
                                else if (OutputDiv == 'Output_AuthorizedPersonFSDetails')
                                    AuthorisedSubscribersDetails();
                                else if (OutputDiv == 'Output_PrincipalDetails') {
                                    PrincipalDetails();
                                    BindddlPrincipalDetails();
                                    AuthorizedPersonPrincipalDetails();
                                    BindddlPrincipalDetailsForNominee();
                                }
                                else if (OutputDiv == 'Output_AuthorizedPersonPrincipalDetails')
                                    AuthorizedPersonPrincipalDetails();

                            } catch (e) {
                                console.log(e);
                            }

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

function FirstSubscribersDetails() {

    var WOID = $("#hdnWOID").val();
    $.ajax({
        type: "POST",
        url: "/WODI/GetWOINCORPFirstSubscribers",
        data: "{'WOID':'" + WOID + "'}",
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
                else {
                    BindIncropFSDetails(msg, "Output_FirstSubscribers");
                }
            } catch (e) {
                console.log(e);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            //throw new Error(xhr.statusText);
        }
    });
}

function AuthorisedSubscribersDetails() {

    var WOID = $("#hdnWOID").val();
    $.ajax({
        type: "POST",
        url: "/WODI/GetWOINCORPAuthorisedFirstSubscribers",
        data: "{'WOID':'" + WOID + "'}",
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
                else {
                    BindIncropFSDetails(msg, 'Output_AuthorizedPersonFSDetails');
                }
            } catch (e) {
                console.log(e);
            }

        },
        error: function (xhr, ajaxOptions, thrownError) {
            //throw new Error(xhr.statusText);
        }
    });
}

function BindddlFirstSubscriber() {
    try {
        $('#ddlFirstSubscriber').find('option:gt(0)').remove();
        var WOID = $("#hdnWOID").val();
        CallMasterData("/WODI/GetddlFirstSubscriber", 'ddlFirstSubscriberTemplate', 'ddlFirstSubscriber', "{'WOID':'" + WOID + "'}", false, '');

    } catch (e) {
        console.log(e);
    }
}

function BindNomineeDirectorsInPD() {

    $('#ddlNomineeDirectorDetailsInPD').find('option:gt(0)').remove();
    var WOID = parseInt($("#hdnWOID").val());
    var GridCodeOfNomineeDirector = "INCNDD";
    CallMasterData("/WODI/GetNomineeDirectorsInPrincipalDetails", 'ddlNomineeDirectorDetailsInPDTemplate', 'ddlNomineeDirectorDetailsInPD', "{'WOID':" + WOID + ",'InfoCode':'" + GridCodeOfNomineeDirector + "'}", false, '');

}

function PrincipalDetails() {
    try {
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
                try {
                    if (msg == '0') {
                        ShowNotify('Invalid Session login again.', 'error', 3000);
                        return false;
                    }
                    else {
                        BindIncropFSDetails(msg, "Output_PrincipalDetails");
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
        console.log(e);
    }

}

function AuthorizedPersonPrincipalDetails() {
    try {
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

                if (msg == '0') {
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
    } catch (e) {
        console.log(e);
    }
}

function BindddlPrincipalDetails() {
    try {
        $('#ddlPrincipalDetails').find('option:gt(0)').remove();
        var WOID = $("#hdnWOID").val();
        CallMasterData("/WODI/GetddlPrincipalDetails", 'ddlAuthorizedPersonPrincipalDetailsTemplate', 'ddlPrincipalDetails', "{'WOID':'" + WOID + "'}", false, '');

    } catch (e) {
        console.log(e);
    }
}

function BindddlPrincipalDetailsForNominee() {
    try {
        $('#ddlPrincipalDetailsForNominee').find('option:gt(0)').remove();
        var WOID = $("#hdnWOID").val();
        CallMasterData("/WODI/GetddlPrincipalDetails", 'ddlAuthorizedPersonPrincipalDetailsTemplate', 'ddlPrincipalDetailsForNominee', "{'WOID':'" + WOID + "'}", false, '');
    } catch (e) {
        console.log(e);
    }
}


function BindIncropFSDetails(msg, OutputDiv) {
    try {
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
                            d += ' onclick=DeleteFirstSubscribers("' + DeleteParams + '","' + OutputDiv + '"); >';
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

    } catch (e) {
        console.log(e);
    }
}



function CompanyDetailscallBack() {
    try {
        if ($GlobalIncorpData.IncorpData.MeetingAddressCountry != null && $GlobalIncorpData.IncorpData.MeetingAddressCountry != undefined) {
            $("#txtAddressLineOne").val($GlobalIncorpData.IncorpData.MeetingAddressLine1);
            $("#txtAddressLineTwo").val($GlobalIncorpData.IncorpData.MeetingAddressLine2);
            $("#txtAddressLineThree").val($GlobalIncorpData.IncorpData.MeetingAddressLine3);
            if ($GlobalIncorpData.IncorpData.MeetingAddressCountry == 0 || $GlobalIncorpData.IncorpData.MeetingAddressCountry == -1)
                $("#ddlCountryForIncorp").val('109');
            else
                $("#ddlCountryForIncorp").val($GlobalIncorpData.IncorpData.MeetingAddressCountry);
            $("#txtPostalCode").val($GlobalIncorpData.IncorpData.MeetingAddressPostalCode);
        }

    } catch (e) {
        console.log(e);
    }
}

function EGMCallServices(path, templateId, containerId, parameters, clearContent, callBack) {
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
                        ShowNotify('Invalid session login again.', 'error', 3000);
                        return false;
                    }
                    $GlobalEGMData.EGMList = msg;
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
        console.log(e);
    }
}

function CallbackAuthorizedPersonPrincipalDetails() {
    try {
        if ($FSAuthorizedPersonPrincipalDetails.data == '1') {
            $('#divPupup_AuthorizedPersonPrincipalDetails').modal("hide");
            AuthorizedPersonPrincipalDetails();
            ShowNotify('Success.', 'success', 2000);

        }
        else if ($FSAuthorizedPersonPrincipalDetails.data == '0')
            ShowNotify('Session expired.', 'error', 3000);

    } catch (e) {
        console.log(e);
    }
}

function CallbackPrincipalDetails() {
    try {
        if ($FSPrincipalDetails.data == '1') {
            $('#divPupup_PrincipalDetails').modal("hide");
            PrincipalDetails();
            BindddlPrincipalDetails();
            BindddlPrincipalDetailsForNominee();
            ShowNotify('Success.', 'success', 2000);

        }
        else if ($FSPrincipalDetails.data == '0')
            ShowNotify('Session expired.', 'error', 3000);

    } catch (e) {
        console.log(e);
    }
}


function CallbackAuthorizedPersonFSDetails() {
    try {
        if ($FSAuthorizedPersonFSDetails.data == '1') {
            $('#divPupup_AuthorizedPersonFSDetails').modal("hide");
            AuthorisedSubscribersDetails();
            ShowNotify('Success.', 'success', 2000);

        }
        else if ($FSAuthorizedPersonFSDetails.data == '0')
            ShowNotify('Session expired.', 'error', 3000);
    } catch (e) {
        console.log(e);
    }
}



$(document).ready(function () {

    BindCountry();
    BindCurrency();
    BindCompanyType();
    BindIncorpCountry();
    BindClientType();
    BindCompanyStatus();
    $('#ddlTypeofCompanyForIncrop').val('1');
    BindShareClass();
    $GlobalIncorpData = {};
    $GlobalIncorpData.IncorpData = '';
    BindWOIncropDetails();

    $FSDirectorAddressDetails = {};
    $FSDirectorAddressDetails.data = '';

    $FSAuthorizedPersonFSDetails = {};
    $FSAuthorizedPersonFSDetails.data = '';

    $FSPrincipalDetails = {};
    $FSPrincipalDetails.data = '';

    $FSAuthorizedPersonPrincipalDetails = {};
    $FSAuthorizedPersonPrincipalDetails.data = '';


    $FSNomineeDirectorsDetails = {};
    $FSNomineeDirectorsDetails.data = '';

    if ($('#hdnWOCloseStatus').val() == 'hide') {
        $('.btnWOClose').hide();
    }

    $("#chkFMGasRegisteredAddress").change(function () {
        try {
            if ($(this).is(":checked")) {

                InorpDetailsCallService("/WODI/GetCompanyAddressesByWOID", "", "", "{'WOID':" + parseInt($("#hdnWOID").val()) + ",'IsFMGAddress':" + true + "}", false, CompanyDetailscallBack);
            }
            else {
                $("#txtAddressLineOne").val('');
                $("#txtAddressLineTwo").val('');
                $("#txtAddressLineThree").val('');
                $("#ddlCountryForIncorp").val('109');
                $("#txtPostalCode").val('');

            }

        } catch (e) {
            console.log(e);
        }
    });


    //-------------------------------First Subscriber details----------------------------------------

    FirstSubscribersDetails();

    $("#btn_FirstSubscribers").click(function () {
        try {
            $('#ddlDirectorChosen_FirstSubscribers').val('-1').trigger("chosen:updated");
            $('#txtDirectorEmail_FirstSubscribers').val('');
            $('#txtDirectorContactNo_FirstSubscribers').val('');
            $('#txtDirectorFax_FirstSubscribers').val('');
            $('#txtOccupation_FirstSubscribers').val('');
            //  $('#Currency_FirstSubscribers').val('14');
            $("#txtNoofSharesHeld_FirstSubscribers").val('');
            $("#txtTotalAmountPaid_FirstSubscribers").val('');

            $('#divPupup_FirstSubscribers').modal({
                "backdrop": "static",
                "show": "true"
            });

        } catch (e) {
            console.log(e);
        }
    });

    $("#btnWOInfoSave_FirstSubscribers").click(function () {
        try {
            var Mandatory = 0;

            if ($("#ddlDirectorChosen_FirstSubscribers").val() == '')
                Mandatory += 1;

            if ($("#txtNoofSharesHeld_FirstSubscribers").val() == '')
                Mandatory += 1;
            if ($("#txtTotalAmountPaid_FirstSubscribers").val() == '')
                Mandatory += 1;
            //if ($("#Currency_FirstSubscribers").val() == '')
            //    Mandatory += 1;

            if (Mandatory > 0) {
                ShowNotify('Please Enter Data For All Mandatory Feilds.', 'error', 3000);
                return false;
            }

            var checkEmailFormat = validEmail($('#txtDirectorEmail_FirstSubscribers'), false);
            if (checkEmailFormat == 1) {
                ShowNotify('Please enter valid Email.', 'error', 3000);
                return false;
            }
            var Directory = $("#ddlDirectorChosen_FirstSubscribers").val();
            if (Directory == -1 || Directory == undefined || Directory == '') {
                ShowNotify('Please select Director.', 'error', 3000);
                return false;
            }
            //******Start*********
            var personid = $('#ddlDirectorChosen_FirstSubscribers').find('option:selected').attr('personid');
            var sourcecode = $('#ddlDirectorChosen_FirstSubscribers').find('option:selected').attr('sourcecode');

            var DirectorEmail = $('#txtDirectorEmail_FirstSubscribers').val();
            var DirectorContactNo = $('#txtDirectorContactNo_FirstSubscribers').val();
            var DirectorFax = $('#txtDirectorFax_FirstSubscribers').val();

            CallForDirectorAddressDetailsForFS("/PartialContent/InsertOrUpdateDirectorAddress", "", "", "{'PersonId':" + personid + ",'SourceCode':'" + sourcecode + "','DirectorEmail':'" + DirectorEmail + "','DirectorContactNo':'" + DirectorContactNo + "','DirectorFax':'" + DirectorFax + "'}", '', 'FirstSubscribers');
            var occupation = $("#txtOccupation_FirstSubscribers").val();
            var NoOfSharesHeld = $("#txtNoofSharesHeld_FirstSubscribers").val();
            var TotalAmountPaid = $("#txtTotalAmountPaid_FirstSubscribers").val();
            var WOID = $("#hdnWOID").val();
            //  var Currency = $("#Currency_FirstSubscribers").val();

            CallForDirectorAddressDetailsForFS("/WODI/InsertWOINCORPFirstSubscribers", "", "", "{'WOID':'" + WOID + "', 'personid':'" + personid + "','sourcecode':'" + sourcecode + "','occupation':'" + occupation + "','NoOfSharesHeld':'" + NoOfSharesHeld + "','TotalAmountPaid':'" + TotalAmountPaid + "'}", CallbackFirstSubscribers, 'FirstSubscribers');

        } catch (e) {
            console.log(e);
        }
    });

    $('#ddlDirectorChosen_FirstSubscribers').change(function () {
        try {
            var personid = $(this).find('option:selected').attr('personid');
            var sourcecode = $(this).find('option:selected').attr('sourcecode');
            if (personid != '' && personid != undefined) {
                GetAddressDetailsForFirstSubscribers(personid, sourcecode, 'FirstSubscribers');
            }

        } catch (e) {
            console.log(e);
        }
    });

    $('#ddlDirectorChosen_AuthorizedPersonFSDetails').change(function () {
        try {
            var personid = $(this).find('option:selected').attr('personid');
            var sourcecode = $(this).find('option:selected').attr('sourcecode');
            if (personid != '' && personid != undefined) {
                GetAddressDetailsForFirstSubscribers(personid, sourcecode, 'AuthorizedPersonFSDetails');
            }

        } catch (e) {
            console.log(e);
        }
    });

    $('#ddlDirectorChosen_AuthorizedPersonPrincipalDetails').change(function () {
        try {
            var personid = $(this).find('option:selected').attr('personid');
            var sourcecode = $(this).find('option:selected').attr('sourcecode');
            if (personid != '' && personid != undefined) {
                GetAddressDetailsForFirstSubscribers(personid, sourcecode, 'AuthorizedPersonPrincipalDetails');
            }

        } catch (e) {
            console.log(e);
        }
    });

    $('#ddlDirectorChosen_PrincipalDetails').change(function () {
        try {
            var personid = $(this).find('option:selected').attr('personid');
            var sourcecode = $(this).find('option:selected').attr('sourcecode');
            if (personid != '' && personid != undefined) {
                GetAddressDetailsForFirstSubscribers(personid, sourcecode, 'PrincipalDetails');
            }

        } catch (e) {
            console.log(e);
        }
    });

    $('#ddlDirectorChosen_NomineeDirectorsDetails').change(function () {
        try {
            var personid = $(this).find('option:selected').attr('personid');
            var sourcecode = $(this).find('option:selected').attr('sourcecode');
            if (personid != '' && personid != undefined) {
                GetAddressDetailsForFirstSubscribers(personid, sourcecode, 'NomineeDirectorsDetails');
            }

        } catch (e) {
            console.log(e);
        }
    });

    $('#btnIndividual_FirstSubscribers').unbind('click').click(function () {
        $('.divIndividualOrCompany').empty();
        $("#divIndividualOrCompany_FirstSubscribers").load('/WO/partialcontent/_CreateIndividual', function () {
            $("#divIndividualOrCompany_FirstSubscribers").find('.modalIndividual').modal({
                "backdrop": "static",
                "show": "true"
            });
        });

    });

    $('#btnCompany_FirstSubscribers').unbind('click').click(function () {
        $('.divIndividualOrCompany').empty();
        $("#divIndividualOrCompany_FirstSubscribers").load('/WO/partialcontent/_CreateCompany', function () {
            $("#divIndividualOrCompany_FirstSubscribers").find('.modalCompany').modal({
                "backdrop": "static",
                "show": "true"
            });
        });

    });

    //---------------------Authorised FS--------------

    AuthorisedSubscribersDetails();
    BindddlFirstSubscriber();

    $("#btnAuthorizedPersonFSDetails").click(function () {
        try {
            $('#ddlFirstSubscriber').val('');

            $('#ddlDirectorChosen_AuthorizedPersonFSDetails').val('-1').trigger("chosen:updated");
            $('#txtDirectorEmail_AuthorizedPersonFSDetails').val('');
            $('#txtDirectorContactNo_AuthorizedPersonFSDetails').val('');
            $('#txtDirectorFax_AuthorizedPersonFSDetails').val('');

            $('#divPupup_AuthorizedPersonFSDetails').modal({
                "backdrop": "static",
                "show": "true"
            });

        } catch (e) {
            console.log(e);
        }
    });

    $("#btnSave_AuthorizedPersonFSDetails").click(function () {
        try {
            var Mandatory = 0;

            if ($("#ddlDirectorChosen_AuthorizedPersonFSDetails").val() == '')
                Mandatory += 1;
            if ($("#ddlFirstSubscriber").val() == '')
                Mandatory += 1;

            if (Mandatory > 0) {
                ShowNotify('Please Enter Data For All Mandatory Feilds.', 'error', 3000);
                return false;
            }

            var checkEmailFormat = validEmail($('#txtDirectorEmail_AuthorizedPersonFSDetails'), false);
            if (checkEmailFormat == 1) {
                ShowNotify('Please enter valid Email.', 'error', 3000);
                return false;
            }
            var Directory = $("#ddlDirectorChosen_AuthorizedPersonFSDetails").val();
            if (Directory == -1 || Directory == undefined || Directory == '') {
                ShowNotify('Please select Director.', 'error', 3000);
                return false;
            }

            //******Start*********
            var personid = $('#ddlDirectorChosen_AuthorizedPersonFSDetails').find('option:selected').attr('personid');
            var sourcecode = $('#ddlDirectorChosen_AuthorizedPersonFSDetails').find('option:selected').attr('sourcecode');

            var DirectorEmail = $('#txtDirectorEmail_AuthorizedPersonFSDetails').val();
            var DirectorContactNo = $('#txtDirectorContactNo_AuthorizedPersonFSDetails').val();
            var DirectorFax = $('#txtDirectorFax_AuthorizedPersonFSDetails').val();

            CallForDirectorAddressDetailsForFS("/PartialContent/InsertOrUpdateDirectorAddress", "", "", "{'PersonId':" + personid + ",'SourceCode':'" + sourcecode + "','DirectorEmail':'" + DirectorEmail + "','DirectorContactNo':'" + DirectorContactNo + "','DirectorFax':'" + DirectorFax + "'}", '', 'AuthorizedPersonFSDetails');



            var WOID = $("#hdnWOID").val();
            var FSID = $("#ddlFirstSubscriber").val();

            CallForDirectorAddressDetailsForFS("/WODI/InsertWOINCORPAuthorizedPersonFS", "", "", "{'WOID':'" + WOID + "', 'personid':'" + personid + "','sourcecode':'" + sourcecode + "','FSID':'" + FSID + "'}", CallbackAuthorizedPersonFSDetails, 'AuthorizedPersonFSDetails');


            //*******End*************

        } catch (e) {
            console.log(e);
        }
    });

    $('#btnIndividual_AuthorizedPersonFSDetails').unbind('click').click(function () {
        $('.divIndividualOrCompany').empty();
        $("#divIndividualOrCompany_AuthorizedPersonFSDetails").load('/WO/partialcontent/_CreateIndividual', function () {
            $("#divIndividualOrCompany_AuthorizedPersonFSDetails").find('.modalIndividual').modal({
                "backdrop": "static",
                "show": "true"
            });
        });

    });

    $('#btnCompany_AuthorizedPersonFSDetails').unbind('click').click(function () {
        $('.divIndividualOrCompany').empty();
        $("#divIndividualOrCompany_AuthorizedPersonFSDetails").load('/WO/partialcontent/_CreateCompany', function () {
            $("#divIndividualOrCompany_AuthorizedPersonFSDetails").find('.modalCompany').modal({
                "backdrop": "static",
                "show": "true"
            });
        });

    });


    //---------------------Authorised end------------------------

    //---------------------Principal Details--------------
    PrincipalDetails();

    $("#btnPrincipalDetails").click(function () {
        try {
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
        } catch (e) {
            console.log(e);
        }
    });

    $("#btnSave_PrincipalDetails").click(function () {
        try {
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
            CallForDirectorAddressDetailsForFS("/WODI/InsertWOINCORPPrincipalDetails", "", "", "{'WOID':'" + WOID + "', 'personid':'" + personid + "','sourcecode':'" + sourcecode + "','NDPersonId':" + NDpersonid + ",'NDSourceCode':'" + NDsourcecode + "','ContactPerson':'" + ContactPerson + "'}", CallbackPrincipalDetails, 'PrincipalDetails');


            //*******End*************

        } catch (e) {
            console.log(e);
        }
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

    //---------------------end PrincipalDetails------------------------


    //---------------------AuthorizedPersonPrincipalDetails--------------

    AuthorizedPersonPrincipalDetails();
    BindddlPrincipalDetails();

    $("#btnAuthorizedPersonPrincipalDetails").click(function () {
        try {
            $('#ddlPrincipalDetails').val('');
            $('#ddlDirectorChosen_AuthorizedPersonPrincipalDetails').val('-1').trigger("chosen:updated");
            $('#txtDirectorEmail_AuthorizedPersonPrincipalDetails').val('');
            $('#txtDirectorContactNo_AuthorizedPersonPrincipalDetails').val('');
            $('#txtDirectorFax_AuthorizedPersonPrincipalDetails').val('');

            $('#divPupup_AuthorizedPersonPrincipalDetails').modal({
                "backdrop": "static",
                "show": "true"
            });
        } catch (e) {
            console.log(e);
        }
    });

    $("#btnSave_AuthorizedPersonPrincipalDetails").click(function () {
        try {
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


        } catch (e) {

        }
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

    //---------------------AuthorizedPersonPrincipalDetails end------------------------




    $('#txtIncropDate').datepicker({
        showOtherMonths: true,
        selectOtherMonths: true,
        showWeek: true,
        changeYear: true,
        changeMonth: true,
        dateFormat: 'dd/mm/yy',
        yearRange: "-10:+50"
    });
    $('#dateFinancialYearEnd').datepicker({
        showOtherMonths: true,
        selectOtherMonths: true,
        showWeek: true,
        changeYear: false,
        changeMonth: false,
        dateFormat: 'dd MM'
    });
    $('#dateFirstFinancialYearEnd').datepicker({
        showOtherMonths: true,
        selectOtherMonths: true,
        showWeek: true,
        changeYear: true,
        changeMonth: true,
        dateFormat: 'dd MM yy',
        yearRange: "-10:+50"
    });



    $("#btnSaveWoInCorpDetails").click(function () {
        try {
            SaveInCorpDetails();
        } catch (e) {
            console.log(e);
        }
    });

    $("#btnCancelWoInCorpDetails").click(function () {
        try {
            clearIncorpData();
        } catch (e) {
            console.log(e);
        }
    });

});



function CallbackAuthorizedPersonPrincipalDetails() {
    try {
        if ($FSAuthorizedPersonPrincipalDetails.data == '1') {
            $('#divPupup_AuthorizedPersonPrincipalDetails').modal("hide");
            AuthorizedPersonPrincipalDetails();
            ShowNotify('Success.', 'success', 2000);

        }
        else if ($FSAuthorizedPersonPrincipalDetails.data == '0')
            ShowNotify('Session expired.', 'error', 3000);
    } catch (e) {
        console.log(e);
    }
}


function SaveInCorpDetails() {
    try {
        InCorpDetails = {};
        InCorpDetails.WOID = $("#hdnWOID").val();
        InCorpDetails.CompanyName = $('#txtCompanyName').val();
        InCorpDetails.ClientNo = $('#txtClientNo').val();
        InCorpDetails.IncropDate = $('#txtIncropDate').val();
        InCorpDetails.RegistrationNo = $('#txtRegistrationNo').val();
        InCorpDetails.TypeofCompany = $('#ddlTypeofCompanyForIncrop').find("option:selected").val();
        InCorpDetails.FMGasRegisteredAddress = $('#chkFMGasRegisteredAddress').is(':checked');
        InCorpDetails.FMGClient = $('#chkFMGClient').is(':checked');
        InCorpDetails.ClientType = $('#ddlClientType').find("option:selected").val();
        InCorpDetails.IncorporationCountry = $('#ddlIncorporationCountry').find("option:selected").val();
        InCorpDetails.CompanyStatus = $('#ddlCompanyStatus').find("option:selected").val();


        InCorpDetails.AddressLine1 = $('#txtAddressLineOne').val();
        InCorpDetails.AddressLine2 = $('#txtAddressLineTwo').val();
        InCorpDetails.AddressLine3 = $('#txtAddressLineThree').val();
        InCorpDetails.Country = $('#ddlCountryForIncorp').find("option:selected").val();
        InCorpDetails.PostalCode = $('#txtPostalCode').val();
        InCorpDetails.FinancialYearEnd = $('#dateFinancialYearEnd').val();

        InCorpDetails.FirstFinancialYearEnd = $('#dateFirstFinancialYearEnd').val();
        InCorpDetails.Currency = $('#ddlCurrencyForIncrop').find("option:selected").val();
        InCorpDetails.ClassofShare = $('#ddlShareClassForIncrop').find("option:selected").val();
        InCorpDetails.PaidupCapital = $('#txtPaidupCapital').val();
        InCorpDetails.AmountPaidupperShare = $('#txtAmountPaidupperShare').val();
        InCorpDetails.ActivityOne = $('#txtActivityOne').val();
        InCorpDetails.ActivityOneDescription = $('#txtActivityOneDesc').val();
        InCorpDetails.ActivityTwo = $('#txtActivityTwo').val();
        InCorpDetails.ActivityTwoDescription = $('#txtActivityTwoDesc').val();

        InCorpDetails.AmountGuaranteedByEachMember = $('#txtGuaranteedAmount').val();
        InCorpDetails.GuaranteedAmountCurrency = $('#ddlGuaranteedAmountCurrency').val();

        var jsonText = JSON.stringify({ InCorpDetails: InCorpDetails });

        var count = 0;
        count += ControlEmptyNess(false, $('#txtCompanyName'), 'Please enter CompanyName.');
        count += ControlEmptyNess(false, $('#txtClientNo'), 'Please enter Client No.');
        count += ControlEmptyNess(false, $("#txtIncropDate"), 'Please enter Incorporation Date.');
        count += ControlEmptyNess(false, $("#txtRegistrationNo"), 'Please enter Registration No.');
        count += ControlEmptyNess(false, $("#txtAddressLineOne"), 'Please enter Address Line1.');

        if (InCorpDetails.Country == '')
            InCorpDetails.Country = 0;
        if (InCorpDetails.Currency == '')
            InCorpDetails.Currency = 0;
        if (InCorpDetails.ClassofShare == '')
            InCorpDetails.ClassofShare = 0;
        if (InCorpDetails.GuaranteedAmountCurrency == '')
            InCorpDetails.GuaranteedAmountCurrency = 0;

        if (count > 0) {
            ShowNotify('Please Enter values for all mandatory fields.', 'error', 3000);
            return false;
        }
        else {
            InorpDetailsCallService("/WODI/InsertWOINCORPDetails", "", "", jsonText, false, IncorpSavedCallBack);
        }

    } catch (e) {
        console.log(e);
    }
}

function IncorpSavedCallBack() {
    try {
        if ($GlobalIncorpData.IncorpData > 1) {
            ShowNotify('Success.', 'success', 2000);
        }
        return false;
    } catch (e) {
        console.log(e);
    }
}

function BindWOIncropDetails() {

    try {
        var WOID = $("#hdnWOID").val();
        InorpDetailsCallService("/WODI/BindWOIncorpDetails", '', '', "{'WOID':" + WOID + "}", false, BindIncorpDetails);
    } catch (e) {
        console.log(e);
    }
}

function BindCurrency() {
    try {
        //   CallMasterData("/PartialContent/GetCurrencyDetails", 'CurrencyDropDownTemplateinForIncorp', 'Currency_FirstSubscribers', "{}", false, CurrencyCallBackforIncorp);
        CallMasterData("/PartialContent/GetCurrencyDetails", 'DropDownCurrencyTemplateFroIncorp', 'ddlCurrencyForIncrop', "{}", false, CurrencyCallBackforIncorp);
        CallMasterData("/PartialContent/GetCurrencyDetails", 'DropDownGuaranteedAmountCurrency', 'ddlGuaranteedAmountCurrency', "{}", false, CurrencyCallBackforIncorp);

    } catch (e) {
        console.log(e);
    }
}

function BindCompanyType() {
    try {
        CallMasterData("/PartialContent/GetCompanyTypeDetails", 'DropDownCompanyTypeTemplateFroIncorp', 'ddlTypeofCompanyForIncrop', "{'IsIncorp': 1}", false, "");
    } catch (e) {
        console.log(e);
    }
}

function BindShareClass() {
    try {
        var WOID = parseInt($("#hdnWOID").val());
        CallMasterData("/PartialContent/GetShareClassDetails", 'DropDownShareClassTemplateFroIncorp', 'ddlShareClassForIncrop', "{'WOID':" + WOID + "}", false, callbackClassofsharefoIncorp);
    } catch (e) {
        console.log(e);
    }
}

function BindCountry() {
    try {
        CallMasterData("/PartialContent/GetCountryDetails", 'DropDownCompanyTemplateForIncorp', 'ddlCountryForIncorp', "{}", false, ContryCallbackforIncorp);
    } catch (e) {
        console.log(e);
    }
}

function BindIncorpCountry() {
    try {
        CallMasterData("/PartialContent/GetCountryDetails", 'DropDownCompanyTemplateForIncorp', 'ddlIncorporationCountry', "{}", false, IncorpCountryCallback);
    } catch (e) {
        console.log(e);
    }
}

function IncorpCountryCallback() {
    try {
        $("#ddlIncorporationCountry").val("109");
    } catch (e) {
        console.log(e);
    }
}

function BindClientType() {
    try {
        CallMasterData("/PartialContent/GetClientType", 'scriptClientType', 'ddlClientType', "{}", false, ClientTypeCallBack);
    } catch (e) {
        console.log(e);
    }
}

function BindCompanyStatus() {
    try {
        CallMasterData("/PartialContent/GetCompanyStatus", 'scriptCompanyStatus', 'ddlCompanyStatus', "{}", false, CompanyStatusCallbackforIncorp);
    } catch (e) {
        console.log(e);
    }
}

function ClientTypeCallBack() {
    try {
        $("#ddlClientType").val("1");
    } catch (e) {
        console.log(e);
    }
}



function CurrencyCallBackforIncorp() {
    try {
        $("#ddlCurrencyForIncrop").val("14");
        // $("#Currency_FirstSubscribers").val("14");
        $("#ddlGuaranteedAmountCurrency").val("14");

    } catch (e) {
        console.log(e);
    }
}

function CompanyStatusCallbackforIncorp() {
    try {
        $("#ddlCompanyStatus").val("1");
    } catch (e) {
        console.log(e);
    }
}

function ContryCallbackforIncorp() {
    try {
        $("#ddlCountryForIncorp").val("109");
    } catch (e) {
        console.log(e);
    }
}

function callbackClassofsharefoIncorp() {
    $("#ddlShareClassForIncrop").val("11");
}


function InorpDetailsCallService(path, templateId, containerId, parameters, clearContent, callBack) {
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

                $GlobalIncorpData.IncorpData = msg;

                if (callBack != undefined && callBack != '')
                    callBack();

            },
            error: function (xhr, ajaxOptions, thrownError) {
                //throw new Error(xhr.statusText);
            }
        });

    } catch (e) {
        console.log(e);
    }
}



function BindIncorpDetails() {
    try {
        if ($GlobalIncorpData.IncorpData == 0) {
            ShowNotify('Session expired.', 'error', 3000);
            return false;
        }
        else if ($GlobalIncorpData.IncorpData == -1) {
            ShowNotify('Error.', 'error', 3000);
            return false;
        }
        else if ($GlobalIncorpData.IncorpData == -2) {

            ShowNotify('Please enter valid date.', 'error', 3000);
            return false;
        }

        if ($GlobalIncorpData.IncorpData.ClientID != '0' && $GlobalIncorpData.IncorpData.ClientSource != '') {

            $('#txtCompanyName').attr("disabled", "disabled");
            $('#txtRegistrationNo').attr("disabled", "disabled");

        }
        else {
            $('#txtCompanyName').removeAttr("disabled", "disabled");
            $('#txtRegistrationNo').removeAttr("disabled", "disabled");
        }
        $('#txtCompanyName').val($GlobalIncorpData.IncorpData.CompanyName);
        $('#txtRegistrationNo').val($GlobalIncorpData.IncorpData.RegistrationNo);

        if ($GlobalIncorpData.IncorpData > 1) {
            ShowNotify('Success.', 'success', 2000);
        }
        else if ($GlobalIncorpData.IncorpData.WOID != 0) {

            $('#txtCompanyName').val($GlobalIncorpData.IncorpData.CompanyName);
            $('#txtClientNo').val($GlobalIncorpData.IncorpData.ClientNo);

            $('#txtIncropDate').val($GlobalIncorpData.IncorpData.IncropDate);

            $('#txtRegistrationNo').val($GlobalIncorpData.IncorpData.RegistrationNo);

            $('#chkFMGClient').prop('checked', $GlobalIncorpData.IncorpData.FMGClient);
            $('#ddlClientType').val($GlobalIncorpData.IncorpData.ClientType);
            $('#ddlIncorporationCountry').val($GlobalIncorpData.IncorpData.IncorporationCountry);
            $('#ddlCompanyStatus').val($GlobalIncorpData.IncorpData.CompanyStatus);

            if ($GlobalIncorpData.IncorpData.TypeofCompany != "" && $GlobalIncorpData.IncorpData.TypeofCompany != null)
                $('#ddlTypeofCompanyForIncrop').val($GlobalIncorpData.IncorpData.TypeofCompany);

            var ischecked = $GlobalIncorpData.IncorpData.FMGasRegisteredAddress;

            $('#chkFMGasRegisteredAddress').prop('checked', ischecked);


            $('#txtAddressLineOne').val($GlobalIncorpData.IncorpData.AddressLine1);
            $('#txtAddressLineTwo').val($GlobalIncorpData.IncorpData.AddressLine2);
            $('#txtAddressLineThree').val($GlobalIncorpData.IncorpData.AddressLine3);
            $('#ddlCountryForIncorp').val($GlobalIncorpData.IncorpData.Country);
            $('#txtPostalCode').val($GlobalIncorpData.IncorpData.PostalCode);
            $('#dateFinancialYearEnd').val($GlobalIncorpData.IncorpData.FinancialYearEnd);
            $('#dateFirstFinancialYearEnd').val($GlobalIncorpData.IncorpData.FirstFinancialYearEnd);
            $('#ddlCurrencyForIncrop').val($GlobalIncorpData.IncorpData.Currency);
            $('#ddlShareClassForIncrop').val($GlobalIncorpData.IncorpData.ClassofShare);
            $('#txtPaidupCapital').val($GlobalIncorpData.IncorpData.PaidupCapital == 0 ? '' : $GlobalIncorpData.IncorpData.PaidupCapital);
            $('#txtAmountPaidupperShare').val($GlobalIncorpData.IncorpData.AmountPaidupperShare == 0 ? '1.0' : $GlobalIncorpData.IncorpData.AmountPaidupperShare);
            $('#txtActivityOne').val($GlobalIncorpData.IncorpData.ActivityOne);
            $('#txtActivityOneDesc').val($GlobalIncorpData.IncorpData.ActivityOneDescription);
            $('#txtActivityTwo').val($GlobalIncorpData.IncorpData.ActivityTwo);
            $('#txtActivityTwoDesc').val($GlobalIncorpData.IncorpData.ActivityTwoDescription);

            $('#txtGuaranteedAmount').val($GlobalIncorpData.IncorpData.AmountGuaranteedByEachMember == 0 ? '' : $GlobalIncorpData.IncorpData.AmountGuaranteedByEachMember);
            $('#ddlGuaranteedAmountCurrency').val($GlobalIncorpData.IncorpData.GuaranteedAmountCurrency);


        }
        else
            $('#chkFMGClient').prop('checked', true);

    } catch (e) {
        console.log(e);
    }
}

function clearIncorpData() {
    try {
        var IsCompanyReadOnly = $('#txtCompanyName').prop("disabled");
        if (IsCompanyReadOnly == false)
            $('#txtCompanyName').val("");

        $('#txtClientNo').val("");
        $('#txtIncropDate').val("");
        var IsRegReadOnly = $('#txtRegistrationNo').prop("disabled");
        if (IsRegReadOnly == false)
            $('#txtRegistrationNo').val("");
        $('#ddlTypeofCompanyForIncrop').val("");
        $('#chkFMGClient').prop('checked', false);
        $('#chkFMGasRegisteredAddress').prop('checked', false);
        $('#txtAddressLineOne').val("");
        $('#txtAddressLineTwo').val("");
        $('#txtAddressLineThree').val("");
        $('#txtPostalCode').val("");
        $('#dateFinancialYearEnd').val("");
        $('#dateFirstFinancialYearEnd').val("");
        $('#ddlShareClassForIncrop').val("");
        $('#txtPaidupCapital').val("");
        $('#txtAmountPaidupperShare').val("");
        $('#txtActivityOne').val("");
        $('#txtActivityOneDesc').val("");
        $('#txtActivityTwo').val("");
        $('#txtActivityTwoDesc').val("");
        $('#txtGuaranteedAmount').val("");
        IncorpCountryCallback();
        CompanyStatusCallbackforIncorp();
        IncorpCountryCallback();
        CurrencyCallBackforIncorp();
        ContryCallbackforIncorp();
    } catch (e) {
        console.log(e);
    }
}


function keypressvalidation() {
    try {
        var checkCharater = AllowNumbersOnly($(this).val(), event);
        if (!checkCharater) {
            event.preventDefault();
        }
    } catch (e) {
        console.log(e);
    }

}






function CallbackFirstSubscribers() {
    try {
        if ($FSDirectorAddressDetails.data == '1') {
            $('#divPupup_FirstSubscribers').modal("hide");
            FirstSubscribersDetails();
            BindddlFirstSubscriber();
            ShowNotify('Success.', 'success', 2000);

        }
        else if ($FSDirectorAddressDetails.data == '0')
            ShowNotify('Session expired.', 'error', 3000);
    } catch (e) {
        console.log(e);
    }
}
function GetAddressDetailsForFirstSubscribers(personid, sourcecode, CallingFrom) {
    try {
        CallForDirectorAddressDetailsForFS("/PartialContent/GetDirectorAddressDetails", "", "", "{'PersonId':" + personid + ",'SourceCode':'" + sourcecode + "'}", CallBackDirectorAddressDetails, CallingFrom);
    } catch (e) {
        console.log(e);
    }
}

function CallBackDirectorAddressDetails() {
    try {
        if ($FSDirectorAddressDetails.data.length > 0) {
            $('#txtDirectorEmail_FirstSubscribers').val($FSDirectorAddressDetails.data[0].Email);
            $('#txtDirectorContactNo_FirstSubscribers').val($FSDirectorAddressDetails.data[0].Phone);
            $('#txtDirectorFax_FirstSubscribers').val($FSDirectorAddressDetails.data[0].Fax);

        }
        else {
            $('#txtDirectorEmail_FirstSubscribers').val("");
            $('#txtDirectorContactNo_FirstSubscribers').val("");
            $('#txtDirectorFax_FirstSubscribers').val("");
        }


        if ($FSAuthorizedPersonFSDetails.data.length > 0) {
            $('#txtDirectorEmail_AuthorizedPersonFSDetails').val($FSAuthorizedPersonFSDetails.data[0].Email);
            $('#txtDirectorContactNo_AuthorizedPersonFSDetails').val($FSAuthorizedPersonFSDetails.data[0].Phone);
            $('#txtDirectorFax_AuthorizedPersonFSDetails').val($FSAuthorizedPersonFSDetails.data[0].Fax);

        }
        else {
            $('#txtDirectorEmail_AuthorizedPersonFSDetails').val("");
            $('#txtDirectorContactNo_AuthorizedPersonFSDetails').val("");
            $('#txtDirectorFax_AuthorizedPersonFSDetails').val("");
        }


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


        if ($FSNomineeDirectorsDetails.data.length > 0) {
            $('#txtDirectorEmail_NomineeDirectorsDetails').val($FSNomineeDirectorsDetails.data[0].Email);
            $('#txtDirectorContactNo_AuthorizedPersonPrincipalDetails').val($FSNomineeDirectorsDetails.data[0].Phone);
            $('#txtDirectorFax_NomineeDirectorsDetails').val($FSNomineeDirectorsDetails.data[0].Fax);
        }
        else {
            $('#txtDirectorEmail_NomineeDirectorsDetails').val("");
            $('#txtDirectorContactNo_NomineeDirectorsDetails').val("");
            $('#txtDirectorFax_NomineeDirectorsDetails').val("");
        }

    } catch (e) {
        console.log(e);
    }
}
//-----------------------------------------------------------------------
function CallForDirectorAddressDetailsForFS(path, templateId, containerId, parameters, callBack, CallingFrom) {
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
                    if (CallingFrom == 'FirstSubscribers')
                        $FSDirectorAddressDetails.data = msg;
                    else if (CallingFrom == 'AuthorizedPersonFSDetails')
                        $FSAuthorizedPersonFSDetails.data = msg;
                    else if (CallingFrom == 'PrincipalDetails')
                        $FSPrincipalDetails.data = msg;
                    else if (CallingFrom == 'AuthorizedPersonPrincipalDetails')
                        $FSAuthorizedPersonPrincipalDetails.data = msg;
                    else if (CallingFrom == 'NomineeDirectorsDetails')
                        $FSNomineeDirectorsDetails.data = msg;


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