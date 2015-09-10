
$(document).ready(function () {

    $GlobalWOInfoDetails = {};
    $GlobalWOInfoDetails.ddlID = '';
    $GlobalWOInfoDetails.gridCode = '';
    $GlobalWOInfoDetails.WOID = '';
    $GlobalWOInfoDetails.ReturnValue = '';

    $GlobalDirectorAddressDetails = {};
    $GlobalDirectorAddressDetails.data = '';
    $GlobalDirectorAddressDetails.DynamicID = '';

    if ($('#hdnWOCloseStatus').val() == 'hide') {
        $('.btnWOClose').hide();
    }

    if (window.location.search.split('?').length > 1) {
        var params = window.location.search.split('?')[1].split('&');
        for (var i = 0; i < params.length; i++) {
            var key = params[i].split('=')[0];
            var value = decodeURIComponent(params[i].split('=')[1]);

            if (key.toUpperCase() == "ID") {
                WOID = value
                $GlobalWOInfoDetails.WOID = value;
            }

        }
    }

});

function GetPopupForm(id, gridCode, gridDefaultLoad) {
    //if we want to clear dropdown values while opening the popup  
    //if (gridDefaultLoad != '1') {
    //    $("#ddlDirectorChosen_" + id + " > option:not(:eq(0))").remove();
    //    $("#ddlDirectorChosen_" + id).trigger("chosen:updated");
    //    $('#ddlDirectorChosen_' + id).val('');
    //}
    //else if (gridDefaultLoad == '1') {
    //    $("#ddlDirectorChosen_" + id).val('').trigger("chosen:updated");
    //    $("#ddlDirectorChosen_" + id + '_chosen').find('.chosen-results').empty();
    //}

    $('#txtDirectorEmail_' + id).val('');
    $('#txtDirectorContactNo_' + id).val('');
    $('#txtDirectorFax_' + id).val('');
    $('#txt0').val('');
    $("#txt1").prop('checked', false);
    $(".divDynamicForm").html('');
    CreateForm("/PartialContent/WOInformationForm", id, "{'gridCode':'" + gridCode + "'}");

    $('#divPupup_' + id).appendTo("body").modal({
        "backdrop": "static",
        "show": "true"
    });
}



function CallServices(path, templateId, containerId, parameters, clearContent, callBack) {
    $.ajax({
        type: "POST",
        url: path,
        data: parameters,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        cache: true,
        success: function (msg) {
            $GlobalWOInfoDetails.ReturnValue = msg;
            if (templateId != '' && containerId != '') {

                if (!clearContent) {
                    $.tmpl($('#' + templateId).html(), msg.DirectorsList).appendTo("#" + containerId);
                }
                else {
                    $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg.DirectorsList));
                }

            }
            if (callBack != undefined && callBack != '') {
                if (path == '/PartialContent/InsertWoInformation') {

                    callBack();
                }
                else {
                    callBack(containerId, '');
                }
            }

        },
        error: function (xhr, ajaxOptions, thrownError) {
            throw new Error(xhr.statusText);
        }
    });
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

            if (templateId != '' && containerId != '') {

                if (!clearContent) {
                    $.tmpl($('#' + templateId).html(), msg).appendTo("#" + containerId);
                }
                else {
                    $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg));
                }

            }
            if (callBack != undefined && callBack != '') {
                if (path == '/PartialContent/InsertWoInformation') {

                    callBack();
                }
                else {
                    callBack(containerId, '');
                }
            }

        },
        error: function (xhr, ajaxOptions, thrownError) {
            throw new Error(xhr.statusText);
        }
    });
}


function CreateForm(path, divId, parameters, callBack) {

    $.ajax({
        type: "POST",
        url: path,
        data: parameters,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        cache: true,
        success: function (msg) {

            SetDataToUpdate(msg.QItems, divId);

            if (callBack != undefined && callBack != '')
                callBack("ddlDirectorChosen_" + divId, '');
        },
        error: function (xhr, ajaxOptions, thrownError) {
            throw new Error(xhr.statusText);
        }
    });
}

function SetDataToUpdate(data, divID) {

    $('#hdnItemCounts_' + divID).val(data.length);
    for (var counter = 0; counter < data.length; counter++) {


        var newTextBoxDiv = $(document.createElement('div')).attr("id", 'TextBoxDiv' + counter).attr('class', 'margin-17l');
        newTextBoxDiv.after('#TextBoxDiv' + counter).html(' <div class="row alternate first-child"><div class="col-xs-12"><input type="hidden" id="hdnFieldName' + counter + '" /><input type="hidden" id="hdnIteamId' + counter + '" /><input type="hidden" id="hdnMandatory' + counter + '" /><input type="hidden" id="hdnInfoCode' + counter + '" />' +
        '<input type="hidden" id="hdnType' + counter + '" /><input type="hidden" id="hdnIsMulti' + counter + '" /><label id="lq' + counter + '"> </label><span id="sapn' + counter + '" class="redstar">* </span> </div>' +
        '<div class="anstype col-xs-12"><input id="txt' + counter + '" type="checkbox" class="chk"  />  <input id="txt' + counter + '" type="text" class="large s special" maxlength="50" />  <input id="txt' + counter + '" type="text" class="special large numbers"  onkeypress="return EnterOnlyNumbers(event)" maxlength="30" onpaste="return false"    />   <input id="txt' + counter + '" type="text" class="large decimal special" onkeypress="return AllowDecimalNumbersOnly(this,event,34,4)"   onpaste="return false" /> <textarea  maxlength="400" id="txt' + counter + '" type="text" class="form-control m" cols="" rows"" /> ' +
        '<select id="txt' + counter + '" class="medium typechnage ddl"></select><div id="txt' + counter + '" class="rc form-control"> </div>   </div> </div>');

        newTextBoxDiv.appendTo("#divForm_" + divID);


        //SetLabelText('#lq' + counter, data[counter].FieldName);
        SetLabelText('#lq' + counter, data[counter].FieldDisplayName);


        $('#hdnFieldName' + counter).val(data[counter].FieldName);
        $('#hdnIteamId' + counter).val(data[counter].ID);
        $('#hdnInfoCode' + counter).val(data[counter].InfoCode);
        $('#hdnType' + counter).val(data[counter].FieldType);
        $('#hdnIsMulti' + counter).val(data[counter].IsMultiSelection);
        $('#hdnMandatory' + counter).val(data[counter].IsMandatory);



        if (data[counter].FieldName == 'Dummy') {
            $("#lq" + counter).hide();
        }

        RemoveControls('#txt' + counter, data[counter].AnswerType, data[counter].FieldType, data[counter].RatingFrom, data[counter].RatingTo, data[counter].QuestionOptions, data[counter].IsMultiSelection, data[counter].IncludeOthers, data[counter].IsMandatory, '#sapn' + counter);

        $('#txt' + counter).val(data[counter].DefaultValue);

        if (data[counter].FieldType == 3) {
            var value = "SGD";
            $('#txt' + counter).find("option[value=" + value + "]").attr('selected', 'selected');
        }
    }
}

function RemoveControls(id, anstype, type, from, to, givenOptions, IsMulti, includeOthers, IsMandatory, spanId) {
    if (type == 1) { // Text
        if (anstype == 1) { // Single Line
            $(id).parents('.anstype').find('.m').remove();
            $(id).parents('.anstype').find('.numbers').remove();
            $(id).parents('.anstype').find('.decimal').remove();

        }
        else if (anstype == 2) { // Multi Line
            $(id).parents('.anstype').find('.s').remove();
            $(id).parents('.anstype').find('.numbers').remove();
            $(id).parents('.anstype').find('.decimal').remove();
        }
        else if (anstype == 3) { // Numbers
            $(id).parents('.anstype').find('.s').remove();
            $(id).parents('.anstype').find('.m').remove();
            $(id).parents('.anstype').find('.decimal').remove();
        }
        else if (anstype == 4) { // Decimal
            $(id).parents('.anstype').find('.s').remove();
            $(id).parents('.anstype').find('.m').remove();
            $(id).parents('.anstype').find('.numbers').remove();
        }
        else if (anstype == 5) { // Dummy
            $(id).parents('.anstype').find('.m').remove();
            $(id).parents('.anstype').find('.numbers').remove();
            $(id).parents('.anstype').find('.decimal').remove();
            $(id).parents('.anstype').find('.s').hide();
        }

        $(id).parents('.anstype').find('.ddl').remove();
        $(id).parents('.anstype').find('.rc').remove();
        $(id).parents('.anstype').find('.chk').remove();
    }
    else if (type == 2) { // CheckBox
        $(id).parents('.anstype').find('.s').remove();
        $(id).parents('.anstype').find('.m').remove();
        $(id).parents('.anstype').find('.numbers').remove();
        $(id).parents('.anstype').find('.rc').remove();
        $(id).parents('.anstype').find('.ddl').remove();
        $(id).parents('.anstype').find('.decimal').remove();
    }
    else if (type == 3) { // Drop Down
        $(id).parents('.anstype').find('.s').remove();
        $(id).parents('.anstype').find('.m').remove();
        $(id).parents('.anstype').find('.numbers').remove();
        $(id).parents('.anstype').find('.rc').remove();
        $(id).parents('.anstype').find('.chk').remove();
        $(id).parents('.anstype').find('.decimal').remove();
        LoadScale(id.replace('#', ''), from, to);
    }
    else if (type == 4) { //  CheckBox/Radio List
        $(id).parents('.anstype').find('.s').remove();
        $(id).parents('.anstype').find('.m').remove();
        $(id).parents('.anstype').find('.numbers').remove();
        $(id).parents('.anstype').find('.ddl').remove();
        $(id).parents('.anstype').find('.decimal').remove();
        if (IsMulti == 1)
            LoadListValues(id.replace('#', ''), 'chkTemplate', givenOptions, includeOthers)
        else
            LoadListValues(id.replace('#', ''), 'rdioTemplate', givenOptions, includeOthers)
    }

    if (IsMandatory == 0) {
        $(spanId).remove();
    }
}

function LoadScale(parentId, from, to) {
    CallServicesData("/PartialContent/GetDDLValues", 'ddlTemplate', parentId, "{'from':" + from + ",'to':" + to + "}", true);
}

function LoadListValues(parentId, templateId, givenOptions, includeOthers) {
    CallServicesData("/PartialContent/GetListValues", templateId, parentId, "{'givenOptions':'" + givenOptions + "','includeOthers':" + includeOthers + "}", true);
}

function SetLabelText(id, value) {
    $(id).text(value);
}

function SaveWOInfo(id, gridCode) {

    var checkEmailFormat = validEmail($('#txtDirectorEmail_' + id), false);
    if (checkEmailFormat == 1) {
        ShowNotify('Please enter valid Email.', 'error', 3000);
        return false;
    }
    var Directory = $("#ddlDirectorChosen_" + id).val();
    if (Directory == -1 || Directory == undefined || Directory == '') {
        var DisplayName = $("#ddlDirectorChosen_" + id).attr('data-placeholder');
        ShowNotify('Please select ' + DisplayName + '.', 'error', 3000);
        return false;
    }

    $GlobalWOInfoDetails.ddlID = id;
    $GlobalWOInfoDetails.gridCode = gridCode;

    if (CheckForMandatorys(id)) {


        //******Start*********
        SaveDirectorAddressDetails(id);
        var givenAnswers = givenAnswersData(id);
        //if (CheckForSpecialCharacters(givenAnswers, '', '', false))
        //{
        CallServices("/PartialContent/InsertWoInformation", '', '', "{'givenAnswers':'" + givenAnswers + "'}", true, FillCallBack);
        //}
        //else {
        //    ShowNotify('please remove special characters in given data ( like[!@#$%\^&*(){}[\]<>?/|\-;`]).', 'error', 3000);
        //}

        //*******End*************       

    }
    else {
        ShowNotify('Please Enter Data For All Mandatory Feilds.', 'error', 3000);

    }
}

function SaveDirectorAddressDetails(id) {
    var personid = $('#ddlDirectorChosen_' + id).find('option:selected').attr('personid');
    var sourcecode = $('#ddlDirectorChosen_' + id).find('option:selected').attr('sourcecode');
    var DirectorEmail = $('#txtDirectorEmail_' + id).val();
    var DirectorContactNo = $('#txtDirectorContactNo_' + id).val();
    var DirectorFax = $('#txtDirectorFax_' + id).val();

    CallForDirectorAddressDetails("/PartialContent/InsertOrUpdateDirectorAddress", "", "", "{'PersonId':" + personid + ",'SourceCode':'" + sourcecode + "','DirectorEmail':'" + DirectorEmail + "','DirectorContactNo':'" + DirectorContactNo + "','DirectorFax':'" + DirectorFax + "'}");
}

function CheckForMandatorys(id) {
    var ItemCounts = $('#hdnItemCounts_' + id).val();
    var IsOk = true;
    for (i = 0; i < ItemCounts; i++) {
        try {
            var IsMandatory = $('#hdnMandatory' + i).val();
            var givenAnswer = '';
            var type = $('#hdnType' + i).val();
            var isMulti = $('#hdnIsMulti' + i).val();
            if (type == 4) {
                givenAnswer = GetSelectedValues($('#txt' + i), isMulti);
            }
            else {
                givenAnswer = $('#txt' + i).val();
            }

            if (IsMandatory == 1) {
                if (givenAnswer.length <= 0) {
                    IsOk = false;
                    return IsOk;
                }
            }
        }

        catch (ex) {
        }
    }
    return IsOk;
}

function givenAnswersData(id) {

    var ItemCounts = $('#hdnItemCounts_' + id).val();
    var data = '';
    var WOID = '';

    var DirectorsddlId = $('#ddlDirectorChosen_' + id + ' option:selected').val();
    var PersonID = $('#ddlDirectorChosen_' + id + " option[value=" + DirectorsddlId + "]").attr("personid");
    var PersonSource = $('#ddlDirectorChosen_' + id + " option[value=" + DirectorsddlId + "]").attr("sourcecode");

    for (i = 0; i < ItemCounts; i++) {
        try {
            var InfoCode = $('#hdnInfoCode' + i).val();
            var WOID = $GlobalWOInfoDetails.WOID;

            var FieldID = $('#hdnIteamId' + i).val();
            var FieldDisplayName = $('#lq' + i).text();
            var FieldName = $('#hdnFieldName' + i).val();
            var FieldValue = '';
            var type = $('#hdnType' + i).val();
            var isMulti = $('#hdnIsMulti' + i).val();
            if (type == 2) {
                FieldValue = GetCheckBoxValue($('#txt' + i));
            }
            else if (type == 4) {
                FieldValue = GetSelectedValues($('#txt' + i), isMulti);
            }
            else {
                FieldValue = $('#txt' + i).val();
            }
            data += WOID + ";" + InfoCode + ";" + FieldID + ";" + FieldName + ";" + FieldValue + ";" + PersonID + ";" + PersonSource + ";" + FieldDisplayName + "-";
        }

        catch (ex) {
        }
    }
    return data;
}

function GetCheckBoxValue(id) {

    var str = 0;
    if (id.is(':checked')) {
        str = 1;
    }
    return str;
}

function GetSelectedValues(id, IsMulti) {
    var str = '';
    if (IsMulti == 1) {
        id.find('input:checkbox:checked').each(function () {
            str += $(this).attr('value') + ",";
        });
    }
    else {
        id.find('input:radio:checked').each(function () {
            str += $(this).attr('value') + ",";
        });
    }
    return str;
}

function FillCallBack() {

    if ($GlobalWOInfoDetails.ReturnValue == "0") {
        ShowNotify('Session expired.', 'error', 3000);
        return false;
    }
    var ddlID = $GlobalWOInfoDetails.ddlID;
    var GridCode = $GlobalWOInfoDetails.gridCode;
    var WOID = $GlobalWOInfoDetails.WOID;
    $('#divPupup_' + ddlID).modal('hide');
    BindWOInformationDetails(WOID, GridCode, ddlID);
    ShowNotify('Success.', 'success', 2000);

}

function FillDeleteCallBack() {
    if ($GlobalWOInfoDetails.ReturnValue == "0") {
        ShowNotify('Session expired.', 'error', 3000);
        return false;
    }
    var ddlID = $GlobalWOInfoDetails.ddlID;
    var GridCode = $GlobalWOInfoDetails.gridCode;
    var WOID = $GlobalWOInfoDetails.WOID;

    BindWOInformationDetails(WOID, GridCode, ddlID);

    if (GridCode == 'FDD') {
        BindWOInformationDetails(WOID, 'INCNDD', 'IncorpNomineeDirectorDetails');
        BindWOInformationDetails(WOID, 'INCONDSA', 'INCONDSA');
        BindWOInformationDetails(WOID, 'INCONSSA', 'INCONSSA');

        // $("#divMaintainPartial").load('_WoInCorpDetails');
    }
    if (GridCode == 'CSD') {
        BindWOInformationDetails(WOID, 'INCNSD', 'NomineeSecretaryDetails');
    }
    ShowNotify('Success.', 'success', 2000);
}

function DeleteWOInfoDetails(val1, WoID, gridCode, ddlID) {

    val1 = val1.replace(/,$/g, '');
    var jsonText = JSON.stringify('{' + val1 + '}');

    $("#dialog-confirm").removeClass('hide').dialog({
        resizable: false,
        modal: true,
        title_html: true,
        buttons: [
            {
                html: "<i class='ace-icon fa fa-trash-o bigger-110'></i>&nbsp; Delete Item",
                "class": "btn btn-danger btn-xs",
                click: function () {
                    //******Start*********

                    $GlobalWOInfoDetails.ddlID = ddlID;
                    $GlobalWOInfoDetails.gridCode = gridCode;
                    $GlobalWOInfoDetails.WOID = WoID;
                    $.ajax({
                        type: "POST",
                        url: "/PartialContent/DeleteWOInformationDetails",
                        data: '{' + val1 + '}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: false,
                        cache: true,
                        success: function (msg) {
                            $GlobalWOInfoDetails.ReturnValue = msg;
                            FillDeleteCallBack();
                        },
                        error: function (xhr, ajaxOptions, thrownError) {
                            throw new Error(xhr.statusText);
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

function BindWOInformationDetails(WOID, GridCode, ddlID) {
    $.ajax({
        type: "POST",
        url: "/PartialContent/WOInformationDetails",
        data: "{'GridCode':'" + GridCode + "', 'WOID':'" + WOID + "'}",
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

                if (msg.length <= 2) {
                    $('#Output_' + ddlID).html('');
                    return false;
                }

                var result = jQuery.parseJSON(msg);
                var cols = new Array();
                var p = result.Table[0];
                for (var key in p) {
                    cols.push(key);
                }
                //*******Binding Dynamic tables*********                
                var obj = result;
                var table = $('<table class="table table-striped table-bordered table-hover" style="background-color: white !important;"></table>');
                var thbody = $('<thead></thead>');

                var th = $('<tr></tr>');
                for (var i = 0; i <= cols.length; i++) {
                    if (i == cols.length) {
                        th.append('<th>Delete</th>');
                    }
                    else {
                        if (cols[i] == 'WOID' || cols[i] == 'InfoCode' || cols[i] == 'PersonID' || cols[i] == 'PersonSource') {
                            th.append('<th style="display:none;>' + cols[i] + '</th>');
                        }
                        else
                            th.append('<th>' + cols[i] + '</th>');
                    }
                }
                table.append(thbody.append(th));

                for (var j = 0; j < obj.Table.length; j++) {
                    var player = obj.Table[j];
                    var tr = $('<tr></tr>');
                    var DeleteParams = '';
                    for (var k = 0; k <= cols.length; k++) {
                        var Ids = '';

                        if (k == cols.length) {
                            var d = '<td style="text-align:center"><a class="red btnWOClose" href="javascript:void(0)"';
                            d += ' onclick=DeleteWOInfoDetails("' + DeleteParams + '","' + player['WOID'] + '","' + player['InfoCode'] + '","' + ddlID + '"); >';
                            d += '<i class=" ace-icon fa fa-trash-o bigger-130"></i></a></td>';
                            tr.append(d);
                        }
                        else {
                            var columnName = cols[k];

                            if (columnName.toUpperCase() == 'WOID' || columnName.toUpperCase() == 'INFOCODE' || columnName.toUpperCase() == 'PERSONID' || columnName.toUpperCase() == 'PERSONSOURCE') {
                                tr.append('<td style="display:none;">' + player[columnName] + '</td>');
                                DeleteParams = DeleteParams + '\'' + columnName + '\'' + ':' + '\'' + player[columnName] + '\'' + ',';

                            }
                            else
                                tr.append('<td>' + player[columnName] + '</td>');

                        }
                    }
                    table.append(tr);
                }
                //***************End Binding Dynamic Tables*******************

                $('#Output_' + ddlID).html('');
                $('#Output_' + ddlID).append(table);

                if ($('#hdnWOCloseStatus').val() == 'hide') {
                    $('.btnWOClose').hide();
                }
            }

        },
        error: function (xhr, ajaxOptions, thrownError) {
            throw new Error(xhr.statusText);
        }
    });


}

function GetAddressDetails(personid, sourcecode, DynamicId) {
    $GlobalDirectorAddressDetails.DynamicID = DynamicId;
    CallForDirectorAddressDetails("/PartialContent/GetDirectorAddressDetails", "", "", "{'PersonId':" + personid + ",'SourceCode':'" + sourcecode + "'}", DirectorAddressDetailsCallBack);
}

function DirectorAddressDetailsCallBack() {
    var DynamicID = $GlobalDirectorAddressDetails.DynamicID;
    if ($GlobalDirectorAddressDetails.data.length > 0) {
        $('#txtDirectorEmail' + DynamicID).val($GlobalDirectorAddressDetails.data[0].Email);
        $('#txtDirectorContactNo' + DynamicID).val($GlobalDirectorAddressDetails.data[0].Phone);
        $('#txtDirectorFax' + DynamicID).val($GlobalDirectorAddressDetails.data[0].Fax);
    }
    else {
        $('#txtDirectorEmail' + DynamicID).val("");
        $('#txtDirectorContactNo' + DynamicID).val("");
        $('#txtDirectorFax' + DynamicID).val("");
    }
}

function CallForDirectorAddressDetails(path, templateId, containerId, parameters, callBack) {
    $.ajax({
        type: "POST",
        url: path,
        data: parameters,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        cache: true,
        success: function (msg) {

            $GlobalDirectorAddressDetails.data = msg;

            if (callBack != undefined && callBack != '')
                callBack();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            throw new Error(xhr.statusText);
        }
    });
}