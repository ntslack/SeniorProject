using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models.DTOs;
using SeniorProject.Models.Interfaces;

namespace SeniorProject.Models.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseService(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public Task<List<ExpenseDTO>> GetExpensesAsync(int userID) => _expenseRepository.GetExpensesAsync(userID);
    }
}
