$("#search").keyup(function () {
    var searchText = $(this).val().toLowerCase();
    $.each($("table tbody tr"), function () {
        if ($(this).text().toLowerCase().indexOf(searchText) === -1)
            $(this).hide();
        else
            $(this).show();
    });
});

var BudgetViewModel = function (userID, viewModel) {
    var self = this;

    self.mainView = viewModel;

    self.userExpensesObject = ko.observable({
        expenseID: ko.observable(),
        userID: ko.observable(),
        expenseTitle: ko.observable(),
        expenseValue: ko.observable(),
        expenseCreationDate: ko.observable(),
        expenseDescription: ko.observable()
    });

    self.userExpenses = ko.observableArray([]);
    self.selectedExpense = ko.observable();

    self.dismissEditModal = function () {
        $("#editExpenseModal").modal("toggle");
    }

    self.getUserExpenses = function () {
        $.ajax({
            url: "/Home/expenses/?userID=" + userID,
            type: "GET",
            success: function (data) {
                self.userExpenses(data);
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        })
    }

    self.createUserExpense = function (expenseData) {
        $.ajax({
            url: "/Home/expenses",
            type: "POST",
            datatype: "json",
            contentType: "application/json",
            data: JSON.stringify(expenseData),
            success: function (result) {
                if (result == -1) {
                    toastr.error("Expense was not successfully created");
                } else {
                    toastr.success("Expense was created");
                    self.getUserExpenses();
                }
            },
            error: function (result) {
                toastr.error(result);
            }
        })
    }

    self.submitUserExpense = function () {
        var expenseTitle = $("#expenseTitle").val();
        var expenseValue = $("#expenseValue").val();
        var expenseDescription = $("#expenseDescription").val();
        let payload = {
            UserID: userID,
            ExpenseTitle: expenseTitle,
            ExpenseValue: expenseValue,
            ExpenseDescription: expenseDescription
        };
        console.log(payload);
        self.createUserExpense(payload);
        $("#addExpenseModal").modal("toggle");
        return;
    }

    var Id;
    self.editExpense = function (Object) {
        ko.mapping.fromJS(Object, {}, self.userExpensesObject);
        Id = Object.expenseID;
    }

    self.submitEditedUserExpense = function () {
        var form = $("#editExpenseForm");
        var expenseID = Id;
        var expenseTitle = $("#editExpenseTitle").val();
        var expenseValue = $("#editExpenseValue").val();
        var expenseDescription = $("#editExpenseDescription").val();

        let payload2 = {
            userId: userID,
            expenseId: expenseID,
            expensetitle: expenseTitle,
            expensevalue: expenseValue,
            expensedescription: expenseDescription
        }

        self.updateExpense(payload2);
    }

    self.updateExpense = function (payload2) {
        $.ajax({
            url: "/Home/expenses",
            type: "PUT",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(payload2),
            success: function (result) {
                if (result == -1) {
                    toastr.error("Error");
                } else {
                    toastr.success("Successfully Changed Expense");
                    self.getUserExpenses();
                    self.dismissEditModal();
                }
            },
            error: function () {
                toastr.error("Error Updating Expense")
            }
        })
    }

    self.deleteExpense = function (Object) {
        $.ajax({
            url: '/Home/expenses/' + Object.expenseID,
            type: 'DELETE',
            success: function () {
                self.getUserExpenses();
                toastr.success("Expense was deleted");
            },
            error: function (jqXHR) {
                toastr.error(jqXHR.responseText);
            }
        })
    }
}