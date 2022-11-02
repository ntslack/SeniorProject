﻿$("#search").keyup(function () {
    var searchText = $(this).val().toLowerCase();
    $.each($("#notes div"), function () {
        if ($(this).text().toLowerCase().indexOf(searchText) === -1)
            $(this).hide();
        else
            $(this).show();
    });
});

var NotesViewModel = function (userID) {
    var self = this;

    self.userNotesObject = ko.observable({
        noteID: ko.observable(),
        userID: ko.observable(),
        noteTitle: ko.observable(),
        noteValue: ko.observable(),
        noteCreationDate: ko.observable(),
        noteIsFavorited: ko.observable()
    });

    self.userNotes = ko.observableArray([]);

    self.getUserNotes = function () {
        $.ajax({
            url: "/Home/notes/?userID=" + userID,
            type: "GET",
            success: function (data) {
                self.userNotes(data);
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        })
    }

    self.createUserNote = function (noteData) {
        $.ajax({
            url: "/Home/notes",
            type: "POST",
            datatype: "json",
            contentType: "application/json",
            data: JSON.stringify(noteData),
            success: function (result) {
                if (result == -1) {
                    toastr.error("Note was not successfully created");
                } else {
                    toastr.success(noteData.noteTitle + " was created");
                    self.getUserNotes();
                }
            },
            error: function (result) {
                toastr.error(result);
            }
        })
    }

    self.submitUserNote = function () {
        var noteTitle = $("#noteTitle").val();
        var noteValue = $("#noteValue").val();
        let payload = {
            UserID: userID,
            NoteTitle: noteTitle,
            NoteValue: noteValue
        };
        self.createUserNote(payload);
        $("#createNoteModal").modal("toggle");
        return;
    }

    self.deleteNote = function (Object) {
        $.ajax({
            url: '/Home/notes/' + Object.noteID,
            type: 'DELETE',
            success: function () {
                //self.userExpenses.remove(selectedExpense);
                self.getUserNotes();
                toastr.success("Note " + Object.noteTitle + " was deleted");
            },
            error: function (jqXHR) {
                toastr.error(jqXHR.responseText);
            }
        })
    }
}