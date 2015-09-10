

$(document).ready(function () {

        SetPageAttributes('liMasters', 'GL Codes', 'To Manage GL Codes', 'liGLCodes');

        $GlobalData = {};

        $GlobalData.GLCodeId = 0;
        $GlobalData.GLCodeData = '';
        $GlobalData.searchText = '';
        $GlobalData.status = '-1';
        $GlobalData.startPage = 0;
        $GlobalData.resultPerPage = 10;
        $GlobalData.totalRow = 10;
        $GlobalData.InsertedID = '';
        $GlobalData.OrderBy = '';

        LoadData();

        var txtareaDescription = $('#txtDescription');
        textareaLimiter(txtareaDescription, 500);

        $('#chkStatus').prop('checked', true);

        $('#txtCode').keypress(function (event) {
            try {
                var checkCharater = AllowNumbersCharactersOnly($(this).val(), event);
                if (!checkCharater) {
                    event.preventDefault();
                }
            } catch (e) {
                console.log(e);
            }
        });

        $('#txtDescription').keypress(function (event) {
            try {
                var checkCharater = AllowNumbersCharactersOnly($(this).val(), event);
                if (!checkCharater) {
                    event.preventDefault();
                }
            } catch (e) {
                console.log(e);
            }
        });

        function LoadData() {
            CallServices("GetGLCodesData", 'GLCodeTemplate', 'trData', "{'searchText':'" + $GlobalData.searchText + "','status':" + $GlobalData.status + ",'startpage':" + $GlobalData.startPage + ",'rowsperpage':" + $GlobalData.resultPerPage + ",'OrderBy':'" + $GlobalData.OrderBy + "'}", true, DataLoadCallBack);
        }

        function DataLoadCallBack() {
            try {
                if ($GlobalData.InsertedID == '0') {
                    ShowNotify('Invalid Session login again.', 'error', 3000);
                    return false;
                }
                else {
                    var trLength = $('#trData').find('tr').length;
                    if (trLength == 0) {
                        $('#divGlCodesData').show();
                        $('#divPagination').hide();
                    }
                    else {
                        $('#divGlCodesData').hide();
                        $('#divPagination').show();

                        $('#trData').find('.aDelete').unbind('click');
                        $('#trData').find('.aDelete').click(DeleteGLCode);

                        $('#trData').find('.aEdit').unbind('click');
                        $('#trData').find('.aEdit').click(EditGLCode);

                        $('#trData').find('.aView').unbind('click');
                        $('#trData').find('.aView').click(ViewGLCode);


                        GenerateNumericPaging();
                    }
                }

            } catch (e) {
                console.log(e);
            }
        }

        function DeleteGLCode() {
            try {
                RemoveValidateRequiredClass();
                var id = $(this).attr('glcodeid');

                $("#dialog-confirm").removeClass('hide').dialog({
                    resizable: false,
                    modal: true,
                    title_html: true,
                    buttons: [
                        {
                            html: "<i class='ace-icon fa fa-trash-o bigger-110'></i>&nbsp; Delete",
                            "class": "btn btn-danger btn-xs",
                            click: function () {
                                CallServices("DeleteData", '', '', "{'id':" + id + "}", true, DeleteCallBack);
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
                    LoadData();
                    ClearValues();
                }

            } catch (e) {
                console.log(e);
            }
        }


        $('#btnCreate').click(function () {
            try {
                var code = $('#txtCode');
                var description = $('#txtDescription');
                var active = false;
                var isActive = $('#chkStatus');
                if (isActive.is(':checked'))
                    active = true;

                var codeMaxLengthValidation = ValidateMaxLength(code, 10);
                if (codeMaxLengthValidation == 1) {
                    return false;
                }
                var descMaxLengthValidation = ValidateMaxLength(description, 100);
                if (descMaxLengthValidation == 1) {
                    return false;
                }

                code.change(function () { ValidateRequired(code) });
                description.change(function () { ValidateRequired(description) });

                var count = 0;
                count += ValidateRequired(code);
                count += ValidateRequired(description);

                if (count > 0) {
                    ShowNotify('Please Enter values for all mandatory fields.', 'error', 3000);
                    return false;
                }
                else {
                    CallServices("CreateGLCode", '', '', "{'GLCodeId':" + $GlobalData.GLCodeId + ",'code':'" + code.val() + "','desc':'" + description.val() + "','status':" + active + "}", true, CreateCallBack);
                }

            } catch (e) {
                console.log(e);
            }
        });

        function EditGLCode() {
            try {
                $GlobalData.GLCodeId = $(this).attr('glcodeid');
                CallServices("GetGLCode", '', '', "{'GLCodeId':" + $GlobalData.GLCodeId + "}", true, EditCallBack);
            } catch (e) {
                console.log(e);
            }
        }

        function EditCallBack() {
            try {
                if ($GlobalData.InsertedID == '0') {
                    ShowNotify('Invalid Session login again.', 'error', 3000);
                    return false;
                }
                else {
                    scrollToTop();
                    RemoveValidateRequiredClass();
                    $('#btnCreate').html('<i class="glyphicon glyphicon-edit"></i> Update');
                    $('#txtCode').val($GlobalData.GLCodeData.Code);
                    $('#txtDescription').val($GlobalData.GLCodeData.Description);
                    if ($GlobalData.GLCodeData.Status)
                        $('#chkStatus').prop('checked', true);
                    else
                        $('#chkStatus').prop('checked', false);
                }

            } catch (e) {
                console.log(e);
            }
        }

        function ViewGLCode() {
            try {
                $GlobalData.GLCodeId = $(this).attr('glcodeid');
                CallServices("GetGLCode", '', '', "{'GLCodeId':" + $GlobalData.GLCodeId + "}", true, ViewGLCodeCallBack);
            } catch (e) {
                console.log(e);
            }
        }


        function ViewGLCodeCallBack() {
            try {
                if ($GlobalData.InsertedID == '0') {
                    ShowNotify('Invalid Session login again.', 'error', 3000);
                    return false;
                }
                else {
                    RemoveValidateRequiredClass();
                    $('#labelCode').text($GlobalData.GLCodeData.Code);
                    $('#labelDescription').text($GlobalData.GLCodeData.Description);
                    if ($GlobalData.GLCodeData.Status)
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

        $('.close-btn').click(function () {
            Popup.hide('divGLCodeInfo');
        });

        $('#btnCancel').click(function () {
            RemoveValidateRequiredClass();
            ClearValues();
        });

        function ClearValues() {
            try {
                $('#txtCode').val('');
                $('#txtDescription').val('');
                $('#chkStatus').prop('checked', true);
                $GlobalData.GLCodeId = 0;
                $('#btnCreate').html('<i class="ace-icon fa fa-check bigger-110"></i> Submit');
                $GlobalData.searchText = '';
                $GlobalData.status = '-1';
            } catch (e) {
                console.log(e);
            }
        }

        function CreateCallBack() {
            if ($GlobalData.InsertedID == -2) {
                ShowNotify('Invalid Session login again.', 'error', 3000);
                return false;
            }
            else {
                if ($GlobalData.InsertedID <= 0)
                    ShowNotify('GLCode Info Already exist.', 'error', 3000);
                else if ($GlobalData.GLCodeId > 0) {
                    ShowNotify('Success.', 'success', 2000);
                    ClearValues();
                }
                else {
                    ShowNotify('Success.', 'success', 2000);
                    ClearValues();
                }
                LoadData();
            }
        }


        $('#btnSearch').click(function () {
            $GlobalData.searchText = $('#txtSearch').val();
            $GlobalData.status = $('#ddlStatus').val();
            $GlobalData.OrderBy = $("#ddlOrderBy").find("option:Selected").val();
            $GlobalData.resultPerPage = $('#ddlPageSize').val();
            $GlobalData.startPage = 0;
            LoadData();
            ClearValues();
        });

        function CallServices(path, templateId, containerId, parameters, clearContent, callBack) {
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
                    $GlobalData.GLCodeData = msg;
                    $GlobalData.InsertedID = msg;
                    if (msg.GLCodeCount != null && msg.GLCodeCount != 'undefined') {

                        $GlobalData.totalRow = msg.GLCodeCount;
                    }
                    if (templateId != '' && containerId != '' && msg != '') {

                        if (!clearContent) {
                            $.tmpl($('#' + templateId).html(), msg.GLCodeList).appendTo("#" + containerId);
                        }
                        else {
                            $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg.GLCodeList));
                        }
                    }


                    if (callBack != undefined && callBack != '')
                        callBack();

                },
                error: function (xhr, ajaxOptions, thrownError) {
                    //throw new Error(xhr.statusText);
                }
            });
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
                            $GlobalData.startPage = parseInt($GlobalData.startPage) + parseInt($GlobalData.resultPerPage);
                        else
                            return false;

                    }
                    else if ($(this).hasClass('last')) {
                        $GlobalData.startPage = total - (total % $GlobalData.resultPerPage);
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
                    LoadData();
                });
                $('#txtPageToGo').keypress(function (e) {
                    if (e.which == 13) {
                        var page = parseInt($(this).val());
                        var lastpage = total - (total % $GlobalData.resultPerPage);
                        if (page <= lastpage) {
                            $GlobalData.startPage = $GlobalData.resultPerPage * (page - 1);
                            if ($GlobalData.startPage >= total)
                                $GlobalData.startPage = 0;
                            LoadData();
                        }
                    }
                });

            }
            catch (err) {
                showError('Unable to create paging due to the following error occurred : ' + err.message);
            }
        }

        function scrollToTop() {
            $("html, body").animate({ scrollTop: 0 }, "slow");
            return false;
        }

});