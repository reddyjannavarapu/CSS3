// to check date format starts MMDDYYYY
function validateDateMMDDYYYY(dateval) {
    if (dateval.value != '') {

        var month;
        var day;
        var year;

        var DateOfBirth = dateval.value;

        if (DateOfBirth.length == 8) {

            month = DateOfBirth.substring(0, 2);
            day = DateOfBirth.substring(2, 4);
            year = DateOfBirth.substring(4, 8);
        }
        else if (DateOfBirth.length == 10) {
            month = DateOfBirth.substring(0, 2);
            day = DateOfBirth.substring(3, 5);
            year = DateOfBirth.substring(6, 10);
        }
        else {
            alert('Invalid Date Format: Please enter MMDDYYYY!');
            dateval.focus();
            return false;
        }

        if (day.length == 2 && month.length == 2 && year.length == 4) {
            flag = true;
        }
        else {
            alert('Invalid Date Format: Please enter MMDDYYYY!');
            dateval.focus();
            return false;
        }


        var d = new Date();
        var currentYear = d.getFullYear();

        if (year.length != 4)
        { dateval.focus(); return false; }

        if (IsNumeric(year)) {
            if (year > 0 && year <= currentYear) {
                flag = true;
            }
            else {
                alert('Year should be in Between 1753 and Current year');
                dateval.focus();
                return false;
            }

        }
        else {
            dateval.focus();
            return false;
        }



        if (year > 1753 && year <= currentYear) {
            flag = true;
        }
        else {
            alert('Year should be in Between 1753 and Current year');
            dateval.focus();
            return false;
        }

        if (IsNumeric(month)) {
            if (month > 0 && month < 13) {
                flag = true;
            }
            else {
                alert('Months should be in between 1 - 12');
                dateval.focus();
                return false;
            }
        }
        else {
            alert('Months should be in digits');
            dateval.focus();
            return false;
        }

        if (IsNumeric(day)) {
            if (day > 0 && day < 32) {
                flag = true;
            }
            else {
                alert('Invalid Date Format: Days should be in between 1 - 31');
                dateval.focus();
                return false;
            }
        }
        else {
            alert('Days should be in digits');
            dateval.focus();
            return false;
        }


        if ((month == 4 || month == 6 || month == 9 || month == 11) && day == 31) {
            alert("Month " + month + " doesn't have 31 days!")
            dateval.focus();
            return false;
        }
        if (month == 02) {

            var isleap = (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0));
            if (day > 29 || (day == 29 && !isleap)) {
                alert("February " + year + " doesn't have " + day + " days!");
                dateval.focus();
                return false;
            }
        }




        dateval.value = month + '/' + day + '/' + year;
        return true;

    }


}

// Prevent Past dates 
function checkdate(dateval) {

    if (dateval.value != '') {

        var day;
        var month;
        var year;

        var DateOfBirth = dateval.value;

        if (DateOfBirth.length == 8) {
            day = DateOfBirth.substring(0, 2);
            month = DateOfBirth.substring(2, 4);
            year = DateOfBirth.substring(4, 8);
        }
        else if (DateOfBirth.length == 10) {
            day = DateOfBirth.substring(0, 2);
            month = DateOfBirth.substring(3, 5);
            year = DateOfBirth.substring(6, 10);
        }
        else {
            alert('Invalid Date Format: Please enter DDMMYYYY!');
            dateval.focus();
            return false;
        }

        if (day.length == 2 && month.length == 2 && year.length == 4) {
            flag = true;
        }
        else {
            alert('Invalid Date Format: Please enter DDMMYYYY!');
            dateval.focus();
            return false;
        }


        var d = new Date();
        var currentYear = d.getFullYear();

        if (year.length != 4)
        { dateval.focus(); return false; }


        if (IsNumeric(month)) {
            if (month > 0 && month < 13) {
                flag = true;
            }
            else {
                alert('Months should be in between 1 - 12');
                dateval.focus();
                return false;
            }
        }
        else {
            alert('Months should be in digits');
            dateval.focus();
            return false;
        }

        if (IsNumeric(day)) {
            if (day > 0 && day < 32) {
                flag = true;
            }
            else {
                alert('Invalid Date Format: Days should be in between 1 - 31');
                dateval.focus();
                return false;
            }
        }
        else {
            alert('Days should be in digits');
            dateval.focus();
            return false;
        }


        if ((month == 4 || month == 6 || month == 9 || month == 11) && day == 31) {
            alert("Month " + month + " doesn't have 31 days!")
            dateval.focus();
            return false;
        }
        if (month == 02) {

            var isleap = (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0));
            if (day > 29 || (day == 29 && !isleap)) {
                alert("February " + year + " doesn't have " + day + " days!");
                dateval.focus();
                return false;
            }
        }

        var selecteddate, rightNow, date1;
        selecteddate = month + '/' + day + '/' + year;
        rightNow = new Date();
        date1 = new Date(selecteddate);

        rightNow.setMinutes(0);
        rightNow.setSeconds(0);
        rightNow.setHours(0);
        rightNow.setMilliseconds(0);

//        if (date1 < rightNow) {

//            alert("You can't select day earlier than today!");
//            dateval.value = '';
//            dateval.focus();
//            return false;
//        }

        dateval.value = day + '/' + month + '/' + year;
        return true;
    }
}

function checklastDate(dateval) {

    if (dateval.value != '') {

        var day;
        var month;
        var year;

        var DateOfBirth = dateval.value;

        if (DateOfBirth.length == 8) {
            day = DateOfBirth.substring(0, 2);
            month = DateOfBirth.substring(2, 4);
            year = DateOfBirth.substring(4, 8);
        }
        else if (DateOfBirth.length == 10) {
            day = DateOfBirth.substring(0, 2);
            month = DateOfBirth.substring(3, 5);
            year = DateOfBirth.substring(6, 10);
        }
        else {
            alert('Invalid Date Format: Please enter DDMMYYYY!');
            dateval.focus();
            return false;
        }

        if (day.length == 2 && month.length == 2 && year.length == 4) {
            flag = true;
        }
        else {
            alert('Invalid Date Format: Please enter DDMMYYYY!');
            dateval.focus();
            return false;
        }


        var d = new Date();
        var currentYear = d.getFullYear();

        if (year.length != 4)
        { dateval.focus(); return false; }


        if (IsNumeric(month)) {
            if (month > 0 && month < 13) {
                flag = true;
            }
            else {
                alert('Months should be in between 1 - 12');
                dateval.focus();
                return false;
            }
        }
        else {
            alert('Months should be in digits');
            dateval.focus();
            return false;
        }

        if (IsNumeric(day)) {
            if (day > 0 && day < 32) {
                flag = true;
            }
            else {
                alert('Invalid Date Format: Days should be in between 1 - 31');
                dateval.focus();
                return false;
            }
        }
        else {
            alert('Days should be in digits');
            dateval.focus();
            return false;
        }


        if ((month == 4 || month == 6 || month == 9 || month == 11) && day == 31) {
            alert("Month " + month + " doesn't have 31 days!")
            dateval.focus();
            return false;
        }
        if (month == 02) {

            var isleap = (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0));
            if (day > 29 || (day == 29 && !isleap)) {
                alert("February " + year + " doesn't have " + day + " days!");
                dateval.focus();
                return false;
            }
        }

        var selecteddate, rightNow, date1;
        selecteddate = month + '/' + day + '/' + year;
        rightNow = new Date();
        date1 = new Date(selecteddate);

        rightNow.setMinutes(0);
        rightNow.setSeconds(0);
        rightNow.setHours(0);
        rightNow.setMilliseconds(0);

        if (date1 > rightNow) {
            alert("You can't select future date!");
            dateval.value = '';
            dateval.focus();
            return false;

        }
        else {

            return true;

            dateval.value = day + '/' + month + '/' + year;
        }

    }


}

// to check date format starts DD MMMM
function validateDateDD_MM(dateval) {
    
    if (dateval.value != '') {

        var day;
        var month= 0;

        var datesplit = dateval.value.split(' ');

        if (datesplit.length != 2) {
            //alert('Invalid Date Format');
            alert('Invalid Date Format,Date should be like (Date Month) format');
            $("#" + dateval.id).val('');
            $("#" + dateval.id).focus();
            return false;
        }

        var DateOfBirth = dateval.value;

        var MonthList = {
            "January": 1,
            "February": 2,
            "March": 3,
            "April": 4,
            "May": 5,
            "June": 6,
            "July": 7,
            "August": 8,
            "September": 9,
            "October": 10,
            "November": 11,
            "December": 12
        }

        for (var a in MonthList) {
            if (a.toString().toLowerCase() == datesplit[1].toLowerCase()) {
                month = MonthList[a];
                break;
            }
        }

        if (month == 0) {
            alert('Invalid Date Format,Date should be like (Date Month) format');
            $("#" + dateval.id).val('');
            $("#" + dateval.id).focus();
            
            return false;
        }



        day = datesplit[0];
        var d = new Date();
        var currentYear = d.getFullYear();



        if (IsNumeric(month)) {
            if (month > 0 && month < 13) {
                flag = true;
            }
            else {
                alert('Months should be in between 1 - 12');
                $("#" + dateval.id).val('');
                $("#" + dateval.id).focus();
                //dateval.focus();
                return false;
            }
        }
        else {
            alert('Months should be in digits');
            $("#" + dateval.id).val('');
            $("#" + dateval.id).focus();

            //dateval.focus();
            return false;
        }

        if (IsNumeric(day)) {
            if (day > 0 && day < 32) {
                flag = true;
            }
            else {
                alert('Invalid Date Format: Days should be in between 1 - 31');
                $("#" + dateval.id).val('');
                $("#" + dateval.id).focus();
                //dateval.focus();
                return false;
            }
        }
        else {
            alert('Days should be in digits');
            $("#" + dateval.id).val('');
            $("#" + dateval.id).focus();
            //dateval.focus();
            return false;
        }


        if ((month == 4 || month == 6 || month == 9 || month == 11) && day == 31) {
            alert("Month " + month + " doesn't have 31 days!")
            $("#" + dateval.id).val('');
            $("#" + dateval.id).focus();
            //dateval.focus();
            return false;
        }
        if (month == 02) {

            var isleap = (currentYear % 4 == 0 && (currentYear % 100 != 0 || currentYear % 400 == 0));
            if (day > 29 || (day == 29 && !isleap)) {
                // if (day > 29) {
                alert("February doesn't have " + day + " days!");
                $("#" + dateval.id).val('');
                $("#" + dateval.id).focus();
                //dateval.focus();
                return false;
            }
        }

        //dateval.value = day + '/' + month;

        return true;

    }


}

// to check date format DD MMMM yyyy
function validateDateDD_MM_YYYY(dateval) {

    if (dateval.value != '') {

        var datesplit = dateval.value.split(' ');

        if (datesplit.length != 3) {
            alert('Invalid Date Format,Date should be like (Date Month Year) format');
            $("#" + dateval.id).val('');
            $("#" + dateval.id).focus();
            return false;
        }


        var day;
        var month = 0;

        var DateOfBirth = dateval.value;

        var MonthList = {
            "January": 1,
            "February": 2,
            "March": 3,
            "April": 4,
            "May": 5,
            "June": 6,
            "July": 7,
            "August": 8,
            "September": 9,
            "October": 10,
            "November": 11,
            "December": 12
        }

        for (var a in MonthList) {
            if (a.toString().toLowerCase() == datesplit[1].toLowerCase()) {
                month = MonthList[a];
                break;
            }
        }

        if (month == 0) {
            alert('Invalid Date Format,Date should be like (Date Month Year) format');
            $("#" + dateval.id).val('');
            $("#" + dateval.id).focus();
            return false;
        }

        day = dateval.value.split(' ')[0];
        var d = new Date();

        var currentYear = datesplit[2];



        if (IsNumeric(month)) {
            if (month > 0 && month < 13) {
                flag = true;
            }
            else {
                alert('Months should be in between 1 - 12');
                $("#" + dateval.id).val('');
                $("#" + dateval.id).focus();
                //dateval.focus();
                return false;
            }
        }
        else {
            alert('Months should be in digits');
            $("#" + dateval.id).val('');
            $("#" + dateval.id).focus();

            //dateval.focus();
            return false;
        }

        if (IsNumeric(day)) {
            if (day > 0 && day < 32) {
                flag = true;
            }
            else {
                alert('Invalid Date Format: Days should be in between 1 - 31');
                $("#" + dateval.id).val('');
                $("#" + dateval.id).focus();
                //dateval.focus();
                return false;
            }
        }
        else {
            alert('Days should be in digits');
            $("#" + dateval.id).val('');
            $("#" + dateval.id).focus();
            //dateval.focus();
            return false;
        }


        if ((month == 4 || month == 6 || month == 9 || month == 11) && day == 31) {
            alert("Month " + month + " doesn't have 31 days!")
            $("#" + dateval.id).val('');
            $("#" + dateval.id).focus();
            //dateval.focus();
            return false;
        }
        if (month == 02) {

            var isleap = (currentYear % 4 == 0 && (currentYear % 100 != 0 || currentYear % 400 == 0));
            if (day > 29 || (day == 29 && !isleap)) {
                // if (day > 29) {
                alert("February doesn't have " + day + " days!");
                $("#" + dateval.id).val('');
                $("#" + dateval.id).focus();
                //dateval.focus();
                return false;
            }
        }

        //dateval.value = day + '/' + month;

        return true;

    }


}


// to check date format starts DDMMYYYY
function validateDateDDMMYYYY(dateval) {

    if (dateval.value != '') {

        var day;
        var month;
        var year;

        var DateOfBirth = dateval.value;

        if (DateOfBirth.length == 8) {
            day = DateOfBirth.substring(0, 2);
            month = DateOfBirth.substring(2, 4);
            year = DateOfBirth.substring(4, 8);
        }
        else if (DateOfBirth.length == 10) {
            day = DateOfBirth.substring(0, 2);
            month = DateOfBirth.substring(3, 5);
            year = DateOfBirth.substring(6, 10);
        }
        else {
            alert('Invalid Date Format: Please enter DDMMYYYY!');
            dateval.focus();
            return false;
        }

        if (day.length == 2 && month.length == 2 && year.length == 4) {
            flag = true;
        }
        else {
            alert('Invalid Date Format: Please enter DDMMYYYY!');
            dateval.focus();
            return false;
        }


        var d = new Date();
        var currentYear = d.getFullYear();

        if (year.length != 4)
        { dateval.focus(); return false; }

        if (IsNumeric(year)) {
            if (year > 0) {
                flag = true;
            }
            //            else {
            //                alert('Year should be in Between 1753 and Current year');
            //                dateval.focus();
            //                return false;
            //            }

        }
        else {
            dateval.focus();
            return false;
        }

        //        if (year > 1753 && year <= currentYear) {
        //            flag = true;
        //        }
        //        else {
        //            alert('Year should be in Between 1753 and Current year');
        //            dateval.focus();
        //            return false;
        //        }

        if (IsNumeric(month)) {
            if (month > 0 && month < 13) {
                flag = true;
            }
            else {
                alert('Months should be in between 1 - 12');
                dateval.focus();
                return false;
            }
        }
        else {
            alert('Months should be in digits');
            dateval.focus();
            return false;
        }

        if (IsNumeric(day)) {
            if (day > 0 && day < 32) {
                flag = true;
            }
            else {
                alert('Invalid Date Format: Days should be in between 1 - 31');
                dateval.focus();
                return false;
            }
        }
        else {
            alert('Days should be in digits');
            dateval.focus();
            return false;
        }


        if ((month == 4 || month == 6 || month == 9 || month == 11) && day == 31) {
            alert("Month " + month + " doesn't have 31 days!")
            dateval.focus();
            return false;
        }
        if (month == 02) {

            var isleap = (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0));
            if (day > 29 || (day == 29 && !isleap)) {
                alert("February " + year + " doesn't have " + day + " days!");
                dateval.focus();
                return false;
            }
        }

        dateval.value = day + '/' + month + '/' + year;

        return true;

    }


}
// to check date format starts DDMM 
function validateDateDDMM(dateval) {

    if (dateval.value != '') {

        var day;
        var month;

        var DateOfBirth = dateval.value;

        if (DateOfBirth.length == 4) {
            day = DateOfBirth.substring(0, 2);
            month = DateOfBirth.substring(2, 4);
        }
        else if (DateOfBirth.length == 5) {
            day = DateOfBirth.substring(0, 2);
            month = DateOfBirth.substring(3, 5);

        }
        else {
            alert('Invalid Date Format: Please enter DDMM!');
            dateval.focus();
            return false;
        }

        if (day.length == 2 && month.length == 2) {
            flag = true;
        }
        else {
            alert('Invalid Date Format: Please enter DDMM!');
            dateval.focus();
            return false;
        }


        var d = new Date();
        var currentYear = d.getFullYear();



        if (IsNumeric(month)) {
            if (month > 0 && month < 13) {
                flag = true;
            }
            else {
                alert('Months should be in between 1 - 12');
                $("#" + dateval.id).focus();
                //dateval.focus();
                return false;
            }
        }
        else {
            alert('Months should be in digits');
            $("#" + dateval.id).focus();
            //dateval.focus();
            return false;
        }

        if (IsNumeric(day)) {
            if (day > 0 && day < 32) {
                flag = true;
            }
            else {
                alert('Invalid Date Format: Days should be in between 1 - 31');
                $("#" + dateval.id).focus();
                //dateval.focus();
                return false;
            }
        }
        else {
            alert('Days should be in digits');
            $("#" + dateval.id).focus();
            //dateval.focus();
            return false;
        }


        if ((month == 4 || month == 6 || month == 9 || month == 11) && day == 31) {
            alert("Month " + month + " doesn't have 31 days!")
            $("#" + dateval.id).focus();
            //dateval.focus();
            return false;
        }
        if (month == 02) {

            //var isleap = (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0));
            //  if (day > 29 || (day == 29 && !isleap))
            if (day > 29) {
                alert("February doesn't have " + day + " days!");
                $("#" + dateval.id).focus();
                //dateval.focus();
                return false;
            }
        }

        dateval.value = day + '/' + month;

        return true;

    }


}

function IsNumeric(sText) {
    var ValidChars = "0123456789.";
    var IsNumber = true;
    var Char;

    for (i = 0; i < sText.length && IsNumber == true; i++) {
        Char = sText.charAt(i);
        if (ValidChars.indexOf(Char) == -1) {
            IsNumber = false;
        }
    }

    return IsNumber;
}
