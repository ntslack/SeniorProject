using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeniorProject.Models.Interfaces
{
    public interface IExpenseService
    {
        Task<List<ExpenseDTO>> GetExpensesAsync(int userID);

        Task<ExpenseDTO> GetExpenseByID(int expenseID);

        Task<ExpenseDTO> CreateExpenseAsync(ExpenseDTO expenseDTO);

        Task<ExpenseDTO> UpdateExpenseAsync(ExpenseDTO expenseDTO);

        Task<int> DeleteExpenseAsync(ExpenseDTO expenseDTO);
    }
}
