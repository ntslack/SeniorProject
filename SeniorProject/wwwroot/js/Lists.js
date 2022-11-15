﻿$("#search").keyup(function () {
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

    self.listItemValues = ko.observableArray();

    self.dismissEditModal = function () {
        $("#editListModal").modal("toggle");
    }

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

    var Id;
    self.editList = function (Object) {
        ko.mapping.fromJS(Object, {}, self.userListsObject);
        console.log(self.userListsObject());
        Id = Object.listID;
    }

    self.submitEditedUserList = function () {
        var listID = Id;
        var listName = $("#editListName").val();
        var listDescription = $("#editListDescription").val();

        let payload2 = {
            userId: userID,
            listId: listID,
            listname: listName,
            listdescription: listDescription
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
        console.log(self.userListsObject())
        //let listID = Object.listID;
        self.getUserListItems(self.userListsObject().listID());
    }

    self.getUserListItems = function (listID, Object) {
        console.log(listID);
        $.ajax({
            url: "/Home/listitems/?listID=" + listID,
            type: "GET",
            success: function (result) {
                self.userListItems.push(result.filter(element => element.listID == listID));
                ko.mapping.fromJS(result, {}, self.userListItemsObject);
                self.listItemValues.removeAll();
                for (var i = 0; i < self.userListItems().length; i++) {
                    self.listItemValues.push(self.userListItems()[i].listItemValue());
                }
                console.log(self.listItemValues());

                //self.userListItems.push(Object.filter(element => element.listID == listID));
                //console.log(self.userListItems());
                //self.userListItems();
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
                toastr.success("List was deleted");
            },
            error: function (jqXHR) {
                toastr.error(jqXHR.responseText);
            }
        })
    }
}