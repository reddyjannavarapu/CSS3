$(document).ready(function () {

    SetPageAttributes('liMasters', 'Services', 'Available services in the system', 'liServices');

    $GlobalData = {};

    $GlobalData.serviceId = 0;
    $GlobalData.serviceData = '';
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
        var checkCharater = AllowNumbersCharactersOnly($(this).val(), event);
        if (!checkCharater) {
            event.preventDefault();
        }
    });

    //$('#txtDescription').keypress(function (event) {
    //    var checkCharater = AllowNumbersCharactersOnly($(this).val(), event);
    //    if (!checkCharater) {
    //        event.preventDefault();
    //    }
    //});

    function LoadData() {
        CallServices("GetServiceData", 'serviceTemplate', 'trData', "{'searchText':'" + $GlobalData.searchText + "','status':" + $GlobalData.status + ",'startpage':" + $GlobalData.startPage + ",'rowsperpage':" + $GlobalData.resultPerPage + ",'OrderBy':'" + $GlobalData.OrderBy + "'}", true, DataLoadCallBack);
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
                    $('#divPagination').hide();
                    $('#divServiceData').show();
                }
                else {
                    $('#divServiceData').hide();
                    $('#divPagination').show();
                    $('#trData').find('.aDelete').unbind('click');

                    $('#trData').find('.aDelete').click(DeleteService);

                    $('#trData').find('.aEdit').unbind('click');
                    $('#trData').find('.aEdit').click(EditService);

                    $('#trData').find('.aView').unbind('click');
                    $('#trData').find('.aView').click(ViewService);

                    GenerateNumericPaging();
                }
            }

        } catch (e) {
            console.log(e);
        }
    }

    function DeleteService() {
        try {
            RemoveValidateRequiredClass();
            var id = $(this).attr('serviceid');

            $("#dialog-confirm").removeClass('hide').dialog({
                resizable: false,
                modal: true,
                title_html: true,
                buttons: [
                    {
                        html: "<i class='ace-icon fa fa-trash-o bigger-110'></i>&nbsp; Delete Item",
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

            var count = 0;

            count += ValidateRequired(code);
            count += ValidateRequired(description);

            if (count > 0) {
                ShowNotify('Please Enter values for all mandatory fields.', 'error', 3000);
                return false;
            }
            else {
                CallServices("CreateService", '', '', "{'serviceId':" + $GlobalData.serviceId + ",'code':'" + $.trim(code.val()) + "','desc':'" + $.trim(description.val()) + "','status':" + active + "}", true, CreateCallBack);
            }

        } catch (e) {
            console.log(e);
        }

    });

    function EditService() {
        try {
            $GlobalData.serviceId = $(this).attr('serviceid');
            CallServices("GetService", '', '', "{'serviceId':" + $GlobalData.serviceId + "}", true, EditCallBack);
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
                $('#txtCode').val($GlobalData.serviceData.Code);
                $('#txtDescription').val($GlobalData.serviceData.Description);
                if ($GlobalData.serviceData.Status)
                    $('#chkStatus').prop('checked', true);
                else
                    $('#chkStatus').prop('checked', false);
            }
        } catch (e) {
            console.log(e);
        }
    }

    function ViewService() {
        try {
            RemoveValidateRequiredClass();
            $GlobalData.serviceId = $(this).attr('serviceid');
            CallServices("GetService", '', '', "{'serviceId':" + $GlobalData.serviceId + "}", true, ViewServiceCallBack);
        } catch (e) {
            console.log(e);
        }
    }


    function ViewServiceCallBack() {
        try {
            if ($GlobalData.InsertedID == '0') {
                ShowNotify('Invalid Session login again.', 'error', 3000);
                return false;
            }
            else {
                $('#labelCode').text($GlobalData.serviceData.Code);
                $('#labelDescription').text($GlobalData.serviceData.Description);
                if ($GlobalData.serviceData.Status)
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
        try {
            Popup.hide('divServiceInfo');
        } catch (e) {
            console.log(e);
        }
    });

    $('#btnCancel').click(function () {
        try {
            RemoveValidateRequiredClass();
            ClearValues();
        } catch (e) {
            console.log(e);
        }
    });

    function ClearValues() {
        try {
            $('#txtCode').val('');
            $('#txtDescription').val('');
            $('#chkStatus').prop('checked', true);
            $GlobalData.serviceId = 0;
            $('#btnCreate').html('<i class="ace-icon fa fa-check bigger-110"></i> Submit');
            $GlobalData.searchText = '';
            $GlobalData.status = '-1';
        } catch (e) {
            console.log(e);
        }
    }

    function CreateCallBack() {
        try {
            if ($GlobalData.InsertedID == -2) {
                ShowNotify('Invalid Session login again.', 'error', 3000);
                return false;
            }
            else {
                if ($GlobalData.InsertedID <= 0)
                    ShowNotify('Service Info Already exist.', 'error', 3000);
                else if ($GlobalData.serviceId > 0) {
                    ShowNotify('Success.', 'success', 2000);
                    ClearValues();
                }
                else {
                    ShowNotify('Success.', 'success', 2000);
                    ClearValues();
                }
                LoadData();
            }
        } catch (e) {
            console.log(e);
        }
    }


    $('#btnSearch').click(function () {
        try {
            $GlobalData.searchText = $.trim($('#txtSearch').val());
            $GlobalData.status = $('#ddlStatus').val();
            $GlobalData.OrderBy = $("#ddlOrderBy").find("option:Selected").val();
            $GlobalData.resultPerPage = $('#ddlPageSize').val();
            $GlobalData.startPage = 0;
            LoadData();
            ClearValues();
        } catch (e) {
            console.log(e);
        }
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
                try {


                    if (msg == '0') {
                        ShowNotify('Invalid Session login again.', 'error', 3000);
                        return false;
                    }
                    $GlobalData.serviceData = msg;
                    $GlobalData.InsertedID = msg;
                    if (msg.ServiceCount != null && msg.ServiceCount != 'undefined') {

                        $GlobalData.totalRow = msg.ServiceCount;
                    }
                    if (templateId != '' && containerId != '' && msg != '') {

                        if (!clearContent) {
                            $.tmpl($('#' + templateId).html(), msg.ServiceList).appendTo("#" + containerId);
                        }
                        else {
                            $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg.ServiceList));
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