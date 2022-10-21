using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models.DTOs;
using SeniorProject.Models.Interfaces;

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
    }
}
