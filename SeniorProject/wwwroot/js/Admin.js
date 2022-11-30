$("#search").keyup(function () {
    var searchText = $(this).val().toLowerCase();
    $.each($("table tbody tr"), function () {
        if ($(this).text().toLowerCase().indexOf(searchText) === -1)
            $(this).hide();
        else
            $(this).show();
    });
});

var AdminViewModel = function () {
    var self = this;

    self.adminObject = ko.observable({
        userID: ko.observable(),
        username: ko.observable(),
        firstName: ko.observable(),
        lastName: ko.observable(),
        mobile: ko.observable(),
        isAdmin: ko.observable()
    });

    self.userInfo = ko.observableArray([]);

    self.dismissEditModal = function () {
        $("#editUserModal").modal("toggle");
    }

    self.dismissDeleteModal = function () {
        $("#deleteUserModal").modal("toggle");
    }

    self.getUserInfo = function () {
        $.ajax({
            url: "/Home/users",
            type: "GET",
            success: function (data) {
                self.userInfo(data);
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        })
    }

    var Id;
    self.editInfo = function (Object) {
        ko.mapping.fromJS(Object, {}, self.adminObject);
        Id = Object.userID;
    }

    self.submitEditedUserInfo = function () {
        var userID = Id;
        var username = $("#editUsername").val();
        var mobile = $("#editMobile").val();

        let payload2 = {
            userId: userID,
            userName: username,
            Mobile: mobile
        }

        self.updateUser(payload2);
    }

    self.updateUser = function (payload2) {
        $.ajax({
            url: "/Home/users",
            type: "PUT",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(payload2),
            success: function (result) {
                if (result == -1) {
                    toastr.error("Error");
                } else {
                    toastr.success("Successfully Changed Contact Info");
                    self.getUserInfo();
                    self.dismissEditModal();
                }
            },
            error: function (result) {
                toastr.error("Error Updating Contact Info")
            }
        })
    }

    self.deleteUser = function (Object) {
        $.ajax({
            url: '/Home/users/' + Object.userID,
            type: 'DELETE',
            success: function () {
                self.getUserInfo();
                toastr.success("User was deleted");
            },
            error: function (jqXHR) {
                toastr.error(jqXHR.responseText);
            }
        })
    }
}