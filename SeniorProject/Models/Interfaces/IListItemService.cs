using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeniorProject.Models.Interfaces
{
    public interface IListItemService
    {
        Task<List<ListItemDTO>> GetListItemsAsync(int listID);

        Task<ListItemDTO> GetListItemByID(int listItemID);

        Task<ListItemDTO> CreateListItemAsync(ListItemDTO listItemDTO);

        Task<ListItemDTO> UpdateListItemAsync(ListItemDTO listItemDTO);

        Task<bool> DeleteListItemAsync(int listItemID);
    }
}
