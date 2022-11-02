$("#search").keyup(function () {
    var searchText = $(this).val().toLowerCase();
    $.each($("#lists div"), function () {
        if ($(this).text().toLowerCase().indexOf(searchText) === -1)
            $(this).hide();
        else
            $(this).show();
    });
});

var ListViewModel = function (userID) {
    var self = this;

    self.userListsObject = ko.observable({
        listID: ko.observable(),
        userID: ko.observable(),
        listName: ko.observable(),
        listDescription: ko.observable(),
        listIsFavorited: ko.observable(),
        listCreationDate: ko.observable()
    });

    self.userListItemsObject = ko.observable({
        listItemID: ko.observable(),
        listID: ko.observable(),
        listItemValue: ko.observable(),
        listItemCreationDate: ko.observable()
    });

    self.userLists = ko.observableArray([]);

    self.userListItems = ko.observableArray([]);

    self.getUserLists = function () {
        $.ajax({
            url: "/Home/lists/?userID=" + userID,
            type: "GET",
            success: function (data) {
                self.userLists(data);
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        })
    }

    self.createUserList = function (listData) {
        $.ajax({
            url: "/Home/lists",
            type: "POST",
            datatype: "json",
            contentType: "application/json",
            data: JSON.stringify(listData),
            success: function (result) {
                if (result == -1) {
                    toastr.error("List was not successfully created");
                } else {
                    toastr.success(listData.listTitle + " was created");
                    self.getUserLists();
                }
            },
            error: function (result) {
                toastr.error(result);
            }
        })
    }

    self.submitUserList = function () {
        var listName = $("#listName").val();
        var listDescription = $("#listDescription").val();
        let payload = {
            UserID: userID,
            ListName: listName,
            ListDescription: listDescription
        };
        self.createUserList(payload);
        $("#createListModal").modal("toggle");
        return;
    }

    self.getUserListItems = function () {
        $.ajax({
            url: "Home/listitems/?listID=" + listID,
            type: "GET",
            success: function (data) {
                self.userListItems(data);
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        })
    }

    self.createUserListItem = function (listItemData) {
        $.ajax({
            url: "/Home/listitems",
            type: "POST",
            datatype: "json",
            contentType: "application/json",
            data: JSON.stringify(listItemData),
            success: function (result) {
                if (result == -1) {
                    toastr.error("List Item was not successfully created");
                } else {
                    toastr.success("List Item was created");
                    self.getUserListItems();
                }
            },
            error: function (result) {
                toastr.error(result);
            }
        })
    }

    self.submitUserListItem = function () {
        var listItemValue = $("#listItemValue").val();
        let payload = {
            ListID: listID,
            ListItemValue: listItemValue
        };
        self.createUserListItem(payload);
        $("#createListItemModal").modal("toggle");
        return;
    }

    self.deleteList = function (Object) {
        $.ajax({
            url: '/Home/lists/' + Object.listID,
            type: 'DELETE',
            success: function () {
                self.getUserLists();
                toastr.success("List " + Object.listName + " was deleted");
            },
            error: function (jqXHR) {
                toastr.error(jqXHR.responseText);
            }
        })
    }
}