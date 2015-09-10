

function CallMasterData(path, templateId, containerId, parameters, clearContent, callBack) {
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
    HideLoadNotify();
}