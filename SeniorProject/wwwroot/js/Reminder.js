
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
}