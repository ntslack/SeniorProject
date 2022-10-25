using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models.DTOs;
using SeniorProject.Models.Interfaces;

namespace SeniorProject.Models.Services
{
    public class ListItemService : IListItemService
    {
        private readonly IListItemRepository _listItemRepository;

        public ListItemService(IListItemRepository listItemRepository)
        {
            _listItemRepository = listItemRepository;
        }

        public Task<List<ListItemDTO>> GetListItemsAsync(int listID) => _listItemRepository.GetListItemsAsync(listID);
    }
}
