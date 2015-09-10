function collapsedShow(widget) {

    if ($('#' + widget).hasClass('collapsed')) {
        $('#' + widget).removeClass('collapsed');
        $('#' + widget).find('.fa-chevron-down').removeClass('fa-chevron-down').addClass('fa-chevron-up');
    }
    else {
        $('#' + widget).addClass('collapsed');
        $('#' + widget).find('.fa-chevron-up').removeClass('fa-chevron-up').addClass('fa-chevron-down');
    }
}

$(document).ready(function () {

    $UnAssignedCount = 0;
    $AssignedCount = 0;
    $DraftCount = 0;
    $DocumentCount = 0;
    $CompletedCount = 0;

    SetPageAttributes('liHome', 'Home', 'Dashboard');
    LoadData();

    function LoadData() {
        SearchWOCallServices("/Home/GetDashbordDetails", "scripttblAssignedWO", "tblAssignedWOBody", "scripttblUnAssignedWO", "tblUnAssignedWOBody",
                             "scripttblDocGeneratedWO", "tblDocGeneratedWOBody", "scripttblCompletedWO", "tblCompletedWOBody", "scripttblDraftWO", "tblDraftWOBody",
                             "", true, callbackLoadData);
        CallBatchService("/WOTypes/GetMBatchType", 'scriptBatchType', 'ddlBatchTypeS', "{}", false, '');
    }

    $("#btnViewGapByBatch").click(function () {
        var BatchType = $("#ddlBatchTypeS").val();
        if (BatchType == '') {
            ShowNotify('Please select Batch Type.', 'error', 3000);
            return false;
        }

        if (BatchType != '' && BatchType != 'undefined')
            CallBatchService("/WOTypes/GetBabBatchGapByBatchType", "scriptGapCabBatchDetails", "trGapBody", "{'BatchType':'" + BatchType + "'}", true, ShowGapAnalysisTable);



    });



    function ShowGapAnalysisTable() {
        try {
            var tblLength = $("#gapTable").find("tr").length;
            if (tblLength <= 1) {
                $("#gapTable").hide();
                $("#dataFound").hide();
                $("#noData").show();
            }
            else {
                $("#gapTable").show();
                $("#dataFound").show();
                $("#noData").hide();
            }
            $('#modal-form1').modal({
                "backdrop": "static",
                "show": "true"
            });

        } catch (e) {
            console.log(e);
        }
    }




    function callbackLoadData() {
        var exists = $('.NotePartialJS').length;
        if (exists > 0) {
            $('.NotePartialJS').remove();
        }
        var headID = document.getElementsByTagName("head")[0];
        var newScript = document.createElement('script');
        newScript.type = 'text/javascript';
        newScript.src = '/Areas/WO/Content/Scripts/_NotePartial.js';
        newScript.className = "NotePartialJS";
        headID.appendChild(newScript);

        $('#tblUnAssignedWOBody').find('.aEdit').unbind('click');
        $('#tblUnAssignedWOBody').find('.aEdit').click(EditDetails);

        $('#tblAssignedWOBody').find('.aEdit').unbind('click');
        $('#tblAssignedWOBody').find('.aEdit').click(EditDetails);

        $('#tblDraftWOBody').find('.aEdit').unbind('click');
        $('#tblDraftWOBody').find('.aEdit').click(EditDetails);

        $('#tblDocGeneratedWOBody').find('.aEdit').unbind('click');
        $('#tblDocGeneratedWOBody').find('.aEdit').click(EditDetails);

        $('#tblCompletedWOBody').find('.aEdit').unbind('click');
        $('#tblCompletedWOBody').find('.aEdit').click(EditDetails);


        $("#UnAssignedCount").text($UnAssignedCount);
        $("#AssignedCount").text($AssignedCount);
        $("#DraftCount").text($DraftCount);
        $("#DocumentCount").text($DocumentCount);
        $("#CompletedCount").text($CompletedCount);
             
        if ($UnAssignedCount == 0)
            $('#divUnAssignedWO').show();
        else
            $('#divUnAssignedWO').hide();
        
        if ($AssignedCount == 0)
            $('#divAssignedWO').show();
        else
            $('#divAssignedWO').hide();

        if ($DraftCount == 0)
            $('#divDraftWO').show();
        else
            $('#divDraftWO').hide();
       
        if ($DocumentCount == 0)
            $('#divDocGeneratedWO').show();
        else
            $('#divDocGeneratedWO').hide();
      
        if ($CompletedCount == 0)
            $('#divCompletedWO').show();
        else
            $('#divCompletedWO').hide();

        //collapsing All Headers
        collapsedShow('widget1');
        collapsedShow('widget');
        collapsedShow('widget4');
        collapsedShow('widget2');
        collapsedShow('widget3');
    }

    function EditDetails() {
        scrollToTop();
        var ID = $(this).attr("ID");
        if (ID != '')
            window.location.href = "/WO/WODI/WorkOrder?ID=" + ID;

        return false;
    }

    function scrollToTop() {
        $("html, body").animate({ scrollTop: 0 }, "slow");
        return false;
    }

    //$('#dvheader').click(function () {
    //    if ($('#widget').hasClass('collapsed')) {
    //        $('#widget').removeClass('collapsed');
    //        $('#widget').find('.fa-chevron-down').removeClass('fa-chevron-down').addClass('fa-chevron-up');
    //    }
    //    else {
    //        $('#widget').addClass('collapsed');
    //        $('#widget').find('.fa-chevron-up').removeClass('fa-chevron-up').addClass('fa-chevron-down');
    //    }
    //});   

    //$('#dvheader1').click(function () {
    //    if ($('#widget1').hasClass('collapsed')) {
    //        $('#widget1').removeClass('collapsed');
    //        $('#widget1').find('.fa-chevron-down').removeClass('fa-chevron-down').addClass('fa-chevron-up');
    //    }            
    //    else {       
    //        $('#widget1').addClass('collapsed');
    //        $('#widget1').find('.fa-chevron-up').removeClass('fa-chevron-up').addClass('fa-chevron-down');
    //    }
    //});

    //$('#dvheader2').click(function () {
    //    if ($('#widget2').hasClass('collapsed')) {
    //        $('#widget2').removeClass('collapsed');
    //        $('#widget2').find('.fa-chevron-down').removeClass('fa-chevron-down').addClass('fa-chevron-up');
    //    }            
    //    else {       
    //        $('#widget2').addClass('collapsed');
    //        $('#widget2').find('.fa-chevron-up').removeClass('fa-chevron-up').addClass('fa-chevron-down');
    //    }
    //});

    //$('#dvheader3').click(function () {
    //    if ($('#widget3').hasClass('collapsed')) {
    //        $('#widget3').removeClass('collapsed');
    //        $('#widget3').find('.fa-chevron-down').removeClass('fa-chevron-down').addClass('fa-chevron-up');
    //    }            
    //    else {       
    //        $('#widget3').addClass('collapsed');
    //        $('#widget3').find('.fa-chevron-up').removeClass('fa-chevron-up').addClass('fa-chevron-down');
    //    }
    //});

    //$('#dvheader4').click(function () {
    //    if ($('#widget4').hasClass('collapsed')) {
    //        $('#widget4').removeClass('collapsed');
    //        $('#widget4').find('.fa-chevron-down').removeClass('fa-chevron-down').addClass('fa-chevron-up');
    //    }            
    //    else {       
    //        $('#widget4').addClass('collapsed');
    //        $('#widget4').find('.fa-chevron-up').removeClass('fa-chevron-up').addClass('fa-chevron-down');
    //    }
    //});

    function SearchWOCallServices(path, templateId, containerId, templateId1, containerId1, templateId2, containerId2,
                                    templateId3, containerId3, templateId4, containerId4, parameters, clearContent, callBack) {
        $.ajax({
            type: "POST",
            url: path,
            data: parameters,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            cache: true,
            success: function (msg) {

                if (msg == '0') {
                    ShowNotify('Invalid Session login again.', 'error', 3000);
                    return false;
                }


                if (templateId != '' && containerId != '') {
                                        
                    if (!clearContent) {
                        $.tmpl($('#' + templateId).html(), msg.WOAssignedList).appendTo("#" + containerId);
                        $.tmpl($('#' + templateId1).html(), msg.WOUnAssignedList).appendTo("#" + containerId1);
                        $.tmpl($('#' + templateId2).html(), msg.WODocGeneratedList).appendTo("#" + containerId2);
                        $.tmpl($('#' + templateId3).html(), msg.WOCompletedList).appendTo("#" + containerId3);
                        $.tmpl($('#' + templateId4).html(), msg.WODraftList).appendTo("#" + containerId4);
                    }
                    else {
                        $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg.WOAssignedList));
                        $("#" + containerId1).html($.tmpl($('#' + templateId1).html(), msg.WOUnAssignedList));
                        $("#" + containerId2).html($.tmpl($('#' + templateId2).html(), msg.WODocGeneratedList));
                        $("#" + containerId3).html($.tmpl($('#' + templateId3).html(), msg.WOCompletedList));
                        $("#" + containerId4).html($.tmpl($('#' + templateId4).html(), msg.WODraftList));
                    }
                                       

                    if (msg.WOUnAssignedList.length > 0)
                        $UnAssignedCount = msg.WOUnAssignedList[0]["RowCount"];
                    if (msg.WOAssignedList.length > 0)
                        $AssignedCount = msg.WOAssignedList[0]["RowCount"];
                    if (msg.WODraftList.length > 0)
                        $DraftCount = msg.WODraftList[0]["RowCount"];
                    if (msg.WODocGeneratedList.length > 0)
                        $DocumentCount = msg.WODocGeneratedList[0]["RowCount"];
                    if (msg.WOCompletedList.length > 0)
                        $CompletedCount = msg.WOCompletedList[0]["RowCount"];                 
                }

                if (callBack != undefined && callBack != '')
                    callBack();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                //throw new Error(xhr.statusText);
            }
        });
    }

    function CallBatchService(path, templateId, containerId, parameters, clearContent, callBack) {
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
                        ShowNotify('Invalid Session login again.', 'error', 3000);
                        return false;
                    }
                   
                    if (templateId != '' && containerId != '' && msg != '') {

                        if (!clearContent) {
                            $.tmpl($('#' + templateId).html(), msg.CABBatchList).appendTo("#" + containerId);
                        }
                        else {
                            $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg.CABBatchList));
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
    }
});