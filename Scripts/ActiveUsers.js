$(document).ready(function () {
    //  SetPageAttributes('liUsers', 'Users', 'Available users in CSS2', 'liUserAndDetails');
    var NoOfDays = $("#ddlUsersDaysCount").val();

    LoadUsersData(NoOfDays);
    LoadErrorLogData();
    LoadCABErrorInfo();
    LoadAllTables();
    LoadCss1IntegrationErrors();
    LoadScheduleHistory();

    $("#btnSearchUsers").click(function () {
        var NoOfDays = $("#ddlUsersDaysCount").val();
        LoadUsersData(parseInt(NoOfDays));
    });

    $("#TabEvents").show();

    $('#tabActiveUsers').click(function () {
        $('#TabsMenu').find('.active').attr('class', '');
        $(this).parent().attr('class', 'active');
        $('#spnPageHeadder').text('Active Users');
        $('#spnPageDescription').text('Login History');

        $('#dvErrorLog').hide();
        $('#dvActiveUsers').show();
        $('#dvCABErrorInfo').hide();
        $('#dvDownloadLogFiles').hide();
        $('#dvDownloadTableData').hide();
        $('#dvCss1IntegrationErrorInfo').hide();
        $('#dvScheduleHistory').hide();

    });

    $('#tabErrorLog').click(function () {
        $('#TabsMenu').find('.active').attr('class', '');
        $(this).parent().attr('class', 'active');
        $('#spnPageHeadder').text('Error Logs');
        $('#spnPageDescription').text('Errors Information');

        $('#dvErrorLog').show();
        $('#dvActiveUsers').hide();
        $('#dvCABErrorInfo').hide();
        $('#dvDownloadLogFiles').hide();
        $('#dvDownloadTableData').hide();
        $('#dvCss1IntegrationErrorInfo').hide();
        $('#dvScheduleHistory').hide();

    });

    $('#tabCABErrorInfo').click(function () {
        $('#TabsMenu').find('.active').attr('class', '');
        $(this).parent().attr('class', 'active');
        $('#spnPageHeadder').text('CAB Errors');
        $('#spnPageDescription').text('CAB Errors Information');

        $('#dvErrorLog').hide();
        $('#dvActiveUsers').hide();
        $('#dvCABErrorInfo').show();
        $('#dvDownloadLogFiles').hide();
        $('#dvDownloadTableData').hide();
        $('#dvCss1IntegrationErrorInfo').hide();
        $('#dvScheduleHistory').hide();
    });


    $('#tabScheduleHistory').click(function () {
        $('#TabsMenu').find('.active').attr('class', '');
        $(this).parent().attr('class', 'active');
        $('#spnPageHeadder').text('Schedule History');
        $('#spnPageDescription').text('Schedule History');

        $('#dvErrorLog').hide();
        $('#dvActiveUsers').hide();
        $('#dvCABErrorInfo').hide();
        $('#dvDownloadLogFiles').hide();
        $('#dvDownloadTableData').hide();
        $('#dvCss1IntegrationErrorInfo').hide();

        $('#dvScheduleHistory').show();

    });


    $('#tabDownloadLogFiles').click(function () {
        $('#TabsMenu').find('.active').attr('class', '');
        $(this).parent().attr('class', 'active');
        $('#spnPageHeadder').text('Log Files');
        $('#spnPageDescription').text('Download Log Files');

        $('#dvErrorLog').hide();
        $('#dvActiveUsers').hide();
        $('#dvCABErrorInfo').hide();
        $('#dvDownloadLogFiles').show();
        $('#dvDownloadTableData').hide();
        $('#dvCss1IntegrationErrorInfo').hide();
        $('#dvScheduleHistory').hide();
    });

    $('#tabDownloadTableData').click(function () {
        $('#TabsMenu').find('.active').attr('class', '');
        $(this).parent().attr('class', 'active');
        $('#spnPageHeadder').text('Tables Data');
        $('#spnPageDescription').text('Download Table Data');

        $('#dvErrorLog').hide();
        $('#dvActiveUsers').hide();
        $('#dvCABErrorInfo').hide();
        $('#dvDownloadLogFiles').hide();
        $('#dvDownloadTableData').show();
        $('#dvCss1IntegrationErrorInfo').hide();
        $('#dvScheduleHistory').hide();
    });

    $('#btnDownloadTableData').click(function () {
        var Table = $("#ddlTables").find("option:Selected").val();

        if (Table == '-1') {
            ShowNotify('Please select Table Name.', 'error', 3000);
            return false;
        }
    });

    $('#tabCss1IntegrationErrorInfo').click(function () {
        $('#TabsMenu').find('.active').attr('class', '');
        $(this).parent().attr('class', 'active');
        $('#spnPageHeadder').text('Tables Data');
        $('#spnPageDescription').text('Download Table Data');

        $('#dvErrorLog').hide();
        $('#dvActiveUsers').hide();
        $('#dvCABErrorInfo').hide();
        $('#dvDownloadLogFiles').hide();
        $('#dvDownloadTableData').hide();
        $('#dvCss1IntegrationErrorInfo').show();
        $('#dvScheduleHistory').hide();
    });

});

function LoadAllTables() {
    try {
        CallErrorLog("GetAllTableNames", 'scriptTabledata', 'ddlTables', "{}", false, '');
    } catch (e) {
        console.log(e);
    }
}

function LoadUsersData(NoOfDays) {
    try {
        CallActiveUsers("GetActiveUsersByDays", 'scriptActiveUsers', 'bodyActiveUsers', "{'NoOfDays':" + NoOfDays + "}", true, DataLoadCallBack);
    } catch (e) {
        console.log(e);
    }
}

function LoadErrorLogData() {
    try {
        CallErrorLog("GetErrorLogs", 'scriptErrorLogs', 'bodyErrorLogs', "{}", true, '');
    } catch (e) {
        console.log(e);
    }
}

function LoadCABErrorInfo() {
    try {
        CallErrorLog("GetCABErrors", 'scriptCABErrors', 'bodyCABErrors', "{}", true, '');
    } catch (e) {
        console.log(e);
    }
}

function LoadScheduleHistory() {
    try {
        CallScheduleHistory("GetScheduleHistory", 'scriptScheduleHistory', 'bodyScheduleHistory', "{}", true, '');
    } catch (e) {
        console.log(e);
    }
}

function LoadCss1IntegrationErrors() {
    try {
        CallErrorLog("GetAllCss1IntegrationErrors", 'scriptCss1IntegrationErrors', 'bodyCss1IntegrationErrors', "{}", true, '');
    } catch (e) {
        console.log(e);
    }
}


function DataLoadCallBack() {
    try {
        var trLength = $('#bodyActiveUsers').find("tr").length;
        if (trLength >= 1) {
            $("#divSearchNoUserData").hide();
            $("#Divpagination").show();
            //GenerateNumericPaging();
        }
        else {
            $("#divSearchNoUserData").show();
            $("#Divpagination").hide();
        }

    } catch (e) {
        console.log(e);
    }
}
function CallActiveUsers(path, templateId, containerId, parameters, clearContent, callBack) {
    try {
        $.ajax({
            type: "POST",
            url: path,
            data: parameters,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (msg) {

                if (msg == '0') {
                    ShowNotify('Invalid Session login again.', 'error', 3000);
                    return false;
                }
                if (msg.ActiveUsersList != null && msg.ActiveUsersList != 'undefined') {

                    //$GlobalData.totalRow = msg.UserCount;
                }
                if (templateId != '' && containerId != '' && msg != '') {
                    if (!clearContent) {
                        $.tmpl($('#' + templateId).html(), msg.ActiveUsersList).appendTo("#" + containerId);
                    }
                    else {
                        $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg.ActiveUsersList));
                    }
                }

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

function CallErrorLog(path, templateId, containerId, parameters, clearContent, callBack) {
    try {
        $.ajax({
            type: "POST",
            url: path,
            data: parameters,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (msg) {

                if (msg == '0') {
                    ShowNotify('Invalid Session login again.', 'error', 3000);
                    return false;
                }
                if (msg != null && msg != 'undefined') {

                    //$GlobalData.totalRow = msg.UserCount;
                }
                if (templateId != '' && containerId != '' && msg != '') {
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
                //throw new Error(xhr.statusText);
            }
        });

    } catch (e) {
        console.log(e);
    }
}


function CallScheduleHistory(path, templateId, containerId, parameters, clearContent, callBack) {
    try {
        $.ajax({
            type: "POST",
            url: path,
            data: parameters,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (msg) {

                if (msg == '0') {
                    ShowNotify('Invalid Session login again.', 'error', 3000);
                    return false;
                }
                if (msg != null && msg != 'undefined') {

                    //$GlobalData.totalRow = msg.UserCount;
                }
                if (templateId != '' && containerId != '' && msg != '') {
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
                //throw new Error(xhr.statusText);
            }
        });

    } catch (e) {
        console.log(e);
    }
}


function ShowNotify(text, type, duration) {
    //top-warning-message
    var css;
    if (type == 'warning') {
        css = 'top-warning-message', div = document.getElementById('divNotification');
    }
    else {
        css = (type == 'success') ? 'top-success-message' : 'top-error-message', div = document.getElementById('divNotification');
    }
    div.innerHTML = text;
    div.className = css;
    document.getElementById('divNotification').style.display = '';
    document.getElementById('divNotificationParent').style.display = '';
    setTimeout(HideNotify, duration);
}

function HideNotify() {
    document.getElementById('divNotification').style.display = 'none';
}