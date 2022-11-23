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
        listCreationDate: ko.observable(),
        listItems: ko.observableArray([{
            listItemID: ko.observable(),
            listID: ko.observable(),
            listItemValue: ko.observable()
        }])
    });

    self.userListItemsObject = ko.observable({
        listItemID: ko.observable(),
        listID: ko.observable(),
        listItemValue: ko.observable(),
        listItemCreationDate: ko.observable()
    });

    self.userLists = ko.observableArray([]);
    self.userListItems = ko.observableArray([]);

    self.noLists = ko.observable(false);
    self.listsAvailable = ko.observable(false);

    self.dismissEditModal = function () {
        $("#editListModal").modal("toggle");
    }

    self.getUserLists = function () {
        $.ajax({
            url: "/Home/lists/?userID=" + userID,
            type: "GET",
            success: function (response) {
                ko.mapping.fromJS(response, {}, self.userLists);
                if (self.userLists().length < 1) {
                    self.noLists(true);
                } else {
                    self.listsAvailable(true);
                }
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
                    self.noLists(false);
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
        $("#createListModal").on("hidden.bs.modal", function () {
            $("#createListForm").find("input, textarea, select").val('').end();
        });
        return;
    }

    var Id;
    self.editList = function (Object) {
        ko.mapping.fromJS(Object, {}, self.userListsObject);
        console.log(self.userListsObject());
        Id = Object.listID();
    }

    self.submitEditedUserList = function () {
        console.log(self.userListsObject());
        var listID = Id;
        var listName = $("#editListName").val();
        var listDescription = $("#editListDescription").val();
        var favorited = self.userListsObject().listIsFavorited();

        let payload2 = {
            userId: userID,
            listId: listID,
            listname: listName,
            listdescription: listDescription,
            listisfavorited: favorited
        }

        self.updateList(payload2);
    }

    self.updateList = function (payload2) {
        $.ajax({
            url: "/Home/lists",
            type: "PUT",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(payload2),
            success: function (result) {
                if (result == -1) {
                    toastr.error("Error");
                } else {
                    toastr.success("Successfully Changed List");
                    self.getUserLists();
                    self.dismissEditModal();
                }
            },
            error: function () {
                toastr.error("Error Updating Expense")
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

    self.createUserListItem = function () {
        let listid = self.userListsObject().listID();
        let input = $("#createListItemInput").val();

        let payload3 = {
            listID: listid,
            listItemValue: input
        }

        if (input == '') {
            toastr.error("Can't create an empty item");
        }
        else if (self.userListItems().indexOf(input) != -1) {
            toastr.error(input + " already exists");
        }
        else {
            $.ajax({
                url: "/Home/listitems",
                type: "POST",
                datatype: "json",
                contentType: "application/json",
                data: JSON.stringify(payload3),
                success: function (result) {
                    if (result == -1) {
                        toastr.error("List Item was not successfully created");
                    } else {
                        self.getUserListItems(self.userListsObject().listID());
                        toastr.success("List Item was created");
                        $("#createListItemInput").val('');
                    }
                },
                error: function (result) {
                    toastr.error(result);
                }
            })
        }
    }

    self.deleteList = function (Object) {
        $.ajax({
            url: '/Home/lists/' + Object.listID(),
            type: 'DELETE',
            success: function () {
                self.getUserLists();
                toastr.success("List was deleted");
            },
            error: function (jqXHR) {
                toastr.error(jqXHR.responseText);
            }
        })
    }

    self.favoriteList = function (Object) {
        payload4 = {
            userId: Object.userID(),
            listId: Object.listID(),
            listName: Object.listName(),
            listDescription: Object.listDescription(),
            listIsFavorited: Object.listIsFavorited(),
            listCreationDate: Object.listCreationDate()
        }
        $.ajax({
            url: "/Home/favlists",
            type: "PUT",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(payload4),
            success: function (result) {
                if (result == -1) {
                    toastr.error("Error");
                } else {
                    if (payload4.listIsFavorited == true) {
                        toastr.success("List Unfavorited")
                    } else {
                        toastr.success("List Favorited");
                    }
                    self.getUserLists();
                }
            },
            error: function () {
                toastr.error("Error Updating List")
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
}