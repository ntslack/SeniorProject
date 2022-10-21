using SeniorProject.Models.DTOs;
using SeniorProject.Models.Context;
using SeniorProject.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using SeniorProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace SeniorProject.Models.Repositories
{
    public class ListRepository : IListRepository
    {
        DatabaseContext _dbcontext;
        public ListRepository(DatabaseContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<List<ListDTO>> GetListsAsync(int userID)
        {
            var notes = (from l in _dbcontext.List
                         select new ListDTO()
                         {
                             listID = l.listID,
                             listName = l.listName,
                             listDescription = l.listDescription,
                             listCreationDate = l.listCreationDate,
                             listIsFavorited = l.listIsFavorited,
                             ListItems = ((ICollection<ListItemDTO>)(from lI in _dbcontext.ListItem
                                          select new ListItemDTO()
                                          {
                                              listItemID = lI.listItemID,
                                              listItemValue = lI.listItemValue,
                                              listItemCreationDate = lI.listItemCreationDate
                                          }))
                         }).ToListAsync();
            return await notes;
        }
    }
}
