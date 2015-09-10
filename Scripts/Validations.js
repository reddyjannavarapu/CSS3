// Anji Common Validations

$(document).ready(function () {
    $('a.scrollToTop').click(function () {
        $('html, body').animate({ scrollTop: 0 }, 'slow');
        return false;
    });

    //$('input[type=text]').attr('autocomplete','off');
    $("form").attr('autocomplete', 'off');
});

function ChangeToErrorClass(obj, errorcount) {

    if (errorcount == 1) {

        obj.addClass('input-validation-error');
        return 1;
    }
    else {
        obj.removeClass('input-validation-error');
        return 0;
    }
}

//Remove Escape character from input
String.prototype.escapeSpecialChars = function () {
    return this.replace(/\\/g, "");
};

function AllowNumbersOnly(input, kbEvent) {
    var keyCode, keyChar;
    keyCode = kbEvent.keyCode;
    if (window.event)
        keyCode = kbEvent.keyCode; 	// IE
    else
        keyCode = kbEvent.which; 	//firefox		         
    if (keyCode == null) return true;
    // get character
    keyChar = String.fromCharCode(keyCode);
    var charSet = "0123456789";
    // check valid chars
    if (charSet.indexOf(keyChar) != -1) return true;
    // control keys
    if (keyCode == null || keyCode == 0 || keyCode == 8 || keyCode == 9 || keyCode == 13 || keyCode == 27) return true;
    return false;
}


function AllowCharactersOnly(input, kbEvent) {
    var keyCode, keyChar;
    keyCode = kbEvent.keyCode;
    if (window.event)
        keyCode = kbEvent.keyCode; 	// IE
    else
        keyCode = kbEvent.which; 	//firefox		         
    if (keyCode == null) return true;
    // get character
    keyChar = String.fromCharCode(keyCode);
    var charSet = "ABCÇDEFGHIJKLMNÑOPQRSTUVWXYZabcçdefghijklmnñopqrstuvwxyz ";
    // check valid chars
    if (charSet.indexOf(keyChar) != -1) return true;
    // control keys
    if (keyCode == null || keyCode == 0 || keyCode == 8 || keyCode == 9 || keyCode == 13 || keyCode == 27) return true;
    return false;
}


function AllowNumbersCharactersOnly(input, kbEvent) {
    var keyCode, keyChar;
    keyCode = kbEvent.keyCode;
    if (window.event)
        keyCode = kbEvent.keyCode; 	// IE
    else
        keyCode = kbEvent.which; 	//firefox		         
    if (keyCode == null) return true;
    // get character
    keyChar = String.fromCharCode(keyCode);
    var charSet = "0123456789ABCÇDEFGHIJKLMNÑOPQRSTUVWXYZabcçdefghijklmnñopqrstuvwxyz ";
    // check valid chars
    if (charSet.indexOf(keyChar) != -1) return true;
    // control keys
    if (keyCode == null || keyCode == 0 || keyCode == 8 || keyCode == 9 || keyCode == 13 || keyCode == 27) return true;
    return false;
}

function preventBackspace(e) {
    var evt = e || window.event;
    if (evt) {
        var keyCode = evt.charCode || evt.keyCode;
        if (keyCode === 8 || keyCode === 46) {
            if (evt.preventDefault) {
                evt.preventDefault();
            } else {
                evt.returnValue = false;
            }
        }
        else {
            evt.returnValue = false;
        }
    }
}

function AllowBackspace(e) {
    var evt = e || window.event;
    if (evt) {
        var keyCode = evt.charCode || evt.keyCode;
        if (keyCode === 220) {
            if (evt.preventDefault) {
                evt.preventDefault();
            } else {
                evt.returnValue = false;
            }
        }
        else {
            evt.returnValue = false;
        }
    }
}

function ShowNotify(text, type, duration) {
    //top-warning-message
    var css;
    if (type == 'warning') {
        css = 'top-warning-message', div = document.getElementById('divNotification');
    }
    else {
        css = (type == 'success') ? 'top-success-message' : 'top-error-message', div = document.getElementById('divNotification');
    }
    div.innerHTML = text;
    div.className = css;
    document.getElementById('divNotification').style.display = '';
    document.getElementById('divNotificationParent').style.display = '';
    setTimeout(HideNotify, duration);
}

function HideNotify() {
    document.getElementById('divNotification').style.display = 'none';
}

function ShowLoadNotify() {

    div = document.getElementById('divLoadNotification');
    div.className = 'top-load-message';
    document.getElementById('divLoadNotification').style.display = '';
    document.getElementById('divLoadNotificationParent').style.display = '';

}

function HideLoadNotify() {
    document.getElementById('divLoadNotification').style.display = 'none';
}

function ControlEmptyNess(Mandatory, Element, Message) {
    var ElementVal = Element.val();
    if (Mandatory == true && (ElementVal == '' || ElementVal == '-1')) {
        //ShowNotify(Message, 'error', 3000);
        return 1;
    }
    else return 0;
}


function ValidateRequired(obj, initialtext) {
    //if it's Not valid
    var errcount = 0;
    if ($.trim(obj.val()).length == 0 || obj.val() == initialtext) {
        errcount = 1;
        ChangeToErrorClass(obj, errcount);
        return errcount;
    }
        //if it's valid
    else {
        errcount = 0;
        ChangeToErrorClass(obj, errcount);
        return errcount;
    }
}

function validEmail(obj, IsRequired) {
    var emailcount = 0;
    if (IsRequired) {
        emailcount = emailcount + ValidateRequired(obj, 'Enter Email Address');
    }
    if (emailcount == 0)
        emailcount = emailcount + validateEmail(obj);
    ChangeToErrorClass(obj, errcount);

    return emailcount;
}

function validateEmail(obj) {
    var emailRegEx = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    str = $.trim(obj.val());
    if (str != '') {
        if (str.match(emailRegEx)) {
            if (checkMailId(str)) {
                errcount = 0;
                ChangeToErrorClass(obj, errcount);
                return errcount;
            }
            else {
                errcount = 1;

                return errcount;
            }
        }
        else {
            errcount = 1;

            return errcount;
        }
    }
    else {
        errcount = 0;

        return errcount;
    }
}

function CheckSelect(ctrl, msg) {
    var errcount = 0;
    var obj = document.getElementById(ctrl);
    //alert(obj.value);
    if (obj.value == '') {
        errcount = 1;

        return errcount;
    }
    if (obj.value == '0') {
        errcount = 1;

        return errcount;
    }
    return errcount;
}

function checkMailId(mailids) {
    var arr = new Array(
    '.com', '.net', '.org', '.biz', '.coop', '.info', '.museum', '.name', '.pro',
    '.edu', '.gov', '.int', '.mil', '.ac', '.ad', '.ae', '.af', '.ag', '.ai', '.al',
    '.am', '.an', '.ao', '.aq', '.ar', '.as', '.at', '.au', '.aw', '.az', '.ba', '.bb',
    '.bd', '.be', '.bf', '.bg', '.bh', '.bi', '.bj', '.bm', '.bn', '.bo', '.br', '.bs',
    '.bt', '.bv', '.bw', '.by', '.bz', '.ca', '.cc', '.cd', '.cf', '.cg', '.ch', '.ci',
    '.ck', '.cl', '.cm', '.cn', '.co', '.cr', '.cu', '.cv', '.cx', '.cy', '.cz', '.de',
    '.dj', '.dk', '.dm', '.do', '.dz', '.ec', '.ee', '.eg', '.eh', '.er', '.es', '.et',
    '.fi', '.fj', '.fk', '.fm', '.fo', '.fr', '.ga', '.gd', '.ge', '.gf', '.gg', '.gh',
    '.gi', '.gl', '.gm', '.gn', '.gp', '.gq', '.gr', '.gs', '.gt', '.gu', '.gv', '.gy',
    '.hk', '.hm', '.hn', '.hr', '.ht', '.hu', '.id', '.ie', '.il', '.im', '.in', '.io',
    '.iq', '.ir', '.is', '.it', '.je', '.jm', '.jo', '.jp', '.ke', '.kg', '.kh', '.ki',
    '.km', '.kn', '.kp', '.kr', '.kw', '.ky', '.kz', '.la', '.lb', '.lc', '.li', '.lk',
    '.lr', '.ls', '.lt', '.lu', '.lv', '.ly', '.ma', '.mc', '.md', '.mg', '.mh', '.mk',
    '.ml', '.mm', '.mn', '.mo', '.mp', '.mq', '.mr', '.ms', '.mt', '.mu', '.mv', '.mw',
    '.mx', '.my', '.mz', '.na', '.nc', '.ne', '.nf', '.ng', '.ni', '.nl', '.no', '.np',
    '.nr', '.nu', '.nz', '.om', '.pa', '.pe', '.pf', '.pg', '.ph', '.pk', '.pl', '.pm',
    '.pn', '.pr', '.ps', '.pt', '.pw', '.py', '.qa', '.re', '.ro', '.rw', '.ru', '.sa',
    '.sb', '.sc', '.sd', '.se', '.sg', '.sh', '.si', '.sj', '.sk', '.sl', '.sm', '.sn',
    '.so', '.sr', '.st', '.sv', '.sy', '.sz', '.tc', '.td', '.tf', '.tg', '.th', '.tj',
    '.tk', '.tm', '.tn', '.to', '.tp', '.tr', '.tt', '.tv', '.tw', '.tz', '.ua', '.ug',
    '.uk', '.um', '.us', '.uy', '.uz', '.va', '.vc', '.ve', '.vg', '.vi', '.vn', '.vu',
    '.ws', '.wf', '.ye', '.yt', '.yu', '.za', '.zm', '.zw');


    var temp = "wrong";
    var mai = mailids; //ids[j];

    if (mai.charCodeAt(mai.length - 1) == 13)
        mai = mai.substring(0, mai.length - 1);

    var dot = mai.lastIndexOf(".");
    var con = mai.substring(dot, mai.length);
    con = con.toLowerCase();
    con = con.toString();

    for (var i = 0; i < (arr.length) ; i++) {
        if (con == arr[i]) {
            temp = 'right';
        }
    }
    if (temp == "wrong") {
        return false;
    }
    return true;

}

function CheckDropDownIDAndSource(ID) {
    var SelectedVals = new Array();
    var OptionRowId = $('#' + ID + ' option:selected').val();
    var SelectedValsID = $("#" + ID + " option[value=" + OptionRowId + "]").attr("personid");
    var SelectedValsSource = $("#" + ID + " option[value=" + OptionRowId + "]").attr("sourcecode");
    if (SelectedValsID == undefined) {
        SelectedVals[0] = 0
    }
    else SelectedVals[0] = SelectedValsID;


    if (SelectedValsSource == undefined) {
        SelectedVals[1] = ''
    }
    else SelectedVals[1] = SelectedValsSource;
    return SelectedVals;
}




function checkValidDate(CSSDate, event) {

    var arrDate = [];
    var check = false;
    var DateCheckExpr = /^\d{1,2}\/\d{1,2}\/\d{4}$/;
    if (DateCheckExpr.test(CSSDate)) {
        arrDate = CSSDate.split('/');
        var dd = parseInt(arrDate[0], 10); //  day  from entered Date
        var mm = parseInt(arrDate[1], 10); //  month from entered Date
        var yyyy = parseInt(arrDate[2], 10); // year from entered Date
        var xdata = new Date(yyyy, mm - 1, dd);
        if ((xdata.getFullYear() == yyyy) && (xdata.getMonth() == mm - 1) && (xdata.getDate() == dd))
            check = true;
        else
            check = false;
    }
    return check;

    //arrDate = CSSDate.split('/');
    //var ValidDate = new Date(arrDate[2], arrDate[1]-1, arrDate[0]);
    //if (ValidDate == 'Invalid Date') {
    //    return false
    //}
}


function checkPassword(password) {
    errcount = 0;
    var regexp1 = new RegExp("^(?=.*[a-zA_Z])(?=.*[0-9])[a-zA-Z0-9@#$%^&+=_!]*$", "g");
    if (regexp1.test(password.val())) {
        errcount = 0;
        ChangeToErrorClass(password, errcount);
        return errcount;
    }
    else {
        errcount = 1;
        ChangeToErrorClass(password, errcount);
        return errcount;
    }
}


function validatePass2(firstobj, obj) {
    //are NOT valid
    if ($.trim(firstobj.val()) != $.trim(obj.val()) || $.trim(firstobj.val()) == '' || $.trim(obj.val()).length < 8) {
        errcount = 1;
        return errcount;
    }
        //are valid
    else {
        errcount = 0;
        return errcount;
    }
}
function ValidateTwoDropdown(firstobj, obj) {
    errcount = 0;
    if ($.trim(firstobj.val()) == $.trim(obj.val())) {
        errcount = 1;

    }
    else {
        errcount = 0;

    }
    ChangeToErrorClass(firstobj, errcount);
    return errcount;
}

function ValidateMinLength(obj) {

    if ($.trim(obj.val()).length < 8) {
        errcount = 1;
    }
    else {
        errcount = 0;
    }
    ChangeToErrorClass(obj, errcount);
    return errcount;
}

function ShowNotify(text, type, duration) {
    var css;
    if (type == 'warning') {
        css = 'top-warning-message', div = document.getElementById('divNotification');
    }
    else {
        css = (type == 'success') ? 'top-success-message' : 'top-error-message', div = document.getElementById('divNotification');
    }
    //var div = document.getElementById('divNotification');
    div.innerHTML = text;
    div.className = css;
    document.getElementById('divNotification').style.display = '';
    document.getElementById('divNotificationParent').style.display = '';
    setTimeout(HideNotify, duration);
}

function HideNotify() {
    document.getElementById('divNotification').style.display = 'none';
}

function EnterOnlyNumbers(key) {
    var keycode = (key.which) ? key.which : key.keyCode;
    if (keycode == 8 || keycode == 9) {
        return true;
    }
    else if (keycode < 48 || keycode > 57) {
        return false;
    }
}

function VaidateListVew(mes) {
    var chkedchk = $('.chked input:checkbox:checked');
    if (chkedchk.length > 0) {
        return 0;
    }
    else {
        ShowNotify(mes, 'error', 3000);
        return 1;
    }
}


function ValidateDropDown(obj) {
    var errcount = 0;
    if (obj.val() == 0) {
        errcount = 1;
    }
    else {
        errcount = 0;
    }
    ChangeToErrorClass(obj, errcount);
    return errcount;
}

function ValidateListBox(obj) {
    //if it's Not valid
    var errcount = 0;
    var id = '';
    $(obj).find('option:selected').each(function () {
        id = id + $(this).val();
    });
    if (id == '' || id == "0") {
        errcount = 1;
    }
    else {
        errcount = 0;
    }
    ChangeToErrorClass(obj, errcount);
    return errcount;
}

// End Anji Common Validations

function ValidateMaxLength(obj, value) {
    if ($.trim(obj.val()).length > value) {
        ShowNotify('Please Enter values Not More Than ' + value + ' characters.', 'error', 3000);
        errcount = 1;
    }
    else {
        errcount = 0;
    }
    ChangeToErrorClass(obj, errcount);
    return errcount;
}

function RemoveValidateRequiredClass() {
    $('.input-validation-error').removeClass("input-validation-error");
}

function DataLoadingStart() {
    Popup.showModal('divLoding', null, null, { 'screenColor': '#fff', 'screenOpacity': .6, 'Background-color': 'red' });
}

function DataLoadingComplete() {
    Popup.hide('divLoding');
}


//***************Decimal Validation***********************

function AllowDecimalNumbersOnly(Current, element, BeforeDecimal, AfterDecimal) {
    var Val = Current.value;
    var checkCharater = DecimalValid(Val, element, BeforeDecimal, AfterDecimal);
    if (!checkCharater) {
        element.preventDefault();
    }

    if (checkCharater == -1) {
        var ValBefore = BeforeDecimal - AfterDecimal;

        ShowNotify('Please Enter only ' + ValBefore + ' digits before decimal.', 'error', 3000);
        return false;
    }


}

function AllowDecimalNumbersOnlyWithNegative(Current, element, BeforeDecimal, AfterDecimal) {
    var Val = Current.value;
    var checkCharater = DecimalValid(Val, element, BeforeDecimal, AfterDecimal);
    if (!checkCharater && element.keyCode!='45') {
        element.preventDefault();
    }

    if (checkCharater == -1) {
        var ValBefore = BeforeDecimal - AfterDecimal;

        ShowNotify('Please Enter only ' + ValBefore + ' digits before decimal.', 'error', 3000);
        return false;
    }


}

function DecimalValid(input, kbEvent, BeforeDecimal, AfterDecimal) {

    var keyCode, keyChar;
    keyCode = kbEvent.keyCode;
    if (window.event)
        keyCode = kbEvent.keyCode; 	// IE
    else
        keyCode = kbEvent.which; 	//firefox		         
    if (keyCode == null) return true;
    // get character
    keyChar = String.fromCharCode(keyCode);

    //for setting charset by checking . count for valid chars
    var countofdot = 0;
    for (var i = 0; i < input.length; i++) {
        if (input.charAt(i).match(/[.]/) != null) {
            countofdot++;
        }
    }
    if (countofdot >= 1) {
        var charSet = "0123456789";
    }
    else {
        var charSet = "0123456789.";
    }
    var IndexOfDecimalPoint = input.substring(input.indexOf('.')).length;
    var kbEventsss = kbEvent.key;
    var ValBefore = BeforeDecimal - (AfterDecimal + 1);
    //if (IndexOfDecimalPoint == ValBefore && input.indexOf('.') == -1) {

    //    var arrLen = [];
    //    arrLen = input.split('.');
    //    var LenBeforeDecimal = arrLen[0];
    //    if (LenBeforeDecimal.length > ValBefore || IndexOfDecimalPoint > AfterDecimal) {
    //        kbEvent.preventDefault();
    //        return false;
    //    }

    //    kbEvent.preventDefault();
    //    return -1;
    //}



    if (input.indexOf('.') == -1 && (IndexOfDecimalPoint != ValBefore && kbEvent.key != '.' && kbEvent.keyCode != 8 && kbEvent.keyCode != 37 && kbEvent.keyCode != 39)) {
        var LenBeforeDecimal = input.length;

        if (LenBeforeDecimal > ValBefore) {
            kbEvent.preventDefault();
            return -1;
        }
    }

    if ((input.indexOf('.') != -1) && (IndexOfDecimalPoint > AfterDecimal) && kbEvent.keyCode != 8 && kbEvent.keyCode != 37 && kbEvent.keyCode != 39) {
        var arrLen = [];
        arrLen = input.split('.');
        var LenBeforeDecimal = arrLen[0];
        if (LenBeforeDecimal.length > ValBefore || IndexOfDecimalPoint > AfterDecimal) {
            kbEvent.preventDefault();
            return false;
        }

    }

    // check valid chars
    if (charSet.indexOf(keyChar) != -1) return true;
    // control keys
    if (keyCode == null || keyCode == 0 || keyCode == 8 || keyCode == 9 || keyCode == 13 || keyCode == 27) return true;

    return false;
}

function checkDegitsBeforeDecimal(Val, Len) {
    var indexOfBeforeDecimal = Val.indexOf('.');
    if (indexOfBeforeDecimal == -1 && Val.length > Len) {
        return false;
    }
    else
        return true;

}


//***************End Decimal Validation******************************

function SetPageAttributes(liId, pageName, pageDesc, pageId) {
    $('#sidebar').find('.hover').removeClass('active');
    $('#sidebar').find('#' + liId).addClass('active');
    $('#sidebar').find('#' + pageId).addClass('active');
    $('#spnPageHeadder').text(pageName);
    $('#spnPageDescription').text(pageDesc);
}

function textareaLimiter(elementobj, maxsizelimit) {
    elementobj.inputlimiter({
        remText: '%n / ' + maxsizelimit + ' Characters ',
        limitText: '',
        limit: maxsizelimit
    });
}

function CheckForSpecialCharacters(name, note, itemsData, IsAddMore) {
    var exp = '';
    if (IsAddMore) {
        exp = /[!#$%\&*(){}[\]<>?/|\;~`]/;
    }
    else {
        exp = /[!#$%\&*(){}[\]<>?/|\`]/;
    }
    if ($.trim(name).match(exp) || $.trim(name).match("'")) {
        return false;
    }
    if ($.trim(note).match(exp) || $.trim(note).match("'")) {
        return false;
    }
    if ($.trim(itemsData).match(exp) || $.trim(itemsData).match("'")) {
        return false;
    }
    return true;
}

function CheckCharacterForNumberandCharacter(keyCode) {
    var keyCode, keyChar;
    keyCode = keyCode;
    if (window.event)
        keyCode = keyCode; 	// IE
    else
        keyCode = keyCode; 	//firefox		         
    if (keyCode == null) return true;
    // get character
    keyChar = String.fromCharCode(keyCode);
    var charSet = "0123456789ABCÇDEFGHIJKLMNÑOPQRSTUVWXYZabcçdefghijklmnñopqrstuvwxyz ";
    // check valid chars
    if (charSet.indexOf(keyChar) != -1) return true;
    // control keys
    if (keyCode == null || keyCode == 0 || keyCode == 8 || keyCode == 9 || keyCode == 13 || keyCode == 27) return true;
    return false;
}

//keypress event for date
function checkDateValidation(e) {

    var keynum
    var keychar
    var numcheck
    // For Internet Explorer 
    if (window.event) {
        keynum = e.keyCode
    }
        // For Netscape/Firefox/Opera 
    else if (e.which) {
        keynum = e.which
    }
    keychar = String.fromCharCode(keynum)
    //List of special characters you want to restrict
    if (keychar == "1" || keychar == "2" || keychar == "3" || keychar == "4" || keychar == "5" || keychar == "6" || keychar == "7" || keychar == "8" || keychar == "9" || keychar == "0") {
        return true;
    }
    else {
        return false;
    }
}


$('.help a').hover(
    function () {
        $('#tipDivDashboard').removeClass('tipdiv');
        $('#tipDivDashboard').addClass('tipdivShow');
    },
    function () {
        $('#tipDivDashboard').removeClass('tipdivShow');
        $('#tipDivDashboard').addClass('tipdiv');
    }
);

$('.DisableCutCopyPaste').bind('copy paste cut drop', function (e) {
    e.preventDefault();
});


$(document).on('change', ':text:not(.special) , textarea:not(.special)', function () {
    if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
        this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
    }
});

//HIDE/Show Widget
$('.widgetGridHeader').unbind('click').click(function (e) {

    if ($(e.target).is('img')) {
        e.preventDefault();
        return;
    }
    else {
        if ($(this).closest('.widgetGrid').hasClass('collapsed')) {
            $(this).closest('.widgetGrid').removeClass('collapsed');
            $(this).closest('.widgetGrid').find('.fa-chevron-down').removeClass('fa-chevron-down').addClass('fa-chevron-up');
        }
        else {
            $(this).closest('.widgetGrid').addClass('collapsed');
            $(this).closest('.widgetGrid').find('.fa-chevron-up').removeClass('fa-chevron-up').addClass('fa-chevron-down');
        }
    }
});

function ApplyDatePickerFromAndToDate(objFrom, objTo) {

    var d = new Date();
    //var month = d.getMonth() + 1;
    //var day = d.getDate();
    //var PresentDateOfCurrentYear = (day < 10 ? '0' : '') + day + '/' +
    //    (month < 10 ? '0' : '') + month + '/' + d.getFullYear();
    //var PreviousYear = d.getFullYear() - 1;

    //var PreviDay = day - 1;
    //if (PreviDay == 0) {
    //    var date = new Date();
    //    //var firstDay = new Date(date.getFullYear(), date.getMonth(), 1);
    //    var lastDay = new Date(date.getFullYear(), date.getMonth() + 1, 0);
    //    PreviDay = lastDay.getDate();
    //}
    //var PresentDateOfPreviousYear = (PreviDay < 9 ? '0' : '') + PreviDay + '/' +
    //   (month < 9 ? '0' : '') + month + '/' + PreviousYear;
    //// var outputFromDate = (day < 10 ? '0' : '') + day + '/' +

    var year = d.getFullYear() - 1;
    var month = d.getMonth() + 1;
    var day = d.getDate();

    if (day == 29 && month == 2) {          //if leapYear
        var Fromdate = (day < 10 ? '0' : '') + (day - 1) + "/" + (month < 10 ? '0' : '') + month + "/" + year;    //PresentDateOfPreviousYear;
    }
    else {
        var Fromdate = (day < 10 ? '0' : '') + day + "/" + (month < 10 ? '0' : '') + month + "/" + year;    //PresentDateOfPreviousYear;
    }

    var Todate = (day < 10 ? '0' : '') + day + '/' + (month < 10 ? '0' : '') + month + '/' + d.getFullYear();           //PresentDateOfCurrentYear;

    $(objFrom).datepicker("destroy").datepicker({
        'maxDate': Todate,
        showOtherMonths: true,
        selectOtherMonths: true,
        showWeek: true,
        changeYear: true,
        changeMonth: true,
        dateFormat: 'dd/mm/yy',
        yearRange: "-10:+50",
        onClose: function (selectedDate) {
            $(objTo).datepicker("option", "minDate", selectedDate);
        }
    }).val(Fromdate);

    $(objTo).datepicker("destroy").datepicker({
        'minDate': Fromdate,
        showOtherMonths: true,
        selectOtherMonths: true,
        showWeek: true,
        changeYear: true,
        changeMonth: true,
        dateFormat: 'dd/mm/yy',
        yearRange: "-10:+50",
        onClose: function (selectedDate) {
            $(objFrom).datepicker("option", "maxDate", selectedDate);
        }
    }).val(Todate);

}

function ApplyDatePickerFromAndToDateForOneMonth(objFrom, objTo) {
    var d = new Date();
    var year = d.getFullYear();
    var month = d.getMonth();
    var day = d.getDate();

    if(month == 0) {
        month = 12;
        year = year-1;
    }
     
    if (day == 29 && month == 2) {          //if leapYear
        var Fromdate = (day < 10 ? '0' : '') + (day - 1) + "/" + (month < 10 ? '0' : '') + month + "/" + year;    //PresentDateOfPreviousYear;
    }
    else {
        var Fromdate = (day < 10 ? '0' : '') + day + "/" + (month < 10 ? '0' : '') + month + "/" + year;    //PresentDateOfPreviousYear;
    }

    month = d.getMonth() + 1;
    year = d.getFullYear();
    var Todate = (day < 10 ? '0' : '') + day + '/' + (month < 10 ? '0' : '') + month + '/' + d.getFullYear();           //PresentDateOfCurrentYear;

    $(objFrom).datepicker("destroy").datepicker({
        'maxDate': Todate,
        showOtherMonths: true,
        selectOtherMonths: true,
        showWeek: false,
        changeYear: true,
        changeMonth: true,
        dateFormat: 'dd/mm/yy',
        yearRange: "-10:+50",
        onClose: function (selectedDate) {
            $(objTo).datepicker("option", "minDate", selectedDate);
        }
    }).val(Fromdate);

   

    $(objTo).datepicker("destroy").datepicker({
        'minDate': Fromdate,
        showOtherMonths: true,
        selectOtherMonths: true,
        showWeek: false,
        changeYear: true,
        changeMonth: true,
        dateFormat: 'dd/mm/yy',
        yearRange: "-10:+50",
        onClose: function (selectedDate) {
            $(objFrom).datepicker("option", "maxDate", selectedDate);
        }
    }).val(Todate);

}
function setListCount(count) {
    $('#listName').text('Total Entries');
    $('#listCount').text(count);
}