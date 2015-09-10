$(document).ready(function () {

    SetPageAttributes('liMasters', 'Masters', 'Create Individual', 'liCreateIndividual');

    $('#modal-form_Individual').removeClass('modal');
    $('#modal-form_Individual').find('.modal-content').removeAttr('style').removeClass('modal-content');
    $('#modal-form_Individual').find('.modal-header').remove();
    $('#modal-form_Individual').find('.modal-dialog').removeClass('modal-dialog');

    $GlobalData = {};

    //$GlobalData.serviceId = 0;
    //$GlobalData.serviceData = '';
    //$GlobalData.searchText = '';
    //$GlobalData.status = '-1';
    $GlobalData.startPage = 0;
    $GlobalData.resultPerPage = 10;
    $GlobalData.totalRow = 10;
    //$GlobalData.InsertedID = '';
    //$GlobalData.OrderBy = '';

    LoadData();


    $('#btnSaveIndividual').click(function () {
        try {
            LoadData();
        } catch (e) {
            console.log(e);
        }
    });

    $('#btnSearchIndividual').click(function () {
        try {
            var ClientName = $("#txtNameOfThecompany").val();
            var OrderBy = $("#ddlOrderBy").val();
            $GlobalData.startPage = 0;
            $GlobalData.resultPerPage = $("#ddlPageSize").val();
            if (OrderBy == '-1') {
                OrderBy = '';
            }

            IndividualDataCall(ClientName, OrderBy);

        } catch (e) {
            console.log(e);
        }
    });



});



function LoadData() {
    try {

        var CompanyName = '';
        var OrderBy = 'Name ASC';
        IndividualDataCall(CompanyName, OrderBy);
    } catch (e) {
        console.log(e);
    }
}
function IndividualDataCall(CompanyName, OrderBy) {
    CallIndividual("GetAllIndividualData", 'IndividualTemplate', 'trData', "{'CompanyName':'" + escape($.trim(CompanyName)) + "','OrderBy':'" + OrderBy + "','startpage':" + $GlobalData.startPage + ",'rowsperpage':" + $GlobalData.resultPerPage + "}", true, DataLoadCallBack);

}
function DataLoadCallBack() {
    try {
        var trLength = $('#trData').find('tr').length;
        if (trLength == 0) {
            $('#divPagination').hide();
            $('#divIndividualData').show();
        }
        else {
            $('#divIndividualData').hide();
            $('#divPagination').show();

            $('#trData').find('.aEdit').unbind('click');
            $('#trData').find('.aEdit').click(EditIndividual);

            $('#trData').find('.aView').unbind('click');
            $('#trData').find('.aView').click(ViewIndividual);

            GenerateNumericPaging();
        }

    } catch (e) {
        console.log(e);
    }
}
function EditIndividual() {
    try {
        scrollToTop();

        var ID = $(this).attr('indvid');
        var name = $(this).attr('name');
        var nationality = $(this).attr('nationality');
        var singaporepr = $(this).attr('singaporepr');
        var passport = $(this).attr('passport');
        var dateofbirth = $(this).attr('dateofbirth');
        var nricfinno = $(this).attr('nricfinno');
        var AccPacCode = $(this).attr('accpaccode');
        var addressline1 = $(this).attr('addressline1');
        var addressline2 = $(this).attr('addressline2');
        var addressline3 = $(this).attr('addressline3');
        var country = $(this).attr('country');
        var postalcode = $(this).attr('postalcode');
        var occupation = $(this).attr('occupation');
        var email = $(this).attr('email');
        var phoneno = $(this).attr('phone');
        var fax = $(this).attr('fax');
        var NRICExpirydate = $(this).attr('nricexpirydate');

        $('#txtName').val(name);
        $('#ddlCountryForNationality').val(nationality);
        if (singaporepr == "True") {
            $('#chkSingaporePR').prop('checked', true)
        }
        else {
            $('#chkSingaporePR').prop('checked', false)
        }
        $('#txtPassport').val(passport);
        $("#NRICExpirydate").val(NRICExpirydate);
        $('#txtDateofBirth').val(dateofbirth);
        $('#txtNricFinNo').val(nricfinno);
        $("#txtACCPACCode").val(AccPacCode);
        $('#txtIndividualAddressLine1').val(addressline1);
        $('#txtIndividualAddressLine2').val(addressline2);
        $('#txtIndividualAddressLine3').val(addressline3);
        $('#ddlIndividualCountry').val(country);
        $('#txtIndividualPostalCode').val(postalcode);
        $('#txtOccupation').val(occupation);
        $('#txtEmail').val(email);
        $('#txtContactNo').val(phoneno);
        $('#txtFax').val(fax);
        $('#hdnCreateIndividual').val(ID);
        

    } catch (e) {
        console.log(e);
    }
}
function ViewIndividual() {
    try {
        $('#spnName').text($(this).attr('name'));
        $('#spnNationality').text($(this).attr('nationalityname'));
        $('#spnSingaporePR').text($(this).attr('singaporepr'));
        $('#spnPassport').text($(this).attr('passport'));
        $('#spnNRICExpiryDate').text($(this).attr('nricexpirydate'));
        $('#spnDOB').text($(this).attr('dateofbirth'));
        $('#spnNricFinNo').text($(this).attr('nricfinno'));
        $('#spnAccPacCode').text($(this).attr('accpaccode'));
        $('#spnAddressLine1').text($(this).attr('addressline1'));
        $('#spnAddressLine2').text($(this).attr('addressline2'));
        $('#spnAddressLine3').text($(this).attr('addressline3'));
        $('#spnCountry').text($(this).attr('countryname'));
        $('#spnPostalCode').text($(this).attr('postalcode'));
        $('#spnOccupation').text($(this).attr('occupation'));
        $('#spnEmail').text($(this).attr('email'));
        $('#spnPhoneNo').text($(this).attr('phone'));
        $('#spnFax').text($(this).attr('fax'));

        $('#modal-form').modal('show');

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
                    $GlobalData.startPage = parseInt($GlobalData.startPage) + parseInt($GlobalData.resultPerPage);
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
        console.log(err);
    }
}
function scrollToTop() {
    $("html, body").animate({ scrollTop: 0 }, "slow");
    return false;
}
function CallIndividual(path, templateId, containerId, parameters, clearContent, callBack) {
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

            if (msg.IndividualCount != null && msg.IndividualCount != 'undefined') {
                $GlobalData.totalRow = msg.IndividualCount;
            }
            if (templateId != '' && containerId != '' && msg != '') {
                if (!clearContent) {
                    $.tmpl($('#' + templateId).html(), msg.IndividualList).appendTo("#" + containerId);
                }
                else {
                    $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg.IndividualList));
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
