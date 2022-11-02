using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models.DTOs;
using SeniorProject.Models.Interfaces;
using SeniorProject.Models.Repositories;

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

        public Task<ExpenseDTO> GetExpenseByID(int expenseID) => _expenseRepository.GetExpenseByID(expenseID);

        public Task<ExpenseDTO> CreateExpenseAsync(ExpenseDTO expenseDTO) => _expenseRepository.CreateExpenseAsync(expenseDTO);

        public Task<ExpenseDTO> UpdateExpenseAsync(ExpenseDTO expenseDTO) => _expenseRepository.UpdateExpenseAsync(expenseDTO);

        public Task<bool> DeleteExpenseAsync(int expenseID) => _expenseRepository.DeleteExpenseAsync(expenseID);
    }
}
