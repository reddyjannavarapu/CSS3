
$(document).ready(function () {

    SetPageAttributes('liMasters', 'WO Types', 'Available WOTypes in the system for Category A', 'liWoTypes');

    $GlobalData = {};

    $GlobalData.wotypeId = 0;
    $GlobalData.wotypeData = '';
    $GlobalData.searchText = '';
    $GlobalData.status = '-1';
    $GlobalData.startPage = 0;
    $GlobalData.resultPerPage = 10;
    $GlobalData.totalRow = 10;
    $GlobalData.InsertedID = '';
    $GlobalData.OrderBy = '';

    LoadWOTypeData();

    var txtareaDescription = $('#txtWODescription');
    textareaLimiter(txtareaDescription, 500);

    function LoadWOTypeData() {
        try {
            CallServices("GetWOTypeData", 'WOTypeTemplate', 'trData', "{'searchText':'" + escape($.trim($GlobalData.searchText)) + "','status':" + $GlobalData.status + ",'startpage':" + $GlobalData.startPage + ",'rowsperpage':" + $GlobalData.resultPerPage + ",'OrderBy':'" + $GlobalData.OrderBy + "'}", true, WOTypeDataLoadCallBack);
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
                        if (msg == '0') {
                            ShowNotify('Invalid Session login again.', 'error', 3000);
                            return false;
                        }
                        $GlobalData.wotypeData = msg;
                        $GlobalData.InsertedID = msg;
                        if (msg.wotypeCount != null && msg.wotypeCount != 'undefined') {

                            $GlobalData.totalRow = msg.wotypeCount;
                        }
                        if (templateId != '' && containerId != '' && msg != '') {

                            if (!clearContent) {
                                $.tmpl($('#' + templateId).html(), msg.WOTypeList).appendTo("#" + containerId);
                            }
                            else {
                                $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg.WOTypeList));
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

        } catch (e) {
            console.log(e);
        }
    }

    function WOTypeDataLoadCallBack() {
        try {
            if ($GlobalData.InsertedID == '0') {
                ShowNotify('Invalid Session login again.', 'error', 3000);
                return false;
            }
            else {
                var trLength = $('#trData').find('tr').length;
                if (trLength == 0) {
                    $('#divPagination').hide();
                    $('#divWTypeData').show();
                }
                else {
                    $('#divWTypeData').hide();
                    $('#divPagination').show();
                    //$('#trData').find('.aDelete').unbind('click');
                    //$('#trData').find('.aDelete').click(DeleteWOType);

                    $('#trData').find('.aEdit').unbind('click');
                    $('#trData').find('.aEdit').click(EditWOTypeService);

                    $('#trData').find('.aView').unbind('click');
                    $('#trData').find('.aView').click(ViewWOType);

                    GenerateNumericPaging();
                }
            }

        } catch (e) {
            console.log(e);
        }
    }

    $('#btnWOTypeSearch').click(function () {
        try {
            $GlobalData.searchText = $.trim($('#txtWOTypeSearch').val());
            $GlobalData.status = $('#ddlWOTypeStatus').val();
            $GlobalData.OrderBy = $("#ddlWOTypeOrderBy").find("option:Selected").val();
            $GlobalData.resultPerPage = $('#ddlWOTypePageSize').val();
            $GlobalData.startPage = 0;
            LoadWOTypeData();
            ClearValues();
        } catch (e) {
            console.log(e);
        }
    });


    function ViewWOType() {
        try {
            RemoveValidateRequiredClass();
            $GlobalData.wotypeId = $(this).attr('wotypeId');
            CallServices("GetWOType", '', '', "{'WOTypeid':" + $GlobalData.wotypeId + "}", true, ViewWOTypeCallBack);
        } catch (e) {
            console.log(e);
        }
    }



    $('#btnWOCancel').click(function () {
        try {
            RemoveValidateRequiredClass();
            ClearValues();
        } catch (e) {
            console.log(e);
        }
    });


    function ViewWOTypeCallBack() {
        try {
            if ($GlobalData.InsertedID == '0') {
                ShowNotify('Invalid Session login again.', 'error', 3000);
                return false;
            }
            else {
                $('#labelCode').text($GlobalData.wotypeData.WOCode);
                $('#labelWO').text($GlobalData.wotypeData.WOName);
                $('#labelDescription').text($GlobalData.wotypeData.WODescription);
                if ($GlobalData.wotypeData.Status)
                    $('#labelStatus').text('Active');
                else
                    $('#labelStatus').text('In-Active');

                $('#modal-form').modal('show');

                ClearValues();
            }

        } catch (e) {
            console.log(e);
        }
    }

    function GenerateNumericPaging() {
        try {
            var numericcontainer = $('#numericcontainer');
            setListCount($GlobalData.totalRow)
            var pagesize = parseInt($GlobalData.resultPerPage);
            var total = parseInt($GlobalData.totalRow);
            var start = parseInt($GlobalData.startPage);

            var currentpagenumber = Math.ceil((start / pagesize));
            var startpagenumber = currentpagenumber - currentpagenumber % 10;
            var numberofpages = Math.ceil((total - startpagenumber * pagesize) / pagesize);


            numberofpages = numberofpages > 10 ? 10 : (numberofpages == 0 ? 1 : numberofpages);
            var paginghtml = '';

            if (startpagenumber * pagesize <= start && start > 0 && startpagenumber > 0)
                paginghtml = paginghtml + "<li><a id='page" + ((startpagenumber * pagesize) - pagesize) + "' class='mappager numpage'>...</a></li>";
            for (var i = startpagenumber; i < startpagenumber + numberofpages; i++) {
                if (i == currentpagenumber)
                    paginghtml = paginghtml + "<li class='active'><a>" + (i + 1) + "</a></li>";
                else
                    paginghtml = paginghtml + "<li class='normal'><a id='page" + (i * pagesize) + "' class='mappager numpage'>" + (i + 1) + "</a></li>";

            }
            if (((startpagenumber + 10) * pagesize) < total)
                paginghtml = paginghtml + "<a  href='javascript:void(0)' id='page" + (i * pagesize) + "' class='mappager numpage'>...</a>";

            var prevsection = '<li class="mappager first"><a href="javascript:void(0)">« First</a></li><li class="mappager previous"><a href="javascript:void(0)">« Previous</a></li>';
            var nextsection = '<li class="mappager next"><a href="javascript:void(0)">Next »</a></li><li class="mappager last"><a href="javascript:void(0)">Last »</a></li>';
            numericcontainer.find('ul').html(prevsection + paginghtml + nextsection);

            if (currentpagenumber == 0) {
                var _id = $('#numericcontainer ul li.first');
                _id.addClass('firstInactive');
                _id = $('#numericcontainer ul li.previous');
                _id.addClass('previousInactive');

            }
            else {
                var _id = $('#numericcontainer ul li.first');
                _id.removeClass('firstInactive');
                _id = $('#numericcontainer ul li.previous');
                _id.removeClass('previousInactive');
            }
            if ((start + pagesize) >= total) {
                var _id = $('#numericcontainer ul li.last');
                _id.addClass('lastInactive');
                _id = $('#numericcontainer ul li.next');
                _id.addClass('nextInactive');
                _id.removeClass('next');
            }
            else {
                var _id = $('#numericcontainer ul li.last');
                _id.removeClass('lastInactive');
                _id = $('#numericcontainer ul li.next');
                _id.removeClass('nextInactive');
            }

            $('.mappager').click(function () {
                if ($(this).hasClass('numpage'))
                    $GlobalData.startPage = $(this).attr('id').replace('page', '');
                else if ($(this).hasClass('first')) {
                    $GlobalData.startPage = 0;
                }
                else if ($(this).hasClass('next')) {

                    if ($GlobalData.startPage < total)
                        $GlobalData.startPage = parseInt($GlobalData.startPage + $GlobalData.resultPerPage);
                    else
                        return false;

                }
                else if ($(this).hasClass('last')) {
                    var modulovalue = (total % $GlobalData.resultPerPage);
                    $GlobalData.startPage = (modulovalue == '0') ? (total - $GlobalData.resultPerPage) : (total - modulovalue);
                    
                    //$GlobalData.startPage = total - (total % $GlobalData.resultPerPage);

                    if ($(this).hasClass('lastInactive'))
                        return false;
                }
                else if ($(this).hasClass('previous')) {
                    $GlobalData.startPage = $GlobalData.startPage - $GlobalData.resultPerPage;
                    if ($GlobalData.startPage < 0)
                        $GlobalData.startPage = 0;
                }
                if ($(this).hasClass('nextInactive'))
                    return false;
                if ($(this).hasClass('previousInactive'))
                    return false;
                if ($(this).hasClass('firstInactive', 'previousInactive', 'lastInactive', 'nextInactive'))
                    return false;
                LoadWOTypeData();
            });
            $('#txtPageToGo').keypress(function (e) {
                if (e.which == 13) {
                    var page = parseInt($(this).val());
                    var lastpage = total - (total % $GlobalData.resultPerPage);
                    if (page <= lastpage) {
                        $GlobalData.startPage = $GlobalData.resultPerPage * (page - 1);
                        if ($GlobalData.startPage >= total)
                            $GlobalData.startPage = 0;
                        LoadWOTypeData();
                    }
                }
            });

        }
        catch (err) {
            showError('Unable to create paging due to the following error occurred : ' + err.message);
        }
    }



    function DeleteWOType() {
        try {
            RemoveValidateRequiredClass();
            var id = $(this).attr('wotypeId');

            $("#dialog-confirm").removeClass('hide').dialog({
                resizable: false,
                modal: true,
                title_html: true,
                buttons: [
                    {
                        html: "<i class='ace-icon fa fa-trash-o bigger-110'></i>&nbsp; Delete Item",
                        "class": "btn btn-danger btn-xs",
                        click: function () {
                            CallServices("DeleteWOTypeData", '', '', "{'id':" + id + "}", true, DeleteCallBack);
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

        } catch (e) {
            console.log(e);
        }
    }

    function DeleteCallBack() {
        try {
            if ($GlobalData.InsertedID == '0') {
                ShowNotify('Invalid Session login again.', 'error', 3000);
                return false;
            }
            else {
                ShowNotify('Success.', 'success', 2000);
                LoadWOTypeData();
                ClearValues();
            }
        } catch (e) {
            console.log(e);
        }
    }

    function EditWOTypeService() {
        try {
            $GlobalData.wotypeId = $(this).attr('WOTypeid');
            CallServices("GetWOType", '', '', "{'WOTypeid':" + $GlobalData.wotypeId + "}", true, EditWOtypeCallBack);
        } catch (e) {
            console.log(e);
        }
    }

    function EditWOtypeCallBack() {
        try {
            if ($GlobalData.InsertedID == '0') {
                ShowNotify('Invalid Session login again.', 'error', 3000);
                return false;
            }
            else {
                scrollToTop();
                RemoveValidateRequiredClass();
                $('#btnWOCreate').html('<i class="fa fa-edit"></i> Update');
                $('#txtWOCode').val($GlobalData.wotypeData.WOCode);
                $('#txtWOCode').attr('disabled', 'disabled');

                $('#ddlCategory').val($GlobalData.wotypeData.WOCategoryCode);
                $('#txtWO').val($GlobalData.wotypeData.WOName);
                $('#txtWODescription').val($GlobalData.wotypeData.WODescription);
                if ($GlobalData.wotypeData.Status)
                    $('#chkWOStatus').prop('checked', true);
                else
                    $('#chkWOStatus').prop('checked', false);
            }

        } catch (e) {
            console.log(e);
        }
    }

    function ClearValues() {
        try {
            $('#txtWO').val('');
            $('#txtWOCode').val('');
            $('#txtWOCode').removeAttr("disabled");
         //   $('#ddlCategory').val('');
            $('#txtWODescription').val('');
            $('#chkWOStatus').prop('checked', false);
            $GlobalData.wotypeId = 0;
            $('#btnWOCreate').html('<i class="ace-icon fa fa-check bigger-110"></i> SUBMIT');
            $GlobalData.searchText = '';
            $GlobalData.status = '-1';
        } catch (e) {
            console.log(e);
        }
    }

    function scrollToTop() {
        $("html, body").animate({ scrollTop: 0 }, "slow");
        return false;
    }

    $('#btnWOCreate').click(function () {
        try {
            var woCetogery = $("#ddlCategory");
            var woName = $("#txtWO");
            var code = $('#txtWOCode');

            var description = $('#txtWODescription');

            var active = false;
            var isActive = $('#chkWOStatus');
            if (isActive.is(':checked'))
                active = true;

            var woNameValidation = ValidateMaxLength(woName, 200)
            if (woNameValidation == 1) {
                return false;
            }

            var codeMaxLengthValidation = ValidateMaxLength(code, 100);
            if (codeMaxLengthValidation == 1) {
                return false;
            }
            var descMaxLengthValidation = ValidateMaxLength(description, 500);
            if (descMaxLengthValidation == 1) {
                return false;
            }

            code.change(function () { ValidateRequired(code) });
            description.change(function () { ValidateRequired(description) });
            woName.change(function () { ValidateRequired(woName) });


            var count = 0;

            count += ValidateRequired(code);
            count += ValidateRequired(description);
            count += ValidateRequired(woName);
            if (count > 0) {
                ShowNotify('Please Enter values for all mandatory fields.', 'error', 3000);
                return false;
            }
            else {
                CallServices("CreateWOTypeA", '', '', "{'WOTypeId':" + $GlobalData.wotypeId + ",'WOName':'" + escape($.trim(woName.val())) + "','code':'" + $.trim(escape(code.val())) + "','CategoryCode':'" + $.trim(woCetogery.val()) + "','desc':'" + $.trim(escape(description.val())) + "','status':" + active + "}", true, CreateCallBack);
            }

        } catch (e) {
            console.log(e);
        }
    });

    function CreateCallBack() {
        try {
            if ($GlobalData.InsertedID == -2) {
                ShowNotify('Invalid Session login again.', 'error', 3000);
                return false;
            }
            else {
                if ($GlobalData.InsertedID <= 0)
                    ShowNotify('Work order Type is Already exist.', 'error', 3000);
                else if ($GlobalData.wotypeId > 0) {
                    ShowNotify('Success.', 'success', 2000);
                    ClearValues();
                }
                else {
                    ShowNotify('Success.', 'success', 2000);
                    ClearValues();
                }
                LoadWOTypeData();
            }

        } catch (e) {
            console.log(e);
        }
    }

});