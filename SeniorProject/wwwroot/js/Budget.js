$("#search").keyup(function () {
    var searchText = $(this).val().toLowerCase();
    $.each($("table tbody tr"), function () {
        if ($(this).text().toLowerCase().indexOf(searchText) === -1)
            $(this).hide();
        else
            $(this).show();
    });
});

ko.bindingHandlers.sort = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var asc = false;
        element.style.cursor = 'pointer';

        element.onclick = function () {
            var value = valueAccessor();
            var prop = value.prop;
            var data = value.arr;

            asc = !asc;

            data.sort(function (left, right) {
                var rec1 = left;
                var rec2 = right;

                if (!asc) {
                    rec1 = right;
                    rec2 = left;
                }

                var props = prop.split('.');
                for (var i in props) {
                    var propName = props[i];
                    var parenIndex = propName.indexOf('()');
                    if (parenIndex > 0) {
                        propName = propName.substring(0, parenIndex);
                        rec1 = rec1[propName]();
                        rec2 = rec2[propName]();
                    } else {
                        rec1 = rec1[propName];
                        rec2 = rec2[propName];
                    }
                }

                return rec1 == rec2 ? 0 : rec1 < rec2 ? -1 : 1;
            });
        };
    }
};

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

    self.noExpenses = ko.observable(false);
    self.expensesAvailable = ko.observable(false);

    config = {
        enableTime: true,
        dateFormat: "Y-m-d H:i",
    }

    flatpickr("input[type=datetime-local]", config);

    self.dismissEditModal = function () {
        $("#editExpenseModal").modal("toggle");
    }

    self.getUserExpenses = function () {
        $.ajax({
            url: "/Home/expenses/?userID=" + userID,
            type: "GET",
            success: function (response) {
                ko.mapping.fromJS(response, {}, self.userExpenses);
                if (self.userExpenses().length < 1) {
                    self.noExpenses(true);
                } else {
                    self.expensesAvailable(true);
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
                    self.noExpenses(false);
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
        var expenseCreationDate = new Date($("#expenseCreationDate").val());
        var newExpenseCreationDate = new Date(expenseCreationDate.getTime() - (expenseCreationDate.getTimezoneOffset() * 60000)).toJSON();
        let payload = {
            UserID: userID,
            ExpenseTitle: expenseTitle,
            ExpenseValue: expenseValue,
            ExpenseDescription: expenseDescription,
            ExpenseCreationDate: newExpenseCreationDate
        };
        self.createUserExpense(payload);
        $("#addExpenseModal").modal("toggle");
        $("#addExpenseModal").on("hidden.bs.modal", function () {
            $("#addExpenseForm").find("input, textarea, select").val('').end();
        });
        return;
    }

    var Id;
    self.editExpense = function (Object) {
        ko.mapping.fromJS(Object, {}, self.userExpensesObject);
        Id = Object.expenseID();
    }

    self.submitEditedUserExpense = function () {
        var form = $("#editExpenseForm");
        var expenseID = Id;
        var expenseTitle = $("#editExpenseTitle").val();
        var expenseValue = $("#editExpenseValue").val();
        var expenseDescription = $("#editExpenseDescription").val();
        var expenseCreationDate = new Date($("#editExpenseCreationDate").val());
        var newExpenseCreationDate = new Date(expenseCreationDate.getTime() - (expenseCreationDate.getTimezoneOffset() * 60000)).toJSON();

        let payload2 = {
            userId: userID,
            expenseId: expenseID,
            expensetitle: expenseTitle,
            expensevalue: expenseValue,
            expensedescription: expenseDescription,
            expensecreationdate: newExpenseCreationDate
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
            url: '/Home/expenses/' + Object.expenseID(),
            type: 'DELETE',
            success: function () {
                self.getUserExpenses();
                toastr.success("Expense was deleted");
                location.reload();
            },
            error: function (jqXHR) {
                toastr.error(jqXHR.responseText);
            }
        })
    }
}