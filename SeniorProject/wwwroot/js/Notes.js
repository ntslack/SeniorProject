var NotesViewModel = function () {
    var self = this;

    self.userNotesObject = ko.observable({
        id: ko.observable(),
        userID: ko.observable(),
        noteTitle: ko.observable(),
        noteValue: ko.observable(),
        noteCreationDate: ko.observable(),
        noteIsFavorited: ko.observable()
    });

    console.log(self.userNotesObject())

    self.userNotes = ko.observableArray([]);

    self.getUserNotes = function () {
        $.ajax({
            url: "/api/notes",
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
}