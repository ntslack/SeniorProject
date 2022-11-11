
var ReminderViewModel = function (userID) {
    var self = this;

    self.userRemindersObject = ko.observable({
        reminderID: ko.observable(),
        userID: ko.observable(),
        reminderTitle: ko.observable(),
        reminderDescription: ko.observable(),
        reminderCreationDate: ko.observable()
    });

    self.userReminders = ko.observableArray([]);

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
                    toastr.success(reminderData.reminderTitle + " was created");
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
        return;
    }

    var Id;
    self.editReminder = function (Object) {
        console.log(Object);
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
                toastr.error("Error Updating Note")
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
                toastr.success("Reminder " + Object.reminderTitle + " was deleted");
            },
            error: function (jqXHR) {
                toastr.error(jqXHR.responseText);
            }
        })
    }
}