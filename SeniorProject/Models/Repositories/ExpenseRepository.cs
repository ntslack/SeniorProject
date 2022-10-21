using SeniorProject.Models.DTOs;
using SeniorProject.Models.Context;
using SeniorProject.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using SeniorProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace SeniorProject.Models.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        DatabaseContext _dbcontext;
        public ExpenseRepository(DatabaseContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<List<ExpenseDTO>> GetExpensesAsync(int userID)
        {
            var expenses = (from e in _dbcontext.Expense
                         select new ExpenseDTO()
                         {
                             expenseID = e.expenseID,
                             expenseTitle = e.expenseTitle,
                             expenseDescription = e.expenseDescription,
                             expenseValue = e.expenseValue,
                             expenseCreationDate = e.expenseCreationDate,
                         }).ToListAsync();
            return await expenses;
        }
    }
}
