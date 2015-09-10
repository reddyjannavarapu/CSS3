$(document).ready(function () {

    $('.btnIndividual').unbind('click').click(function () {
        try {
            $('.modalIndividual').modal({
                "backdrop": "static",
                "show": "true"
            });
        } catch (e) {
            console.log(e);
        }
    });

    $('.btnCompany').unbind('click').click(function () {
        try {
            $('.modalCompany').modal({
                "backdrop": "static",
                "show": "true"
            });
        } catch (e) {
            console.log(e);
        }

    });

});

function BindUsersList(UniQueClientID) {
    try {
        var ddlID = "ddlUser_" + UniQueClientID;
        CallServices("/PartialContent/GetUsersList", "scriptUser", ddlID, "{'UserType':'" + $('#' + ddlID).attr("UserType") + "','selectedId':'" + $('#' + ddlID).attr("selectedId") + "'}", false, DataCallBack);
    } catch (e) {
        console.log(e);
    }
}

function DataCallBack(containerId) {
    try {
        var selectedId = $("#" + containerId).attr("selectedId");
        $("#" + containerId + " option[value=" + selectedId + "]").attr("selected", "selected");
        $("#" + containerId).chosen({ allow_single_deselect: true });
        //resize the chosen on window resize
        //$(window).on('resize.chosen', function () {
        //    var w = $("#" + containerId).parent().width();
        //    $("#" + containerId).next().css({ 'width': w });

        //}).trigger('resize.chosen');
    } catch (e) {
        console.log(e);
    }
}

function CallServices(path, templateId, containerId, parameters, clearContent, callBack) {
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
                    if (templateId != '' && containerId != '') {

                        if (!clearContent) {
                            $.tmpl($('#' + templateId).html(), msg.UserList).appendTo("#" + containerId);
                        }
                        else {
                            $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg.UserList));
                        }
                        if (callBack != undefined && callBack != '')
                            callBack(containerId);
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