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
                            join u in _dbcontext.User
                            on e.userID equals u.userID
                            where e.userID == userID
                            orderby e.expenseCreationDate descending
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

        public async Task<ExpenseDTO> GetExpenseByID(int expenseID)
        {
            var expense = await (from e in _dbcontext.Expense
                                where e.expenseID == expenseID
                                select new ExpenseDTO()
                                {
                                    expenseID = e.expenseID,
                                    expenseTitle = e.expenseTitle,
                                    expenseDescription = e.expenseDescription,
                                    expenseValue = e.expenseValue
                                }).SingleOrDefaultAsync();
            return expense;
        }

        public async Task<ExpenseDTO> CreateExpenseAsync(ExpenseDTO expenseDTO)
        {
            await _dbcontext.AddAsync(expenseDTO);
            await _dbcontext.SaveChangesAsync();

            return expenseDTO;
        }

        public async Task<ExpenseDTO> UpdateExpenseAsync(ExpenseDTO expenseDTO)
        {
            if (expenseDTO == null)
            {
                throw new ArgumentNullException(nameof(expenseDTO));
            }

            _dbcontext.Update(expenseDTO);
            await _dbcontext.SaveChangesAsync();

            return expenseDTO;
        }

        public async Task<int> DeleteExpenseAsync(ExpenseDTO expenseDTO)
        {
            _dbcontext.Set<ExpenseDTO>().Remove(expenseDTO);
            await _dbcontext.SaveChangesAsync();

            return 0;
        }
    }
}
