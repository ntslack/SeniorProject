$("#search").keyup(function () {
    var searchText = $(this).val().toLowerCase();
    $.each($("table tbody tr"), function () {
        if ($(this).text().toLowerCase().indexOf(searchText) === -1)
            $(this).hide();
        else
            $(this).show();
    });
});

var BudgetViewModel = function (userID) {
    var self = this;

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
                    toastr.success(expenseData.expenseTitle + " was created");
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
        self.createUserExpense(payload);
        $("#addExpenseModal").modal("toggle");
        return;
    }

    self.deleteExpense = function (Object) {
        $.ajax({
            url: '/Home/expenses/' + Object.expenseID,
            type: 'DELETE',
            success: function () {
                //self.userExpenses.remove(selectedExpense);
                self.getUserExpenses();
                toastr.success("Expense " + Object.expenseTitle + " was deleted");
            },
            error: function (jqXHR) {
                toastr.error(jqXHR.responseText);
            }
        })
    }
}