﻿using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeniorProject.Models.Interfaces
{
    public interface IListItemRepository
    {
        Task<List<ListItemDTO>> GetListItemsAsync(int listID);
    }
}