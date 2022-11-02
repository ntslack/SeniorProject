using SeniorProject.Models.DTOs;
using SeniorProject.Models.Context;
using SeniorProject.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using SeniorProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace SeniorProject.Models.Repositories
{
    public class ListItemRepository : IListItemRepository
    {
        DatabaseContext _dbcontext;
        public ListItemRepository(DatabaseContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<List<ListItemDTO>> GetListItemsAsync(int listID)
        {
            var listItems = (from lI in _dbcontext.ListItem
                             select new ListItemDTO()
                             {
                                 listItemID = lI.listItemID,
                                 listItemValue = lI.listItemValue,
                                 listItemCreationDate = lI.listItemCreationDate,
                                 listID = lI.listItemID
                             }).ToListAsync();
            return await listItems;
        }

        public async Task<ListItemDTO> GetListItemByID(int listItemID)
        {
            var listItem = await (from lI in _dbcontext.ListItem
                                where lI.listItemID == listItemID
                                select new ListItemDTO()
                                {
                                    listItemID = lI.listItemID,
                                    listItemValue = lI.listItemValue
                                }).SingleOrDefaultAsync();
            return listItem;
        }

        public async Task<ListItemDTO> CreateListItemAsync(ListItemDTO listItemDTO)
        {
            await _dbcontext.AddAsync(listItemDTO);
            await _dbcontext.SaveChangesAsync();

            return listItemDTO;
        }

        public async Task<ListItemDTO> UpdateListItemAsync(ListItemDTO listItemDTO)
        {
            if (listItemDTO == null)
            {
                throw new ArgumentNullException(nameof(listItemDTO));
            }

            _dbcontext.Update(listItemDTO);
            await _dbcontext.SaveChangesAsync();

            return listItemDTO;
        }

        public async Task<bool> DeleteListItemAsync(int listItemID)
        {
            ListItemDTO listItemDTO = _dbcontext.ListItem.Find(listItemID);
            _dbcontext.ListItem.Remove(listItemDTO);
            await _dbcontext.SaveChangesAsync();

            return true;
        }
    }
}
