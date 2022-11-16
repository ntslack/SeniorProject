$("#search").keyup(function () {
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

    self.dismissEditModal = function () {
        $("#editNoteModal").modal("toggle");
    }

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
        console.log(noteData);
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
                    toastr.success("Note successfully created");
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

    var Id;
    self.editNote = function (Object) {
        console.log(Object);
        ko.mapping.fromJS(Object, {}, self.userNotesObject);
        Id = Object.noteID;
    }

    self.submitEditedUserNote = function () {
        var noteID = Id;
        var noteTitle = $("#editNoteTitle").val();
        var noteValue = $("#editNoteValue").val();

        let payload2 = {
            userId: userID,
            noteId: noteID,
            notetitle: noteTitle,
            notevalue: noteValue
        }

        self.updateNote(payload2);
    }

    self.updateNote = function (payload2) {
        $.ajax({
            url: "/Home/notes",
            type: "PUT",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(payload2),
            success: function(result) {
                if (result == -1) {
                    toastr.error("Error");
                } else {
                    toastr.success("Successfully Changed Note");
                    self.getUserNotes();
                    self.dismissEditModal();
                }
            },
            error: function () {
                toastr.error("Error Updating Note")
            }
        })
    }

    self.deleteNote = function (Object) {
        $.ajax({
            url: '/Home/notes/' + Object.noteID,
            type: 'DELETE',
            success: function () {
                self.getUserNotes();
                toastr.success("Note " + Object.noteTitle + " was deleted");
            },
            error: function (jqXHR) {
                toastr.error(jqXHR.responseText);
            }
        })
    }
}