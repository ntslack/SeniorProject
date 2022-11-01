using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeniorProject.Models.Interfaces
{
    public interface IListRepository
    {
        Task<List<ListDTO>> GetListsAsync(int userID);

        Task<ListDTO> GetListByID(int listID);

        Task<ListDTO> CreateListAsync(ListDTO listDTO);

        Task<ListDTO> UpdateListAsync(ListDTO listDTO);

        Task<int> DeleteListAsync(ListDTO listDTO);
    }
}
