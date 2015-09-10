$(document).ready(function () {

    $("#divMainTemp").hide();
    $IsTemplateInserted = '';
    $WOID = '';
    $WOTYPE = '';

    //BindTemplates(); 

    $("#btnLoadDefault").click(function () {
        BindTemplates();
    });

    $("#btnTemplateUpdate").click(function () {

        var TFiles = new Array();
        var IsTemlatedCkd = 0;
        var IsSectionCkd = 0;
        var count = 0;
        var Doctype = $("#ddlDocType").val();

        $(".chkSection").each(function () {
            if ($(this).is(':checked')) {
                IsTemlatedCkd++;
                if ($(this).is(':checked')) {
                    IsSectionCkd++;
                    template = {};
                    //template.WOID = $(this).attr("WOID");
                    //template.SetID = $(this).attr("SetID");
                    template.FID = $(this).attr("FileID");
                    //template.FileFullName = $(this).attr("FileFullName");
                    //template.FilePath = $(this).attr("FilePath");
                    //template.DocType = Doctype;
                    TFiles[count] = template;
                    count++;
                }
            }
        });

        if (IsTemlatedCkd == 0 || IsSectionCkd == 0) {

            ShowNotify('Please select at least one file.', 'error', 3000);
            return false;
        }

        var WOID = $WOID;
        var jsonText = JSON.stringify({ TFiles: TFiles, WOID: $WOID, WOTYPE: $WOTYPE });
        window.open("/TM/TemplateMapping/Documents?files=" + jsonText);
        //CallWoTemplate("/TemplateMapping/InsertWOTemplateDetails", "", "", jsonText, true, CallBackTemplate);
    });

    function CallBackTemplate() {

        if ($IsTemplateInserted == 0) {
            ShowNotify('Invalid Session login again.', 'error', 3000);
            return false;
        }
        else {
            if ($IsTemplateInserted == -2)
                ShowNotify('Template Not Generated successfully.', 'error', 3000);
            else {
                ShowNotify('Success.', 'success', 2000);
            }
        }

    }

});
function BindTemplates() {
    $WOTYPE = $GlobalDataWOTYPE.Type;
    $WOID = $GlobalDataWOTYPE.WOID;
    if ($WOID == '' || $WOTYPE == '')
        $("#divMainTemp").hide();
    else {

        $("#divTemplates").empty();
        if (isNaN($WOTYPE) && !isNaN($WOID))
            CallWoTemplate("/TemplateMapping/BindTemplateMapping", "scriptMainTemplate", "divTemplates", "{'WOID':'" + $WOID + "','WOTYPE':'" + $WOTYPE + "'}", true, CallbackBindTemplates);
    }
}


function CallbackBindTemplates() {
    if ($IsTemplateInserted == 0)
        $("#divMainTemp").hide();
    else
        $("#divMainTemp").show();



    $('#divTemplates').find('.chkTemplate').each(function () {

        $(this).closest('tr').next().show();
    });

    $('#divTemplates').find('.btnOpenClose').unbind('click');
    $('#divTemplates').find('.btnOpenClose').click(TemplateCheck);


    $('#divTemplates').find('.chkTemplate').unbind('change');
    $('#divTemplates').find('.chkTemplate').change(SelectionAll);

    $('#divTemplates').find('.chkSection').unbind('click').click(function () {
        var RowIndex = $(this).closest('.sections').parent().parent().index();
        var chkCheckedCount = $(this).closest('.sections').find('input.chkSection:checkbox:checked').length;
        var chkTotalCount = $(this).closest('.sections').find('.ChkStatus').length;
        if (chkCheckedCount != chkTotalCount) {
            $(this).closest('.sections').parent().parent().prev().find('.chkTemplate').prop('checked', false);
        }
        else {
            $(this).closest('.sections').parent().parent().prev().find('.chkTemplate').prop('checked', true);
        }
    });

    $('.sections').each(function () {
        var checked = $(this).find('input.chkSection:checkbox:checked').length;
        var chkAll = $(this).find('.chkSection').length;
        if (checked == chkAll) {
            $(this).parent().parent().prev().find('.chkTemplate').prop('checked', true);
        }
        else {
            $(this).parent().parent().prev().find('.chkTemplate').prop('checked', false);
        }
    });

}


function SelectionAll() {
    var IsCheckHeaderDoc = $(this).prop("checked");
    $(this).closest("tr").next("tr").find(".sections").find(".ace").each(function (e) {
        if (IsCheckHeaderDoc == true)
            $(this).prop("checked", true);
        else $(this).prop("checked", false);
    });
}
function TemplateCheck() {

    var index = $(this).closest('tr').index();
    if ($(this).hasClass("glyphicon-plus")) {
        $(this).closest('tr').next().show();
        $(this).removeClass('glyphicon-plus').addClass('glyphicon-minus');

    }
    else {
        $(this).closest('tr').next().hide();
        $(this).removeClass('glyphicon-minus').addClass('glyphicon-plus');
    }
}

function CallWoTemplate(path, templateId, containerId, parameters, clearContent, callBack) {

    $.ajax({
        type: "POST",
        url: path,
        data: parameters,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        cache: true,
        success: function (msg) {
            if (msg != 0) {
                $IsTemplateInserted = msg;
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
        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {
            //throw new Error(xhr.statusText);
        }
    });
}