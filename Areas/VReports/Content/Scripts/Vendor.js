$(document).ready(function () {

    SetPageAttributes('liReports', 'Vendor Report', 'Vendor Report History', 'liVendorReport');

    $GlobalData = {};

    $GlobalData.VendorId = 0;
    $GlobalData.VendorData = '';

    $GlobalData.startPage = 0;
    $GlobalData.resultPerPage = 10;
    $GlobalData.totalRow = 10;
    $GlobalData.OrderBy = '';
    $GlobalData.FromDate = '';
    $GlobalData.ToDate = '';
    var href = window.location.href;
    href = href.split('/').pop();
    ApplyDatePickerFromAndToDate($('#txtFromDate'), $('#txtToDate'));

    $GlobalData.FromDate = $.trim($("#txtFromDate").val());;
    $GlobalData.ToDate = $.trim($("#txtToDate").val());

    if (href == "VendorReport") {
        //BindTypeDropDown();  // on 12-July-2015, changed to static values  
        LoadData();
    }

    function BindTypeDropDown() {
        try {
            CallVendor("GetVendorReportType", 'TypeDropdownTemplate', 'ddlType', "{}", false, '');
        } catch (e) {
            console.log(e);
        }
    }

    $('#btnSearch').click(function () {
        try {
            var Type = $('#ddlType option:selected').val();
            if (Type == 'Select') {
                Type = '';
                ShowNotify('Please select Type.', 'error', 3000);
                return false;
            }
          
            $GlobalData.OrderBy = Type;
            $GlobalData.FromDate = $.trim($("#txtFromDate").val());;
            $GlobalData.ToDate = $.trim($("#txtToDate").val());
            $GlobalData.startPage = 0;
            $GlobalData.resultPerPage = $("#ddlPageSize").val();

            if (($GlobalData.FromDate == '' && $GlobalData.ToDate != '') || ($GlobalData.ToDate == '' && $GlobalData.FromDate != '')) {
                ShowNotify('Please enter From Date and ToDate.', 'error', 6000);
                return false;
            } 
            LoadData();
        } catch (e) {
            console.log(e);
        }
    });

    function LoadData() {
        try {
           
            CallVendor("GetData", 'vendorTemplate', 'trData', "{'startpage':" + $GlobalData.startPage + ",'rowsperpage':" + $GlobalData.resultPerPage + ",'OrderBy':'" + $GlobalData.OrderBy + "','FromDate':'" + $GlobalData.FromDate + "','ToDate':'" + $GlobalData.ToDate + "'}", true, DataLoadCallBack);
        } catch (e) {
            console.log(e);
        }
    }

    function DataLoadCallBack() {
        try {
            var vendorLength = $("#trData").find("tr").length;
            if (vendorLength >= 1) {
                $("#divServiceList").show();
                $('#divPagination').show();
                $('#divVendorReportData').hide();

                $('#trData').find('.aDownload').unbind('click');
                $('#trData').find('.aDownload').click(Download);

                $('#trData').find('.aViewHistory').unbind('click');
                $('#trData').find('.aViewHistory').click(ViewHistory);
                GenerateNumericPaging();
            }
            else {
                //$("#divServiceList").hide();
                $('#divPagination').hide();
                $('#divVendorReportData').show();
            }
        } catch (e) {
            console.log(e);
        }
    }

    function ViewHistory() {
        try {
            var vrid = parseInt($(this).attr('vrid'));
            var vridStatus = isNaN(vrid);

            if (vrid != '' && vrid != undefined && vridStatus == false)
                window.location.href = "/VReports/VReports/VendorUpload?VRID=" + vrid;
            return false;
        } catch (e) {
            console.log(e);
        }
    }

    function Download() {
        try {
            var vrid = parseInt($(this).attr('vrid'));

            var vridStatus = isNaN(vrid);

            if (vrid != '' && vrid != undefined && vridStatus == false)
                window.location.href = "/VReports/VReports/DownloadReport?VRID=" + vrid;
            return false;
        } catch (e) {
            console.log(e);
        }
    }

    $('#ddlPageSize').change(function () {
        try {
            $GlobalData.startPage = 0;
            $GlobalData.resultPerPage = $('#ddlPageSize').val();
            LoadData();
        } catch (e) {
            console.log(e);
        }
    });

    function CallVendor(path, templateId, containerId, parameters, clearContent, callBack) {
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
                        if (msg.VendorCount != null && msg.VendorCount != 'undefined') {
                            $GlobalData.totalRow = msg.VendorCount;
                        }
                        if (templateId != '' && containerId != '') {

                            if (!clearContent) {
                                $.tmpl($('#' + templateId).html(), msg.VendorList).appendTo("#" + containerId);
                            }
                            else {
                                $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg.VendorList));
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
                paginghtml = paginghtml + "<a id='page" + (i * pagesize) + "' class='mappager numpage'>...</a>";

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

                    //$GlobalData.startPage = total - ($GlobalData.resultPerPage % total);

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

});