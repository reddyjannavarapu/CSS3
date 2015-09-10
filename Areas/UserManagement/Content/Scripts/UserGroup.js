$(document).ready(function () {

    SetPageAttributes('liUsers', 'Users', 'Available users in CSS2', 'liUserAndDetails');
    $GlobalData = {};
    $GlobalData.searchText = '';
    $GlobalData.OrderBy = ''
    $GlobalData.InsertedID = 0;
    $GlobalData.startPage = 0;
    $GlobalData.resultPerPage = 10;
    $GlobalData.totalRow = 10;
    $GlobalData.Status = '-1';
    $GlobalData.Role = '';
    LoadData();

    $("#btnUpdate").click(function () {
        try {
            var IsManager = $("#chkManager").is(":checked");
            var IsAdmin = $("#chkAdmin").is(":checked");
            var IsSuperAdmin = $("#chkSuperAdmin").is(":checked");
            var UserID = $("#txtUserName").attr("userid");
            var CheckEmptyNess = $("#txtUserName").val();
            var IsActive = $("#chkActive").is(":checked");
            if (CheckEmptyNess == '') {
                ShowNotify('Please update valid Userdetails.', 'error', 3000);
                return false;
            }
            CallServices("UserDetails/InsertANDUpdateUserDetails", '', '', "{'UserID':" + UserID + ",'IsManager':" + IsManager + ",'IsAdmin':" + IsAdmin + ",'IsSuperAdmin':" + IsSuperAdmin + ",'IsActive':" + IsActive + "}", '', InsertAndUpdateCall);
        } catch (e) {
            console.log(e);
        }
    });

    $('#btnCancel').click(function () {
        try {
            ClearValues();
        } catch (e) {
            console.log(e);
        }
    });

    $('#btnSearch').click(function () {
        try {
            $GlobalData.OrderBy = $('#ddlOrderBy').find('option:Selected').val();
            $GlobalData.searchText = $('#txtSearch').val();
            $GlobalData.startPage = 0;
            $GlobalData.resultPerPage = $('#ddlPageSize').find('option:Selected').val();
            $GlobalData.Role = $('#ddlRole').find('option:Selected').val();
            $GlobalData.Status = $('#ddlStatus').find('option:Selected').val();
            CallServices("UserDetails/GetUserGroupDetails", 'UserServiceTemplate', 'trUserGroupDetails', "{'startPage':" + $GlobalData.startPage + ",'resultPerPage':" + $GlobalData.resultPerPage + ",'Search':'" + escape($.trim($GlobalData.searchText)) + "','Role':'" + $GlobalData.Role + "','Status':" + parseInt($GlobalData.Status) + ",'OrderBy':'" + $GlobalData.OrderBy + "'}", true, DataLoadCallBack);
            ClearValues();
        } catch (e) {
            console.log(e);
        }
    });

   

});
function InsertAndUpdateCall() {
    try {
        scrollToTop();

        if ($GlobalData.InsertedID >= 1) {
            $("#txtUserName").removeAttr("userid");
            LoadData();
            ShowNotify('Success.', 'success', 2000);
            return false;
        }
        else {
            ShowNotify('Invalid Session login again.', 'error', 3000);
            return false;
        }

    } catch (e) {
        console.log(e);
    }
}
function LoadData() {
    try {
        CallServices("UserDetails/GetUserGroupDetails", 'UserServiceTemplate', 'trUserGroupDetails', "{'startPage':" + $GlobalData.startPage + ",'resultPerPage':" + $GlobalData.resultPerPage + ",'Search':'" + escape($.trim($GlobalData.searchText)) + "','Role':'" + $GlobalData.Role + "','Status':" + parseInt($GlobalData.Status) + ",'OrderBy':'Name ASC'}", true, DataLoadCallBack);
    } catch (e) {
        console.log(e);
    }
}
function ClearValues() {
    try {
        $('#txtUserName').val('');
        $('#chkManager').prop('checked', false);
        $('#chkAdmin').prop('checked', false);
        $('#chkSuperAdmin').prop('checked', false);
        $("#txtUserName").removeAttr("userid");
        $("#chkActive").prop("checked", false);
       // $GlobalData.searchText = '';
       // $GlobalData.OrderBy = ''
    } catch (e) {
        console.log(e);
    }
}
function DataLoadCallBack() {
    try {
        var trLength = $('#trUserGroupDetails').find("tr").length;
        if (trLength >= 1) {
            $('#trUserGroupDetails').find('.aEdit').unbind('click');
            $('#trUserGroupDetails').find('.aEdit').click(EditService);
            $("#divSearchNoUserData").hide();
            $("#Divpagination").show();
            ClearValues();

            GenerateNumericPaging();
        }
        else {
            $("#divSearchNoUserData").show();
            $("#Divpagination").hide();
        }

    } catch (e) {
        console.log(e);
    }
}
function EditService() {
    try {
        var LoginID = $(this).attr("loginid");
        var IsManager = $(this).attr("ismanager");
        var IsSuperAdmin = $(this).attr("issuperadmin");
        var IsAdmin = $(this).attr("isadmin");
        var UserID = $(this).attr("userid");
        var IsActive = $(this).attr("isactive");
        $("#txtUserName").val(LoginID);
        $("#txtUserName").attr("UserID", UserID);

        if (IsManager == "true") $("#chkManager").prop("checked", true);
        else $("#chkManager").prop("checked", false);

        if (IsSuperAdmin == "true") $("#chkSuperAdmin").prop("checked", true);
        else $("#chkSuperAdmin").prop("checked", false);

        if (IsAdmin == "true") $("#chkAdmin").prop("checked", true);
        else $("#chkAdmin").prop("checked", false);

        if (IsActive == "true") $("#chkActive").prop("checked", true);
        else $("#chkActive").prop("checked", false);

        scrollToTop();

    } catch (e) {
        console.log(e);
    }
}
function GenerateNumericPaging() {
    try {

        setListCount($GlobalData.totalRow)

        var numericcontainer = $('#numericcontainer');

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
            if ($(this).hasClass('numpage')) {
                $GlobalData.startPage = $(this).attr('id').replace('page', '');

            }

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
                var modulovalue = (total % $GlobalData.resultPerPage);
                $GlobalData.startPage = (modulovalue == '0') ? (total - $GlobalData.resultPerPage) : (total - modulovalue);

                //$GlobalData.startPage = total - ($GlobalData.resultPerPage % total);

                if ($(this).hasClass('lastInactive'))
                    return false;
            }
            else if ($(this).hasClass('previous')) {
                $GlobalData.startPage = parseInt($GlobalData.startPage) - parseInt($GlobalData.resultPerPage);
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
function CallServices(path, templateId, containerId, parameters, clearContent, callBack) {
    try {
        $.ajax({
            type: "POST",
            url: path,
            data: parameters,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (msg) {

                if (msg == '0') {
                    ShowNotify('Invalid Session login again.', 'error', 3000);
                    return false;
                }
                $GlobalData.InsertedID = msg;

                if (msg.UserList != null && msg.UserList != 'undefined') {

                    $GlobalData.totalRow = msg.UserCount;
                }
                if (templateId != '' && containerId != '' && msg != '') {
                    if (!clearContent) {
                        $.tmpl($('#' + templateId).html(), msg.UserList).appendTo("#" + containerId);
                    }
                    else {
                        $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg.UserList));
                    }
                }

                if (callBack != undefined && callBack != '')
                    callBack();

            },
            error: function (xhr, ajaxOptions, thrownError) {
                //throw new Error(xhr.statusText);
            }
        });

    } catch (e) {
        console.log(e);
    }
}
