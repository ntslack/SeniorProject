using Microsoft.AspNetCore.Http;
using SeniorProject.Models.DTOs;
using SeniorProject.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace SeniorProject.Controllers
{
    [Route("/api/expenses")]
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
            userID = 1;
            List<ExpenseDTO>? expenses = await _expenseService.GetExpensesAsync(userID);
            return Ok(expenses);
        }
    }
}
