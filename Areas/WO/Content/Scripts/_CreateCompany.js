$(document).ready(function () {

    $GlobalCorporationData = {};
    $GlobalCorporationData.InsertedId = 0;

    $('#txtDateOfInCorporation').datepicker({
        showOtherMonths: true,
        selectOtherMonths: true,
        showWeek: true,
        changeYear: true,
        changeMonth: true,
        dateFormat: 'dd/mm/yy',
        yearRange: "-10:+50"
    });

    $('#txtContactNoinCompany').keypress(function (event) {
        try {
            var checkCharater = AllowNumbersOnly($(this).val(), event);
            if (!checkCharater) {
                event.preventDefault();
            }
        } catch (e) {
            console.log(e);
        }
    });

    $('#txtFaxinCompany').keypress(function (event) {
        try {
            var checkCharater = AllowNumbersOnly($(this).val(), event);
            if (!checkCharater) {
                event.preventDefault();
            }
        } catch (e) {
            console.log(e);
        }
    });

    BindCountryOfIncorporationDropDown();

    function BindCountryOfIncorporationDropDown() {
        try {
            CallMasterData("/PartialContent/GetCountryDetails", 'CountryDropDownCompanyTemplate', 'ddlCountryOfIncorporation', "{}", false, CorporationCountryOfIncorpCallBack);
        } catch (e) {
            console.log(e);
        }
    }

    function CorporationCountryOfIncorpCallBack() {
        try {
            $('#ddlCountryOfIncorporation').val('109');
            BindCountryForCompany();
        } catch (e) {
            console.log(e);
        }
    }

    function BindCountryForCompany() {
        try {
            CallMasterData("/PartialContent/GetCountryDetails", 'CountryDropDownCompanyTemplate', 'ddlCountry', "{}", false, CountryInCompany);
        } catch (e) {
            console.log(e);
        }
    }

    function CountryInCompany() {
        try {
            $('#ddlCountry').val('109');
        } catch (e) {
            console.log(e);
        }
    }

    $('#btnSaveCompany').unbind("click");

    $('#btnSaveCompany').click(function () {
        try {
            var ID = $('#hdnCreateCompany').val();
            var NameOfCompany = $('#txtNameOfCompany');
            var CountryOfIncorporation = $('#ddlCountryOfIncorporation option:selected').val();
            var DateOfIncorporation = $('#txtDateOfInCorporation');
            var RegistrationNo = $('#txtCompanyRegistrationNo');
            var AccPacCode = $('#txtACCPACCode');
            var AddressLine1 = $('#txtCompanyAddressLine1');
            var AddressLine2 = $('#txtCompanyAddressLine2');
            var AddressLine3 = $('#txtCompanyAddressLine3');
            var Country = $('#ddlCountry option:selected').val();
            var PostalCode = $('#txtCompanyPostalCode');
            var Email = $('#txtEmailinCompany');
            var ContactNo = $('#txtContactNoinCompany');
            var Fax = $('#txtFaxinCompany');

            var count = 0;
            count += ControlEmptyNess(true, NameOfCompany, 'Please enter Name Of the company.');
            count += ControlEmptyNess(false, AddressLine1, 'Please enter AddressLine 1.');


            if (count > 0) {
                ShowNotify('Please Enter values for all mandatory fields.', 'error', 3000);
                return false;
            }
            else {
                var checkEmailFormat = validEmail($('#txtEmailinCompany'), false);
                if (checkEmailFormat == 1) {
                    ShowNotify('Please enter valid Email.', 'error', 3000);
                    return false;
                }

                if (Country == '109' && $('#txtCompanyPostalCode').val() == '') {
                    ShowNotify('Please enter Postal Code.', 'error', 3000);
                    return false;
                }

                Corporation = {};
                Corporation.ID = ID;
                Corporation.NameOfCompany = $.trim(NameOfCompany.val());
                Corporation.CountryOfIncorporation = $.trim(CountryOfIncorporation);
                Corporation.DateOfIncorporation = $.trim(DateOfIncorporation.val());
                Corporation.RegistrationNo = $.trim(RegistrationNo.val());
                Corporation.AccPacCode = $.trim(AccPacCode.val());
                Corporation.AddressLine1 = $.trim(AddressLine1.val());
                Corporation.AddressLine2 = $.trim(AddressLine2.val());
                Corporation.AddressLine3 = $.trim(AddressLine3.val());
                Corporation.Country = $.trim(Country);
                Corporation.PostalCode = $.trim(PostalCode.val());
                Corporation.Email = $.trim(Email.val());
                Corporation.ContactNo = $.trim(ContactNo.val());
                Corporation.Fax = $.trim(Fax.val());

                var jsonText = JSON.stringify({ Corporation: Corporation });

                //CallCompany("/PartialContent/CreateCorporation", '', '', "{'ID':" + ID + ",'NameOfCompany':'" + $.trim(NameOfCompany.val()) + "','CountryOfIncorporation':" + $.trim(CountryOfIncorporation) + ",'DateOfIncorporation':'" + $.trim(DateOfIncorporation.val()) + "','RegistrationNo':'" + $.trim(RegistrationNo.val()) + "','AccPacCode':'" + $.trim(AccPacCode.val()) + "','AdderessLine1':'" + $.trim(AddressLine1.val()) + "','AddressLine2':'" + $.trim(AddressLine2.val()) + "','AddressLine3':'" + $.trim(AddressLine3.val()) + "','Country':" + $.trim(Country) + ",'PostalCode':'" + $.trim(PostalCode.val()) + "','Email':'" + $.trim(Email.val()) + "','ContactNo':'" + $.trim(ContactNo.val()) + "','Fax':'" + $.trim(Fax.val()) + "'}", true, CreateCallBack);
                CallCompany("/PartialContent/CreateCorporation", '', '', jsonText, true, CreateCallBack);
            }

        } catch (e) {
            console.log(e);
        }
    });

    function CreateCallBack() {
        try {
            if ($GlobalCorporationData.InsertedId > 0) {
                ShowNotify('Success.', 'success', 2000);
                ClearCorporationValues();
                return false;
            }
        } catch (e) {
            console.log(e);
        }
    }

    $('#btnCompanyCancel').click(function () {
        try {
            ClearCorporationValues();
        } catch (e) {
            console.log(e);
        }
    });

    function ClearCorporationValues() {
        try {
            $('#txtNameOfCompany').val('');
            $('#ddlCountryOfIncorporation').val('109');
            $('#txtDateOfInCorporation').val('');
            $('#txtCompanyRegistrationNo').val('');
            $('#txtACCPACCode').val('');
            $('#txtCompanyAddressLine1').val('');
            $('#txtCompanyAddressLine2').val('');
            $('#txtCompanyAddressLine3').val('');
            $('#ddlCountry').val("109");
            $('#txtCompanyPostalCode').val('');
            $('#txtEmailinCompany').val('');
            $('#txtContactNoinCompany').val('');
            $('#txtFaxinCompany').val('');
            $('#hdnCreateCompany').val('0');
        } catch (e) {
            console.log(e);
        }
    }

    function CallCompany(path, templateId, containerId, parameters, clearContent, callBack) {
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
                    $GlobalCorporationData.InsertedId = msg;
                    if (templateId != '' && containerId != '') {

                        if (!clearContent) {
                            $.tmpl($('#' + templateId).html(), msg.PartialContentList).appendTo("#" + containerId);
                        }
                        else {
                            $("#" + containerId).html($.tmpl($('#' + templateId).html(), msg.PartialContentList));
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

});