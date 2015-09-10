$(document).ready(function () {
    SetPageAttributes('liTemplateMnge', 'Template Management', 'Manage Template', 'liManageTemplate');
    $WOID = '';
    $WOTYPE = '';
    $TemplateEventFlag = '';
    $FieldID = '0';
    $SetID = 0;
    $FileName = '';
    $TemplateResult = '';
    $DeletedFilePath = '';
    $DownLoadedFilePath = '';
   
    //CategoryBind(); // on 12-July-2015, changed to static values 

    function CategoryBind() {
        $('#ddlCategory').find('option:gt(0)').remove();
        CallService("/WO/WODI/GetMWOCategory", "scriptCategory", "ddlCategory", '{}', '.WorkOrdersList', false);
                       
    }

    $('#ddlCategory').change(function () {
        
        $("#divTemplateAdd").hide();      
        $("#divMainTempMgmt").hide();

        $('#ddlDIType').find('option:gt(0)').remove();
        
        var checkCategory = $(this).find("option:selected").val();

        $('#Category').val(checkCategory);

        BindType(checkCategory)

    });

    function BindType(checkCategory)
    {
        if (checkCategory != "-1" && checkCategory != undefined && checkCategory != '') {
            CallService("/WO/WODI/GetMWOTypeByCategoryCode", "DITypeScript", "ddlDIType", "{'CategoryCode':'" + checkCategory + "'}", '.WorkOrdersList', false, enableType);
        }
        else {
            $("#divMainTempMgmt").hide();
            $("#divTemplateAdd").hide();
        }
    }

    function enableType() {
        var length = $('#ddlDIType').find("option").length;
        if (length <= 1)
            $('#ddlDIType').prop("disabled", true);
        else $('#ddlDIType').prop("disabled", false);       
    }

    $(".fileUpload").change(function () {

        var ValidFileExtension = ['DOCX', 'docx', 'DOC', 'doc'];

        if ($.inArray($(this).val().split('.').pop().toLowerCase(), ValidFileExtension) == -1) {

            ShowNotify("Please select .DOCX or .DOC formatted files", 'error', 3000);

            $(".fileUpload").val("");
            return false;
        }
    });

    var msg = message.split('|');
    if (msg[0] == 'FileUploaded') {
        var fName = message.split('|');
        CategoryBind();
       
        BindType(fName[2])

        BindDDlSet(fName[1]);

        BindTemplateDetails(fName[1]);
        BindDDlMDocMultipleEntity(fName[1]);
        $("#divTemplateAdd").show();
        $("#ddlDIType").val(fName[1]);
        $("#ddlCategory").val(fName[2]);
        $("#Type").val(fName[1]);
        $("#Category").val(fName[2]);
    }
    else if (msg[0] == 'FileNotValid' || msg[0] == 'error') {
        $("#modal-form_TemplateDetails").modal({
            "backdrop": "static",
            "show": "true"
        });
    }

    $("#ddlMultiple").change(function () {
        if ($(this).val() == "true")
            $("#divMultipleEntity").show();
        else
            $("#divMultipleEntity").hide();
    });

    $("#ddlTemplateSet").change(function () {
        $("#FileSetPath").val($(this).find('option:selected').attr("filepath"));
    });

    $("#btnTemplateUpload").click(function () {
        
        if ($TemplateEventFlag == "Add") {

            if ($("#FileSetPath").val() == "") {
                ShowNotify("Please select template set.", 'error', 3000);
                return false;
            }

            if ($("#fileupload").val() == "") {
                ShowNotify("Please select file.", 'error', 3000);
                return false;
            }

            if ($.inArray($(this).val()) == "") {
                ShowNotify("Please select file.", 'error', 3000);
                return false;
            }
        }

        if ($("#txtDisplayName").val() == "") {
            ShowNotify("Please enter file display name.", 'error', 3000);
            return false;
        }
        
        if ($("#ddlMultiple").val() == "true") {
            if ($("#ddlMultipleEntity").val() == "") {
                ShowNotify("Please select Multiple Entity.", 'error', 3000);
                return false;
            }
        }


        TemplateDetails = {};

        TemplateDetails.SetID = $("#ddlTemplateSet").val();
        TemplateDetails.DisplayName = $("#txtDisplayName").val();
        TemplateDetails.FilePath = $("#FileSetPath").val();
        TemplateDetails.Description = $("#txtDescription").val();
        TemplateDetails.Status = $("#chkStatus").is(":checked");
        TemplateDetails.IsDefault = $("#chkDefault").is(":checked");
        TemplateDetails.IsMultiple = $("#ddlMultiple").val();
        TemplateDetails.MultipleEntity = $("#ddlMultipleEntity").val();
        TemplateDetails.Flag = $TemplateEventFlag;
        TemplateDetails.FieldID = $FieldID;
        TemplateDetails.FileName = $FileName;

        var jsonText = JSON.stringify({ TemplateDetails: TemplateDetails });

        $("#TemplateDetails").val(jsonText);

        return true;
    });

    $("#btnTemplateAdd").click(function () {

        $TemplateEventFlag = 'Add';

        $("#btnfileDelete").hide();
        $("#btnfileDownload").hide();

        ClearPopup();
        $("#ddlTemplateSet").attr("disabled", false);
        $("#modal-form_TemplateDetails").modal({
            "backdrop": "static",
            "show": "true"
        });
    });

    $("#btnSetAdd").click(function () {
        $("#btnSetUpdate").html('<i class="ace-icon fa fa-check bigger-110"></i>SUBMIT');
        $("#btnSetDelete").hide();
        $SetID = 0;
        $('#txtSetName').val('');
        $('#txtSetDescription').val('')
        $("#chkSetStatus").prop("checked", false);

        //Popup.showModal('SetDetails', null, null, { 'screenColor': '#fff', 'screenOpacity': .6 });

        $("#modal-SetDetails").modal({
            "backdrop": "static",
            "show": "true"
        });

    });

    $("#btnSetUpdate").click(function () {
        
        var setID = $SetID;
        var woCode = $('#ddlDIType').val();
        var setName = $('#txtSetName').val();
        var setStatus = $("#chkSetStatus").is(":checked");
        var description = $('#txtSetDescription').val();

        if (setName == "") {
            ShowNotify("Please enter set name.", 'error', 3000);
            return false;
        }
        
        CallService("/TemplateMapping/InsertSetDetails", "", "", "{'SetID':'" + setID + "','woCode':'" + woCode + "', 'setName':'" + setName + "', 'description':'" + description + "', 'setStatus':'" + setStatus + "'}", '.TemplateList', false, InsertSetCallBack);
        
    });


    $("#btnSetDelete").click(function () {

        var SetID = $SetID;        

        var jsonText = JSON.stringify({ TemplateFiles: TemplateFiles, SetID: SetID });

        if (SetID != 0)
        {
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

                            CallService("/TemplateMapping/DeleteTemplateSet", "", "", jsonText, '.TemplateList', false, CallBackDeleteSet);

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
    });


    function CallBackDeleteSet()
    {
        if ($TemplateResult != 0) {
            
            ShowNotify("Success.", 'success', 2000);
            BindDDlSet($("#Type").val());
            BindTemplateDetails($("#Type").val());
            // BindDDlMDocMultipleEntity($("#ddlDIType").val());           
            $("#divTemplateAdd").show();
            $("#modal-SetDetails").modal("hide");
        }
        else if ($TemplateResult == 0) {
            ShowNotify('Session expired.', 'error', 3000);
            return false;
        }
        else
            ShowNotify("File is Not Deleted", 'error', 3000);
    }


    function InsertSetCallBack() {
        if ($TemplateResult == 1) {
            $SetID = 0;
            $('#modal-SetDetails').modal('hide');
            var woCode = $('#ddlDIType').val();
            BindDDlSet(woCode);
            BindTemplateDetails(woCode);
            ShowNotify("Success.", 'success', 2000);
        }
        else if ($TemplateResult == 0) {
            ShowNotify('Session expired.', 'error', 3000);
            return false;
        }
        else if ($TemplateResult == 2) {
            ShowNotify("Set Name is already exists.", 'error', 3000);
        }
        else 
            ShowNotify("File is Not Deleted", 'error', 3000);     
      
    }

    $("#btnfileDelete").click(function () {
        DeleteDocumentFile();
    });
    
    $("#btnfileDownload").click(function () {
        
        if($DownLoadedFilePath !='')
          window.location.href = $DownLoadedFilePath;
    });
    var TemplateFiles;
    function EditSetDetails() {
        
        TemplateFiles = new Array();
        $("#btnSetDelete").show();
        $("#btnSetUpdate").html('<i class="ace-icon fa fa-check bigger-110"></i>UPDATE');

        $SetID = $(this).attr('setID');
        $('#txtSetName').val('');
        $('#txtSetDescription').val('')
        $("#chkSetStatus").prop("checked", false);

        $('#txtSetName').val($(this).attr('SetName'));
        $('#txtSetDescription').val($(this).attr('setdesc'));        
        $(this).attr("setstatus").toLowerCase() == "true" ? $("#chkSetStatus").prop("checked", true) : $("#chkSetStatus").prop("checked", false);


        $("#modal-SetDetails").modal({
            "backdrop": "static",
            "show": "true"
        });

        
        $(this).closest('tr').next(".trSection").find('.sections').find('.ChkStatus').find(".btnTemplateFile").each(function (index) {            
            TemplateFiles[index] = ($(this).attr("filepath") + "/" + $(this).attr("filefullname"))
        });

    }

    function EditTemplateDetails() {
        
        $DeletedFilePath = $(this).attr("filepath") + "/" + $(this).attr("filefullname");

        $DownLoadedFilePath = $(this).attr("filepath").replace('~', '') + "/" + $(this).attr("filefullname");

        $("#btnfileDelete").show();
        $("#btnfileDownload").show();

        ClearPopup();

        $TemplateEventFlag = 'Edit';
        $FieldID = $(this).attr("FileID");
        $FileName = $(this).attr("filefullname");

        $("#btnTemplateUpload").html('<i class="ace-icon fa fa-check bigger-110"></i>UPDATE');

        $("#ddlTemplateSet").val($(this).attr("SetID"));
        $("#ddlMultipleEntity").val($(this).attr("MultipleEntity"));

        $("#txtDisplayName").val($(this).attr("FileName"));
        $("#txtDescription").val($(this).attr("Description"));

        $(this).attr("TFileStatus").toLowerCase() == "true" ? $("#chkStatus").prop("checked", true) : $("#chkStatus").prop("checked", false);
        $(this).attr("IsDefault").toLowerCase() == "true" ? $("#chkDefault").prop("checked", true) : $("#chkDefault").prop("checked", false);

        $("#FileSetPath").val($(this).attr("FilePath"));

        if ($(this).attr("IsMultiple").toLowerCase() == 'true') {
            $("#ddlMultiple").val('true');
            $("#divMultipleEntity").show();
        }
        else
            $("#ddlMultiple").val('false');

        $("#ddlTemplateSet").attr("disabled", true);

        $("#modal-form_TemplateDetails").modal({
            "backdrop": "static",
            "show": "true"
        });
    }


    function BindDDlMDocMultipleEntity(wocode) {
        $('#ddlMultipleEntity').find('option:gt(0)').remove();
        CallService("/TemplateMapping/BindMDocMultipleEntity", "ddlMultipleEntitySetScript", "ddlMultipleEntity", "{'wocode':'" + wocode + "'}", '', false);
    }

    $("#ddlDIType").change(function () {


        if ($("#ddlDIType").val() == "")
            $("#divTemplateAdd").hide();
        else
            $("#divTemplateAdd").show();

        $("#ddlTemplateSet" + " > option:not(:eq(0))").remove();

        $("#Type").val($(this).val());


        BindDDlSet($(this).val());
        BindDDlMDocMultipleEntity($("#ddlDIType").val());

        BindTemplateDetails($(this).val());
    });

    function BindDDlSet(WOTYPE) {

        $("#ddlTemplateSet").find("option:gt(0)").remove();

        if (WOTYPE == '')
            $("#divMainTempMgmt").hide();
        else {

            $("#divTemplatesMgmt").empty();
            if (isNaN(WOTYPE))
                CallService("/TemplateMapping/BindTemplateSet", "ddlTemplateSetScript", "ddlTemplateSet", "{'WOTYPE':'" + WOTYPE + "'}", '.TemplateList', false);
            else
                $("#divMainTempMgmt").hide();

        }
    }

    function BindTemplateDetails(WOTYPE) {

        if (WOTYPE == '')
            $("#divMainTempMgmt").hide();
        else {

            $("#divTemplatesMgmt").empty();
            if (isNaN(WOTYPE))
                CallService("/TemplateMapping/BindTemplateDetails", "scriptMainTemplateMgmt", "divTemplatesMgmt", "{'WOTYPE':'" + WOTYPE + "'}", '', true, CallbackBindTemplates);
            else
                $("#divMainTempMgmt").hide();
        }
    }

    function CallService(path, templateId, containerId, parameters, listName, clearContent, callBack) {
        $TemplateResult = '';

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
                    $TemplateResult = msg;
                    if (templateId != '' && containerId != '') {

                        if (!clearContent) {
                            $.tmpl($('#' + templateId).html(), eval('msg' + listName)).appendTo("#" + containerId);
                        }
                        else {
                            $("#" + containerId).html($.tmpl($('#' + templateId).html(), eval('msg' + listName)));
                        }
                    }


                }
                if (callBack != undefined && callBack != '')
                    callBack();
            }
            ,
            error: function (xhr, ajaxOptions, thrownError) {
                //throw new Error(xhr.statusText);
            }
        });
    }

    function CallbackBindTemplates() {

        if ($TemplateResult == 0)
            $("#divMainTempMgmt").hide();
        else
            $("#divMainTempMgmt").show();

        $('#divTemplatesMgmt').find('.chkTemplate').each(function () {
            $(this).closest('tr').next().show();
        });

        $('#divTemplatesMgmt').find('.btnOpenClose').unbind('click');
        $('#divTemplatesMgmt').find('.btnOpenClose').click(TemplateCheck);


        $('#divTemplatesMgmt').find('.chkTemplate').unbind('change');
        $('#divTemplatesMgmt').find('.chkTemplate').change(SelectionAll);

        $('#divTemplatesMgmt').find('.sections').find('.btnTemplateFile').unbind('click');
        $('#divTemplatesMgmt').find('.sections').find('.btnTemplateFile').click(EditTemplateDetails);

        $('#divTemplatesMgmt').find('.btnSet').unbind('click');
        $('#divTemplatesMgmt').find('.btnSet').click(EditSetDetails);
        
    }

    function DeleteDocumentFile() {
        
        if ($DeletedFilePath != '' && $FieldID != '') {

            var FilePath = $DeletedFilePath;
            var fileid = $FieldID;

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

                            CallService("/TemplateMapping/DeleteTemplateDoc", "", "", "{'FileId':'" + fileid + "', 'FilePath':'" + FilePath + "'}", '.TemplateList', false, CallBackDeleteDocFile);

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
    }

    function CallBackDeleteDocFile() {
        if ($TemplateResult != 0) {
            ShowNotify("Success.", 'success', 2000);
            BindTemplateDetails($("#Type").val());
            BindDDlMDocMultipleEntity($("#ddlDIType").val());
            $("#divTemplateAdd").show();
            $("#modal-form_TemplateDetails").modal("hide");          
        }
        else if ($TemplateResult == 0) {
            ShowNotify('Session expired.', 'error', 3000);
            return false;
        }
        else
            ShowNotify("File is Not Deleted", 'error', 3000);
    }

    function ClearPopup() {
        $("#btnTemplateUpload").html('<i class="ace-icon fa fa-check bigger-110"></i>SUBMIT')
        $("#ddlTemplateSet").val("");
        $("#txtDisplayName").val("");
        $("#txtDescription").val("");
        $("#chkStatus").prop("checked", false);
        $("#chkDefault").prop("checked", false);
        $("#ddlMultiple").val("false");
        $("#ddlMultipleEntity").val("");
        $("#FileSetPath").val("");
        $("#divMultipleEntity").hide();

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

    function SelectionAll() {
        var IsCheckHeaderDoc = $(this).prop("checked");
        $(this).closest("tr").next("tr").find(".sections").find(".ace").each(function (e) {
            if (IsCheckHeaderDoc == true)
                $(this).prop("checked", true);
            else $(this).prop("checked", false);
        });
    }

});

