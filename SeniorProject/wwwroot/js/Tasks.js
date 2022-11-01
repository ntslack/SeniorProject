
var TaskViewModel = function (userID) {
    var self = this;

    self.userTasksObject = ko.observable({
        taskID: ko.observable(),
        userID: ko.observable(),
        taskValue: ko.observable(),
        taskCreationDate: ko.observable(),
        taskIsFavorited: ko.observable()
    });

    self.userTasks = ko.observableArray([]);

    self.getUserTasks = function () {
        $.ajax({
            url: "/Home/tasks/?userID=" + userID,
            type: "GET",
            success: function (data) {
                self.userTasks(data);
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