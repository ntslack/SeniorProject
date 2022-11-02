using Microsoft.AspNetCore.Http;
using SeniorProject.Models.DTOs;
using SeniorProject.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models.Services;

namespace SeniorProject.Controllers
{
    [Route("Home/expenses")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetExpensesAsync(int userID)
        {
            List<ExpenseDTO>? expenses = await _expenseService.GetExpensesAsync(userID);
            return Ok(expenses);
        }

        [HttpGet("{expenseID}")]
        public async Task<IActionResult> GetExpenseByID(int expenseID)
        {
            ExpenseDTO? expense = await _expenseService.GetExpenseByID(expenseID);
            return Ok(expense);
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpenseAsync([FromBody] ExpenseDTO expenseDTO)
        {
            var expense = await _expenseService.CreateExpenseAsync(expenseDTO);
            return Ok(expense);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateExpenseAsync(ExpenseDTO expenseDTO)
        {
            var expense = await _expenseService.UpdateExpenseAsync(expenseDTO);
            return Ok(expense);
        }

        [HttpDelete("{expenseID}")]
        public async Task<IActionResult> DeleteExpenseAsync([FromRoute] int expenseID)
        {
            bool expense = await _expenseService.DeleteExpenseAsync(expenseID);
            return Ok(expense);
        }
    }
}
