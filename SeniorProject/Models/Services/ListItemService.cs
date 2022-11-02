using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models.DTOs;
using SeniorProject.Models.Interfaces;
using SeniorProject.Models.Repositories;

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

        public Task<ListItemDTO> GetListItemByID(int listItemID) => _listItemRepository.GetListItemByID(listItemID);

        public Task<ListItemDTO> CreateListItemAsync(ListItemDTO listItemDTO) => _listItemRepository.CreateListItemAsync(listItemDTO);

        public Task<ListItemDTO> UpdateListItemAsync(ListItemDTO listItemDTO) => _listItemRepository.UpdateListItemAsync(listItemDTO);

        public Task<bool> DeleteListItemAsync(int listItemID) => _listItemRepository.DeleteListItemAsync(listItemID);
    }
}
