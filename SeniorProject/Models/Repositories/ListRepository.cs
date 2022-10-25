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
                         join u in _dbcontext.User
                         on l.userID equals u.userID
                         where l.userID == userID
                         orderby l.listCreationDate descending
                         select new ListDTO()
                         {
                             listID = l.listID,
                             listName = l.listName,
                             listDescription = l.listDescription,
                             listCreationDate = l.listCreationDate,
                             listIsFavorited = l.listIsFavorited,
                             userID = l.userID,
                             ListItem = ((ICollection<ListItemDTO>)(
                                          from lI in _dbcontext.ListItem
                                          join l in _dbcontext.List
                                          on lI.listID equals l.listID
                                          select new ListItemDTO()
                                          {
                                              listItemID = lI.listItemID,
                                              listItemValue = lI.listItemValue,
                                              listItemCreationDate = lI.listItemCreationDate,
                                              listID = lI.listItemID,
                                          }))
                         }).ToListAsync();
            return await notes;
        }
    }
}
