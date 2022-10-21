﻿using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeniorProject.Models.Interfaces
{
    public interface IExpenseService
    {
        Task<List<ExpenseDTO>> GetExpensesAsync(int userID);
    }
}