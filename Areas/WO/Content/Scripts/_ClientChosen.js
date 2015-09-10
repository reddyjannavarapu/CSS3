$GlobalData = {};

function BindClientDropDown(UniQueClientID, ClientName, WOID) {
    try {        
        var ddlID = "ddlChosenClient_" + UniQueClientID;
        var clientType = $('#' + ddlID).attr("clientType");
        
        if (clientType == 'BillingParty') 
            BillingPartyCallServices("/PartialContent/GetChosenClientInfo", "scriptChosenClient", ddlID, "{'clientType':'" + clientType + "','ClientName':'" + ClientName + "','WOID':'" + WOID + "'}", false, BillingPartyCallBack);
        else
          ClientCallServices("/PartialContent/GetChosenClientInfo", "scriptChosenClient", ddlID, "{'clientType':'" + clientType + "','ClientName':'" + ClientName + "','WOID':'" + WOID + "'}", false, DataCallBack);

    } catch (e) {
        console.log(e);
    }
}
function DataCallBack(containerId) {    
    try {
         if ($GlobalData.ClientName != '') {
            var UniqueClient = $("#" + containerId + " option[clientcode=" + $GlobalData.ClientCode + "]option[sourceid=" + $GlobalData.SourceID + "]").attr("value");
            $("#" + containerId).val(UniqueClient);
        }
        $("#" + containerId).chosen({ allow_single_deselect: true });
        //resize the chosen on window resize
        //$(window).on('resize.chosen', function () {
        //    var w = $("#" + containerId).parent().width();
        //    $("#" + containerId).next().css({ 'width': w });
        //}).trigger('resize.chosen');

        var maintainFocusVal = $("#" + containerId + '_chosen').find(".txtAuto").val();
        $("#" + containerId + '_chosen').find(".txtAuto").keyup(GetClientDetailsBySearch);
        $("#" + containerId + '_chosen').find(".txtAuto").attr("Selector", containerId);
        $("#" + containerId).trigger("chosen:updated");
        $("#" + containerId + '_chosen').find(".txtAuto").val(maintainFocusVal);
    } catch (e) {
        console.log(e);
    }
}

function BillingPartyCallBack(containerId) {    
    try {        
        if ($GlobalBillingPartyData.BillingPartyID != '' && $GlobalBillingPartyData.BillingPartySourceID != '') {
            var UniqueClient = $("#" + containerId + " option[clientcode=" + $GlobalBillingPartyData.BillingPartyID + "]option[sourceid=" + $GlobalBillingPartyData.BillingPartySourceID + "]").attr("value");
            $("#" + containerId).val(UniqueClient);
        }

        $("#" + containerId).chosen({ allow_single_deselect: true });
        //resize the chosen on window resize
        //$(window).on('resize.chosen', function () {
        //    var w = $("#" + containerId).parent().width();
        //    $("#" + containerId).next().css({ 'width': w });

        //}).trigger('resize.chosen');

        var maintainFocusVal = $("#" + containerId + '_chosen').find(".txtAuto").val();
        $("#" + containerId + '_chosen').find(".txtAuto").keyup(GetClientDetailsBySearch);
        $("#" + containerId + '_chosen').find(".txtAuto").attr("Selector", containerId);
        $("#" + containerId).trigger("chosen:updated");
        $("#" + containerId + '_chosen').find(".txtAuto").val(maintainFocusVal);
    } catch (e) {
        console.log(e);
    }
}

function GetClientDetailsBySearch(e) {

    try {
        var ClientSearch = $.trim($(this).val()).length;
        var ClientName = $(this).val();
        var ClientName = ClientName.escapeSpecialChars();
        $(this).val(ClientName);

        var validChar = CheckCharacterForNumberandCharacter(e.keyCode);
            
        if ((ClientSearch >= 3 || ClientName == '') && (validChar == true || e.keyCode == 46)) {
            if (e.keyCode === 13) {
                var ddlID = $(this).attr("selector");
                var woid = $('#' + ddlID).attr("woid");

                var clientType = $('#' + ddlID).attr("clientType");
                $("#" + ddlID + " > option:not(:eq(0))").remove();
                $('#' + ddlID + '_chosen').find('.txtAuto').unbind('keyup', GetClientDetailsBySearch);

                var clientType = $('#' + ddlID).attr("clientType");
                if (clientType == 'BillingParty')
                    BillingPartyCallServices("/PartialContent/GetChosenClientInfo", "scriptChosenClient", ddlID, "{'clientType':'" + clientType + "','ClientName':'" + ClientName + "','WOID':'" + woid + "'}", false, BillingPartyCallBack);
                else
                    ClientCallServices("/PartialContent/GetChosenClientInfo", "scriptChosenClient", ddlID, "{'clientType':'" + clientType + "','ClientName':'" + ClientName + "','WOID':'" + woid + "'}", false, DataCallBack);

                return false;
            }
        }

    } catch (e) {
        console.log(e);
    }
}


function ClientCallServices(path, templateId, containerId, parameters, clearContent, callBack) {
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
                if (templateId != '' && containerId != '') {

                    if (!clearContent) {
                        $.tmpl($('#' + templateId).html(), msg._ChoosenClientList).appendTo("#" + containerId);
                    }
                    else {
                        $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg._ChoosenClientList));
                    }
                    if (callBack != undefined && callBack != '')
                        callBack(containerId);
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

function BillingPartyCallServices(path, templateId, containerId, parameters, clearContent, callBack) {
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
                
                
                if (msg._ChoosenBillingPartyList.length > 0 ) {
                    $GlobalBillingPartyData.BillingPartyID = msg._ChoosenBillingPartyList[0].BillingPartyID;
                    $GlobalBillingPartyData.BillingPartySourceID = msg._ChoosenBillingPartyList[0].BillingPartySource;
                }

                if (templateId != '' && containerId != '') {                                       
                    if (!clearContent) {
                        $.tmpl($('#' + templateId).html(), msg._ChoosenBillingPartyList).appendTo("#" + containerId);
                    }
                    else {
                       
                        $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg._ChoosenBillingPartyList));
                    }
                    if (callBack != undefined && callBack != '')
                        callBack(containerId);
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
