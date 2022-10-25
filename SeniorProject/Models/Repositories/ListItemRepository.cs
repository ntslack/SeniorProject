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
    }
}
