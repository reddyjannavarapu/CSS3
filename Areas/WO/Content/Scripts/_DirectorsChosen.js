
function BindDirectorsChosenDropdowns(UniQueClientID, SearchType, SearchRole, WOID, SearchKeyWord, NatureAppoint) {
    try {
        if (WOID == '') {
            WOID = 0;
        }
        CallWOAGMDetailsForDirectors("/PartialContent/GetDirectorDetails", 'DirectorDetailsTemplate', UniQueClientID, "{'SearchType':'" + SearchType + "','SearchKeyWord':'" + SearchKeyWord + "','SearchRole':'" + SearchRole + "','WOID':" + WOID + ",'NatureAppoint':'" + NatureAppoint + "','ClassOfShare':" + -1 + "}", false, BindDirectorsChosenDropdownsCallBack);
    } catch (e) {
        console.log(e);
    }
}

function BindDirectorsChosenDropdownsCallBack(containerId) {
    try {
        $("#" + containerId).chosen({ allow_single_deselect: true });

        var AutoSearch = $('#' + containerId).attr('autoSearch');
        if (AutoSearch == 1) {
            //$('#' + containerId + '_chosen').removeClass('txtAuto');
            $('#' + containerId + '_chosen').find('.txtAuto').attr('ChosenID', containerId);
            //var searchType = $('#' + containerId).attr('searchType');
            //$('#' + containerId + '_chosen').find('.txtAuto').attr('searchType', searchType);
            $('#' + containerId + '_chosen').find('.txtAuto').unbind('keyup');
            $('#' + containerId + '_chosen').find('.txtAuto').keyup(DirectorChosenChange);
        }
    } catch (e) {
        console.log(e);
    }
}

function DirectorChosenChange(e) {
    try {
        var SearchKeyWord = $(this).val();
        var SearchKeyWordLength = SearchKeyWord.length;
        if (SearchKeyWord == undefined) {
            SearchKeyWord = '';
        }

        var SearchKeyWord = SearchKeyWord.escapeSpecialChars();

        var validChar = CheckCharacterForNumberandCharacter(e.keyCode);

        if ((SearchKeyWordLength >= 3 || SearchKeyWord == '') && (validChar == true || e.keyCode == 46)) {
            if (e.keyCode == 13) {
                var ChosenID = $(this).attr('ChosenID');
                var searchType = $('#' + ChosenID).attr('searchType');
                var WOID = $('#' + ChosenID).attr('woid');
                var SearchRole = $('#' + ChosenID).attr('searchrole');
                var NatureAppoint = $('#' + ChosenID).attr('NatureAppoint');

                $("#" + ChosenID + " > option:not(:eq(0))").remove();
                $('#' + ChosenID + '_chosen').find('.txtAuto').unbind('keyup', DirectorChosenChange);

                BindDirectorsChosenDropdowns(ChosenID, searchType, SearchRole, WOID, SearchKeyWord, NatureAppoint);

                $("#" + ChosenID).trigger("chosen:updated");
                $(this).val(SearchKeyWord);
            }
        }
    } catch (e) {
        console.log(e);
    }
}

function CallWOAGMDetailsForDirectors(path, templateId, containerId, parameters, clearContent, callBack) {
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
                            $.tmpl($('#' + templateId).html(), msg.DirectorsList).appendTo("#" + containerId);
                        }
                        else {
                            $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg.DirectorsList));
                        }
                    }

                    if (callBack != undefined && callBack != '')
                        callBack(containerId);

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