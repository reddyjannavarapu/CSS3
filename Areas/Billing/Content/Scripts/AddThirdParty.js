$(document).ready(function () {

    SetPageAttributes('liBilling', 'Billing', 'To Manage Billing Third Party', 'liAddThirdParty');

    $GlobalBillingThirdParty = {};
    $GlobalBillingThirdParty.ID = 0;
    $GlobalBillingThirdParty.startPage = 0;
    $GlobalBillingThirdParty.resultPerPage = 10;
    $GlobalBillingThirdParty.totalRow = 10;
    BindCountry();


    $('#txtContactNo').keypress(function (event) {
        try {
            var checkCharater = AllowNumbersOnly($(this).val(), event);
            if (!checkCharater) {
                event.preventDefault();
            }
        } catch (e) {
            console.log(e);
        }
    });




    $("#btnClearDetails").click(function () {
        try {
            ClearThirdPartyDetails();
        } catch (e) {
            console.log(e);
        }
    });
    $('#txtCompanyPostalCode').keypress(function (event) {
        try {

            var checkCharater = AllowNumbersCharactersOnly($(this).val(), event);
            if (!checkCharater) {
                event.preventDefault();
            }
        } catch (e) {
            console.log(e);
        }
    });

    $("#btnSaveThirdPartyDetails").click(function () {
        try {
            $GlobalBillingThirdParty1 = {};
            var ThirdPrtyID = $.trim($("#hdnThirdPrtyID").val());
            if (ThirdPrtyID == '' || ThirdPrtyID == undefined)
                $GlobalBillingThirdParty1.ID = 0;
            else $GlobalBillingThirdParty1.ID = parseInt(ThirdPrtyID);

            $GlobalBillingThirdParty1.CompanyName = $("#txtNameOfCompany").val();
            $GlobalBillingThirdParty1.ACCPACCode = $("#txtACCPACCode").val();
            $GlobalBillingThirdParty1.AddressLine1 = $("#txtCompanyAddressLine1").val();
            $GlobalBillingThirdParty1.AddressLine2 = $("#txtCompanyAddressLine2").val();
            $GlobalBillingThirdParty1.AddressLine3 = $("#txtCompanyAddressLine3").val();
            $GlobalBillingThirdParty1.CountryCode = $('#ddlCountry').val();
            $GlobalBillingThirdParty1.PostalCode = $("#txtCompanyPostalCode").val();
            $GlobalBillingThirdParty1.Email = $("#txtEmail").val();
            $GlobalBillingThirdParty1.ContactNo = $("#txtContactNo").val();
            $GlobalBillingThirdParty1.Fax = $("#txtFax").val();
            var count = 0;
            count += ControlEmptyNess(true, $("#txtNameOfCompany"), 'Please enter Name Of the company.');
            count += ControlEmptyNess(true, $("#txtACCPACCode"), 'Please enter ACCPAC Code.');
            count += ControlEmptyNess(false, $("#txtCompanyAddressLine1"), 'Please enter Address Line1.');
            count += ControlEmptyNess(false, $("#txtCompanyAddressLine1"), 'Please enter Address Line2.');
            count += ControlEmptyNess(false, $("#txtCompanyAddressLine3"), 'Please enter Address Line3.');
            count += ControlEmptyNess(false, $("#ddlCountry"), 'Please select country.');
            count += ControlEmptyNess(false, $("#txtCompanyPostalCode"), 'Please enter Postal Code.');

            count += ControlEmptyNess(false, $("#txtEmail"), 'Please enter Email.');
            count += ControlEmptyNess(false, $("#txtContactNo"), 'Please enter Contact No.');
            count += ControlEmptyNess(false, $("#txtFax"), 'Please enter Fax.');

            if (count > 0) {
                ShowNotify('Please enter all mandatory fields.', 'error', 3000);
                return false;
            }

            var JsonThirdPartyData = JSON.stringify({ ThirdPartyDetails: $GlobalBillingThirdParty1 });
            BillingThirdPartyCallService("SaveBillingThirdPartyDetails", '', '', JsonThirdPartyData, false, SavedStatusCallBack);

        }
        catch (ex) {
            console.log(ex);
        }
    });


    $("#btnSearchThirdParty").click(function () {

        var ClientName = $("#txtNameOfThecompany").val();
        var OrderBy = $("#ddlOrderBy").val();
        $GlobalBillingThirdParty.startPage = 0;
        $GlobalBillingThirdParty.resultPerPage = $("#ddlPageSize").val();
        if (OrderBy == '-1') {
            OrderBy = '';
        }
        BindBillingDataForAllCalls(ClientName, OrderBy);

    });
});

function ClearThirdPartyDetails() {

    $("#txtNameOfCompany").val('');
    $("#txtACCPACCode").val('');
    $("#txtCompanyAddressLine1").val('');
    $("#txtCompanyAddressLine2").val('');
    $("#txtCompanyAddressLine3").val('');
    $('#ddlCountry').val('109');
    $("#txtCompanyPostalCode").val('');
    $("#txtEmail").val('');
    $("#txtContactNo").val('');
    $("#txtFax").val('');
    $("#hdnThirdPrtyID").val('');
}
function BindCountry() {
    try {
        CallMasterData("/PartialContent/GetCountryDetails", 'CountryDropDownCompanyTemplate', 'ddlCountry', "{}", false, CountryCallBack);
    } catch (e) {
        console.log(e);
    }
}
function CountryCallBack() {
    try {
        $('#ddlCountry').val('109');
        BindBillingThirdPartyDetails();
    } catch (e) {
        console.log(e);
    }
}

function BindBillingThirdPartyDetails() {
    try {
        var CompanyName = '';
        var OrderBy = 'CompanyName ASC';
        BindBillingDataForAllCalls(CompanyName, OrderBy);
    } catch (e) {
        console.log(e);
    }
}

function BindBillingDataForAllCalls(CompanyName, OrderBy) {

    BillingThirdPartyCallService("BindThirdPartyBillingDetails", 'scriptThirdPartyDetails', 'trThirdPartyData', "{'CompanyName':'" + escape($.trim(CompanyName)) + "','OrderBy':'" + OrderBy + "','StartPage':" + $GlobalBillingThirdParty.startPage + ",'RowsPerPage':" + $GlobalBillingThirdParty.resultPerPage + "}", true, BindBillingThirdPartyDetailsCallBack);

}




function BindBillingThirdPartyDetailsCallBack() {
    try {
        var trLength = $("#trThirdPartyData").find("tr").length;
        if (trLength >= 1) {
            //$("#tblBillingThirdParty").show();
            $("#trThirdPartyData").find(".aView").unbind("click");
            $("#trThirdPartyData").find(".aView").click(ShowThirdPartyDetails);
            $("#trThirdPartyData").find(".aEdit").unbind("click");
            $("#trThirdPartyData").find(".aEdit").click(EditThirdPartyDetails);
            $("#trThirdPartyData").find(".aDelete").unbind("click");
            $("#trThirdPartyData").find(".aDelete").click(DeleteThirdPartyDetails);
            $("#divNoData").hide();
            $("#divPaging").show();
            GenerateNumericPaging();
        }
        else {
            //$("#tblBillingThirdParty").hide();
            $("#divNoData").show();
            $("#divPaging").hide();
        }

    } catch (e) {
        console.log(e);
    }
}
function ShowThirdPartyDetails() {
    try {
        $("#lblCompanyName").text($(this).attr("companyname"));
        $("#lblAccPacCode").text($(this).attr("accpaccode"));
        $("#lblCompanyAddressLine1").text($(this).attr("addressline1"));
        $("#lblCompanyAddressLine2").text($(this).attr("addressline2"));
        $("#lblCompanyAddressLine3").text($(this).attr("addressline3"));
        $('#lblCountryName').text($(this).attr("countryname"));
        $("#lblPostalCode").text($(this).attr("postalcode"));
        $("#lblEmail").text($(this).attr("email"));
        $("#lblContactNo").text($(this).attr("contactno"));
        $("#lblFax").text($(this).attr("fax"));
        $('#modal-form').modal('show');

    } catch (e) {
        console.log(e);
    }
}

function EditThirdPartyDetails() {
    try {
        $("#hdnThirdPrtyID").val($(this).attr("ID"));
        $("#txtNameOfCompany").val($(this).attr("companyname"));
        $("#txtACCPACCode").val($(this).attr("accpaccode"));
        $("#txtCompanyAddressLine1").val($(this).attr("addressline1"));
        $("#txtCompanyAddressLine2").val($(this).attr("addressline2"));
        $("#txtCompanyAddressLine3").val($(this).attr("addressline3"));
        $('#ddlCountry').val($(this).attr("countrycode"));
        $("#txtCompanyPostalCode").val($(this).attr("postalcode"));
        $("#txtEmail").val($(this).attr("email"));
        $("#txtContactNo").val($(this).attr("contactno"));
        $("#txtFax").val($(this).attr("fax"));
        scrollToTop();
    } catch (e) {
        console.log(e);
    }
}

function scrollToTop() {
    $("html, body").animate({ scrollTop: 0 }, "slow");
    return false;
}

function DeleteThirdPartyDetails() {
    try {
        $GlobalBillingThirdParty.ID = $(this).attr("ID");
        $("#dialog-confirm").removeClass('hide').dialog({
            resizable: false,
            modal: true,
            title_html: true,
            buttons: [
                {
                    html: "<i class='ace-icon fa fa-trash-o bigger-110'></i>&nbsp; Delete",
                    "class": "btn btn-danger btn-xs",
                    click: function () {
                        BillingThirdPartyCallService("DeleteBillingThirdPartyDetailsByID", '', '', "{'ID':" + $GlobalBillingThirdParty.ID + "}", '', DeleteThirdPartyCallBack);
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

function SavedStatusCallBack() {
    try {
        if ($GlobalBillingThirdParty.SavedStatus >= 1) {
            ShowNotify('Success.', 'success', 3000);
            BindBillingThirdPartyDetails();
            ClearThirdPartyDetails();
        }
        else if ($GlobalBillingThirdParty.SavedStatus == -1) {
            ShowNotify('Third Party details already exists.', 'error', 3000);
            return false;
        }

    } catch (e) {
        console.log(e);
    }
}
function DeleteThirdPartyCallBack() {
    try {
        if ($GlobalBillingThirdParty.SavedStatus >= 1) {
            ShowNotify('Success.', 'success', 3000);
            BindBillingThirdPartyDetails();
            ClearThirdPartyDetails();
        }
        else if ($GlobalBillingThirdParty.SavedStatus == -1) {
            ShowNotify('Third Party details are in use.', 'error', 3000);
            return false;
        }

    } catch (e) {
        console.log(e);
    }
}
function BillingThirdPartyCallService(path, templateId, containerId, parameters, clearContent, callBack) {
    ShowLoadNotify();
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
                if (msg == '0') {
                    ShowNotify('Invalid session login again.', 'error', 3000);
                    return false
                }
                $GlobalBillingThirdParty.SavedStatus = msg;
                $GlobalBillingThirdParty.ThirdPartyData = msg;

                if (msg.RecordsCount != null && msg.RecordsCount != 'undefined') {
                    $GlobalBillingThirdParty.totalRow = msg.RecordsCount;
                }


                if (templateId != '' && containerId != '') {

                    if (!clearContent) {
                        $.tmpl($('#' + templateId).html(), msg.BillingThirdPartyList).appendTo("#" + containerId);
                    }
                    else {
                        $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg.BillingThirdPartyList));
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
    HideLoadNotify();
}

function GenerateNumericPaging() {
    try {
        var numericcontainer = $('#numericcontainer');
        setListCount($GlobalBillingThirdParty.totalRow)
        var pagesize = parseInt($GlobalBillingThirdParty.resultPerPage);
        var total = parseInt($GlobalBillingThirdParty.totalRow);
        var start = parseInt($GlobalBillingThirdParty.startPage);

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
                $GlobalBillingThirdParty.startPage = $(this).attr('id').replace('page', '');
            else if ($(this).hasClass('first')) {
                $GlobalBillingThirdParty.startPage = 0;
            }
            else if ($(this).hasClass('next')) {

                if ($GlobalBillingThirdParty.startPage < total)
                    $GlobalBillingThirdParty.startPage = parseInt($GlobalBillingThirdParty.startPage) + parseInt($GlobalBillingThirdParty.resultPerPage);
                else
                    return false;

            }
            else if ($(this).hasClass('last')) {
                var modulovalue = (total % $GlobalBillingThirdParty.resultPerPage);
                $GlobalBillingThirdParty.startPage = (modulovalue == '0') ? (total - $GlobalBillingThirdParty.resultPerPage) : (total - modulovalue);

                //$GlobalBillingThirdParty.startPage = total - (total % $GlobalBillingThirdParty.resultPerPage);

                if ($(this).hasClass('lastInactive'))
                    return false;
            }
            else if ($(this).hasClass('previous')) {
                $GlobalBillingThirdParty.startPage = $GlobalBillingThirdParty.startPage - $GlobalBillingThirdParty.resultPerPage;
                if ($GlobalBillingThirdParty.startPage < 0)
                    $GlobalBillingThirdParty.startPage = 0;
            }
            if ($(this).hasClass('nextInactive'))
                return false;
            if ($(this).hasClass('previousInactive'))
                return false;
            if ($(this).hasClass('firstInactive', 'previousInactive', 'lastInactive', 'nextInactive'))
                return false;
            BindBillingThirdPartyDetails();
        });

        $('#txtPageToGo').keypress(function (e) {
            if (e.which == 13) {
                var page = parseInt($(this).val());
                var lastpage = total - (total % $GlobalBillingThirdParty.resultPerPage);
                if (page <= lastpage) {
                    $GlobalBillingThirdParty.startPage = $GlobalBillingThirdParty.resultPerPage * (page - 1);
                    if ($GlobalBillingThirdParty.startPage >= total)
                        $GlobalBillingThirdParty.startPage = 0;
                    BindBillingThirdPartyDetails();
                }
            }
        });

    }
    catch (err) {
        console.log(err);
    }
}