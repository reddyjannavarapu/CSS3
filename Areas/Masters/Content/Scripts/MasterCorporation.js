$(document).ready(function () {

    SetPageAttributes('liMasters', 'Masters', 'Create Company', 'liCreateCompany');

    $('#modal-form_Company').removeClass('modal');
    $('#modal-form_Company').find('.modal-content').removeAttr('style').removeClass('modal-content');
    $('#modal-form_Company').find('.modal-header').remove();
    $('#modal-form_Company').find('.modal-dialog').removeClass('modal-dialog');

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

    LoadData("", "NameofCompany ASC");

    $('#btnSaveCompany').click(function () {
        LoadData("", "");
    });

    $('#btnSearchCorporation').click(function () {
        var ClientName = $("#txtNameOfThecompany").val();
        var OrderBy = $("#ddlOrderBy").val();
        $GlobalData.startPage = 0;
        $GlobalData.resultPerPage = $("#ddlPageSize").val();
        if (OrderBy == '-1') {
            OrderBy = '';
        }
        LoadData(ClientName, OrderBy);
    });



});



function LoadData(ClientName, OrderBy) {
    CallCorporation("GetAllCorporationData", 'CorporationTemplate', 'trData', "{'CompanyName':'" + escape($.trim(ClientName)) + "','OrderBy':'" + OrderBy + "','startpage':" + $GlobalData.startPage + ",'rowsperpage':" + $GlobalData.resultPerPage + "}", true, DataLoadCallBack);
}

function DataLoadCallBack() {
    try {
        var trLength = $('#trData').find('tr').length;
        if (trLength == 0) {
            $('#divPagination').hide();
            $('#divCorporationData').show();
        }
        else {
            $('#divCorporationData').hide();
            $('#divPagination').show();

            $('#trData').find('.aEdit').unbind('click');
            $('#trData').find('.aEdit').click(EditCorporation);

            $('#trData').find('.aView').unbind('click');
            $('#trData').find('.aView').click(ViewCorporation);

            GenerateNumericPaging();
        }

    } catch (e) {
        console.log(e);
    }
}

function EditCorporation() {
    try {
        scrollToTop();

        var ID = $(this).attr('CorpID');
        var nameofcompany = $(this).attr('nameofcompany');
        var countryofincorporation = $(this).attr('countryofincorporation');
        var dateofincorporation = $(this).attr('dateofincorporation');
        var registrationno = $(this).attr('registrationno');

        var AccPacCode = $(this).attr('accpaccode');

        var addressline1 = $(this).attr('addressline1');
        var addressline2 = $(this).attr('addressline2');
        var addressline3 = $(this).attr('addressline3');
        var country = $(this).attr('country');
        var postalcode = $(this).attr('postalcode');
        var email = $(this).attr('email');
        var phoneno = $(this).attr('phone');
        var fax = $(this).attr('fax');

        $('#txtNameOfCompany').val(nameofcompany);
        $('#ddlCountryOfIncorporation').val(countryofincorporation);
        $('#txtDateOfInCorporation').val(dateofincorporation);
        $('#txtCompanyRegistrationNo').val(registrationno);
        $("#txtACCPACCode").val(AccPacCode);

        $('#txtCompanyAddressLine1').val(addressline1);
        $('#txtCompanyAddressLine2').val(addressline2);
        $('#txtCompanyAddressLine3').val(addressline3);
        $('#ddlCountry').val(country);
        $('#txtCompanyPostalCode').val(postalcode);
        $('#txtEmailinCompany').val(email);
        $('#txtContactNoinCompany').val(phoneno);
        $('#txtFaxinCompany').val(fax);
        $('#hdnCreateCompany').val(ID);

    } catch (e) {
        console.log(e);
    }

}

function ViewCorporation() {
    try {
        $('#spnName').text($(this).attr('nameofcompany'));
        $('#spnCountryOfIncorp').text($(this).attr('CountryNameOfIncorp'));
        $('#spnDateOfIncorp').text($(this).attr('dateofincorporation'));
        $('#spnRegistrationNo').text($(this).attr('registrationno'));
        $('#spnAccPacCode').text($(this).attr('accpaccode'));
        $('#spnAddressLine1').text($(this).attr('addressline1'));
        $('#spnAddressLine2').text($(this).attr('addressline2'));
        $('#spnAddressLine3').text($(this).attr('addressline3'));
        $('#spnCountry').text($(this).attr('CountryName'));
        $('#spnPostalCode').text($(this).attr('postalcode'));
        $('#spnEmail').text($(this).attr('email'));
        $('#spnContactNo').text($(this).attr('phone'));
        $('#spnFax').text($(this).attr('fax'));

        $('#modal-form').modal('show');

    } catch (e) {
        console.log(e);
    }
}

function CallCorporation(path, templateId, containerId, parameters, clearContent, callBack) {
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

            if (msg.CorporationCount != null && msg.CorporationCount != 'undefined') {
                $GlobalData.totalRow = msg.CorporationCount;
            }
            if (templateId != '' && containerId != '' && msg != '') {
                if (!clearContent) {
                    $.tmpl($('#' + templateId).html(), msg.CorporationList).appendTo("#" + containerId);
                }
                else {
                    $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg.CorporationList));
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
                if ($GlobalData.startPage < total) {
                    var startPageNum = parseInt($GlobalData.startPage);
                    var ResPageNum = parseInt($GlobalData.resultPerPage);
                    $GlobalData.startPage = startPageNum + ResPageNum;
                }
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
            LoadData("", "");
        });
        $('#txtPageToGo').keypress(function (e) {
            if (e.which == 13) {
                var page = parseInt($(this).val());
                var lastpage = total - (total % $GlobalData.resultPerPage);
                if (page <= lastpage) {
                    $GlobalData.startPage = $GlobalData.resultPerPage * (page - 1);
                    if ($GlobalData.startPage >= total)
                        $GlobalData.startPage = 0;
                    LoadData("", "");
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
