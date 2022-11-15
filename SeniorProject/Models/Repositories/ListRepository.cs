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
            var lists = (from l in _dbcontext.List
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
                             //ListItem = ((ICollection<ListItemDTO>)(
                             //             from lI in _dbcontext.ListItem
                             //             join l in _dbcontext.List
                             //             on lI.listID equals l.listID
                             //             select new ListItemDTO()
                             //             {
                             //                 listItemID = lI.listItemID,
                             //                 listItemValue = lI.listItemValue,
                             //                 listItemCreationDate = lI.listItemCreationDate,
                             //                 listID = lI.listID,
                             //             }))
                         }).ToListAsync();
            return await lists;
        }

        public async Task<ListDTO> GetListByID(int listID)
        {
            var list = await (from l in _dbcontext.List
                                where l.listID == listID
                                select new ListDTO()
                                {
                                    listID = l.listID,
                                    listName = l.listName,
                                    listDescription = l.listDescription,
                                    ListItem = ((ICollection<ListItemDTO>)(
                                          from lI in _dbcontext.ListItem
                                          join ll in _dbcontext.List
                                          on lI.listID equals l.listID
                                          select new ListItemDTO()
                                          {
                                              listItemID = lI.listItemID,
                                              listItemValue = lI.listItemValue,
                                              listItemCreationDate = lI.listItemCreationDate,
                                              listID = lI.listID,
                                          }))
                                }).SingleOrDefaultAsync();
            return list;
        }

        public async Task<ListDTO> CreateListAsync(ListDTO listDTO)
        {
            await _dbcontext.AddAsync(listDTO);
            await _dbcontext.SaveChangesAsync();

            return listDTO;
        }

        public async Task<ListDTO> UpdateListAsync(ListDTO listDTO)
        {
            if (listDTO == null)
            {
                throw new ArgumentNullException(nameof(listDTO));
            }

            _dbcontext.Update(listDTO);
            await _dbcontext.SaveChangesAsync();

            return listDTO;
        }

        public async Task<bool> DeleteListAsync(int listID)
        {
            ListDTO listDTO = _dbcontext.List.Find(listID);
            _dbcontext.List.Remove(listDTO);
            await _dbcontext.SaveChangesAsync();

            return true;
        }
    }
}
