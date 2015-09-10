$(document).ready(function () {

    $GlobalIndividualData = {};
    $GlobalIndividualData.InsertedId = 0;

    $('#txtDateofBirth').datepicker({
        changeYear: true,
        changeMonth: true,
        showOtherMonths: true,
        selectOtherMonths: true,
        showWeek: true,
        maxDate: 0,
        dateFormat: 'dd/mm/yy',
        yearRange: "-100:+0"
    });
    $('#txtNRICExpiryDate').datepicker({
        changeYear: true,
        changeMonth: true,
        showOtherMonths: true,
        selectOtherMonths: true,
        showWeek: true,
        dateFormat: 'dd/mm/yy',
        yearRange: "-10:+50"
    });

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

    $('#txtFax').keypress(function (event) {
        try {
            var checkCharater = AllowNumbersOnly($(this).val(), event);
            if (!checkCharater) {
                event.preventDefault();
            }
        } catch (e) {
            console.log(e);
        }
    });

    BindCountryDropDownIndividualNationality();

    function BindCountryDropDownIndividualNationality() {
        try {
            CallMasterData("/PartialContent/GetCountryDetails", 'NationalityDropDownForIndividualTemplate', 'ddlCountryForNationality', "{}", false, IndividualNationalityCallBack);
        } catch (e) {
            console.log(e);
        }
    }

    function IndividualNationalityCallBack() {
        try {
            $('#ddlCountryForNationality').val('109');
            BindCountryDropDownIndividual();
        } catch (e) {
            console.log(e);
        }
    }

    function BindCountryDropDownIndividual() {
        try {
            CallMasterData("/PartialContent/GetCountryDetails", 'CountryDropDownForIndividualTemplate', 'ddlIndividualCountry', "{}", false, CountryIndividualDropDown);
        } catch (e) {
            console.log(e);
        }
    }

    function CountryIndividualDropDown() {
        try {
            $('#ddlIndividualCountry').val('109');
        } catch (e) {
            console.log(e);
        }
    }

    $('#btnSaveIndividual').unbind("click");
    $('#btnSaveIndividual').click(function () {
        try {
            var ID = $('#hdnCreateIndividual').val();
            var Name = $('#txtName');
            var Nationality = $('#ddlCountryForNationality option:selected').val();
            var SingaporePR = $('#chkSingaporePR').is(':checked');
            var Passport = $('#txtPassport');
            var NRICExpiryDate = $('#txtNRICExpiryDate');
            var DateOfBirth = $('#txtDateofBirth')
            var NricFinNo = $('#txtNricFinNo');
            var AccPacCode = $('#txtACCPACCode');
            var AddressLine1 = $('#txtIndividualAddressLine1');
            var AddressLine2 = $('#txtIndividualAddressLine2');
            var AddressLine3 = $('#txtIndividualAddressLine3');
            var Country = $('#ddlIndividualCountry option:selected').val();
            var PostalCode = $('#txtIndividualPostalCode');
            var Occupation = $('#txtOccupation');
            var Email = $('#txtEmail');
            var ContactNo = $('#txtContactNo');
            var Fax = $('#txtFax');


            var count = 0;
            count += ControlEmptyNess(true, Name, 'Please enter Name.');
            //count += ControlEmptyNess(false, AddressLine1, 'Please enter AddressLine 1.');


            if (count > 0) {
                ShowNotify('Please Enter values for all mandatory fields.', 'error', 3000);
                return false;
            }
            else {
                var checkEmailFormat = validEmail($('#txtEmail'), false);
                if (checkEmailFormat == 1) {
                    ShowNotify('Please enter valid Email.', 'error', 3000);
                    return false;
                }


                if (Country == '109' && $('#txtIndividualPostalCode').val() == '') {
                    ShowNotify('Please enter Postal Code.', 'error', 3000);
                    return false;
                }

                Individual = {};
                Individual.ID = ID;
                Individual.Name = $.trim(Name.val());
                Individual.Nationality = $.trim(Nationality);
                Individual.SingaporePR = $.trim(SingaporePR);
                Individual.Passport = $.trim(Passport.val());
                Individual.NRICExpiryDate = $.trim(NRICExpiryDate.val());
                Individual.DateOfBirth = $.trim(DateOfBirth.val());
                Individual.NricFinNo = $.trim(NricFinNo.val());
                Individual.AccPacCode = $.trim(AccPacCode.val());
                Individual.AddressLine1 = $.trim(AddressLine1.val());
                Individual.AddressLine2 = $.trim(AddressLine2.val());
                Individual.AddressLine3 = $.trim(AddressLine3.val());
                Individual.Country = $.trim(Country);
                Individual.PostalCode = $.trim(PostalCode.val());
                Individual.Occupation = $.trim(Occupation.val());
                Individual.Email = $.trim(Email.val());
                Individual.ContactNo = $.trim(ContactNo.val());
                Individual.Fax = $.trim(Fax.val());

                var jsonText = JSON.stringify({ Individual: Individual });
                CallIndividual("/PartialContent/CreateIndividual", '', '', jsonText, true, CreateCallBack);
            }

        } catch (e) {
            console.log(e);
        }
    });

    function CreateCallBack() {
        try {
            if ($GlobalIndividualData.InsertedId > 0) {
                ShowNotify('Success.', 'success', 2000);
                ClearIndividualValues();
                return false;
            }
        } catch (e) {
            console.log(e);
        }
    }

    $('#btnIndividualCancel').click(function () {
        try {
            ClearIndividualValues();
        } catch (e) {
            console.log(e);
        }
    });

    function ClearIndividualValues() {
        try {
            $('#txtName').val('');
            $('#ddlCountryForNationality').val('109');
            $('#chkSingaporePR').prop('checked', false);
            $('#txtPassport').val('');
            $('#txtDateofBirth').val('');
            $('#txtNricFinNo').val('');
            $('#txtACCPACCode').val('');
            $('#txtIndividualAddressLine1').val('');
            $('#txtIndividualAddressLine2').val('');
            $('#txtIndividualAddressLine3').val('');
            $('#ddlIndividualCountry').val("109");
            $('#txtIndividualPostalCode').val('');
            $('#txtOccupation').val('');
            $('#txtEmail').val('');
            $('#txtContactNo').val('');
            $('#txtFax').val('');
            $('#hdnCreateIndividual').val('0');
            $("#txtNRICExpiryDate").val('');

        } catch (e) {
            console.log(e);
        }
    }

    function CallIndividual(path, templateId, containerId, parameters, clearContent, callBack) {
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

                        $GlobalIndividualData.InsertedId = msg;

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

        } catch (e) {
            console.log(e);
        }
    }

});