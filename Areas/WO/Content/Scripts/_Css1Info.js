/// <reference path="../../../../Content/css/assets/js/jquery.min.js" />
/// <reference path="../../../../Content/css/assets/js/ace/elements.scroller.js" />
$(document).ready(function () {

    $("#btnCss1Info").click(function () {
        try {
            BindCss1Info($GlobalDataWOTYPE.WOID, $GlobalDataWOTYPE.Type);

            $('.ace-scroll').ace_scroll({ size: 400, styleClass: 'scroll-visible' });

            $('#dvCss1Info').modal({
                "backdrop": "static",
                "show": "true"
            });


        } catch (e) {
            console.log(e);
        }
    });

    function BindCss1Info(WOID, Wotype) {
        try {
            $.ajax({
                type: "POST",
                url: "/Css1Info/Css1InfoDetails",
                data: "{'WOID':'" + WOID + "','WOType':'" + Wotype + "'}",
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
                        else {
                            bindDynamicTables(msg);
                        }

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

    function bindDynamicTables(msg) {
        try {

            var ddlID = '1';
            $('#Output_' + ddlID).html('');

            var IsEmpty = '0';

            var result = $.parseJSON(msg);

            $.each(result, function (index, data) {

                if (data[0] != undefined) {
                    IsEmpty = '1';

                    var Title = '<div style="margin:15px 0px 10px;"><h5 class="blue bigger">' + data[0]['tableName'] + '</h5></div>';
                    $('#Output_' + ddlID).append(Title);

                    var cols = new Array();
                    for (var key in data[0]) {
                        cols.push(key);
                    }

                    var table = $('<table class="table table-striped table-bordered table-hover" style="background-color: white !important;"></table>');
                    var thbody = $('<thead></thead>');
                    var th = $('<tr></tr>');

                    for (var i = 0; i < data.length; i++) {
                        var row = data[i];
                        var tr = $('<tr></tr>');

                        for (var j = 1; j < cols.length; j++) {
                            if (i == 0) {
                                th.append('<th>' + cols[j] + '</th>');
                            }

                            var columnName = cols[j];
                            tr.append('<td>' + row[columnName] + '</td>');
                        }

                        if (i == 0) {
                            table.append(thbody.append(th));
                        }

                        table.append(tr);
                        $('#Output_' + ddlID).append(table);
                    }
                }
            });

            if (IsEmpty == '0') {
                $('#Output_' + ddlID).html('<h5 class="red medium"> No data found.</h5>');
                return false;
            }


        } catch (e) {
            console.log(e);
        }

    }

});