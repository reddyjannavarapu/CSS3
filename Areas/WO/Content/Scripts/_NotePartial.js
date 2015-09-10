
$(document).ready(function () {
    $GlobalDataNote = {};
    $GlobalDataNote.InsertedID = 0;
    $GlobalDataNote.totalRow1 = 10;
    $GlobalDataNote.resultPerPage1 = 10;
    $GlobalDataNote.startPage = 0;

    $GlobalDataNote.ReferId = 0;
    $GlobalDataNote.Type = '';
    $GlobalDataNote.NoteID = 0;
    $GlobalDataNote.popupContent = '';
    $GlobalDataNote.NoteCount = '';
    $('.ace-scroll').ace_scroll({ size: 300 });


    //var txtNoteDescription = $('#txtNoteDescription');
    //textareaLimiter(txtNoteDescription, 250);

    $('.spnNoteCount').each(function () {
        try {
            var count = $(this).text();
            if (count == 0) {
                $(this).hide();
            }
            else {
                $(this).show();
            }

        } catch (e) {
            console.log(e);
        }
    });

    $('.NoteAddInList').unbind('click');
    $('.NoteAddInList').click(function () {
        try {
            var popupContent = $(this).siblings('.NoteContentForPopup');
            $GlobalDataNote.popupContent = popupContent;
            $GlobalDataNote.NoteCount = $(this).find('.spnNoteCount');
            ClearValuesInNotePartial();

            var Page = window.location.href.split('/').pop();

            var Type = $(this).attr('notetype');
            var RefID = $(this).attr('refid');

            GetNoteByRefIdAndType(RefID, Type);

            $GlobalDataNote.popupContent.find('.close-btn').unbind('click');
            $GlobalDataNote.popupContent.find('.close-btn').bind('click', closeDialog);

            $GlobalDataNote.popupContent.find('#txtNoteDescription').unbind('keypress').keypress(NoteTextDisableEnter);
            $GlobalDataNote.popupContent.find('.btnAddNote').unbind('click').click(SaveNoteClick);

        } catch (e) {
            console.log(e);
        }
    });

    function closeDialog() {
        try {
            $('body').css('overflow', 'auto');
            $GlobalDataNote.popupContent.dialog('close');
            $GlobalDataNote.popupContent.dialog('destroy');
        } catch (e) {
            console.log(e);
        }
    }

    function NoteTextDisableEnter(e) {
        if (e.which == 13) {
            $('.btnAddNote').trigger('click');
            return false;
        }
    }

    function SaveNoteClick() {
        try {
            $GlobalDataNote.NoteID = 0;
            var Description = $(this).parent().parent().find('#txtNoteDescription').val();
            InsertOrUpdateNote(Description, 'Add');
        } catch (e) {
            console.log(e);
        }
    }
});

function GetNoteByRefIdAndType(ReferId, Type) {
    try {
        $GlobalDataNote.ReferId = ReferId;
        $GlobalDataNote.Type = Type;

        $('#dvPartial').show();
        $('.spnType').text(Type);
        $('.spnRefId').text(ReferId);

        CallNotes("/Note/GetNoteData", 'NoteTemplate', 'trNoteData', "{'ReferId':" + ReferId + ",'Type':'" + Type + "','startpage':" + $GlobalDataNote.startPage + ",'rowsperpage':" + $GlobalDataNote.resultPerPage1 + "}", true, NoteDataLoadCallBack);
    } catch (e) {
        console.log(e);
    }
}

function NoteDataLoadCallBack() {
    try {
        $('.ace-scroll').ace_scroll('reset');
        //$('.ace-scroll').ace_scroll({ size: 300 });

        //$GlobalDataNote.popupContent.dialog({ autoOpen: false, resizable: false, modal: true, width: 740.833, closeOnEscape: false });
        $GlobalDataNote.popupContent.dialog({ autoOpen: false, resizable: false, modal: true, width: '52%', dialogClass: 'no-padding NoteBorderRadius', closeOnEscape: false, minHeight: '0px' });
        $GlobalDataNote.popupContent.dialog('open');
        $("body").css("overflow", "hidden");
        $('.ui-dialog-titlebar').hide();

        $('.aSave').unbind('click');
        $('.aSave').click(UpdateNote);

        $('.aDelete').unbind('click');
        $('.aDelete').click(DeleteNote);


    } catch (e) {
        console.log(e);
    }
}

function UpdateNote() {
    try {
        var NoteID = $(this).attr('noteid');
        $GlobalDataNote.NoteID = NoteID;

        var desc = $(this).parent().parent().find('.txtAreaDescription').val();
        if (desc != undefined && desc != '') {
            InsertOrUpdateNote(desc, 'Update');
        }
        else {
            ShowNotify('Note is required.', 'error', 3000);
            return false;
        }
    } catch (e) {
        console.log(e);
    }
}

function InsertOrUpdateNote(Description, Action) {
    try {
        var Type = $('#spnType').text();
        var ReferId = $('#spnRefId').text();

        if ($.trim(Description) == '' || Description == undefined) {
            ShowNotify('Note is required.', 'error', 3000);
            return false;
        }
        else {
            CallNotes("/Note/InsertNote", '', '', "{'Type':'" + Type + "','ReferId':" + ReferId + ",'Description':'" + escape($.trim(Description)) + "','ID':'" + $GlobalDataNote.NoteID + "','Action':'" + Action + "'}", true, CreateCallBack);
        }
    } catch (e) {
        console.log(e);
    }
}

function CreateCallBack() {
    try {
        if ($GlobalDataNote.InsertedID == 0) {
            ShowNotify('Invalid Session login again.', 'error', 3000);
            return false;
        }
        else {
            if ($GlobalDataNote.NoteID == 0) {
                ShowNotify('Success.', 'success', 2000);
            }
            else {
                ShowNotify('Success.', 'success', 2000);
            }

            ClearValuesInNotePartial();
            GetNoteByRefIdAndType($GlobalDataNote.ReferId, $GlobalDataNote.Type);
            return false;
        }
    } catch (e) {
        console.log(e);
    }
}

function DeleteNote() {
    try {
        if (confirm('Are you sure you want to delete...?')) {
            var NoteID = $(this).attr('NoteID');

            CallNotes("/Note/DeleteNote", '', '', "{'ID':" + NoteID + "}", true, NoteDeleteCallBack);
        }
    } catch (e) {
        console.log(e);
    }
}

function NoteDeleteCallBack() {
    try {
        if ($GlobalDataNote.InsertedID == 0) {
            ShowNotify('Invalid Session login again.', 'error', 3000);
            return false;
        }
        else {
            ShowNotify('Success.', 'success', 2000);
            ClearValuesInNotePartial();
            GetNoteByRefIdAndType($GlobalDataNote.ReferId, $GlobalDataNote.Type);
            return false;
        }
    } catch (e) {
        console.log(e);
    }
}

function ClearValuesInNotePartial() {
    try {
        $GlobalDataNote.popupContent.find('#txtNoteDescription').val('');
    } catch (e) {
        console.log(e);
    }
}

function CallNotes(path, templateId, containerId, parameters, clearContent, callBack) {
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

                    if (msg == '0') {
                        ShowNotify('Invalid Session login again.', 'error', 3000);
                        return false;
                    }
                    $GlobalDataNote.InsertedID = msg;

                    if (msg != 1) {

                        $GlobalDataNote.popupContent.find('.scroll-content').empty();
                        var tr = '';
                        for (var i = 0; i < msg.NoteList.length; i++) {
                            tr = '';
                            tr += '<div class="itemdiv dialogdiv"><div class="body"> <div class="time" style="margin-right:15%;"><i class="ace-icon fa fa-clock-o">';
                            //tr += '</i><span class="green">' + msg.NoteList[i].UpdatedDate + '</span></div><div class="name"><a href="#">' + msg.NoteList[i].Name + '</a></div>';
                            tr += '</i> <span class="green">' + msg.NoteList[i].UpdatedDate + '</span></div><div class="name"><label class="blue">' + msg.NoteList[i].Name + '</label></div>';


                            if (msg.NoteList[i].IsLoggedUserForAction == 1) {
                                tr += '<div class="text"><textarea class="txtAreaDescription special"  maxlength="500" type="text" style="width:85% !important;max-width:85% !important" noteid="' + msg.NoteList[i].ID + '" >' + msg.NoteList[i].Description + '</textarea>';
                                //tr += '<div class="tools"> <a class="btn btn-minier btn-info" href="#"><i class="icon-only ace-icon fa fa-share"></i></a></div>';
                                tr += '<a style="margin-left: 2%;outline: none;" class="aSave green" href="javascript:void(0)" noteid="' + msg.NoteList[i].ID + '"><i style="margin-top:2%;" class="btn btn-xs btn-success ace-icon fa fa-check bigger-110"></i></a>';
                                tr += '<a style="margin-left: 1%;outline: none;" class="aDelete red" href="javascript:void(0)" noteid="' + msg.NoteList[i].ID + '"><i style="margin-top:2%;" class="btn btn-xs btn-danger ace-icon fa fa-trash-o bigger-110"></i></a></div></div>';
                            }
                            else {
                                tr += '<div class="text"><textarea class="txtAreaDescription" type="text" style="width:85% !important;max-width:85% !important" disabled="disabled" >' + msg.NoteList[i].Description + '</textarea></div>';
                            }

                            tr += '</div></div>';

                            $GlobalDataNote.popupContent.find('.scroll-content').append(tr);

                        }

                        if (msg.NoteList.length > 0) {
                            $GlobalDataNote.popupContent.find('.NoteListContent').show();
                            $GlobalDataNote.NoteCount.show();
                            $GlobalDataNote.NoteCount.text(msg.NoteList.length);
                        }
                        else {
                            $GlobalDataNote.popupContent.find('.NoteListContent').hide();
                            $GlobalDataNote.NoteCount.hide();
                            $GlobalDataNote.NoteCount.text(msg.NoteList.length);
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