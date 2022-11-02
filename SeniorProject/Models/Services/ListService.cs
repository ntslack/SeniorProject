using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models.DTOs;
using SeniorProject.Models.Interfaces;
using SeniorProject.Models.Repositories;

namespace SeniorProject.Models.Services
{
    public class ListService : IListService
    {
        private readonly IListRepository _listRepository;

        public ListService(IListRepository listRepository)
        {
            _listRepository = listRepository;
        }

        public Task<List<ListDTO>> GetListsAsync(int userID) => _listRepository.GetListsAsync(userID);

        public Task<ListDTO> GetListByID(int listID) => _listRepository.GetListByID(listID);

        public Task<ListDTO> CreateListAsync(ListDTO listDTO) => _listRepository.CreateListAsync(listDTO);

        public Task<ListDTO> UpdateListAsync(ListDTO listDTO) => _listRepository.UpdateListAsync(listDTO);

        public Task<bool> DeleteListAsync(int listID) => _listRepository.DeleteListAsync(listID);
    }
}
