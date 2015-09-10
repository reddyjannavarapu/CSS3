function InvoicePreviewDetails(Name, Address1, Address2, Address3, Amount, PreviewData, InvoiceNo) {
    try {
        $('#ViewInvoice').find(".modalIndividual").modal({
            "backdrop": "static",
            "show": "true"
        });
        $("#lblCompanyName").text(Name);
        $("#lblAddress1").text(Address1);
        $("#lblAddress2").text(Address2);
        $("#lblAddress3").text(Address3);
        // InvoicePreviewTable(PreviewData);
        $('#spnTaxInvoiceNo').text(InvoiceNo);

        CallToBindInvoicePreview(PreviewData, "scriptInvoice", "tblPreviewData", Amount, BindInvoicePreviewCallBack);

    } catch (e) {
        console.log(e);
    }
}

function CallToBindInvoicePreview(PreviewData, templateId, containerId, Amount, callBack) {
    ShowLoadNotify();
    $("#" + containerId).html($.tmpl($('#' + templateId).html(), PreviewData));

    $(".lblTotalAmount").text(Amount);
    if (callBack != undefined && callBack != '')
        callBack();
    HideLoadNotify();
}




function InvoicePreviewDetailsDI(Name, Address1, Address2, Address3, Amount, arrAmount, arrItem, arrDescription) {
    try {
        $('#ViewInvoiceDI').find(".modalIndividual").modal({
            "backdrop": "static",
            "show": "true"
        });
        $("#ViewInvoiceDI #lblCompanyName").text(Name);
        $("#ViewInvoiceDI #lblAddress1").text(Address1);
        $("#ViewInvoiceDI #lblAddress2").text(Address2);
        $("#ViewInvoiceDI #lblAddress3").text(Address3);
        // InvoicePreviewTable(PreviewData);
        CallToBindInvoicePreviewDI("ViewInvoiceDI", arrAmount, arrItem, arrDescription, "tblPreviewData", Amount, BindInvoicePreviewCallBack);

    } catch (e) {
        console.log(e);
    }
}

function CallToBindInvoicePreviewDI(DivID, arrAmount, arrItem, arrDescription, containerId, Amount, callBack) {
    ShowLoadNotify();
    for (var count = 0; count < arrAmount.length; count++) {
        if (count == 0)
            var InvoiceRow = "<tr><td><label class='lblDescription'>" + arrDescription[count] + "</label> </td><td>" + arrItem[count] + "</td><td>" + arrAmount[count] + "</td></tr>";
        else InvoiceRow = InvoiceRow + "<tr><td><label class='lblDescription'>" + arrDescription[count] + "</label> </td><td>" + arrItem[count] + "</td><td>" + arrAmount[count] + "</td></tr>";
    }
    $('#' + DivID + '  #' + containerId).append(InvoiceRow);
    $(".lblTotalAmount").text(Amount);
    if (callBack != undefined && callBack != '')
        callBack();
    HideLoadNotify();

}


function InvoicePreviewDetailsFee(Name, Address1, Address2, Address3, Amount, arrAmount, arrItem, arrDescription) {
    try {
        $('#ViewInvoiceFee').find(".modalIndividual").modal({
            "backdrop": "static",
            "show": "true"
        });
        $("#ViewInvoiceFee #lblCompanyName").text(Name);
        $("#ViewInvoiceFee #lblAddress1").text(Address1);
        $("#ViewInvoiceFee #lblAddress2").text(Address2);
        $("#ViewInvoiceFee #lblAddress3").text(Address3);
        // InvoicePreviewTable(PreviewData);
        CallToBindInvoicePreviewDI("ViewInvoiceFee", arrAmount, arrItem, arrDescription, "tblPreviewData", Amount, BindInvoicePreviewCallBack);

    } catch (e) {
        console.log(e);
    }
}



function InvoicePreviewDetailsDIAndFee(Name, Address1, Address2, Address3, AmountDI, arrAmountDI, arrItemDI, arrDescriptionDI, AmountFee, arrAmountFee, arrItemFee, arrDescriptionFee) {
    try {
        $('#divViewInvoiceForDIandFee').find(".modalIndividual").modal({
            "backdrop": "static",
            "show": "true"
        });
        $("#divViewInvoiceForDIandFee #lblCompanyName").text(Name);
        $("#divViewInvoiceForDIandFee #lblAddress1").text(Address1);
        $("#divViewInvoiceForDIandFee #lblAddress2").text(Address2);
        $("#divViewInvoiceForDIandFee #lblAddress3").text(Address3);
        // InvoicePreviewTable(PreviewData);
        CallToBindInvoicePreviewDIAndFee("divViewInvoiceForDIandFee", arrAmountDI, arrItemDI, arrDescriptionDI, "tblPreviewData", AmountDI, AmountFee, arrAmountFee, arrItemFee, arrDescriptionFee, BindInvoicePreviewCallBack);

    } catch (e) {
        console.log(e);
    }
}


function CallToBindInvoicePreviewDIAndFee(DivID, arrAmount, arrItem, arrDescription, containerId, Amount, AmountFee, arrAmountFee, arrItemFee, arrDescriptionFee, callBack) {
    ShowLoadNotify();
    for (var count = 0; count < arrAmount.length; count++) {
        if (count == 0)
            var InvoiceRowDI = "<tr><td><label class='lblDescription'>" + arrDescription[count] + "</label> </td><td>" + arrItem[count] + "</td><td>" + arrAmount[count] + "</td></tr>";
        else InvoiceRowDI = InvoiceRowDI + "<tr><td><label class='lblDescription'>" + arrDescription[count] + "</label> </td><td>" + arrItem[count] + "</td><td>" + arrAmount[count] + "</td></tr>";
    }
    for (var count = 0; count < arrAmountFee.length; count++) {
        if (count == 0)
            var InvoiceRowFee = "<tr><td><label class='lblDescription'>" + arrDescriptionFee[count] + "</label> </td><td>" + arrItemFee[count] + "</td><td>" + arrAmountFee[count] + "</td></tr>";
        else InvoiceRowFee = InvoiceRowFee + "<tr><td><label class='lblDescription'>" + arrDescriptionFee[count] + "</label> </td><td>" + arrItemFee[count] + "</td><td>" + arrAmountFee[count] + "</td></tr>";
    }
    $('#' + DivID + '  #' + containerId).append(InvoiceRowFee + InvoiceRowDI);
    if (Amount == '')
        Amount = 0;
    if (AmountFee == '')
        AmountFee = 0;
    var TotalNetAmount = parseInt(Amount) + parseInt(AmountFee);
    $(".lblTotalAmount").text(TotalNetAmount);
    if (callBack != undefined && callBack != '')
        callBack();
    HideLoadNotify();

}





function BindInvoicePreviewCallBack() {

    $("#tblPreviewData").find("tr  td .lblDescription").each(function () {
        var description = $(this).text();

        var arrDesc = [];
        arrDesc = description.split('~^');
        var result;
        if (arrDesc.length > 1) {
            $.each(arrDesc, function (index, value) {
                if (index == 0)
                    result = value;
                else result = result + '</br>' + value;


            });
            $(this).html(result);
        }

        $('.ace-scroll').ace_scroll({ size: 300, styleClass: 'scroll-visible', mouseWheelLock: true });

    });
}