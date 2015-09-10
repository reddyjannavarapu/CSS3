$GlobalData = {};
function BindGroupDropDown(UniQueClientID) {
    var ddlID = "ddlGroupCode_" + UniQueClientID;
    GroupInfoCallServices("/PartialContent/GetGroupInfo", "scriptGroup", ddlID, "{'clientType':'" + $('#' + ddlID).attr("clientType") + "','selectedId':'" + $('#' + ddlID).attr("selectedId") + "'}", false, DataGroupCallBack);
}

function DataGroupCallBack(containerId) {
    
    $("#" + containerId + " option[value=" + $GlobalData.GroupCode + "]").attr("selected", "selected");
    $("#" + containerId).chosen({ allow_single_deselect: true });
    ////resize the chosen on window resize
    //$(window).on('resize.chosen', function () {
    //    var w = $("#" + containerId).parent().width();
    //    $("#" + containerId).next().css({ 'width': w });
    //}).trigger('resize.chosen');
}

function GroupInfoCallServices(path, templateId, containerId, parameters, clearContent, callBack) {
    ShowLoadNotify();
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
                    $.tmpl($('#' + templateId).html(), msg.GroupInfoList).appendTo("#" + containerId);
                }
                else {
                    $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg.GroupInfoList));
                }
            }

            if (callBack != undefined && callBack != '')
                callBack(containerId);

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {
            //throw new Error(xhr.statusText);
        }
    });

    HideLoadNotify()
}

