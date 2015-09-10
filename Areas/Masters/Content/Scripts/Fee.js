
$(document).ready(function () {

    SetPageAttributes('liMasters', 'FEE', 'To Manage Fee', 'liFee');

    $GlobalFeeData = {};

    $GlobalFeeData.FeeID = 0;

    $GlobalFeeData.searchText = '';
    $GlobalFeeData.startPage = 0;
    $GlobalFeeData.resultPerPage = 10;
    $GlobalFeeData.totalRow = 0;
    $GlobalFeeData.InsertedID = '';
    $GlobalFeeData.OrderBy = 'CodeASC';
    $GlobalFeeData.status = '-1';

    LoadData();

    var txtareaDescription = $('#txtareaDescription');
    textareaLimiter(txtareaDescription, 250);

    //$('#chkStatus').prop('checked', true);


    $('#btnSearch').click(function () {
        try {
            $GlobalFeeData.searchText = $.trim($('#txtSearch').val());
            $GlobalFeeData.OrderBy = $("#ddlOrderBy").find("option:Selected").val();
            $GlobalFeeData.status = $("#ddlStatus").find("option:Selected").val();
            $GlobalFeeData.resultPerPage = $('#ddlPageSize').val();
            LoadData();
        } catch (e) {
            console.log(e);
        }
    });


    $('#btnAdd').click(function () {
        try {
            var FeeType = $('#ddlFeeType').val();
            var name = $('#txtName');
            var code = $('#txtCode');
            var ItemNumber = $('#txtItemNumber')
            var ACCPACCode = $('#txtACCPACCode')
            //var description = $('#txtareaDescription');
            var NeedSecurityDeposit = $('#chkNeedSecurityDepositStatus').is(':checked');
            var status = $('#chkStatus').is(':checked');
            var nameMaxLengthValidation = ValidateMaxLength(name, 100);
            if (codeMaxLengthValidation == 1) {
                return false;
            }

            var codeMaxLengthValidation = ValidateMaxLength(code, 20);
            if (codeMaxLengthValidation == 1) {
                return false;
            }

            //var descMaxLengthValidation = ValidateMaxLength(description, 250);
            //if (descMaxLengthValidation == 1) {
            //    return false;
            //}

            name.change(function () { ValidateRequired(name) });
            code.change(function () { ValidateRequired(code) });
            ItemNumber.change(function () { ValidateRequired(ItemNumber) });
            ACCPACCode.change(function () { ValidateRequired(ACCPACCode) });
            //description.change(function () { ValidateRequired(description) });

            var count = 0;
            count += ValidateRequired(name);
            count += ValidateRequired(code);
            count += ValidateRequired(ItemNumber);
            count += ValidateRequired(ACCPACCode);
            //count += ValidateRequired(description);
            if (FeeType == '-1' || FeeType == undefined) {
                count = count + 1;
            }
            var Description = '';
            $('.DescriptionMultiLine').each(function (index, Value) {
                var val = $.trim($(this).val());
                if (val != '') {
                    if (index == 0)
                        Description = val;
                    else
                        Description = Description + '~^' + val;
                }
            });

            if (count > 0 || Description == '') {
                ShowNotify('Please Enter values for all mandatory fields.', 'error', 3000);
                return false;
            }
            else {
                //CallFee("CreateFee", '', '', "{'FeeId':" + $GlobalFeeData.FeeID + ",'code':'" + $.trim(code.val()) + "','name':'" + $.trim(name.val()) + "','ItemNumber':'" + $.trim(ItemNumber.val()) + "','ACCPACCode':'" + $.trim(ACCPACCode.val()) + "','desc':'" + $.trim(escape(Description)) + "','NeedSecurityDeposit':" + NeedSecurityDeposit + ",'status':" + status + ",'FeeType':'" + FeeType + "'}", true, CreateCallBack);

                Fee = {};
                Fee.ID = $GlobalFeeData.FeeID;
                Fee.Code = $.trim(code.val());
                Fee.Name = $.trim(name.val());
                Fee.ItemNumber = $.trim(ItemNumber.val());
                Fee.ACCPACCode = $.trim(ACCPACCode.val());
                Fee.Description = $.trim(Description);
                Fee.NeedSecurityDeposit = NeedSecurityDeposit;
                Fee.Status = status;
                //Fee.CreatedBy = createdBy;
                Fee.FeeType = FeeType;
                var jsonText = JSON.stringify({ Fee: Fee });

                CallFee("CreateFee", '', '', jsonText, true, CreateCallBack);
            }

        } catch (e) {
            console.log(e);
        }
    });


    $('#btnCancel').click(function () {
        try {
            ClearDescriptionToOneTextArea();
            RemoveValidateRequiredClass();
            ClearValues();
        } catch (e) {
            console.log(e);
        }
    });

    $('.close-btn').click(function () {
        try {
            Popup.hide('divFeeInfo');
        } catch (e) {
            console.log(e);
        }
    });

    $('#btnAddDynamicDescription').click(function () {
        try {
            var checkforempty = 0;

            $('.DescriptionMultiLine').each(function () {
                var val = $.trim($(this).val());
                if (val == '') {
                    checkforempty = 1;
                }
            });

            if (checkforempty != 1) {
                //var index = $(this).closest('tr').index();
                //var txtCount = $('.DescriptionMultiLine').length;
                //var eq = index + (txtCount - 1);

                $('#dvAddDynamicDescription').append('<div class="col-sm-11 no-padding-left margin-10t"> <textarea id="txtdesc" maxlength="250" class="col-sm-4 DescriptionMultiLine form-control limited special" style="resize:none;padding-left:4px;"></textarea></div>');
            }

            ApplyMaxLengthForDescription();

        } catch (e) {
            console.log(e);
        }
    });

});

function ApplyMaxLengthForDescription() {
    try {
        var txtareaDescription = $('.DescriptionMultiLine');
        textareaLimiter(txtareaDescription, 250);
    } catch (e) {
        console.log(e);
    }
}

function ClearDescriptionToOneTextArea() {
    try {

        $('.DescriptionMultiLine').each(function (index) {
            if (index > 0) {
                $(this).closest('div').remove();
            }
        });

    } catch (e) {
        console.log(e);
    }
}

function LoadData() {
    CallFee("GetFeeData", 'FeeTemplate', 'trFeeData', "{'searchText':'" + escape($.trim($GlobalFeeData.searchText)) + "','status':" + $GlobalFeeData.status + ",'startpage':" + $GlobalFeeData.startPage + ",'rowsperpage':" + $GlobalFeeData.resultPerPage + ",'OrderBy':'" + $GlobalFeeData.OrderBy + "'}", true, DataLoadCallBack);
}

function DataLoadCallBack() {
    try {
        var trLength = $('#trFeeData').find('tr').length;
        if (trLength == 0) {
            $('#divPagination').hide();
            $('#divFeeData').show();
        }
        else {
            $('#divFeeData').hide();
            $('#divPagination').show();

            $('#trFeeData').find('.aDelete').unbind('click');
            $('#trFeeData').find('.aDelete').click(DeleteFee);

            $('#trFeeData').find('.aEdit').unbind('click');
            $('#trFeeData').find('.aEdit').click(EditFee);

            $('#trFeeData').find('.aView').unbind('click');
            $('#trFeeData').find('.aView').click(ViewFee);

            GenerateNumericPaging();
        }

    } catch (e) {
        console.log(e);
    }
}

function DeleteFee() {
    try {

        RemoveValidateRequiredClass();
        var feeid = $(this).attr('feeid');
        $("#dialog-confirm").removeClass('hide').dialog({
            resizable: false,
            modal: true,
            title_html: true,
            buttons: [
                {
                    html: "<i class='ace-icon fa fa-trash-o bigger-110'></i>&nbsp; Delete",
                    "class": "btn btn-danger btn-xs",
                    click: function () {
                        CallFee("DeleteFee", '', '', "{'FeeId':" + feeid + "}", true, DeleteCallBack);
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
        if ($GlobalFeeData.InsertedID == 1) {
            ShowNotify('Success.', 'success', 2000);
            LoadData();
            ClearValues();
        }
        else if ($GlobalFeeData.InsertedID == '-1') {
            ShowNotify('Fee details are in use.', 'error', 3000);
            return false;
        }
    } catch (e) {
        console.log(e);
    }
}

function EditFee() {
    try {
        RemoveValidateRequiredClass();
        var feeid = $(this).attr('feeid');
        var code = $(this).attr('code');
        var feename = $(this).attr('feename');
        var ItemNumber = $(this).attr('ItemNumber');
        var ACCPACCode = $(this).attr('ACCPACCode');
        var description = $(this).attr('description');
        var status = $(this).attr('status');
        var NeedSecurityDeposit = $(this).attr('NeedSecurityDeposit');
        var FeeType = $(this).attr('FeeType');

        $GlobalFeeData.FeeID = feeid;
        $('#txtCode').val(code);
        $('#txtCode').attr("disabled", true);
        $('#txtItemNumber').val(ItemNumber);
        $('#txtACCPACCode').val(ACCPACCode);
        $('#txtName').val(feename);


        //$('#txtareaDescription').val(description);

        var arrDescription = [];
        arrDescription = description.split('~^');
        var ArrLength = arrDescription.length;

        ClearDescriptionToOneTextArea();

        if (ArrLength > 1) {
            var tableRowValue = 0;
            $.each(arrDescription, function (index, value) {
                if (index >= 1) {
                    if (value != '') {
                        $('#dvAddDynamicDescription').append('<div class="col-sm-11 no-padding-left margin-10t"> <textarea id="txtdesc" maxlength="250" class="col-sm-4 DescriptionMultiLine form-control limited special" style="resize:none;padding-left:4px;">' + value + '</textarea></div>');
                        //tableRowValue++;
                    }
                }
                else {
                    $('#txtareaDescription').val(value);
                }

            });
        }
        else
            $("#txtareaDescription").val(description);

        ApplyMaxLengthForDescription();



        if (NeedSecurityDeposit == 'false') {
            $('#chkNeedSecurityDepositStatus').prop('checked', false);
        }
        else {
            $('#chkNeedSecurityDepositStatus').prop('checked', true);
        }
        if (status == 'false') {
            $('#chkStatus').prop('checked', false);
        }
        else {
            $('#chkStatus').prop('checked', true);
        }
        $('#ddlFeeType option:contains(' + FeeType + ')').attr('selected', true);

        $('#btnAdd').html('<i class="fa fa-edit"></i> Update');
        scrollToTop();
    } catch (e) {
        console.log(e);
    }
}

function ViewFee() {
    try {
        var feeid = $(this).attr('feeid');
        var code = $(this).attr('code');
        var ItemNumber = $(this).attr('ItemNumber');
        var ACCPACCode = $(this).attr('ACCPACCode');
        var feename = $(this).attr('feename');
        var description = $(this).attr('DescriptionWithBreak');
        var NeedSecurityDeposit = $(this).attr('NeedSecurityDeposit');
        var status = $(this).attr('status');

        RemoveValidateRequiredClass();

        $('#labelName').text(feename);
        $('#labelCode').text(code);
        $('#labelItemNumber').text(ItemNumber);
        $('#labelACCPACCode').text(ACCPACCode);
        $('#labelDescription').text(description);

        if (NeedSecurityDeposit == 'true') {
            $('#labelNeedSDeposit').text('Active');
        }
        else {
            $('#labelNeedSDeposit').text('In-Active');
        }

        if (status)
            $('#labelStatus').text('Active');
        else
            $('#labelStatus').text('In-Active');

        $('#modal-form').modal('show');

        ClearValues();

    } catch (e) {
        console.log(e);
    }
}
function CreateCallBack() {
    try {
        if ($GlobalFeeData.InsertedID <= 0)
            ShowNotify('Fee Info Already exists.', 'error', 3000);
        else if ($GlobalFeeData.FeeID > 0) {
            ShowNotify('Success.', 'success', 2000);
            ClearValues();
        }
        else {
            ShowNotify('Success.', 'success', 2000);
            ClearValues();
        }

        LoadData();

    } catch (e) {
        console.log(e);
    }
}
function ClearValues() {
    try {
        $('#txtCode').val('');
        $('#txtareaDescription').val('');
        $('#txtName').val('');
        $('#txtItemNumber').val('');
        $('#txtACCPACCode').val('');
        $('#chkNeedSecurityDepositStatus').prop('checked', false);
        $('#chkStatus').prop('checked', false);
        $('#btnAdd').html('<i class="ace-icon fa fa-check bigger-110"></i> SUBMIT');
        $('#txtCode').attr("disabled", false);
        $('#ddlFeeType').val('-1');
        $GlobalFeeData.FeeID = 0;
        ClearDescriptionToOneTextArea();
    } catch (e) {
        console.log(e);
    }
}
function CallFee(path, templateId, containerId, parameters, clearContent, callBack) {
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
            $GlobalFeeData.InsertedID = msg;
            if (msg.FeeCount != null && msg.FeeCount != 'undefined') {

                $GlobalFeeData.totalRow = msg.FeeCount;
            }
            if (templateId != '' && containerId != '' && msg != '') {

                if (!clearContent) {
                    $.tmpl($('#' + templateId).html(), msg.FeeList).appendTo("#" + containerId);
                }
                else {
                    $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg.FeeList));
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
        setListCount($GlobalFeeData.totalRow)
        var numericcontainer = $('#numericcontainer');

        var pagesize = parseInt($GlobalFeeData.resultPerPage);
        var total = parseInt($GlobalFeeData.totalRow);
        var start = parseInt($GlobalFeeData.startPage);

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
            paginghtml = paginghtml + "<li><a  href='javascript:void(0)' id='page" + (i * pagesize) + "' class='mappager numpage'>...</a></li>";

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
                $GlobalFeeData.startPage = $(this).attr('id').replace('page', '');
            else if ($(this).hasClass('first')) {
                $GlobalFeeData.startPage = 0;
            }
            else if ($(this).hasClass('next')) {

                if ($GlobalFeeData.startPage < total)
                    $GlobalFeeData.startPage = parseInt($GlobalFeeData.startPage) + parseInt($GlobalFeeData.resultPerPage);
                else
                    return false;

            }
            else if ($(this).hasClass('last')) {
                var modulovalue = (total % $GlobalFeeData.resultPerPage);
                $GlobalFeeData.startPage = (modulovalue == '0') ? (total - $GlobalFeeData.resultPerPage) : (total - modulovalue);

                //$GlobalFeeData.startPage = total - (total % $GlobalFeeData.resultPerPage);

                if ($(this).hasClass('lastInactive'))
                    return false;
            }
            else if ($(this).hasClass('previous')) {
                $GlobalFeeData.startPage = $GlobalFeeData.startPage - $GlobalFeeData.resultPerPage;
                if ($GlobalFeeData.startPage < 0)
                    $GlobalFeeData.startPage = 0;
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
                var lastpage = total - (total % $GlobalFeeData.resultPerPage);
                if (page <= lastpage) {
                    $GlobalFeeData.startPage = $GlobalFeeData.resultPerPage * (page - 1);
                    if ($GlobalFeeData.startPage >= total)
                        $GlobalFeeData.startPage = 0;
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
