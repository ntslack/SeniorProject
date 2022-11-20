
var ReminderViewModel = function (userID) {
    var self = this;

    self.userRemindersObject = ko.observable({
        reminderID: ko.observable(),
        userID: ko.observable(),
        reminderTitle: ko.observable(),
        reminderDescription: ko.observable(),
        reminderCreationDate: ko.observable()
    });

    self.userListsObject = ko.observable({
        listID: ko.observable(),
        userID: ko.observable(),
        listName: ko.observable(),
        listDescription: ko.observable(),
        listIsFavorited: ko.observable(),
        listCreationDate: ko.observable(),
        listItems: ko.observableArray([{
            listItemID: ko.observable(),
            listID: ko.observable(),
            listItemValue: ko.observable()
        }])
    });

    self.userReminders = ko.observableArray([]);

    self.favoritedNotes = ko.observableArray([]);
    self.favoritedLists = ko.observableArray([]);
    self.favoritedEvents = ko.observableArray([]);

    self.userListItems = ko.observableArray([]);

    self.dismissEditModal = function () {
        $("#editReminderModal").modal("toggle");
    }

    self.getUserReminders = function () {
        $.ajax({
            url: "/Home/reminders/?userID=" + userID,
            type: "GET",
            success: function (data) {
                self.userReminders(data);
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        })
    }

    self.getFavoritedNotes = function () {
        $.ajax({
            url: "/Home/favnotes/?userID=" + userID,
            type: "GET",
            success: function (data) {
                self.favoritedNotes(data);
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        })
    }

    self.getFavoritedLists = function () {
        $.ajax({
            url: "/Home/favlists/?userID=" + userID,
            type: "GET",
            success: function (data) {
                self.favoritedLists(data);
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        })
    }

    self.getFavoritedEvents = function () {
        $.ajax({
            url: "/Home/favevents/?userID=" + userID,
            type: "GET",
            success: function (data) {
                self.favoritedEvents(data);
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        })
    }

    self.editListItems = function (Object) {
        ko.mapping.fromJS(Object, {}, self.userListsObject);
        self.getUserListItems(self.userListsObject().listID());
    }

    self.getUserListItems = function (listID) {
        $.ajax({
            url: "/Home/listitems/?listID=" + listID,
            type: "GET",
            success: function (result) {
                self.userListItems(result);
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        })
    }

    self.deleteListItem = function (Object) {
        $.ajax({
            url: '/Home/listitems/' + Object.listItemID,
            type: 'DELETE',
            success: function () {
                self.getUserListItems(Object.listID);
                toastr.success("Item was deleted");
            },
            error: function (jqXHR) {
                toastr.error(jqXHR.responseText);
            }
        })
    }

    self.createUserReminder = function (reminderData) {
        $.ajax({
            url: "/Home/reminders",
            type: "POST",
            datatype: "json",
            contentType: "application/json",
            data: JSON.stringify(reminderData),
            success: function (result) {
                if (result == -1) {
                    toastr.error("Reminder was not successfully created");
                } else {
                    toastr.success("Reminder was created");
                    self.getUserReminders();
                }
            },
            error: function (result) {
                toastr.error(result);
            }
        })
    }

    self.submitUserReminder = function () {
        var reminderTitle = $("#reminderTitle").val();
        var reminderDescription = $("#reminderDescription").val();
        let payload = {
            UserID: userID,
            ReminderTitle: reminderTitle,
            ReminderDescription: reminderDescription
        };
        self.createUserReminder(payload);
        $("#createReminderModal").modal("toggle");
        $("#createReminderModal").on("hidden.bs.modal", function () {
            $("#createReminderForm").find("input, textarea, select").val('').end();
        });
        return;
    }

    var Id;
    self.editReminder = function (Object) {
        ko.mapping.fromJS(Object, {}, self.userRemindersObject);
        Id = Object.reminderID;
    }

    self.submitEditedUserReminder = function () {
        var reminderID = Id;
        var reminderTitle = $("#editReminderTitle").val();
        var reminderDescription = $("#editReminderDescription").val();

        let payload2 = {
            userId: userID,
            reminderId: reminderID,
            remindertitle: reminderTitle,
            reminderdescription: reminderDescription
        }

        self.updateReminder(payload2);
    }

    self.updateReminder = function (payload2) {
        $.ajax({
            url: "/Home/reminders",
            type: "PUT",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(payload2),
            success: function (result) {
                if (result == -1) {
                    toastr.error("Error");
                } else {
                    toastr.success("Successfully Changed Reminder");
                    self.getUserReminders();
                    self.dismissEditModal();
                }
            },
            error: function () {
                toastr.error("Error Updating Reminder")
            }
        })
    }

    self.deleteReminder = function (Object) {
        $.ajax({
            url: '/Home/reminders/' + Object.reminderID,
            type: 'DELETE',
            success: function () {
                //self.userExpenses.remove(selectedExpense);
                self.getUserReminders();
                toastr.success("Reminder  was deleted");
            },
            error: function (jqXHR) {
                toastr.error(jqXHR.responseText);
            }
        })
    }

    self.unfavoriteNote = function (Object) {
        $.ajax({
            url: "/Home/favnotes",
            type: "PUT",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(Object),
            success: function (result) {
                if (result == -1) {
                    toastr.error("Error");
                } else {
                    toastr.success("Success!");
                    self.getFavoritedNotes();
                }
            },
            error: function () {
                toastr.error("Error Updating Note")
            }
        })
    }

    self.unfavoriteList = function (Object) {
        $.ajax({
            url: "/Home/favlists",
            type: "PUT",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(Object),
            success: function (result) {
                if (result == -1) {
                    toastr.error("Error");
                } else {
                    toastr.success("Success!");
                    self.getFavoritedLists();
                }
            },
            error: function () {
                toastr.error("Error Updating List")
            }
        })
    }

    self.unfavoriteEvent = function (Object) {
        console.log(Object);
        $.ajax({
            url: "/Home/favevents",
            type: "PUT",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(Object),
            success: function (result) {
                if (result == -1) {
                    toastr.error("Error");
                } else {
                    toastr.success("Success!");
                    self.getFavoritedEvents();
                }
            },
            error: function () {
                toastr.error("Error Updating Event")
            }
        })
    }
}